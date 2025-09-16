// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranDetailHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// A CA transaction detail helper class. Contains common logic for classes which implement <see cref="T:PX.Objects.CA.ICATranDetail" />.
/// </summary>
internal static class CATranDetailHelper
{
  /// <summary>
  /// Handles the CA transaction detail <see cref="P:PX.Objects.CA.ICATranDetail.AccountID" /> field updated event.
  /// </summary>
  /// <param name="tranDetailsCache">The transaction details cache.</param>
  /// <param name="e">Field updated event arguments.</param>
  public static void OnAccountIdFieldUpdatedEvent(
    PXCache tranDetailsCache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ICATranDetail row))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(tranDetailsCache.Graph, new object[1]
    {
      (object) row.AccountID
    }));
    if ((account != null ? (!account.IsCashAccount.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    CashAccount cashAccount = CATranDetailHelper.GetCashAccount(tranDetailsCache.Graph, account.AccountID, row.SubID, row.BranchID, true);
    if (cashAccount == null)
      return;
    tranDetailsCache.SetValueExt((object) row, "BranchID", (object) cashAccount.BranchID);
    tranDetailsCache.SetValueExt((object) row, "SubID", (object) cashAccount.SubID);
  }

  /// <summary>
  /// Handles the CA transaction detail <see cref="P:PX.Objects.CA.ICATranDetail.CashAccountID" /> field updated event.
  /// </summary>
  /// <param name="tranDetailsCache">The transaction details cache.</param>
  /// <param name="e">Field updated event arguments.</param>
  public static void OnCashAccountIdFieldUpdatedEvent(
    PXCache tranDetailsCache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ICATranDetail row))
      return;
    int? cashAccountId = row.CashAccountID;
    int? nullable1 = (int?) e.OldValue;
    if (cashAccountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & cashAccountId.HasValue == nullable1.HasValue)
      return;
    CashAccount cashAccount = (CashAccount) null;
    nullable1 = row.CashAccountID;
    if (nullable1.HasValue)
      cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(tranDetailsCache.Graph, new object[1]
      {
        (object) row.CashAccountID
      }));
    PXCache pxCache1 = tranDetailsCache;
    ICATranDetail caTranDetail1 = row;
    int? nullable2;
    if (cashAccount == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = cashAccount.AccountID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local1 = (ValueType) nullable2;
    pxCache1.SetValue((object) caTranDetail1, "AccountID", (object) local1);
    PXCache pxCache2 = tranDetailsCache;
    ICATranDetail caTranDetail2 = row;
    int? nullable3;
    if (cashAccount == null)
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = cashAccount.SubID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local2 = (ValueType) nullable3;
    pxCache2.SetValue((object) caTranDetail2, "SubID", (object) local2);
    PXCache pxCache3 = tranDetailsCache;
    ICATranDetail caTranDetail3 = row;
    int? nullable4;
    if (cashAccount == null)
    {
      nullable1 = new int?();
      nullable4 = nullable1;
    }
    else
      nullable4 = cashAccount.BranchID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local3 = (ValueType) nullable4;
    pxCache3.SetValue((object) caTranDetail3, "BranchID", (object) local3);
  }

  /// <summary>
  /// Handles the CA transaction detail <see cref="P:PX.Objects.CA.ICATranDetail.CashAccountID" /> field defaulting event.
  /// </summary>
  /// <param name="tranDetailsCache">The transaction details cache.</param>
  /// <param name="e">Field defaulting event arguments.</param>
  public static void OnCashAccountIdFieldDefaultingEvent(
    PXCache tranDetailsCache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ICATranDetail row) || row.CashAccountID.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CABankTranDetail.accountID>>, And<CashAccount.subID, Equal<Required<CABankTranDetail.subID>>, And<CashAccount.branchID, Equal<Required<CABankTranDetail.branchID>>, And<CashAccount.active, Equal<True>>>>>>.Config>.Select(tranDetailsCache.Graph, new object[3]
    {
      (object) row.AccountID,
      (object) row.SubID,
      (object) row.BranchID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.CashAccountCD;
  }

  /// <summary>Handles the CA transaction detail row updating event.</summary>
  /// <param name="tranDetailsCache">The transaction details cache.</param>
  /// <param name="e">Row updating event arguments.</param>
  public static void OnCATranDetailRowUpdatingEvent(
    PXCache tranDetailsCache,
    PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is ICATranDetail newRow) || tranDetailsCache == null || !newRow.AccountID.HasValue)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(tranDetailsCache.Graph, new object[1]
    {
      (object) newRow.AccountID
    }));
    if ((account != null ? (!account.IsCashAccount.GetValueOrDefault() ? 1 : 0) : 1) != 0 || CATranDetailHelper.GetCashAccount(tranDetailsCache.Graph, newRow.AccountID, newRow.SubID, newRow.BranchID, false) != null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("There is no Cash Account matching these Branch and Subaccount", (PXErrorLevel) 4);
    string field1 = (string) PXSelectorAttribute.GetField(tranDetailsCache, (object) newRow, "BranchID", (object) newRow.BranchID, typeof (PX.Objects.GL.Branch.branchCD).Name);
    string field2 = (string) PXSelectorAttribute.GetField(tranDetailsCache, (object) newRow, "SubID", (object) newRow.SubID, typeof (PX.Objects.GL.Sub.subCD).Name);
    tranDetailsCache.RaiseExceptionHandling("BranchID", (object) newRow, (object) field1, (Exception) propertyException);
    tranDetailsCache.RaiseExceptionHandling("SubID", (object) newRow, (object) field2, (Exception) propertyException);
    ((CancelEventArgs) e).Cancel = true;
  }

  /// <summary>
  /// Gets the new CA transaction detail <see cref="T:PX.Objects.CA.ICATranDetail" /> with <see cref="P:PX.Objects.CA.ICATranDetail.AccountID" />, <see cref="P:PX.Objects.CA.ICATranDetail.SubID" />
  /// and <see cref="P:PX.Objects.CA.ICATranDetail.BranchID" /> fields filled with default values.
  /// </summary>
  /// <typeparam name="TTransactionDetail">Type of transaction detail</typeparam>
  /// <param name="graph">The sender graph.</param>
  /// <param name="cashAccountID">The cash account ID</param>
  /// <param name="entryTypeID">The entry type ID</param>
  /// <returns />
  public static TTransactionDetail CreateCATransactionDetailWithDefaultAccountValues<TTransactionDetail>(
    PXGraph graph,
    int? cashAccountID,
    string entryTypeID)
    where TTransactionDetail : ICATranDetail, new()
  {
    CashAccountETDetail cashAccountEtDetail = PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXSelect<CashAccountETDetail, Where<CashAccountETDetail.cashAccountID, Equal<Required<CashAccountETDetail.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Required<CashAccountETDetail.entryTypeID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) cashAccountID,
      (object) entryTypeID
    }));
    int? nullable1;
    int num;
    if (cashAccountEtDetail != null)
    {
      nullable1 = cashAccountEtDetail.OffsetAccountID;
      if (nullable1.HasValue)
      {
        nullable1 = cashAccountEtDetail.OffsetSubID;
        if (nullable1.HasValue)
        {
          nullable1 = cashAccountEtDetail.OffsetBranchID;
          num = nullable1.HasValue ? 1 : 0;
          goto label_5;
        }
      }
    }
    num = 0;
