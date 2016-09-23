using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Inventec.Ultimus.Entity;

namespace Ultimus.UWF.Workflow.Logic
{
    public class TypeUtil
    {
        public static Hashtable GetHashtable(List<ParameterEntity> paras)
        {
            Hashtable table = new Hashtable();
            foreach (ParameterEntity para in paras)
            {
                table.Add(para.Name, para.Value);
            }

            return table;
        }

        public static List<ParameterEntity> GetParameterList(Hashtable table)
        {
            List<ParameterEntity> list = new List<ParameterEntity>();
            foreach (DictionaryEntry ety in table)
            {
                ParameterEntity p = new ParameterEntity();
                p.Name =Convert.ToString( ety.Key);
                p.Value =Convert.ToString(ety.Value);
                list.Add(p);
            }
            return list;
        }


    }
}