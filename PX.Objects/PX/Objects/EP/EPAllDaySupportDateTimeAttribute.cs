// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAllDaySupportDateTimeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class EPAllDaySupportDateTimeAttribute : PXDBDateAndTimeAttribute, IPXDependsOnFields
{
  protected virtual PXTimeZoneInfo DisplayTimeZone { get; set; }

  public Type TimeZone { get; set; }

  public Type OwnerID { get; set; }

  public Type AllDayField { get; set; }

  protected virtual PXTimeZoneInfo GetTimeZone()
  {
    return this.DisplayTimeZone == null ? ((PXDBDateAttribute) this).GetTimeZone() : this.DisplayTimeZone;
  }

  public virtual void Time_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this.SetPreserveTime(sender, e.Row);
    base.Time_FieldSelecting(sender, e);
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.SetPreserveTime(sender, e.Row, true);
    ((PXDBFieldAttribute) this).CommandPreparing(sender, e);
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    this.SetPreserveTime(sender, e.Row);
    ((PXDBDateAttribute) this).RowSelecting(sender, e);
  }

  private void SetPreserveTime(PXCache sender, object row, bool disregardOwner = false)
  {
    if (!(this.AllDayField != (Type) null))
      return;
    ((PXDBDateAttribute) this).PreserveTime = !((bool?) sender.GetValue(row, sender.GetField(this.AllDayField))).GetValueOrDefault();
    this.DisplayTimeZone = (PXTimeZoneInfo) null;
    if ((sender.Graph.IsImport || sender.Graph.IsExport) && !sender.Graph.IsMobile || !((PXDBDateAttribute) this).PreserveTime || row == null || !(this.TimeZone != (Type) null) || !(this.OwnerID != (Type) null))
      return;
    int? nullable1 = (int?) sender.GetValue(row, sender.GetField(this.OwnerID));
    if (!disregardOwner && nullable1.HasValue)
    {
      int num;
      if (sender == null)
      {
        num = !nullable1.HasValue ? 1 : 0;
      }
      else
      {
        int? contactId = (int?) sender.Graph?.Accessinfo?.ContactID;
        int? nullable2 = nullable1;
        num = contactId.GetValueOrDefault() == nullable2.GetValueOrDefault() & contactId.HasValue == nullable2.HasValue ? 1 : 0;
      }
      if (num == 0)
        return;
    }
    this.DisplayTimeZone = PXTimeZoneInfo.FindSystemTimeZoneById((string) sender.GetValue(row, sender.GetField(this.TimeZone)));
  }

  public virtual ISet<Type> GetDependencies(PXCache cache)
  {
    HashSet<Type> dependencies = new HashSet<Type>();
    if (this.TimeZone != (Type) null)
      dependencies.Add(this.TimeZone);
    if (this.OwnerID != (Type) null)
      dependencies.Add(this.OwnerID);
    if (this.AllDayField != (Type) null)
      dependencies.Add(this.AllDayField);
    return (ISet<Type>) dependencies;
  }
}
