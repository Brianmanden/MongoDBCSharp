using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
//using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await InsertIntoCollection();

            await FindInCollection("overskrift");
        }

        public static async Task InsertIntoCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("testdatabase");

            dynamic person = new ExpandoObject();
            person.FirstName = "Jane";
            person.Age = 12;
            person.PetNames = new List<dynamic> { "Sherlock", "Watson" };
            database.GetCollection<dynamic>("testcollection").InsertOneAsync(person);

            dynamic person2 = new ExpandoObject();
            person2.FirstName = "Tarzan";
            person2.Weight = 125;
            person2.Related = new List<dynamic> { "Boy", "Jane" };
            person2.ComplexObject = new { 
                fieldset = new { 
                    headline = "overskrift 123",
                    RTE = new { 
                        image = "imageUrl.jpg",
                        text = "some text",
                        bodyText = "some bodytext"
                    }
                }
            };
            database.GetCollection<dynamic>("testcollection").InsertOneAsync(person2);

            await Task.Delay(250);
            return;
        }

        public static async Task FindInCollection(string searchString) {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("testdatabase");
            var collection = database.GetCollection<dynamic>("testcollection");
            FilterDefinition<dynamic> filter = Builders<dynamic>.Filter.Text(searchString.ToLower().Trim());
            // HERE !!!
            var res = database
                        .GetCollection<dynamic>("testcollection")
                        .AsQueryable()
                        .Where(x => filter.Inject());


            //FilterDefinition<dynamic> filter = Builders<dynamic>.Filter.Text(searchString.ToLower().Trim());
            //var filter = new FilterDefinitionBuilder<dynamic>
            //var cursor = await collection.FindAsync(filter);
            //var results = await cursor.ToListAsync();

            //var builder = Builders<dynamic>.Filter.Text(searchString.ToLower().Trim());
            //var filter = Builders<dynamic>.Filter.Text(searchString.ToLower().Trim());
            //var filter = builder.Regex("$**" , searchString);


            //{ { "$text" : { "$search" : "overskrift" } } }
            //IMongoQuery query = Query.Text(searchString);
            //var filter = Builders<dynamic>.Filter.Text(searchString.ToLower().Trim());

            //var thing = collection.Find(filter);

            //await collection
            //        .Find(filter)
            //        .ForEachAsync(
            //            document => Console.WriteLine(document)
            //        );


            //List<dynamic> find = database
            //                        .GetCollection<dynamic>("testcollection")
            //                        .Find((FilterDefinition<dynamic>)query)
            //                        .ToList();

            //var results = collection.Find(Builders<dynamic>.Filter.Text("$**", searchString.ToLower().Trim())).ToList();

            //foreach (var res in results)
            //{
            //    Console.WriteLine(res);
            //}

            await Task.Delay(250);
            return;
        }
    }
}
