using System;
using System.Linq;
using System.Collections.Generic;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace MongoDBTest
{
    class MongoDriverDao : ITodoListDao
    {
        private MongoCollection<TodoItem> itemsCollection;

        public MongoDriverDao(MongoCollection<TodoItem> itemsCollection)
        {
            this.itemsCollection = itemsCollection;
        }

        public ObjectId CreateNewItem(string pContent, int pPriority)
        {
            TodoItem itemToInsert = new TodoItem { content = pContent, priority = pPriority };
            this.itemsCollection.Insert(itemToInsert);
            return itemToInsert.Id;
        }

        public ObjectId[] ListAllItems()
        {
            var cursor = this.itemsCollection.FindAll();
            var result = new List<ObjectId>();
            foreach (var todoItem in cursor)
            {
                result.Add(todoItem.Id);
            }
            return result.ToArray();
        }

        public void RemoveItem(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItemContent(ObjectId id, string newContent)
        {
            throw new NotImplementedException();
        }

        public void UpdateItemPriority(ObjectId id, int newPriority)
        {
            throw new NotImplementedException();
        }
    }
}
