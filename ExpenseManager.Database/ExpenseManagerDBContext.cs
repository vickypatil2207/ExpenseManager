using ExpenseManager.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Database
{
    public class ExpenseManagerDBContext : DbContext
    {
        public ExpenseManagerDBContext()
        {

        }
        public ExpenseManagerDBContext(DbContextOptions<ExpenseManagerDBContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<DefaultExpenseCategory> DefaultExpenseCategories { get; set; }
        public DbSet<UserExpenseCategory> UserExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder != null)
            {
                modelBuilder.HasDefaultSchema("dbo");

                modelBuilder.Entity<User>(ConfigureUser);
                modelBuilder.Entity<PaymentType>(ConfigurePaymentType);
                modelBuilder.Entity<DefaultExpenseCategory>(ConfigureDefaultExpenseCategory);
                modelBuilder.Entity<UserExpenseCategory>(ConfigureUserExpenseCategory);
                modelBuilder.Entity<Expense>(ConfigureUserExpenseCategory);
            }
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).UseIdentityColumn().IsRequired();
            builder.Property(u => u.Firstname).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(40);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(200);
            builder.Property(u => u.DateOfBirth).HasColumnType("date");
            builder.Property(u => u.Gender).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(u => u.RegistrationDate).HasColumnType("datetime2");

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
        }

        private void ConfigurePaymentType(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("PaymentTypes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);

            builder.HasData(new PaymentType() { Id = 1, Description = "Cash" },
                new PaymentType() { Id = 2, Description = "Debit Card" },
                new PaymentType() { Id = 3, Description = "Credit Card" },
                new PaymentType() { Id = 4, Description = "Net Banking" },
                new PaymentType() { Id = 5, Description = "UPI" },
                new PaymentType() { Id = 6, Description = "Check" },
                new PaymentType() { Id = 7, Description = "Other (Unspecified)" }
            );
        }

        private void ConfigureDefaultExpenseCategory(EntityTypeBuilder<DefaultExpenseCategory> builder)
        {
            builder.ToTable("DefaultExpenseCategories");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn().IsRequired();
            builder.Property(d => d.Title).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Description).HasMaxLength(500);

            builder.HasData(new DefaultExpenseCategory() { Id = 1, Title = "Housing", Description = "Rent, Mortgage, Property Taxes, Home Insurance" },
                new DefaultExpenseCategory() { Id = 2, Title = "Utilities", Description = "Electricity, Gas, Water, Internet, Cable/Streaming" },
                new DefaultExpenseCategory() { Id = 3, Title = "Groceries", Description = "Food, Beverages, Household Supplies" },
                new DefaultExpenseCategory() { Id = 4, Title = "Transportation", Description = "Gas, Public Transport, Car Maintenance, Parking" },
                new DefaultExpenseCategory() { Id = 5, Title = "Dining Out", Description = "Restaurants, Cafes, Fast Food" },
                new DefaultExpenseCategory() { Id = 6, Title = "Entertainment", Description = "Movies, Concerts, Sports, Hobbies" },
                new DefaultExpenseCategory() { Id = 7, Title = "Shopping", Description = "Clothing, Shoes, Accessories, Electronics" },
                new DefaultExpenseCategory() { Id = 8, Title = "Healthcare", Description = "Doctor visits, Prescriptions, Medical Supplies" },
                new DefaultExpenseCategory() { Id = 9, Title = "Personal Care", Description = "Hair Salons, Gyms, Cosmetics" },
                new DefaultExpenseCategory() { Id = 10, Title = "Education", Description = "Tuition fees, Books, School Supplies" },
                new DefaultExpenseCategory() { Id = 11, Title = "Travel", Description = "Flights, Hotels, Car Rentals, Tours" },
                new DefaultExpenseCategory() { Id = 12, Title = "Gifts", Description = "Birthdays, Holidays, Special Occasions" },
                new DefaultExpenseCategory() { Id = 13, Title = "Pets", Description = "Food, Vet bills, Supplies" },
                new DefaultExpenseCategory() { Id = 14, Title = "Subscriptions", Description = "Streaming services, Gym memberships, Magazines" },
                new DefaultExpenseCategory() { Id = 15, Title = "Insurance", Description = "Health insurance, Life insurance, Car insurance" },
                new DefaultExpenseCategory() { Id = 16, Title = "Debt Repayment", Description = "Loans, Credit Cards" },
                new DefaultExpenseCategory() { Id = 17, Title = "Savings", Description = "Retirement savings, Emergency fund" },
                new DefaultExpenseCategory() { Id = 18, Title = "Charitable", Description = "Donations	Non-profit organizations" },
                new DefaultExpenseCategory() { Id = 19, Title = "Home Improvement", Description = "Repairs, Renovations, Landscaping" },
                new DefaultExpenseCategory() { Id = 20, Title = "Other", Description = "Miscellaneous expenses not fitting other categories" }
            );

        }

        private void ConfigureUserExpenseCategory(EntityTypeBuilder<UserExpenseCategory> builder)
        {
            builder.ToTable("UserExpenseCategories");
            builder.HasKey(uec => uec.Id);
            builder.Property(uec => uec.Id).UseIdentityColumn().IsRequired();
            builder.Property(uec => uec.Title).IsRequired().HasMaxLength(100);
            builder.Property(uec => uec.Description).HasMaxLength(500);

            builder.HasOne(uec => uec.User)
                .WithMany(u => u.UserExpenseCategories)
                .HasForeignKey(uec => uec.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(uec => uec.UserId);
        }

        private void ConfigureUserExpenseCategory(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
            builder.Property(e => e.ExpenseDate).HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.Amount).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(e => e.Notes).HasMaxLength(500);
            builder.Property(e => e.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.ModifiedDate).HasColumnType("datetime2");

            builder.HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UserExpenseCategory)
                .WithMany(uec => uec.Expenses)
                .HasForeignKey(e => e.UserExpenseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PaymentType)
                .WithMany(p => p.Expenses)
                .HasForeignKey(e => e.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.UserId);
            builder.HasIndex(e => e.UserExpenseCategoryId);
            builder.HasIndex(e => e.PaymentTypeId);
        }
    }
}
