using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Plivo;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Plivo.XML.Response resp = new Plivo.XML.Response();
        Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() { });
        if (Request.HttpMethod == "GET")
        {
            int i = 0;
            for(;i<Request.QueryString.Count;i++)
            {
                if (Request.QueryString.GetKey(i) == "Numbers")
                    dial.AddNumber(Request.QueryString.Get(i), new Dict
ionary<string, string>());
                else if (Request.QueryString.GetKey(i) == "Users")
                    dial.AddUser(Request.QueryString.Get(i), new Dictionary<string, string>());                    
            }
            resp.Add(dial);
        }
        else if(Request.HttpMethod == "POST")
        {
            int i = 0;
            for (; i < Request.Form.Count; i++)
            {
                if (Request.Form.GetKey(i) == "Numbers")
                    dial.AddNumber(Request.Form.Get(i), new Dictionary<string, string>());
                else if (Request.Form.GetKey(i) == "Users")
                    dial.AddUser(Request.Form.Get(i), new Dictionary<string, string>());
            }
            resp.Add(dial);
        }

        Response.Write(resp.ToString());
        Response.ContentType = "text/xml";
		Response.End();
    }
}
