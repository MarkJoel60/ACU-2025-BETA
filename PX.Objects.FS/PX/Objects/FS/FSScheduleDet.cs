// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSScheduleDet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSScheduleDet")]
[Serializable]
public class FSScheduleDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  private 
  #nullable disable
  string _LineType;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ScheduleID")]
  [PXParent(typeof (Select<FSSchedule, Where<FSSchedule.scheduleID, Equal<Current<FSScheduleDet.scheduleID>>>>))]
  [PXDBDefault(typeof (FSSchedule.scheduleID))]
  public virtual int? ScheduleID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSSchedule.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? ScheduleDetID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Line Type")]
  [FSLineType.List]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXFormula(typeof (Default<FSScheduleDet.lineType>))]
  [InventoryIDByLineType(typeof (FSScheduleDet.lineType), null, Filterable = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<True>, Or<Current<FSSrvOrdType.requireRoute>, Equal<False>>>>), "Non-route service cannot be handled with current route Service Order Type.", new Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<False>, Or<Current<FSSrvOrdType.requireRoute>, Equal<True>>>>), "Route service cannot be handled with current non-route Service Order Type.", new Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, Or<Current<FSSrvOrdType.postToSOSIPM>, Equal<True>>>), "This stock item cannot be handled for current Service Order Type.", new Type[] {})]
  public virtual int? InventoryID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXFormula(typeof (Default<FSScheduleDet.inventoryID, FSScheduleDet.lineType>))]
  [PXDefault(typeof (Switch<Case<Where<FSScheduleDet.lineType, Equal<ListField_LineType_ALL.Service>>, Selector<FSScheduleDet.inventoryID, FSxService.billingRule>>, ListField_BillingRule.FlatRate>))]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [FSDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXFormula(typeof (Default<FSScheduleDet.inventoryID, FSScheduleDet.lineType>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSScheduleDet.lineType, Equal<FSLineType.Service>>, FSScheduleDet.estimatedDuration>, SharedClasses.int_0>), typeof (SumCalc<FSSchedule.estimatedDurationTotal>))]
  [PXDefault(typeof (Switch<Case<Where<FSScheduleDet.lineType, Equal<FSLineType.Service>>, Selector<FSScheduleDet.inventoryID, FSxService.estimatedDuration>>, SharedClasses.int_0>))]
  public virtual int? EstimatedDuration { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Estimated Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Service Template ID")]
  [PXSelector(typeof (Search<FSServiceTemplate.serviceTemplateID, Where<FSServiceTemplate.srvOrdType, Equal<Current<FSSchedule.srvOrdType>>>>), SubstituteKey = typeof (FSServiceTemplate.serviceTemplateCD), DescriptionField = typeof (FSServiceTemplate.descr))]
  public virtual int? ServiceTemplateID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  public virtual int? SortOrder { get; set; }

  [PXDefault]
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Transaction Description")]
  public virtual string TranDesc { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the sales unit for the Inventory Item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.InventoryItem.SalesUnit">Sales Unit</see> associated with the <see cref="P:PX.Objects.FS.FSScheduleDet.InventoryID">Inventory Item</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSScheduleDet.inventoryID>>>>))]
  [INUnit(typeof (FSScheduleDet.inventoryID), DisplayName = "UOM")]
  public virtual string UOM { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_Schedule_EquipmentAction.ListAtrribute]
  [PXDefault("NO")]
  [PXUIField(DisplayName = "Equipment Action", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual string EquipmentAction { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorMaintenanceEquipment(typeof (FSSchedule.srvOrdType), typeof (FSSchedule.billCustomerID), typeof (FSSchedule.customerID), typeof (FSSchedule.customerLocationID), typeof (FSSchedule.branchID), typeof (FSSchedule.branchLocationID), DescriptionField = typeof (FSEquipment.serialNumber))]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new Type[] {typeof (FSEquipment.status)})]
  [PXForeignReference(typeof (Field<FSScheduleDet.SMequipmentID>.IsRelatedTo<FSEquipment.SMequipmentID>))]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorComponentIDByFSEquipmentComponent(typeof (FSScheduleDet.SMequipmentID))]
  public virtual int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Line Nbr.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorEquipmentLineRef(typeof (FSScheduleDet.SMequipmentID), typeof (FSScheduleDet.componentID))]
  public virtual int? EquipmentLineRef { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSSchedule.projectID))]
  [PXForeignReference(typeof (FSScheduleDet.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [PXFormula(typeof (Default<FSScheduleDet.inventoryID, FSScheduleDet.lineType>))]
  [PXDefault(typeof (Switch<Case<Where<FSScheduleDet.lineType, NotEqual<ListField_LineType_UnifyTabs.Comment>, And<FSScheduleDet.lineType, NotEqual<ListField_LineType_UnifyTabs.Instruction>>>, Current<FSSchedule.dfltProjectTaskID>>>))]
  [PXUIEnabled(typeof (Where<FSScheduleDet.lineType, NotEqual<ListField_LineType_UnifyTabs.Comment>, And<FSScheduleDet.lineType, NotEqual<ListField_LineType_UnifyTabs.Instruction>>>))]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSScheduleDet.projectID>>>))]
  [PXForeignReference(typeof (FSScheduleDet.FK.Task))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDefault]
  [PXFormula(typeof (Default<FSScheduleDet.inventoryID, FSScheduleDet.serviceTemplateID, FSScheduleDet.lineType>))]
  [SMCostCode(typeof (FSScheduleDet.skipCostCodeValidation), null, typeof (FSScheduleDet.projectTaskID), DisplayName = "Cost Code", Filterable = false, Enabled = false)]
  [PXForeignReference(typeof (FSScheduleDet.FK.CostCode))]
  public virtual int? CostCodeID { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where<FSScheduleDet.inventoryID, IsNotNull, Or<FSScheduleDet.lineType, Equal<ListField_LineType_ALL.Service_Template>>>, False, True>))]
  public virtual bool? SkipCostCodeValidation { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string EquipmentItemClass { get; set; }

  public bool IsService => this.LineType == "SERVI" || this.LineType == "NSTKI";

  public bool IsInventoryItem => this.LineType == "SLPRO";

  public class PK : PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.lineNbr>
  {
    public static FSScheduleDet Find(
      PXGraph graph,
      int? scheduleID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSScheduleDet) PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.lineNbr>.FindBy(graph, (object) scheduleID, (object) lineNbr, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.scheduleDetID>
  {
    public static FSScheduleDet Find(
      PXGraph graph,
      int? scheduleID,
      int? scheduleDetID,
      PKFindOptions options = 0)
    {
      return (FSScheduleDet) PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.scheduleDetID>.FindBy(graph, (object) scheduleID, (object) scheduleDetID, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.SMequipmentID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.inventoryID>
    {
    }

    public class ServiceTemplate : 
      PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.serviceTemplateID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.componentID>
    {
    }

    public class EquipmentComponent : 
      PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.SMequipmentID, FSScheduleDet.equipmentLineRef>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.projectID, FSScheduleDet.projectTaskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSScheduleDet>.By<FSScheduleDet.costCodeID>
    {
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.scheduleID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.lineNbr>
  {
  }

  public abstract class scheduleDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.scheduleDetID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSScheduleDet.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.inventoryID>
  {
  }

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleDet.estimatedDuration>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSScheduleDet.qty>
  {
  }

  public abstract class serviceTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleDet.serviceTemplateID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.sortOrder>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSScheduleDet.tranDesc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSScheduleDet.noteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSScheduleDet.uOM>
  {
  }

  public abstract class equipmentAction : ListField_Schedule_EquipmentAction
  {
  }

  public abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.SMequipmentID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.componentID>
  {
  }

  public abstract class equipmentLineRef : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleDet.equipmentLineRef>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSScheduleDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSScheduleDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSScheduleDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSScheduleDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSScheduleDet.Tstamp>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleDet.costCodeID>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSScheduleDet.skipCostCodeValidation>
  {
  }

  public abstract class equipmentItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleDet.equipmentItemClass>
  {
  }
}
