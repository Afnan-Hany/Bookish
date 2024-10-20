using ApiConsume;
using Library.Models;
using System.Linq.Expressions;
using System.Net.Http;

namespace YatApp.UI_PresentaionLayer.ApiConsume
{
    public class ApiCall : IApiCall
    {
        string baseUrl = null;
        HttpClient client;
        public ApiCall(IConfiguration configuration)
        {
            baseUrl = configuration.GetSection("api")["baseUrl"];
            client = new HttpClient();
            client.BaseAddress=new Uri(baseUrl);
           
        }
        #region Get
        public async Task<IEnumerable<T>> GetAllAsync<T>(string apiName)
        {
            var response = await client.GetAsync(apiName);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }
        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            var response = await client.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<IEnumerable<Member>> GetByRoleAsync(string url, int roleId)
        {
            var response = await client.GetAsync($"{url}/GetByRole/{roleId}"); 
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadAsAsync<IEnumerable<Member>>(); 
        }
        public async Task<Member> GetMemberByUsernameAsync(string url, string username)
        {
            var response = await client.GetAsync($"{url}/GetByUsername/{username}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Member>();
        }

        #endregion

        #region Create
        public async Task<bool> CreateAsync<T>(string url, T entity)
        {
            var response = await client.PostAsJsonAsync(url, entity);
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateAsync<T>(string url, int id, T entity)
        {
            var response = await client.PutAsJsonAsync($"{url}/{id}", entity);
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAsync<T>(string url, int id)
        {
            var response = await client.DeleteAsync($"{url}/{id}");
            return response.IsSuccessStatusCode;
        }

        public Task<IEnumerable<T>> GetByRoleAsync<T>(string url, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetMemberByUserameAsync<Member>(string url, string Username)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
