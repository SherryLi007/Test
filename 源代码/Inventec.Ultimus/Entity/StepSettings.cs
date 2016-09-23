using System;
using System.Collections.Generic;
using System.Text;

namespace Inventec.Ultimus.Entity
{
    public class StepSetting
    {
        public string Process { get; set; }
        public string StepName { get; set; }
        public string PageName { get; set; }
    }

    public class Approver
    {
        public string STEPLABEL { get; set; }
        public string ASSIGNEDTOUSER { get; set; }
    }
}
