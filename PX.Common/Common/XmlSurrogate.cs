// Decompiled with JetBrains decompiler
// Type: PX.Common.XmlSurrogate
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

#nullable disable
namespace PX.Common;

internal class XmlSurrogate : ISerializationSurrogate
{
  public void GetObjectData(object _param1, SerializationInfo _param2, StreamingContext _param3)
  {
    XmlSerializer xmlSerializer = new XmlSerializer(_param1.GetType());
    StringWriter stringWriter = new StringWriter();
    xmlSerializer.Serialize((TextWriter) stringWriter, _param1);
    stringWriter.Close();
    string s = stringWriter.ToString();
    if (PXSurrogateSelector.IsCheckEnabled)
    {
      object b = xmlSerializer.Deserialize((TextReader) new StringReader(s));
      try
      {
        new PXObjectComparer().Compare(_param1, b, (PXObjectComparer.StackFrame) null);
      }
      catch (Exception ex)
      {
        throw new Exception("XML serialization failed " + _param1.GetType().FullName, ex);
      }
    }
    _param2.AddValue("xml", (object) s);
  }

  public object SetObjectData(
    object _param1,
    SerializationInfo _param2,
    StreamingContext _param3,
    ISurrogateSelector _param4)
  {
    string s = _param2.GetString("xml");
    return new XmlSerializer(_param1.GetType()).Deserialize((TextReader) new StringReader(s));
  }
}
