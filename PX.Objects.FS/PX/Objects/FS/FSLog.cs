// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLog
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Log")]
[Serializable]
public class FSLog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? LogID { get; set; }

  [PXDBString(4, IsFixed = true)]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(20, IsUnicode = true)]
  public virtual string DocRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? DocID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Log Type")]
  [ListField_Log_ItemType.List]
  public virtual string ItemType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Status_Log.ListAtrribute]
  [PXDefault("P")]
  [PXUIField(DisplayName = "Log Line Status")]
  public virtual string Status { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start Time")]
  public virtual DateTime? DateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Time")]
  public virtual DateTime? DateTimeEnd { get; set; }

  [FSDBTimeSpanLong]
  [PXFormula(typeof (IsNull<Sub<FSLog.dateTimeEnd, FSLog.dateTimeBegin>, SharedClasses.int_0>))]
  [PXUIField(DisplayName = "Duration")]
  public virtual int? TimeDuration { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Approved", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public virtual bool? ApprovedTime { get; set; }

  [SMCostCode(typeof (FSLog.skipCostCodeValidation), null, typeof (FSLog.projectTaskID))]
  [PXForeignReference(typeof (FSLog.FK.CostCode))]
  public virtual int? CostCodeID { get; set; }

  [PXDBCurrency(typeof (FSLog.curyInfoID), typeof (FSLog.extCost))]
  [PXUIField(Enabled = false, Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (FSLog.curyInfoID), typeof (FSLog.unitCost))]
  [PXUIField(Visible = false, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  public virtual string DetLineRef { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public virtual string EarningType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Staff Member")]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  public virtual int? BAccountID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PX.Objects.CR.BAccountType.List]
  public virtual string BAccountType { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manage Time Manually", Visible = false)]
  public virtual bool? KeepDateTimes { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Labor Item ID")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXForeignReference(typeof (FSLog.FK.LaborItem))]
  public virtual int? LaborItemID { get; set; }

  [FSDBLineRef(typeof (FSLog.lineNbr))]
  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  public virtual string LineRef { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false, FieldClass = "PROJECT")]
  [PXForeignReference(typeof (FSLog.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSLog.projectID>>>))]
  [PXForeignReference(typeof (FSLog.FK.Task))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Time")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  public virtual bool? TrackTime { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Add to Actual Duration")]
  public virtual bool? TrackOnService { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCC")]
  [PXUIField(DisplayName = "Time Card Ref. Nbr.", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>))]
  [PXSelector(typeof (Search<EPTimeCard.timeCardCD>), new System.Type[] {typeof (EPTimeCard.timeCardCD), typeof (EPTimeCard.employeeID), typeof (EPTimeCard.weekDescription), typeof (EPTimeCard.status)})]
  public virtual string TimeCardCD { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXWorkgroupSelector]
  public virtual int? WorkgroupID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? IsBillable { get; set; }

  [FSDBTimeSpanLong]
  [PXUIField(DisplayName = "Billable Time")]
  [PXDefault(0)]
  public virtual int? BillableTimeDuration { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billable Quantity", Enabled = false)]
  public virtual Decimal? BillableQty { get; set; }

  [PXDBCurrency(typeof (FSLog.curyInfoID), typeof (FSLog.billableTranAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billable Amount", Enabled = false)]
  public virtual Decimal? CuryBillableTranAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Amount", Enabled = false)]
  public virtual Decimal? BillableTranAmount { get; set; }

  /// <summary>Approval status of the related PMTimeActivity.</summary>
  [PXString(2)]
  [PXDBScalar(typeof (Search<PMTimeActivity.approvalStatus, Where<FSxPMTimeActivity.appointmentID, Equal<FSLog.docID>, And<FSxPMTimeActivity.logLineNbr, Equal<FSLog.lineNbr>>>>))]
  public virtual string TimeActivityStatus { get; set; }

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

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  public virtual bool? SkipCostCodeValidation { get; set; }

  [PXInt]
  [PXFormula(typeof (FSLog.timeDuration))]
  public virtual int? TimeDurationReport { get; set; }

  public class PK : PrimaryKeyOf<FSLog>.By<FSLog.logID>
  {
    public static FSLog Find(PXGraph graph, int? logID, PKFindOptions options = 0)
    {
      return (FSLog) PrimaryKeyOf<FSLog>.By<FSLog.logID>.FindBy(graph, (object) logID, options);
    }
  }

  public static class FK
  {
    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSLog>.By<FSLog.bAccountID>
    {
    }

    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<FSLog>.By<FSLog.earningType>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSLog>.By<FSLog.costCodeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSLog>.By<FSLog.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSLog>.By<FSLog.projectID, FSLog.projectTaskID>
    {
    }

    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSLog>.By<FSLog.laborItemID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSLog>.By<FSLog.curyInfoID>
    {
    }

    public class WorkGorupID : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<FSLog>.By<FSLog.workgroupID>
    {
    }

    public class TimeCard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<FSLog>.By<FSLog.timeCardCD>
    {
    }
  }

  public abstract class logID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.logID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.docType>
  {
  }

  public abstract class docRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.docRefNbr>
  {
  }

  public abstract class docID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.docID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.lineNbr>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.itemType>
  {
    public abstract class Values : ListField_Log_ItemType
    {
    }
  }

  public abstract class status : ListField_Status_Log
  {
  }

  public abstract class dateTimeBegin : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSLog.dateTimeBegin>
  {
  }

  public abstract class dateTimeEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSLog.dateTimeEnd>
  {
  }

  public abstract class timeDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.timeDuration>
  {
  }

  public abstract class approvedTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLog.approvedTime>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.costCodeID>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSLog.curyExtCost>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSLog.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSLog.curyUnitCost>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.descr>
  {
  }

  public abstract class detLineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.detLineRef>
  {
  }

  public abstract class earningType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.earningType>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.bAccountID>
  {
  }

  public abstract class bAccountType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.bAccountType>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSLog.extCost>
  {
  }

  public abstract class keepDateTimes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLog.keepDateTimes>
  {
  }

  public abstract class laborItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.laborItemID>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.lineRef>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.projectTaskID>
  {
  }

  public abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLog.trackTime>
  {
  }

  public abstract class trackOnService : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLog.trackOnService>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLog.timeCardCD>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSLog.unitCost>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.workgroupID>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLog.isBillable>
  {
  }

  public abstract class billableTimeDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSLog.billableTimeDuration>
  {
  }

  public abstract class billableQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSLog.billableQty>
  {
  }

  public abstract class curyBillableTranAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSLog.curyBillableTranAmount>
  {
  }

  public abstract class billableTranAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSLog.billableTranAmount>
  {
  }

  public abstract class timeActivityStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLog.timeActivityStatus>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLog.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLog.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLog.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLog.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLog.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLog.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLog.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSLog.Tstamp>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSLog.skipCostCodeValidation>
  {
  }

  public abstract class timeDurationReport : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLog.timeDurationReport>
  {
  }
}
