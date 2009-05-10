using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Zuse.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public UIntPtr dwData;
        public int cbData;
        public IntPtr data;
    }

    public static class Win32
    {
        public static readonly int WM_COPYDATA = 74;
    }
}
