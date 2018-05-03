using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class Visit : INotifyPropertyChanged
    {
        private Patient patient;
        private DateTime time;

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (value != this.patient)
                {
                    this.patient = value;
                    NotifyPropertyChanged("Patient");
                }
            }
        }
        public Doctor Doctor { get; }        
        public string Time
        {
            get
            {
                return time.ToShortTimeString();
            }
        }
        public string Date
        {
            get
            {
                return time.ToShortDateString();
            }
        }
        public DateTime DT { get { return time; } }
        public Visit(Doctor d, DateTime t, Patient p=null)
        {
            time = t;
            Doctor = d;
            Patient = p;
        }
        public Visit(DBConnectionLogic.SurgeryModel.VisitTime v)
        {
            time = v.Date;
            Doctor = new Doctor(v.Doctor);
            Patient = new Patient(v.Patient);
        }

        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
