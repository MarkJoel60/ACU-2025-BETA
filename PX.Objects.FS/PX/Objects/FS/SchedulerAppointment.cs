// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerAppointment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (Select2<FSAppointment, InnerJoin<FSAppointmentStatusColor, On<BqlOperand<FSAppointmentStatusColor.statusID, IBqlString>.IsEqual<FSAppointment.status>>>>))]
[PXCacheName("Appointment")]
[Serializable]
public class SchedulerAppointment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(BqlField = typeof (FSAppointment.appointmentID))]
  public virtual int? AppointmentID { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = true, Enabled = true)]
  [PXFieldDescription]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceOrder.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>>>, Where2<Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA", BqlField = typeof (FSAppointment.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new System.Type[] {})]
  [PXFieldDescription]
  public virtual string SrvOrdType { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.sOID))]
  public virtual int? SOID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSAppointment.status))]
  [ListField.AppointmentStatus.List]
  [PXUIField(DisplayName = "Appointment Status", Enabled = false)]
  public virtual string Status { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.closed))]
  [PXUIField(DisplayName = "Closed")]
  public virtual bool? Closed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.canceled))]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? Canceled { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.completed))]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? Completed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.billed))]
  [PXUIField(DisplayName = "Billed")]
  public virtual bool? Billed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.confirmed))]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointment.validatedByDispatcher))]
  [PXUIField(DisplayName = "Validated by Dispatcher")]
  public virtual bool? ValidatedByDispatcher { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Start Date", DisplayNameTime = "Start Time", BqlField = typeof (FSAppointment.scheduledDateTimeBegin))]
  [PXUIField(DisplayName = "Scheduled Start Date")]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time", BqlField = typeof (FSAppointment.scheduledDateTimeEnd))]
  [PXUIField(DisplayName = "Scheduled End Date")]
  public virtual DateTime? ScheduledDateTimeEnd { get; set; }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true, BqlField = typeof (FSAppointment.longDescr))]
  [PXUIField(DisplayName = "Other")]
  public virtual string LongDescr { get; set; }

  [PXDBDecimal(6, BqlField = typeof (FSAppointment.mapLatitude))]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? MapLatitude { get; set; }

  [PXDBDecimal(6, BqlField = typeof (FSAppointment.mapLongitude))]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? MapLongitude { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
  public virtual int? EstimatedDurationTotal { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.staffCntr))]
  [PXUIField(DisplayName = "Multiple Staff Members", Enabled = false, Visible = true)]
  public virtual int? StaffCntr { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (FSAppointment.createdDateTime))]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointmentStatusColor.isVisible))]
  public virtual bool? IsVisible { get; set; }

  [PXDBString(7, IsUnicode = true, InputMask = "C<AAAAAA", BqlField = typeof (FSAppointmentStatusColor.bandColor))]
  public virtual string BandColor { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Locked", Enabled = false, Visible = true)]
  [PXFormula(typeof (IIf<Where<SchedulerAppointment.closed, Equal<True>, Or<SchedulerAppointment.completed, Equal<True>, Or<SchedulerAppointment.canceled, Equal<True>, Or<SchedulerAppointment.billed, Equal<True>>>>>, True, False>))]
  public virtual bool? Locked { get; set; }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointment.appointmentID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerAppointment.refNbr>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerAppointment.srvOrdType>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerAppointment.sOID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerAppointment.status>
  {
    public abstract class Values : ListField.AppointmentStatus
    {
    }
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.closed>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.canceled>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.completed>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.billed>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.confirmed>
  {
  }

  public abstract class validatedByDispatcher : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerAppointment.validatedByDispatcher>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerAppointment.scheduledDateTimeBegin>
  {
  }

  public abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerAppointment.scheduledDateTimeEnd>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerAppointment.docDesc>
  {
  }

  public abstract class longDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerAppointment.longDescr>
  {
  }

  public abstract class mapLatitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SchedulerAppointment.mapLatitude>
  {
  }

  public abstract class mapLongitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SchedulerAppointment.mapLongitude>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointment.estimatedDurationTotal>
  {
  }

  public abstract class staffCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerAppointment.staffCntr>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerAppointment.createdDateTime>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.isVisible>
  {
  }

  public abstract class bandColor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerAppointment.bandColor>
  {
  }

  public abstract class locked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SchedulerAppointment.locked>
  {
  }
}
