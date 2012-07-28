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
            Console.WriteLine("==================================================");
            
            // Modify account name
            IRestResponse<GenericResponse> resp = plivo.modify_account(new dict {
                { "name", "user2" }
            });
            if (resp.Data != null)
            {
                Console.WriteLine(resp.Data.message);
                Console.WriteLine(resp.Data.error);
            }
            else
                Console.WriteLine(resp.ErrorMessage);
            Console.WriteLine("==================================================");
            
            // Get account details
            IRestResponse<Account> response_account_details = plivo.get_account();
            if (response_account_details.Data != null)
            {
                PropertyInfo[] properties = response_account_details.Data.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(response_account_details.Data, null));
            }
            else
                Console.WriteLine(response_account_details.ErrorMessage);
            Console.WriteLine("==================================================");
            
            //Create subaccount
            IRestResponse<GenericResponse> resp2 = plivo.create_subaccount(new dict {
                { "name", "sub1"}
            });
            Console.WriteLine(resp2.StatusCode);
            Console.WriteLine(resp2.StatusDescription);
            Console.WriteLine("==================================================");
            
            // Get all subaccounts
            IRestResponse<SubAccountList> response_all_subaccounts = plivo.get_subaccounts();
            string sub_account_id = "";
            if (response_all_subaccounts.Data != null)
            {
                if (response_all_subaccounts.Data.meta.total_count > 0)
                {
                    foreach (SubAccount subacc in response_all_subaccounts.Data.objects)
                    {
                        foreach (PropertyInfo property in subacc.GetType().GetProperties())
                        {
                            Console.WriteLine("{0}: {1}", property.Name, property.GetValue(subacc, null));
                            if (String.Equals(property.Name, "name") && String.Equals(property.GetValue(subacc, null), "sub1"))
                            {
                                sub_account_id = subacc.auth_id;
                                break;
                            }
                        }
                    }
                }
            }
            else
                Console.WriteLine(response_all_subaccounts.ErrorMessage);
            Console.WriteLine("==================================================");
            
            // Get a subaccount where subauth_id = sub_account_id
            if (!String.IsNullOrEmpty(sub_account_id))
            {
                IRestResponse<SubAccount> response_subaccount_info = plivo.get_subaccount(new dict {
                    { "subauth_id", sub_account_id }
                });

                if (response_subaccount_info.Data != null)
                {
                    PropertyInfo[] subaccdetails = response_subaccount_info.Data.GetType().GetProperties();
                    foreach (PropertyInfo property in subaccdetails)
                        Console.WriteLine("{0}: {1}", property.Name, property.GetValue(response_subaccount_info.Data, null));
                }
                else
                    Console.WriteLine(response_subaccount_info.ErrorMessage);
            }
            else
                Console.WriteLine("Could not capture the sub account id from the list.");
            Console.WriteLine("==================================================");
            
            // Modify subaccount
            IRestResponse<GenericResponse> resp1 = plivo.modify_subaccount(new dict {
                { "subauth_id", sub_account_id },
                { "name", "sub2" }
            });
            if (resp1.Data != null)
                Console.WriteLine(resp1.Data.message);
            else
                Console.WriteLine(resp1.ErrorMessage);
            Console.WriteLine("==================================================");

            // Delete subaccount
            IRestResponse<GenericResponse> response_delete_subaccount = plivo.delete_subaccount(new dict {
                    { "subauth_id", sub_account_id }
                });
            if ( response_delete_subaccount.Data != null )
                Console.WriteLine(response_delete_subaccount.Data.message);
            else
                Console.WriteLine(response_delete_subaccount.ErrorMessage);
            Console.WriteLine("==================================================");
        }
    }
}
