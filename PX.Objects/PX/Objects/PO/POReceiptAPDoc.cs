// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptAPDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Receipt Billing History")]
public class POReceiptAPDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(3, IsKey = true, IsFixed = true)]
  [APDocType.List]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<POReceiptAPDoc.docType>>>>), Filterable = true)]
  public virtual string RefNbr { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  [APDocStatus.List]
  public virtual string Status { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Billed Amt.", Enabled = false)]
  public virtual Decimal? TotalAmt { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Accrued Qty.")]
  public virtual Decimal? AccruedQty { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Accrued Amt.")]
  public virtual Decimal? AccruedAmt { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "PPV Amt")]
  public virtual Decimal? TotalPPVAmt { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptAPDoc.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptAPDoc.refNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POReceiptAPDoc.docDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptAPDoc.status>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptAPDoc.totalQty>
  {
  }

  public abstract class totalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptAPDoc.totalAmt>
  {
  }

  public abstract class accruedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptAPDoc.accruedQty>
  {
  }

  public abstract class accruedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptAPDoc.accruedAmt>
  {
  }

  public abstract class totalPPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptAPDoc.totalPPVAmt>
  {
  }

  public abstract class statusText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptAPDoc.statusText>
  {
  }
}
