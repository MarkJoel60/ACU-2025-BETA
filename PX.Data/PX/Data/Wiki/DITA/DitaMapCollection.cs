// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.DitaMapCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class DitaMapCollection : IEnumerable<DitaMap>, IEnumerable
{
  private readonly TopicCollection _globaltopics;
  private readonly List<DitaMap> _innerCol;
  private readonly FileManager _filemanager;

  public IEnumerator<DitaMap> GetEnumerator()
  {
    return (IEnumerator<DitaMap>) new DitaMapCollection.DitaMapEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new DitaMapCollection.DitaMapEnumerator(this);
  }

  internal DitaMapCollection(TopicCollection topics, FileManager filemanager)
  {
    this._globaltopics = topics;
    this._innerCol = new List<DitaMap>();
    this._filemanager = filemanager;
  }

  public DitaMap Add(string mapname, string maptitle)
  {
    DitaMap ditaMap = new DitaMap(this._globaltopics, mapname, maptitle, this._filemanager);
    if (!this._innerCol.Contains(ditaMap))
      this._innerCol.Add(ditaMap);
    return ditaMap;
  }

  internal class DitaMapEnumerator : IEnumerator<DitaMap>, IDisposable, IEnumerator
  {
    private readonly IEnumerator _collection;

    public DitaMapEnumerator(DitaMapCollection collection)
    {
      this._collection = (IEnumerator) collection._innerCol.GetEnumerator();
    }

    public bool MoveNext() => this._collection.MoveNext();

    public void Reset() => this._collection.Reset();

    void IDisposable.Dispose()
    {
    }

    public DitaMap Current => (DitaMap) this._collection.Current;

    object IEnumerator.Current => (object) this.Current;
  }
}
