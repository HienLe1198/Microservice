using Catalog.Microservice.Data.Interface;
using Catalog.Microservice.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Microservice.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Item> Products { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Item>(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            CatalogContextSeed.SeedData(Products);
        }
    }
}
