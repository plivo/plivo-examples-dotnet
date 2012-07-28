using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace PlivoEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            string auth_id = "XXXXXXXXXXXXX";
            string auth_token = "YYYYYYYYYYYYYYYYYYYYYYYY";
            
            RestAPI plivo = new RestAPI(auth_id, auth_token);
            
            Console.WriteLine("==========================================");
            // Create an endpoint
            IRestResponse<GenericResponse> resp1 = plivo.create_endpoint(new dict {
                { "username", "user1234" },
                { "password", "password" },
                { "alias", "phone1" }
            });
            if (resp1.Data != null)
                Console.WriteLine(resp1.Data.message);
            else
                Console.WriteLine(resp1.ErrorMessage);
            Console.WriteLine("==========================================");
            // Get all endpoints
            string endpoint_id = "";
            // you can pass parameter, such as, limit, offset filter search.
            IRestResponse<EndpointList> resp2 = plivo.get_endpoints(new dict {
                { "", "" }
            });
            if (resp2.Data != null)
            {
                if (resp2.Data.meta.total_count > 0)
                {
                    endpoint_id = resp2.Data.objects[0].endpoint_id;
                    foreach (Endpoint endpoint in resp2.Data.objects)
                        foreach (PropertyInfo property in endpoint.GetType().GetProperties())
                            Console.WriteLine("{0}: {1}", property.Name, property.GetValue(endpoint, null));
                }
            }
            else
                Console.WriteLine(resp2.ErrorMessage);
            Console.WriteLine("==========================================");
            // Get details of a particular endpoint
            IRestResponse<Endpoint> resp3 = plivo.get_endpoint(new dict {
                { "endpoint_id", endpoint_id }
            });
            if (resp3.Data != null)
                foreach (PropertyInfo property in resp3.Data.GetType().GetProperties())
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(resp3.Data, null));
            else
                Console.WriteLine(resp3.ErrorMessage);
            Console.WriteLine("==========================================");
            // Modify username/password of an endpoint
            IRestResponse<GenericResponse> resp4 = plivo.modify_endpoint(new dict {
                { "endpoint_id", endpoint_id },
                { "username", "plivouser" },
                { "password", "password" }
            });
            if (resp4.Data != null)
                Console.WriteLine(resp4.Data.message);
            else
                Console.WriteLine(resp4.ErrorMessage);
            Console.WriteLine("==========================================");
            Console.WriteLine("End.."); Console.Read();
 		}
    }
}