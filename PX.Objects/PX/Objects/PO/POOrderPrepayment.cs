// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderPrepayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Prepayment")]
public class POOrderPrepayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [POOrderType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PX.Objects.AP.APDocType.List]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.AP.APRegister.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AP.APRegister.refNbr, Where<PX.Objects.AP.APRegister.docType, Equal<Current<POOrderPrepayment.aPDocType>>>>), DirtyRead = true)]
  public virtual string APRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsRequest { get; set; }

  [PXDBLong]
  [PXDefault(typeof (Search<POOrder.curyInfoID, Where<POOrder.orderType, Equal<Current<POOrderPrepayment.orderType>>, And<POOrder.orderNbr, Equal<Current<POOrderPrepayment.orderNbr>>>>>))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (POOrderPrepayment.curyInfoID), typeof (POOrderPrepayment.appliedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Applied to Order", Enabled = false)]
  public virtual Decimal? CuryAppliedAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AppliedAmt { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PX.Objects.AP.APDocType.List]
  public virtual string PayDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Payment Ref.")]
  [PXSelector(typeof (Search<PX.Objects.AP.APRegister.refNbr, Where<PX.Objects.AP.APRegister.docType, Equal<Current<POOrderPrepayment.payDocType>>>>))]
  public virtual string PayRefNbr { get; set; }

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

  [PXString]
  public virtual string StatusText { get; set; }

  public class PK : 
    PrimaryKeyOf<POOrderPrepayment>.By<POOrderPrepayment.orderType, POOrderPrepayment.orderNbr, POOrderPrepayment.aPDocType, POOrderPrepayment.aPRefNbr>
  {
    public static POOrderPrepayment Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (POOrderPrepayment) PrimaryKeyOf<POOrderPrepayment>.By<POOrderPrepayment.orderType, POOrderPrepayment.orderNbr, POOrderPrepayment.aPDocType, POOrderPrepayment.aPRefNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderPrepayment>.By<POOrderPrepayment.orderType, POOrderPrepayment.orderNbr>
    {
    }

    public class APRegister : 
      PrimaryKeyOf<PX.Objects.AP.APRegister>.By<PX.Objects.AP.APRegister.docType, PX.Objects.AP.APRegister.refNbr>.ForeignKeyOf<POOrderPrepayment>.By<POOrderPrepayment.aPDocType, POOrderPrepayment.aPRefNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POOrderPrepayment>.By<POOrderPrepayment.curyInfoID>
    {
    }

    public class Payment : 
      PrimaryKeyOf<PX.Objects.AP.APRegister>.By<PX.Objects.AP.APRegister.docType, PX.Objects.AP.APRegister.refNbr>.ForeignKeyOf<POOrderPrepayment>.By<POOrderPrepayment.payDocType, POOrderPrepayment.payRefNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.orderNbr>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.aPRefNbr>
  {
  }

  public abstract class isRequest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrderPrepayment.isRequest>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POOrderPrepayment.curyInfoID>
  {
  }

  public abstract class curyAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderPrepayment.curyAppliedAmt>
  {
  }

  public abstract class appliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderPrepayment.appliedAmt>
  {
  }

  public abstract class payDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.payDocType>
  {
  }

  public abstract class payRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.payRefNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrderPrepayment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderPrepayment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrderPrepayment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderPrepayment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderPrepayment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrderPrepayment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POOrderPrepayment.Tstamp>
  {
  }

  public abstract class statusText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPrepayment.statusText>
  {
  }
}
