using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class Doctor: INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private Clinic clinic;
        private Specialization specialization;
        private int visitPrice;
       
        public Doctor()
        {

        }

        public Doctor(string name, string surname, Clinic clinic, Specialization specialization, int price)
        {
            Name = name;
            Surname = surname;
            Clinic = clinic;
            Specialization = specialization;
            VisitPrice = price;
        }

        public Doctor(DBConnectionLogic.SurgeryModel.Doctor dr)
        {
            Name = dr.Name;
            Surname = dr.Surname;
            if (dr.Clinic == null)
                Clinic = new Clinic();
            else
                Clinic = new Clinic(dr.Clinic);
            Specialization = dr.Specialization;
            VisitPrice = dr.VisitPrice == null ? 0 : (int)dr.VisitPrice;
            LicenseNumber = dr.LicenceNumber;
        }

        public string LicenseNumber { get; set; }

        public string FullName
        {
            get
            {
                return (SpecializationPL)specialization + " - " + name.First() + ". " + surname;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public Specialization Specialization
        {
            get { return this.specialization; }
            set
            {
                if (value != this.specialization)
                {
                    this.specialization = value;
                    NotifyPropertyChanged("Specialization");
                }
            }
        }

        public int VisitPrice
        {
            get { return this.visitPrice; }
            set
            {
                if (value != this.visitPrice)
                {
                    this.visitPrice = value;
                    NotifyPropertyChanged("VisitPrice");
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

        public Clinic Clinic
        {
            get { return this.clinic; }
            set
            {
                if (value != this.clinic)
                {
                    this.clinic = value;
                    NotifyPropertyChanged("Clinic");
                }
            }
        }
    }
}
