using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InitialPaymentAMEX : System.Web.UI.Page
{
    private string responseData;
    public string ResponseData { get { return responseData; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        responseData = initialPayment()["result"]["description"];
    }

    public Dictionary<string, dynamic> initialPayment()
    {
        Dictionary<string, dynamic> responseData;
        var data = new NameValueCollection() {
		{"authentication.userId", "8a8294174b7ecb28014b9699220015cc"},
		{"authentication.password", "sy6KJsT8"},
		{"authentication.entityId", "8a8294174b7ecb28014b9699a3cf15d1"},
		{"amount", "92.12"},
		{"currency", "EUR"},
		{"paymentBrand", "KLARNA_INVOICE"},
		{"paymentType", "PA"},
		{"customer.givenName", "Jane"},
		{"customer.surname", "Jones"},
		{"billing.country", "SE"},
		{"cart.items[0].merchantItemId", "1"},
		{"cart.items[0].discount", "0.00"},
		{"cart.items[0].quantity", "5"},
		{"cart.items[0].name", "Product 1"},
		{"cart.items[0].price", "1.00"},
		{"cart.items[0].tax", "6.00"},
		{"shopperResultUrl", "https://docs.oppwa.com"},
	};
        using (var wc = new WebClient())
        {
            var rslt = wc.UploadValues("https://test.oppwa.com/v1/payments", data);
            var s = new JavaScriptSerializer();
            responseData = s.Deserialize<Dictionary<string, dynamic>>(Encoding.UTF8.GetString(rslt));
        }
        return responseData;
    } 
}