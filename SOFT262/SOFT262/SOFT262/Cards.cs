using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SOFT262
{
    class Cards
    {
        public string Subject { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Cards(string sub, string que, string ans) => (Subject, Question, Answer) = (sub, que, ans);
    }
}
