using FundsLibrary.InterviewTest.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAndReadFromContentGetAsync<T>(string apiFundmanager);
        Task<Guid> Post(FundManager fundManager, string requestUri);
        Task<T> PostFromContentPostAsync<T>(string requestUri, T postObj);
        Task<T> PutFromContentPutAsync<T>(string requestUri, T putObj);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly string _serviceAppUrl;

        public HttpClientWrapper(string serviceAppUrl)
        {
            _serviceAppUrl = serviceAppUrl;
        }

        public async Task<T> GetAndReadFromContentGetAsync<T>(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUri);

               
                response.EnsureSuccessStatusCode();
               
                //TODO: Handle non success HTTP codes more gracefully.

                return await response.Content.ReadAsAsync<T>();
            }
        }

        //public async Task<TResult> PostAsync<TEntity, TResult>(TEntity entity, string url)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new StringContent(JsonConvert.SerializeObject(entity)) };
        //    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var result = await _http.SendAsync(request);
        //    return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        //}

        //public async Task<TResult> PutAsync<TEntity, TResult>(TEntity entity, string url)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = new StringContent(JsonConvert.SerializeObject(entity)) };
        //    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var result = await _http.SendAsync(request);
        //    return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        //}


        public async Task<Guid> Post(FundManager fundManager, string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(fundManager), Encoding.UTF8, "application/json"));


                response.EnsureSuccessStatusCode();

                //TODO: Handle non success HTTP codes more gracefully.

                return await response.Content.ReadAsAsync<Guid>();
            }
        }


        /// <summary>
        /// Post data back
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<T> PostFromContentPostAsync<T>(string requestUri, T postObj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(postObj), Encoding.UTF8, "application/json"));

                //await client.PostAsync()

                //var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode(); //TODO: Handle non success HTTP codes more gracefully.

                return await response.Content.ReadAsAsync<T>();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="putObj"></param>
        /// <returns></returns>
        public async Task<T> PutFromContentPutAsync<T>(string requestUri, T putObj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceAppUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(putObj), Encoding.UTF8, "application/json"));
                
                response.EnsureSuccessStatusCode(); //TODO: Handle non success HTTP codes more gracefully.

                return await response.Content.ReadAsAsync<T>();

            }
        }
    
    }
}
