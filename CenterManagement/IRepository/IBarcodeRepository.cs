﻿using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface IBarcodeRepository
    {

        public Task<IEnumerable<Barcode>> GetBarcodes(AcademYearVM model);

    }
}
