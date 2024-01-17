﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class ReportingPolicy_Attachment
    {

        [Key]
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
    }
}