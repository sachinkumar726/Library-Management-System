using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    [Table("BookIssue")]
    public class BookIssueModel
    {
       
        //Book
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public BookModel Book { get; set; }
        
       
        
        //Member
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualRetuerndate { get; set; }

    }
}
