// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.TopicCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.DITA;

internal class TopicCollection : IEnumerable<Topic>, IEnumerable
{
  private readonly List<Topic> _innerCol;
  private readonly List<int> _topicsnumber;

  public IEnumerator<Topic> GetEnumerator()
  {
    return (IEnumerator<Topic>) new TopicCollection.TopicEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new TopicCollection.TopicEnumerator(this);
  }

  internal TopicCollection()
  {
    this._innerCol = new List<Topic>();
    this._topicsnumber = new List<int>();
  }

  internal void Add(Topic item)
  {
    int index = this._innerCol.IndexOf(item);
    if (index == -1)
    {
      this._innerCol.Add(item);
      this._topicsnumber.Add(1);
    }
    else
      this._topicsnumber[index]++;
  }

  internal int Length() => this._innerCol.Count;

  internal int Find(Topic item) => this._innerCol.IndexOf(item);

  internal void Insert(int index, Topic item)
  {
    int index1 = this._innerCol.IndexOf(item);
    if (index1 == -1)
    {
      this._innerCol.Insert(index, item);
      this._topicsnumber.Add(1);
    }
    else
      this._topicsnumber[index1]++;
  }

  internal class TopicEnumerator : IEnumerator<Topic>, IDisposable, IEnumerator
  {
    private readonly IEnumerator _collection;

    public TopicEnumerator(TopicCollection collection)
    {
      this._collection = (IEnumerator) collection._innerCol.GetEnumerator();
    }

    public bool MoveNext() => this._collection.MoveNext();

    public void Reset() => this._collection.Reset();

    void IDisposable.Dispose()
    {
    }

    public Topic Current
    {
      get
      {
        try
        {
          return (Topic) this._collection.Current;
        }
        catch (Exception ex)
        {
          return (Topic) null;
        }
      }
    }

    object IEnumerator.Current => (object) this.Current;
  }
}
