using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Plivo.Util;
using Plivo.API;
using Nancy;

namespace validateSignature
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/receive_sms/"] = x =>
            {
                IEnumerable<string> signature = Request.Headers["X-Plivo-Signature"];
                String[] sign = (String[])signature;
                String actualsignature = sign[0];

                String auth_token = "Your AUHT TOKEN";

                Dictionary<string,string> parameters = new Dictionary<string,string>();
                if (Request.Query != null)
                {
                    foreach (String key in Request.Query.Keys)
                    {
                        String value = Request.Query[key];
                        parameters.Add(key, value);
                    }
                }

                String url = Request.Url.SiteBase + Request.Url.Path;
                
                if (Request.Form != null)
                {
                    foreach (String key in Request.Form.Keys)
                    {
                        String value = Request.Form[key];
                        parameters.Add(key, value);
                    }
                }

                bool valid = XPlivoSignature.Verify(url, parameters, actualsignature, auth_token);
                Debug.WriteLine("Valid : " + valid);
                
                String from_number = Request.Query["From"];
                String to_number = Request.Query["To"];
                String text = Request.Query["Text"];

                Debug.WriteLine("From : {0}, To : {1}, Text : {2}", from_number, to_number, text);
                Console.ReadLine(); 
                return "OK";
            };
        }
    }
}