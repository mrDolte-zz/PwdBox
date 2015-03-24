using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace PwdBox
{
   
    public class PropsFields
    {
        public String XMLFileName = "settings.xml";
        //public String TextValue = @"File Settings";
        //public Decimal DecimalValue = 555;
        public Boolean AuthorizedToStart = true;
        public Boolean WhatNewWindow = true;

    }
    public class Props
    {
        public PropsFields Fields;
        public Props()
        {
            Fields = new PropsFields();
        }
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        public void ReadXml()
        {
            if (File.Exists(Fields.XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(Fields.XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
        }
    }
}
