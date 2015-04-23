using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InitialPaymentAMEX : System.Web.UI.Page
{
    private string result;
    public string ResponseData { get { return result; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        getPaymentStatus("8a8294494ce19bdf014ce509f20b13e7");
    }

    private void getPaymentStatus(string paymentId)
    {
        string url = "https://test.oppwa.com/v1/payments/" + paymentId +
            "?authentication.userId=8a8294174b7ecb28014b9699220015cc" +
            "&authentication.password=sy6KJsT8" +
            "&authentication.entityId=8a8294174b7ecb28014b9699a3cf15d1";

        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Method = "GET";
        string response = String.Empty;
        using (HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse())
        {
            Stream dataStream = webresponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            response = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
        }

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        Dictionary<string, dynamic> responseJson = serializer.Deserialize<Dictionary<string, dynamic>>(response);

        if (responseJson["result"]["code"].StartsWith("000"))
        {
            result = "SUCCESS <br/><br/> Here is the result of your transaction: <br/><br/>";
            result += response;
        }
        else
        {
            result = "ERROR &lt;br/>&lt;br/> Here is the result of your transaction: &lt;br/>&lt;br/>";
            result += response;
        }
    }
}