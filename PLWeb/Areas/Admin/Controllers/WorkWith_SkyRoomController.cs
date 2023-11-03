using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLCore.DTOs.Course;

namespace PLWeb.Areas.Admin.Controllers
{
    [Route("https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe")]
    [ApiController]
    public class WorkWithSkyRoomController : ControllerBase
    {
        // HttpClient singleton
        public static readonly HttpClient HttpClient = new HttpClient();
        
        //public async Task<IActionResult> GetRoom(PostData data)
        //{
        //    if (data.APIUrl.Length == 0 || data.Action.Length == 0)
        //    {
        //        return Content("Invalid request");
        //    }

        //    // Initiate params object
        //    var paramz = new Dictionary<string, object>();
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, data.APIUrl);
            
        //    string json = JsonConvert.SerializeObject(request);

        //    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        //    HttpClient http = new HttpClient();
        //    HttpResponseMessage response = await http.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //    }
        //    else
        //    {
        //    }
        //}
    }
}