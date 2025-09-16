// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractLineNbr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

public class ContractLineNbr(Type sourceType) : PXLineNbrAttribute(sourceType)
{
  protected HashSet<object> _justInserted;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._justInserted = new HashSet<object>();
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    base.RowInserted(sender, e);
    this._justInserted.Add(e.Row);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!this._justInserted.Contains(e.Row))
      return;
    base.RowDeleted(sender, e);
    this._justInserted.Remove(e.Row);
  }
}
