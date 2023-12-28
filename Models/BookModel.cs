using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Models
{
    [Table("Book")]
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Author { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        
        //Publisher
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public PublisherModel Publisher { get; set; }

       //BookCategory
        [ForeignKey("BookCategory")]
        public int BookTypeId { get; set; }
        public BookCategoryModel BookCategory { get; set; }

        public string FileName { get; set; }

        
    }
}
