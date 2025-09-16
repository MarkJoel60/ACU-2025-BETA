// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSOEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSSOEmployee")]
[Serializable]
public class FSSOEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Type;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSOEmployee.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSOEmployee.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSServiceOrder))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSServiceOrder.sOID))]
  [PXUIField(DisplayName = "SOID")]
  public virtual int? SOID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  public virtual string LineRef { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  [FSSelectorServiceOrderSODetID]
  public virtual string ServiceLineRef { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Staff Member")]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Comment", Enabled = false)]
  public virtual string Comment { get; set; }

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

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [EmployeeType.List]
  public virtual string Type { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSServiceOrder.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (FSSOEmployee.curyInfoID), typeof (FSSOEmployee.unitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBCurrency(typeof (FSSOEmployee.curyInfoID), typeof (FSSOEmployee.extCost))]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Mem_Selected { get; set; }

  public class PK : 
    PrimaryKeyOf<FSSOEmployee>.By<FSSOEmployee.srvOrdType, FSSOEmployee.refNbr, FSSOEmployee.lineNbr>
  {
    public static FSSOEmployee Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSSOEmployee) PrimaryKeyOf<FSSOEmployee>.By<FSSOEmployee.srvOrdType, FSSOEmployee.refNbr, FSSOEmployee.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSOEmployee>.By<FSSOEmployee.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSSOEmployee>.By<FSSOEmployee.srvOrdType, FSSOEmployee.refNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSSOEmployee>.By<FSSOEmployee.employeeID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOEmployee.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOEmployee.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOEmployee.lineNbr>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOEmployee.sOID>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOEmployee.lineRef>
  {
  }

  public abstract class serviceLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSOEmployee.serviceLineRef>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSOEmployee.employeeID>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOEmployee.comment>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSOEmployee.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSOEmployee.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSOEmployee.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSOEmployee.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSOEmployee.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSOEmployee.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSOEmployee.Tstamp>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSOEmployee.type>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSOEmployee.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSOEmployee.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSOEmployee.unitCost>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSOEmployee.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSOEmployee.extCost>
  {
  }

  public abstract class mem_Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSOEmployee.mem_Selected>
  {
  }
}
