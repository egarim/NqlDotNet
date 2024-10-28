using Newtonsoft.Json;

namespace NqlDotNet.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeserializeEntitiesJson()
        {
            // Read the Entities.json embedded resource
            string jsonContent = EmbeddedResourceHelper.ReadEmbeddedResource("Entities.json", "NqlDotNet.Tests.Data", this.GetType());

            // Deserialize into EntitiesWrapper class
            var entitiesWrapper = JsonConvert.DeserializeObject<EntitiesWrapper>(jsonContent);

            // Assertions to verify deserialization
            Assert.IsNotNull(entitiesWrapper, "EntitiesWrapper should not be null.");
            Assert.IsNotNull(entitiesWrapper.Entities, "Entities list should not be null.");
            Assert.IsNotEmpty(entitiesWrapper.Entities, "Entities list should not be empty.");

            // Additional assertions can be added here
        }

        [Test]
        public void DeserializeEntityPropertiesJson()
        {
            // Read the EntityProperties.json embedded resource
            string jsonContent = EmbeddedResourceHelper.ReadEmbeddedResource("EntityProperties.json", "NqlDotNet.Tests.Data", this.GetType());

            // Deserialize into EntityPropertiesWrapper class
            var entityPropertiesWrapper = JsonConvert.DeserializeObject<EntityPropertiesWrapper>(jsonContent);

            // Assertions to verify deserialization
            Assert.IsNotNull(entityPropertiesWrapper, "EntityPropertiesWrapper should not be null.");
            Assert.IsNotNull(entityPropertiesWrapper.EntityProperties, "EntityProperties list should not be null.");
            Assert.IsNotEmpty(entityPropertiesWrapper.EntityProperties, "EntityProperties list should not be empty.");

            // Additional assertions or operations can be added here
        }

        [Test]
        public async Task CreateCriteriaTest()
        {
            // Read the EntityProperties.json embedded resource
            string Doc = EmbeddedResourceHelper.ReadEmbeddedResource("DevExpressCriteriaSyntax.txt", "NqlDotNet.Tests.Data", this.GetType());
            string Schema = EmbeddedResourceHelper.ReadEmbeddedResource("EntityProperties.json", "NqlDotNet.Tests.Data", this.GetType());

            string Nlq = "Retrieve all invoices between the year 2000 and the current year for the customer named Joche that contains the products like hamburgers.";
            NqlService nqlService = new NqlService();
           var Criteria= await  nqlService.CreateCriteria(Nlq, Schema, Doc);
        }
    }
}