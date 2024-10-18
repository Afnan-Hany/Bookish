using Interface;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Interfaces;
namespace Repo;

public class UnitOfWork : IUnitofWork
{
    private readonly BookishAppDbContext _context;

    public UnitOfWork(BookishAppDbContext context)
    {
        _context = context;
        Books = new Repository<Book>(_context);
        Authors = new Repository<Author>(_context);
        Categories = new Repository<Category>(_context);
        Checkouts = new Repository<Checkout>(_context);
        Members = new Repository<Member>(_context);
        Orders = new Repository<Order>(_context);
        Payments = new Repository<Payment>(_context);
        PaymentMethods = new Repository<PaymentMethod>(_context);
        Penalties = new Repository<Penalty>(_context);
        Roles = new Repository<Role>(_context);
        Series = new Repository<Series>(_context);
        Shippings = new Repository<Shipping>(_context);
        OrderDetails = new Repository<OrderDetail>(_context);
    }
    public IRepository<Book> Books { get; }
    public IRepository<Author> Authors { get; }
    public IRepository<Category> Categories { get; }
    public IRepository<Checkout> Checkouts { get; }
    public IRepository<Member> Members { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<Payment> Payments { get; }
    public IRepository<PaymentMethod> PaymentMethods { get; }
    public IRepository<Penalty> Penalties { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<Series> Series { get; }
    public IRepository<Shipping> Shippings { get; }
    public IRepository<OrderDetail> OrderDetails { get; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

   
}
