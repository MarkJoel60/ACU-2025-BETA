// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPayBillsVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APPayBillsVisibilityRestriction : PXGraphExtension<APPayBills>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [APInvoiceType.AdjdRefNbr(typeof (Search2<APInvoice.refNbr, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<APInvoice.vendorID>, And<Where<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.oneTime>>>>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>, Where<APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And<PX.Objects.CR.BAccount.vOrgBAccountID, RestrictByBranch<Current<PayBillsFilter.branchID>>, And2<Where<APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>, Or<APInvoice.pendingPayment, Equal<True>>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APInvoice.hold, Equal<False>, And<APInvoice.openDoc, Equal<True>, And<APAdjust.adjgRefNbr, IsNull, And<APPayment.refNbr, IsNull, And<APInvoice.pendingPPD, NotEqual<True>>>>>>>>>>>), Filterable = true)]
  protected virtual void APAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public virtual BqlCommand ComposeBQLCommandForAPDocumentListSelect(
    APPayBillsVisibilityRestriction.ComposeBQLCommandForAPDocumentListSelectDelegate baseMethod)
  {
    BqlCommand bqlCommand = baseMethod();
    bqlCommand.WhereAnd<Where<Vendor.vOrgBAccountID, RestrictByBranch<Current<PayBillsFilter.branchID>>>>();
    return bqlCommand;
  }

  public delegate BqlCommand ComposeBQLCommandForAPDocumentListSelectDelegate();
}
