using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.BLL
{
    class BLL_Lichtap
    {
        private static BLL_Lichtap _Instance;
        public static BLL_Lichtap Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Lichtap();

                }
                return _Instance;
            }
            private set { }
        }
        public lichtap GetLichTapById(int id)
        {

            return DataSet.db.lichtaps.Where(p => p.id == id).Single();

        }
        public List<lichtap> GetlichTapByDay(string x)
        {
            List<lichtap> list = new List<lichtap>();

            var l1 = DataSet.db.lichtaps.Where(p => p.ThoiGian.Time == x).ToList();

            list = l1;
            return list;
        }
        public List<CBBItems> GetListDayOfWeed()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (Ngay i in DataSet.db.Ngays.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.Thu });
            }


            return list;
        }
        public List<CBBItems> GetTimeOfDay()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (ThoiGian i in DataSet.db.ThoiGians.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.Time });
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
        public DataGridView ListTT(string x)
        {
            DataGridView d = new DataGridView();
            var l1 = from p in BLL_Lichtap.Instance.GetlichTapByDay(x).ToList()
                     select new { ID = p.id, tenKH = p.Hopdong.TTKH.ten, tenNV = p.Hopdong.PT.TTNV.ten };
            d.DataSource = l1.ToList();
            return d;

        }
        public int GetIDHopDongByIDLichTap(int id)
        {
            return DataSet.db.lichtaps.Where(p => p.id == id).Single().Hopdong.id;
        }
        public Hopdong GetHopdongByID(int id)
        {
            return DataSet.db.Hopdongs.Where(p => p.id == id).Single();
        }
        public DataGridView GetLichtapByIdHD(int id)
        {
            var l=  DataSet.db.lichtaps.Where(p => p.idhopdong == id).Select(p => new { p.id, p.Ngay.Thu, p.ThoiGian.Time, p.idngay, p.idtime}).ToList();
            DataGridView d = new DataGridView();
            d.DataSource = l.ToList();
            return d;
        }
        public void DelLichTapById(int id)
        {
            DataSet.db.lichtaps.Remove(GetLichTapById(id));
            DataSet.db.SaveChanges();



        }



    }
}
