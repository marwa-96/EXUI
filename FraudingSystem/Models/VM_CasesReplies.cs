using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class VM_CasesReplies
    {
        public string Case { get; set; }
        public List<VM_Replies> Replys { get; set; }
    }
}