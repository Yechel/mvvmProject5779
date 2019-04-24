using FL_Project.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FL_Project.Model
{
    public class AccurateFallLocation : FallLocation
    {
        private string picPath;

        public AccurateFallLocation(int id, string adress, string date, int numberOfFalls, string picPath) : base(id,adress, date, numberOfFalls)
        {
            PicPath = picPath;
        }

        public AccurateFallLocation(string date, string picPat, Action sucsess, Action fail)
        {
            try
            {
                ID = Settings.Default.fallLocationCounter;
                Settings.Default.fallLocationCounter++;
                this.Date = date;
                NumberOfFalls = 1;
                string destinationFile = Settings.Default.PicPathLocation;
                destinationFile = System.IO.Path.Combine(destinationFile,"AFL_"+ ID + ".jpg");
                System.IO.File.Copy(picPat, destinationFile, true);
                Adress = FallLocationService.getAdressFromPath(picPath);
                PicPath = picPath;
                sucsess();
            }
            catch(Exception e)
            {
                fail();
            }
        }



        public string PicPath
        {
            get
            {
                return picPath;
            }
            set
            {
                Set<string>(() => this.PicPath, ref picPath, value);
            }
        }
    }
}
