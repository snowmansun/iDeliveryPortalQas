using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Model
{
    public class SurveyModel
    {
        public string QuestionID { get; set; }

        public string QuestionName { get; set; }

        public string CallQuesID { get; set; }

        public string AnswerType { get; set; }

        public string AnswerTypeDesc { get; set; }

        public int PhotoCount { get; set; }

        public List<AnswerModel> Answers { get; set; }

        public SurveyModel()
        {
            Answers = new List<AnswerModel>();
        }
    }

    public class AnswerModel
    {
        public string AnswerID { get; set; }

        public string AnswerDesc { get; set; }

        public bool IsSelected { get; set; }
    }
}