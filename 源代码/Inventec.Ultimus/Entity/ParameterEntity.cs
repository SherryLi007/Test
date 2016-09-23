using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventec.Ultimus.Entity
{
    public class ParameterEntity
    {
        public ParameterEntity()
        {
        }

        public ParameterEntity(string name,string value)
        {
            _Name = name;
            _Value = value;
        }


        string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}