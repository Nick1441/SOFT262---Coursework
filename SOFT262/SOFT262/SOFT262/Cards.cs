using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SOFT262
{
    public class Cards
    {
        public string Subject { get; set; }
        public bool Card { get; set; } = false;
        public string Question { get; set; } = "N/A";
        public string Answer { get; set; } = "N/A";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        [JsonConstructor]
        public Cards(string sub, bool car, string que, string ans) => (Subject, Card, Question, Answer) = (sub, car, que, ans);
        public Cards(string sub) => (Subject) = (sub);
    }
}
