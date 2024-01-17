using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FraudingSystem.Models
{
    public class FraudDescriptionContent
    {
        [Key]
        public int Id { get; set; }
        public string Description_box { get; set; }
        public string DescriptionboxEN { get; set; }
      
    }
}