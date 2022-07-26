using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class userinfoschema
    {
        [MaxLength(250)]
        public string name { get; set; }
        [Key]
        [MaxLength(250)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        [MaxLength(250)]
        public string password { get; set; }
        public DateTime? registerDate { get; set; }
        public DateTime? lastLoginDate { get; set; }
        public bool? activateDeactivate { get; set; }
        [MaxLength(300)]
        public string activateDeactivatedBy { get; set; }
        public DateTime? activateDeactivatedatetime { get; set; }
        public DateTime? passwordCreatedDate { get; set; }
    }

}