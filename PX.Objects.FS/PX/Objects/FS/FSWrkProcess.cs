// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSWrkProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSWrkProcess")]
[Serializable]
public class FSWrkProcess : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _ScheduledDateTimeBegin;
  protected DateTime? _ScheduledDateTimeEnd;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? ProcessID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Order ID")]
  public virtual int? SOID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Appointment ID")]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch ID")]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch Location ID")]
  public virtual int? BranchLocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Customer ID")]
  [PXForeignReference(typeof (FSWrkProcess.FK.Customer))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Room ID")]
  public virtual string RoomID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Scheduled Date Time Begin")]
  public virtual DateTime? ScheduledDateTimeBegin
  {
    get => this._ScheduledDateTimeBegin;
    set
    {
      this.ScheduledDateTimeBeginUTC = value;
      this._ScheduledDateTimeBegin = value;
    }
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Scheduled Date Time End")]
  public virtual DateTime? ScheduledDateTimeEnd
  {
    get => this._ScheduledDateTimeEnd;
    set
    {
      this.ScheduledDateTimeEndUTC = value;
      this._ScheduledDateTimeEnd = value;
    }
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Ref. Nbr. List")]
  public virtual string LineRefList { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Employee ID List")]
  public virtual string EmployeeIDList { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Equipment ID List")]
  public virtual string EquipmentIDList { get; set; }

  [PXDBString(8, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Target Screen ID")]
  public virtual string TargetScreenID { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Extra Parameters")]
  public virtual string ExtraParms { get; set; }

  [PXDBInt]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "Scheduled Date Time Begin")]
  public virtual DateTime? ScheduledDateTimeBeginUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "Scheduled Date Time End")]
  public virtual DateTime? ScheduledDateTimeEndUTC { get; set; }

  public class PK : PrimaryKeyOf<FSWrkProcess>.By<FSWrkProcess.processID>
  {
    public static FSWrkProcess Find(PXGraph graph, int? processID, PKFindOptions options = 0)
    {
      return (FSWrkProcess) PrimaryKeyOf<FSWrkProcess>.By<FSWrkProcess.processID>.FindBy(graph, (object) processID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.appointmentID>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.sOID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.customerID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.branchLocationID>
    {
    }

    public class Room : 
      PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.branchLocationID, FSWrkProcess.roomID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.smEquipmentID>
    {
    }

    public class TargetScreen : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<FSWrkProcess>.By<FSWrkProcess.targetScreenID>
    {
    }
  }

  public abstract class processID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.processID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWrkProcess.srvOrdType>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.sOID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.appointmentID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.branchID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.branchLocationID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.customerID>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWrkProcess.roomID>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkProcess.scheduledDateTimeBegin>
  {
  }

  public abstract class scheduledDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkProcess.scheduledDateTimeEnd>
  {
  }

  public abstract class lineRefList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWrkProcess.lineRefList>
  {
  }

  public abstract class employeeIDList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkProcess.employeeIDList>
  {
  }

  public abstract class equipmentIDList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkProcess.equipmentIDList>
  {
  }

  public abstract class targetScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkProcess.targetScreenID>
  {
  }

  public abstract class extraParms : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWrkProcess.extraParms>
  {
  }

  public abstract class smEquipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkProcess.smEquipmentID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSWrkProcess.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkProcess.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkProcess.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSWrkProcess.Tstamp>
  {
  }

  public abstract class scheduledDateTimeBeginUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkProcess.scheduledDateTimeBeginUTC>
  {
  }

  public abstract class scheduledDateTimeEndUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkProcess.scheduledDateTimeEndUTC>
  {
  }
}
