using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class TruckCheckListData
    {
        public int Id { get; set; }
        public int CheckListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public List<Questions> LstQuestion { get; set; }

        public TruckCheckListData()
        {
            LstQuestion = new List<Questions>();
        }

    }
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public Nullable<bool> MustToDo { get; set; }
        public string InputeType { get; set; }
        public string InputeTypeDesc { get; set; }
        public int AnswerCount { get; set; }
        public List<Answers> LstAnswer { get; set; }

        public Questions()
        {
            LstAnswer = new List<Answers>();
        }
    }
    public class Answers
    {
        public int Id { get; set; }
        public string Answer { get; set; }
    }
}
