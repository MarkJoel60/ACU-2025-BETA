// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXFileStatusWriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

[Serializable]
internal class PXFileStatusWriter
{
  private readonly string _filename;

  public PXFileStatusWriter()
  {
    this._filename = Path.Combine(PXInstanceHelper.AppDataFolder, "UpdateStatus.xml");
  }

  public void Write(string status, string message)
  {
    if (string.IsNullOrEmpty(status))
      status = "None";
    using (StreamWriter streamWriter = new StreamWriter((Stream) new FileStream(this._filename, FileMode.Create, FileAccess.ReadWrite)))
    {
      streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
      streamWriter.WriteLine("<Content status=\"{0}\" message=\"{1}\" />", (object) status, (object) (message ?? string.Empty));
    }
  }

  public static bool WasUpdated()
  {
    bool flag = false;
    string str = Path.Combine(PXInstanceHelper.AppDataFolder, "UpdateStatus.xml");
    FileInfo fileInfo = new FileInfo(str);
    if (!fileInfo.Exists || fileInfo.Length < 10L || string.IsNullOrWhiteSpace(File.ReadAllText(str)))
    {
      flag = true;
      new PXFileStatusWriter().Write((string) null, (string) null);
    }
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    xmlDocument.Load(str);
    XmlAttribute attribute = xmlDocument.DocumentElement.Attributes["viewed"];
    if (attribute == null)
    {
      flag = true;
      xmlDocument.DocumentElement.Attributes.Append(attribute = xmlDocument.CreateAttribute("viewed"));
    }
    if (string.IsNullOrWhiteSpace(attribute.Value))
    {
      flag = true;
      attribute.Value = System.DateTime.UtcNow.ToString();
    }
    if (flag)
      xmlDocument.Save(str);
    return flag;
  }

  public static bool GetUpdateStatus()
  {
    string path = Path.Combine(PXInstanceHelper.AppDataFolder, "UpdateStatus.xml");
    if (!File.Exists(path))
      return true;
    string xml = File.ReadAllText(path);
    if (string.IsNullOrWhiteSpace(xml))
      return true;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.LoadXml(xml);
    XmlAttribute attribute = xmlDocument.DocumentElement.Attributes["status"];
    return attribute == null || attribute.Value != "Failed";
  }

  public static void ClearUpdateStatus()
  {
    string str = Path.Combine(PXInstanceHelper.AppDataFolder, "UpdateStatus.xml");
    if (!File.Exists(str))
      return;
    string xml = File.ReadAllText(str);
    if (string.IsNullOrWhiteSpace(xml))
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.LoadXml(xml);
    XmlAttribute attribute = xmlDocument.DocumentElement.Attributes["status"];
    if (attribute != null)
      attribute.Value = "Cleared";
    xmlDocument.Save(str);
  }
}
