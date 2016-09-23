using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using Inventec.Ultimus.Entity;
using System.Data;
using Inventec.Models;

namespace Ultimus.UWF.Workflow.Logic
{
    public class StepSettingsLogic
    {
        static List<StepSetting> _stepSettings = new List<StepSetting>();

        static StepSettingsLogic()
        {
            string section = ConfigurationManager.GetSection("stepSettings").ToString();
            XmlDocument node = new XmlDocument();
            node.LoadXml(section);
            if (node != null && node.ChildNodes.Count > 0 && node.ChildNodes[0].ChildNodes.Count > 0)
            {
                foreach (XmlNode xn in node.ChildNodes[0].ChildNodes)
                {
                    if (xn.Attributes != null)
                    {
                        StepSetting ss = new StepSetting();
                        foreach (XmlAttribute xa in xn.Attributes)
                        {
                            if (xa.Name == "Process")
                                ss.Process = xa.Value;
                            if (xa.Name == "StepName")
                                ss.StepName = xa.Value;
                            else if (xa.Name == "PageName")
                                ss.PageName = xa.Value;
                        }
                        _stepSettings.Add(ss);
                    }
                }
            }
            else
            {
                try
                {
                    using (InventecbizContext db = new InventecbizContext())
                    {
                        foreach (WfProcessstep step in db.WfProcessstep)
                        {
                            StepSetting ss = new StepSetting();

                            ss.Process = step.processname;
                            ss.StepName = step.stepname;
                            ss.PageName = step.pcform;

                            _stepSettings.Add(ss);
                        }

                        foreach (WfProcess process in db.WfProcess)
                        {
                            StepSetting ss = new StepSetting();

                            ss.Process = process.processname;

                            ss.PageName = process.defaultpcform;

                            ss.StepName = "__ALL";

                            _stepSettings.Add(ss);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        public static string GetStepPage(string processName, string stepName)
        {
            StepSetting ss = _stepSettings.Find(delegate(StepSetting stepSetting)
            {
                if (stepSetting.Process.ToUpper().Trim() == processName.ToUpper().Trim() && stepSetting.StepName.ToUpper().Trim() == stepName.ToUpper().Trim())
                {
                    return true;
                }
                if (stepSetting.Process.ToUpper().Trim() == processName.ToUpper().Trim() && stepSetting.StepName == "__ALL")
                {
                    return true;
                }
                return false;
            });
            if (ss != null)
            {
                return ss.PageName;
            }
            return null;
        }

        public static string GetDraftPage(string processName)
        {
            StepSetting ss = _stepSettings.Find(delegate(StepSetting stepSetting)
            {
                if (stepSetting.Process.ToUpper().Trim() == processName.ToUpper().Trim())
                {
                    return true;
                }
                return false;
            });
            if (ss != null)
            {
                return ss.PageName;
            }
            return null;
        }

        public static string GetViewPage(string processName)
        {
            StepSetting ss = _stepSettings.Find(delegate(StepSetting stepSetting)
            {
                if (stepSetting.Process.ToUpper().Trim() == processName.ToUpper().Trim())
                {
                    return true;
                }
                return false;
            });
            if (ss != null)
            {
                return ss.PageName;
            }
            return null;
        }

        public static string GetFirstStep(string processName)
        {
            StepSetting ss = _stepSettings.Find(delegate(StepSetting stepSetting)
            {
                if (stepSetting.Process.ToUpper().Trim() == processName.ToUpper().Trim())
                {
                    return true;
                }
                return false;
            });
            if (ss != null)
            {
                return ss.StepName;
            }
            return null;
        }
    }


}
