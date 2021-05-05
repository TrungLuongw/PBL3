using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    class HopDong
    {
        public int id { get; set; }
        public int idKH { get; set; }
        public int idPT { get; set; }
        public int idGoiTap { get; set; }
        public int idStatus { get; set; }
        public DateTime NgayKi { get; set; }
        public DateTime NgayHH { get; set; }
        public float sotien { get; set; }
        public int sobuoi { get; set; }

        public HopDong(int id , int idkh , int idpt , int idgoitap ,int idstatus , DateTime n , DateTime n1 , float sotien , int sobuoi)
        {
            this.id = id;
            this.idKH = idkh;
            this.idPT = idpt;
            this.idGoiTap = idgoitap;
            this.idStatus = idstatus;
            this.NgayKi = n;
            this.NgayHH = n1;
            this.sotien = sotien;
            this.sobuoi = sobuoi;
        }

    }
}
