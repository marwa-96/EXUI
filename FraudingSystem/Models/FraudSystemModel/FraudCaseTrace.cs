using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class FraudCaseTrace
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FraudId { get; set; }
        public string Reply { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public List<FraudCasesAttachments> Docs { get; set; }
    }
}