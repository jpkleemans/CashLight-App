using CashLight_App.Models;
using CashLight_App.Services.CSV.Banks;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Repositories.Interfaces
{
    public interface IUploadRepository
    {
        //Upload GetByFile(StorageFile file);

        void ToDatabase(IBank bank, StorageFile storageFile);
    }
}
