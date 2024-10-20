using ApiConsume;
using Microsoft.AspNetCore.Mvc;

namespace YatApp.UI_presentationLayer.Base
{
    public class BaseUiController: Controller
    {
        protected readonly IApiCall _api;

        public BaseUiController(IApiCall api)
        {
            _api = api;
        }
    }
}
