using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventec.Ultimus.Entity
{
    public class ServerEntity
    {
        string _SERVERNAME;

        public string SERVERNAME
        {
            get { return _SERVERNAME; }
            set { _SERVERNAME = value; }
        }
        string _DESCRIPTION;

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        string _PRODUCT;

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        string _VERSION;

        public string VERSION
        {
            get { return _VERSION; }
            set { _VERSION = value; }
        }
        string _WEBSERVICEURL;

        public string WEBSERVICEURL
        {
            get { return _WEBSERVICEURL; }
            set { _WEBSERVICEURL = value; }
        }
        string _CLIENTASSEMBLY;

        public string CLIENTASSEMBLY
        {
            get { return _CLIENTASSEMBLY; }
            set { _CLIENTASSEMBLY = value; }
        }
        string _DOMAINNAME;

        public string DOMAINNAME
        {
            get { return _DOMAINNAME; }
            set { _DOMAINNAME = value; }
        }

        string _DBNAME;

        public string DBNAME
        {
            get { return _DBNAME; }
            set { _DBNAME = value; }
        }
    }
}