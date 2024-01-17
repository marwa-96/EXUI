using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class FraudCasesAttachments
    {
        [Key]
        public int Id { get; set; }
        public int FraudId { get; set; }
        public string URL { get; set; }
        public string AttachmentName { get; set; }
    }
}