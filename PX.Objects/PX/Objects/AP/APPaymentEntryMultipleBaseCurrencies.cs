// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentEntryMultipleBaseCurrencies : PXGraphExtension<APPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldVerifying<APPayment.branchID> e)
  {
    if (e.NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<APPayment.branchID>(e.Cache, e.Row, (object) (int) e.NewValue) as PX.Objects.GL.Branch;
    string str = (string) PXFormulaAttribute.Evaluate<APPaymentMultipleBaseCurrenciesRestriction.vendorBaseCuryID>(e.Cache, e.Row);
    if (str != null && branch != null && branch.BaseCuryID != str)
    {
      e.NewValue = (object) branch.BranchCD;
      BAccountR baccountR = PXSelectorAttribute.Select<APPayment.vendorID>(e.Cache, e.Row) as BAccountR;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.VOrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APPayment> e)
  {
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<APPayment.branchID>(e.Cache, (object) e.Row, (object) e.Row.BranchID) as PX.Objects.GL.Branch;
    if (!(e.Cache.GetValueExt<APPaymentMultipleBaseCurrenciesRestriction.vendorBaseCuryID>((object) e.Row) is PXFieldState valueExt) || valueExt.Value == null || branch == null || !(branch.BaseCuryID != valueExt.ToString()))
      return;
    e.Row.BranchID = new int?();
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [APInvoiceType.AdjdRefNbr(typeof (Search5<APAdjust.APInvoice.refNbr, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APAdjust2.voided, Equal<False>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APAdjust.APInvoice.docType>, And<APPayment.refNbr, Equal<APAdjust.APInvoice.refNbr>, And<Where<APPayment.docType, Equal<APDocType.prepayment>, Or<APPayment.docType, Equal<APDocType.debitAdj>>>>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.AP.Standalone.APRegister.branchID>>>>>>, Where<APAdjust.APInvoice.vendorID, Equal<Optional<APPayment.vendorID>>, And<APAdjust.APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And<PX.Objects.GL.Branch.baseCuryID, Equal<Optional<APPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>>, And2<Where<APAdjust.APInvoice.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, And<APAdjust.APInvoice.openDoc, Equal<True>, And<APRegister.hold, Equal<False>, And2<Where<APAdjust.adjgRefNbr, IsNull, Or<APAdjust.APInvoice.isJointPayees, Equal<True>>>, And<APAdjust2.adjdRefNbr, IsNull, And2<Where<APPayment.refNbr, IsNull, And<Current<APPayment.docType>, NotEqual<APDocType.refund>, Or<APPayment.refNbr, IsNotNull, And<Current<APPayment.docType>, Equal<APDocType.refund>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.check>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.voidCheck>>>>>>>>>, And2<Where<APAdjust.APInvoice.docDate, LessEqual<Current<APPayment.adjDate>>, And<APAdjust.APInvoice.tranPeriodID, LessEqual<Current<APPayment.adjTranPeriodID>>, Or<Current<APPayment.adjTranPeriodID>, IsNull, Or<Current<APPayment.docType>, Equal<APDocType.check>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.voidCheck>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.prepayment>, And<Current<APSetup.earlyChecks>, Equal<True>>>>>>>>>>, And2<Where<Current<APSetup.migrationMode>, NotEqual<True>, Or<APAdjust.APInvoice.isMigratedRecord, Equal<Current<APRegister.isMigratedRecord>>>>, And<Where<APAdjust.APInvoice.pendingPPD, NotEqual<True>, Or<Current<APRegister.pendingPPD>, Equal<True>>>>>>>>>>>>>>>, Aggregate<GroupBy<APAdjust.APInvoice.docType, GroupBy<APAdjust.APInvoice.refNbr>>>>), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<APAdjust.adjdRefNbr> e)
  {
  }
}
