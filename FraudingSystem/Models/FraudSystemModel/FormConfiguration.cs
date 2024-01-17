using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ext_FraudingSystem.Models
{
    public class FormConfiguration
    {
        [Key]
        public int Id { get; set; }
        public int PropId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsHidden { get; set; }
        public string PropName { get; set; }

        public string SysPropName { get; set; }
    }


    //public static class FormConfigurationList
    //{

    //    public static List<FormConfiguration> GetList()
    //    {
    //        return new List<FormConfiguration>
    //        {
    //            new FormConfiguration { Id = 0, IsHidden = false, IsRequired = true, PropId = 0, SysPropName = "Title" },
    //            new FormConfiguration { Id = 1, IsHidden = false, IsRequired = true, PropId = 1, SysPropName = "Case" },
    //            new FormConfiguration { Id = 2, IsHidden = false, IsRequired = false, PropId = 2, SysPropName = "Personal Information (Optional)" },
    //            new FormConfiguration { Id = 3, IsHidden = false, IsRequired = true, PropId = 3, SysPropName = "People Involved" },
    //            new FormConfiguration { Id = 4, IsHidden = false, IsRequired = true, PropId = 4, SysPropName = "When" },
    //            new FormConfiguration { Id = 5, IsHidden = false, IsRequired = true, PropId = 5, SysPropName = "Where" },

    //        };



    //    }

    //}

    public enum FormLables
    {
        Title,
        Case,
        PersonalInformation,
        PeopleInvolved,
        When,
        Where,


    }
}