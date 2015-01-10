using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Repositories.Interfaces
{
    public interface IUploadRepository
    {
        //Upload GetByFile(StorageFile file);

        void ToDatabase(StorageFile storageFile);

        void SaveTransaction(Dictionary<string, string> dic);
    }
}
