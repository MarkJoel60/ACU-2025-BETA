// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptPOOriginal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Receipt to Purchase Return Link")]
[Serializable]
public class POReceiptPOOriginal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReceiptNbr;

  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = "", BqlField = typeof (POReceipt.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "PR Type")]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (POReceipt.receiptNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Optional<POReceiptPOOriginal.receiptType>>>>), Filterable = true)]
  public virtual string ReceiptNbr { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (POReceipt.receiptDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceipt.status))]
  [PXUIField]
  [POReceiptStatus.List]
  public virtual string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceiptLine.baseReceiptQty))]
  [PXUIField(DisplayName = "Total Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InvtDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POReceiptPOOriginal.invtDocType>>>>))]
  public virtual string InvtRefNbr { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.receiptNbr>
  {
    public const string DisplayName = "PR Nbr.";
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POReceiptPOOriginal.docDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.finPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptPOOriginal.status>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptPOOriginal.totalQty>
  {
  }

  public abstract class invtDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.invtDocType>
  {
  }

  public abstract class invtRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.invtRefNbr>
  {
  }

  public abstract class statusText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptPOOriginal.statusText>
  {
  }
}
