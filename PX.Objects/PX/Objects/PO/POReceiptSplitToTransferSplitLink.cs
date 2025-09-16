// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptSplitToTransferSplitLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
public class POReceiptSplitToTransferSplitLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ReceiptLineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptSplitToTransferSplitLink.FK.ReceiptLineSplit))]
  public virtual int? ReceiptSplitLineNbr { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  public virtual string TransferDocType
  {
    get => "T";
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>, And<PX.Objects.IN.INRegister.transferType, Equal<INTransferType.oneStep>>>>))]
  public virtual string TransferRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? TransferLineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptSplitToTransferSplitLink.FK.TransferLineSplit))]
  public virtual int? TransferSplitLineNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.receiptType, POReceiptSplitToTransferSplitLink.receiptNbr, POReceiptSplitToTransferSplitLink.receiptLineNbr, POReceiptSplitToTransferSplitLink.receiptSplitLineNbr, POReceiptSplitToTransferSplitLink.transferDocType, POReceiptSplitToTransferSplitLink.transferRefNbr, POReceiptSplitToTransferSplitLink.transferLineNbr, POReceiptSplitToTransferSplitLink.transferSplitLineNbr>
  {
    public static POReceiptSplitToTransferSplitLink Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? receiptLineNbr,
      int? receiptSplitLineNbr,
      string transferDocType,
      string transferRefNbr,
      int? transferLineNbr,
      int? transferSplitLineNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptSplitToTransferSplitLink) PrimaryKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.receiptType, POReceiptSplitToTransferSplitLink.receiptNbr, POReceiptSplitToTransferSplitLink.receiptLineNbr, POReceiptSplitToTransferSplitLink.receiptSplitLineNbr, POReceiptSplitToTransferSplitLink.transferDocType, POReceiptSplitToTransferSplitLink.transferRefNbr, POReceiptSplitToTransferSplitLink.transferLineNbr, POReceiptSplitToTransferSplitLink.transferSplitLineNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) receiptLineNbr, (object) receiptSplitLineNbr, (object) transferDocType, (object) transferRefNbr, (object) transferLineNbr, (object) transferSplitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.receiptType, POReceiptSplitToTransferSplitLink.receiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.receiptType, POReceiptSplitToTransferSplitLink.receiptNbr, POReceiptSplitToTransferSplitLink.receiptLineNbr>
    {
    }

    public class ReceiptLineSplit : 
      PrimaryKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr, POReceiptLineSplit.splitLineNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.receiptType, POReceiptSplitToTransferSplitLink.receiptNbr, POReceiptSplitToTransferSplitLink.receiptLineNbr, POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>
    {
    }

    public class Transfer : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.transferDocType, POReceiptSplitToTransferSplitLink.transferRefNbr>
    {
    }

    public class TransferLine : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.transferDocType, POReceiptSplitToTransferSplitLink.transferRefNbr, POReceiptSplitToTransferSplitLink.transferLineNbr>
    {
    }

    public class TransferLineSplit : 
      PrimaryKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.lineNbr, INTranSplit.splitLineNbr>.ForeignKeyOf<POReceiptSplitToTransferSplitLink>.By<POReceiptSplitToTransferSplitLink.transferDocType, POReceiptSplitToTransferSplitLink.transferRefNbr, POReceiptSplitToTransferSplitLink.transferLineNbr, POReceiptSplitToTransferSplitLink.transferSplitLineNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.receiptNbr>
  {
  }

  public abstract class receiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.receiptLineNbr>
  {
  }

  public abstract class receiptSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>
  {
  }

  public abstract class transferDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.transferDocType>
  {
  }

  public abstract class transferRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.transferRefNbr>
  {
  }

  public abstract class transferLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.transferLineNbr>
  {
  }

  public abstract class transferSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.transferSplitLineNbr>
  {
  }

  public abstract class qty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptSplitToTransferSplitLink.qty>
  {
  }
}
