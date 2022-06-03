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

            return Data.db.lichtaps.Where(p => p.id == id).Single();

        }
        public DataGridView Listlichtap(string x)
        {
            DataGridView d = new DataGridView();
            //var l1 = from p in DAL_LichTap.Instance.GetTimeByDay(x).ToList()
            //         group p by p.ThoiGian.Time into g
            //         select new
            //         {

            //             Time = g.Key,
            //             count = g.Count()

            //         };
            var l2 = Data.db.lichtaps.Where(p => p.Ngay.Thu.Equals(x) && p.Hopdong.ngayhethan > DateTime.Now)
                .GroupBy(p => p.ThoiGian.Time).Select(p => new { Time = p.Key, count = p.Count() });
            l2 = l2.OrderBy(p => p.Time.Length);
            l2 = l2.OrderBy(p => p.Time.Length == 4);
            l2 = l2.OrderBy(p => p.Time.Length == 5);
            d.DataSource = l2.ToList();
            return d;

        }
        public List<lichtap> GetlichTapByDay(string x)
        {


            var l1 = Data.db.lichtaps.Where(p => p.ThoiGian.Time == x).ToList();


            return l1;
        }
        public List<CBBItems> GetListDayOfWeed()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (Ngay i in Data.db.Ngays.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.Thu });
            }


            return list;
        }
        public List<CBBItems> GetTimeOfDay()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (ThoiGian i in Data.db.ThoiGians.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.Time });
            }
            return list;
        }
        public List<lichtap> GetTimeByDay(string x)
        {
            List<lichtap> list = new List<lichtap>();
            var l1 = Data.db.lichtaps.Where(p => p.Ngay.Thu == x).ToList();

            list = l1;


            return list;

        }
        public DataGridView ListTT(string time , string day)
        {
            DataGridView d = new DataGridView();
            var l1 = Data.db.lichtaps.Where(p => p.ThoiGian.Time == time && p.Ngay.Thu == day&&p.Hopdong.ngayhethan>DateTime.Now)
                .Select(p => new { ID = p.id, tenKH = p.Hopdong.TTKH.ten, tenNV = p.Hopdong.PT.TTNV.ten });
                     
            d.DataSource = l1.ToList();
            return d;

        }
        public int GetIDHopDongByIDLichTap(int id)
        {
            return Data.db.lichtaps.Where(p => p.id == id).Single().Hopdong.id;
        }
        public Hopdong GetHopdongByID(int id)
        {
            return Data.db.Hopdongs.Where(p => p.id == id).Single();
        }
        public DataGridView GetLichtapByIdHD(int id)
        {
            var l = Data.db.lichtaps.Where(p => p.idhopdong == id).Select(p => new { p.id, p.Ngay.Thu, p.ThoiGian.Time, p.idngay, p.idtime }).ToList();
            DataGridView d = new DataGridView();
            l.OrderBy(p => p.idngay);
            d.DataSource = l.OrderBy(p => p.idngay).ToList();
            return d;
        }
        public void DelLichTapById(int id)
        {
            Data.db.lichtaps.Remove(GetLichTapById(id));
            Data.db.SaveChanges();



        }
        public List<Hopdong> GetIDHopDongByIDKKandPT(int idKH, int idPT)
        {

            if (idPT != 0 && idKH != 0)
            {

                var s = Data.db.Hopdongs.Where(p => p.idKH == idKH && p.idpt == idPT).ToList();
                return s;
            }
            else
            {
                if (idPT == 0)
                {
                    var s = Data.db.Hopdongs.Where(p => p.idKH == idKH).ToList();
                    return s;
                }
                else
                {
                    var s = Data.db.Hopdongs.Where(p => p.idpt == idPT).ToList();
                    return s;
                }
            }
        }
        public void UpdateLichTapById(int id, int idngay, int idTime)
        {

            var s = Data.db.lichtaps.Where(p => p.id == id).Single();
            s.idtime = idTime;
            s.idngay = idngay;
            Data.db.SaveChanges();
            

        }
        public void AddLichTap(int idhd, int idtime, int idNgay)
        {
            lichtap l = new lichtap { idhopdong = idhd, idngay = idNgay, idtime = idtime };
            Data.db.lichtaps.Add(l);
            Data.db.SaveChanges();
        }

        public bool kiemtraLichTap(int idPT, int idtime, int idNgay)
        {
            if (Data.db.lichtaps.Count(p => p.idngay == idNgay && p.idtime == idtime && p.Hopdong.idpt == idPT ) > 0 )
                return false;
            if (Data.db.lichtaps.Count(p => p.idngay == idNgay && p.idtime > idtime - 3 && p.idtime < idtime + 3 && p.Hopdong.idpt == idPT ) > 0)
                return false;
            return true;
        }
        public List<LichTapCuaPT> MesKiemTraLichtap(int idpt, int idtime, int idNgay)
        {

            List<LichTapCuaPT> list = new List<LichTapCuaPT>();
            foreach (lichtap i in Data.db.lichtaps.Where(p => p.Hopdong.idpt == idpt && p.idngay == idNgay && p.idtime > idtime - 3 && p.idtime < idtime + 3 && p.Hopdong.idpt != 0).ToList())
            {
                list.Add(new LichTapCuaPT { Time = i.ThoiGian.Time, Day = i.Ngay.Thu });
            }


            return list;
        }
        public DataGridView SearchTT(string x)
        {
            var l = Data.db.TTKHs.Where(p => p.id.ToString().Contains(x) || p.sdt.Contains(x) || p.ten.Contains(x) || p.cmnd.Contains(x)).Select(p => new { ID = p.id, TenKH = p.ten });
            DataGridView d = new DataGridView();
            d.DataSource = l.ToList();
            return d;



        }
        public List<CBBItems> ListCBBPTByIdKH(int idKH)
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (Hopdong i in Data.db.Hopdongs.Where(p => p.idKH == idKH).ToList())
            {
                list.Add(new CBBItems { value = Convert.ToInt32(i.idpt), text = i.PT.TTNV.ten });
            }
            return list;
        }
        ///BLl khach hang
        public TTKH GetTTKHById(int idkh)
        {
            return Data.db.TTKHs.Where(p => p.id == idkh).Single();
        }
        public bool KiemTraCoHOpDong(int idkh)
        {
            if (Data.db.Hopdongs.Where(p => p.idKH == idkh && p.GoiTap.sobuoi == 0).Count() > 0)
                return false;     
            if (Data.db.Hopdongs.Count(p => p.idKH == idkh) > 0)
                return true;
            else return false;
        }
        


    }
}
