// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AccountGroupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class AccountGroupMaint : PXGraph<
#nullable disable
AccountGroupMaint, PMAccountGroup>
{
  public PXSelect<PMAccountGroup> AccountGroup;
  public PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Current<PMAccountGroup.groupID>>>> AccountGroupProperties;
  [PXCopyPasteHiddenView]
  [PXVirtualDAC]
  public PXSelect<AccountGroupMaint.AccountPtr> Accounts;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.GL.Account> GLAccount;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Current<PMAccountGroup.groupID>>, And<PMBudget.type, NotEqual<Required<PMBudget.type>>>>> BudgetToFix;
  [PXViewName("Account Group Answers")]
  public CRAttributeList<PMAccountGroup> Answers;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  public IEnumerable accounts()
  {
    AccountGroupMaint accountGroupMaint = this;
    Dictionary<int, AccountGroupMaint.AccountPtr> inCache = new Dictionary<int, AccountGroupMaint.AccountPtr>();
    int? accountId1;
    foreach (AccountGroupMaint.AccountPtr accountPtr1 in ((PXSelectBase) accountGroupMaint.Accounts).Cache.Cached)
    {
      PXEntryStatus status = ((PXSelectBase) accountGroupMaint.Accounts).Cache.GetStatus((object) accountPtr1);
      Dictionary<int, AccountGroupMaint.AccountPtr> dictionary = inCache;
      accountId1 = accountPtr1.AccountID;
      int key = accountId1.Value;
      AccountGroupMaint.AccountPtr accountPtr2 = accountPtr1;
      dictionary.Add(key, accountPtr2);
      if (status == 2 || status == 1 || status == null)
        yield return (object) accountPtr1;
    }
    PXSelectBase<PX.Objects.GL.Account> pxSelectBase = (PXSelectBase<PX.Objects.GL.Account>) new PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountGroupID, Equal<Current<PMAccountGroup.groupID>>>>((PXGraph) accountGroupMaint);
    bool isDirty = ((PXSelectBase) accountGroupMaint.Accounts).Cache.IsDirty;
    foreach (PXResult<PX.Objects.GL.Account> pxResult in pxSelectBase.Select(Array.Empty<object>()))
    {
      PX.Objects.GL.Account account = PXResult<PX.Objects.GL.Account>.op_Implicit(pxResult);
      Dictionary<int, AccountGroupMaint.AccountPtr> dictionary = inCache;
      accountId1 = account.AccountID;
      int key = accountId1.Value;
      if (!dictionary.ContainsKey(key))
      {
        AccountGroupMaint.AccountPtr accountPtr3 = new AccountGroupMaint.AccountPtr();
        accountPtr3.AccountID = account.AccountID;
        accountPtr3.AccountClassID = account.AccountClassID;
        accountPtr3.CuryID = account.CuryID;
        accountPtr3.Description = account.Description;
        accountPtr3.Type = account.Type;
        AccountGroupMaint.AccountPtr accountPtr4 = accountPtr3;
        accountId1 = ((PXSelectBase<PMAccountGroup>) accountGroupMaint.AccountGroup).Current.AccountID;
        int? accountId2 = account.AccountID;
        bool? nullable = new bool?(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue);
        accountPtr4.IsDefault = nullable;
        AccountGroupMaint.AccountPtr accountPtr5 = ((PXSelectBase<AccountGroupMaint.AccountPtr>) accountGroupMaint.Accounts).Insert(accountPtr3);
        ((PXSelectBase) accountGroupMaint.Accounts).Cache.SetStatus((object) accountPtr5, (PXEntryStatus) 0);
        yield return (object) accountPtr5;
      }
    }
    ((PXSelectBase) accountGroupMaint.Accounts).Cache.IsDirty = isDirty;
  }

  public AccountGroupMaint()
  {
    PXUIFieldAttribute.SetVisible<AccountGroupMaint.AccountPtr.curyID>(((PXSelectBase) this.Accounts).Cache, (object) null, this.IsMultiCurrency);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<AccountGroupMaint.AccountPtr> e)
  {
    AccountGroupMaint.AccountPtr row = e.Row;
    AccountGroupMaint.AccountPtr oldRow = e.OldRow;
    if (row == null)
      return;
    PX.Objects.GL.Account accountById = this.GetAccountByID(row.AccountID);
    if (accountById != null)
    {
      row.AccountClassID = accountById.AccountClassID;
      row.CuryID = accountById.CuryID;
      row.Description = accountById.Description;
      row.Type = accountById.Type;
    }
    bool flag1 = false;
    bool? isDefault1 = row.IsDefault;
    bool? isDefault2 = oldRow.IsDefault;
    if (!(isDefault1.GetValueOrDefault() == isDefault2.GetValueOrDefault() & isDefault1.HasValue == isDefault2.HasValue))
    {
      if (row.IsDefault.GetValueOrDefault())
      {
        foreach (PXResult<AccountGroupMaint.AccountPtr> pxResult in ((PXSelectBase<AccountGroupMaint.AccountPtr>) this.Accounts).Select(Array.Empty<object>()))
        {
          AccountGroupMaint.AccountPtr accountPtr = PXResult<AccountGroupMaint.AccountPtr>.op_Implicit(pxResult);
          int? accountId1 = accountPtr.AccountID;
          int? accountId2 = row.AccountID;
          if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue) && accountPtr.IsDefault.GetValueOrDefault())
          {
            ((PXSelectBase) this.Accounts).Cache.SetValue<AccountGroupMaint.AccountPtr.isDefault>((object) accountPtr, (object) false);
            GraphHelper.SmartSetStatus(((PXSelectBase) this.Accounts).Cache, (object) accountPtr, (PXEntryStatus) 1, (PXEntryStatus) 0);
            flag1 = true;
          }
        }
      }
    }
    else
    {
      bool flag2 = false;
      foreach (PXResult<AccountGroupMaint.AccountPtr> pxResult in ((PXSelectBase<AccountGroupMaint.AccountPtr>) this.Accounts).Select(Array.Empty<object>()))
      {
        AccountGroupMaint.AccountPtr accountPtr = PXResult<AccountGroupMaint.AccountPtr>.op_Implicit(pxResult);
        int? accountId3 = accountPtr.AccountID;
        int? accountId4 = row.AccountID;
        if (!(accountId3.GetValueOrDefault() == accountId4.GetValueOrDefault() & accountId3.HasValue == accountId4.HasValue) && accountPtr.IsDefault.GetValueOrDefault())
          flag2 = true;
      }
      if (!flag2)
        row.IsDefault = new bool?(true);
    }
    if (!flag1)
      return;
    ((PXSelectBase) this.Accounts).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<AccountGroupMaint.AccountPtr> e)
  {
    AccountAttribute.VerifyAccountIsNotControl<AccountGroupMaint.AccountPtr.accountID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<AccountGroupMaint.AccountPtr>>) e).Cache, (EventArgs) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<AccountGroupMaint.AccountPtr>>) e).Args);
    AccountGroupMaint.AccountPtr row = e.Row;
    if (row == null)
      return;
    PXDBOperation operation = e.Operation;
    if (operation - 1 > 1)
    {
      if (operation == 3)
        this.RemoveAccount(row);
    }
    else
      this.AddAccount(row);
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID> e)
  {
    AccountAttribute.VerifyAccountIsNotControl<AccountGroupMaint.AccountPtr.accountID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>>) e).Cache, (EventArgs) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>>) e).Args);
    AccountGroupMaint.AccountPtr row = e.Row;
    if (row == null)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account>.Config>.Search<PX.Objects.GL.Account.accountID>((PXGraph) this, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>, AccountGroupMaint.AccountPtr, object>) e).NewValue, Array.Empty<object>()));
    if (account != null && account.IsCashAccount.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>>) e).Cache.RaiseExceptionHandling<AccountGroupMaint.AccountPtr.accountID>((object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>, AccountGroupMaint.AccountPtr, object>) e).NewValue, (Exception) new PXSetPropertyException("The {0} cash account is not recommended to be used for project purposes.", (PXErrorLevel) 2, new object[1]
      {
        (object) account.AccountCD
      }));
    if (account == null || ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current == null)
      return;
    int? accountGroupId = account.AccountGroupID;
    if (!accountGroupId.HasValue)
      return;
    accountGroupId = account.AccountGroupID;
    int? groupId = ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current.GroupID;
    if (accountGroupId.GetValueOrDefault() == groupId.GetValueOrDefault() & accountGroupId.HasValue == groupId.HasValue)
      return;
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, account.AccountGroupID);
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>>) e).Cache.RaiseExceptionHandling<AccountGroupMaint.AccountPtr.accountID>((object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<AccountGroupMaint.AccountPtr, AccountGroupMaint.AccountPtr.accountID>, AccountGroupMaint.AccountPtr, object>) e).NewValue, (Exception) new PXSetPropertyException("This account is already added to the '{0}' account group. By clicking Save, you will move the account to the currently selected account group.", (PXErrorLevel) 2, new object[1]
    {
      (object) pmAccountGroup.GroupCD
    }));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<AccountGroupMaint.AccountPtr> e)
  {
    AccountGroupMaint.AccountPtr row = e.Row;
    if (e.Row == null)
      return;
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (pmSetup == null)
      return;
    int? accountId = row.AccountID;
    int? remainderAccountId = pmSetup.UnbilledRemainderAccountID;
    if (accountId.GetValueOrDefault() == remainderAccountId.GetValueOrDefault() & accountId.HasValue == remainderAccountId.HasValue)
    {
      e.Cancel = true;
      throw new PXSetPropertyException("The account cannot be deleted from the account group because it is selected as the Debit Account in the Unbilled Remainders section on the Project Preferences (PM101000) form.", (PXErrorLevel) 4);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<AccountGroupMaint.AccountPtr> e)
  {
    ((PXSelectBase) this.AccountGroup).Cache.IsDirty = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMAccountGroup> e)
  {
    PMAccountGroup row = e.Row;
    if (row == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache;
    PMAccountGroup pmAccountGroup1 = row;
    short? coaOrder = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.COAOrder;
    int num1 = (coaOrder.HasValue ? new int?((int) coaOrder.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 4 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAccountGroup.sortOrder>(cache1, (object) pmAccountGroup1, num1 != 0);
    bool flag1 = row.Type != "O";
    ((PXSelectBase) this.Accounts).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Accounts).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Accounts).Cache.AllowUpdate = flag1;
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.type>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache, (object) row, !this.IsAccountsExist() && !this.IsBalanceExist());
    PXUIFieldAttribute.SetVisible<PMAccountGroup.isExpense>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache, (object) row, this.DisplayIsExpenseToggleForOffBalance() && row.Type == "O");
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache;
    PMAccountGroup pmAccountGroup2 = row;
    bool? isExpense = row.IsExpense;
    int num2 = isExpense.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.revenueAccountGroupID>(cache2, (object) pmAccountGroup2, num2 != 0);
    int num3;
    if (row.Type == "O")
    {
      isExpense = row.IsExpense;
      if (isExpense.GetValueOrDefault())
      {
        num3 = 1;
        goto label_5;
      }
    }
    num3 = row.Type == "E" ? 1 : 0;
label_5:
    bool flag2 = num3 != 0;
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.createsCommitment>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.defaultLineMarkupPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMAccountGroup>>) e).Cache, (object) row, flag2);
  }

  public virtual bool DisplayIsExpenseToggleForOffBalance() => true;

  protected virtual void _(PX.Data.Events.RowDeleting<PMAccountGroup> e)
  {
    PMAccountGroup row = e.Row;
    if (row == null)
      return;
    if (this.IsAccountsExist())
    {
      e.Cancel = true;
      throw new PXException("Account Group cannot be deleted. One or more Accounts are mapped to this Account Group.");
    }
    if (PXResultset<PMBudget>.op_Implicit(PXSelectBase<PMBudget, PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Required<PMAccountGroup.groupID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.GroupID
    })) != null)
    {
      e.Cancel = true;
      throw new PXException("The {0} account group cannot be deleted because it is used in at least one project budget line.", new object[1]
      {
        (object) row.GroupCD.Trim()
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMAccountGroup, PMAccountGroup.isActive> e)
  {
    if (e.Row != null && !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMAccountGroup, PMAccountGroup.isActive>, PMAccountGroup, object>) e).NewValue).GetValueOrDefault() && this.IsAccountsExist())
      throw new PXSetPropertyException("Account Group cannot be deactivated. One or more Accounts are mapped to this Account Group.", (PXErrorLevel) 4);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMAccountGroup, PMAccountGroup.type> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMAccountGroup, PMAccountGroup.type>>) e).Cache.SetDefaultExt<PMAccountGroup.sortOrder>((object) e.Row);
    if (!(e.Row.Type != "O"))
      return;
    e.Row.IsExpense = new bool?(e.Row.Type == "E");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMAccountGroup> e)
  {
    if (e.Row == null || !(e.Row.Type == "O"))
      return;
    bool? isExpense1 = e.Row.IsExpense;
    bool? isExpense2 = e.OldRow.IsExpense;
    if (isExpense1.GetValueOrDefault() == isExpense2.GetValueOrDefault() & isExpense1.HasValue == isExpense2.HasValue)
      return;
    string str = e.Row.IsExpense.GetValueOrDefault() ? "E" : "O";
    foreach (PXResult<PMBudget> pxResult in ((PXSelectBase<PMBudget>) this.BudgetToFix).Select(new object[1]
    {
      (object) str
    }))
    {
      PMBudget pmBudget = PXResult<PMBudget>.op_Implicit(pxResult);
      pmBudget.Type = str;
      ((PXSelectBase<PMBudget>) this.BudgetToFix).Update(pmBudget);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.Type == "O")
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) (short) 5;
    }
    else
    {
      int startIndex = AccountType.Ordinal(e.Row.Type);
      PX.Objects.GL.GLSetup glSetup = PXResultset<PX.Objects.GL.GLSetup>.op_Implicit(PXSelectBase<PX.Objects.GL.GLSetup, PXSelect<PX.Objects.GL.GLSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (glSetup == null)
        return;
      if (glSetup.COAOrder.Value.ToString() == "128")
      {
        if (e.Row.Type == "I")
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) (short) 1;
        else if (e.Row.Type == "E")
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) (short) 2;
        else if (e.Row.Type == "A")
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) (short) 3;
        }
        else
        {
          if (!(e.Row.Type == "L"))
            return;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) (short) 4;
        }
      }
      else
      {
        string coaOrderOption = AccountType.COAOrderOptions[(int) glSetup.COAOrder.Value];
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMAccountGroup, PMAccountGroup.sortOrder>, PMAccountGroup, object>) e).NewValue = (object) short.Parse(coaOrderOption.Substring(startIndex, 1));
      }
    }
  }

  private bool IsAccountsExist()
  {
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountGroupID, Equal<Current<PMAccountGroup.groupID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (account == null)
    {
      IEnumerator enumerator = ((PXSelectBase) this.Accounts).Cache.Inserted.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          return true;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    return account != null;
  }

  private bool IsBalanceExist()
  {
    return PXResultset<PMBudget>.op_Implicit(PXSelectBase<PMBudget, PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Current<PMAccountGroup.groupID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null;
  }

  protected virtual void RemoveAccount(AccountGroupMaint.AccountPtr ptr)
  {
    PX.Objects.GL.Account accountById = this.GetAccountByID(ptr.AccountID);
    if (accountById == null || !accountById.AccountGroupID.HasValue)
      return;
    accountById.AccountGroupID = new int?();
    ((PXSelectBase<PX.Objects.GL.Account>) this.GLAccount).Update(accountById);
    ((PXSelectBase) this.GLAccount).Cache.PersistUpdated((object) accountById);
  }

  protected virtual void AddAccount(AccountGroupMaint.AccountPtr ptr)
  {
    PX.Objects.GL.Account accountById = this.GetAccountByID(ptr.AccountID);
    if (accountById == null || ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current == null)
      return;
    accountById.AccountGroupID = ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current.GroupID;
    ((PXSelectBase<PX.Objects.GL.Account>) this.GLAccount).Update(accountById);
    ((PXSelectBase) this.GLAccount).Cache.PersistUpdated((object) accountById);
  }

  private PX.Objects.GL.Account GetAccountByID(int? id)
  {
    return PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) id
    }));
  }

  private bool IsMultiCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  public virtual void Persist()
  {
    foreach (PXResult<AccountGroupMaint.AccountPtr> pxResult in ((PXSelectBase<AccountGroupMaint.AccountPtr>) this.Accounts).Select(Array.Empty<object>()))
    {
      AccountGroupMaint.AccountPtr accountPtr = PXResult<AccountGroupMaint.AccountPtr>.op_Implicit(pxResult);
      if (accountPtr.IsDefault.GetValueOrDefault())
      {
        int? accountId1 = ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current.AccountID;
        int? accountId2 = accountPtr.AccountID;
        if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue))
        {
          ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current.AccountID = accountPtr.AccountID;
          ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Update(((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current);
        }
      }
    }
    ((PXGraph) this).Persist();
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class AccountPtr : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AccountID;
    protected string _Type;
    protected string _AccountClassID;
    protected string _Description;
    protected string _CuryID;
    protected bool? _IsDefault;

    [PMAccount(IsKey = true)]
    [AvoidControlAccounts]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXString(1)]
    [AccountType.List]
    [PXUIField(DisplayName = "Type", Enabled = false)]
    public virtual string Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    [PXString(20, IsUnicode = true)]
    [PXUIField(DisplayName = "Account Class", Enabled = false)]
    [PXSelector(typeof (AccountClass.accountClassID))]
    public virtual string AccountClassID
    {
      get => this._AccountClassID;
      set => this._AccountClassID = value;
    }

    [PXString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Enabled = false)]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency", Enabled = false)]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Default")]
    public virtual bool? IsDefault
    {
      get => this._IsDefault;
      set => this._IsDefault = value;
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccountGroupMaint.AccountPtr.accountID>
    {
    }

    public abstract class type : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountGroupMaint.AccountPtr.type>
    {
    }

    public abstract class accountClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccountGroupMaint.AccountPtr.accountClassID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccountGroupMaint.AccountPtr.description>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccountGroupMaint.AccountPtr.curyID>
    {
    }

    public abstract class isDefault : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AccountGroupMaint.AccountPtr.isDefault>
    {
    }
  }
}
