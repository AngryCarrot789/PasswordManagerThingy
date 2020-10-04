using System.Security;

namespace PSWRDMGR.Login
{
    public interface IPassword
    {
        SecureString Password { get; }
    }
}
