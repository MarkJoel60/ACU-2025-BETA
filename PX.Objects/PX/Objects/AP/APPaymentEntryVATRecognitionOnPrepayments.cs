// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.Standalone;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentEntryVATRecognitionOnPrepayments : PXGraphExtension<APPaymentEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.Base.Document.WhereAnd<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.docType, NotEqual<APDocType.prepaymentInvoice>>>>>.Or<BqlOperand<APPayment.released, IBqlBool>.IsEqual<True>>>>();
    this.Base.APPost.WhereAnd<Where<APTranPostBal.accountID, Equal<Current<APPayment.aPAccountID>>, PX.Data.Or<Where2<Not<APTranPostBal.docType, Equal<APDocType.prepaymentInvoice>, And<APTranPostBal.sourceDocType, Equal<APDocType.debitAdj>>>, PX.Data.And<Not<APTranPostBal.docType, Equal<APDocType.debitAdj>, And<APTranPostBal.sourceDocType, Equal<APDocType.prepaymentInvoice>>>>>>>>();
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (APPaymentType.RefNbrAttribute))]
  [APPaymentType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegisterAlias.docType>, And<APPayment.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>>>>, Where<APRegisterAlias.docType, Equal<Current<APPayment.docType>>, And2<Where<APRegisterAlias.docType, NotEqual<APDocType.prepaymentInvoice>, Or<APRegisterAlias.released, Equal<True>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>, PX.Data.OrderBy<Desc<APRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  protected virtual void APPayment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<APPayment> e)
  {
    if (!(e.Row?.DocType == "PPI"))
      return;
    e.Cache.SetValue<APPayment.released>((object) e.Row, (object) true);
  }

  [PXOverride]
  public virtual void CheckDocumentBeforeReversing(PXGraph graph, APAdjust application)
  {
    if (application == null)
      return;
    if (application.AdjdDocType == "PPI" && application.AdjgDocType == "ADR")
      throw new PXException("An application of a reversing debit adjustment to a prepayment invoice cannot be reversed.");
    if (!(application.AdjdDocType == "PPI") || !(application.AdjgDocType == "CHK") && !(application.AdjgDocType == "PPM"))
      return;
    APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<APDocType.prepaymentInvoice>>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.Config>.Select(graph, (object) application.AdjdRefNbr);
    if (apAdjust != null)
      throw new PXException("The application to the prepayment invoice cannot be reversed because the prepayment invoice has already been applied to the {0} document with the {1} ref. number. To reverse the application, reverse the application of the prepayment invoice to the {0} document.", new object[2]
      {
        (object) APDocType.GetDisplayName(apAdjust.AdjdDocType),
        (object) apAdjust.AdjdRefNbr
      });
  }
}
