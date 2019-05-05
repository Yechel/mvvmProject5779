using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Report
    {
        //public Report(int id, DateTime time, string name, string report_Adress, int boom_count, string imagePath, double lat, double log)
        //{
        //    this.Id = id;
        //    Time = time;
        //    Name = name;
        //    Report_Adress = report_Adress;
        //    Boom_count = boom_count;
        //    ImagePath = imagePath;
        //    this.lat = lat;
        //    this.log = log;
        //}

        public int Id { get; set; }
        public int Report_Id { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string Report_Adress { get; set; }
        public int Boom_count { get; set; }
        public string ImagePath { get; set; }
        public double lat { get; set; }
        public double log { get; set; }


    }
}
