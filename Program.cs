using System;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("test");
            var collection = database.GetCollection<TodoItem>("todo");
            ITodoListDao todoListDao = new MongoDriverDao(collection);
            readInput(todoListDao);
        }

        static void readInput(ITodoListDao todoListDao)
        {
            Console.WriteLine("Please type a command:");
            string currentCommand;
            while ((currentCommand = Console.ReadLine()) != null)
            {
                string[] args = currentCommand.Split();
                try
                {
                    switch (args[0])
                    {
                        case "create":
                            if (args.Length != 3)
                            {
                                Console.WriteLine("Usage: create <contents> <priority>");
                            }
                            else
                            {
                                var id = todoListDao.CreateNewItem(args[1], Convert.ToInt32(args[2].Trim()));
                                Console.WriteLine("Created an item with the following id: " + id);
                            }
                            break;
                        case "read":
                            if (args.Length != 1)
                            {
                                Console.WriteLine("Usage: read");
                            }
                            else
                            {
                                var allItems = todoListDao.ListAllItems();
                                foreach (var item in allItems)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case "update":
                            if (args.Length != 4)
                            {
                                Console.WriteLine("Usage: update <id> <contents> <priority>");
                            }
                            else
                            {
                                todoListDao.UpdateItemContent(ObjectId.Parse(args[1]), args[2]);
                                todoListDao.UpdateItemPriority(ObjectId.Parse(args[1]), Convert.ToInt32(args[3]));
                                Console.WriteLine("Update successful!");
                            }
                            break;
                        case "delete":
                            if (args.Length != 2)
                            {
                                Console.WriteLine("Usage: delete <id>");
                            }
                            else
                            {
                                todoListDao.RemoveItem(ObjectId.Parse(args[1]));
                                Console.WriteLine("Remove successful!");
                            }
                            break;
                        default:
                            Console.WriteLine("Try one of create, read, update or delete.");
                            break;
                    }
                }
                catch (MongoConnectionException)
                {
                    Console.WriteLine("No connection to the database.");
                }
            }
        }
    }
}
