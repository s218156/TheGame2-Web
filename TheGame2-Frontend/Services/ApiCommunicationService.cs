using NuGet.Common;
using TheGame2_Library.Exceptions;
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
                throw new TheGameWebException(res.StatusCode.ToString(), "API rejected request");
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

        public async void CreateUser(UserModel model)
        {
            string path = "/user/create";
            HttpResponseMessage res = await requestService.PostRequestToApi(path, model);
            CheckResponseMessage(res);

        }
		public async void UpdateUserData(string token, UserModel model)
		{
			string path = "/user/update";
			HttpResponseMessage res = await requestService.PostRequestToApi(path, token ,model);
			CheckResponseMessage(res);

		}

		public async void UpdateTextureID(string token, UserModel model)
		{
			string path = "/user/SetTextureID";
			HttpResponseMessage res = await requestService.PostRequestToApi(path, token, model);
			CheckResponseMessage(res);

		}

		public async Task LogoutUser(string token)
        {
            string path = "/user/logout";
            HttpResponseMessage res = await requestService.GetRequestToApi(path, token);
            CheckResponseMessage(res);
        }


        public async Task<GameStatsModel> GetGameStatsForUser(string token)
        {
            string path = "/gamestats/GetGameStats";
            HttpResponseMessage res = await requestService.GetRequestToApi(path, token);
            if (CheckResponseMessage(res))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<GameStatsModel>(res.Content.ReadAsStringAsync().Result);
            else
                return new GameStatsModel();
        }
    }
}
