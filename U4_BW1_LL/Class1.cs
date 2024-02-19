using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;

namespace U4_BW1_LL
{
    public class MyConnection
    {
        private string connectioString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
        private static MyConnection instance;


        private MyConnection() { 
        
        }
        public static MyConnection getInstance()
        {
            if (instance == null)
            {
                instance = new MyConnection();
                return instance;
            }
            else { 
            
            return instance;
            }
        }

        public string GetConnectionString()
        {
            return connectioString;
        }

    }
}