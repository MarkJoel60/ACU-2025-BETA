// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Order Adjust")]
[Serializable]
public class POAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAdjustmentStub
{
  public const int AdjgDocTypeLength = 3;
  public const int AdjgRefNbrLength = 15;
  public const 
  #nullable disable
  string EmptyApDocType = "---";

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Linked to Prepayment", Enabled = false)]
  public virtual bool? IsRequest { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [APPaymentType.List]
  [PXDefault(typeof (PX.Objects.AP.APPayment.docType))]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false, Visible = false)]
  public virtual string AdjgDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.AP.APPayment.refNbr), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false, Visible = false)]
  [PXParent(typeof (Select<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Current<POAdjust.adjgDocType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Current<POAdjust.adjgRefNbr>>>>>))]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("RO")]
  [PXStringList(new string[] {"RO", "DP"}, new string[] {"Normal", "Drop-Ship"})]
  [PXUIField(DisplayName = "PO Type")]
  public virtual string AdjdOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXParent(typeof (POAdjust.FK.AdjustedOrder))]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<POAdjust.adjdOrderType>>, And<Where<Current<POAdjust.isRequest>, Equal<True>, Or<Current<POAdjust.released>, Equal<True>, Or<Where<POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<POOrder.curyUnprepaidTotal, Greater<decimal0>>>>>>>>>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.orderDate), typeof (POOrder.status), typeof (POOrder.curyUnprepaidTotal), typeof (POOrder.curyLineTotal), typeof (POOrder.curyID)}, Filterable = true)]
  public virtual string AdjdOrderNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PX.Objects.AP.APPayment.adjCntr))]
  public virtual int? AdjNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("---")]
  [APDocType.List]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<POAdjust.adjdDocType>>>>), DirtyRead = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Voided", Enabled = false)]
  public virtual bool? Voided { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AP.APPayment.curyInfoID))]
  public virtual long? AdjgCuryInfoID { get; set; }

  [PXDBCurrency(typeof (POAdjust.adjgCuryInfoID), typeof (POAdjust.adjgAmt))]
  [PXUIField(DisplayName = "Applied To Order")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Switch<Case<Where<POAdjust.isRequest, NotEqual<True>>, POAdjust.curyAdjgAmt>, decimal0>), typeof (SumCalc<PX.Objects.AP.APPayment.curyPOApplAmt>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<POAdjust.isRequest, NotEqual<True>, And<POAdjust.released, NotEqual<True>>>, POAdjust.curyAdjgAmt>, decimal0>), typeof (SumCalc<PX.Objects.AP.APPayment.curyPOUnreleasedApplAmt>))]
  [PXUnboundFormula(typeof (Add<POAdjust.curyAdjgAmt, POAdjust.curyAdjgBilledAmt>), typeof (SumCalc<PX.Objects.AP.APPayment.curyPOFullApplAmt>))]
  public virtual Decimal? CuryAdjgAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjgAmt { get; set; }

  [PXDBCurrency(typeof (POAdjust.adjgCuryInfoID), typeof (POAdjust.adjgBilledAmt))]
  [PXUIField(DisplayName = "Transferred to Bill", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgBilledAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjgBilledAmt { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (Search<POOrder.curyInfoID, Where<POOrder.orderType, Equal<Current<POAdjust.adjdOrderType>>, And<POOrder.orderNbr, Equal<Current<POAdjust.adjdOrderNbr>>>>>))]
  public virtual long? AdjdCuryInfoID { get; set; }

  [PXDBCurrency(typeof (POAdjust.adjdCuryInfoID), typeof (POAdjust.adjdAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjdAmt { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public string StubNbr { get; set; }

  [PXDBInt]
  public int? CashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public string PaymentMethodID { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? ForceDelete { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the adjustment.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public bool Persistent => true;

  public Decimal? CuryAdjgDiscAmt => new Decimal?(0M);

  public Decimal? CuryOutstandingBalance => new Decimal?();

  public DateTime? OutstandingBalanceDate => new DateTime?();

  public class PK : 
    PrimaryKeyOf<POAdjust>.By<POAdjust.adjgDocType, POAdjust.adjgRefNbr, POAdjust.adjdOrderType, POAdjust.adjdOrderNbr, POAdjust.adjNbr, POAdjust.adjdDocType, POAdjust.adjdRefNbr>
  {
    public static POAdjust Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      string adjdOrderType,
      string adjdOrderNbr,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      PKFindOptions options = 0)
    {
      return (POAdjust) PrimaryKeyOf<POAdjust>.By<POAdjust.adjgDocType, POAdjust.adjgRefNbr, POAdjust.adjdOrderType, POAdjust.adjdOrderNbr, POAdjust.adjNbr, POAdjust.adjdDocType, POAdjust.adjdRefNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjdOrderType, (object) adjdOrderNbr, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, options);
    }
  }

  public static class FK
  {
    public class AdjustedOrder : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POAdjust>.By<POAdjust.adjdOrderType, POAdjust.adjdOrderNbr>
    {
    }

    public class AdjustedInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POAdjust>.By<POAdjust.adjdDocType, POAdjust.adjdRefNbr>
    {
    }

    public class AdjustingPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<POAdjust>.By<POAdjust.adjgDocType, POAdjust.adjgRefNbr>
    {
    }

    public class CacheAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<POAdjust>.By<POAdjust.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<POAdjust>.By<POAdjust.paymentMethodID>
    {
    }
  }

  public abstract class isRequest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAdjust.isRequest>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjgRefNbr>
  {
  }

  public abstract class adjdOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjdOrderNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAdjust.adjNbr>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.adjdRefNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAdjust.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAdjust.voided>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POAdjust.adjgCuryInfoID>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAdjust.adjgAmt>
  {
  }

  public abstract class curyAdjgBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAdjust.curyAdjgBilledAmt>
  {
  }

  public abstract class adjgBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAdjust.adjgBilledAmt>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POAdjust.adjdCuryInfoID>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAdjust.curyAdjdAmt>
  {
  }

  public abstract class adjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAdjust.adjdAmt>
  {
  }

  public abstract class stubNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.stubNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAdjust.cashAccountID>
  {
  }

  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAdjust.paymentMethodID>
  {
  }

  public abstract class forceDelete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAdjust.forceDelete>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAdjust.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAdjust.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAdjust.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POAdjust.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAdjust.noteID>
  {
  }
}
