using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Drop
    {

        //public Drop(int id,int drop_Id, string drop_Adress, DateTime drop_time, List<Report> reports, double real_lat, double real_log, double estimeated_lat, double estimeated_log)
        //{
        //    Id= id;
        //    Drop_Id = drop_Id;
        //    Drop_Adress = drop_Adress;
        //    Drop_time = drop_time;
        //    Reports = reports;
        //    Real_lat = real_lat;
        //    Real_log = real_log;
        //    Estimeated_lat = estimeated_lat;
        //    Estimeated_log = estimeated_log;
        //}
        public int Id { get; set; }
        public int Drop_Id { get; set; }
        public string Drop_Adress { get; set; }
        public DateTime Drop_time { get; set; }
        //delete report
        public List<Report> Reports_list { get; set; }
        public double Real_lat { get; set; }

        public double Real_log { get; set; }
        public double Estimeated_lat { get; set; }
        public double Estimeated_log { get; set; }
        public string ImagePath{get; set;}

        // private  string GPS_Coordinates { get; set; }
        //  private struct Lat_Long {public double Latitude;  public double Longitude;}  


    }
}
