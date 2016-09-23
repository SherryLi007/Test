using System;
using System.Collections.Generic;
using System.Text;

namespace Inventec.Ultimus.Entity
{
    [Serializable]
    public class VarEntity
    {
        string _name;

        public string Name
        {
            get { return _name == null ? "" : _name.Trim(); }
            set { _name = value; }
        }

        string _value;
        /// <summary>
        /// 如果为数组，那么以"|"分隔
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        bool nillable;

        public bool Nillable
        {
            get { return nillable; }
            set { nillable = value; }
        }

        string description;

        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(description))
                {
                    description = Name;
                }
                return description;
            }
            set { description = value; }
        }

        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        string complexType;

        public string ComplexType
        {
            get { return complexType; }
            set { complexType = value; }
        }

        string _ParentName;

        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
    }
}