using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FL_Project.Model
{

#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class FallsLocationGroup : ObservableObject, IComparable
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    {
        private static int colorCounter = 0;
        private static string[] groupColorsArray = { "Gold", "Plum", "LawnGreen", "HotPink", "Peru", "OrangeRed", "ForestGreen" };

        private ObservableCollection<FallLocation> fallsLocationlist;
        private FallLocation estimateFallLocation;
        private AccurateFallLocation accurateFallLocation;
        public String groupColor;
        private String gruopId;
        public event Action<FallsLocationGroup,FallLocation> FallLocationChanged;
        public event Action<FallsLocationGroup> EsimateFallLocationChanged;
        public event Action<FallsLocationGroup> AccurateFallLocationChanged;

        public ObservableCollection<FallLocation> FallsLocationlist
        {
            get
            {
                return fallsLocationlist;
            }
            set {
                Set<ObservableCollection<FallLocation>>(() => this.FallsLocationlist, ref fallsLocationlist, value);
            }
        }

        public FallLocation EstimateFallLocation
        {
            get
            {
                return estimateFallLocation;
            }
            set
            {
                Set<FallLocation>(() => this.EstimateFallLocation, ref estimateFallLocation, value);
                if (EsimateFallLocationChanged != null)
                {
                    EsimateFallLocationChanged.Invoke(this);
                }
            }
        }

        public AccurateFallLocation AccurateFallLocation {
            get
            {
                return accurateFallLocation;
            }
            set
            {
                Set<AccurateFallLocation>(() => this.AccurateFallLocation, ref accurateFallLocation, value);
                if (AccurateFallLocationChanged != null)
                {
                    AccurateFallLocationChanged.Invoke(this);
                }

            }
        }

      

        public string GroupColor
        {
            get
            {
                return groupColor;
            }
            set
            {
               
                Set<string>(() => this.GroupColor, ref groupColor, value);
            }
        }


        public string GruopId
        {
            get
            {
                return gruopId;
            }
            set
            {
                Set<string>(() => this.GruopId, ref gruopId, value);
            }
        }

       /* public FallsLocationGroup(String _gruopId, Action<FallLocation> fallLocationChanged, Action<FallLocation> estimateFallLocationChanged, Action<AccurateFallLocation> accurateFallLocationChanged)
        {
            FallsLocationlist = new ObservableCollection<FallLocation>();
            FallsLocationlist.CollectionChanged += this.OnCollectionChanged;
            if (fallLocationChanged != null) { 
            FallLocationChanged = fallLocationChanged;
             }
            if (estimateFallLocationChanged != null)
            {
                EsimateFallLocationChanged = estimateFallLocationChanged;
            }
            if (accurateFallLocationChanged != null)
            {
                AccurateFallLocationChanged = accurateFallLocationChanged;
            }
            EstimateFallLocation = null;
            AccurateFallLocation = null;
            this.GruopId = _gruopId;
            GroupColor = (groupColorsArray[(colorCounter++) % 7]);
        }*/

        public FallsLocationGroup(String _gruopId)
        {
            FallsLocationlist = new ObservableCollection<FallLocation>();
            EstimateFallLocation = null;
            AccurateFallLocation = null;
            this.GruopId = _gruopId;
            GroupColor = (groupColorsArray[(colorCounter++) % 7]);
            FallsLocationlist.CollectionChanged += this.OnCollectionChanged;
        }


        // private NotifyCollectionChangedEventHandler OnCollectionChanged() => EvaluateEstimateFallLocation();
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var item = (FallLocation) e.NewItems[0];
            if (FallLocationChanged != null)
            {
                FallLocationChanged.Invoke(this,item);
            }
            EvaluateEstimateFallLocation();
        }


        public void EvaluateEstimateFallLocation() {

            if (FallsLocationlist.Count == 0)
            {
                EstimateFallLocation = null;
            }
            else
            {
                //TODO: add here the algorithem of get astimate location as TASK;
                EstimateFallLocation = FallLocationService.EstimateFallLocation(FallsLocationlist);
            };
            
        }

        public void AddFallLocation(FallLocation fl) {
            FallsLocationlist.Add(fl);
    //        FallLocationChanged.BeginInvoke(this, null, null);
        }

        public override bool Equals(object obj)
        {
            var group = obj as FallsLocationGroup;
            return group != null &&
                   GruopId == group.GruopId;
        }

        public bool Equals(string _gruopId)
        {
          
            return _gruopId != null &&
                   GruopId == _gruopId;
        }

        public int CompareTo(object obj)
        {
            DateTime st = DateTime.Parse(obj as string);
            return (DateTime.Parse(GruopId).CompareTo(st));
        }

        public static bool operator ==(FallsLocationGroup group1, FallsLocationGroup group2)
        {
            return EqualityComparer<FallsLocationGroup>.Default.Equals(group1, group2);
        }

        public static bool operator !=(FallsLocationGroup group1, FallsLocationGroup group2)
        {
            return !(group1 == group2);
        }
    }
 
}
