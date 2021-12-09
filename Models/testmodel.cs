using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class testmodel
    {
        public class SLBAdminUser
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string MobileNo { get; set; }
            public string BlockNo { get; set; }
            public string Postalcode { get; set; }
            public string SLBId { get; set; }
            public string NFCdata { get; set; }
            public string SingPostID { get; set; }
            public string RoleId { get; set; }
            public bool? IsActive { get; set; }
            public string Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public DateTime? LastUpdated { get; set; }
        }

    }

}