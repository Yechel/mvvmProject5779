using BE;
using BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FL_Project.Model
{
    static class FallLocationService
    {
        public static Action DataChanged;
    //public static Action NewFLAdded;
      //  public static Action EstimateFlChanged;
        //public static Action NewAFLAdded;

        private static ObservableCollection<FallsLocationGroup> FallLocationData;


        static readonly object _getDataLock = new object();

        public static BL_imp bl = new BL_imp();

        public static FallLocation FromReportToFallLocation(Report r)
        {

            FallLocation fl = new FallLocation();
            fl.Adress = r.Report_Adress;
            fl.Date = r.Time.ToString();
            fl.ID = r.Report_Id;                          //צריך לבדוק ID או REOPRT_ID
            fl.NumberOfFalls = r.Boom_count;
            return fl;
        }

        public static Report FlToReport(FallLocation fl)
        {
            Report r = new Report();
            r.Report_Adress = fl.Adress;
            //////////////TODO
            //r.Time = fl.Date;                              //המרה מSTRING  לDATETIME
            r.Report_Id = fl.ID;
            r.Boom_count = fl.NumberOfFalls;
            return r;

        }

        public static List<FallLocation> reportsTOListOfFallLocation(List<Report> reports)
        {
            List<FallLocation> fl = new List<FallLocation>();
            for (int i = 0; i < reports.Count; i++)
            {
                fl.Add(FromReportToFallLocation(reports[i]));
            }
            return fl;
        }

        public static ref ObservableCollection<FallsLocationGroup> GetData(Action<bool> callback)
        {
            lock (_getDataLock)
            {
                bool isSucceded = false;


                if (FallLocationData == null)
                {
                    Task task = new Task(() =>
                    {
                        try
                        {
                            FallLocationData = new ObservableCollection<FallsLocationGroup>();

                            //getting the falls from the Data Source and orderong it into list
                            Dictionary<string, ObservableCollection<FallLocation>> locationData = new Dictionary<string, ObservableCollection<FallLocation>>();
                            string key;
                            //TODO: get data of FallsAs a List as a List  from SQL
                            List<FallLocation> fallLocations = getDummyList();
                            //sort the FallLocations to the fit list
                            fallLocations.ForEach(x =>
                                {
                                    key = CreateKey(x.Date);
                                 
                                    if (!locationData.ContainsKey(key))
                                    {
                                        ObservableCollection<FallLocation> oc = new ObservableCollection<FallLocation>
                                        {
                                            x
                                        };
                                        locationData.Add(key, oc);
                                    }
                                    else
                                    {
                                        locationData[key].Add(x);
                                    }
                                });

                            //setting it into the map
                            foreach (var item in locationData)
                            {

                                FallsLocationGroup flg = new FallsLocationGroup(item.Key);
                                foreach (var item1 in item.Value)
                                {
                                    if (item1 is AccurateFallLocation)
                                    {
                                        flg.AccurateFallLocation =new AccurateFallLocation ((AccurateFallLocation)item1);
                                    }
                                    else { 
                                    flg.FallsLocationlist.Add(item1);
                                    }
                                    
                                }
                                FallLocationData.Add(flg);

                            }

                            //getting the accurate falls from the Data Source and orderong it into list
                            //TODO: get AccurateLocation From SQL;
                            //TODO: add accurateFalls

                       
                            isSucceded = true;
                        }
                        catch (Exception e)
                        {
                            isSucceded = false;

                        }


                    });
                   task.Start();
                   while (task.Status == TaskStatus.Running)
                    { Thread.Sleep(100); }
                    callback(isSucceded);
                    return ref FallLocationData;
                }
                else
                {
                    callback(isSucceded);
                    return ref FallLocationData;
                }
            }
        }

        //TODO: implement this with  ניתוח אשכולות
        internal static FallLocation EstimateFallLocation(ObservableCollection<FallLocation> fallsLocationlist)
        {
            return fallsLocationlist.First();
        }

       

        //TODO: realImplimentation
        private static double movement = 0;
        internal static double[] getLocation(string adress)
        {
            double d1 = 31.76367679378092 + movement;
            double d2 = 35.22701968033334 + movement;
            movement = movement + 0.01111;
            double[] d = { d1, d2 };
            return d;
        }

        internal static string GetAdressFromPath(string picPath)
        {
            //TODO: add this function
            return "חיים  הלוי, 6 שדרות";
        }

        private static string CreateKey(string date)
        {
            return date.Remove(date.Length - 1, 1) + "0";
        }

        private static List<FallLocation> getDummyList()
        {
            List<Report> Report_List = new List<Report>()
            {
          new Report{lat=32.184448,log= 34.870766,Report_Id = 222,Id=1, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          new Report{lat=31.705791,log= 35.200657,Report_Id = 222,Id=1, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=31.801447,log= 34.643497,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.699635,log= 35.303547,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.017136,log= 34.745441,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.109333,log= 34.855499,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.794044,log= 34.989571,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.919945,log= 35.290146,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=32.166313,log= 34.843311,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=31.894756,log= 34.809322,Report_Id = 222, Time = DateTime.Now,Name = "d",Report_Adress = "d",Boom_count = 3,ImagePath = ""},
          //new Report{lat=31.771959,log= 35.217018, Report_Id = 222, Time = DateTime.Now, Name = "d", Report_Adress = "d", Boom_count = 3, ImagePath = "" }
        };
            for (int i = 0; i < Report_List.Count; i++)
            {
                   bl.AddReport(Report_List[i]);
            }
           // var v = new Report { Report_Id = 123456, Time = DateTime.Now, Name = "d", Report_Adress = "d", Boom_count = 3, ImagePath = "" };
            //   bl.AddReport(v);
            // var lll = bl.getReportList();
            int dddi = 0;


            List<FallLocation> fallLocations = new List<FallLocation>();
            fallLocations = reportsTOListOfFallLocation(bl.getReportList());
            int k = 699;
            return fallLocations;
            //FallLocation fallLocation1 = new FallLocation("בר יוחאי 10, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //FallLocation fallLocation2 = new FallLocation("בר יוחאי 11, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 26, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //FallLocation fallLocation3 = new FallLocation("אבן צורי 9, שדרות, ישראל" , (new DateTime(2019, 1, 3, 7, 12, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //FallLocation fallLocation4 = new FallLocation("דוד המלך 5, שדרות, ישראל" , (new DateTime(2019, 1, 5, 15, 45, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //FallLocation fallLocation5 = new FallLocation("ירושלים 6, שדרות, ישראל"  , (new DateTime(2018, 12, 5, 14, 07, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //FallLocation fallLocation6 = new FallLocation("המאירי 8, שדרות, ישראל"   ,  (new DateTime(2018, 12, 6, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //FallLocation fallLocation7 = new FallLocation("גולני 2, שדרות, ישראל"    , (new DateTime(2018, 12, 7, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //FallLocation fallLocation8 =new FallLocation("חיל החימוש 5, שדרות, ישראל", (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //FallLocation fallLocation9 = new FallLocation("סירני 10, שדרות, ישראל",    (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //fallLocations.Add(fallLocation1);
            //fallLocations.Add(fallLocation2);
            //fallLocations.Add(fallLocation3);
            //fallLocations.Add(fallLocation4);
            //fallLocations.Add(fallLocation5);
            //fallLocations.Add(fallLocation6);
            //fallLocations.Add(fallLocation7);
            //fallLocations.Add(fallLocation8);
            //fallLocations.Add(fallLocation9);
            //return fallLocations;
            //       BL.BL_imp bl = new BL.BL_imp();
            //       var newReportToAdd = new BE.Report
            //       {




            //           Id = 1111,
            //           Report_Id =111111,
            //           Time = new DateTime(1,1,1),
            //           Name = "1",
            //           Report_Adress ="1",
            //           Boom_count =2,
            //           ImagePath ="2",
            //           lat = 1,
            //           log = 1
            //       };
            //   //    var i = bl.getDropsList();
            ////       var i =  bl.getReportList();
            //       bl.AddReport(newReportToAdd);


            //       List<FallLocation> fallLocations = new List<FallLocation>();

            //       AccurateFallLocation afallLocation1 = new AccurateFallLocation(12, "בר יוחאי 10, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"),1, @"C:\Users\יחיאל\Source\Repos\FL_Project\FL_Project\afl_pics\AFL_17.jpg");
            //       fallLocations.Add(afallLocation1);
            //       FallLocation fallLocation1 = new FallLocation("בר יוחאי 10, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //       FallLocation fallLocation2 = new FallLocation("בר יוחאי 11, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 26, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //       FallLocation fallLocation3 = new FallLocation("אבן צורי 9, שדרות, ישראל", (new DateTime(2019, 1, 3, 7, 12, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //       FallLocation fallLocation4 = new FallLocation("דוד המלך 5, שדרות, ישראל", (new DateTime(2019, 1, 5, 15, 45, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //       FallLocation fallLocation5 = new FallLocation("ירושלים 6, שדרות, ישראל", (new DateTime(2018, 12, 5, 14, 07, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //       FallLocation fallLocation6 = new FallLocation("המאירי 8, שדרות, ישראל", (new DateTime(2018, 12, 6, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //       FallLocation fallLocation7 = new FallLocation("גולני 2, שדרות, ישראל", (new DateTime(2018, 12, 7, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            //       FallLocation fallLocation8 = new FallLocation("חיל החימוש 5, שדרות, ישראל", (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //       FallLocation fallLocation9 = new FallLocation("סירני 10, שדרות, ישראל", (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            //       fallLocations.Add(fallLocation1);
            //       fallLocations.Add(fallLocation2);
            //       fallLocations.Add(fallLocation3);
            //       fallLocations.Add(fallLocation4);
            //       fallLocations.Add(fallLocation5);
            //       fallLocations.Add(fallLocation6);
            //       fallLocations.Add(fallLocation7);
            //       fallLocations.Add(fallLocation8);
            //       fallLocations.Add(fallLocation9);
            return fallLocations;

        }
        //TODO need to add to SQL
        internal static void AddAFL(AccurateFallLocation afl)
        {
            string key = CreateKey(afl.Date);
            bool isAdded = false;
            foreach (var item in FallLocationData)
            {
                if (item.GruopId.Equals(key))
                {
                    item.AccurateFallLocation = afl;
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded)
            {
                FallsLocationGroup flg = new FallsLocationGroup(key)
                {
                    AccurateFallLocation = afl
                };
                FallLocationData.Add(flg);
            }
            if (DataChanged != null)
            { DataChanged.Invoke(); };
        }

        public static void AddFL(FallLocation fl)
        {
            //add to DATALayer
            //add fitt gouplist
            string key = CreateKey(fl.Date);
            bool isAdded = false;
            foreach (var item in FallLocationData)
            {
                if (item.GruopId.Equals(key))
                {
                    item.FallsLocationlist.Add(fl);
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded)
            {
                FallsLocationGroup flg = new FallsLocationGroup(key);
                flg.FallsLocationlist.Add(fl);
                FallLocationData.Add(flg);
            }
            if (DataChanged != null)
            { DataChanged.Invoke(); };
        }

        public static List<int> getListOfDistance()
        {
            if (FallLocationData == null)
            { return null; }
            List<int> result = new List<int>();
            FallLocation efl;
            AccurateFallLocation afl;
            int dis = 0;
            foreach (var group in FallLocationData)
            {
                if (group.AccurateFallLocation != null && group.EstimateFallLocation != null)
                {
                    efl = group.EstimateFallLocation;
                    afl = group.AccurateFallLocation;
                    dis = GetDistance(efl, afl);
                    result.Add(dis);
                }
            }
            result.Sort((x, y) => x.CompareTo(y));
            return result;
        }

        static Random random = new Random();
        private static int GetDistance(FallLocation efl, FallLocation afl)
        {
            //TODO impliment real one, now it return a random number 
            return random.Next(0, 150);
        }


        static FallLocation EvaluateEstimateFallLocation(ObservableCollection<FallLocation> fallLocations)
        {
              //TODO Implement for real
             return fallLocations.First();
        }
    }
}
