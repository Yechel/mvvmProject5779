using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface Ibl
    {
    
        bool AddReport(Report report);
        bool RemoveReport(int id);
        bool UpdateReport(Report report);
        Report GetReportById(int id);
        List<Report> getReportList();

       bool AddDrop(Drop drop);
       bool RemoveDrop(int id);
       bool UpdateDrop(Drop Drop);
       Drop GetDropById(int id);
        //
       double[] GetCoordinate(string address);//צריך לממש ולהחזיר LAT LOG
        //
       double[] GetCoordinateFromExif(string imagePath);//צריך לממש ולהחזיר LAT LOG

        //more functions
        #region helpFunctions
        //bool RemoveAllDrops();

        //return list of all the drops from DataSource
        List<Drop> getDropsList();

        //return list of all the AccurateDrops from DataSource
       List<Drop> getAccurateDropList();

        //taking list of drops and return a drop with estimate drop
         List<Drop> CalculateEstimateDrop(List<Report> report_list);

        //return the distance from the acuurateDrop to the estimate drop 
        float EvaluateDistance(Drop d1, Drop d2);

        //get address and return location (lat and long) return null if not success
        //יש כבר פונקציה כזו: GetCoordinate 
        double[] getLatAndLong(string adress);

        //return the picture of the given drop
        string getPath(Drop id);

        #endregion


    }

}
