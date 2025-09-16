// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.ChildPlansByLS
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

[PXHidden]
[PXProjection(typeof (Select4<INItemPlan, Where<INItemPlan.origPlanID, IsNotNull, And<INItemPlan.lotSerialNbr, IsNotNull>>, Aggregate<GroupBy<INItemPlan.origPlanID, GroupBy<INItemPlan.lotSerialNbr, Sum<INItemPlan.planQty>>>>>))]
public class ChildPlansByLS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBLong(BqlField = typeof (INItemPlan.origPlanID), IsKey = true)]
  public virtual long? OrigPlanID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "", BqlField = typeof (INItemPlan.lotSerialNbr), IsKey = true)]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemPlan.planQty))]
  public virtual Decimal? OveralQty { get; set; }

  public abstract class origPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ChildPlansByLS.origPlanID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ChildPlansByLS.lotSerialNbr>
  {
  }

  public abstract class overalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ChildPlansByLS.overalQty>
  {
  }
}
