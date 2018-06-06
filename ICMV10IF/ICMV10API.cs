using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ICMV10IF
{
    class ICMV10API
    {
        private string token;
        private long initialTimeTick;
        private string mySiteURL;
        private string myModel;
        public ICMV10API(string siteURL, string model, string userName, string passWord)
        {
            initialTimeTick = DateTime.Now.Ticks;
            mySiteURL = siteURL;
            token = GetToken(userName, passWord);
            myModel = model;
        }

        public string GetToken(string icmuserName, string icmpassWord)
        {
            string apiURL = mySiteURL + "/services/login";
            RestClient myRestClient = new RestClient();
            myRestClient.BaseUrl = new Uri(apiURL);
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.Method = Method.POST;
            request.AddJsonBody(new { email = icmuserName, password = icmpassWord });
            var response = myRestClient.Execute(request);
            var detail = JObject.Parse(response.Content);
            return detail["token"].ToString();
        }

        public string GetCalculations()
        {
            string apiURL = mySiteURL + "/api/v1/calculations";
            RestClient myRestClient = new RestClient();
            myRestClient.BaseUrl = new Uri(apiURL);
            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Model", myModel);
            request.Method = Method.GET;
            var response = myRestClient.Execute(request);
            dynamic detail = JsonConvert.DeserializeObject(response.Content);
            foreach (var i in detail)
            {
                Console.WriteLine("CalculationID: {0}, name: {1}", i["calculationId"], i["name"]);
            }
            return response.Content;
        }

        public string GetDataFromCalc(string CalcID, string condition)
        {
            string apiURL = mySiteURL + "/api/v1/calculations/" + CalcID + "/data?filter=" + condition;
            RestClient myRestClient = new RestClient();
            myRestClient.BaseUrl = new Uri(apiURL);
            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Model", myModel);
            request.Method = Method.GET;
            var response = myRestClient.Execute(request);
            var detail = JObject.Parse(response.Content);
            return response.Content;
        }
    }
}
