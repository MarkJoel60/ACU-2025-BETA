// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractForecastDet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractForecastDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (FSContractForecast.serviceContractID))]
  public virtual int? ServiceContractID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXParent(typeof (Select<FSContractForecast, Where<FSContractForecast.forecastID, Equal<Current<FSContractForecastDet.forecastID>>, And<FSContractForecast.serviceContractID, Equal<Current<FSContractForecastDet.serviceContractID>>>>>))]
  [PXDBDefault(typeof (FSContractForecast.forecastID))]
  public virtual Guid? ForecastID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSContractForecast.lineCntr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Forecast Type")]
  public virtual 
  #nullable disable
  string ForecastDetType { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type")]
  [FSLineType.List]
  [PXDefault("SERVI")]
  public virtual string LineType { get; set; }

  [PXDBInt]
  public virtual int? ScheduleID { get; set; }

  [PXDBInt]
  public virtual int? ScheduleDetID { get; set; }

  [PXDBInt]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBInt]
  public virtual int? ContractPeriodDetID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXDefault("NONE")]
  [PXUIField(DisplayName = "Billing Rule", Enabled = false)]
  public virtual string BillingRule { get; set; }

  [PXDefault]
  [PXFormula(typeof (Default<FSContractForecastDet.lineType>))]
  [InventoryIDByLineType(typeof (FSContractForecastDet.lineType), null, Filterable = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBInt]
  public virtual int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault(TypeCode.Int32, "0")]
  public virtual int? Occurrences { get; set; }

  [INUnit(typeof (FSContractForecastDet.inventoryID), DisplayName = "UOM")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSContractForecastDet.inventoryID>>>>))]
  [PXFormula(typeof (Default<FSContractForecastDet.inventoryID>))]
  public virtual string UOM { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_EquipmentAction.ListAtrribute]
  [PXDefault("NO")]
  [PXUIField(DisplayName = "Equipment Action", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual string EquipmentAction { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Line Ref.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorEquipmentLineRefServiceOrderAppointment(typeof (FSContractForecastDet.inventoryID), typeof (FSContractForecastDet.SMequipmentID), typeof (FSContractForecastDet.componentID), typeof (FSContractForecastDet.equipmentAction))]
  public virtual int? EquipmentLineRef { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Overage Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OveragePrice { get; set; }

  [PXDefault(typeof (FSServiceContract.projectID))]
  [ProjectBase(typeof (FSServiceContract.billCustomerID), Visible = false)]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSContractForecastDet.projectID>>>))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Visible = false, Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Price per Duration")]
  [PXFormula(typeof (Mult<Mult<FSContractForecastDet.qty, FSContractForecastDet.unitPrice>, FSContractForecastDet.occurrences>))]
  public virtual Decimal? TotalPrice { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Recurrence Description")]
  public virtual string RecurrenceDesc { get; set; }

  [FSDBTimeSpanLong]
  [PXUIField(DisplayName = "Duration")]
  public virtual int? TimeDuration { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.serviceContractID>
  {
  }

  public abstract class forecastID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractForecastDet.forecastID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.lineNbr>
  {
  }

  public abstract class forecastDetType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractForecastDet.forecastDetType>
  {
    public abstract class Values : ListField_ForecastDet_Type
    {
    }
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractForecastDet.lineType>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.scheduleID>
  {
  }

  public abstract class scheduleDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.scheduleDetID>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.contractPeriodID>
  {
  }

  public abstract class contractPeriodDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.contractPeriodDetID>
  {
  }

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.inventoryID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.componentID>
  {
  }

  public abstract class occurrences : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.occurrences>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractForecastDet.uOM>
  {
  }

  public abstract class unitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractForecastDet.unitPrice>
  {
  }

  public abstract class SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.SMequipmentID>
  {
  }

  public abstract class equipmentAction : ListField_EquipmentAction
  {
  }

  public abstract class equipmentLineRef : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.equipmentLineRef>
  {
  }

  public abstract class extPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractForecastDet.extPrice>
  {
  }

  public abstract class overagePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractForecastDet.overagePrice>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractForecastDet.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.projectTaskID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSContractForecastDet.qty>
  {
  }

  public abstract class totalPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractForecastDet.totalPrice>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSContractForecastDet.tranDesc>
  {
  }

  public abstract class recurrenceDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractForecastDet.recurrenceDesc>
  {
  }

  public abstract class timeDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractForecastDet.timeDuration>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractForecastDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractForecastDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractForecastDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractForecastDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractForecastDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractForecastDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractForecastDet.Tstamp>
  {
  }
}
