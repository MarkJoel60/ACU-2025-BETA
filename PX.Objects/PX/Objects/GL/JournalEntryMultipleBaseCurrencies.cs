// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using PX.Objects.GL.Standalone;
using System;

#nullable disable
namespace PX.Objects.GL;

public sealed class JournalEntryMultipleBaseCurrencies : PXGraphExtension<JournalEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [Branch(null, typeof (Search5<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>, InnerJoin<OrganizationLedgerLink, On<OrganizationLedgerLink.organizationID, Equal<Branch.organizationID>>, InnerJoin<Ledger, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, InnerJoin<LedgerAlias, On<LedgerAlias.ledgerID, Equal<Current<Batch.ledgerID>>, And<Where<LedgerAlias.balanceType, In3<LedgerBalanceType.report, LedgerBalanceType.statistical>, And<LedgerAlias.ledgerID, Equal<Ledger.ledgerID>, Or<LedgerAlias.balanceType, Equal<LedgerBalanceType.actual>, And<Ledger.balanceType, Equal<LedgerBalanceType.actual>, And<Ledger.baseCuryID, Equal<LedgerAlias.baseCuryID>>>>>>>>>>>>, Where<MatchWithBranch<Branch.branchID>>, Aggregate<GroupBy<Branch.branchID, GroupBy<Branch.bAccountID>>>>), false, true, false)]
  [PXDefault(typeof (Batch.branchID))]
  public void _(Events.CacheAttached<GLTran.branchID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBScalarAttribute), "Forced", true)]
  public void _(Events.CacheAttached<Branch.acctName> e)
  {
  }

  protected void _(Events.FieldVerifying<GLTran, GLTran.branchID> e)
  {
    if (e.Row == null || ((Events.FieldVerifyingBase<Events.FieldVerifying<GLTran, GLTran.branchID>, GLTran, object>) e).NewValue == null)
      return;
    Batch current = ((PXSelectBase<Batch>) this.Base.BatchModule)?.Current;
    if (current == null || !(PXSelectorAttribute.Select<Batch.branchID>(((PXSelectBase) this.Base.BatchModule)?.Cache, (object) current) is Branch branch))
      return;
    Branch branchFromNewValue = this.GetBranchFromNewValue(((Events.FieldVerifyingBase<Events.FieldVerifying<GLTran, GLTran.branchID>, GLTran, object>) e).NewValue);
    if (branchFromNewValue != null && branchFromNewValue.BaseCuryID != branch.BaseCuryID)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<GLTran, GLTran.branchID>, GLTran, object>) e).NewValue = (object) branchFromNewValue.BranchCD;
      throw new PXSetPropertyException("The base currency of the {0} branch differs from the base currency of the originating branch.", new object[1]
      {
        (object) branchFromNewValue.BranchCD
      });
    }
    if (e.Row.Module != null)
      return;
    try
    {
      JournalEntry.CheckBatchBranchHasLedger(((PXSelectBase) this.Base.BatchModule)?.Cache, current);
    }
    catch (PXException ex)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<GLTran, GLTran.branchID>, GLTran, object>) e).NewValue = (object) branchFromNewValue?.BranchCD;
      throw new PXSetPropertyException(((Exception) ex).Message);
    }
  }

  protected void _(Events.RowPersisting<GLTran> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = CurrencyInfoAttribute.GetCurrencyInfo<GLTran.curyInfoID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<GLTran>>) e).Cache, (object) e.Row);
    PX.Objects.CM.CurrencyInfo currencyInfo2 = this.Base.currencyInfo;
    Ledger ledger = Ledger.PK.Find((PXGraph) this.Base, e.Row.LedgerID);
    if (e.Operation != 3 && (currencyInfo2?.BaseCuryID != currencyInfo1?.BaseCuryID || ledger?.BaseCuryID != currencyInfo1?.BaseCuryID))
      throw new PXException("The transaction cannot be released because the currency of the {0} ledger differs from the transaction base currency.", new object[1]
      {
        (object) ledger?.LedgerCD
      });
  }

  protected void _(Events.RowPersisting<Batch> e)
  {
    if (e.Row == null || e.Operation == 3)
      return;
    this.CheckBatchAndLedgerBaseCurrency(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<Batch>>) e).Cache, e.Row);
  }

  protected void CheckBatchAndLedgerBaseCurrency(PXCache cache, Batch batch)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) batch.CuryInfoID
    }));
    Ledger ledger = Ledger.PK.Find(cache.Graph, batch.LedgerID);
    if (currencyInfo != null && ledger != null && !currencyInfo.BaseCuryID.Equals(ledger.BaseCuryID))
      throw new PXException("The transaction cannot be released because the currency of the {0} ledger differs from the transaction base currency.", new object[1]
      {
        (object) ledger.LedgerCD
      });
  }

  private Branch GetBranchFromNewValue(object newValue)
  {
    return newValue is string str ? PXResultset<Branch>.op_Implicit(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchCD, Equal<Required<Branch.branchCD>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) str
    })) : PXResultset<Branch>.op_Implicit(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      newValue
    }));
  }
}
