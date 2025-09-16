// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PayLink.PayLinkDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.PayLink;

/// <summary>
/// Represents a mapped cache extension for SOOrder/ARInvoice to store data for the Payment Link processing.
/// </summary>
public class PayLinkDocument : PXMappedCacheExtension
{
  /// <exclude />
  public 
  #nullable disable
  string OrderType { get; set; }

  /// <exclude />
  public string OrderNbr { get; set; }

  /// <exclude />
  public string DocType { get; set; }

  /// <exclude />
  public string RefNbr { get; set; }

  /// <exclude />
  public int? BranchID { get; set; }

  /// <exclude />
  public string CuryID { get; set; }

  /// <exclude />
  public string ProcessingCenterID { get; set; }

  /// <summary>Payment Link delivery method (N - none, E - email).</summary>
  public string DeliveryMethod { get; set; }

  /// <summary>Acumatica specific Payment Link Id.</summary>
  public virtual int? PayLinkID { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayLinkDocument.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayLinkDocument.orderNbr>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayLinkDocument.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayLinkDocument.refNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayLinkDocument.branchID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayLinkDocument.curyID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PayLinkDocument.processingCenterID>
  {
  }

  public abstract class deliveryMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PayLinkDocument.deliveryMethod>
  {
  }

  public abstract class payLinkID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayLinkDocument.payLinkID>
  {
  }
}
