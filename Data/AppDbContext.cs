// using Midterm.Models;
// using Microsoft.EntityFrameworkCore;


// namespace Midterm.Data
// {
//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//         {
//         }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<User>().Property(p => p.UserID).ValueGeneratedOnAdd();
//             modelBuilder.Entity<User>().Property(p => p.Created).HasDefaultValueSql("GETDATE()");
//             modelBuilder.Entity<User>().Property(p => p.Modified).HasDefaultValueSql("GETDATE()");

//             modelBuilder.Entity<Room>()
//                 .HasOne(r => r.RoomType)
//                 .WithMany()
//                 .HasForeignKey(r => r.RoomTypeId)
//                 .OnDelete(DeleteBehavior.Restrict);

//             base.OnModelCreating(modelBuilder);
//         }

//         public DbSet<User> Users { get; set; }
//         public DbSet<Employee> Employees { get; set; }
//         public DbSet<Customer> Customers { get; set; }
//         public DbSet<Room> Rooms { get; set; }
//         public DbSet<RoomType> RoomTypes { get; set; }
//     }
// }







using Midterm.Models;
using Microsoft.EntityFrameworkCore;

namespace Midterm.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.UserID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(p => p.Created)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(p => p.Modified)
                .HasDefaultValueSql("GETDATE()");

            // Room and RoomType Relationship
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany()
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation and Customer Relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation and Room Relationship (Since ReservationDetail is merged)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; } // Updated Reservation model
    }
}

