using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_HoatDong
    {
        private static BLL_HoatDong _Instance;
        public static BLL_HoatDong Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_HoatDong();

                }
                return _Instance;
            }
            private set { }
        }
        public void AddHD(int Idnv , int Idkh ,int Maso, int num, double Tien )
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            float Tien1 = (float)Tien;
            string Noidung = "";
            if (Maso == 2)
            {
                Noidung += Environment.NewLine+" Thêm hợp đồng mới : " + Tien.ToString("c",culture) +Environment.NewLine + " Số tháng : " + num + "          ";
            }           
            LichSu t = new LichSu { idnv=Idnv,idkh=Idkh,maso=Maso,noidung = Noidung,tien=Tien1,ngay = DateTime.Now};
            Data.db.LichSus.Add(t);
            Data.db.SaveChanges();
        }
        public void AddGiaHan(int Idnv ,int  IdHD, int Maso,int num, double Tien)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            float Tien1 = (float)Tien;
            string Noidung = "";
            if (Maso==1)
            {
                Noidung += "\n Gia hạn hợp đồng : " + Tien.ToString("c",culture) + Environment.NewLine + "\n Số tháng : " +num + "          " ;
            }    
            int Idkh = (int)Data.db.Hopdongs.Single(p => p.id == IdHD).idKH;
            LichSu t = new LichSu { idnv = Idnv, idkh = Idkh, maso = Maso, noidung = Noidung, tien = Tien1 ,ngay=DateTime.Now};
            Data.db.LichSus.Add(t);
            Data.db.SaveChanges();
        }
        public void commit(int Idnv, int Idkh,int Maso,string Noidung)
        {
            LichSu t;
            if (Idkh == 0)
            {
                int x = Data.db.TTKHs.Max(p => p.id);

                t = new LichSu { idnv = Idnv, idkh = x, maso = Maso, noidung = Noidung, ngay = DateTime.Now };
                
            }
            else
            {
                t = new LichSu { idnv = Idnv, idkh = Idkh, maso = Maso, noidung = Noidung, ngay = DateTime.Now };

            }
            Data.db.LichSus.Add(t);
            Data.db.SaveChanges();
        }

        public void commitHD(int Idnv, int IdHD, int Maso, string Noidung)
        {
            LichSu t;
            if (IdHD != 0)
                t = new LichSu { idnv = Idnv, idkh = GetTTkhByIDHD(IdHD), maso = Maso, noidung = Noidung, ngay = DateTime.Now };
            else
                t = new LichSu { idnv = Idnv, maso = Maso, noidung = Noidung, ngay = DateTime.Now };
            Data.db.LichSus.Add(t);
            Data.db.SaveChanges();
        }
        public int  GetTTkhByIDHD(int id)
        {
            var l = Data.db.Hopdongs.Single(p => p.id == id);
            return (int)l.idKH;
        }
        public List<LichSu> ListAllLS(DateTime t1 , DateTime t2)
        {
            var l = Data.db.LichSus.Where(p=> p.ngay >= t1.Date && p.ngay <= t2.Date).ToList();
            return l;
        }
        public List<LichSu> ListLSGH(DateTime t1, DateTime t2)
        {
            var l = Data.db.LichSus.Where(p=>p.maso==1&&p.ngay>=t1.Date&&p.ngay<=t2.Date).ToList();
            return l;
        }
        public List<LichSu> ListLSADD(DateTime t1, DateTime t2)
        {
            var l = Data.db.LichSus.Where(p => p.maso == 2 && p.ngay >= t1.Date && p.ngay <= t2.Date).ToList();
            return l;
        }
        public List<LichSu> ListLSCommit(DateTime t1, DateTime t2)
        {
            var l = Data.db.LichSus.Where(p => p.maso == 3 && p.ngay >= t1.Date && p.ngay <= t2.Date).ToList();
            return l;
        }
        public string GetText(int idLS)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            string x = "";
            var l = Data.db.LichSus.Single(p => p.id == idLS);
            x = l.noidung + "          " + "\nID nhân viên : " + l.idnv + Environment.NewLine + "\n Ngày : " + l.ngay.ToString() + Environment.NewLine + "ID khách hàng : " +l.idkh;

            return x;
        }
        public void Del(int id)
        {
            var l = Data.db.LichSus.Single(p => p.id == id);
            Data.db.LichSus.Remove(l);
            Data.db.SaveChanges();
        }
        public List<LichSu> listAdd()
        {
            int x = (int)DateTime.Now.Year;
            var l = Data.db.LichSus.Where(p => p.maso == 2&&p.ngay.Value.Year==x).ToList();
                return l;
        }
        public List<LichSu> listGiaHan()
        {
            int x = (int)DateTime.Now.Year;
            var l = Data.db.LichSus.Where(p => p.maso == 1 && p.ngay.Value.Year == x).ToList();
            return l;
        }
        public void DelByIDNV(int idNV)
        {
            var l = Data.db.LichSus.Where(p => p.idnv == idNV).ToList();
            foreach(LichSu i in l)
            {
                Data.db.LichSus.Remove(i);
            }
            Data.db.SaveChanges();
        }
        public void DelByIDKH(int idKh)
        {
            var l = Data.db.LichSus.Where(p => p.idkh == idKh).ToList();
            foreach (LichSu i in l)
            {
                Data.db.LichSus.Remove(i);
            }
            Data.db.SaveChanges();
        }
        public GoiTap GetGoiTap(int id)
        {
            var l = Data.db.GoiTaps.Single(p => p.id == id);
            return l;
        }
        public List<GoiTap> listGoiTap()
        {

            var l = Data.db.GoiTaps.ToList();
            return l;
        }
        public List<GoiTap> listGoiTapTT()
        {

            var l = Data.db.GoiTaps.Where(p=>(int)p.sobuoi<1).ToList();
            return l;
        }
        public List<GoiTap> listGoiTapPT()
        {

            var l = Data.db.GoiTaps.Where(p => (int)p.sobuoi > 0).ToList();
            return l;
        }
        public void updateGT(int id , string ten,string Mota,int sobuoi,float giatien)
        {
            var l = Data.db.GoiTaps.Single(p => p.id == id);
            l.ten = ten;
            l.mota = Mota;
            l.sobuoi = sobuoi;
            l.giatien = giatien;
            Data.db.SaveChanges();
        }
        public void DelGt(int id)
        {

            var l = Data.db.GoiTaps.Single(p => p.id == id);
            Data.db.GoiTaps.Remove(l);
            Data.db.SaveChanges();

        }
        public bool kiemtraTenGT(string x)
        {
            int l = Data.db.GoiTaps.Where(p => p.ten.Equals(x)).Count();
            if (l > 0) return false;
            else
                return true;
        }
        public void AddGt(string Ten , string Mota,int sobuoi , float giatien)
        {
            GoiTap t = new GoiTap { ten = Ten, mota = Mota, sobuoi = sobuoi, giatien = giatien };
            Data.db.GoiTaps.Add(t);
            Data.db.SaveChanges();
        }

    }
}
