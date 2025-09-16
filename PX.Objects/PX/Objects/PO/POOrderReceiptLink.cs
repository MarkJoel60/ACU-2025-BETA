// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderReceiptLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.IN;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select3<POOrderReceipt, LeftJoin<POOrder, On<POOrderReceipt.pOType, Equal<POOrder.orderType>, And<POOrderReceipt.pONbr, Equal<POOrder.orderNbr>>>>, OrderBy<Asc<POOrderReceipt.pONbr>>>), Persistent = false)]
[PXCacheName("Purchase Receipt to Purchase Order Link")]
[Serializable]
public class POOrderReceiptLink : POOrderReceipt
{
  protected 
  #nullable disable
  string _Status;
  protected string _TaxZoneID;
  protected string _CuryID;

  [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.status))]
  [PXUIField]
  [POOrderStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POOrder.taxZoneID))]
  [PXUIField]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.taxCalcMode))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POOrder.termsID))]
  [PXUIField]
  public virtual string TermsID { get; set; }

  [POOrderPayToVendor(CacheGlobal = true, Filterable = true, BqlField = typeof (POOrder.payToVendorID))]
  public virtual int? PayToVendorID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrder.curyID))]
  [PXUIField]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBQuantity(BqlField = typeof (POOrder.unbilledOrderQty))]
  [PXUIField(DisplayName = "Unbilled Quantity", Enabled = false)]
  public virtual Decimal? UnbilledOrderQty { get; set; }

  [PXDBCury(typeof (POOrderReceiptLink.curyID), BqlField = typeof (POOrder.curyUnbilledOrderTotal))]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  public virtual Decimal? CuryUnbilledOrderTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.ExternalTaxExemptionNumber" />
  [PXDBString(30, IsUnicode = true, BqlField = typeof (POOrder.externalTaxExemptionNumber))]
  [PXUIField]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.EntityUsageType" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.entityUsageType))]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  public new class PK : 
    PrimaryKeyOf<POOrderReceiptLink>.By<POOrderReceiptLink.receiptType, POOrderReceiptLink.receiptNbr, POOrderReceiptLink.pOType, POOrderReceiptLink.pONbr>
  {
    public static POOrderReceiptLink Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      string pOType,
      string pONbr,
      PKFindOptions options = 0)
    {
      return (POOrderReceiptLink) PrimaryKeyOf<POOrderReceiptLink>.By<POOrderReceiptLink.receiptType, POOrderReceiptLink.receiptNbr, POOrderReceiptLink.pOType, POOrderReceiptLink.pONbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) pOType, (object) pONbr, options);
    }
  }

  public new static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POOrderReceiptLink>.By<POOrderReceiptLink.receiptType, POOrderReceiptLink.receiptNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderReceiptLink>.By<POOrderReceiptLink.pOType, POOrderReceiptLink.pONbr>
    {
    }
  }

  public new abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderReceiptLink.receiptType>
  {
  }

  public new abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderReceiptLink.receiptNbr>
  {
  }

  public new abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.pOType>
  {
  }

  public new abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.pONbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.status>
  {
  }

  public new abstract class receiptNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderReceiptLink.receiptNoteID>
  {
  }

  public new abstract class orderNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderReceiptLink.orderNoteID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.taxZoneID>
  {
  }

  public abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderReceiptLink.taxCalcMode>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.termsID>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderReceiptLink.payToVendorID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceiptLink.curyID>
  {
  }

  public abstract class unbilledOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderReceiptLink.unbilledOrderQty>
  {
  }

  public abstract class curyUnbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderReceiptLink.curyUnbilledOrderTotal>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POOrderReceiptLink.Tstamp>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderReceiptLink.externalTaxExemptionNumber>
  {
  }

  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderReceiptLink.entityUsageType>
  {
  }
}
