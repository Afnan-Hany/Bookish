using System.ComponentModel;
using System.Linq.Expressions;
using Library.Models;

namespace Interfaces;
public interface IRepository<T> where T : class
{
    #region Get
    T GetById(int id);
    Task<T> GetByIdAsync(int id);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    public List<string> GetDistinct(Expression<Func<T, string>> col);
    Task<IEnumerable<Member>> GetMembersByRoleAsync(int roleId);
    Task<Member> GetMemberByUsernameAsync(string username);
    #endregion

    #region Find
    T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
    Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
        Expression<Func<T, object>> orderBy = null, bool IsDesc = false);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
        Expression<Func<T, object>> orderBy = null, bool IsDesc = false);

    IEnumerable<T> FindWithFilters(
    Expression<Func<T, bool>> criteria = null,
    string sortColumn = null,
    string sortColumnDirection = null,
    int? skip = null,
    int? take = null);
    #endregion

    #region Add
    T Add(T entity);
    Task<T> AddAsync(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    #endregion
    
    #region Update
    T Update(T entity);
    bool UpdateRange(IEnumerable<T> entities);
    #endregion
    
    #region Delete
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    #endregion
    
    #region Aggregate Function
    int Count();
    int Count(Expression<Func<T, bool>> criteria);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    Task<Int64> MaxAsync(Expression<Func<T, object>> column);

    Task<Int64> MaxAsync(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column);

    Int64 Max(Expression<Func<T, object>> column);

    Int64 Max(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column);
    #endregion
    
    #region IsExist Last
    public bool IsExist(Expression<Func<T, bool>> criteria);
    T Last(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy); 
    #endregion

}
