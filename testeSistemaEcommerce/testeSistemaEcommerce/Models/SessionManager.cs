using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testeSistemaEcommerce.Models
{
    public class SessionManager
    {
        public static int? IdUser
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdUser"]);
            }
            set
            {
                HttpContext.Current.Session["IdUser"] = value;
            }
        }
        public static dynamic CryIDUser
        {
            get
            {
                return HttpContext.Current.Session["CryIDUser"];
            }
            set
            {
                HttpContext.Current.Session["CryIDUser"] = value;
            }
        }
        public static dynamic ArrayNSelec
        {
            get
            {
                return HttpContext.Current.Session["ArrayNSelec"];
            }
            set
            {
                HttpContext.Current.Session["ArrayNSelec"] = value;
            }
        }

    }
}