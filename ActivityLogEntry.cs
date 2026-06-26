using System;

namespace PROGP3
{
    public class ActivityLogEntry
    {
        public DateTime TimeStamp { get; set; }

        public required string Action { get; set; }

        public required string Category { get; set; } // Task / Quiz / NLP / System

        public override string ToString()
        {
            return $"[{TimeStamp:HH:mm:ss}] ({Category}) {Action}";
        }
    }
}
