using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    [Table("BookCategory")]
    public class BookCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
