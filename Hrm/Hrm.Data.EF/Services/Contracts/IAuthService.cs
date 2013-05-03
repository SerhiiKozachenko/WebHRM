using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Services.Contracts
{
    public interface IAuthService
    {
        string EncryptPassword(string clearPassword);

        string DecryptPassword(string encryptedPassword);

        bool IsUserExist(string login, string clearPassword);

        bool SignIn(string login, string password, bool rememberMe);

        void SignOut();

        void Register(User user);

        bool IsUserNameExist(string username);

        bool IsEmailExist(string username);
    }
}