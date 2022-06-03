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
    class BLL_HopDong
    {
        private static BLL_HopDong _Instance;
        public static BLL_HopDong Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_HopDong();

                }
                return _Instance;
            }
            private set { }
        }
        public DataGridView DanhsachHD()
        {
            DataGridView d = new DataGridView();
            var l = Data.db.Hopdongs.Select(p => new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public List<CBBItems> GetStatus()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (Status i in Data.db.Status.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.ten });
            }
            return list;
        }
        public DataGridView DanhsachHDHH()
        {
            DataGridView d = new DataGridView();
            DateTime t = DateTime.Now;
            var l = Data.db.Hopdongs.Where(p => p.ngayhethan < t).Select(p => new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public DataGridView DanhsachHDTPT()
        {
            DataGridView d = new DataGridView();
            DateTime t = DateTime.Now;
            var l = Data.db.Hopdongs.Where(p =>p.idpt==null).Select(p => new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public DataGridView DanhsachHDTT()
        {
            DataGridView d = new DataGridView();
            DateTime t = DateTime.Now;
            var l = Data.db.Hopdongs.Where(p => (int)p.GoiTap.sobuoi==0).Select(p => new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public void HetHan()
        {
            DateTime t = DateTime.Now;
            var l = Data.db.Hopdongs.Where(p => p.ngayhethan < t).ToList();
            foreach (Hopdong i in l.ToList())
            {
                i.idstatus = 2;
                Data.db.SaveChanges();
            }
            var l2 = Data.db.Hopdongs.Where(p => p.ngayhethan > t).ToList();
            foreach (Hopdong i in l2.ToList())
            {
                i.idstatus = 1;
                Data.db.SaveChanges();
            }
        }
        public DataGridView DanhsachHDTL()
        {
            DataGridView d = new DataGridView();

            var l = Data.db.Hopdongs.Where(p => p.lichtaps.Count < p.GoiTap.sobuoi).Select(p => new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public void GiahanHD(int idhd, DateTime d)
        {
            var l = Data.db.Hopdongs.Where(p => p.id == idhd).Single();
            l.ngayhethan = d;
            Data.db.SaveChanges();
        }
        public List<lichtap> GetListLichTapByIdHD(int idhd)
        {
            var l = Data.db.lichtaps.Where(p => p.idhopdong == idhd).ToList();
            return l;
        }
        public void DelHD(int idhd)
        {
            if (BLL_Lichtap.Instance.GetHopdongByID(idhd).idpt != null)
            {
                int z = (int)BLL_Lichtap.Instance.GetHopdongByID(idhd).idpt;
                var l = Data.db.PTs.Where(p => p.id == z).Single();
                int sum = (int)l.soluonghd - 1;
                l.soluonghd = sum;
                Data.db.SaveChanges();
            }
            foreach (lichtap i in GetListLichTapByIdHD(idhd))
                {
                    Data.db.lichtaps.Remove(i);

                }
                Data.db.SaveChanges();
                Data.db.Hopdongs.Remove(BLL_Lichtap.Instance.GetHopdongByID(idhd));
                Data.db.SaveChanges();

            
            return ;
            
        }
        public List<IDNgayIDGio> ListCheckDateTime(int idhd)
        {
            List<IDNgayIDGio> list = new List<IDNgayIDGio>();
            foreach (lichtap i in Data.db.lichtaps.Where(p => p.idhopdong == idhd).ToList())
            {
                list.Add(new IDNgayIDGio { ngay = i.Ngay.Thu, gio = i.ThoiGian.Time, idngay = (int)i.idngay, idgio = (int)i.idtime });
            }
            return list;
        }
        public DataGridView SearchPT(List<IDNgayIDGio> list , string x)
        {
            DataGridView d = new DataGridView();
            if (x == ""&& list!= null)
            {
                bool check;
                List<PT> p = new List<PT>();
                foreach (PT i in Data.db.PTs.Where(f=>f.id!=0).ToList())
                {
                    check = true;
                    foreach (IDNgayIDGio j in list)
                    {
                        if (BLL_Lichtap.Instance.kiemtraLichTap(i.id, j.idgio, j.idngay) == false)
                            check = false;
                    }
                    if (check) p.Add(i);
                }
                d.DataSource = p.Select(f => new { IDPT = f.id, Ten = f.TTNV.ten, HopDong = f.soluonghd }).ToList();
            }
            else
            {
                var l = Data.db.PTs.Where(p => p.TTNV.ten.Contains(x) || p.TTNV.cmnd.Contains(x) || p.id.ToString().Contains(x) ||
                p.TTNV.sdt.Contains(x)).Select(f => new { IDPT = f.id, Ten = f.TTNV.ten, HopDong = f.soluonghd });
                d.DataSource = l.ToList();
            }    
            return d;
        }
        public DataGridView GetListLichTapByIDPT(int idpt)
        {
            DataGridView d = new DataGridView();
            var l = Data.db.lichtaps.Where(p => p.Hopdong.PT.id == idpt).Select(p => new { IDLichTap = p.id, tenKH = p.Hopdong.TTKH.ten, p.Ngay.Thu, p.ThoiGian.Time });
            l = l.OrderBy(p => p.Thu);
            d.DataSource = l.ToList();
            return d;

        }
        public bool kiemtraThoiHan(int idhd)
        {
            if (Data.db.Hopdongs.Where(p => p.id == idhd).Single().ngayhethan > DateTime.Now)
                return true;
            else
                return false;
           
        }
        public void swapPT(int idpt , int idhd)
        {
            
            var l = Data.db.Hopdongs.Single(p => p.id == idhd);
            if(l.idpt!=null)
            l.PT.soluonghd = l.PT.soluonghd - 1;
            l.idpt = idpt;

            Data.db.SaveChanges();
            var l1 = Data.db.PTs.Single(p => p.id == idpt);
            l1.soluonghd = l1.soluonghd + 1;
            Data.db.SaveChanges();
        }
        public DataGridView SearchHD(string x)
        {
            DataGridView d = new DataGridView();
            var l = Data.db.Hopdongs.Where(p => p.TTKH.ten.Contains(x) 
            || p.PT.TTNV.ten.Contains(x) || 
            p.idKH.ToString().Contains(x)||p.idpt.ToString().Contains(x)||p.id.ToString().Contains(x)).Select(p=>new { ID = p.id, TenKH = p.TTKH.ten, TenPT = p.PT.TTNV.ten });
            d.DataSource = l.ToList();
            return d;
        }
        public void XoaPTofHD(int idhd)
        {
            var l = Data.db.Hopdongs.Single(p => p.id == idhd);
            var l2 = Data.db.PTs.Single(p => p.id == l.idpt);
            l2.soluonghd = l2.soluonghd - 1;
            l.idpt = null;
            Data.db.SaveChanges();
        }
        public bool CheckIDPt(int idhd)
        {
            var l = Data.db.Hopdongs.Single(p => p.id == idhd);
            if (l.idpt == 0) return true;
            else
                return false;
        }
    }
}
