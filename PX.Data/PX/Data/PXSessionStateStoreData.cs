// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStateStoreData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Session;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Data;

internal sealed class PXSessionStateStoreData : SessionStateStoreData
{
  private static readonly string Key = typeof (IPXSessionState).FullName;

  public PXSessionStateStoreData(SessionStateStoreData inner)
    : base((ISessionStateItemCollection) new PXSessionStateStoreData.AlwaysDirtySessionStateItemCollection(inner.Items), inner.StaticObjects, inner.Timeout)
  {
    object obj = this.Items[PXSessionStateStoreData.Key];
    if (obj != null)
    {
      this.PXSessionState = obj is IPXSessionState pxSessionState ? pxSessionState : throw new InvalidOperationException($"Unexpected type {obj.GetType()} for key {PXSessionStateStoreData.Key}");
    }
    else
    {
      this.PXSessionState = (IPXSessionState) new DictionarySessionState();
      this.Items[PXSessionStateStoreData.Key] = (object) this.PXSessionState;
    }
  }

  internal IPXSessionState PXSessionState { get; }

  internal SessionStateStoreData PrepareForSet()
  {
    if (this.Items[PXSessionStateStoreData.Key] != this.PXSessionState)
      throw new InvalidOperationException("IPXSessionState was replaced in the session data");
    return new SessionStateStoreData(((PXSessionStateStoreData.AlwaysDirtySessionStateItemCollection) this.Items).Inner, this.StaticObjects, this.Timeout);
  }

  internal static void Clear(HttpSessionStateBase session)
  {
    for (int index = session.Keys.Count - 1; index >= 0; --index)
    {
      if (!(session.Keys[index] == PXSessionStateStoreData.Key))
        session.RemoveAt(index);
    }
  }

  private sealed class AlwaysDirtySessionStateItemCollection(ISessionStateItemCollection inner) : 
    ISessionStateItemCollection,
    ICollection,
    IEnumerable
  {
    internal ISessionStateItemCollection Inner { get; } = inner ?? throw new ArgumentNullException(nameof (inner));

    bool ISessionStateItemCollection.Dirty
    {
      get => true;
      set
      {
      }
    }

    object ISessionStateItemCollection.this[string name]
    {
      get => this.Inner[name];
      set => this.Inner[name] = value;
    }

    object ISessionStateItemCollection.this[int index]
    {
      get => this.Inner[index];
      set => this.Inner[index] = value;
    }

    void ISessionStateItemCollection.Remove(string name) => this.Inner.Remove(name);

    void ISessionStateItemCollection.RemoveAt(int index) => this.Inner.RemoveAt(index);

    void ISessionStateItemCollection.Clear() => this.Inner.Clear();

    NameObjectCollectionBase.KeysCollection ISessionStateItemCollection.Keys => this.Inner.Keys;

    void ICollection.CopyTo(Array array, int index) => this.Inner.CopyTo(array, index);

    int ICollection.Count => this.Inner.Count;

    bool ICollection.IsSynchronized => this.Inner.IsSynchronized;

    object ICollection.SyncRoot => this.Inner.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => this.Inner.GetEnumerator();
  }
}
