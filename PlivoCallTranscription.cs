using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace PlivoAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            string auth_id = "<your_auth_id>";
            string auth_token = "<your_auth_token>";
            RestAPI plivo = new RestAPI(auth_id, auth_token);

			string call_uuid = # get the live call's uuid
			// Record a live call
            IRestResponse<Record> resp = plivo.record(new dict {
                { "call_uuid", call_uuid },
				{ "transcription_url", "http://some.server/url/" },
				{ "transcription_method", "GET" },
				{ "transcription_type", "auto" },
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