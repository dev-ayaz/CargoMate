namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Length> Lengths { get; set; }
        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ModelYearCombination> ModelYearCombinations { get; set; }
        public virtual DbSet<PayLoadType> PayLoadTypes { get; set; }
        public virtual DbSet<VehicleCapacity> VehicleCapacities { get; set; }
        public virtual DbSet<VehicleTypeConfiguration> VehicleTypeConfigurations { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Weight> Weights { get; set; }
        public virtual DbSet<Year> Years { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<LocalizedCapacity> LocalizedCapacities { get; set; }
        public virtual DbSet<LocalizedCountry> LocalizedCountries { get; set; }
        public virtual DbSet<LocalizedLength> LocalizedLengths { get; set; }
        public virtual DbSet<LocalizedMake> LocalizedMakes { get; set; }
        public virtual DbSet<LocalizedModel> LocalizedModels { get; set; }
        public virtual DbSet<LocalizedPayLoadType> LocalizedPayLoadTypes { get; set; }
        public virtual DbSet<LocalizedVehicleType> LocalizedVehicleTypes { get; set; }
        public virtual DbSet<LocalizedVehicleTypesConfiguration> LocalizedVehicleTypesConfigurations { get; set; }
        public virtual DbSet<LocalizedWeight> LocalizedWeights { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Make>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Make>()
                .HasMany(e => e.LocalizedMakes)
                .WithOptional(e => e.Make)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Model>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.LocalizedModels)
                .WithOptional(e => e.Model)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ModelYearCombination>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<PayLoadType>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<PayLoadType>()
                .HasMany(e => e.LocalizedPayLoadTypes)
                .WithOptional(e => e.PayLoadType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleCapacity>()
                .Property(e => e.CultureCode)
                .IsFixedLength();

            modelBuilder.Entity<VehicleCapacity>()
                .HasMany(e => e.LocalizedCapacities)
                .WithOptional(e => e.VehicleCapacity)
                .HasForeignKey(e => e.CapacityId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleTypeConfiguration>()
                .HasMany(e => e.LocalizedVehicleTypesConfigurations)
                .WithOptional(e => e.VehicleTypeConfiguration)
                .HasForeignKey(e => e.ConfigurationId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.PayLoadTypes)
                .WithOptional(e => e.VehicleType)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.VehicleCapacities)
                .WithOptional(e => e.VehicleType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.VehicleTypeConfigurations)
                .WithOptional(e => e.VehicleType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.LocalizedVehicleTypes)
                .WithOptional(e => e.VehicleType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Year>()
                .Property(e => e.YearName)
                .IsUnicode(false);
        }
    }
}
