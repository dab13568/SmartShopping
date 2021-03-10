using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LastDate : INotifyPropertyChanged
    {
        private int _Id;

        public event PropertyChangedEventHandler PropertyChanged;

        public string lastDate { get; set; }
        public LastDate()
        {
            lastDate = "1990-01-01";
        }
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));

            }
        }
    }
}
