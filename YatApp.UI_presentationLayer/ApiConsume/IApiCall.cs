using Library.Models;

namespace ApiConsume;
public interface IApiCall
{
    Task<IEnumerable<T>> GetAllAsync<T>(string url);
    Task<IEnumerable<T>> GetByRoleAsync<T>(string url, int roleId);
    Task <Member> GetMemberByUserameAsync<Member>(string url,string Username);
    Task<T> GetByIdAsync<T>(string url, int id);
    Task<bool> CreateAsync<T>(string url, T entity);
    Task<bool> UpdateAsync<T>(string url, int id, T entity);
    Task<bool> DeleteAsync<T>(string url, int id);

}
