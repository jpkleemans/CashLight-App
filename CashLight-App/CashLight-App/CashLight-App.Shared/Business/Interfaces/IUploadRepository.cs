﻿using CashLight_App.Models;
using CashLight_App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Business.Interfaces
{
    public interface IUploadRepository
    {
        //Upload GetByFile(StorageFile file);

        void ToDatabase(IBank bank, StorageFile storageFile);
    }
}
