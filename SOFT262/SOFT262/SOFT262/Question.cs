using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SOFT262
{
    class Question
    {
        [JsonProperty(PropertyName = "QuestionID")]

        public int Number { get; set; }
        public string question { get; set; }
        public string answer { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Question(int Num, string ques, string ans) => (Number, question, answer) = (Num, ques, ans);
    }
}
