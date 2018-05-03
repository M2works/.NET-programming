using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class Patient : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string phoneNumber;
        private string pesel;
        private Address address;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public string FullName
        {
            get
            {
                return name.First() + ". " + surname;
            }
        }


        public Patient(string name, string surname, string phoneNumber, string pesel, Address address)
        {
            this.Name = name;
            this.Surname = surname;
            this.PhoneNumber = phoneNumber;
            this.PESELNumber = pesel;
            this.Address = address;
        }

        public Patient(DBConnectionLogic.SurgeryModel.Patient p)
        {
            this.Name = p.Name;
            this.Surname = p.Surname;
            this.PhoneNumber = p.PhoneNumber;
            this.PESELNumber = p.PESELNumber;
            this.Address = p.Address;
        }
        public Patient() { address = new Address(); address.Country = "Polska"; }

        public string Name
        {
            get { return this.name; }
            set
            {
                if(value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get { return this.surname; }
            set
            {
                if (value != this.surname)
                {
                    this.surname = value;
                    NotifyPropertyChanged("Surname");
                }
            }
        }

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set
            {
                if (value != this.phoneNumber)
                {
                    this.phoneNumber = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
            }
        }

        public string PESELNumber
        {
            get { return this.pesel; }
            set
            {
                if (value != this.pesel)
                {
                    this.pesel = value;
                    NotifyPropertyChanged("PESELNumber");
                }
            }
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

        public void NotifyFullName()
        {
            NotifyPropertyChanged("FullName");
        }
    }
}
