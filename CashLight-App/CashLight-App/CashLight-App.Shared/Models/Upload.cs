using CashLight_App.Models.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Models
{
    public class Upload : ObservableObject, IUpload
    {
        public StorageFile file
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
