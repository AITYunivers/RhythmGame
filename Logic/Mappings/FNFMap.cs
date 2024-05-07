using Newtonsoft.Json;

namespace RhythmGame.Logic.Mappings
{
    public class FNFMap
    {
        [JsonProperty("version")]
        public string Version = string.Empty;
        [JsonProperty("scrollSpeed")]
        public ScrollSpeedData ScrollSpeed = new ScrollSpeedData();
        [JsonProperty("events")]
        public Event[] Events = [];
        [JsonProperty("notes")]
        public NoteData Notes = new NoteData();
        [JsonProperty("generatedBy")]
        public string GeneratedBy = string.Empty;

        public class ScrollSpeedData
        {
            [JsonProperty("easy")]
            public float Easy = 1.0f;
            [JsonProperty("normal")]
            public float Normal = 1.0f;
            [JsonProperty("hard")]
            public float Hard = 1.0f;
        }

        public class NoteData
        {
            [JsonProperty("easy")]
            public Note[] Easy = [];
            [JsonProperty("normal")]
            public Note[] Normal = [];
            [JsonProperty("hard")]
            public Note[] Hard = [];
        }

        public class Note
        {
            [JsonProperty("t")]
            public int Time = 0;
            [JsonProperty("d")]
            public int Direction = 0;
            [JsonProperty("l")]
            public int? Length;
        }

        public class Event
        {
            [JsonProperty("e")]
            public string EventName = string.Empty;
            [JsonProperty("t")]
            public int Time = 0;
            [JsonProperty("v")]
            public object? EventData;
        }
    }
}
