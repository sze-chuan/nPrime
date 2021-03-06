﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace nPrimeApi.Models
{
    public class Event
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
