using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.BLL
{
    class BLL_QL
    {
        private static BLL_QL _Instance;
        public static BLL_QL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_QL();

                }
                return _Instance;
            }
            private set { }
        }

        private BLL_QL() { }

        public DataGridView Listlichtap(string x)
        {
            DataGridView d = new DataGridView();
            var l1 = from p in DAL_LichTap.Instance.GetTimeByDay(x).ToList()
                     group p by p.ThoiGian.Time into g
                     select new
                     {

                         Time = g.Key,
                         count = g.Count()

                     };
            d.DataSource = l1.ToList();
            return d;

        }
        public DataGridView ListTTKDandPT(string x)
        {
            DataGridView d = new DataGridView();
            var l1 = from p in DAL_LichTap.Instance.GetlichTapByDay(x).ToList()
                     select new { ID = p.id , tenKH = p.Hopdong.TTKH.ten, tenNV = p.Hopdong.PT.TTNV.ten };
            d.DataSource = l1.ToList();
            return d;

        }
        public lichtap GetLichTapById(int id)
        {

            return DAL_LichTap.Instance.GetLichTapById(id);

        }



    }
}
