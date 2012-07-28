using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace PlivoNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string auth_id = "<your auth_id>";
            string auth_token = "<your auth_token>";

            RestAPI plivo = new RestAPI(auth_id, auth_token);

            Console.WriteLine("============================================");
            string varNumber = "";
            // Get all rented numbers from account 
            IRestResponse<NumberList> response_number_list = plivo.get_numbers();
            if (response_number_list.Data != null)
            {
                varNumber = response_number_list.Data.objects[0].number;
                foreach (Number number in response_number_list.Data.objects)
                    foreach (PropertyInfo attr in number.GetType().GetProperties())
                        Console.WriteLine("{0}: {1}", attr.Name, attr.GetValue(number, null));
            }
            else
                Console.WriteLine(response_number_list.ErrorMessage);
            Console.WriteLine("============================================");
            // Get details of a particular number
            IRestResponse<Number> response_number_info = plivo.get_number(new dict {
                { "number", varNumber }
            });
            if (response_number_info.Data != null)
            {
                foreach (PropertyInfo property in response_number_info.Data.GetType().GetProperties())
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(response_number_info.Data, null));
            }
            else
                Console.WriteLine(response_number_info.ErrorMessage);
            Console.WriteLine("============================================");

            // Search for numbers
            string rent_number = "";
            Console.WriteLine("============================================");
            IRestResponse<NumberList> response_search_list = plivo.search_numbers(new dict {
                { "country_code", "1" },
                { "prefix", "2*" },
                { "limit", "2" }
            });
            if (response_search_list.Data != null)
            {
                if (response_search_list.Data.meta.total_count > 0)
                {
                    rent_number = response_search_list.Data.objects[0].number;
                    foreach (Number number in response_search_list.Data.objects)
                        foreach (PropertyInfo property in number.GetType().GetProperties())
                        {
                            Console.WriteLine("{0}: {1}", property.Name, property.GetValue(number, null));
                            Console.WriteLine("--------------------------------------------------");
                        }
                }
                else
                    Console.WriteLine(response_search_list.Data.error);
            }
            else
                Console.WriteLine(response_search_list.ErrorMessage);
            Console.WriteLine("==================================================");

            //Rent number
            IRestResponse<GenericResponse> response_rent_number = plivo.rent_number(new dict {
                { "number", rent_number }
            });
            if (String.Equals(response_rent_number.Data.message, "created"))
                Console.WriteLine("Rented {0}", rent_number);
            Console.WriteLine("==================================================");
            
            // Unrent number
            IRestResponse<GenericResponse> response_unrent_number = plivo.unrent_number(new dict {
                { "number", rent_number },
            });
            if (response_unrent_number.Data != null)
                Console.WriteLine(response_unrent_number.Data.message);
            else
        }       Console.WriteLine(response_unrent_number.ErrorMessage);
    }
}
