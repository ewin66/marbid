using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("driver")]
    public class Driver : BaseObject
    {
        private Marbid.Module.BusinessObjects.CRM.Car _car;
        private Marbid.Module.BusinessObjects.Administration.Employee _driverName;
        public Driver(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public Marbid.Module.BusinessObjects.Administration.Employee DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                SetPropertyValue("DriverName", ref _driverName, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("Schedules-AssignedDriver")]
        public XPCollection<Marbid.Module.BusinessObjects.CRM.Schedule> Schedules
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.CRM.Schedule>("Schedules");
            }
        }
        public Marbid.Module.BusinessObjects.CRM.Car Car
        {
            get
            {
                return _car;
            }
            set
            {
                if (_car == value)
                    return;
                Marbid.Module.BusinessObjects.CRM.Car prevCar = _car;
                _car = value;
                if (IsLoading)
                    return;
                if (prevCar != null && prevCar.DefaultDriver == this)
                    prevCar.DefaultDriver = null;
                if (_car != null)
                    _car.DefaultDriver = this;
                OnChanged("Car");
            }
        }
    }
}
