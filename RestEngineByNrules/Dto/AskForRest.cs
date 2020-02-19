using System;
using System.Collections.Generic;

namespace RestEngineByNrules.Dto
{
    /// <summary>
    /// 請假單
    /// </summary>
    public class AskForRest
    {
        public Guid key { get; set; }
        public bool IsFinish
        {
            get => CheckingPerson == String.Empty;
        }

        public RequestStatus CheckingStatus { get; set; }

        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Proxy { get; set; }
        public string CheckingPerson
        {
            get => (CheckList.Count>0)? CheckList.Peek() : String.Empty;
        }

        public Queue<string> CheckList { get; set; }

        public AskForRest(string name, string proxy, DateTime startTime, DateTime endTime)
        {
            Name = name;
            Proxy = proxy;
            StartTime = startTime;
            EndTime = endTime;
            key = Guid.NewGuid();
            CheckingStatus = RequestStatus.Init;
            CheckList = new Queue<string>(GetFakeCheckList());
        }
        public IEnumerable<string> GetFakeCheckList()
        {
            yield return "遠松";
            yield return "純輝";
            yield return "志軒";
        }
    }
}
