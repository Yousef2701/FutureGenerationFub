using System.Security.Claims;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.Repository
{
    public class BarcodeRepository : IBarcodeRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public BarcodeRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        #endregion


        #region Get Barcodes

        public async Task<IEnumerable<Barcode>> GetBarcodes(AcademYearVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();

                var barcodes = _context.Barcodes.Where(m => m.TeacherId == userId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).ToList();
                return barcodes;
            }
            return null;
        }

        #endregion

    }
}
