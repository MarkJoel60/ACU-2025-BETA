// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeStampScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public sealed class PXTimeStampScope : IDisposable
{
  private bool _Disposed;
  private PXTimeStampScope _Previous;
  private byte[] _TimeStamp;
  private Dictionary<System.Type, PXHashtable> _Persisted;
  private Dictionary<System.Type, PXHashtable> _PersistedRecords;
  private Dictionary<System.Type, bool> _RecordComesFirst;

  public PXTimeStampScope(byte[] timeStamp)
  {
    this._TimeStamp = timeStamp;
    this._Previous = PXContext.GetSlot<PXTimeStampScope>();
    if (this._Previous != null)
    {
      this._Persisted = this._Previous._Persisted;
      this._PersistedRecords = this._Previous._PersistedRecords;
      this._RecordComesFirst = this._Previous._RecordComesFirst;
    }
    else
    {
      this._Persisted = new Dictionary<System.Type, PXHashtable>();
      this._PersistedRecords = new Dictionary<System.Type, PXHashtable>();
      this._RecordComesFirst = new Dictionary<System.Type, bool>();
    }
    PXContext.SetSlot<PXTimeStampScope>(this);
  }

  public void Dispose()
  {
    if (this._Disposed)
      return;
    PXContext.SetSlot<PXTimeStampScope>(this._Previous);
    this._Disposed = true;
  }

  public static byte[] GetValue() => PXContext.GetSlot<PXTimeStampScope>()?._TimeStamp;

  public static void PutPersisted(PXCache sender, object item, params object[] values)
  {
    if (item == null)
      return;
    for (PXTimeStampScope pxTimeStampScope = PXContext.GetSlot<PXTimeStampScope>(); pxTimeStampScope != null; pxTimeStampScope = pxTimeStampScope._Previous)
    {
      System.Type bqlTable = sender.BqlTable;
      PXHashtable pxHashtable;
      if (!pxTimeStampScope._Persisted.TryGetValue(bqlTable, out pxHashtable))
        pxTimeStampScope._Persisted[bqlTable] = pxHashtable = new PXHashtable();
      pxHashtable.Put(sender, item, values);
      if (!pxTimeStampScope._PersistedRecords.TryGetValue(bqlTable, out pxHashtable))
        pxTimeStampScope._PersistedRecords[bqlTable] = pxHashtable = new PXHashtable();
      pxHashtable.Put(sender, item, item);
    }
  }

  public static object[] GetPersisted(PXCache sender, object item)
  {
    if (item != null)
    {
      PXTimeStampScope slot = PXContext.GetSlot<PXTimeStampScope>();
      if (slot != null)
      {
        if (slot._Persisted == null)
          return (object[]) null;
        System.Type bqlTable = sender.BqlTable;
        PXHashtable pxHashtable;
        return !slot._Persisted.TryGetValue(bqlTable, out pxHashtable) ? (object[]) null : pxHashtable.Get(sender, item);
      }
    }
    return (object[]) null;
  }

  public static object GetPersistedRecord(PXCache sender, object item)
  {
    if (item != null)
    {
      PXTimeStampScope slot = PXContext.GetSlot<PXTimeStampScope>();
      if (slot != null)
      {
        if (slot._PersistedRecords == null)
          return (object) null;
        System.Type bqlTable = sender.BqlTable;
        PXHashtable pxHashtable;
        if (!slot._PersistedRecords.TryGetValue(bqlTable, out pxHashtable))
          return (object) null;
        return pxHashtable.Get(sender, item)?[0];
      }
    }
    return (object) null;
  }

  public static void DuplicatePersisted(PXCache sender, object item, System.Type sourceTable)
  {
    if (item == null)
      return;
    for (PXTimeStampScope pxTimeStampScope = PXContext.GetSlot<PXTimeStampScope>(); pxTimeStampScope != null; pxTimeStampScope = pxTimeStampScope._Previous)
    {
      PXHashtable pxHashtable1;
      if (pxTimeStampScope._Persisted.TryGetValue(sourceTable, out pxHashtable1))
      {
        object[] objArray = pxHashtable1.Get(sender, item);
        if (objArray != null && objArray.Length != 0)
        {
          System.Type bqlTable = sender.BqlTable;
          PXHashtable pxHashtable2;
          if (!pxTimeStampScope._Persisted.TryGetValue(bqlTable, out pxHashtable2))
            pxTimeStampScope._Persisted[bqlTable] = pxHashtable2 = new PXHashtable();
          pxHashtable2.Put(sender, item, objArray[0]);
          if (!pxTimeStampScope._PersistedRecords.TryGetValue(bqlTable, out pxHashtable2))
            pxTimeStampScope._PersistedRecords[bqlTable] = pxHashtable2 = new PXHashtable();
          pxHashtable2.Put(sender, item, item);
        }
      }
    }
  }

  public static void SetRecordComesFirst(System.Type Table, bool value)
  {
    PXTimeStampScope slot = PXContext.GetSlot<PXTimeStampScope>();
    if (slot == null)
      return;
    slot._RecordComesFirst[Table] = value;
  }

  public static bool GetRecordComesFirst(System.Type Table)
  {
    PXTimeStampScope slot = PXContext.GetSlot<PXTimeStampScope>();
    bool flag;
    return slot != null && slot._RecordComesFirst.TryGetValue(Table, out flag) && flag;
  }
}
