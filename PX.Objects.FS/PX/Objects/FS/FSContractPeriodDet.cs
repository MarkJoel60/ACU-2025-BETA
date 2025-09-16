// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPeriodDet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Contract Period Detail")]
[Serializable]
public class FSContractPeriodDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXDBDefault(typeof (FSServiceContract.serviceContractID))]
  [PXUIField(DisplayName = "Service Contract ID")]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<FSContractPeriod, Where<FSContractPeriod.contractPeriodID, Equal<Current<FSContractPeriodDet.contractPeriodID>>>>))]
  [PXDBDefault(typeof (FSContractPeriod.contractPeriodID))]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? ContractPeriodDetID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type")]
  [ListField_LineType_ContractPeriod.ListAtrribute]
  [PXDefault("SERVI")]
  public virtual 
  #nullable disable
  string LineType { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "Inventory ID")]
  [InventoryIDByLineType(typeof (FSContractPeriodDet.lineType), null, Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<True>, Or<Current<FSServiceContract.recordType>, Equal<ListField_RecordType_ContractSchedule.ServiceContract>>>>), "Non-route service cannot be handled with current route Service Order Type.", new Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<False>, Or<Current<FSServiceContract.recordType>, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>>>), "Route service cannot be handled with current non-route Service Order Type.", new Type[] {})]
  [PXCheckUnique(new Type[] {typeof (FSContractPeriodDet.SMequipmentID)}, Where = typeof (Where<FSContractPeriodDet.serviceContractID, Equal<Current<FSContractPeriodDet.serviceContractID>>, And<FSContractPeriodDet.contractPeriodID, Equal<Current<FSContractPeriodDet.contractPeriodID>>, And<Where<Current<FSContractPeriodDet.SMequipmentID>, IsNull, Or<FSContractPeriodDet.SMequipmentID, Equal<Current<FSContractPeriodDet.SMequipmentID>>>>>>>))]
  public virtual int? InventoryID { get; set; }

  [INUnit(typeof (FSContractPeriodDet.inventoryID), DisplayName = "UOM")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSContractPeriodDet.inventoryID>>>>))]
  public virtual string UOM { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorContractPeriodEquipment]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new Type[] {typeof (FSEquipment.status)})]
  [PXForeignReference(typeof (Field<FSContractPeriodDet.SMequipmentID>.IsRelatedTo<FSEquipment.SMequipmentID>))]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule_ContractPeriod.ListAtrribute]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [PXDBTimeSpanLong]
  [PXFormula(typeof (Default<FSContractPeriodDet.inventoryID>))]
  [PXUIField(DisplayName = "Time", Enabled = false, Visible = false)]
  public virtual int? Time { get; set; }

  [PXDBQuantity]
  [PXFormula(typeof (Default<FSContractPeriodDet.inventoryID, FSContractPeriodDet.time>))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity", Visible = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Used Period Quantity", Enabled = false, Visible = false)]
  public virtual Decimal? UsedQty { get; set; }

  [PXDefault(0)]
  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Used Period Time", Enabled = false, Visible = false)]
  public virtual int? UsedTime { get; set; }

  [PXUIField(DisplayName = "Recurring Item Price")]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  public virtual Decimal? RecurringUnitPrice { get; set; }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Default<FSContractPeriodDet.qty, FSContractPeriodDet.recurringUnitPrice>))]
  [PXUIField(DisplayName = "Total Recurring Price", Enabled = false)]
  [PXFormula(typeof (Mult<FSContractPeriodDet.qty, FSContractPeriodDet.recurringUnitPrice>), typeof (SumCalc<FSContractPeriod.periodTotal>))]
  public virtual Decimal? RecurringTotalPrice { get; set; }

  [PXUIField(DisplayName = "Overage Item Price")]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  public virtual Decimal? OverageItemPrice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Rollover", IsReadOnly = true)]
  public virtual bool? Rollover { get; set; }

  [PXDBDecimal]
  [PXDefault(typeof (Switch<Case<Where<FSContractPeriodDet.usedQty, LessEqual<FSContractPeriodDet.qty>>, Sub<FSContractPeriodDet.qty, FSContractPeriodDet.usedQty>>, SharedClasses.decimal_0>))]
  [PXFormula(typeof (Default<FSContractPeriodDet.qty, FSContractPeriodDet.usedQty>))]
  [PXUIField(DisplayName = "Remaining Period Quantity", Enabled = false, Visible = false)]
  public virtual Decimal? RemainingQty { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault(typeof (Switch<Case<Where<FSContractPeriodDet.usedTime, LessEqual<FSContractPeriodDet.time>>, Sub<FSContractPeriodDet.time, FSContractPeriodDet.usedTime>>, SharedClasses.int_0>))]
  [PXFormula(typeof (Default<FSContractPeriodDet.time, FSContractPeriodDet.usedTime>))]
  [PXUIField(DisplayName = "Remaining Period Time", Enabled = false, Visible = false)]
  public virtual int? RemainingTime { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Scheduled Period Quantity", Enabled = false, Visible = false)]
  public virtual Decimal? ScheduledQty { get; set; }

  [PXDefault(0)]
  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Scheduled Period Time", Enabled = false, Visible = false)]
  public virtual int? ScheduledTime { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.income>>>))]
  [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXFormula(typeof (Default<FSContractPeriodDet.inventoryID>))]
  public virtual string DeferredCode { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSServiceContract.projectID))]
  [PXForeignReference(typeof (FSContractPeriodDet.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (FSServiceContract.dfltProjectTaskID))]
  [PXUIEnabled(typeof (Where<FSContractPeriodDet.lineType, NotEqual<ListField_LineType_ALL.Comment>, And<FSContractPeriodDet.lineType, NotEqual<ListField_LineType_ALL.Instruction>>>))]
  [ActiveOrInPlanningProjectTask(typeof (FSContractPeriodDet.projectID), DisplayName = "Project Task", DescriptionField = typeof (PMTask.description))]
  [PXForeignReference(typeof (FSContractPeriodDet.FK.Task))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDefault]
  [SMCostCode(typeof (FSContractPeriodDet.skipCostCodeValidation), null, typeof (FSContractPeriodDet.projectTaskID), DisplayName = "Cost Code", Filterable = false, Enabled = false)]
  [PXForeignReference(typeof (FSContractPeriodDet.FK.CostCode))]
  public virtual int? CostCodeID { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where<FSContractPeriodDet.lineType, Equal<ListField_LineType_ALL.Service>, Or<FSContractPeriodDet.lineType, Equal<ListField_LineType_ALL.NonStockItem>>>, False, True>))]
  public virtual bool? SkipCostCodeValidation { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created DateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By ID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXUIField(DisplayName = "Regular Price", IsReadOnly = true, Visible = false)]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  public virtual Decimal? RegularPrice { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Amount { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = " Remaining Period Value", Enabled = false)]
  public virtual string RemainingAmount { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Used Period Value", Enabled = false)]
  public virtual string UsedAmount { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Scheduled Period Value", Enabled = false)]
  public virtual string ScheduledAmount { get; set; }

  public class PK : 
    PrimaryKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.contractPeriodID, FSContractPeriodDet.contractPeriodDetID>
  {
    public static FSContractPeriodDet Find(
      PXGraph graph,
      int? contractPeriodID,
      int? contractPeriodDetID,
      PKFindOptions options = 0)
    {
      return (FSContractPeriodDet) PrimaryKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.contractPeriodID, FSContractPeriodDet.contractPeriodDetID>.FindBy(graph, (object) contractPeriodID, (object) contractPeriodDetID, options);
    }
  }

  public static class FK
  {
    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.serviceContractID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.inventoryID>
    {
    }

    public class ContractPeriod : 
      PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.serviceContractID, FSContractPeriod.contractPeriodID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.serviceContractID, FSContractPeriodDet.contractPeriodID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.SMequipmentID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.projectID, FSContractPeriodDet.projectTaskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSContractPeriodDet>.By<FSContractPeriodDet.costCodeID>
    {
    }
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.serviceContractID>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.contractPeriodID>
  {
  }

  public abstract class contractPeriodDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.contractPeriodDetID>
  {
  }

  public abstract class lineType : ListField_LineType_ContractPeriod
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPeriodDet.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractPeriodDet.uOM>
  {
  }

  public abstract class SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.SMequipmentID>
  {
  }

  public abstract class billingRule : ListField_BillingRule_ContractPeriod
  {
  }

  public abstract class time : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPeriodDet.time>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSContractPeriodDet.qty>
  {
  }

  public abstract class usedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSContractPeriodDet.usedQty>
  {
  }

  public abstract class usedTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPeriodDet.usedTime>
  {
  }

  public abstract class recurringUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.recurringUnitPrice>
  {
  }

  public abstract class recurringTotalPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.recurringTotalPrice>
  {
  }

  public abstract class overageItemPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.overageItemPrice>
  {
  }

  public abstract class rollover : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSContractPeriodDet.rollover>
  {
  }

  public abstract class remainingQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.remainingQty>
  {
  }

  public abstract class remainingTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.remainingTime>
  {
  }

  public abstract class scheduledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.scheduledQty>
  {
  }

  public abstract class scheduledTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.scheduledTime>
  {
  }

  public abstract class deferredCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.deferredCode>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPeriodDet.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodDet.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractPeriodDet.costCodeID>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSContractPeriodDet.skipCostCodeValidation>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriodDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractPeriodDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriodDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractPeriodDet.Tstamp>
  {
  }

  public abstract class regularPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodDet.regularPrice>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractPeriodDet.amount>
  {
  }

  public abstract class remainingAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.remainingAmount>
  {
  }

  public abstract class usedAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.usedAmount>
  {
  }

  public abstract class scheduledAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodDet.scheduledAmount>
  {
  }
}
