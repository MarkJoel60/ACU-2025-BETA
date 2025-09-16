// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAllDayAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public sealed class EPAllDayAttribute : 
  PXDBBoolAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowSelectedSubscriber,
  IPXDependsOnFields
{
  private readonly System.Type StartDate;
  private readonly System.Type EndDate;

  public EPAllDayAttribute(System.Type startDate, System.Type endDate)
  {
    this.StartDate = startDate;
    this.EndDate = endDate;
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool? nullable1 = (bool?) sender.GetValue(e.OldRow, ((PXEventSubscriberAttribute) this)._FieldName);
    bool? nullable2 = (bool?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    DateTime? nullable3 = (DateTime?) sender.GetValue(e.Row, this.StartDate.Name);
    DateTime? nullable4 = (DateTime?) sender.GetValue(e.Row, this.EndDate.Name);
    DateTime? nullable5 = nullable3;
    DateTime? nullable6 = nullable4;
    DateTime? date1 = nullable3?.Date;
    DateTime? date2 = nullable4?.Date;
    DateTime? nullable7;
    DateTime? nullable8;
    if (nullable2.GetValueOrDefault() && date1.HasValue)
    {
      if (date2.HasValue)
      {
        nullable7 = date2;
        nullable8 = date1;
        if ((nullable7.HasValue & nullable8.HasValue ? (nullable7.GetValueOrDefault() < nullable8.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          nullable8 = date2;
          nullable7 = date1;
          if ((nullable8.HasValue == nullable7.HasValue ? (nullable8.HasValue ? (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || !nullable1.HasValue || nullable1.GetValueOrDefault())
            goto label_5;
        }
      }
      nullable6 = new DateTime?(date1.Value.AddDays(1.0));
label_5:
      nullable5 = date1;
    }
    else if (!nullable2.GetValueOrDefault())
    {
      bool? nullable9 = nullable2;
      bool? nullable10 = nullable1;
      if (!(nullable9.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable9.HasValue == nullable10.HasValue))
      {
        nullable5 = date1;
        nullable6 = date2;
      }
    }
    nullable7 = nullable3;
    nullable8 = nullable5;
    if ((nullable7.HasValue == nullable8.HasValue ? (nullable7.HasValue ? (nullable7.GetValueOrDefault() != nullable8.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
    {
      nullable8 = nullable4;
      nullable7 = nullable6;
      if ((nullable8.HasValue == nullable7.HasValue ? (nullable8.HasValue ? (nullable8.GetValueOrDefault() != nullable7.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
    }
    sender.SetValue(e.Row, this.StartDate.Name, (object) nullable5);
    sender.SetValue(e.Row, this.EndDate.Name, (object) nullable6);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool? nullable = (bool?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    PXFieldState stateExt1 = sender.GetStateExt(e.Row, typeof (CRActivity.startDate).Name + "_Date") as PXFieldState;
    PXFieldState stateExt2 = sender.GetStateExt(e.Row, typeof (CRActivity.endDate).Name + "_Date") as PXFieldState;
    if (stateExt1 != null)
      PXDBDateAndTimeAttribute.SetTimeEnabled<CRActivity.startDate>(sender, e.Row, stateExt1.Enabled && !nullable.GetValueOrDefault());
    if (stateExt2 == null)
      return;
    PXDBDateAndTimeAttribute.SetTimeEnabled<CRActivity.endDate>(sender, e.Row, stateExt2.Enabled && !nullable.GetValueOrDefault());
  }

  public ISet<System.Type> GetDependencies(PXCache cache)
  {
    HashSet<System.Type> dependencies = new HashSet<System.Type>();
    if (this.StartDate != (System.Type) null)
      dependencies.Add(this.StartDate);
    if (this.EndDate != (System.Type) null)
      dependencies.Add(this.EndDate);
    return (ISet<System.Type>) dependencies;
  }
}
