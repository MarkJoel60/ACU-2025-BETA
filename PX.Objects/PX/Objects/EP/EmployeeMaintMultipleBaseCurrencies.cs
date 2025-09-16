// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public sealed class EmployeeMaintMultipleBaseCurrencies : PXGraphExtension<EmployeeMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected void _(
    PX.Data.Events.FieldUpdated<EPEmployee, EPEmployee.parentBAccountID> e)
  {
    e.Row.BaseCuryID = PXOrgAccess.GetBaseCuryID(e.Row.ParentBAccountID);
  }

  protected void _(
    PX.Data.Events.FieldUpdating<EPEmployee, EPEmployee.parentBAccountID> e)
  {
    if (PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APHistory.vendorID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null).Any<PXResult<APHistory>>() && e.Row.BaseCuryID != PXOrgAccess.GetBaseCuryID(e.NewValue.ToString()))
      throw new PXSetPropertyException("A branch with the base currency other than {0} cannot be associated with the {1} account.", PXErrorLevel.Error, new object[2]
      {
        (object) e.Row.BaseCuryID,
        (object) e.Row.AcctCD
      });
    string documentsNotReleased;
    if (this.HasAnyRelatedDocumentsInvolved(e, out documentsNotReleased) && e.Row.BaseCuryID != PXOrgAccess.GetBaseCuryID(e.NewValue.ToString()))
      throw new PXSetPropertyException("You cannot select a branch with the currency different from {0} for the employee until there is at least one unreleased document with this base currency. Remove or release the following documents: {1}", PXErrorLevel.Error, new object[2]
      {
        (object) e.Row.BaseCuryID,
        (object) documentsNotReleased
      });
  }

  private bool HasAnyRelatedDocumentsInvolved(
    PX.Data.Events.FieldUpdating<EPEmployee, EPEmployee.parentBAccountID> e,
    out string documentsNotReleased)
  {
    documentsNotReleased = "";
    bool flag1 = PXSelectBase<EPExpenseClaimDetails, PXViewOf<EPExpenseClaimDetails>.BasedOn<SelectFromBase<EPExpenseClaimDetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPExpenseClaimDetails.employeeID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null).Any<PXResult<EPExpenseClaimDetails>>();
    bool flag2 = PXSelectBase<EPExpenseClaim, PXViewOf<EPExpenseClaim>.BasedOn<SelectFromBase<EPExpenseClaim, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPExpenseClaim.employeeID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null).Any<PXResult<EPExpenseClaim>>();
    bool flag3 = PXSelectBase<PX.Objects.AP.APInvoice, PXViewOf<PX.Objects.AP.APInvoice>.BasedOn<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.APInvoice.vendorID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null).Any<PXResult<PX.Objects.AP.APInvoice>>();
    bool flag4 = PXSelectBase<APQuickCheck, PXViewOf<APQuickCheck>.BasedOn<SelectFromBase<APQuickCheck, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<APQuickCheck.vendorID, IBqlInt>.IsEqual<BqlField<EPEmployee.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null).Any<PXResult<APQuickCheck>>();
    if (flag1)
      documentsNotReleased += PXLocalizer.Localize("Expense Receipt");
    if (flag2)
      documentsNotReleased = $"{documentsNotReleased}, {PXLocalizer.Localize("Expense Claim")}";
    if (flag3)
      documentsNotReleased = $"{documentsNotReleased}, {PXLocalizer.Localize("AP document")}";
    if (flag4)
      documentsNotReleased = $"{documentsNotReleased}, {PXLocalizer.Localize("Cash Purchase")}";
    return flag1 | flag2 | flag3 | flag4;
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<CACorpCard, Where<CACorpCard.corpCardID, Equal<Current<EPEmployeeCorpCardLink.corpCardID>>>>))]
  [PXSelector(typeof (Search2<CACorpCard.corpCardID, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<Current<EPEmployeeCorpCardLink.employeeID>>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CACorpCard.cashAccountID>>>>, Where<PX.Objects.CR.BAccount.baseCuryID, Equal<PX.Objects.CA.CashAccount.baseCuryID>>>), new System.Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (CACorpCard.cashAccountID)}, SubstituteKey = typeof (CACorpCard.corpCardCD))]
  protected void _(
    PX.Data.Events.CacheAttached<EPEmployeeCorpCardLink.corpCardID> e)
  {
  }

  protected void _(PX.Data.Events.RowSelected<EPEmployee> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<EPEmployee.baseCuryID>(e.Cache, (object) e.Row, true);
  }
}
