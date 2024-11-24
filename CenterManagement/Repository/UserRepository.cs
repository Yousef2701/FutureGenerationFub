using CenterManagement.Data;
using CenterManagement.IRepository;
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

    }
}
