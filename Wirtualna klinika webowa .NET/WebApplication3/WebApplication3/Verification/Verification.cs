using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Verification : IDisposable
    {
        private HttpRequestBase request;

        public Verification(HttpRequestBase request)
        {
            this.request = request;
        }

        public bool IsAdmin()
        {
            if (request.Cookies["user"] == null) return false;
            if (request.Cookies["user"].Value == "admin") return true;
            return false;
        }

        public int GetCurrentUserID()
        {
            if (request.Cookies["user"] == null) return -1;
            if (request.Cookies["user"].Value != "patient") return -1;
            if (request.Cookies["user_id"] == null) return -1;
            int value;
            if (int.TryParse(request.Cookies["user_id"].Value, out value)) return value;
            else return -1;            
        }


        public void Dispose()
        {
            
        }
    }
}