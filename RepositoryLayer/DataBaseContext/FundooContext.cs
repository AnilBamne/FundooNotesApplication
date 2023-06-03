using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.DataBaseContext
{
    public class FundooContext:DbContext
    {
        public FundooContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<UserEntity> UserDataTable { get; set; }
        public DbSet<NoteEntity> NoteDataTable { get; set; }
        public DbSet<LabelEntity> LableTable { get; set; }
        public DbSet<CollabEntity> CollabTable { get; set; }
        public DbSet<ProductEntity> ProductTable { get; set; }
        public DbSet<EmpEntity> EmployeeTable { get; set; }
    }
}
