using MessageService.UI.Dtos;
using MessageService.UI.Repos.IRepos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.UI.Repos
{
    public class MessageRepo : IMessageRepo
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Security _secret;
        public MessageRepo(IHttpClientFactory httpClientFactory, IOptions<Security> security)
        {
            _httpClientFactory = httpClientFactory;
            _secret = security.Value;
        }

        public async Task<SingleMessageResponse> SendMessage(string url, OutMessageDto outMessageDto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                if (outMessageDto != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(outMessageDto), Encoding.UTF8, "application/json");
                }
                else
                {
                    return new SingleMessageResponse { Success = false };
                }
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Token", _secret.Key.ToString());

                HttpResponseMessage httpResponseMessage = await client.SendAsync(request);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SingleMessageResponse>(jsonData);
                }
                return new SingleMessageResponse { Success = false };
            }
            catch (System.Exception e)
            {

                throw new System.Exception(e.Message);
            }
           
        }

        public async Task<ListMessageResponse> GetMessages(string url)
        {
            try
            {
               
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Token", _secret.Key.ToString());

                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ListMessageResponse>(jsonData);
                }
                return new ListMessageResponse { Success = false };
            }
            catch (System.Exception e)
            {

                throw new System.Exception(e.Message);
            }

        }
        public async Task<SingleMessageResponse> GetMessage(string url)
        {
            try
            {
               
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Token", _secret.Key.ToString());

                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SingleMessageResponse>(jsonData);
                }
                return new SingleMessageResponse { Success = false };
            }
            catch (System.Exception e)
            {

                throw new System.Exception(e.Message);
            }

        }
    }
}
