using Newtonsoft.Json;
using Olive;
using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace Sneakify.Mitsuku
{
    static class Taker
    {
        public static string Talk(this string input)
        {
            var session = ConfigurationFactory.Get("mitsuku:403431885");
            //var request = new HttpRequestMessage
            //{
                
            //    RequestUri = new Uri($"https://miapi.pandorabots.com/talk?botkey=n0M6dW2XZacnOgCWTp0FRYUuMjSfCkJGgobNpgPv9060_72eKnu3Yl-o1v2nFGtSXqfwJBG2Ros~&input={input.UrlEncode()}&client_name=cw16c16d11b31&sessionid={session}&channel=6"),
            //    Method = HttpMethod.Post,
            //    Headers = {

            //        { "Origin", "https://www.pandorabots.com" },
            //        { "Referer", "https://www.pandorabots.com/mitsuku" },
            //        { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36" }
            //    }
                
            //};

            using (var client = new HttpClient())
            {
                var requestUri = new Uri($"https://miapi.pandorabots.com/talk?botkey=n0M6dW2XZacnOgCWTp0FRYUuMjSfCkJGgobNpgPv9060_72eKnu3Yl-o1v2nFGtSXqfwJBG2Ros~&input={input.UrlEncode()}&client_name=cw16c16d11b31&sessionid={session}&channel=6");
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Origin", "https://www.pandorabots.com");
                client.DefaultRequestHeaders.Add("Referer", "https://www.pandorabots.com/mitsuku");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36");
                var response = client.PostAsync(requestUri,null).GetAwaiter().GetResult();
                var txt = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var obj = JsonConvert.DeserializeObject<BotDto>(txt);
                return string.Join(Environment.NewLine, obj.Responses).Trim() ;
            }

           
        }

        
    }
    public partial class BotDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("responses")]
        public string[] Responses { get; set; }

        [JsonProperty("sessionid")]
        public long Sessionid { get; set; }

        [JsonProperty("channel")]
        public long Channel { get; set; }
    }
}
