// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEndDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public sealed class EPEndDateAttribute : EPAllDaySupportDateTimeAttribute, IPXRowUpdatedSubscriber
{
  private readonly Type ActivityClass;
  private readonly Type StartDate;
  private readonly Type TimeSpent;

  public EPEndDateAttribute(Type activityClass, Type startDate, Type timeSpent = null)
  {
    this.ActivityClass = activityClass;
    this.StartDate = startDate;
    this.TimeSpent = timeSpent;
    ((PXDBDateAttribute) this).InputMask = "g";
    ((PXDBDateAttribute) this).PreserveTime = true;
    this.WithoutDisplayNames = true;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((PXDBDateAttribute) this).InputMask = ((PXDBDateAttribute) this).DisplayMask = this.RequireTimeOnActivity(sender) ? "g" : "d";
    ((PXDBDateAttribute) this).FieldSelecting(sender, e);
  }

  private bool RequireTimeOnActivity(PXCache sender)
  {
    EPSetup epSetup1 = (EPSetup) null;
    try
    {
      if (!(sender.Graph.Caches[typeof (EPSetup)].Current is EPSetup epSetup2))
        epSetup2 = ((PXSelectBase<EPSetup>) new PXSetup<EPSetup>(sender.Graph)).SelectSingle(Array.Empty<object>());
      epSetup1 = epSetup2;
    }
    catch
    {
    }
    return ((bool?) epSetup1?.RequireTimes).GetValueOrDefault();
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    int? nullable1 = (int?) sender.GetValue(e.Row, this.ActivityClass.Name);
    DateTime? nullable2 = (DateTime?) sender.GetValue(e.Row, this.StartDate.Name);
    DateTime? objB = (DateTime?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    int? nullable3 = this.TimeSpent != (Type) null ? (int?) sender.GetValue(e.Row, this.TimeSpent.Name) : new int?();
    if (!nullable1.HasValue || !nullable2.HasValue)
      return;
    DateTime? nullable4 = objB;
    switch (nullable1.Value)
    {
      case 0:
        DateTime? nullable5 = nullable2;
        DateTime? nullable6 = objB;
        if ((nullable5.HasValue & nullable6.HasValue ? (nullable5.GetValueOrDefault() > nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable4 = new DateTime?(nullable2.Value);
          break;
        }
        break;
      case 1:
        DateTime? nullable7 = nullable2;
        DateTime? nullable8 = objB;
        if ((nullable7.HasValue & nullable8.HasValue ? (nullable7.GetValueOrDefault() > nullable8.GetValueOrDefault() ? 1 : 0) : 0) != 0 || !objB.HasValue)
        {
          nullable4 = new DateTime?(nullable2.Value.AddMinutes(30.0));
          break;
        }
        if (object.Equals(sender.GetValue(e.OldRow, ((PXEventSubscriberAttribute) this)._FieldName), (object) objB))
        {
          DateTime? nullable9 = (DateTime?) sender.GetValue(e.OldRow, this.StartDate.Name);
          if (nullable9.HasValue)
          {
            nullable4 = new DateTime?(objB.Value.AddTicks((nullable2.Value - nullable9.Value).Ticks));
            break;
          }
          break;
        }
        break;
      default:
        nullable4 = nullable3.HasValue ? new DateTime?(nullable2.Value.AddMinutes((double) nullable3.Value)) : new DateTime?();
        break;
    }
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) nullable4);
  }

  public override ISet<Type> GetDependencies(PXCache cache)
  {
    ISet<Type> dependencies = base.GetDependencies(cache);
    if (this.ActivityClass != (Type) null)
      dependencies.Add(this.ActivityClass);
    if (this.StartDate != (Type) null)
      dependencies.Add(this.StartDate);
    if (this.TimeSpent != (Type) null)
      dependencies.Add(this.TimeSpent);
    return dependencies;
  }
}
