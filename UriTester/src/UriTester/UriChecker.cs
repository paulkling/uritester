using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;

namespace UriTester
{
    public class UriChecker
    {
        private static String MetricsDotNetSearch = "\"IsHealthy\":true,";

        public static async Task<UriCheckerResponse> CheckSite(String uri)
        {
            return await CheckSite(uri, false, null);
        }

        public static async Task<UriCheckerResponse> CheckSite(String uri, String parseOutput = null)
        {
            return await CheckSite(uri, false, parseOutput);
        }
        public static async Task<UriCheckerResponse> CheckSite(String uri, bool MetricsDotNet=false, String parseOutput = null)
        {
            //should probablly move all this converstion stuff into the read section to fix any uri's
            String uriToCheck = uri;
            if (!(uri.ToLower().StartsWith("http://") || (uri.ToLower().StartsWith("https://"))))
            {
                uriToCheck = "http://" + uri;
            }

            if (MetricsDotNet)
            {
                if (!(uriToCheck.EndsWith("health") || uriToCheck.EndsWith("health/")))
                {
                    if (!uriToCheck.EndsWith("/"))
                        uriToCheck = uriToCheck + "/";
                    uriToCheck = uriToCheck + "health";
                }
            }
            UriCheckerResponse returnValue = new UriCheckerResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriToCheck);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(uriToCheck);
                if (MetricsDotNet)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    returnValue = UriChecker.parseOutput(MetricsDotNetSearch, uriToCheck, responseString);
                    if (returnValue.Status != Server.Status.Ok)
                    {
                        var message = responseString.Replace("\\r\\n", String.Empty);
                        returnValue.Message = "Metrics.Net endpoint Uri: " + uriToCheck + "' return \r\n'" + message;
                    }
                }
                else if ((parseOutput != null) && (parseOutput.Length > 0))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    returnValue = UriChecker.parseOutput(parseOutput, uriToCheck, responseString);

                }
                //Just checking 200 
                else if (response.IsSuccessStatusCode)
                {
                    returnValue.Status = Server.Status.Ok;
                    returnValue.Message = "";
                }
                else
                {
                    //ohh ohh error
                    var code = response.StatusCode;
                    returnValue.Status = Server.Status.Error;
                    returnValue.Message = "Error server returned: " + code;
                } 
            }
            return returnValue;
        }

        private static UriCheckerResponse parseOutput(string parseOutput, string uriToCheck, string responseString)
        {
            UriCheckerResponse returnValue = new UriCheckerResponse();
            if (responseString.Contains(parseOutput))
            {
                //success
                returnValue.Status = Server.Status.Ok;
                returnValue.Message = "";
            }
            else
            {
                // error text not found 
                returnValue.Status = Server.Status.Error;
                returnValue.Message = "Text: '" + parseOutput + "' not found in output from URI: '" + uriToCheck + "'";
            }
            return returnValue;
        }

    }
}
