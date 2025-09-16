// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.DitaMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class DitaMap : IEnumerable<Topic>, IEnumerable
{
  private Guid _mapId;
  private readonly TopicCollection _topics;
  private readonly TopicCollection _globalCol;
  private readonly FileManager _fileManager;
  private const string DitamapTagName = "map";
  private const string DitamapTitleTagName = "title";
  private const string DitamapReferenceTagName = "topicref";

  public IEnumerator<Topic> GetEnumerator()
  {
    return (IEnumerator<Topic>) new DitaMap.TopicDitaMapEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) new DitaMap.TopicDitaMapEnumerator(this);

  internal string MapName { get; set; }

  internal string MapTitle { get; set; }

  internal DitaMap(
    TopicCollection globalCol,
    string mapname,
    string maptitle,
    FileManager fileManager)
  {
    this._mapId = Guid.NewGuid();
    this._topics = new TopicCollection();
    this._globalCol = globalCol;
    this.MapName = mapname;
    this.MapName = this.MapName.Replace(" ", "_");
    this.MapTitle = maptitle;
    this._fileManager = fileManager;
  }

  internal void Write(Stream stream, FileManager fileManager)
  {
    List<Topic> topicList = new List<Topic>();
    XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, (Encoding) new UnicodeEncoding());
    xmlTextWriter.WriteProcessingInstruction("ditamap", "xml version=\"1.0\" encoding=\"Unicode\"");
    xmlTextWriter.WriteRaw("<!DOCTYPE map SYSTEM \"technicalContent/dtd/map.dtd\">");
    xmlTextWriter.WriteStartElement("map");
    xmlTextWriter.WriteAttributeString("id", "map" + DitaMap.RemoveUnint(this._mapId.ToString()));
    xmlTextWriter.WriteElementString("title", this.MapTitle);
    IEnumerator<Topic> enumerator = this._topics.GetEnumerator();
    enumerator.MoveNext();
    while (enumerator.Current != null)
    {
      Topic current1 = enumerator.Current;
      if (!topicList.Contains(current1))
      {
        topicList.Add(current1);
        xmlTextWriter.WriteStartElement("topicref");
        xmlTextWriter.WriteAttributeString("href", fileManager.GetFullName(current1));
        xmlTextWriter.WriteAttributeString("navtitle", current1.Title);
        Stack<DitaMap.Plate> plateStack = new Stack<DitaMap.Plate>();
        IEnumerable<Topic> childs1 = fileManager.GetChilds(current1);
        if (childs1 != null)
        {
          DitaMap.Plate plate1 = new DitaMap.Plate(childs1.GetEnumerator());
          plateStack.Push(plate1);
          while (plateStack.Count > 0)
          {
            DitaMap.Plate plate2 = plateStack.Pop();
            Topic current2;
            if (plate2.ChildsIterator.MoveNext() && (current2 = plate2.ChildsIterator.Current) != null)
            {
              plateStack.Push(plate2);
              IEnumerable<Topic> childs2 = fileManager.GetChilds(current2);
              if (this._topics.Find(current2) > -1)
              {
                xmlTextWriter.WriteStartElement("topicref");
                xmlTextWriter.WriteAttributeString("href", fileManager.GetFullName(current2));
                xmlTextWriter.WriteAttributeString("navtitle", current2.Title);
                topicList.Add(current2);
              }
              if (childs2 != null)
              {
                DitaMap.Plate plate3 = new DitaMap.Plate(childs2.GetEnumerator());
                plateStack.Push(plate3);
              }
              else
                xmlTextWriter.WriteEndElement();
            }
            else
              xmlTextWriter.WriteEndElement();
          }
        }
        else
          xmlTextWriter.WriteEndElement();
      }
      enumerator.MoveNext();
    }
    xmlTextWriter.WriteEndElement();
    xmlTextWriter.Flush();
  }

  public void AddTopic(Topic topic)
  {
    int index = this._topics.Length();
    this._topics.Insert(index, topic);
    this._globalCol.Insert(index, topic);
    this._fileManager.AddTopic(topic);
  }

  public void AddTopic(Topic child, Topic parent)
  {
    List<Topic> topicList = new List<Topic>();
    if (this._fileManager.GetChilds(parent) != null)
      topicList.AddRange(this._fileManager.GetChilds(parent));
    int num = topicList.Count <= 0 ? this._topics.Find(parent) : this._topics.Find(topicList[topicList.Count - 1]);
    this._topics.Insert(num + 1, child);
    this._globalCol.Insert(num + 1, child);
    this._fileManager.AddTopic(child, parent);
  }

  internal static string RemoveUnint(string filename)
  {
    filename = filename.Replace("-", "");
    return filename;
  }

  private class Plate
  {
    private readonly IEnumerator<Topic> _childsIterator;

    public Plate(IEnumerator<Topic> childsIterator)
    {
      this._childsIterator = childsIterator != null ? childsIterator : throw new ArgumentNullException(nameof (childsIterator));
    }

    public IEnumerator<Topic> ChildsIterator => this._childsIterator;
  }

  internal class TopicDitaMapEnumerator : IEnumerator<Topic>, IDisposable, IEnumerator
  {
    private readonly IEnumerator _collection;

    public TopicDitaMapEnumerator(DitaMap ditamap)
    {
      this._collection = (IEnumerator) ditamap._topics.GetEnumerator();
    }

    public bool MoveNext() => this._collection.MoveNext();

    public void Reset() => this._collection.Reset();

    void IDisposable.Dispose()
    {
    }

    public Topic Current => (Topic) this._collection.Current;

    object IEnumerator.Current => (object) this.Current;
  }
}
