// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemPlanToShip
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INPlanType>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Field<INItemPlan.planType>.IsRelatedTo<INPlanType.planType>.AsSimpleKey.WithTablesOf<INPlanType, INItemPlan>>, And<BqlOperand<INPlanType.isDemand, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INPlanType.isFixed, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<INPlanType.isForDate, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.hold, NotEqual<True>>>>, And<BqlOperand<INItemPlan.planQty, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.fixedSource, IsNull>>>>.Or<BqlOperand<INItemPlan.fixedSource, IBqlString>.IsNotEqual<INReplenishmentSource.transfer>>>>), Persistent = false)]
public class INItemPlanToShip : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBLong(IsKey = true, BqlField = typeof (INItemPlan.planID))]
  public virtual long? PlanID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false, BqlField = typeof (INItemPlan.refEntityType))]
  public 
  #nullable disable
  string RefEntityType { get; set; }

  [PXDBGuid(false, BqlField = typeof (INItemPlan.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBInt(BqlField = typeof (INItemPlan.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBInt(BqlField = typeof (INItemPlan.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INItemPlan.planType))]
  public virtual string PlanType { get; set; }

  [PXDBDate(BqlField = typeof (INItemPlan.planDate))]
  public virtual DateTime? PlanDate { get; set; }

  [PXDBDecimal(6, BqlField = typeof (INItemPlan.planQty))]
  public virtual Decimal? PlanQty { get; set; }

  [PXDBBool(BqlField = typeof (INItemPlan.reverse))]
  public virtual bool? Reverse { get; set; }

  [PXDBShort(BqlField = typeof (INPlanType.inclQtySOBackOrdered))]
  public virtual short? InclQtySOBackOrdered { get; set; }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemPlanToShip>.By<INItemPlanToShip.siteID>
    {
    }
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlanToShip.planID>
  {
  }

  public abstract class refEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemPlanToShip.refEntityType>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemPlanToShip.refNoteID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlanToShip.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlanToShip.inventoryID>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlanToShip.planType>
  {
  }

  public abstract class planDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemPlanToShip.planDate>
  {
  }

  public abstract class planQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemPlanToShip.planQty>
  {
  }

  public abstract class reverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlanToShip.reverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INItemPlanToShip.inclQtySOBackOrdered>
  {
  }
}
