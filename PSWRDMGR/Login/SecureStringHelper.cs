using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PSWRDMGR.Login
{
    public static class SecureStringHelper
    {
        public static string GetUnsecure(this SecureString secure)
        {
            if (secure == null)
                return "";
            IntPtr unmanaged = IntPtr.Zero;
            try
            {
                unmanaged = Marshal.SecureStringToGlobalAllocUnicode(secure);
                return Marshal.PtrToStringUni(unmanaged);
            }
            catch { return ""; }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(unmanaged); }
        }
    }
}
