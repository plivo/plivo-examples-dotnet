using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.API;

namespace link_unlink_apps
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            // Link an application to a phone number    
            IRestResponse<GenericResponse> resp = plivo.link_application_number(new Dictionary<string,string>()
            {
                {"number","1111111111"}, // Number that has to be linked to an application
                {"app_id","16632742496743552"} // Application ID that has to be linked
            });

            Console.WriteLine(resp.Content);
            Debug.WriteLine(resp.Content);

            // Unlink an application from a phone number            
            IRestResponse<GenericResponse> response = plivo.unlink_application_number(new Dictionary<string, string>()
            {
                {"number","1111111111"} // Number that has to be unlikned to an application
            });

            Console.WriteLine(response.Content);
            Debug.WriteLine(response.Content);
            
            Console.ReadLine();
            
        }
    }
}

/*
Sample Output
Link an application to a phone number 
{
  "api_id": "4b48212c-b69b-11e4-ac1f-22000ac51de6",
  "message": "changed"
}

Unlink an application from a phone number   
{
  "api_id": "19247720-b69d-11e4-b423-22000ac8a2f8",
  "message": "changed"
}
*/