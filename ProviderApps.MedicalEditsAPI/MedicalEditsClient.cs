using Newtonsoft.Json;
using ProviderApps.Core.Schemas.MedicalEdits;
using ProviderApps.Core.Schemas.MedicalEdits.CustomExceptions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProviderApps.MedicalEditsAPI
{
    public class MedicalEditsClient : IMedicalEditsClient
    {
        private string _baseUrl = "";
        private HttpClient _httpClient;
        private Lazy<JsonSerializerSettings> _settings;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        public MedicalEditsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                return settings;
            });
        }

        /// <summary>
        ///  Medical Service API to Get Medical Edits 
        /// </summary>
        /// <param name="medicalEditsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ClaimEditResponse> GetClaimsEditsAsync(MedicalEditsRequestModel medicalEditsRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            var client_ = _httpClient;
            var urlBuilder_ = new StringBuilder();
            //set the url of medical edit service
            urlBuilder_.Append(client_?.BaseAddress != null ? client_.BaseAddress?.OriginalString?.TrimEnd('/') : "").Append("/getClaimsEdits");

            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(medicalEditsRequest, _settings.Value));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, UriKind.Absolute);
                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        var status_ = ((int)response_.StatusCode).ToString();
                        // check the status code returned from the response
                        // getClaimsEdits API return 200 status code, the sucess field returned in response indicates Whether the service call is successful or not 
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(ClaimEditResponse);
                            try
                            {
                                result_ = JsonConvert.DeserializeObject<ClaimEditResponse>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new MedicalEditsAPIException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);

                            }
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new MedicalEditsAPIException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }
    }
}
