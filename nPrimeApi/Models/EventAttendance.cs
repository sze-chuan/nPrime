using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace nPrimeApi.Models
{
    public class EventAttendance
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string ObjectId { get; set; }
        public string MemberId { get; set; }
        public string EventId { get; set; }
        public DateTime? EventDate { get; set; }
        public int? Status { get; set; }
        public int? NoOfMinutesLate { get; set; }
        public string Reason { get; set; }
    }

    public class EventAttendanceStatus
    {
        public const int Absent = 0;
        public const int Present = 1;
        public const int Late = 1;
    }
}
