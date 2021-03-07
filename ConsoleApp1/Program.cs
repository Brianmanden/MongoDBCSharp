using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await InsertIntoCollection();

            Console.WriteLine("123");
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
    }
}
