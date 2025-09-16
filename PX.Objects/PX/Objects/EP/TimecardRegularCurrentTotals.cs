// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimecardRegularCurrentTotals
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXProjection(typeof (Select5<PMTimeActivity, InnerJoin<EPEarningType, On<EPEarningType.typeCD, Equal<PMTimeActivity.earningTypeID>>>, Where<PMTimeActivity.timeCardCD, IsNull, And<EPEarningType.isOvertime, Equal<False>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>>>>>, Aggregate<GroupBy<PMTimeActivity.weekID, GroupBy<PMTimeActivity.ownerID, Sum<PMTimeActivity.timeSpent, Sum<PMTimeActivity.timeBillable>>>>>>))]
[PXHidden]
[Serializable]
public class TimecardRegularCurrentTotals : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (PMTimeActivity.weekID))]
  public virtual int? WeekID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.ownerID))]
  public virtual int? Owner { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.timeSpent))]
  public virtual int? TimeSpent { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.timeBillable))]
  public virtual int? TimeBillable { get; set; }

  public abstract class weekID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  TimecardRegularCurrentTotals.weekID>
  {
  }

  public abstract class owner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TimecardRegularCurrentTotals.owner>
  {
  }

  public abstract class timeSpent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardRegularCurrentTotals.timeSpent>
  {
  }

  public abstract class timeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardRegularCurrentTotals.timeBillable>
  {
  }
}
