// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOfflineAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class)]
public class PXOfflineAttribute : PXDBInterceptorAttribute
{
  protected 
  #nullable disable
  BqlCommand _Command;
  protected List<object> _Inserted;
  protected List<object> _Updated;
  protected List<object> _Deleted;

  protected internal virtual List<object> Inserted => this._Inserted;

  protected internal virtual List<object> Updated => this._Updated;

  protected internal virtual List<object> Deleted => this._Deleted;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Command = BqlCommand.CreateInstance(typeof (Select<,>), sender.BqlTable, typeof (Where<PXOfflineAttribute.int0, NotEqual<PXOfflineAttribute.int0>>));
  }

  public override BqlCommand GetRowCommand() => this._Command;

  public override BqlCommand GetTableCommand() => this._Command;

  public override bool PersistDeleted(PXCache sender, object row)
  {
    sender.SetStatus(row, PXEntryStatus.Notchanged);
    return true;
  }

  public override bool PersistInserted(PXCache sender, object row)
  {
    sender.SetStatus(row, PXEntryStatus.Notchanged);
    return true;
  }

  public override bool PersistUpdated(PXCache sender, object row)
  {
    sender.SetStatus(row, PXEntryStatus.Notchanged);
    return true;
  }

  public override object Delete(PXCache sender, object row)
  {
    sender.Current = (object) null;
    if (sender.Graph.Defaults.ContainsKey(sender.GetItemType()))
      sender.Graph.Defaults.Remove(sender.GetItemType());
    if (this._Deleted == null)
      this._Deleted = new List<object>();
    this._Deleted.Add(row);
    return row;
  }

  public override object Insert(PXCache sender, object row)
  {
    sender.Current = (object) null;
    sender.Graph.Defaults[sender.GetItemType()] = (PXGraph.GetDefaultDelegate) (() => row);
    if (this._Inserted == null)
      this._Inserted = new List<object>();
    this._Inserted.Add(row);
    return row;
  }

  public override object Update(PXCache sender, object row)
  {
    sender.Current = (object) null;
    sender.Graph.Defaults[sender.GetItemType()] = (PXGraph.GetDefaultDelegate) (() => row);
    if (this._Updated == null)
      this._Updated = new List<object>();
    this._Updated.Add(row);
    return row;
  }

  protected class int0 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXOfflineAttribute.int0>
  {
    public int0()
      : base(0)
    {
    }
  }
}
