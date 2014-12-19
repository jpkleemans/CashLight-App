using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Models.Interface
{
    public interface IUpload
    {
        StorageFile file { get; set; }
    }
}
