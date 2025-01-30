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

        #region Get Barcodes Count

        public async Task<int> GetBarcodesCount()
        {
            return _context.Barcodes.Count();
        }

        #endregion

        #region Create Barcodes

        public async Task<int[]> CreateBarcodes(EnrollParcodeVM model)
        {
            if(model != null)
            {
                int n = Convert.ToInt32(model.Numbre);

                Random random = new Random();
                int[] randomNumbers = new int[n];

                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = random.Next(10000000, 99999999);

                    var barcode = new Barcode
                    {
                        barcode = randomNumbers[i].ToString(),
                        TeacherId = model.TeacherId,
                        AcademyYear = model.AcademyYear,
                        Month = model.Month,
                        StudendId = "#"
                    };
                    _context.Barcodes.Add(barcode);
                    _context.SaveChanges();
                }

                return randomNumbers;
            }
            return null;
        }

        #endregion

        #region Check Barcode

        public async Task<bool> CheckBarcode(MonthlyBarcodeVM model)
        {
            var check = _context.Barcodes.Where(m => m.barcode == model.Barcode & m.TeacherId == model.TeacherId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).FirstOrDefault();
            if(check != null)
            {
                string id = await _userRepository.GitLoggingUserId();

                if (check.StudendId == "#")
                {
                    check.StudendId = id;
                    _context.Barcodes.Update(check);
                    _context.SaveChanges();

                    return true;
                }
                else if(check.StudendId == id)
                {
                    return true;
                } 
            }
            return false;
        }

        #endregion

    }
}
