using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ext_FraudingSystem.Models
{
    public class FraudCases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Case { get; set; }
        public string PersonalInformation { get; set; }
        public string PeopleInvolved { get; set; }
        public string When { get; set; }
        public string Where { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusId { get; set; }
        public int? AssignedPrivId { get; set; }
        public int LabelCaseId { get; set; }
        public bool IsCustomer { get; set; }
        public string CustomerId { get; set; }
        // public string WhenCase { get; set; }

        public string Note { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string NonCustomerName { get; set; }
        public string NonCustomerPhoneNumber { get; set; }
        // public string WhenCase { get; set; }
        public string PhoneNumber { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public DateTime? _when { get; set; }

        [NotMapped]
        public string Status { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string LabelCase { get; set; }
        [NotMapped]
        public string UpdateByName { get; set; }
        [NotMapped]
        public int CaseId { get; set; }
        [NotMapped]
        public string Reply { get; set; }
        [NotMapped]
        public string ReplyBy { get; set; }
        [NotMapped]
        public List<FraudCasesAttachments> Docs { get; set; }
        [NotMapped]
        public string BaseAddress { get; set; }
        [NotMapped]
        public string IsCustomerName { get; set; }

        [NotMapped]
        public string BackgroundColor { get; set; }

    }
}