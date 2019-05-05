using BE;
using BL.functions;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using static BL.functions.ApiAdressToCoordinate;
using System.Device.Location;


using System.Threading.Tasks;
using System.Threading;

namespace BL
{
    public class BL_imp : Ibl //check working
    {
        

        DAL_imp dal = new DAL_imp();

        #region Report
        public bool AddReport(Report report)
        {
            return dal.AddReport(report);
        }
        
        public bool RemoveReport(int id)
        {
            return dal.RemoveReport(id);
        }
        public bool UpdateReport(BE.Report report)
        {
            throw new NotImplementedException();
        }
        public Report GetReportById(int id)
        {
            return dal.GetReport(id);
        }
        public List<Report> getReportList()
        {
         
            return dal.getReportList();
        }
        #endregion

        #region Drop
        public bool AddDrop(Drop drop)
        {
            return dal.AddDrop(drop);
        } //done
        public bool RemoveDrop(int id)
        {
            return dal.RemoveDrop(id);
        } //done
        public bool UpdateDrop(BE.Drop Drop)
        {
            throw new NotImplementedException();
        }
        public Drop GetDropById(int id)
        {
            return dal.GetDrop(id);
        }
        #endregion



        #region Functions
        public double[]  GetCoordinate(string address)
        {
            double let=0;
            double log=0;
         
            Task task = new Task(async () =>
               {
                   var Coordinate =await ApiAdressToCoordinate.GetGeoCoordinateAsync(address);
                    let = Coordinate.results[0].geometry.location.lat;
                    log = Coordinate.results[0].geometry.location.lng;
                                                                          

                });
                    task.Start();
                    while (task.Status == TaskStatus.Running)
                         { Thread.Sleep(1400);
                         }
    double[] latlLog = { let, log };
            //var r = new List<Report>();
            //Drop d1 = new Drop
            //{
            //    Id = 26544131,
            //    Drop_Id = 207544131,
            //    Drop_Adress = "ישראל יבנה הזמיר 4",
            //    Drop_time = new DateTime(2010, 10, 10),

            //    Reports_list = r,
            //    Real_lat = 0,
            //    Real_log = 0,
            //    Estimeated_lat = 99,
            //    Estimeated_log = 98,
            //};
            //d1.Real_lat = let;
            //d1.Real_log = log;
            //AddDrop(d1);

          
            return latlLog;
}


        public string FromLatLogToAdress(double lat, double log)
        {           
            //FOR TESTING FRO M LAT LOG TO ADRESS
            string ADR = "NOTHING";
            Task task = new Task(async () =>
            {
                //fore a function testinf from lat log to adress
                ADR = await ApiAdressToCoordinate.RetrieveFormatedAddress(lat.ToString(), log.ToString());            
            });
            task.Start();
            while (task.Status == TaskStatus.Running)
            {
                Thread.Sleep(1400);
            }
          
            return ADR;
        }
    //get address and return location (lat and long) return null if not success
    public double[] getLatAndLong(string adress)
        {
            double[] latLong = new double[2];
            return latLong;
        }

        public void GetCoordinateFromExif(string imagePath)
        {
            var img = ExifImageTo_Lat_Log.GetLatLongFromImage(imagePath);
        }
        //#region 
        //public double[][] UseKmeans(List<Report> reports)
        //{
        //    //gets list of reports and create drops and returns double[][] means 
        //    //which is lat log of the means

        //    //
        //    // maybe we wiil add a function that set a list of report for each drop 
        //    // according to the output of kmeans then we will be able to show it
        //    //

        //    //////////NOT DONE!!!!!!///////////////////////////////////////////////
        //    //getting number of cluster according to number of 10 minutes
        //    //and list of first times for each group 
        //    int numClusters = 3;//3 for testing 
        //    List<DateTime> timeOfDrops = new List<DateTime>();
        //    int hour = 0;
            
        //    for (int i = 0; i < reports.Count; i++)
        //    {
        //        if (reports[i].Time.Minute == 10)
        //        {

        //        }
        //    }        
        //    //////////////////////////////////////////////////////////////////
            
        //    //takeing lat log out of the reports which will be our raw_data.
        //    double[][] rawData = new double[reports.Count][];
        //    for (int i = 0; i < reports.Count; i++)
        //    {
        //        rawData[i] = new double[] { reports[i].lat, reports[i].log };
        //    }
        //    //then sending them to k_means function and get double[][] of means.
        //    double[][] clustering = Cluster(rawData, numClusters); // this is it

        //    /////////////////NOT DONE!!!!!!!!////////////////////////////
        //    //get address from lat log
        //    List<string> adresses = new List<string>();

