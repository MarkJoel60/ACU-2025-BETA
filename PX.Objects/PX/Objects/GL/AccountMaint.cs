// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.GL;

public class AccountMaint : PXGraph<
#nullable disable
AccountMaint>, PXImportAttribute.IPrepare
{
  public PXSavePerRow<Account, Account.accountID> Save;
  public PXCancel<Account> Cancel;
  [PXImport(typeof (Account))]
  [PXFilterable(new Type[] {})]
  public PXSelect<Account, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<Account.accountCD>>> AccountRecords;
  public PXSelectReadonly<PX.Objects.GL.GLSetup> GLSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public CMSetupSelect cmsetup;
  public PXFilter<AccountMaint.AccountTypeDialogBoxParameters> AccountTypeChangePrepare;
  protected bool? IsCOAOrderVisible;
  public PXAction<Account> viewRestrictionGroups;
  public PXAction<Account> accountByPeriodEnq;

  public PX.Objects.GL.GLSetup GLSETUP
  {
    get
    {
      PX.Objects.GL.GLSetup glsetup = PXResultset<PX.Objects.GL.GLSetup>.op_Implicit(((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Select(Array.Empty<object>()));
      if (glsetup == null)
      {
        glsetup = new PX.Objects.GL.GLSetup();
        glsetup.COAOrder = new short?((short) 0);
      }
      return glsetup;
    }
  }

  public AccountMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
    if (!this.IsCOAOrderVisible.HasValue)
    {
      short? coaOrder = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.COAOrder;
      int? nullable = coaOrder.HasValue ? new int?((int) coaOrder.GetValueOrDefault()) : new int?();
      int num = 3;
      this.IsCOAOrderVisible = new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
      PXUIFieldAttribute.SetVisible<Account.cOAOrder>(((PXSelectBase) this.AccountRecords).Cache, (object) null, this.IsCOAOrderVisible.Value);
      PXUIFieldAttribute.SetEnabled<Account.cOAOrder>(((PXSelectBase) this.AccountRecords).Cache, (object) null, this.IsCOAOrderVisible.Value);
    }
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<Account.curyID>(((PXSelectBase) this.AccountRecords).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<Account.curyID>(((PXSelectBase) this.AccountRecords).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<Account.revalCuryRateTypeId>(((PXSelectBase) this.AccountRecords).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<Account.revalCuryRateTypeId>(((PXSelectBase) this.AccountRecords).Cache, (object) null, flag);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public static Account FindAccountByCD(PXGraph graph, string accountCD)
  {
    return Account.UK.Find(graph, accountCD);
  }

  protected bool PostedTransInOtherCuryExists(Account account, string curyID)
  {
    return PXSelectBase<GLTran, PXSelectJoin<GLTran, InnerJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>, InnerJoin<Ledger, On<GLTran.ledgerID, Equal<Ledger.ledgerID>>>>, Where<GLTran.accountID, Equal<Current<Account.accountID>>, And<PX.Objects.CM.CurrencyInfo.curyID, NotEqual<Required<PX.Objects.CM.CurrencyInfo.curyID>>, And<GLTran.posted, Equal<True>, And<Ledger.balanceType, NotEqual<LedgerBalanceType.report>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) account
    }, new object[1]{ (object) curyID }).Count > 0;
  }

  protected virtual void Account_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Account row = (Account) e.Row;
    if (row == null)
      return;
    Exception exception = row == null || row.CuryID == null || !(row.CuryID != ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID) || row.RevalCuryRateTypeId != null ? (Exception) null : (Exception) new PXSetPropertyException("Revaluation Rate Type is not Defined", (PXErrorLevel) 2);
    sender.RaiseExceptionHandling<Account.revalCuryRateTypeId>((object) row, (object) row.RevalCuryRateTypeId, exception);
    PXUIFieldAttribute.SetEnabled<Account.curyID>(sender, (object) row, !row.IsCashAccount.GetValueOrDefault());
    PXCache pxCache1 = sender;
    Account account1 = row;
    bool? isCashAccount = row.IsCashAccount;
    int num1 = !isCashAccount.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Account.postOption>(pxCache1, (object) account1, num1 != 0);
    PXCache pxCache2 = sender;
    Account account2 = row;
    isCashAccount = row.IsCashAccount;
    int num2 = !isCashAccount.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Account.controlAccountModule>(pxCache2, (object) account2, num2 != 0);
    PXCache pxCache3 = sender;
    Account account3 = row;
    isCashAccount = row.IsCashAccount;
    int num3 = isCashAccount.GetValueOrDefault() ? 0 : (row.ControlAccountModule != null ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Account.allowManualEntry>(pxCache3, (object) account3, num3 != 0);
  }

  protected virtual void Account_COAOrder_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    bool? isCoaOrderVisible = this.IsCOAOrderVisible;
    bool flag = false;
    if (!(isCoaOrderVisible.GetValueOrDefault() == flag & isCoaOrderVisible.HasValue) || e.Row == null || string.IsNullOrEmpty(((Account) e.Row).Type))
      return;
    e.NewValue = (object) Convert.ToInt16(AccountType.COAOrderOptions[(int) ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.COAOrder.Value].Substring(AccountType.Ordinal(((Account) e.Row).Type), 1));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Account_COAOrder_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    bool? isCoaOrderVisible = this.IsCOAOrderVisible;
    bool flag = false;
    if (!(isCoaOrderVisible.GetValueOrDefault() == flag & isCoaOrderVisible.HasValue) || e.Row == null || string.IsNullOrEmpty(((Account) e.Row).Type))
      return;
    e.NewValue = (object) Convert.ToInt16(AccountType.COAOrderOptions[(int) ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.COAOrder.Value].Substring(AccountType.Ordinal(((Account) e.Row).Type), 1));
  }

  protected virtual void Account_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool? isCoaOrderVisible = this.IsCOAOrderVisible;
    bool flag = false;
    if (!(isCoaOrderVisible.GetValueOrDefault() == flag & isCoaOrderVisible.HasValue))
      return;
    sender.SetDefaultExt<Account.cOAOrder>(e.Row);
  }

  protected virtual void Account_ControlAccountModule_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    Account row = (Account) e.Row;
    if (row.ControlAccountModule != null)
      return;
    row.AllowManualEntry = new bool?(false);
  }

  protected virtual void Account_AllowManualEntry_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || !((bool?) e.NewValue).GetValueOrDefault() || ((Account) e.Row).ControlAccountModule != null)
      return;
    e.NewValue = (object) false;
  }

  protected virtual void Account_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (e.NewRow == null)
      return;
    Account newRow = (Account) e.NewRow;
    try
    {
      this.ValidateAccountGroupID(sender, newRow);
    }
    catch (PXSetPropertyException ex)
    {
      if (ex.ErrorLevel == 4)
      {
        PMAccountGroup pmAccountGroup = (PMAccountGroup) PXSelectorAttribute.Select<Account.accountGroupID>(sender, (object) newRow);
        sender.RaiseExceptionHandling<Account.accountGroupID>((object) newRow, (object) pmAccountGroup.GroupCD, (Exception) ex);
      }
      else
        sender.RaiseExceptionHandling<Account.accountGroupID>((object) newRow, (object) newRow.AccountGroupID, (Exception) ex);
    }
  }

  private void ValidateAccountGroupID(PXCache sender, Account account)
  {
    if (!account.AccountGroupID.HasValue)
      return;
    if (account.IsCashAccount.GetValueOrDefault())
      throw new PXSetPropertyException("The {0} cash account is not recommended to be used for project purposes.", (PXErrorLevel) 2, new object[1]
      {
        (object) account.AccountCD
      });
    AccountAttribute.VerifyAccountIsNotControl(account);
  }

  protected virtual void Account_Type_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Account row = e.Row as Account;
    if (!row.Active.HasValue || !(row.Type != (string) e.NewValue))
      return;
    int? nullable1 = row.AccountID;
    if (!nullable1.HasValue)
      return;
    if (GLUtility.IsAccountHistoryExist((PXGraph) this, row.AccountID))
    {
      if ((string.IsNullOrEmpty(PredefinedRoles.FinancialSupervisor) ? 0 : (PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor) ? 1 : 0)) == 0)
        throw new PXSetPropertyException("To change the type of an account to which GL transactions were posted, the user must have the Financial Supervisor role assigned.");
      if ((!EnumerableExtensions.IsIn<string>(row.Type, "A", "L") || !EnumerableExtensions.IsIn<object>(e.NewValue, (object) "A", (object) "L")) && (!EnumerableExtensions.IsIn<string>(row.Type, "I", "E") || !EnumerableExtensions.IsIn<object>(e.NewValue, (object) "I", (object) "E")) && row.Type != null)
      {
        var data1;
        switch (row.Type)
        {
          case "A":
            data1 = new
            {
              OldAccountType = "Asset",
              NewAccountType = "Liability"
            };
            break;
          case "L":
            data1 = new
            {
              OldAccountType = "Liability",
              NewAccountType = "Asset"
            };
            break;
          case "I":
            data1 = new
            {
              OldAccountType = "Income",
              NewAccountType = "Expense"
            };
            break;
          case "E":
            data1 = new
            {
              OldAccountType = "Expense",
              NewAccountType = "Income"
            };
            break;
          default:
            data1 = new
            {
              OldAccountType = string.Empty,
              NewAccountType = string.Empty
            };
            break;
        }
        var data2 = data1;
        throw new PXSetPropertyException("The type of this account is {0}, and it can be changed to the {1} type only.", new object[2]
        {
          (object) data2.OldAccountType,
          (object) data2.NewAccountType
        });
      }
    }
    nullable1 = row.AccountID;
    int? nullable2 = (int?) ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current?.YtdNetIncAccountID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      throw new PXSetPropertyException("The account type cannot be changed because the account is specified as {0} account on the General Ledger Preferences (GL102000) form.", new object[1]
      {
        (object) "YTD Net Income"
      });
    nullable2 = row.AccountID;
    nullable1 = (int?) ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current?.RetEarnAccountID;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      throw new PXSetPropertyException("The account type cannot be changed because the account is specified as {0} account on the General Ledger Preferences (GL102000) form.", new object[1]
      {
        (object) "Retained Earnings"
      });
  }

  protected virtual void Account_CuryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    Account row = e.Row as Account;
    if (row.CuryID != null && row.CuryID != ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID)
      row.RevalCuryRateTypeId = ((PXSelectBase<CMSetup>) this.cmsetup).Current.GLRateTypeReval;
    else
      row.RevalCuryRateTypeId = (string) null;
  }

  protected virtual void Account_RevalCuryRateTypeId_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Account row = e.Row as Account;
    if ((string) e.NewValue != null && (row.CuryID == null || row.CuryID == ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID) && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      throw new PXSetPropertyException("Revaluation Rate Type can only be entered for a GL Account that is denominated in foreign currency.");
  }

  protected virtual void Account_GLConsolAccountCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Account row))
      return;
    int? accountId = row.AccountID;
    int? ytdNetIncAccountId = this.GLSETUP.YtdNetIncAccountID;
    if (accountId.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & accountId.HasValue == ytdNetIncAccountId.HasValue && e.NewValue != null)
      throw new PXSetPropertyException("A consolidation account cannot be specified for the YTD Net Income account because the consolidation of the YTD Net Income account data is prohibited.");
  }

  protected virtual void Account_CuryID_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if (!(cache.Locate(e.Row) is Account account))
      return;
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(account.CuryID) && !string.IsNullOrEmpty(newValue))
    {
      if (((IQueryable<PXResult<BranchAcctMap>>) PXSelectBase<BranchAcctMap, PXSelect<BranchAcctMap, Where<BranchAcctMap.mapAccountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) account.AccountID
      })).Any<PXResult<BranchAcctMap>>())
        throw new PXSetPropertyException("A currency cannot be assigned to the selected account because this account is specified as an offset one on the Inter-branch Account Mapping (GL101010) form.");
      if (!this.PostedTransInOtherCuryExists(account, newValue))
        return;
    }
    if (account.CuryID != newValue && (newValue != null || account.IsCashAccount.GetValueOrDefault()) && GLUtility.IsAccountHistoryExist((PXGraph) this, account.AccountID))
      throw new PXSetPropertyException("You cannot change denomination currency for the selected GL Account because transactions for the existing currency exist for this GL Account.");
    if (account.IsCashAccount.GetValueOrDefault() && string.IsNullOrEmpty(newValue))
      throw new PXSetPropertyException("You cannot delete denomination currency for selected Cash Account.");
  }

  protected virtual void Account_AccountClassID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<Account.type>(e.Row);
  }

  protected virtual void Account_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!string.IsNullOrEmpty(((Account) e.Row).CuryID))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<Account, Account.active> e)
  {
    if (e.Row == null || ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Account, Account.active>, Account, object>) e).NewValue).GetValueOrDefault())
      return;
    if (((IQueryable<PXResult<BranchAcctMap>>) PXSelectBase<BranchAcctMap, PXViewOf<BranchAcctMap>.BasedOn<SelectFromBase<BranchAcctMap, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BranchAcctMap.mapAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.AccountID
    })).Any<PXResult<BranchAcctMap>>())
    {
      IEnumerable<Branch> list = (IEnumerable<Branch>) PXSelectBase<Branch, PXViewOf<Branch>.BasedOn<SelectFromBase<Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BranchAcctMap>.On<BqlOperand<BranchAcctMap.branchID, IBqlInt>.IsEqual<Branch.branchID>>>>.Where<BqlOperand<BranchAcctMap.mapAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.AccountID
      }).FirstTableItems.ToList<Branch>();
      throw new PXSetPropertyException((IBqlTable) e.Row, "The {0} account cannot be deactivated because it is used on the Inter-Branch Account Mapping (GL101010) form for the following branches: {1}.", new object[2]
      {
        (object) e.Row.AccountCD,
        (object) ((ICollection<string>) list.Select<Branch, string>((Func<Branch, string>) (b => b.BranchCD.Trim())).Distinct<string>().ToArray<string>()).JoinIntoStringForMessage<string>()
      });
    }
  }

  public virtual int Persist(Type cacheType, PXDBOperation operation)
  {
    if (operation == 1)
    {
      List<Account> list1 = GraphHelper.RowCast<Account>(((PXSelectBase) this.AccountRecords).Cache.Updated).Where<Account>((Func<Account, bool>) (x => ((Account) ((PXSelectBase) this.AccountRecords).Cache.GetOriginal((object) x)).Type != x.Type)).ToList<Account>();
      if (list1.Count > 0)
      {
        PXResultset<Account> pxResultset = PXSelectBase<Account, PXSelectJoinGroupBy<Account, InnerJoin<GLHistory, On<Account.accountID, Equal<GLHistory.accountID>>>, Where<GLHistory.accountID, In<Required<GLHistory.accountID>>>, Aggregate<GroupBy<GLHistory.accountID>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) list1.Select<Account, int?>((Func<Account, int?>) (x => x.AccountID)).ToArray<int?>()
        });
        bool flag1 = ((IQueryable<PXResult<GLTran>>) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.accountID, In<Required<Account.accountID>>, And<GLTran.posted, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) list1.Select<Account, int?>((Func<Account, int?>) (x => x.AccountID)).ToArray<int?>()
        })).Any<PXResult<GLTran>>();
        bool flag2 = ((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.accountID, In<Required<Account.accountID>>, Or<PMTran.offsetAccountID, In<Required<Account.accountID>>, And<PMTran.released, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) list1.Select<Account, int?>((Func<Account, int?>) (x => x.AccountID)).ToArray<int?>(),
          (object) list1.Select<Account, int?>((Func<Account, int?>) (x => x.AccountID)).ToArray<int?>()
        })).Any<PXResult<PMTran>>();
        if (pxResultset.Count > 0 | flag2)
        {
          List<string> list2 = pxResultset.FirstTableItems.ToList<Account>().Select<Account, string>((Func<Account, string>) (x => x.AccountCD)).ToList<string>();
          if (flag2 & flag1)
          {
            ((PXSelectBase<AccountMaint.AccountTypeDialogBoxParameters>) this.AccountTypeChangePrepare).Current.Message = PXLocalizer.LocalizeFormat("Changing the type of accounts may affect financial statements because general ledger transactions were posted to these accounts: {0}.<br><br>After you save the changes, the system will run the process of recalculating balances for projects that include transactions with these accounts. This operation may take a significant amount of time.", new object[1]
            {
              (object) string.Join(", ", (IEnumerable<string>) list2)
            });
            PXTrace.Logger.Information(PXLocalizer.LocalizeFormat("Changing the type of accounts may affect financial statements because general ledger transactions were posted to these accounts: {0}.<br><br>After you save the changes, the system will run the process of recalculating balances for projects that include transactions with these accounts. This operation may take a significant amount of time.", new object[1]
            {
              (object) string.Join(", ", (IEnumerable<string>) list2)
            }));
          }
          else if (flag2 && !flag1)
          {
            ((PXSelectBase<AccountMaint.AccountTypeDialogBoxParameters>) this.AccountTypeChangePrepare).Current.Message = "After you save the changes, the system will run the process of recalculating balances for projects that include transactions with these accounts. This operation may take a significant amount of time.";
            PXTrace.Logger.Information("After you save the changes, the system will run the process of recalculating balances for projects that include transactions with these accounts. This operation may take a significant amount of time.");
          }
          else
          {
            ((PXSelectBase<AccountMaint.AccountTypeDialogBoxParameters>) this.AccountTypeChangePrepare).Current.Message = PXLocalizer.LocalizeFormat("Changing the type of accounts may affect financial statements because general ledger transactions were posted to these accounts: {0}.", new object[1]
            {
              (object) string.Join(", ", (IEnumerable<string>) list2)
            });
            PXTrace.Logger.Information(PXLocalizer.LocalizeFormat("Changing the type of accounts may affect financial statements because general ledger transactions were posted to these accounts: {0}.", new object[1]
            {
              (object) string.Join(", ", (IEnumerable<string>) list2)
            }));
          }
          if (((PXSelectBase<AccountMaint.AccountTypeDialogBoxParameters>) this.AccountTypeChangePrepare).AskExt() == 1 & flag2)
            PXGraph.CreateInstance<ProjectBalanceValidationProcess>().RebuildBalanceOnAccountTypeChange((IList<Account>) list1);
          else if (((PXSelectBase<AccountMaint.AccountTypeDialogBoxParameters>) this.AccountTypeChangePrepare).AskExt() == 7)
          {
            foreach (Account account in list1)
              ((PXSelectBase) this.AccountRecords).Cache.SetValueExt<Account.type>((object) (Account) ((PXSelectBase) this.AccountRecords).Cache.Locate((object) account), (object) ((Account) ((PXSelectBase) this.AccountRecords).Cache.GetOriginal((object) account)).Type);
          }
        }
      }
    }
    return ((PXGraph) this).Persist(cacheType, operation);
  }

  protected virtual void Account_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    Account row = (Account) e.Row;
    try
    {
      this.ValidateAccountGroupID(sender, row);
    }
    catch (PXSetPropertyException ex)
    {
      if (ex.ErrorLevel == 4)
      {
        PMAccountGroup pmAccountGroup = (PMAccountGroup) PXSelectorAttribute.Select<Account.accountGroupID>(sender, (object) row);
        sender.RaiseExceptionHandling<Account.accountGroupID>((object) row, (object) pmAccountGroup.GroupCD, (Exception) ex);
        throw ex;
      }
    }
    if (!string.IsNullOrEmpty(row.CuryID))
    {
      CASetup caSetup = PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (caSetup != null)
      {
        int? transitAcctId = caSetup.TransitAcctId;
        if (transitAcctId.HasValue)
        {
          transitAcctId = caSetup.TransitAcctId;
          int? accountId = row.AccountID;
          if (transitAcctId.GetValueOrDefault() == accountId.GetValueOrDefault() & transitAcctId.HasValue == accountId.HasValue)
          {
            PXException pxException = new PXException("Cash-In-Transit Account can not be Cash Account or denominated one.");
            sender.RaiseExceptionHandling<Account.curyID>((object) row, (object) row.CuryID, (Exception) pxException);
            throw pxException;
          }
        }
      }
      if (e.Operation == 1)
      {
        string curyId = row.CuryID;
        byte[] numArray = PXDatabase.SelectTimeStamp();
        PXDatabase.Update<GLHistory>(new PXDataFieldParam[4]
        {
          (PXDataFieldParam) new PXDataFieldAssign("CuryID", (object) curyId),
          (PXDataFieldParam) new PXDataFieldRestrict("AccountID", (object) ((Account) e.Row).AccountID),
          (PXDataFieldParam) new PXDataFieldRestrict("CuryID", (PXDbType) 22, new int?(5), (object) null, (PXComp) 6),
          (PXDataFieldParam) new PXDataFieldRestrict("tstamp", (PXDbType) 19, new int?(8), (object) numArray, (PXComp) 5)
        });
      }
    }
    string valueOriginal = sender.GetValueOriginal<Account.type>((object) row) as string;
    bool flag = ((IQueryable<PXResult<GLTran>>) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.accountID, Equal<Required<GLTran.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AccountID
    })).Any<PXResult<GLTran>>();
    if (((valueOriginal == null ? 0 : (valueOriginal != row.Type ? 1 : 0)) & (flag ? 1 : 0)) == 0)
      return;
    this.UpdateGLHistoryDetails(row.AccountID);
  }

  protected virtual void UpdateGLHistoryDetails(int? accountId)
  {
    PXUpdate<Set<GLHistory.finYtdBalance, Minus<GLHistory.finYtdBalance>, Set<GLHistory.tranYtdBalance, Minus<GLHistory.tranYtdBalance>, Set<GLHistory.curyFinYtdBalance, Minus<GLHistory.curyFinYtdBalance>, Set<GLHistory.curyTranYtdBalance, Minus<GLHistory.curyTranYtdBalance>, Set<GLHistory.finBegBalance, Minus<GLHistory.finBegBalance>, Set<GLHistory.tranBegBalance, Minus<GLHistory.tranBegBalance>, Set<GLHistory.curyFinBegBalance, Minus<GLHistory.curyFinBegBalance>, Set<GLHistory.curyTranBegBalance, Minus<GLHistory.curyTranBegBalance>>>>>>>>>, GLHistory, Where<GLHistory.accountID, Equal<Required<GLHistory.accountID>>, And<Where<GLHistory.balanceType, Equal<LedgerBalanceType.actual>, Or<GLHistory.balanceType, Equal<LedgerBalanceType.report>, Or<GLHistory.balanceType, Equal<LedgerBalanceType.statistical>>>>>>>.Update((PXGraph) this, new object[1]
    {
      (object) accountId
    });
    PXUpdateJoin<Set<GLHistory.finPtdDebit, GLHistory2.finPtdCredit, Set<GLHistory.finPtdCredit, GLHistory2.finPtdDebit, Set<GLHistory.tranPtdDebit, GLHistory2.tranPtdCredit, Set<GLHistory.tranPtdCredit, GLHistory2.tranPtdDebit, Set<GLHistory.curyFinPtdDebit, GLHistory2.curyFinPtdCredit, Set<GLHistory.curyFinPtdCredit, GLHistory2.curyFinPtdDebit, Set<GLHistory.curyTranPtdDebit, GLHistory2.curyTranPtdCredit, Set<GLHistory.curyTranPtdCredit, GLHistory2.curyTranPtdDebit>>>>>>>>, GLHistory, LeftJoin<GLHistory2, On<GLHistory2.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistory2.branchID, Equal<GLHistory.branchID>, And<GLHistory2.accountID, Equal<GLHistory.accountID>, And<GLHistory2.subID, Equal<GLHistory.subID>, And<GLHistory2.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>>, Where<GLHistory.accountID, Equal<Required<GLHistory.accountID>>, And<GLHistory.balanceType, Equal<LedgerBalanceType.budget>>>>.Update((PXGraph) this, new object[1]
    {
      (object) accountId
    });
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<Account>) this.AccountRecords).Current != null)
    {
      GLAccessByAccount instance = PXGraph.CreateInstance<GLAccessByAccount>();
      ((PXSelectBase<Account>) instance.Account).Current = PXResultset<Account>.op_Implicit(((PXSelectBase<Account>) instance.Account).Search<Account.accountCD>((object) ((PXSelectBase<Account>) this.AccountRecords).Current.AccountCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AccountByPeriodEnq(PXAdapter adapter)
  {
    if (((PXSelectBase<Account>) this.AccountRecords).Current != null)
    {
      AccountHistoryByYearEnq instance = PXGraph.CreateInstance<AccountHistoryByYearEnq>();
      ((PXSelectBase<AccountByYearFilter>) instance.Filter).Current.AccountID = ((PXSelectBase<Account>) this.AccountRecords).Current.AccountID;
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Account by Period");
    }
    return adapter.Get();
  }

  public static Account GetAccountByCD(PXGraph graph, string accountCD)
  {
    return Account.UK.Find(graph, accountCD) ?? throw new PXException("Cannot find account {0} in the source company. Verify account mapping on the Chart of Accounts (GL202500) form.", new object[1]
    {
      (object) accountCD
    });
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName != "AccountRecords")
      return true;
    PXCache<Account> pxCache = PXContext.GetSlot<PXCache<Account>>();
    if (pxCache == null)
    {
      pxCache = GraphHelper.Caches<Account>((PXGraph) GraphHelper.Clone<AccountMaint>(this));
      PXContext.SetSlot<PXCache<Account>>(pxCache);
    }
    Account account = ((PXCache) pxCache).Locate(keys) == 0 ? (Account) ((PXCache) pxCache).Insert() : (Account) ((PXCache) pxCache).Current;
    foreach (string key in values.Keys.ToArray<string>())
    {
      if (((PXCache) pxCache).Fields.Contains(key))
      {
        object stateExt = ((PXCache) pxCache).GetStateExt((object) account, key);
        PXFieldState pxFieldState = stateExt is PXFieldState ? (PXFieldState) stateExt : (PXFieldState) null;
        if (pxFieldState != null && (!pxFieldState.Enabled || !pxFieldState.Visible))
        {
          values[(object) key] = ((PXCache) pxCache).GetValue((object) account, key);
        }
        else
        {
          object obj = values[(object) key];
          ((PXCache) pxCache).SetValue((object) account, key, obj is string str ? ((PXCache) pxCache).ValueFromString(key, str) : obj);
          pxCache.RaiseRowSelected(account);
        }
      }
    }
    return true;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public bool RowImporting(string viewName, object row) => row == null;

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [PXHidden]
  [Serializable]
  public class AccountTypeDialogBoxParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    public virtual string Message { get; set; }

    public abstract class message : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AccountMaint.AccountTypeDialogBoxParameters.message>
    {
    }
  }
}
