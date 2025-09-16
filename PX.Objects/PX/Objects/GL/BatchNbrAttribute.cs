// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

public class BatchNbrAttribute(Type searchType) : PXSelectorAttribute(searchType)
{
  public virtual Type IsMigratedRecordField { get; set; }

  private bool IsMigratedRecord(PXCache cache, object data)
  {
    string field = cache.GetField(this.IsMigratedRecordField);
    return (cache.GetValue(data, field) as bool?).GetValueOrDefault();
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.IsMigratedRecord(sender, e.Row))
      ((CancelEventArgs) e).Cancel = true;
    else
      base.FieldVerifying(sender, e);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!this.IsMigratedRecord(sender, e.Row))
      return;
    e.ReturnValue = (object) "MIGRATED";
  }
}
