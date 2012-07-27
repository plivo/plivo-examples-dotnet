using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace PlivoAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            string auth_id = "XXXXXXXXXXXXXXXXXXXX";
            string auth_token = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";

            RestAPI plivo = new RestAPI(auth_id, auth_token);

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

            // Get all subaccounts
            IRestResponse<SubAccountList> response_all_subaccounts = plivo.get_subaccounts();
            string sub_account_id = "";
            if (response_all_subaccounts.Data != null)
            {
                if (response_all_subaccounts.Data.meta.total_count > 0)
                {
                    sub_account_id = response_all_subaccounts.Data.objects[0].auth_id;
                    foreach (SubAccount subacc in response_all_subaccounts.Data.objects)
                    {
                        PropertyInfo[] subaccprops = subacc.GetType().GetProperties();
                        foreach (PropertyInfo property in subaccprops)
                            Console.WriteLine("{0}: {1}", property.Name, property.GetValue(subacc, null));
                    }
                }
            }
            else
                Console.WriteLine(response_all_subaccounts.ErrorMessage);

            // Get a subaccount where subauth_id = sub_account_id
            if (!String.IsNullOrEmpty(sub_account_id))
            {
                IRestResponse<SubAccount> response_subaccount_info = plivo.get_subaccount(new Dictionary<string, string>() {
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

            // Delete subaccount
            IRestResponse<GenericResponse> response_delete_subaccount = plivo.delete_subaccount(new Dictionary<string, string>() {
                    { "subauth_id", sub_account_id }
                });
        }
    }
}
