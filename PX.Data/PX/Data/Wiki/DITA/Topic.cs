// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.Topic
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class Topic
{
  private readonly List<PXDitaElement> _elements;
  private string _filename;
  private readonly string _titlename;
  private System.DateTime _datetime;
  private Guid _topicId;
  private const string DitaTagName = "topic";
  private const string DitaTitleTagName = "title";
  private const string DitaBodyTagName = "body";
  private const string DitaDescTagName = "shortdesc";
  private const string DitaSupName = "sup";

  public Topic(Guid id, string filename, string titlename, System.DateTime datetime)
  {
    this._elements = new List<PXDitaElement>();
    this._datetime = new System.DateTime();
    this._datetime = datetime;
    this._topicId = id;
    this._filename = new string(filename.ToCharArray());
    this._titlename = new string(titlename.ToCharArray());
  }

  public Topic()
  {
    this._elements = new List<PXDitaElement>();
    this._datetime = new System.DateTime();
    this._topicId = Guid.NewGuid();
    this._titlename = "";
    this._filename = "";
  }

  public void AddElement(PXDitaElement pxDitaElement) => this._elements.Add(pxDitaElement);

  public IEnumerable<PXDitaElement> GetElements() => (IEnumerable<PXDitaElement>) this._elements;

  public string FileName
  {
    get => this._filename;
    set => this._filename = value;
  }

  public string Title => this._titlename;

  public void DataTime(System.DateTime dateTime) => this._datetime = dateTime;

  public Guid TopicId => this._topicId;

  internal void Write(Stream stream, FileManager fileManager)
  {
    XmlTextWriter stream1 = new XmlTextWriter(stream, (Encoding) new UnicodeEncoding());
    string dtd = fileManager.GetDtd(this, "\\technicalContent\\dtd\\topic.dtd");
    stream1.WriteProcessingInstruction("dita", "xml version=\"1.0\" encoding=\"Unicode\"");
    stream1.WriteRaw("<!-- This document was created with Acumatica.Parser. -->");
    stream1.WriteRaw($"<!DOCTYPE topic SYSTEM \"{dtd}\">");
    stream1.WriteStartElement("topic");
    stream1.WriteAttributeString("id", "topic" + Topic.RemoveSpace(this._topicId.ToString()));
    stream1.WriteElementString("title", this._titlename);
    stream1.WriteStartElement("shortdesc");
    stream1.WriteStartElement("sup");
    stream1.WriteRaw("Modified: " + this._datetime.ToString());
    stream1.WriteEndElement();
    stream1.WriteEndElement();
    stream1.WriteStartElement("body");
    foreach (PXDitaElement element in this._elements)
      element.Write(stream1, (IFileManager) fileManager);
    stream1.WriteEndElement();
    stream1.WriteEndElement();
    stream1.Flush();
  }

  internal static string RemoveSpace(string filename)
  {
    filename = filename.Replace("-", "");
    return filename;
  }
}
