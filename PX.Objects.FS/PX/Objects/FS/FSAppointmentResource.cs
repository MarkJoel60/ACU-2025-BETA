// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentResource
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSAppointmentResource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentResource.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentResource.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.")]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt(IsKey = true)]
  [FSSelectorServiceOrderResourceEquipment]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new Type[] {typeof (FSEquipment.status)})]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXDefault]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(250)]
  [PXUIField(DisplayName = "Comment", Enabled = false)]
  public virtual string Comment { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  [PXDefault(1)]
  public virtual int? Qty { get; set; }

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

  [PXInt]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID, Where<FSEquipment.resourceEquipment, Equal<True>>>), SubstituteKey = typeof (FSEquipment.refNbr), DescriptionField = typeof (FSEquipment.descr))]
  public virtual int? SMEquipmentIDReport { get; set; }

  public class PK : 
    PrimaryKeyOf<FSAppointmentResource>.By<FSAppointmentResource.srvOrdType, FSAppointmentResource.refNbr, FSAppointmentResource.SMequipmentID>
  {
    public static FSAppointmentResource Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? SMequipmentID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentResource) PrimaryKeyOf<FSAppointmentResource>.By<FSAppointmentResource.srvOrdType, FSAppointmentResource.refNbr, FSAppointmentResource.SMequipmentID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) SMequipmentID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentResource>.By<FSAppointmentResource.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentResource>.By<FSAppointmentResource.srvOrdType, FSAppointmentResource.refNbr>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSAppointmentResource>.By<FSAppointmentResource.SMequipmentID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentResource.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentResource.refNbr>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentResource.appointmentID>
  {
  }

  public abstract class SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentResource.SMequipmentID>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentResource.comment>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentResource.qty>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentResource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentResource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentResource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentResource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentResource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentResource.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentResource.Tstamp>
  {
  }

  public abstract class sMEquipmentIDReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentResource.sMEquipmentIDReport>
  {
  }
}
