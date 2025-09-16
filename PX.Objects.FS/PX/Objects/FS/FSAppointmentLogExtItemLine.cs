// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentLogExtItemLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSAppointmentLog, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<FSAppointmentLog.bAccountID>>, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<FSAppointmentLog.bAccountID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.appointmentID, Equal<FSAppointmentLog.docID>, And<FSAppointmentDet.lineRef, Equal<FSAppointmentLog.detLineRef>>>>>>>))]
[PXBreakInheritance]
[Serializable]
public class FSAppointmentLogExtItemLine : FSAppointmentLog
{
  [PXDBInt(IsKey = true, BqlField = typeof (FSAppointmentLog.docID))]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.", Visible = false, Enabled = false)]
  public override int? DocID { get; set; }

  [PXDBString(3, IsFixed = true, IsKey = true, BqlField = typeof (FSAppointmentLog.lineRef))]
  [PXUIField]
  public override 
  #nullable disable
  string LineRef { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (FSAppointmentLog.detLineRef))]
  [PXDefault]
  [FSSelectorAppointmentSODetID]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  public override string DetLineRef { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (FSAppointmentLog.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXUIVisible(typeof (Where2<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Complete>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Pause>>>>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.Service>>>))]
  public override string Descr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentLog.bAccountID))]
  [PXUIField(DisplayName = "Staff Member")]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.me>, Equal<False>>))]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  public override int? BAccountID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSAppointmentLog.status))]
  [PXUIField(DisplayName = "Log Line Status")]
  [ListField_Status_Log.ListAtrribute]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Complete>>))]
  public override string Status { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSAppointmentLog.itemType))]
  [ListField_Log_ItemType.List]
  public override string ItemType { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, BqlField = typeof (FSAppointmentLog.dateTimeBegin), PreserveTime = true, DisplayNameDate = "Start Date", DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start Time", Enabled = false)]
  public override DateTime? DateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, BqlField = typeof (FSAppointmentLog.dateTimeEnd), PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Time", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>>))]
  public override DateTime? DateTimeEnd { get; set; }

  [FSDBTimeSpanLong(BqlField = typeof (FSAppointmentLog.timeDuration))]
  [PXUIField(DisplayName = "Duration", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>>))]
  public override int? TimeDuration { get; set; }

  [PXBool]
  [PXFormula(typeof (Where<FSAppointmentLog.itemType, Equal<ListField_Log_ItemType.travel>>))]
  [PXUIField(DisplayName = "Travel", Enabled = false)]
  [PXUIVisible(typeof (Where2<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Complete>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Pause>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>>>>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.Travel>>>))]
  public override bool? Travel { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.inventoryID))]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  [PXUIVisible(typeof (Where2<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Complete>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>, Or<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Pause>>>>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.Service>>>))]
  public virtual int? InventoryID { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.EP.EPEmployee.userID))]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual Guid? UserID { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentLogExtItemLine.userID, Equal<Current<AccessInfo.userID>>>, True>, False>))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.docType, FSAppointmentLogExtItemLine.docRefNbr, FSAppointmentLog.lineNbr>
  {
    public static FSAppointmentLogExtItemLine Find(
      PXGraph graph,
      string docType,
      string docRefNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentLogExtItemLine) PrimaryKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.docType, FSAppointmentLogExtItemLine.docRefNbr, FSAppointmentLog.lineNbr>.FindBy(graph, (object) docType, (object) docRefNbr, (object) lineNbr, options);
    }
  }

  public new class UK : PrimaryKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLog.logID>
  {
    public static FSAppointmentLogExtItemLine Find(
      PXGraph graph,
      int? logID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentLogExtItemLine) PrimaryKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLog.logID>.FindBy(graph, (object) logID, options);
    }
  }

  public new static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.docType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.docType, FSAppointmentLogExtItemLine.docRefNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.bAccountID>
    {
    }

    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.earningType>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.costCodeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.projectID, FSAppointmentLogExtItemLine.projectTaskID>
    {
    }

    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.laborItemID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.curyInfoID>
    {
    }

    public class WorkGorupID : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.workgroupID>
    {
    }

    public class TimeCard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.timeCardCD>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<FSAppointmentLogExtItemLine>.By<FSAppointmentLogExtItemLine.userID>
    {
    }
  }

  public new abstract class docID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentLogExtItemLine.docID>
  {
  }

  public new abstract class lineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.lineRef>
  {
  }

  public new abstract class detLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.detLineRef>
  {
  }

  public new abstract class descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.descr>
  {
  }

  public new abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.bAccountID>
  {
  }

  public new abstract class status : ListField_Status_Log
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentLog.itemType>
  {
    public abstract class Values : ListField_Log_ItemType
    {
    }
  }

  public new abstract class dateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.dateTimeBegin>
  {
  }

  public new abstract class dateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.dateTimeEnd>
  {
  }

  public new abstract class timeDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.timeDuration>
  {
  }

  public new abstract class travel : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.travel>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.inventoryID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentLogExtItemLine.userID>
  {
  }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.selected>
  {
  }

  public new abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.docType>
  {
  }

  public new abstract class docRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.docRefNbr>
  {
  }

  public new abstract class earningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.earningType>
  {
  }

  public new abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.costCodeID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.projectTaskID>
  {
  }

  public new abstract class laborItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.laborItemID>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.curyInfoID>
  {
  }

  public new abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.workgroupID>
  {
  }

  public new abstract class timeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentLogExtItemLine.timeCardCD>
  {
  }
}
