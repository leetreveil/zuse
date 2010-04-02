using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zuse.Setup.Core
{
    public delegate void MethodEvent(object sender, string message);
    public delegate void MethodError(object sender, string message);
    public delegate void MethodMessage(object sender, string message);

    public interface IMethod
    {
        event MethodMessage MethodMessage;
        event MethodError MethodError;
        event MethodEvent MethodEvent;

        void Install();
        void Uninstall();
    }
}
