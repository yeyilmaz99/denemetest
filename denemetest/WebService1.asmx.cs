using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;

namespace denemetest
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public List<DonenBrandList> MakeHttpRequest()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://localhost:44345/api/Brands/getall";

                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                        List<DonenBrandList> list = result.Data;
                        return list;
                    }
                    else
                    {
                        List<DonenBrandList> list = new List<DonenBrandList>();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                List<DonenBrandList> list = new List<DonenBrandList>();
                return list;
            }
        }


        [WebMethod]
        public DonenBrandList MakeHttpRequestWithId(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:44345/api/Brands/getbyid?id={id}";
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<SingleResponse>(responseData);
                        DonenBrandList brand = result.Data;
                        return result.Data;

                    }
                    else
                    {
                        DonenBrandList result = null;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                DonenBrandList result = null;
                return result;
            }
        }

        [WebMethod]
        public string MakeHttpPostRequest()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    obj data =  new obj{ColorName="color1"};
                    string apiUrl = "https://localhost:44345/api/Colors/add";
                    var dataToSend = JsonConvert.SerializeObject(data);

                    var httpContent = new StringContent(dataToSend, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(apiUrl, httpContent).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = response.Content.ReadAsStringAsync().Result;
                        return responseData;
                    }
                    else
                    {
                        return "Error: " + response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }

    public class obj
    {
        public string ColorName;
    }

    public class DonenBrandList
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

    }

    public class SingleResponse
    {
        public DonenBrandList Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }


    public class ApiResponse
    {
        public List<DonenBrandList> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
