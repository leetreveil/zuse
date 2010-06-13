/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2009 Zachary Howe
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

using Mono.Cecil;
using Mono.Cecil.Binary;
using Mono.Cecil.Cil;

namespace Zuse.Setup.Core.Methods
{
    class Method1 : IMethod
    {
        public event MethodMessage MethodMessage;
        public event MethodError MethodError;
        public event MethodEvent MethodEvent;

        public Method1()
        {
        }

        public void Install()
        {
            if (!File.Exists(Consts.ZuneAssemblyLocation + ".original"))
            {
                if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Backing up original {0:s} as {0:s}.original", Path.GetFileName(Consts.ZuneAssemblyLocation)));
                File.Copy(Consts.ZuneAssemblyLocation, Consts.ZuneAssemblyLocation + ".original", false);
            }
            else
            {
                if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Found original {0:s}: {0:s}.original", Path.GetFileName(Consts.ZuneAssemblyLocation)));
                File.Delete(Consts.ZuneAssemblyLocation);
            }

            if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Patching original file: {0:s}.original", Path.GetFileName(Consts.ZuneAssemblyLocation)));
            AssemblyDefinition zuneShellAssembly = AssemblyFactory.GetAssembly(Consts.ZuneAssemblyLocation + ".original");
            AssemblyDefinition duubiAssembly = AssemblyFactory.GetAssembly("Duubi.dll");

            MethodDefinition startMethod = null;

            foreach (TypeDefinition type in duubiAssembly.MainModule.Types)
            {
                if (type.Name != "<Module>")
                {
                    foreach (MethodDefinition method in type.Methods)
                    {
                        string methodName = method.Name;
                        string methodType = method.DeclaringType.Name;
                        string methodTypeNamespace = method.DeclaringType.Namespace;

                        if (methodTypeNamespace == "Zuse" && methodType == "Program" && methodName == "Start")
                        {
                            startMethod = method;
                        }
                    }
                }
            }

            // install Zuse's references...
            string[] banned_refs = new string[] { "mscorlib", "System*", "Zune*", "UIX" };

            File.Copy("Duubi.dll", @"C:\Program Files\Zune\Zuse.dll", true);

            foreach (AssemblyNameReference asmref in duubiAssembly.MainModule.AssemblyReferences)
            {
                string ref_name = asmref.Name;
                string ref_file = ref_name + ".dll";

                bool doContinue = false;

                foreach (string banned_ref in banned_refs)
                {
                    if (banned_ref.EndsWith("*"))
                    {
                        if (ref_name.StartsWith(banned_ref.Substring(0, banned_ref.Length - 1)))
                        {
                            doContinue = true;
                            break;
                        }
                    }
                    else
                    {
                        if (ref_name == banned_ref)
                        {
                            doContinue = true;
                            break;
                        }
                    }
                }

                if (doContinue) continue;

                try
                {
                    if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Installing Reference: {0:s}", ref_file));
                    File.Copy(ref_file, @"C:\Program Files\Zune\" + ref_file, true);
                }
                catch (Exception e)
                {
                    if (this.MethodError != null) this.MethodError(this, "Exception: " + e.Message);
                }
            }

            if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Patching file: {0:s}", Consts.ZuneAssemblyLocation));
            foreach (TypeDefinition type in zuneShellAssembly.MainModule.Types)
            {
                if (type.Name != "<Module>")
                {
                    foreach (MethodDefinition method in type.Methods)
                    {
                        string methodName = method.Name;
                        string methodType = method.DeclaringType.Name;
                        string methodTypeNamespace = method.DeclaringType.Namespace;

                        if (methodTypeNamespace == "ZuneUI" && methodType == "Shell" && methodName == "InitializeInstance")
                        {
                            if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Patching method: {0:s}.{1:s}.{2:s}", methodTypeNamespace, methodType, methodName));

                            CilWorker worker = method.Body.CilWorker;

                            MethodReference duubiStart = null;
                            duubiStart = zuneShellAssembly.MainModule.Import(startMethod);

                            Instruction callDuubiStart = null;
                            callDuubiStart = worker.Create(OpCodes.Call, duubiStart);

                            Instruction ins = method.Body.Instructions[0];

                            worker.InsertBefore(ins, callDuubiStart);
                        }
                    }
                }
            }

            Environment.CurrentDirectory = Path.GetDirectoryName(Consts.ZuneAssemblyLocation);
            if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Switched current directory to: {0:s}", Environment.CurrentDirectory));

            string output_asm = Consts.ZuneAssemblyLocation;

            if (this.MethodMessage != null) this.MethodMessage(this, string.Format("Outputing new patched executable: {0:s}", output_asm));

            AssemblyFactory.SaveAssembly(zuneShellAssembly, output_asm);
        }

        public void Uninstall()
        {
            if (this.MethodMessage != null) this.MethodMessage(this, "Deleting modified ZuneShell.dll");
            File.Delete(Consts.ZuneAssemblyLocation);
            if (this.MethodMessage != null) this.MethodMessage(this, "Renaming original unmodified ZuneShell.dll.original to ZuneShell.dll");
            File.Move(Consts.ZuneAssemblyLocation + ".original", Consts.ZuneAssemblyLocation);
        }
    }
}
