// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRRelationDetailDeclarationExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>Relations details mapping</summary>
public abstract class CRRelationDetailDeclarationExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph, new()
{
  [CRRelationDetail(typeof (PX.Objects.AP.APInvoice.status), typeof (PX.Objects.AP.APInvoice.docDesc), typeof (PX.Objects.AP.APRegister.employeeID), typeof (PX.Objects.AP.APInvoice.docDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APInvoice.refNbr> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.AR.ARInvoice.status), typeof (PX.Objects.AR.ARInvoice.docDesc), typeof (PX.Objects.AR.ARInvoice.ownerID), typeof (PX.Objects.AR.ARInvoice.docDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.refNbr> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.CR.BAccount.status), null, typeof (PX.Objects.CR.BAccount.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.bAccountID> e)
  {
  }

  [CRRelationDetail(typeof (CRCampaign.status), typeof (CRCampaign.description), typeof (CRCampaign.ownerID), typeof (CRCampaign.startDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCampaign.campaignID> e)
  {
  }

  [CRRelationDetail(typeof (CRCase.status), typeof (CRCase.subject), typeof (CRCase.ownerID), typeof (CRCase.createdDateTime))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.caseCD> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.CR.Contact.status), null, typeof (PX.Objects.CR.Contact.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.contactID> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.AR.Customer.status), null, typeof (PX.Objects.CR.BAccount.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.Customer.bAccountID> e)
  {
  }

  [CRRelationDetail(typeof (CREmployee.vStatus), null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CREmployee.bAccountID> e)
  {
  }

  [CRRelationDetail(typeof (EPExpenseClaimDetails.status), null, null, typeof (EPExpenseClaimDetails.expenseDate))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPExpenseClaimDetails.claimDetailCD> e)
  {
  }

  [CRRelationDetail(typeof (CRLead.status), null, typeof (CRLead.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRLead.contactID> e)
  {
  }

  [CRRelationDetail(typeof (CROpportunity.status), typeof (CROpportunity.subject), typeof (CROpportunity.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunity.opportunityID> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.PO.POOrder.status), typeof (PX.Objects.PO.POOrder.orderDesc), typeof (PX.Objects.PO.POOrder.ownerID), typeof (PX.Objects.PO.POOrder.orderDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderNbr> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.SO.SOOrder.status), typeof (PX.Objects.SO.SOOrder.orderDesc), typeof (PX.Objects.SO.SOOrder.ownerID), typeof (PX.Objects.SO.SOOrder.orderDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.orderNbr> e)
  {
  }

  [CRRelationDetail(typeof (CRQuote.status), typeof (CRQuote.subject), typeof (CRQuote.ownerID), typeof (CRQuote.documentDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.quoteID> e)
  {
  }

  [CRRelationDetail(typeof (PX.Objects.AP.Vendor.vStatus), null, typeof (PX.Objects.AP.Vendor.ownerID), null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.Vendor.bAccountID> e)
  {
  }
}
