// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARReleaseProcessExt.ValidateRequiredRelatedItems
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN.RelatedItems;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARReleaseProcessExt;

public class ValidateRequiredRelatedItems : 
  ValidateRequiredRelatedItems<ARReleaseProcess, PX.Objects.SO.SOInvoice, ARTran>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.relatedItems>() && PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();
  }

  [PXOverride]
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, SOOrderType, ARTaxTran> r,
    PX.Objects.GL.GLTran tran,
    ValidateRequiredRelatedItems.ReleaseInvoiceTransactionPostProcessingHandler baseImpl)
  {
    if (ardoc.OrigModule == "SO" && !this.Validate(PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, SOOrderType, ARTaxTran>.op_Implicit(r)))
      return;
    baseImpl(je, ardoc, r, tran);
  }

  public override void ThrowError()
  {
    if (this.IsMassProcessing)
      throw new PXException("The invoice cannot be released because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab on the Invoices (SO303000) form.");
    throw new PXException("The invoice cannot be released because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab.");
  }

  public delegate void ReleaseInvoiceTransactionPostProcessingHandler(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    PXResult<ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, SOOrderType, ARTaxTran> r,
    PX.Objects.GL.GLTran tran);
}
