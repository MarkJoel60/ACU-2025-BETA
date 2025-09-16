// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.ARInvoicePayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// Represents database fields which store Payment Link specific data.
/// </summary>
public sealed class ARInvoicePayLink : PXCacheExtension<
#nullable disable
PX.Objects.AR.ARInvoice>
{
  public static bool IsActive() => true;

  /// <summary>Acumatica specific Payment Link Id.</summary>
  [PXDBInt]
  public int? PayLinkID { get; set; }

  /// <exclude />
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search2<CCProcessingCenter.processingCenterID, InnerJoin<PX.Objects.CA.CashAccount, On<CCProcessingCenter.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>, InnerJoin<CCProcessingCenterBranch, On<CCProcessingCenterBranch.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>>, Where<PX.Objects.CA.CashAccount.curyID, Equal<Current<PX.Objects.AR.ARInvoice.curyID>>, And<CCProcessingCenter.allowPayLink, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>, And<CCProcessingCenterBranch.branchID, Equal<Current<PX.Objects.AR.ARInvoice.branchID>>>>>>>), DescriptionField = typeof (CCProcessingCenter.name))]
  [PXUIField(DisplayName = "Processing Center", Visible = true, Enabled = true)]
  public string ProcessingCenterID { get; set; }

  /// <summary>Payment Link delivery method (N - none, E - email).</summary>
  [PXDBString(1, IsFixed = true)]
  [PayLinkDeliveryMethod.List]
  [PXUIField(DisplayName = "Link Delivery Method")]
  public string DeliveryMethod { get; set; }

  public abstract class payLinkID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoicePayLink.payLinkID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoicePayLink.processingCenterID>
  {
  }

  public abstract class deliveryMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoicePayLink.deliveryMethod>
  {
  }

  public abstract class aRDocTypePayLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoicePayLink.aRDocTypePayLink>
  {
    public class ARDocTypesPayLinkAllowed : IBqlConstants
    {
      public IEnumerable<object> GetValues(PXGraph graph)
      {
        return (IEnumerable<object>) new string[3]
        {
          "INV",
          "DRM",
          "FCH"
        };
      }
    }
  }
}
