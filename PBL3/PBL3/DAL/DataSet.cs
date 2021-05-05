using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DAL
{
    public class DataSet
    {
        private static CSDL _db = null;
        
        private DataSet() { }
        public static CSDL db
        {
            get
            {
                if (_db == null)
                {
                    _db = new CSDL();

                }
                return _db;
            }
            private set { }
        }


    }
}
