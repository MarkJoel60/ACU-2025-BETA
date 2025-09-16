// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCloneAppointmentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSCloneAppointmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true)]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.active, Equal<True>>>), DescriptionField = typeof (FSSrvOrdType.descr))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<FSServiceOrder.customerID>>>>, Where<FSAppointment.srvOrdType, Equal<Current<FSCloneAppointmentFilter.srvOrdType>>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (FSServiceOrder.refNbr), typeof (BAccount.acctName), typeof (FSServiceOrder.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin), typeof (FSAppointment.actualDateTimeBegin)})]
  public virtual string RefNbr { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Appointment Start Time", Required = true)]
  [PXDefault(typeof (Current<FSAppointment.scheduledDateTimeBegin>))]
  [PXFormula(typeof (Default<FSCloneAppointmentFilter.srvOrdType, FSCloneAppointmentFilter.refNbr>))]
  public virtual DateTime? ScheduledStartTime { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideApptDuration { get; set; }

  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Appointment Duration")]
  [PXUIEnabled(typeof (Where<FSCloneAppointmentFilter.overrideApptDuration, Equal<True>>))]
  [PXDefault(typeof (Current<FSAppointment.scheduledDuration>))]
  [PXFormula(typeof (Default<FSCloneAppointmentFilter.srvOrdType, FSCloneAppointmentFilter.refNbr, FSCloneAppointmentFilter.overrideApptDuration>))]
  public virtual int? ApptDuration { get; set; }

  [PXString(2, IsFixed = true)]
  [ListField.CloningType_CloneAppointment.List]
  [PXDefault("SI")]
  [PXUIField(DisplayName = "Number of Appointments")]
  public virtual string CloningType { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Date", Required = true)]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.single>>))]
  public virtual DateTime? SingleGenerationDate { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "From Date", Required = true)]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual DateTime? MultGenerationFromDate { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "To Date", Required = true)]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual DateTime? MultGenerationToDate { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Monday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnMonday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Tuesday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnTuesday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Wednesday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnWednesday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Thursday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnThursday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Friday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnFriday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Saturday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnSaturday { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Sunday")]
  [PXUIVisible(typeof (Where<FSCloneAppointmentFilter.cloningType, Equal<ListField.CloningType_CloneAppointment.multiple>>))]
  public virtual bool? ActiveOnSunday { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCloneAppointmentFilter.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSCloneAppointmentFilter.refNbr>
  {
  }

  public abstract class scheduledStartTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCloneAppointmentFilter.scheduledStartTime>
  {
  }

  public abstract class overrideApptDuration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.overrideApptDuration>
  {
  }

  public abstract class apptDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCloneAppointmentFilter.apptDuration>
  {
  }

  public abstract class cloningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCloneAppointmentFilter.cloningType>
  {
    public abstract class Values : ListField.CloningType_CloneAppointment
    {
    }
  }

  public abstract class singleGenerationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCloneAppointmentFilter.singleGenerationDate>
  {
  }

  public abstract class multGenerationFromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCloneAppointmentFilter.multGenerationFromDate>
  {
  }

  public abstract class multGenerationToDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCloneAppointmentFilter.multGenerationToDate>
  {
  }

  public abstract class activeOnMonday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnMonday>
  {
  }

  public abstract class activeOnTuesday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnTuesday>
  {
  }

  public abstract class activeOnWednesday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnWednesday>
  {
  }

  public abstract class activeOnThursday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnThursday>
  {
  }

  public abstract class activeOnFriday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnFriday>
  {
  }

  public abstract class activeOnSaturday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnSaturday>
  {
  }

  public abstract class activeOnSunday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSCloneAppointmentFilter.activeOnSunday>
  {
  }
}
