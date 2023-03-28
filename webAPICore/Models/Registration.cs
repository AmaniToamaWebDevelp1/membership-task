
using System;
using System.Collections.Generic;
using System.Threading.Tasks;




namespace webAPICore.Models
{
    public class Registration
    {
        public int Id {get; set;}
        public string username {get; set;}
        public string email  {get; set;}
        public string password {get; set;}
    }
}