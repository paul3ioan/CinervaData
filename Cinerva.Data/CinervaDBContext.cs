using Microsoft.EntityFrameworkCore;
using Cinerva.Data.Entities;
namespace Cinerva.Data
{
    public class CinervaDBContext : DbContext
    {
        public CinervaDBContext(DbContextOptions<CinervaDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GeneralFeature> GeneralFeatures { get; set; }
        public DbSet<PropertyFacility> PropertyFacilities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<RoomFeature> RoomFeatures { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities).WithOne(x => x.Country);
            modelBuilder.Entity<City>()
                .HasMany(p => p.Properties).WithOne(x => x.City);
            modelBuilder.Entity<Property>()
                .HasOne(p => p.City).WithMany(x => x.Properties).HasForeignKey(p => p.CityId);
            modelBuilder.Entity<Role>()
                .HasMany(p => p.Users).WithOne( p=>p.Role).HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<User>()
                .HasMany(p => p.Reservations).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Reservation>()
                .HasOne(p => p.User).WithMany(p => p.Reservations).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role).WithMany(x => x.Users).HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Properties).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Property>()
                .HasOne(x => x.User).WithMany(x => x.Properties).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Reviews).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Review>()
                .HasOne(x => x.User).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<PropertyType>()
                .HasMany(x => x.Properties).WithOne(x => x.PropertyType).HasForeignKey(x => x.PropertyTypeId);
            modelBuilder.Entity<Property>()
                .HasOne(x=> x.PropertyType).WithMany(x => x.Properties).HasForeignKey(x => x.PropertyTypeId);
            modelBuilder.Entity<Property>()
                .HasMany(x => x.Reviews).WithOne(x => x.Property).HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<Review>()
                .HasOne(x => x.Property).WithMany(x => x.Reviews).HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<PropertyImage>()
                .HasOne(p => p.Property).WithMany(p=>p.PropertyImages).HasForeignKey(p=>p.PropertyId);
            modelBuilder.Entity<Property>()
                .HasMany(x =>x.PropertyImages).WithOne(p=>p.Property).HasForeignKey(p=>p.PropertyId);
            modelBuilder.Entity<Property>()
                .HasMany(x => x.Rooms).WithOne(x => x.Property).HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<Room>()
                .HasOne(x => x.Property).WithMany(x => x.Rooms).HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<Room>()
                .HasOne(x => x.RoomCategory).WithMany(x => x.Rooms).HasForeignKey(x => x.RoomCategoryId);
            modelBuilder.Entity<RoomCategory>()
                .HasMany(x => x.Rooms).WithOne(x => x.RoomCategory).HasForeignKey(x => x.RoomCategoryId);
            modelBuilder.Entity<Property>()
               .HasMany(p => p.GeneralFeatures)
               .WithMany(p => p.Properties)
               .UsingEntity<PropertyFacility>(
               x => x.HasOne(f => f.GeneralFeature)
               .WithMany(g => g.PropertyFacilities)
               .HasForeignKey(p => p.FacilityId),
               x => x.HasOne(f => f.Property).WithMany(p => p.PropertyFaclities)
               .HasForeignKey(f => f.PropertyId),
               x => x.HasKey(f => new { f.PropertyId, f.FacilityId })
               );
            modelBuilder.Entity<Room>()
                .HasMany(p => p.RoomFeatures)
                .WithMany(p => p.Rooms)
                .UsingEntity<RoomFacility>(
                x => x.HasOne(x => x.RoomFeature)
                .WithMany(x => x.RoomFacilities).HasForeignKey(x => x.FacilityId),
                x => x.HasOne(x => x.Room).WithMany(x => x.RoomFacilities).HasForeignKey(x => x.RoomId),
                x => x.HasKey(x => new { x.RoomId, x.FacilityId })
                );
            modelBuilder.Entity<Room>()
              .HasMany(p => p.Reservations)
              .WithMany(p => p.Rooms)
              .UsingEntity<RoomReservation>(
              x => x.HasOne(f => f.Reservation)
              .WithMany(g => g.RoomReservations)
              .HasForeignKey(p => p.ReservationId),
              x => x.HasOne(f => f.Room).WithMany(p => p.RoomReservations)
              .HasForeignKey(f => f.RoomId),
              x => x.HasKey(f => f.Id)
              );
            
            modelBuilder.Entity<PropertyFacility>()
                .HasKey(pf => new { pf.PropertyId, pf.FacilityId });
            modelBuilder.Entity<RoomFacility>()
                .HasKey(rf => new { rf.RoomId, rf.FacilityId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
