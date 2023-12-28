using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    [Table("Fine")]
    public class FineModel
    {
        
        public int Id { get; set; }

        //BookIssue
        [ForeignKey("BookIssue")]
        public int BookIssueId { get; set; }
        public BookIssueModel BookIssue { get; set; }

        public double Amount { get; set; }

        
    }
}
