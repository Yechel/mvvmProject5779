using BE;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_imp : Idal
    {
        #region DROP
        public bool AddDrop(Drop drop)
        {
            try
            {
                using (var db = new Entitys())
                {


                    var newDropToAdd = new Drop
                    {
                        Id = drop.Id,
                        Drop_Id = drop.Drop_Id,
                        Drop_Adress = drop.Drop_Adress,
                        Drop_time = drop.Drop_time,
                        Reports_list = drop.Reports_list,
                        Real_lat = drop.Real_lat,
                        Real_log = drop.Real_log,
                        Estimeated_lat = drop.Estimeated_lat,
                        Estimeated_log = drop.Estimeated_log,
                    };


                    db.Drops.Add(newDropToAdd);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }// done
        public bool RemoveDrop(int id)
        {
            try
            {
                using (var db = new Entitys())
                {
                    //var dropToRemove = (from d in db.Drops where d.Id == id select d).First();
                    // Load one blogs and its related posts
                    // var dropToRemove = db.Drops.Single(d => d.Drop_Id == id);
                    var dropToRemove = db.Drops
                                       .Where(d => d.Drop_Id == id)
                                       .Include(r => r.Reports_list)
                                       .FirstOrDefault();
                    // var dropToRemove = (   from d in db.Drops where d.Id == id select d).FirstOrDefault();

                    db.Drops.Remove(dropToRemove);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }     // done
        public bool UpdateDrop(Drop Drop)
        {
            try
            {
                using (var db = new Entitys())
                {
                    var dropToUpdate = (from d in db.Drops
                                          where d.Drop_Id == Drop.Drop_Id
                                          select d).First();
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }  // done
        public Drop GetDrop(int id)
        {
            ////////////////////////////
            /////////////////////////////
            //using (DbConnection connection = new SqlConnection(@"data source=LAPTOP-438HUAJF\SQLEXPRESS; initial catalog=wpf_MVVM_EntityFramework;integrated security=SSPI"))
            //{
            //    connection.Open();
            //    using (DbCommand command = new SqlCommand("alter table [Drops] DROP COLUMN  [ImagePath]  "))
            //    {
            //        command.Connection = connection;
            //        command.ExecuteNonQuery();
            //    }
            //}
            ///////////////////////////
            ///////////////////////////
            //using (var db =  new Entitys())
            //{
            //    var drop = (from d in db.Drops where d.Drop_Id == id select d).First();
            //    return new Drop
            //    {
            //        Id = drop.Id,
            //        Drop_Id = drop.Drop_Id,
            //        Drop_Adress = drop.Drop_Adress,
            //        Drop_time = drop.Drop_time,
            //        Reports_list = drop.Reports_list,
            //        Real_lat = drop.Real_lat,
            //        Real_log = drop.Real_log,
            //        Estimeated_lat = drop.Estimeated_lat,
            //        Estimeated_log = drop.Estimeated_log,

            //    };
            //}
            using (var db = new Entitys())
            {
                var dropToReturn = (from d in db.Drops where d.Drop_Id == id select d).First();
                return dropToReturn;
            }
        }        // done
        public List<Drop> getDropList()
        {
            using (var db = new  Entitys())
            {
                var drops = (from d in db.Drops  select d);
                return drops.ToList<Drop>();
            }
        }
        //public bool RemoveAllDrops()
        //{
        //    try
        //    {
        //        using (var db = new Entitys())
        //        {
                   
        //            var dropsToRemove = (from a in db.Drops select a);
        //            db.Drops.RemoveRange(dropsToRemove);
        //            db.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region REPORT
        public bool AddReport(Report report)
        {
            try
            {
                using (var db = new Entitys())
                {


                    var newReportToAdd = new Report
                    {
                        Id = 1,
                        Report_Id = report.Report_Id,
                        Time = report.Time,
                        Name = report.Name,
                        Report_Adress = report.Report_Adress,
                        Boom_count = report.Boom_count,
                        ImagePath = report.ImagePath,
                        lat = report.lat,
                        log = report.log
                    };
                    db.Reports.Add(newReportToAdd);
                    db.SaveChanges();


                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        } //done
        public bool UpdateReport(Report Report)
        {
            try
            {
                using (var db = new Entitys())
                {
                    var ReportToUpdate = (from r in db.Reports
                                where r.Report_Id == Report.Report_Id
                                select r).First();
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }// done
        public bool RemoveReport(int id)
        {
            try
            {
                using (var db = new Entitys())
                {
                    var reportToRemove = (from r in db.Reports where r.Report_Id == id select r).First();

                    db.Reports.Remove(reportToRemove);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//done
        public Report GetReport(int id)
        {
            using (var db = new Entitys())
            {
                var report = (from r in db.Reports
                          where r.Report_Id == id
                          select r).FirstOrDefault();
                return new Report
                {
                    Id = report.Id,
                    Report_Id = report.Report_Id,
                    Time = (DateTime)report.Time,
                    Name = report.Name,
                    Report_Adress = report.Report_Adress,
                    Boom_count = report.Boom_count,
                    ImagePath = report.ImagePath,
                    lat = report.lat,
                    log = report.log,
                };
            }
        }
        public List<Report> getReportList()
        {
            using (var db = new Entitys())
            {
                var drops = (from d in db.Reports select d);
                 return drops.ToList<Report>();
                //return null;
            }
        }//done
        #endregion
    }
}
