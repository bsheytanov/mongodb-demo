using System;

using MongoDB.Bson;

namespace MongoDBTest
{
    class TodoItem
    {
        public ObjectId Id { get; set; }
        public string content { get; set; }
        public int priority { get; set; }
    }
}
