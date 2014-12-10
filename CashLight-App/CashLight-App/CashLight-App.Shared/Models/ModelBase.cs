using CashLight_App.Services.Interface;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class ModelBase
    {
        protected static IUnitOfWork _unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
    }
}