        //    ////////////////////////////////////////////////////////
        //    //create drop objects and update them with estimated lat log and adress.
        //    for (int i = 0; i < numClusters; i++)
        //    {
        //        Drop d=new Drop 
        //        {
        //            Id = i,
        //            Drop_Id =i ,
        //            Drop_Adress = adresses[i],
        //            Drop_time = timeOfDrops[i],
        //            Reports_list = null,
        //            Real_lat = 0,
        //            Real_log = 0,
        //            Estimeated_lat = clustering[i][0],
        //            Estimeated_log = clustering[i][1],

        //        };
        //        dal.AddDrop(d);
        //    }
        //    return clustering;


        //}
        //#endregion


        #region k_means
        //Meybe we should make it not static...not sure yet//
        // K-means clustering demo. ('Lloyd's algorithm')
        // Coded using static methods. Normal error-checking removed for clarity.
        // This code can be used in at least two ways. You can do a copy-paste and then insert the code into some system that uses clustering.
        // Or you can wrap the code up in a Class Library. The single public method is Cluster().

        // ============================================================================
        //gets a double[][] and  return [][]means
        public  double[][] Cluster(double[][] rawData, int numClusters)
        {
            // k-means clustering
            // index of return is tuple ID, cell is cluster ID
            // ex: [2 1 0 0 2 2] means tuple 0 is cluster 2, tuple 1 is cluster 1, tuple 2 is cluster 0, tuple 3 is cluster 0, etc.
            // an alternative clustering DS to save space is to use the .NET BitArray class


            // double[][] data = Normalized(rawData); // so large values don't dominate
            double[][] data = rawData; // so large values don't dominate

            bool changed = true; // was there a change in at least one cluster assignment?
            bool success = true; // were all means able to be computed? (no zero-count clusters)

            // init clustering[] to get things started
            // an alternative is to initialize means to randomly selected tuples
            // then the processing loop is
            // loop
            //    update clustering
            //    update means
            // end loop
            int[] clustering = InitClustering(data.Length, numClusters, 0); // semi-random initialization
            double[][] means = Allocate(numClusters, data[0].Length); // small convenience

            int maxCount = data.Length * 10; // sanity check
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct; // k-means typically converges very quickly
                success = UpdateMeans(data, clustering, means); // compute new cluster means if possible. no effect if fail
                changed = UpdateClustering(data, clustering, means); // (re)assign tuples to clusters. no effect if fail
            }
            // consider adding means[][] as an out parameter - the final means could be computed
            // the final means are useful in some scenarios (e.g., discretization and RBF centroids)
            // and even though you can compute final means from final clustering, in some cases it
            // makes sense to return the means (at the expense of some method signature uglinesss)
            //
            // another alternative is to return, as an out parameter, some measure of cluster goodness
            // such as the average distance between cluster means, or the average distance between tuples in 
            // a cluster, or a weighted combination of both
            return means;
        }

