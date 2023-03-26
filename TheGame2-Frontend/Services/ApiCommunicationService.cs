using TheGame2_Library.Models;

namespace TheGame2_Frontend.Services
{
    public class ApiCommunicationService
    {
        private ApiRequestService requestService;

        public ApiCommunicationService(IConfiguration config)
        {
            requestService = new ApiRequestService(config.GetValue<string>("ApiConfig:address")); ;
        }

        private bool CheckResponseMessage(HttpResponseMessage res)
        {
            if (res.StatusCode.ToString() == "600")
                throw new Exception(res.StatusCode.ToString());
            return res.IsSuccessStatusCode;

        }

        public async Task<UserModel> LoginUser(UserModel model)
        {
            string path = "/User/Login";
            HttpResponseMessage res = await requestService.PostRequestToApi(path, model);
            if (CheckResponseMessage(res))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(res.Content.ReadAsStringAsync().Result);
            else
                throw new Exception();
            return new UserModel();

        }

        public async Task<UserModel> GetUserData(string token)
        {
            string path = "/user/GetUserData";
            HttpResponseMessage res = await requestService.GetRequestToApi(path, token);
            if (CheckResponseMessage(res))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(res.Content.ReadAsStringAsync().Result);
            else
                return new UserModel();
        }
    }
}
