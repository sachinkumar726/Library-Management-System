using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    [Table("Member")]
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        
        //MemberType
        [ForeignKey("MemberType")]
        public int MemberTypeId { get; set; }
        public MemberTypeModel MemberType { get; set; }
        
        public DateTime MemberDate { get; set; }
        public DateTime MemberExpDate { get; set; }
    }
}
