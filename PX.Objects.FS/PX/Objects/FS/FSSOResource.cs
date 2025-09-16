// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSOResource
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

[PXCacheName("FSSOResource")]
[Serializable]
public class FSSOResource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSOResource.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSOResource.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [FSSelectorServiceOrderResourceEquipment]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new Type[] {typeof (FSEquipment.status)})]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXDefault]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSServiceOrder.sOID))]
  [PXUIField(DisplayName = "SOID")]
  public virtual int? SOID { get; set; }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Comment", Enabled = false)]
  public virtual string Comment { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Quantity", Visible = false, Enabled = false)]
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

  public class PK : 
    PrimaryKeyOf<FSSOResource>.By<FSSOResource.srvOrdType, FSSOResource.refNbr, FSSOResource.SMequipmentID>
  {
    public static FSSOResource Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? SMequipmentID,
      PKFindOptions options = 0)
    {
      return (FSSOResource) PrimaryKeyOf<FSSOResource>.By<FSSOResource.srvOrdType, FSSOResource.refNbr, FSSOResource.SMequipmentID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) SMequipmentID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSOResource>.By<FSSOResource.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSSOResource>.By<FSSOResource.srvOrdType, FSSOResource.refNbr>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSSOResource>.By<FSSOResource.SMequipmentID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOResource.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOResource.refNbr>
  {
  }

  public abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOResource.SMequipmentID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOResource.sOID>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOResource.comment>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOResource.qty>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSOResource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSOResource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSOResource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSOResource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSOResource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSOResource.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSOResource.Tstamp>
  {
  }
}
