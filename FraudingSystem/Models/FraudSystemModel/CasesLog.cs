using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class CasesLog
    {
        [Key]
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string UpdateUserId { get; set; } // اللي بيعدل دلوقتي
        public int StatusId { get; set; } // الstatus الجديده
        public int AssignedPrivId { get; set; } // عملها اسين لمين
        public int LabelCaseId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }
        [NotMapped]
        public string Status { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string LabelCase { get; set; }
        [NotMapped]
        public string Case { get; set; }
        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public string UpdateByName { get; set; }
    }
}