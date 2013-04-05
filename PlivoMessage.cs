using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace PlivoMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("XXXXXXXXXXXXXXXXXXXXXXX", "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");

            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "11212121211" },
                { "dst", "12212121211" },
                { "text", "Hi, text from Plivo." },
                { "url", "http://some.domain/receivestatus/" },
                { "method", "GET" }
            });
            if (resp.Data != null)
            {
                PropertyInfo[] proplist = resp.Data.GetType().GetProperties();
                foreach (PropertyInfo property in proplist)
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(resp.Data, null));
            }
            else
                Console.WriteLine(resp.ErrorMessage);
		}
	}
}	
