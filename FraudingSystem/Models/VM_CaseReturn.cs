using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem
{
    public class VM_CaseReturn
    {
        public int CaseId { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public string UName { get; set; }
        public string Pwd { get; set; }
    }
}