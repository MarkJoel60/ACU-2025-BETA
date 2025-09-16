// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.DropShipLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.Common.DAC;

/// <summary>
/// Designed for reconciliation of drop-ship details between POLine and SOLine.
/// </summary>
[PXCacheName("Drop-Ship Link")]
public class DropShipLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (DropShipLink.FK.SOLine))]
  [PXDefault]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  public virtual 
  #nullable disable
  string SOOrderType { get; set; }

  [PXDBDefault(typeof (PX.Objects.SO.SOOrder.orderNbr))]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string SOOrderNbr { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public virtual int? SOLineNbr { get; set; }

  [PXParent(typeof (DropShipLink.FK.POLine))]
  [PXDefault]
  [PX.Objects.PO.POOrderType.List]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  public virtual string POOrderType { get; set; }

  [PXDBDefault(typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual string POOrderNbr { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public virtual int? POLineNbr { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? Active { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? InReceipt { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? SOCompleted { get; set; }

  [PXDefault]
  [PXDBInt]
  public virtual int? SOInventoryID { get; set; }

  [PXDefault]
  [PXDBInt]
  public virtual int? SOSiteID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6)]
  public virtual Decimal? SOBaseOrderQty { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? POCompleted { get; set; }

  [PXDefault]
  [PXDBInt]
  public virtual int? POInventoryID { get; set; }

  [PXDefault]
  [PXDBInt]
  public virtual int? POSiteID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6)]
  public virtual Decimal? POBaseOrderQty { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6)]
  public virtual Decimal? BaseReceivedQty { get; set; }

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
    PrimaryKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr, DropShipLink.sOLineNbr, DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>
  {
    public static DropShipLink Find(
      PXGraph graph,
      string sOOrderType,
      string sOOrderNbr,
      int? sOLineNbr,
      string pOOrderType,
      string pOOrderNbr,
      int? pOLineNbr,
      PKFindOptions options = 0)
    {
      return (DropShipLink) PrimaryKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr, DropShipLink.sOLineNbr, DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>.FindBy(graph, (object) sOOrderType, (object) sOOrderNbr, (object) sOLineNbr, (object) pOOrderType, (object) pOOrderNbr, (object) pOLineNbr, options);
    }
  }

  public class UK
  {
    public class ByPOLine : 
      PrimaryKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>
    {
      public static DropShipLink Find(
        PXGraph graph,
        string pOOrderType,
        string pOOrderNbr,
        int? pOLineNbr,
        PKFindOptions options = 0)
      {
        return (DropShipLink) PrimaryKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>.FindBy(graph, (object) pOOrderType, (object) pOOrderNbr, (object) pOLineNbr, options);
      }

      public static DropShipLink FindDirty(
        PXGraph graph,
        string pOOrderType,
        string pOOrderNbr,
        int? pOLineNbr)
      {
        return PXResultset<DropShipLink>.op_Implicit(PXSelectBase<DropShipLink, PXSelect<DropShipLink, Where<DropShipLink.pOOrderType, Equal<Required<DropShipLink.pOOrderType>>, And<DropShipLink.pOOrderNbr, Equal<Required<DropShipLink.pOOrderNbr>>, And<DropShipLink.pOLineNbr, Equal<Required<DropShipLink.pOLineNbr>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
        {
          (object) pOOrderType,
          (object) pOOrderNbr,
          (object) pOLineNbr
        }));
      }
    }

    public class BySOLine : 
      PrimaryKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr, DropShipLink.sOLineNbr>
    {
      public static DropShipLink Find(
        PXGraph graph,
        string sOOrderType,
        string sOOrderNbr,
        int? sOLineNbr,
        PKFindOptions options = 0)
      {
        return (DropShipLink) PrimaryKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr, DropShipLink.sOLineNbr>.FindBy(graph, (object) sOOrderType, (object) sOOrderNbr, (object) sOLineNbr, options);
      }

      public static DropShipLink FindDirty(
        PXGraph graph,
        string sOOrderType,
        string sOOrderNbr,
        int? sOLineNbr)
      {
        return PXResultset<DropShipLink>.op_Implicit(PXSelectBase<DropShipLink, PXSelect<DropShipLink, Where<DropShipLink.sOOrderType, Equal<Required<DropShipLink.sOOrderType>>, And<DropShipLink.sOOrderNbr, Equal<Required<DropShipLink.sOOrderNbr>>, And<DropShipLink.sOLineNbr, Equal<Required<DropShipLink.sOLineNbr>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
        {
          (object) sOOrderType,
          (object) sOOrderNbr,
          (object) sOLineNbr
        }));
      }
    }
  }

  public static class FK
  {
    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr>
    {
    }

    public class SupplyPOLine : 
      PrimaryKeyOf<PX.Objects.SO.SupplyPOLine>.By<PX.Objects.SO.SupplyPOLine.orderType, PX.Objects.SO.SupplyPOLine.orderNbr, PX.Objects.SO.SupplyPOLine.lineNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr, DropShipLink.pOLineNbr>
    {
    }

    public class SupplyPOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SupplyPOOrder>.By<PX.Objects.SO.SupplyPOOrder.orderType, PX.Objects.SO.SupplyPOOrder.orderNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.pOOrderType, DropShipLink.pOOrderNbr>
    {
    }

    public class SOLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr, DropShipLink.sOLineNbr>
    {
    }

    public class DemandSOOrder : 
      PrimaryKeyOf<PX.Objects.PO.DemandSOOrder>.By<PX.Objects.PO.DemandSOOrder.orderType, PX.Objects.PO.DemandSOOrder.orderNbr>.ForeignKeyOf<DropShipLink>.By<DropShipLink.sOOrderType, DropShipLink.sOOrderNbr>
    {
    }
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipLink.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipLink.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.sOLineNbr>
  {
  }

  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipLink.pOOrderType>
  {
  }

  public abstract class pOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipLink.pOOrderNbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.pOLineNbr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipLink.active>
  {
  }

  public abstract class inReceipt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipLink.inReceipt>
  {
  }

  public abstract class soCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipLink.soCompleted>
  {
  }

  public abstract class soInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.soInventoryID>
  {
  }

  public abstract class soSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.soSiteID>
  {
  }

  public abstract class soBaseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DropShipLink.soBaseOrderQty>
  {
  }

  public abstract class poCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipLink.poCompleted>
  {
  }

  public abstract class poInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.poInventoryID>
  {
  }

  public abstract class poSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipLink.poSiteID>
  {
  }

  public abstract class poBaseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DropShipLink.poBaseOrderQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DropShipLink.baseReceivedQty>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DropShipLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DropShipLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DropShipLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DropShipLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DropShipLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DropShipLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DropShipLink.Tstamp>
  {
  }
}
