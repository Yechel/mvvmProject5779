using GalaSoft.MvvmLight;
using FL_Project.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL_Project.Model
{
    public class FallLocation : ObservableObject
    {


        protected int id;
        protected string adress;
        protected string date;
        protected int numberOfFalls;

        public FallLocation(string adress, string date, int numberOfFalls, Action sucsess, Action fail)
        {
            try
            {
                FallLocation fl = new FallLocation(adress, date, numberOfFalls);
                ID = fl.ID;
                Adress = fl.Adress;
                Date = fl.Date;
                NumberOfFalls = fl.NumberOfFalls;
                sucsess();
            }
            catch
            {
                fail();
            }
        }

        public FallLocation(FallLocation fl)
        {
                ID = fl.ID;
                Adress = fl.Adress;
                Date = fl.Date;
                NumberOfFalls = fl.NumberOfFalls;
        }




        public FallLocation(string adress, string date, int numberOfFalls)
        {
            try
            {
                ID = Settings.Default.fallLocationCounter;
                Settings.Default.fallLocationCounter++;
                Adress = adress;
                Date = date;
                NumberOfFalls = numberOfFalls;
            }
            catch
            {
                throw new Exception("fail to constuct new fall location");
            }
        }

        public FallLocation(int id, string adress, string date, int numberOfFalls)
        {
            try
            {
                ID = id;
                if (Settings.Default.fallLocationCounter <= id)
                {
                    Settings.Default.fallLocationCounter = id + 1;
                }
                Adress = adress;
                Date = date;
                NumberOfFalls = numberOfFalls;
            }
            catch
            {
                throw new Exception("fail to constuct new fall location");
            }
        }

        public FallLocation()
        {
            ID = Settings.Default.fallLocationCounter;
            Settings.Default.fallLocationCounter++;
        }



        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                Set<int>(() => this.ID, ref id, value);
            }
        }

        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                Set<string>(() => this.Adress, ref adress, value);
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                Set<string>(() => this.Date, ref date, value);
            }
        }

        public int NumberOfFalls
        {
            get
            {
                return numberOfFalls;
            }
            set
            {
                Set<int>(() => this.NumberOfFalls, ref numberOfFalls, value);

            }
        }



    }
}
