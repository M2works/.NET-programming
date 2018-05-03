using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class Clinic : INotifyPropertyChanged
    {
        private Address address;
        private string name;

        public Clinic(Address address, string name)
        {
            Address = address;
            Name = name;
        }
        public Clinic(DBConnectionLogic.SurgeryModel.Clinic c)
        {
            Address = c.Address;
            Name = c.Name;
        }

        public Clinic() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public Address Address
        {
            get { return this.address; }
            set
            {
                if (value != this.address)
                {
                    this.address = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
    }
}
