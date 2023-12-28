using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<BookModel> Book { get; set; }
        public DbSet<BookCategoryModel> BookCategory { get; set; }
        public DbSet<BookIssueModel> BookIssue  { get; set; }
        public DbSet<MemberModel> Member { get; set; }
        public DbSet<MemberTypeModel> MemberType { get; set; }
        public DbSet<PublisherModel> Publisher { get; set; }
        public DbSet<LibrarianModel> Librarian { get; set; }
        public DbSet<FineModel> Fine { get; set; }
        
    }
}
