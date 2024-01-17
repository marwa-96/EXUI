using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class ChangeFraudData
    {
        public int FraudId { get; set; }
        public int? StatusId { get; set; }
        public int? AssignedPrivId { get; set; }
        public int? LabelCaseId { get; set; }
        public string Note { get; set; }

    }
}