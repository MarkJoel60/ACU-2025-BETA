// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStateItemCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Session;
using System.Collections;
using System.Collections.Specialized;
using System.Web.SessionState;

#nullable disable
namespace PX.Data;

internal class PXSessionStateItemCollection : 
  NameObjectCollectionBase,
  ISessionStateItemCollection,
  ICollection,
  IEnumerable
{
  internal ISessionStateItemCollection innerCollection;

  public PXSessionStateItemCollection()
  {
  }

  public PXSessionStateItemCollection(
    ISessionStateItemCollection collection,
    bool copyStateItemsRequired = false)
  {
    if (copyStateItemsRequired)
    {
      this.innerCollection = (ISessionStateItemCollection) new SessionStateItemCollection();
      try
      {
        for (int index = 0; index < collection.Keys.Count; ++index)
        {
          object obj = collection[index];
          if (obj is IPXSessionState pxSessionState1)
          {
            IPXSessionState pxSessionState = (IPXSessionState) new DictionarySessionState();
            foreach (string key in pxSessionState1.Keys)
              pxSessionState.Set(key, pxSessionState1.Get(key));
            this.innerCollection[collection.Keys[index]] = (object) pxSessionState;
          }
          else
            this.innerCollection[collection.Keys[index]] = obj;
        }
      }
      catch
      {
      }
    }
    else
      this.innerCollection = collection;
  }

  public void Clear() => this.innerCollection.Clear();

  public bool Dirty
  {
    get => this.innerCollection.Dirty;
    set => this.innerCollection.Dirty = value;
  }

  public ISessionStateItemCollection InnerCollection => this.innerCollection;

  public override NameObjectCollectionBase.KeysCollection Keys => this.innerCollection.Keys;

  public void Remove(string name) => this.innerCollection.Remove(name);

  public void RemoveAt(int index) => this.innerCollection.RemoveAt(index);

  public object this[int index]
  {
    get => this.innerCollection[index];
    set => this.innerCollection[index] = value;
  }

  public object this[string name]
  {
    get => this.innerCollection[name];
    set => this.innerCollection[name] = value;
  }

  public override IEnumerator GetEnumerator() => this.innerCollection.GetEnumerator();

  public override int Count => this.innerCollection.Count;
}
