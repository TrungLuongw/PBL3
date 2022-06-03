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
    class BLL_kh
    {
        private static BLL_kh _Instance;
        public static BLL_kh Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_kh();

                }
                return _Instance;
            }
            private set { }
        }
        public List<TTKH> listKH()
        {
            
            var l = Data.db.TTKHs.ToList();
            return l;
        }
        public List<TTKH> listKHold()
        {
            var l = Data.db.TTKHs.Where(p=>p.Hopdongs.Count>0).ToList();
            return l;
        }
        public List<TTKH> listKHnew()
        {
            var l = Data.db.TTKHs.Where(p => p.Hopdongs.Count==0).ToList();
            return l;
        }
        public List<TTKH> listKHHHHD()
        {
            List<TTKH> list = new List<TTKH>();
            foreach(Hopdong i in Data.db.Hopdongs.ToList())
            {
                if (BLL_HopDong.Instance.kiemtraThoiHan(i.id)==false)
                {   if(list.Contains(GetKhByID((int)i.idKH))==false)
                    list.Add(GetKhByID((int)i.idKH));
                }

            }

            return list;
        }
        public List<TTKH> searchTTKH(string x)
        {
            var l = Data.db.TTKHs.Where(p=>p.ten.Contains(x)||p.sdt.Contains(x)||p.cmnd.Contains(x)||p.id.ToString().Equals(x)||p.diachi.Contains(x)).ToList();
            return l;
        }
        public TTKH  GetKhByID(int id)
        {
            var l = Data.db.TTKHs.Single(p => p.id == id);
            return l;
        }
        public void delKh(int id)
        {
            
            foreach(Hopdong i in Data.db.Hopdongs.Where(p=>p.idKH==id).ToList())
            {
                BLL_HopDong.Instance.DelHD(i.id);
            }
            Data.db.TTKHs.Remove(GetKhByID(id));
            Data.db.SaveChanges();
        }
        public void saveHinh(byte[] b,int idkh)
        {
            if(b!=null)
            {
                var l = Data.db.TTKHs.Single(p => p.id == idkh);
                l.Hinh = (byte[])b;
                
            }
            Data.db.SaveChanges();
        }
        public List<CBBItems> GetListGoitap()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach (GoiTap i in Data.db.GoiTaps.ToList())
            {
                list.Add(new CBBItems { value = i.id, text = i.ten });
            }


            return list;
        }
        public bool kiemtraHDExit(int id)
        {
            if (Data.db.Hopdongs.Count(p => p.idKH == id) > 0)
                return false;
            else return true;

        }
        public void createHD(int idkh , int idpt , int idgoitap , int sothang)
        {
            Hopdong d = new Hopdong 
                {idpt = idpt , idKH = idkh, idgoitap = idgoitap, idstatus = 1,
                    ngayki = DateTime.Now , ngayhethan = DateTime.Now.AddMonths(sothang)};

            Data.db.Hopdongs.Add(d);
            var l = Data.db.PTs.Where(p => p.id == idpt).Single();
            int sum = (int)l.soluonghd + 1;
            l.soluonghd = sum;
            Data.db.SaveChanges();


        }
        public GoiTap GetGTByID(int id)
        {
            return Data.db.GoiTaps.Single(p => p.id == id);
        }
        public PT getpt(int id)
        {
            var l = Data.db.PTs.Single(p => p.id == id);
            return l;
        }
        public void updateTTKH(int id,string ten , string cmnd,string sdt,string diachi,DateTime ngaysinh)
        {
            var l = Data.db.TTKHs.Single(p => p.id == id);
            l.ten = ten;
            l.cmnd = cmnd;
            l.sdt = sdt;
            l.diachi = diachi;
            l.ngaysinh = ngaysinh;
            Data.db.SaveChanges();

        }
        public void addTTKH( string ten, string cmnd, string sdt, string diachi, DateTime ngaysinh)
        {
            TTKH t = new TTKH
            {
                ten = ten,
                cmnd = cmnd,
                sdt = sdt,
                diachi = diachi,
                ngaysinh = ngaysinh
            };
            Data.db.TTKHs.Add(t);
            Data.db.SaveChanges();
        }
        public bool KiemtraGOitap(int idgoitap)
        {
            var l = Data.db.GoiTaps.Single(p => p.id == idgoitap);
            if (l.sobuoi > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}
