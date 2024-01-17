using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class VM_Replies
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Date { get; set; }
    }
}