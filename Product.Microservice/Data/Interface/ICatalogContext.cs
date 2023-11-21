using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Microservice.Entities;

namespace Catalog.Microservice.Data.Interface
{
    public interface ICatalogContext
    {
        IMongoCollection<Item> Products { get; }
    }
}
