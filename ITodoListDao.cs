using System;

using MongoDB.Bson;

namespace MongoDBTest
{
    interface ITodoListDao
    {
        ObjectId CreateNewItem(string content, int priority);

        ObjectId[] ListAllItems();

        void UpdateItemContent(ObjectId id, string newContent);

        void UpdateItemPriority(ObjectId id, int newPriority);

        void RemoveItem(ObjectId id);
    }
}
