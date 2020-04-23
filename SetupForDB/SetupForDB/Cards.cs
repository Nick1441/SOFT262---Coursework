using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SetupForDB
{
    class Cards
    {
        public string Subject { get; set; } = "None Set";
        public string Question { get; set; }
        public string Answer { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Cards(string sub, string que, string ans) => (Subject, Question, Answer) = (sub, que, ans);
    }
}
