using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DAL
{
    class DAL_LichTap
    {

        private static DAL_LichTap _Instance;
        public static DAL_LichTap Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_LichTap();

                }
                return _Instance;
            }
            private set { }
        }
        public DAL_LichTap() { }
        public List<CBBItems> GetListDayOfWeed()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach(Ngay i in DataSet.db.Ngays.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.Thu });
            }


            return list;
        }
        
        public List<lichtap> GetTimeByDay(string x)
        {
            List<lichtap> list = new List<lichtap>();
            var l1 = DataSet.db.lichtaps.Where(p => p.Ngay.Thu == x).ToList();

            list = l1;


            return list;

        }
        public List<lichtap> GetlichTapByDay(string x)
        {
            List<lichtap> list = new List<lichtap>();

            var l1 = DataSet.db.lichtaps.Where(p => p.ThoiGian.Time == x).ToList();

            list = l1;
            return list;
        }
        public lichtap GetLichTapById(int id)
        {

            return  DataSet.db.lichtaps.Where(p=>p.id==id).Single();
            
        }







    }
}
