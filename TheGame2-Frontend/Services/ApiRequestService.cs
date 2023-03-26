using System.Net.Http.Headers;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.Services
{
    public class ApiRequestService
    {
        private string _address;

        public ApiRequestService(string address)
        {
            _address = address;

        }

        public async Task<HttpResponseMessage> PostRequestToApi(string path, UserModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_address);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync<UserModel>(path, model);
                postTask.Wait();
                var result = postTask.Result;
                return result;
            }
        }

        public async Task<HttpResponseMessage> GetRequestToApi(string path, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_address);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("auth", token);
                HttpResponseMessage Res = await client.GetAsync(path);
                return Res;
            }
        }

    }
}
