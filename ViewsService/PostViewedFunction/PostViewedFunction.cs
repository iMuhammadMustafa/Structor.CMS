using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PostViewedFunction;

public class PostViewedFunction
{

    private const string QUEUE_NAME = "myqueue";
    private const string CONNECTION_NAME = "RabbitMQ";

    private const string COSMOS_CONNECTION = "COSMOS_CONNECTION";




    [FunctionName(nameof(PostViewedFunction))]
    public static async Task Run(
        [RabbitMQTrigger(QUEUE_NAME, ConnectionStringSetting = CONNECTION_NAME)] string post)
    //        [CosmosDB(
    //    databaseName:  "ToDoList",
    //    collectionName: "Items",
    //    ConnectionStringSetting = "COSMOS_CONNECTION",
    //CreateIfNotExists = true)] out dynamic document
    //)

    {

        string accountKey = "";
        string encodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(accountKey));


        var client = new CosmosClient($"AccountEndpoint=https://viewsdb.documents.azure.com:443/‌​;AccountKey={encodedKey};");
        var db = client.GetDatabase("ToDoList");
        var collection = db.GetContainer("Items");



        var document = new
        {
            id = "123",
            postId = 1, // Assuming postId is always 1 based on your original code
        };

        var x = await collection.CreateItemAsync(document);

    }


}
public record Post(string id, Guid Guid, int? UserId, string? IpAddress);

//public void SendToCosmos(Post inputPost, Post outputPost)
//{



//}
//[CosmosDBOutput("ToDoList", "Items", Connection = COSMOS_CONNECTION, CreateIfNotExists = true)]
//[CosmosDBOutput(
//    "ToDoList",
//    "Items",
//Connection = "COSMOS_CONNECTION",
//CreateIfNotExists = true)]
//[return: CosmosDB(
//        "ToDoList",
//        "Items",
//    ConnectionStringSetting = "COSMOS_CONNECTION",
//    CreateIfNotExists = true)]