        private  double[][] Normalized(double[][] rawData)
        {
            // normalize raw data by computing (x - mean) / stddev
            // primary alternative is min-max:
            // v' = (v - min) / (max - min)

            // make a copy of input data
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        private  int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            // init clustering semi-randomly (at least one tuple in each cluster)
            // consider alternatives, especially k-means++ initialization,
            // or instead of randomly assigning each tuple to a cluster, pick
            // numClusters of the tuples as initial centroids/means then use
            // those means to assign each tuple to an initial cluster.
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i) // make sure each cluster has at least one tuple
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters); // other assignments random
            return clustering;
        }

        private  double[][] Allocate(int numClusters, int numColumns)
        {
            // convenience matrix allocator for Cluster()
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numColumns];
            return result;
        }

        private  bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            // returns false if there is a cluster that has no tuples assigned to it
            // parameter means[][] is really a ref parameter

            // check existing cluster counts
            // can omit this check if InitClustering and UpdateClustering
            // both guarantee at least one tuple in each cluster (usually true)
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to means[][]

            // update, zero-out means so it can be used as scratch matrix 
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                // j < data[i].Length :didnt wrok for some reason  so i wrote 2
                for (int j = 0; j < 2; ++j)
                    means[cluster][j] += data[i][j]; // accumulate sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
            return true;
        }

        private  bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            // (re)assign each tuple to a cluster (closest mean)
            // returns false if no tuple assignments change OR
            // if the reassignment would result in a clustering where
            // one or more clusters have no tuples.

            int numClusters = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length]; // proposed result
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters]; // distances from curr tuple to each mean

            for (int i = 0; i < data.Length; ++i) // walk thru each tuple
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]); // compute distances from curr tuple to all k means

                int newClusterID = MinIndex(distances); // find closest mean ID
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID; // update
                }
            }

            if (changed == false)
                return false; // no change so bail and don't update clustering[][]

            // check proposed clustering[] cluster counts
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to clustering[][]

            Array.Copy(newClustering, clustering, newClustering.Length); // update
            return true; // good clustering and at least one change
        }

        private  double Distance(double[] tuple, double[] mean)
        {
            // Euclidean distance between two vectors for UpdateClustering()
            // consider alternatives such as Manhattan distance
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private  int MinIndex(double[] distances)
        {
            // index of smallest value in array
            // helper for UpdateClustering()
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        // ============================================================================

        // misc display helpers for demo

        static void ShowData(double[][] data, int decimals, bool indices, bool newLine)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (data[i][j] >= 0.0) Console.Write(" ");
                    Console.Write(data[i][j].ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine) Console.WriteLine("");
        } // ShowData

        static void ShowVector(int[] vector, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            if (newLine) Console.WriteLine("\n");
        }

        static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals)
        {
            for (int k = 0; k < numClusters; ++k)
            {
                Console.WriteLine("===================");
                for (int i = 0; i < data.Length; ++i)
                {
                    int clusterID = clustering[i];
                    if (clusterID != k) continue;
                    Console.Write(i.ToString().PadLeft(3) + " ");
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        if (data[i][j] >= 0.0) Console.Write(" ");
                        Console.Write(data[i][j].ToString("F" + decimals) + " ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("===================");
            } // k
        }
        // Program
        #endregion


        #endregion
        #region helpFunctions
        //public bool RemoveAllDrops()
        //{
        //    return dal.RemoveAllDrops();
        //}

        //return list of all the drops from DataSource  vvdonevv
       public List<Drop> getDropsList()
        {
            return dal.getDropList();
        }

        //return list of all the AccurateDrops from DataSource
        public List<Drop> getAccurateDropList()
        {
            ////////////////////////////
            ////////////////////////////code for altering the drop table.i can run it once and it should be enough.
            ////////////////////////////
            //using (DbConnection connection = new SqlConnection("Your connection string"))
            //{
            //    connection.Open();
            //    using (DbCommand command = new SqlCommand("alter table [Product] add [ProductId] int default 0 NOT NULL"))
            //    {
            //        command.Connection = connection;
            //        command.ExecuteNonQuery();
            //    }
            //}
            ///////////////
            ////
            /////
            /////
            List<Drop> A = new List<Drop>();
            return  A;
        }

        //taking list of drops and return a drop with estimate drop
       public List<Drop> CalculateEstimateDrop(List<Report> report_list)
        {

            int numOfClusters = 3;
            double[][] rawData = new double[report_list.Count()][];
            //inistiate the array
            for (int i = 0; i < report_list.Count(); i++)
            {
                rawData[i] = new double[2];
            }
            

            for (int i = 0; i < report_list.Count(); i++)
            {       
                    rawData[i][0] = report_list[i].lat;
                    rawData[i][1] = report_list[i].log;
            }
            //inistiate the array
            double[][] resultedLatLog = new double[numOfClusters][];
            for (int i = 0; i < numOfClusters; i++)
            {
                resultedLatLog[i] = new double[2];
            }
            resultedLatLog = Cluster(rawData, numOfClusters);
           

            List<Drop> result = new List<Drop>(numOfClusters);
            List<Report> r = new List<Report>();

            for (int i = 0; i < numOfClusters; i++)
            {
                result.Add(new Drop
                {
                    Id = i,
                    Drop_Id = 207544131,
                    Drop_Adress = "ישראל יבנה הזמיר 4",
                    Drop_time = new DateTime(2010, 10, 10),

                    Reports_list = r,
                    Real_lat = 0,
                    Real_log = 0,
                    Estimeated_lat = resultedLatLog[i][0],
                    Estimeated_log = resultedLatLog[i][1],
                });
              //  result[i].Estimeated_lat = resultedLatLog[i][0];
               // result[i].Estimeated_log = resultedLatLog[i][1];
            }
            return result;
        }

        //return the distance from the acuurateDrop to the estimate drop 
       public float EvaluateDistance(Drop d1, Drop d2)
        {
            var sCoord = new System.Device.Location.GeoCoordinate(d1.Estimeated_lat, d1.Estimeated_log);
            var eCoord = new System.Device.Location.GeoCoordinate(d2.Estimeated_lat, d2.Estimeated_log);
            
            return (float)sCoord.GetDistanceTo(eCoord);
        }

       

        //return the picture of the given drop
       public string getPath(Drop id)
        {
            Drop drop = GetDropById(id.Drop_Id);
            return drop.ImagePath;
        }

        #endregion
    }
}
