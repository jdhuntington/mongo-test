using MongoDB.Driver;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;

namespace MongoInClustorWithFailover.Controllers
{
    public class DocumentsController : ApiController
    {
        private IMongoDatabase _database;
        private IMongoCollection<Document> _documents;

        public DocumentsController()
        {
            string connection = string.Empty;
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://localhost:27017";
            }
            var client = new MongoClient(connection);
            _database = client.GetDatabase("Documents");
            _documents = _database.GetCollection<Document>("documents");
        }
     
        public async Task<Document> Get(string id)
        {
            var document = await _documents.Find(item => item.Key == id).FirstAsync();

            if (document == null)
            {
               throw new HttpResponseException(HttpStatusCode.NotFound);
            }
     
            return document;
        }
     
        public async Task<Document> Post(Document document)
        {
            document.Key = ObjectId.GenerateNewId().ToString();
            await _documents.InsertOneAsync(document);

            return document;
        }
     
       // public void Put(string id, Document document)
       // {
       //     document.LastModified = DateTime.UtcNow;
       //     var update = new MongoDB.Driver.UpdateDefinition<Document>(;
       //         .Set("Value", document.Value);
       //     var result = _documents.UpdateOneAsync<Document>(item => item.Key == id, update);

       //     if (!result)
       //     {
       //         throw new HttpResponseException(HttpStatusCode.NotFound);
       //     }
       //}
     
        //public void Delete(string id)
        //{
        //    if (!_contacts.RemoveContact(id))
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //}

    }
}