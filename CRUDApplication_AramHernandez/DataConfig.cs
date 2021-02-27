using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CRUDApplication_AramHernandez
{
    public class DataConfig
    {
        static string _constr = null;
        public static string ConnectionString
        {
            get
            {
                if (_constr == null)
                {
                    _constr = ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString;
                }
                return _constr;
            }
        }
    }
}