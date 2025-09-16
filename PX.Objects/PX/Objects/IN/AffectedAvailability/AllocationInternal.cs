// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.AllocationInternal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

[PXHidden]
[PXProjection(typeof (Select2<INItemPlan, LeftJoin<PX.Objects.SO.SOOrder, On<INItemPlan.refEntityType, Equal<EntityType.soOrder>, And<PX.Objects.SO.SOOrder.noteID, Equal<INItemPlan.refNoteID>, And<INItemPlan.planType, In3<INPlanConstants.plan61, INPlanConstants.plan63>>>>, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>, And<PX.Objects.SO.SOLineSplit.planID, Equal<INItemPlan.planID>>>>, LeftJoin<PX.Objects.SO.SOShipment, On<INItemPlan.refEntityType, Equal<EntityType.soShipment>, And<PX.Objects.SO.SOShipment.noteID, Equal<INItemPlan.refNoteID>, And<INItemPlan.planType, In3<INPlanConstants.plan61, INPlanConstants.plan63>>>>, LeftJoin<SOShipLineSplit, On<SOShipLineSplit.shipmentNbr, Equal<PX.Objects.SO.SOShipment.shipmentNbr>, And<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>, LeftJoin<PX.Objects.IN.INRegister, On<INItemPlan.refEntityType, In3<EntityType.inRegister, EntityType.inKitRegister>, And<PX.Objects.IN.INRegister.noteID, Equal<INItemPlan.refNoteID>, And<INItemPlan.planType, In3<INPlanConstants.plan20, INPlanConstants.plan40, INPlanConstants.plan41, INPlanConstants.plan50>>>>, LeftJoin<INTranSplit, On<INTranSplit.docType, Equal<PX.Objects.IN.INRegister.docType>, And<INTranSplit.refNbr, Equal<PX.Objects.IN.INRegister.refNbr>, And<INTranSplit.planID, Equal<INItemPlan.planID>>>>, LeftJoin<ChildPlans, On<ChildPlans.origPlanID, Equal<INItemPlan.planID>>, LeftJoin<ChildPlansByLS, On<ChildPlansByLS.origPlanID, Equal<INItemPlan.planID>, And<ChildPlansByLS.lotSerialNbr, Equal<INItemPlan.lotSerialNbr>>>>>>>>>>>, Where<INItemPlan.planType, In3<INPlanConstants.plan20, INPlanConstants.plan40, INPlanConstants.plan41, INPlanConstants.plan50, INPlanConstants.plan61, INPlanConstants.plan63, INPlanConstants.planF2, INPlanConstants.planM7>, And<Where<ChildPlans.origPlanID, IsNull, Or<ChildPlans.overalQty, Less<INItemPlan.planQty>, Or<INItemPlan.lotSerialNbr, IsNotNull, And<Where<ChildPlansByLS.lotSerialNbr, IsNull, Or<ChildPlansByLS.overalQty, Less<ChildPlans.overalQty>>>>>>>>>>))]
public class AllocationInternal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBLong(IsKey = true, BqlField = typeof (INItemPlan.planID))]
  public virtual long? PlanID { get; set; }

  [PXRefNote(BqlField = typeof (INItemPlan.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false, BqlField = typeof (INItemPlan.refEntityType))]
  public 
  #nullable disable
  string RefEntityType { get; set; }

  [PXInt]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.refEntityType, Equal<EntityType.soOrder>>, PX.Objects.SO.SOLineSplit.lineNbr, Case<Where<INItemPlan.refEntityType, Equal<EntityType.soShipment>>, SOShipLineSplit.lineNbr>>, INTranSplit.lineNbr>), typeof (int))]
  public virtual int? LineNbr { get; set; }

  [StockItem(BqlField = typeof (INItemPlan.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [Site(BqlField = typeof (INItemPlan.siteID))]
  public virtual int? SiteID { get; set; }

  [PXString(100, IsUnicode = true, InputMask = "")]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.lotSerialNbr, IsNotNull>, INItemPlan.lotSerialNbr>, StringEmpty>), typeof (string))]
  public virtual string LotSerialNbr { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.lotSerialNbr, IsNotNull, And<ChildPlans.origPlanID, IsNotNull, And<Where<ChildPlansByLS.lotSerialNbr, IsNull, Or<ChildPlansByLS.overalQty, Less<INItemPlan.planQty>>>>>>, Sub<INItemPlan.planQty, IsNull<ChildPlansByLS.overalQty, decimal0>>, Case<Where<ChildPlans.origPlanID, IsNotNull, And<ChildPlans.overalQty, Less<INItemPlan.planQty>>>, Sub<INItemPlan.planQty, ChildPlans.overalQty>>>, INItemPlan.planQty>), typeof (Decimal))]
  public virtual Decimal? AllocatedQty { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = false)]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.refEntityType, Equal<EntityType.soOrder>>, Add<INItemPlan.refEntityType, PX.Objects.SO.SOOrder.status>, Case<Where<INItemPlan.refEntityType, Equal<EntityType.soShipment>>, Add<INItemPlan.refEntityType, PX.Objects.SO.SOShipment.status>>>, Add<EntityType.inRegister, PX.Objects.IN.INRegister.status>>), typeof (string))]
  public virtual string Status { get; set; }

  [PXInt]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.refEntityType, Equal<EntityType.soOrder>>, PX.Objects.SO.SOOrder.ownerID, Case<Where<INItemPlan.refEntityType, Equal<EntityType.soShipment>>, PX.Objects.SO.SOShipment.ownerID>>, Null>), typeof (int))]
  public virtual int? OwnerID { get; set; }

  [PXInt]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.refEntityType, Equal<EntityType.soOrder>>, PX.Objects.SO.SOOrder.customerID, Case<Where<INItemPlan.refEntityType, Equal<EntityType.soShipment>>, PX.Objects.SO.SOShipment.customerID>>, Null>), typeof (int))]
  public virtual int? CustomerID { get; set; }

  [PXDate]
  [PXDBCalced(typeof (Switch<Case<Where<INItemPlan.refEntityType, Equal<EntityType.soOrder>>, PX.Objects.SO.SOOrder.orderDate, Case<Where<INItemPlan.refEntityType, Equal<EntityType.soShipment>>, PX.Objects.SO.SOShipment.shipDate>>, PX.Objects.IN.INRegister.tranDate>), typeof (DateTime))]
  public virtual DateTime? Date { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOOrder.requestDate))]
  public virtual DateTime? RequestedDate { get; set; }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AllocationInternal.planID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AllocationInternal.refNoteID>
  {
  }

  public abstract class refEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AllocationInternal.refEntityType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AllocationInternal.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AllocationInternal.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AllocationInternal.siteID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AllocationInternal.lotSerialNbr>
  {
  }

  public abstract class allocatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AllocationInternal.allocatedQty>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AllocationInternal.status>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AllocationInternal.ownerID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AllocationInternal.customerID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AllocationInternal.date>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AllocationInternal.requestedDate>
  {
  }
}
