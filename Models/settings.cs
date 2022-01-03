using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class settings
    {
        [Key]
        [MaxLength(200)]
        public string settingsId { get; set; }
        [MaxLength(500)]
        public string settingsType { get; set; }
        [MaxLength(500)]
        public string settingsKey { get; set; }
       
        public string settingsValue { get; set; }
        [MaxLength(500)]
        public string settingsdescription { get; set; }
        [MaxLength(500)]
        public string settingsmodule { get; set; }
        [MaxLength(500)]
        public string settingsApplication { get; set; }
        [MaxLength(500)]
        public string settingsUsedFor { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastupdatedDate { get; set; }
        [MaxLength(500)]
        public string CreatedBy { get; set; }
        [MaxLength(500)]
        public string LastUpdatedBy { get; set; }
    }

}