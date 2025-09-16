// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentScheduleBoard
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, LeftJoin<FSWFStage, On<FSWFStage.wFStageID, Equal<FSAppointment.wFStageID>>, LeftJoin<FSBranchLocation, On<FSBranchLocation.branchLocationID, Equal<FSServiceOrder.branchLocationID>>>>>>>>>))]
[Serializable]
public class FSAppointmentScheduleBoard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _ContactName;
  private string _ContactPhone;

  [PXDBString(4, IsFixed = true, IsKey = true, IsUnicode = true, BqlField = typeof (FSServiceOrder.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string SrvOrdType { get; set; }

  [PXDBIdentity(BqlField = typeof (FSAppointment.appointmentID))]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.sOID))]
  public virtual int? SOID { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSServiceOrder.refNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  public virtual string SORefNbr { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.closed))]
  [PXUIField(DisplayName = "Closed")]
  public virtual bool? Closed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.canceled))]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? Canceled { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.billed))]
  [PXUIField(DisplayName = "Billed")]
  public virtual bool? Billed { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSAppointment.status))]
  [ListField.AppointmentStatus.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.confirmed))]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.validatedByDispatcher))]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? ValidatedByDispatcher { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchID))]
  [PXUIField(DisplayName = "Branch ID")]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location ID")]
  public virtual int? BranchLocationID { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, BqlField = typeof (FSBranchLocation.descr), IsProjection = true)]
  [PXUIField(DisplayName = "Branch Location Desc")]
  public virtual string BranchLocationDesc { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Start Date", DisplayNameTime = "Start Time", BqlField = typeof (FSAppointment.scheduledDateTimeBegin))]
  [PXUIField]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time", BqlField = typeof (FSAppointment.scheduledDateTimeEnd))]
  [PXUIField]
  public virtual DateTime? ScheduledDateTimeEnd { get; set; }

  [PXDBString(50, IsFixed = true, BqlField = typeof (FSContact.attention))]
  [PXUIField(DisplayName = "Contact Name")]
  public virtual string ContactName
  {
    get => this._ContactName?.Trim();
    set => this._ContactName = value;
  }

  [PXDBString(50, IsFixed = true, BqlField = typeof (FSContact.phone1))]
  [PXUIField(DisplayName = "Contact Phone")]
  public virtual string ContactPhone
  {
    get => this._ContactPhone?.Trim();
    set => this._ContactPhone = value;
  }

  [PXDBString(50, IsFixed = true, BqlField = typeof (FSContact.email))]
  [PXUIField(DisplayName = "Contact Email")]
  public virtual string ContactEmail { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.Customer.bAccountID))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(60, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXUIField(DisplayName = "Customer Name")]
  public virtual string CustomerName { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.Customer.defLocationID))]
  [PXUIField(DisplayName = "Customer Location")]
  public virtual int? CustomerLocation { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSWFStage.wFStageCD))]
  [PXUIField(DisplayName = "WFStageCD")]
  public virtual string WFStageCD { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "LocationDesc")]
  public virtual string LocationDesc { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.roomID))]
  [PXUIField(DisplayName = "Room")]
  public virtual string RoomID { get; set; }

  [PXString]
  public virtual string RoomDesc { get; set; }

  [PXString]
  public virtual string FirstServiceDesc { get; set; }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "DocDesc")]
  public virtual string DocDesc { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Location.defAddressID))]
  public virtual int? AddressID { get; set; }

  [PXString]
  public virtual string StatusUI { get; set; }

  [PXString]
  public virtual string CustomID { get; set; }

  [PXString]
  public virtual string CustomDateID { get; set; }

  [PXString]
  public virtual string CustomRoomID
  {
    get
    {
      return this.BranchLocationID.HasValue && !string.IsNullOrEmpty(this.RoomID) ? $"{this.BranchLocationID.ToString()}-{this.RoomID}" : string.Empty;
    }
  }

  [PXInt]
  public virtual string AppointmentCustomID { get; set; }

  [PXString]
  public virtual string CustomDateTimeStart
  {
    get
    {
      return this.ScheduledDateTimeBegin.HasValue ? this.ScheduledDateTimeBegin.ToString() : string.Empty;
    }
  }

  [PXString]
  public virtual string CustomDateTimeEnd
  {
    get => this.ScheduledDateTimeEnd.HasValue ? this.ScheduledDateTimeEnd.ToString() : string.Empty;
  }

  [PXInt]
  public virtual int? EmployeeID { get; set; }

  [PXInt]
  public virtual int? OldEmployeeID { get; set; }

  [PXStringList]
  public virtual List<string> EmployeeList { get; set; }

  [PXInt]
  public virtual int? EmployeeCount { get; set; }

  [PXInt]
  public virtual int? ServiceCount { get; set; }

  [PXStringList]
  public virtual List<string> ServiceList { get; set; }

  [PXDBInt]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSAppointment.postingStatusAPARSO))]
  public virtual string PostingStatusAPARSO { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (FSAppointment.postingStatusIN))]
  public virtual string PostingStatusIN { get; set; }

  [PXDefault(false)]
  public virtual bool? CanDeleteAppointment { get; set; }

  [PXString(22, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCCCC")]
  public virtual string MemRefNbr { get; set; }

  [PXString(62, IsUnicode = true)]
  public virtual string MemAcctName { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? OpenAppointmentScreenOnError { get; set; }

  [PXBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentScheduleBoard.postingStatusAPARSO, NotEqual<ListField_Status_Posting.Posted>, And<FSAppointmentScheduleBoard.postingStatusIN, NotEqual<ListField_Status_Posting.Posted>>>, False>, True>))]
  public virtual bool? IsPosted { get; set; }

  [PXBool]
  public virtual bool? Resizable
  {
    get
    {
      return this.Closed.GetValueOrDefault() || this.Canceled.GetValueOrDefault() || this.Billed.GetValueOrDefault() ? new bool?(false) : new bool?(true);
    }
  }

  [PXBool]
  public virtual bool? Draggable
  {
    get
    {
      return this.Closed.GetValueOrDefault() || this.Canceled.GetValueOrDefault() || this.Billed.GetValueOrDefault() ? new bool?(false) : new bool?(true);
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.srvOrdType>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.appointmentID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.sOID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.refNbr>
  {
  }

  public abstract class soRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.soRefNbr>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.closed>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.canceled>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.billed>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.status>
  {
    public abstract class Values : ListField.AppointmentStatus
    {
    }
  }

  public abstract class confirmed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.confirmed>
  {
  }

  public abstract class validatedByDispatcher : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.validatedByDispatcher>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.branchLocationID>
  {
  }

  public abstract class branchLocationDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.branchLocationDesc>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.scheduledDateTimeBegin>
  {
  }

  public abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.scheduledDateTimeEnd>
  {
  }

  public abstract class contactName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.contactName>
  {
  }

  public abstract class contactPhone : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.contactPhone>
  {
  }

  public abstract class contactEmail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.contactEmail>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customerID>
  {
  }

  public abstract class customerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customerName>
  {
  }

  public abstract class customerLocation : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customerLocation>
  {
  }

  public abstract class wFStageCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.wFStageCD>
  {
  }

  public abstract class locationDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.locationDesc>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.roomID>
  {
  }

  public abstract class roomDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.roomDesc>
  {
  }

  public abstract class firstServiceDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.firstServiceDesc>
  {
  }

  public abstract class docDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.docDesc>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.addressID>
  {
  }

  public abstract class statusUI : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.statusUI>
  {
  }

  public abstract class customID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customID>
  {
  }

  public abstract class customDateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customID>
  {
  }

  public abstract class customRoomID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customRoomID>
  {
  }

  public abstract class appointmentCustomID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.appointmentCustomID>
  {
  }

  public abstract class customDateTimeStart : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customDateTimeStart>
  {
  }

  public abstract class customDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.customDateTimeEnd>
  {
  }

  public abstract class employeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.employeeID>
  {
  }

  public abstract class oldEmployeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.oldEmployeeID>
  {
  }

  public abstract class employeeList : IBqlField, IBqlOperand
  {
  }

  public abstract class employeeCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.employeeCount>
  {
  }

  public abstract class serviceCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.serviceCount>
  {
  }

  public abstract class serviceList : IBqlField, IBqlOperand
  {
  }

  public abstract class smEquipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.smEquipmentID>
  {
  }

  public abstract class postingStatusAPARSO : ListField_Status_Posting
  {
  }

  public abstract class postingStatusIN : ListField_Status_Posting
  {
  }

  public abstract class canDeleteAppointment : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.canDeleteAppointment>
  {
  }

  public abstract class memRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.memRefNbr>
  {
  }

  public abstract class memAcctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.memAcctName>
  {
  }

  public abstract class isPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentScheduleBoard.isPosted>
  {
  }

  public abstract class resizable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.resizable>
  {
  }

  public abstract class draggable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentScheduleBoard.draggable>
  {
  }
}
