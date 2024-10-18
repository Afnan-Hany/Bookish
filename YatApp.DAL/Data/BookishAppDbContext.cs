using System;
using System.Collections.Generic;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public partial class BookishAppDbContext : DbContext
{
   

    public BookishAppDbContext(DbContextOptions<BookishAppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Checkout> Checkouts { get; set; }
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<Penalty> Penalties { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Series> Series { get; set; }
    public virtual DbSet<Shipping> Shippings { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }





}