namespace CenterManagement.IRepository
{
    public interface IUserRepository
    {

        public Task<string> GitLoggingUserId();

    }
}
