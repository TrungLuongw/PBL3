using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_NhanVien
    {
        private static BLL_NhanVien _Instance;
        public static BLL_NhanVien Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NhanVien();

                }
                return _Instance;
            }
            private set { }
        }

        public List<TTNV> listNV()
        {

            var l = Data.db.TTNVs.Where(p=>p.PTs.Count<1&&(p.ngayra==null||p.ngayra>DateTime.Now)&& p.id != 0).ToList();
            return l;
        }
        public List<TTNV> listNVPT()
        {
            var l = Data.db.TTNVs.Where(p => p.PTs.Count > 0 && (p.ngayra == null || p.ngayra > DateTime.Now) && p.id != 0).ToList();
            return l;
        }
        public List<TTNV> listNVOld()
        {
            var l = Data.db.TTNVs.Where(p => p.ngayra<DateTime.Now && p.id != 0).ToList();
            return l;
        }
        public List<CBBItems> listCV()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach(Chucvu i in Data.db.Chucvus)
            {
                list.Add(new CBBItems { value = i.id, text = i.chucvu1 });
            }


            return list;
        }
        public TTNV GetTTNV(int id)
        {
            var l = Data.db.TTNVs.Where(p => p.id==id).Single();
            return l;
        }
        public void saveHinh(byte[] b, int idnv)
        {
            if (b != null)
            {
                var l = Data.db.TTNVs.Single(p => p.id == idnv);
                l.Hinh = (byte[])b;

            }
            Data.db.SaveChanges();
        }
        public void addPT(int idnV)
        {
            PT p = new PT { idnv = idnV, soluonghd = 0 };
            Data.db.PTs.Add(p);
            Data.db.SaveChanges();
        }
        public void delPT(int idnv)
        {
            var l = Data.db.Hopdongs.Where(p => p.PT.TTNV.id == idnv);
            foreach(Hopdong i in l)
            {
                i.idpt = null;
                
            }
            int x = Data.db.TTNVs.Where(p => p.id == idnv).Single().PTs.Count;
            if (x > 0)
            {
                var l1 = Data.db.PTs.Single(p => p.idnv == idnv);
                Data.db.PTs.Remove(l1);
                Data.db.SaveChanges();
            }
        }
        public void addTTNT(string ten, string cmnd, string sdt, string diachi, DateTime ngaysinh,DateTime ngayVao ,int idchucvu)
        {
            TTNV t = new TTNV
            {
                ten = ten,
                cmnd = cmnd,
                sdt = sdt,
                diachi = diachi,
                ngaysinh = ngaysinh,
                ngayvao = ngayVao,
                ngayra = null,
                idchucvu = idchucvu,
                Hinh =null
            };
            Data.db.TTNVs.Add(t);
            Data.db.SaveChanges();
        }
        public void DelTTNV(int idnv)
        {
            BLL_HoatDong.Instance.DelByIDNV(idnv);
            delPT(idnv);
            var l = Data.db.TTNVs.Single(p => p.id == idnv);
            Data.db.TTNVs.Remove(l);
            Data.db.SaveChanges();
        }
        public void updateTTNV(int id, string ten, string cmnd, string sdt, string diachi, DateTime ngaysinh,DateTime ngayvao,int idchucvu)
        {
            var l = Data.db.TTNVs.Single(p => p.id == id);
            l.ten = ten;
            l.cmnd = cmnd;
            l.sdt = sdt;
            l.diachi = diachi;
            l.ngaysinh = ngaysinh;
            l.ngayvao = ngayvao;
            l.ngayra = null;
            l.idchucvu = idchucvu;
            Data.db.SaveChanges();

        }
        public List<Hopdong> ListHDByIDPT(int idpt)
        {
            var l = Data.db.Hopdongs.Where(p=>p.idpt==idpt).ToList();
            return l;
        }
        public List<PT> ListAllPt()
        {
            var l = Data.db.PTs.Where(p=>p.id!=0).ToList();
            return l;
        }
        public List<PT> ListPtNotCount()
        {
            var l = Data.db.PTs.Where(p=>p.Hopdongs.Count <1&&p.id!=0).ToList();
            return l;
        }
        public List<lichtap> GetLichtapByidHD(int idhd)
        {
            var l = Data.db.lichtaps.Where(p => p.idhopdong == idhd).ToList();
           l = l.OrderBy(p => p.Ngay.id).ToList();
            return l;
        }
        public List<lichtap> GetalllichtapCuaPT(int idpt)
        {
            var l = Data.db.lichtaps.Where(p => p.Hopdong.idpt == idpt).ToList();
            l = l.OrderBy(p => p.Ngay.id).ToList();
            return l;
        }
    }
}
