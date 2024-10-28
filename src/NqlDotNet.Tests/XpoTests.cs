using DevExpress.Utils;
using Newtonsoft.Json;
using NqlDotNet.DevExpress;

namespace NqlDotNet.Tests
{
    public class XpoTests
    {
        [SetUp]
        public void Setup()
        {
        }

  

        [Test]
        public async Task NlToCriteria()
        {

            // Read the EntityProperties.json embedded resource
            string Doc = EmbeddedResourceHelper.ReadEmbeddedResource("DevExpressCriteriaSyntax.txt", "NqlDotNet.Tests.Data", this.GetType());
            var props = XpoUtilities.GetEntityProperties(this.GetType().Assembly);
            string Schema =JsonConvert.SerializeObject(props);

            string Nlq = "Retrieve all invoices between the year 2000 and the current year for the customer named Joche that contains the products like hamburgers.";
            DevExNqlService nqlService = new DevExNqlService();
           var Criteria= await  nqlService.NlToCriteria(Nlq, Schema, Doc);
        }
        [Test]
        public async Task CriteriaToNl()
        {
            // Read the EntityProperties.json embedded resource
            string Doc = EmbeddedResourceHelper.ReadEmbeddedResource("DevExpressCriteriaSyntax.txt", "NqlDotNet.Tests.Data", this.GetType());
            var props = XpoUtilities.GetEntityProperties(this.GetType().Assembly);
            string Schema = JsonConvert.SerializeObject(props);
            string Nlq = "Retrieve all invoices between the year 2000 and the current year for the customer named Joche that contains the products like hamburgers.";
            string CriteriaText = "[InvoiceHeader][invoiceDate] Between (#2000-01-01#, Now()) And [InvoiceHeader][customerID] In ([Customer][name] == 'Joche') And [InvoiceDetails][productID] In ([Product][name] like '%hamburgers%')";
            DevExNqlService nqlService = new DevExNqlService();
            var Nl = await nqlService.CriteriaToNl(CriteriaText, Schema, Doc);

        }
    }
}