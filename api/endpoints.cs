using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace endpoints
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
            
            // Create an endpoint
            IRestResponse<Endpoint> resp = plivo.create_endpoint(new Dictionary<string,string>()
            {
              {"username", "testuser"}, // The username for the endpoint to be created
              {"password", "testt"}, // The password for your endpoint username
              {"alias", "Test"} // Alias for this endpoint
            });

            //Prints the response
            Console.Write(resp.Content);

            /*
            Sample Output
            {
              "alias": "Test",
              "api_id": "db41f044-b5cb-11e4-b423-22000ac8a2f8",
              "endpoint_id": "29147375335448",
              "message": "created",
              "username": "testuser150216110629"
            }
            */
             
            // Get details of all existing applications
            IRestResponse<EndpointList> res = plivo.get_endpoints(new Dictionary<string, string>()
            { 
                {"limit","2"}, // The number of results per page
                {"offset","0"} // The number of value items by which the results should be offset
            });

            //Prints the response
            Console.Write(res.Content);

            // Prints the total number of apps
            Console.WriteLine("Total count : " + res.Data.meta.total_count);

            /*
            {
              "api_id": "f09822ba-b5cb-11e4-ac1f-22000ac51de6",
              "meta": {
                "limit": 2,
                "next": "/v1/Account/XXXXXXXXXXXXXXXXX/Endpoint/?limit=2&offset=2",
                "offset": 0,
                "previous": null,
                "total_count": 3
              },
              "objects": [
                {
                  "alias": "Test",
                  "application": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/16982793927977910/",
                  "endpoint_id": "29147375335448",
                  "password": "147538da338b770b61e592afc92b1ee6",
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Endpoint/29147375335448/",
                  "sip_registered": "false",
                  "sip_uri": "sip:testuser150216110629@phone.plivo.com",
                  "sub_account": null,
                  "username": "testuser150216110629"
                },
                {
                  "alias": "TestSample",
                  "application": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/16632742496743552/",
                  "endpoint_id": "24753112937214",
                  "password": "147538da338b770b61e592afc92b1ee6",
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Endpoint/24753112937214/",
                  "sip_registered": "false",
                  "sip_uri": "sip:test150108095716@phone.plivo.com",
                  "sub_account": null,
                  "username": "test150108095716"
                }
              ]
            }
            Total count : 3
            */
                     
            // Get details of a single application
            IRestResponse<Endpoint> res1 = plivo.get_endpoint(new Dictionary<string, string>()
            {
                {"endpoint_id","29147375335448"} // ID of the endpoint for which the details have to be retrieved
            });

            //Prints the response
            Console.Write(res1.Content);

            /*
            Sample Output 
            {
              "alias": "Test",
              "api_id": "798973ee-b5cc-11e4-b423-22000ac8a2f8",
              "application": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/16982793927977910/",
              "endpoint_id": "29147375335448",
              "password": "147538da338b770b61e592afc92b1ee6",
              "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Endpoint/29147375335448/",
              "sip_registered": "false",
              "sip_uri": "sip:testuser150216110629@phone.plivo.com",
              "sub_account": null,
              "username": "testuser150216110629"
            }  
            */
            
            // Modify an application
            IRestResponse<GenericResponse> res2 = plivo.modify_endpoint(new Dictionary<string, string>()
            {
                {"endpoint_id","29147375335448"}, // ID of the endpoint that has to be modified
                {"alias","Testing"} // Values that have to be updated
            });

            //Prints the response
            Console.Write(res2.Content);

            /*
            Sample Output
            {
              "api_id": "97305b10-b5cc-11e4-b423-22000ac8a2f8",
              "message": "changed"
            }
            */
            
            // Delete an application
            IRestResponse<GenericResponse> res3 = plivo.delete_endpoint(new Dictionary<string,string>()
            {
              {"endpoint_id","29147375335448"}  // ID of the endpoint that as to be deleted
            });

            //Prints the response
            Console.Write(res3.Content);

            Console.ReadLine();
            /*
            Successful Output
            " "
    
            Unsuccessful output
            {
              "api_id": "be4512a4-b5cc-11e4-9107-22000afaaa90",
              "error": "not found"
            }
            */
        }
    }
}