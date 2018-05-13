using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace nPrimeApi.Models
{
    public class Member
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Course { get; set; }
        public DateTime? SchoolJoinDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public List<EventGroup> EventGroups { get; set; }
    }

    public class GenderType
    {
        public static readonly string Male = "M";
        public static readonly string Female = "F";
    }
}
