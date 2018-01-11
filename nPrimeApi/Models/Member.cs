using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace nPrimeApi.Models
{
    public class Member
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Course { get; set; }
        public DateTime? SchoolJoinDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IEnumerable<EventAttendance> EventAttendances { get; set; }
    }

    public class GenderType
    {
        public static readonly int Male = 0;
        public static readonly int Female = 1;
    }
}