label_5:
    TTransactionDetail transactionDetail = new TTransactionDetail();
    ref TTransactionDetail local1 = ref transactionDetail;
    int? nullable2;
    if (cashAccountEtDetail == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = cashAccountEtDetail.OffsetAccountID;
    local1.AccountID = nullable2;
    ref TTransactionDetail local2 = ref transactionDetail;
    int? nullable3;
    if (cashAccountEtDetail == null)
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = cashAccountEtDetail.OffsetSubID;
    local2.SubID = nullable3;
    ref TTransactionDetail local3 = ref transactionDetail;
    int? nullable4;
    if (cashAccountEtDetail == null)
    {
      nullable1 = new int?();
      nullable4 = nullable1;
    }
    else
      nullable4 = cashAccountEtDetail.OffsetBranchID;
    local3.BranchID = nullable4;
    TTransactionDetail defaultAccountValues = transactionDetail;
    if (num != 0)
      return defaultAccountValues;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select(graph, new object[1]
    {
      (object) entryTypeID
    }));
    if (caEntryType != null)
    {
      ref TTransactionDetail local4 = ref defaultAccountValues;
      transactionDetail = default (TTransactionDetail);
      if ((object) transactionDetail == null)
      {
        transactionDetail = local4;
        local4 = ref transactionDetail;
      }
      nullable1 = defaultAccountValues.AccountID;
      int? nullable5 = nullable1 ?? caEntryType.AccountID;
      local4.AccountID = nullable5;
      ref TTransactionDetail local5 = ref defaultAccountValues;
      transactionDetail = default (TTransactionDetail);
      if ((object) transactionDetail == null)
      {
        transactionDetail = local5;
        local5 = ref transactionDetail;
      }
      nullable1 = defaultAccountValues.SubID;
      int? nullable6 = nullable1 ?? caEntryType.SubID;
      local5.SubID = nullable6;
      ref TTransactionDetail local6 = ref defaultAccountValues;
      transactionDetail = default (TTransactionDetail);
      if ((object) transactionDetail == null)
      {
        transactionDetail = local6;
        local6 = ref transactionDetail;
      }
      nullable1 = defaultAccountValues.BranchID;
      int? nullable7 = nullable1 ?? caEntryType.BranchID;
      local6.BranchID = nullable7;
    }
    return defaultAccountValues;
  }

  /// <summary>
  /// Updates the new transaction detail CuryTranAmt or CuryUnitPrice.
  /// </summary>
  /// <param name="tranDetailsCache">The transaction details cache.</param>
  /// <param name="oldTranDetail">The old transaction detail.</param>
  /// <param name="newTranDetail">The new transaction detail.</param>
  public static void UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(
    PXCache tranDetailsCache,
    ICATranDetail oldTranDetail,
    ICATranDetail newTranDetail)
  {
    if (tranDetailsCache == null || newTranDetail == null)
      return;
    Decimal? nullable1 = (Decimal?) oldTranDetail?.CuryUnitPrice;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.CuryUnitPrice;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    bool flag1 = valueOrDefault1 != valueOrDefault2;
    Decimal? nullable2;
    if (oldTranDetail == null)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = oldTranDetail.CuryTranAmt;
    nullable1 = nullable2;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.CuryTranAmt;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    int num1 = valueOrDefault3 != valueOrDefault4 ? 1 : 0;
    Decimal? nullable3;
    if (oldTranDetail == null)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = oldTranDetail.Qty;
    nullable1 = nullable3;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.Qty;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    bool flag2 = valueOrDefault5 != valueOrDefault6;
    if (num1 != 0)
    {
      nullable1 = newTranDetail.Qty;
      if (nullable1.HasValue)
      {
        nullable1 = newTranDetail.Qty;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          nullable1 = newTranDetail.CuryTranAmt;
          Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
          nullable1 = newTranDetail.Qty;
          Decimal num3 = nullable1.Value;
          Decimal val = valueOrDefault7 / num3;
          newTranDetail.CuryUnitPrice = new Decimal?(PXCurrencyAttribute.RoundCury(tranDetailsCache, (object) newTranDetail, val));
          return;
        }
      }
      newTranDetail.CuryUnitPrice = newTranDetail.CuryTranAmt;
      newTranDetail.Qty = new Decimal?(1.0M);
    }
    else
    {
      if (!(flag1 | flag2))
        return;
      ICATranDetail caTranDetail = newTranDetail;
      nullable1 = newTranDetail.Qty;
      Decimal? curyUnitPrice = newTranDetail.CuryUnitPrice;
      Decimal? nullable4 = nullable1.HasValue & curyUnitPrice.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * curyUnitPrice.GetValueOrDefault()) : new Decimal?();
      caTranDetail.CuryTranAmt = nullable4;
    }
  }

  /// <summary>Gets cash account.</summary>
  /// <param name="glAccountID">Identifier for the GL account.</param>
  /// <param name="subID">Identifier for the sub account.</param>
  /// <param name="branchID">Identifier for the branch.</param>
  /// <param name="doSearchWithSubsetsOfArgs">True to do search with subsets of search arguments.</param>
  /// <returns />
  public static CashAccount GetCashAccount(
    PXGraph graph,
    int? glAccountID,
    int? subID,
    int? branchID,
    bool doSearchWithSubsetsOfArgs)
  {
    CashAccount cashAccount = (CashAccount) null;
    if (subID.HasValue && branchID.HasValue)
      cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>, And<CashAccount.branchID, Equal<Required<CashAccount.branchID>>, And<CashAccount.active, Equal<True>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) glAccountID,
        (object) subID,
        (object) branchID
      }));
    if (cashAccount != null || !doSearchWithSubsetsOfArgs)
      return cashAccount;
    if (branchID.HasValue)
      cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.branchID, Equal<Required<CashAccount.branchID>>, And<CashAccount.active, Equal<True>>>>>.Config>.Select(graph, new object[2]
      {
        (object) glAccountID,
        (object) branchID
      }));
    else if (subID.HasValue)
      cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>, And<CashAccount.active, Equal<True>>>>>.Config>.Select(graph, new object[2]
      {
        (object) glAccountID,
        (object) subID
      }));
    if (cashAccount == null)
      cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.active, Equal<True>>>>.Config>.Select(graph, new object[1]
      {
        (object) glAccountID
      }));
    return cashAccount;
  }

  /// <summary>
  /// Checks whether a cash account can be found with branchID, accountID and subID, and the found cash account is not the same as in the header of the CA Transaction document
  /// </summary>
  /// <param name="tranDetailsCache">PXCache</param>
  /// <param name="row">CA transactions</param>
  /// <param name="headerCashAccountID">Cash Account ID from the header document</param>
  /// <returns></returns>
  public static bool VerifyOffsetCashAccount(
    PXCache tranDetailsCache,
    ICATranDetail row,
    int? headerCashAccountID)
  {
    bool flag = false;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(tranDetailsCache.Graph, new object[1]
    {
      (object) row.AccountID
    }));
    if ((account != null ? (!account.IsCashAccount.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return false;
    CashAccount cashAccount = CATranDetailHelper.GetCashAccount(tranDetailsCache.Graph, row.AccountID, row.SubID, row.BranchID, false);
    if (cashAccount == null)
    {
      CATranDetailHelper.SetPropertyExceptionOnFields(tranDetailsCache, row, "There is no Cash Account matching these Branch and Subaccount");
      flag = true;
    }
    else if (headerCashAccountID.HasValue)
    {
      int? cashAccountId = cashAccount.CashAccountID;
      int? nullable = headerCashAccountID;
      if (cashAccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & cashAccountId.HasValue == nullable.HasValue)
      {
        CATranDetailHelper.SetPropertyExceptionOnFields(tranDetailsCache, row, "The current branch, offset account, and offset subaccount cannot be used because this account is specified in the Cash Account box.");
        flag = true;
      }
    }
    return flag;
  }

  private static void SetPropertyExceptionOnFields(
    PXCache tranDetailsCache,
    ICATranDetail row,
    string errorMessage)
  {
    int num = PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? 1 : 0;
    string field1 = (string) PXSelectorAttribute.GetField(tranDetailsCache, (object) row, "BranchID", (object) row.BranchID, typeof (PX.Objects.GL.Branch.branchCD).Name);
    string field2 = (string) PXSelectorAttribute.GetField(tranDetailsCache, (object) row, "AccountID", (object) row.AccountID, typeof (PX.Objects.GL.Account.accountCD).Name);
    string field3 = (string) PXSelectorAttribute.GetField(tranDetailsCache, (object) row, "SubID", (object) row.SubID, typeof (PX.Objects.GL.Sub.subCD).Name);
    PXSetPropertyException propertyException = new PXSetPropertyException(errorMessage, (PXErrorLevel) 4);
    tranDetailsCache.RaiseExceptionHandling("BranchID", (object) row, (object) field1, (Exception) propertyException);
    tranDetailsCache.RaiseExceptionHandling("AccountID", (object) row, (object) field2, (Exception) propertyException);
    if (num == 0)
      return;
    tranDetailsCache.RaiseExceptionHandling("SubID", (object) row, (object) field3, (Exception) propertyException);
  }
}
