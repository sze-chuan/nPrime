using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace nPrimeApi.Models
{
    public class EventAttendance
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Event Event { get; set; }
        public DateTime? EventDate { get; set; }
        public int? Status { get; set; }
        public int? LateInMinutes { get; set; }
        public string Reason { get; set; }
    }

    public class EventAttendanceStatus
    {
        public const int Absent = 0;
        public const int Present = 1;
        public const int Late = 1;
    }
}
