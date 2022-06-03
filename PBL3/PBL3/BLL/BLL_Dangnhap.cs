using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_Dangnhap
    {
        private static BLL_Dangnhap _Instance;
        public static BLL_Dangnhap Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Dangnhap();

                }
                return _Instance;
            }
            private set { }
        }

        public bool KiemTraDangNhap(string user , string pass)
        {
            int x = 0;
            x = (int)Data.db.taikhoans.Where(p => p.id.Equals(user) && p.pass.Equals(pass)).ToList().Count;
            if (x > 0) return true;
            else
            {
                return false;
            }
        }
        public int iduser(string user, string pass)
        {
            var l = Data.db.taikhoans.Single(p => p.id.Equals(user) && p.pass.Equals(pass));
            return (int)l.idnv;
        }
        public bool checklevel(int iduser)
        {
            var l = Data.db.TTNVs.Single(p => p.id == iduser);
            if (l.idchucvu == 1) return true;
            else return false;
        }
        public bool KiemTraPass(int iduser , string oldpass , string newpass)
        {
            var l = Data.db.taikhoans.Where(p => p.idnv == iduser).Single();
            if(l.pass.Equals(oldpass))
            {
                l.pass = newpass;
                Data.db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<taikhoan> ListTk(int idtype)
        {
            if (idtype == 0)
            {
                var l1 = Data.db.taikhoans.ToList();
                return l1;
            }
            
            var l = Data.db.taikhoans.Where(p => p.TTNV.idchucvu == idtype).ToList();
            return l;
        }
        public taikhoan GetTkById(string n)
        {
            var l = Data.db.taikhoans.Single(p => p.id.Equals(n));
            return l;
        }
        public void DelTK(string n)
        {
            var l = Data.db.taikhoans.Single(p => p.id.Equals(n));
            Data.db.taikhoans.Remove(l);
            Data.db.SaveChanges();
        }
        public List<CBBItems> listNewUser()
        {
            List<CBBItems> list = new List<CBBItems>();
            foreach(TTNV i in Data.db.TTNVs.Where(p=>p.taikhoans.Count==0 &&p.id!=0).ToList())
            {
                list.Add(new CBBItems { value = (int)i.id, text = i.id + "," + i.ten });
            }
            return list;
        }
        public void resetpw(string n)
        {
            var l = Data.db.taikhoans.Single(p => p.id.Equals(n));
            l.pass = "1";
            Data.db.SaveChanges();
        }
        public void updatetk(string n,int type , string pass)
        {
            var l = Data.db.taikhoans.Single(p => p.id.Equals(n));
            l.pass = pass;
            l.TTNV.idchucvu = type;
            Data.db.SaveChanges();
        }
        public void createTK(string n ,int Idnv,string pass)
        {
            taikhoan t = new taikhoan { id = n, idnv = Idnv, pass = pass };
            Data.db.taikhoans.Add(t);
            Data.db.SaveChanges();
        }
    }
}
