using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.ViewModels;
using System.Security.Claims;

namespace CenterManagement.Repository
{
    public class UserRepository : IUserRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion


        #region Get Logging User Id

        public async Task<string> GitLoggingUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
                return userId;
            else
                return string.Empty;
        }

        #endregion

        #region Get Padding Accounts Count

        public async Task<int> GetPaddingAccountsCount()
        {
            return _context.Users.Where(m => m.EmailConfirmed == false).Count();
        }

        #endregion

        #region Padding User Account

        public async Task<string> PaddingUserAccount(string userId)
        {
            if(userId != null)
            {
                var user = _context.Users.Find(userId);
                user.EmailConfirmed = false;
                _context.Update(user);
                _context.SaveChanges();

                return "Success";
            }
            return "User Id Is Null!";
        }

        #endregion

        #region Get Padding Users List

        public async Task<IEnumerable<string>> GetPaddingUsersList()
        {
            var padding = _context.Users.Where(m => m.EmailConfirmed != true).ToList();
            List<string> usernames = new List<string>();

            foreach (var user in padding)
            {
                string username = _context.Users.Where(m => m.Id == user.Id).Select(m => m.UserName).FirstOrDefault();
                usernames.Add(username);
            }

            return usernames;
        }

        #endregion

        #region Active User Account

        public async Task<string> ActiveUserAccount(UsernameVM model)
        {
            if(model != null)
            {
                var user = _context.Users.Where(m => m.UserName == model.Username).FirstOrDefault();
                if (user != null)
                {
                    user.EmailConfirmed = true;
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return "Success";
                }
            }
            return "Account is Invalid!";
        }

        #endregion

    }
}
