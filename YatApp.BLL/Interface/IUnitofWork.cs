using Interfaces;
using Library.Models;

namespace Interface;

public interface IUnitofWork:IDisposable
{
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

    int Save();

}
