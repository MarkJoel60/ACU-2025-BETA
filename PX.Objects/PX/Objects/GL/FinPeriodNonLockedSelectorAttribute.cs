// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodNonLockedSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.GL;

public class FinPeriodNonLockedSelectorAttribute : 
  FinPeriodSelectorAttribute,
  IPXFieldVerifyingSubscriber
{
  public FinPeriodNonLockedSelectorAttribute()
    : this((Type) null)
  {
  }

  public FinPeriodNonLockedSelectorAttribute(Type sourceType)
    : base(typeof (Search5<MasterFinPeriod.finPeriodID, LeftJoin<FinPeriod, On<FinPeriod.finPeriodID, Equal<MasterFinPeriod.finPeriodID>, And<FinPeriod.organizationID, NotEqual<FinPeriod.organizationID.masterValue>, And<FinPeriod.status, Equal<FinPeriod.status.locked>>>>>, Where<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>, Aggregate<GroupBy<MasterFinPeriod.finPeriodID>>>), sourceType, fieldList: new Type[4]
    {
      typeof (MasterFinPeriod.finPeriodID),
      typeof (MasterFinPeriod.descr),
      typeof (MasterFinPeriod.startDateUI),
      typeof (MasterFinPeriod.endDateUI)
    })
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDefaultAttribute(typeof (Search5<MasterFinPeriod.finPeriodID, LeftJoin<FinPeriod, On<FinPeriod.finPeriodID, Equal<MasterFinPeriod.finPeriodID>, And<FinPeriod.organizationID, NotEqual<FinPeriod.organizationID.masterValue>, And<FinPeriod.status, Equal<FinPeriod.status.locked>>>>>, Where<FinPeriod.finPeriodID, IsNull, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>, Aggregate<GroupBy<MasterFinPeriod.finPeriodID>>, OrderBy<Asc<MasterFinPeriod.finPeriodID>>>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<FinPeriod.finPeriodID, IsNull>), "The {0} financial period is locked in at least one company.", new Type[1]
    {
      typeof (MasterFinPeriod.finPeriodID)
    }));
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    base.FieldVerifying(sender, e);
    if (string.IsNullOrEmpty(e.NewValue?.ToString()))
      throw new PXSetPropertyException("GL Error: You must fill in the Fin. Period box to perform validation.", new object[1]
      {
        (object) "finPeriodID"
      });
  }
}
