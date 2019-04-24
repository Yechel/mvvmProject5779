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


        private static ObservableCollection<FallsLocationGroup> FallLocationData;


        static readonly object _getDataLock = new object();



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
                                key = createKey(x.Date);



                                if (!locationData.ContainsKey(key))
                                {
                                    ObservableCollection<FallLocation> oc = new ObservableCollection<FallLocation>();
                                    oc.Add(x);
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
                                    flg.FallsLocationlist.Add(item1);
                                }
                                FallLocationData.Add(flg);
                            /*foreach (FallsLocationGroup group in FallLocationData)
                            {
                                if (group.Equals(item.Key)
                                    {
                                    group.FallsLocationlist.Add(item.Value as FallLocation);
                                }
                            } (FallLocationData.Contains(item.Key))
                            {

                                FallsLocationGroup fg = new FallsLocationGroup(item.Key);
                                fg.FallsLocationlist = item.Value;
                                fg.EvaluateEstimateFallLocation();
                                retrunData.Add(item.Key, fg);
                                addFallLocationToTheGroup(item);
                            }
                            else
                            {
                                retrunData[item.Key].FallsLocationlist = item.Value;
                            }*/
                            }

                        //getting the accurate falls from the Data Source and orderong it into list
                        //TODO: get AccurateLocation From SQL;
                        //TODO: add accurateFalls


                        isSucceded = true;
                        }
                        catch
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

        internal static string getAdressFromPath(string picPath)
        {
            //TODO: add this function
            return "חיים  הלוי, 6 שדרות";
        }

        private static string createKey(string date)
        {
            return date.Remove(date.Length - 1, 1) + "0";
        }

        private static List<FallLocation> getDummyList()
        {
            List<FallLocation> fallLocations = new List<FallLocation>();

            FallLocation fallLocation1 = new FallLocation("בר יוחאי 10, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            FallLocation fallLocation2 = new FallLocation("בר יוחאי 11, שדרות, ישראל", (new DateTime(2018, 12, 1, 19, 26, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            FallLocation fallLocation3 = new FallLocation("אבן צורי 9, שדרות, ישראל", (new DateTime(2019, 1, 3, 7, 12, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            FallLocation fallLocation4 = new FallLocation("דוד המלך 5, שדרות, ישראל", (new DateTime(2019, 1, 5, 15, 45, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            FallLocation fallLocation5 = new FallLocation("ירושלים 6, שדרות, ישראל", (new DateTime(2018, 12, 5, 14, 07, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            FallLocation fallLocation6 = new FallLocation("המאירי 8, שדרות, ישראל", (new DateTime(2018, 12, 6, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            FallLocation fallLocation7 = new FallLocation("גולני 2, שדרות, ישראל", (new DateTime(2018, 12, 7, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 1);
            FallLocation fallLocation8 = new FallLocation("חיל החימוש 5, שדרות, ישראל", (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            FallLocation fallLocation9 = new FallLocation("סירני 10, שדרות, ישראל", (new DateTime(2018, 12, 8, 19, 25, 00)).ToString("MM/dd/yyyy hh:mm"), 2);
            fallLocations.Add(fallLocation1);
            fallLocations.Add(fallLocation2);
            fallLocations.Add(fallLocation3);
            fallLocations.Add(fallLocation4);
            fallLocations.Add(fallLocation5);
            fallLocations.Add(fallLocation6);
            fallLocations.Add(fallLocation7);
            fallLocations.Add(fallLocation8);
            fallLocations.Add(fallLocation9);
            return fallLocations;

        }

        internal static void addAFL(AccurateFallLocation afl)
        {
            string key = createKey(afl.Date);
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
                FallsLocationGroup flg = new FallsLocationGroup(key);
                flg.AccurateFallLocation = afl;
                FallLocationData.Add(flg);
            }
            if (DataChanged != null)
            { DataChanged.Invoke(); };
        }

        public static void addFL(FallLocation fl)
        {
            //add to DATALayer
            //add fitt gouplist
            string key = createKey(fl.Date);
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
                    dis = getDistance(efl, afl);
                    result.Add(dis);
                }
            }
            result.Sort((x, y) => x.CompareTo(y));
            return result;
        }

        static Random random = new Random();
        private static int getDistance(FallLocation efl, FallLocation afl)
        {
            //TODO impliment real one, now it return a random number 
            return random.Next(0, 150);
        }
    }
}
