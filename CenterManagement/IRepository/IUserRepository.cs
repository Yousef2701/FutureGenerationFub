using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface IUserRepository
    {

        public Task<string> GitLoggingUserId();

        public Task<int> GetPaddingAccountsCount();

        public Task<string> PaddingUserAccount(string userId);

        public Task<IEnumerable<string>> GetPaddingUsersList();

        public Task<string> ActiveUserAccount(UsernameVM model);

    }
}
