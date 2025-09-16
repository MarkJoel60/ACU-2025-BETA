// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents Account Field.
/// The Selector will return all accounts except 'YTD Net Income' account.
/// This attribute also tracks currency (which is supplied as curyField parameter) for the Account and
/// raises an error in case Denominated GL Account currency is different from transaction currency.
/// </summary>
[PXUIField]
[PXDBInt]
[PXInt]
[PXRestrictor(typeof (Where<Account.active, Equal<True>>), "Account is inactive.", new Type[] {})]
[PXRestrictor(typeof (Where<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>), "YTD Net Income Account cannot be used in this context.", new Type[] {})]
public class AccountAttribute : 
  PXEntityAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowPersistingSubscriber
{
  public const 
  #nullable disable
  string DimensionName = "ACCOUNT";
  private const string glSetup = "_GLSetup";
  private bool _SuppressCurrencyValidation;
  private string _ControlAccountForModule;
  private Type _ledgerID;
  private Type _branchID;
  private Type _curyKeyField;

  public bool SuppressCurrencyValidation
  {
    get => this._SuppressCurrencyValidation;
    set => this._SuppressCurrencyValidation = value;
  }

  public Type LedgerID
  {
    get => this._ledgerID;
    set => this._ledgerID = value;
  }

  public string ControlAccountForModule
  {
    get => this._ControlAccountForModule;
    set
    {
      this._ControlAccountForModule = value;
      if (value == null)
        return;
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<Account.isCashAccount, Equal<False>>), (string) null, Array.Empty<Type>()));
    }
  }

  /// <summary>
  /// Field, that returns module name, that is allowed to be control module for account, when AvoidControlAccounts is set to TRUE.
  /// </summary>
  public Type AllowControlAccountForModuleField { get; set; }

  public bool AvoidControlAccounts { get; set; }

  public AccountAttribute()
    : this((Type) null)
  {
  }

  public AccountAttribute(Type branchID)
    : this(branchID, typeof (Search<Account.accountID, Where<Match<Current<AccessInfo.userName>>>>))
  {
  }

  public AccountAttribute(Type branchID, Type SearchType)
  {
    if (SearchType == (Type) null)
      throw new PXArgumentException(nameof (SearchType), "The argument cannot be null.");
    if (branchID != (Type) null)
    {
      this._branchID = branchID;
      List<Type> typeList = new List<Type>()
      {
        SearchType.GetGenericTypeDefinition()
      };
      typeList.AddRange((IEnumerable<Type>) SearchType.GetGenericArguments());
      for (int index = 0; index < typeList.Count; ++index)
      {
        if (typeof (IBqlWhere).IsAssignableFrom(typeList[index]))
        {
          typeList[index] = BqlCommand.Compose(new Type[9]
          {
            typeof (Where2<,>),
            typeof (Match<>),
            typeof (Optional<>),
            branchID,
            typeof (And<,,>),
            typeof (Account.accountingType),
            this.GetAccountTypeValue(),
            typeof (And<>),
            typeList[index]
          });
          SearchType = BqlCommand.Compose(typeList.ToArray());
        }
      }
    }
    else
    {
      List<Type> typeList = new List<Type>()
      {
        SearchType.GetGenericTypeDefinition()
      };
      typeList.AddRange((IEnumerable<Type>) SearchType.GetGenericArguments());
      for (int index = 0; index < typeList.Count; ++index)
      {
        if (typeof (IBqlWhere).IsAssignableFrom(typeList[index]))
        {
          typeList[index] = BqlCommand.Compose(new Type[5]
          {
            typeof (Where<,,>),
            typeof (Account.accountingType),
            this.GetAccountTypeValue(),
            typeof (And<>),
            typeList[index]
          });
          SearchType = BqlCommand.Compose(typeList.ToArray());
        }
      }
    }
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("ACCOUNT", SearchType, typeof (Account.accountCD))
    {
      CacheGlobal = true,
      DescriptionField = typeof (Account.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  protected virtual Type GetAccountTypeValue() => typeof (Equal<AccountEntityType.gLAccount>);

  private static Type SearchKeyField(PXCache sender) => sender.GetBqlField("CuryInfoID");

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    AccountAttribute accountAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) accountAttribute1, __vmethodptr(accountAttribute1, FieldSelecting));
    fieldSelecting.AddHandler(itemType1, fieldName1, pxFieldSelecting);
    this._curyKeyField = this.SuppressCurrencyValidation ? (Type) null : AccountAttribute.SearchKeyField(sender);
    if (!PXAccess.FeatureInstalled<FeaturesSet.financialModule>())
    {
      PXDimensionSelectorAttribute.SetValidCombo(sender, ((PXEventSubscriberAttribute) this)._FieldName, false);
      if (!sender.Graph.Views.Caches.Contains(typeof (Account)))
        sender.Graph.Views.Caches.Add(typeof (Account));
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType2 = sender.GetItemType();
      string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
      AccountAttribute accountAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) accountAttribute2, __vmethodptr(accountAttribute2, Feature_FieldDefaulting));
      fieldDefaulting.AddHandler(itemType2, fieldName2, pxFieldDefaulting);
    }
    else
    {
      if (this._curyKeyField != (Type) null)
      {
        // ISSUE: method pointer
        sender.Graph.RowUpdated.AddHandler<PX.Objects.CM.CurrencyInfo>(new PXRowUpdated((object) this, __methodptr(CurrencyInfoRowUpdated)));
      }
      if (this._branchID != (Type) null)
      {
        PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
        Type itemType3 = BqlCommand.GetItemType(this._branchID);
        string name = this._branchID.Name;
        AccountAttribute accountAttribute3 = this;
        // ISSUE: virtual method pointer
        PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) accountAttribute3, __vmethodptr(accountAttribute3, BranchFieldUpdated));
        fieldUpdated.AddHandler(itemType3, name, pxFieldUpdated);
      }
      if (((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).CacheGlobal)
        return;
      PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
      Type itemType4 = sender.GetItemType();
      string fieldName3 = ((PXEventSubscriberAttribute) this)._FieldName;
      AccountAttribute accountAttribute4 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) accountAttribute4, __vmethodptr(accountAttribute4, FieldUpdating));
      fieldUpdating1.AddHandler(itemType4, fieldName3, pxFieldUpdating1);
      PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
      Type itemType5 = sender.GetItemType();
      string fieldName4 = ((PXEventSubscriberAttribute) this)._FieldName;
      PXDimensionSelectorAttribute attribute = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
      // ISSUE: virtual method pointer
      PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, FieldUpdating));
      fieldUpdating2.RemoveHandler(itemType5, fieldName4, pxFieldUpdating2);
    }
  }

  public virtual void Feature_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    if (!this.Definitions.DefaultAccountID.HasValue)
    {
      object obj = (object) "0";
      sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      e.NewValue = obj;
    }
    else
      e.NewValue = (object) this.Definitions.DefaultAccountID;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void Verify(PXCache sender, Account item, object row)
  {
    if (item != null && item.CuryID != null && !(this._curyKeyField == (Type) null) && sender.Fields.Contains(typeof (PX.Objects.CM.CurrencyInfo.curyID).Name) && !(item.CuryID == AccountAttribute.GetCurrencyID(sender, row)) && this.GetLedger(sender, row)?.BalanceType != "R")
      throw new AccountAttribute.CuryException(item.AccountCD);
  }

  private static string GetCurrencyID(PXCache sender, object row)
  {
    using (new PXCuryViewStateScope(sender.Graph))
      return PXFieldState.UnwrapValue(sender.GetValueExt(row, typeof (PX.Objects.CM.CurrencyInfo.curyID).Name))?.ToString()?.TrimEnd() ?? (string) sender.GetValue(row, typeof (PX.Objects.CM.CurrencyInfo.curyID).Name);
  }

  private Ledger GetLedger(PXCache sender, object row)
  {
    if (this._ledgerID == (Type) null)
      return (Ledger) null;
    int num = (int) PXView.FieldGetValue(sender, row, BqlCommand.GetItemType(this._ledgerID), this._ledgerID.Name);
    return Ledger.PK.Find(sender.Graph, new int?(num));
  }

  private void VerifyControlAccountRules(PXCache sender, EventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    object obj = verifyingEventArgs?.Row ?? updatedEventArgs?.Row ?? persistingEventArgs?.Row ?? selectedEventArgs?.Row;
    if (this.ControlAccountForModule != null)
    {
      AccountAttribute.VerifyControlAccount(sender, ((PXEventSubscriberAttribute) this)._FieldName, e, this.ControlAccountForModule);
    }
    else
    {
      if (!this.AvoidControlAccounts)
        return;
      Account account = AccountAttribute.GetAccount(sender, ((PXEventSubscriberAttribute) this)._FieldName, e);
      if (account == null)
        return;
      if (this.AllowControlAccountForModuleField != (Type) null)
      {
        object valueExt = sender.GetValueExt(obj, this.AllowControlAccountForModuleField.Name);
        string str = valueExt is PXFieldState ? (string) ((PXFieldState) valueExt).Value : (string) valueExt;
        if (!string.IsNullOrEmpty(str) && (str == "ANY" || account.ControlAccountModule == str))
          return;
      }
      AccountAttribute.VerifyAccountIsNotControl(sender, ((PXEventSubscriberAttribute) this)._FieldName, e, account);
    }
  }

  public static void VerifyControlAccount<T>(
    PXCache cache,
    EventArgs e,
    string expectedModule,
    Account account = null)
    where T : IBqlField
  {
    AccountAttribute.VerifyControlAccount(cache, typeof (T).Name, e, expectedModule, account);
  }

  public static void VerifyControlAccount(
    PXCache cache,
    string fieldName,
    EventArgs e,
    string expectedModule,
    Account account = null)
  {
    if (cache == null || e == null || AccountAttribute.SkipControlAccountValidation(e))
      return;
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    object obj1 = verifyingEventArgs?.Row ?? updatedEventArgs?.Row ?? persistingEventArgs?.Row ?? selectedEventArgs?.Row;
    if (account == null)
    {
      object obj2 = verifyingEventArgs != null ? verifyingEventArgs.NewValue : cache.GetValue(obj1, fieldName);
      if (obj2 == null)
        return;
      account = (Account) PXSelectorAttribute.Select(cache, obj1, fieldName, obj2) ?? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountCD>(cache.Graph, obj2, Array.Empty<object>()));
    }
    if (account == null)
      return;
    try
    {
      AccountAttribute.VerifyControlAccount(account, expectedModule);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling(fieldName, obj1, (object) account.AccountCD, (Exception) ex);
      if (ex.ErrorLevel >= 4 && verifyingEventArgs != null)
        ((CancelEventArgs) verifyingEventArgs).Cancel = true;
      else if (ex.ErrorLevel >= 4 && persistingEventArgs != null)
      {
        ((CancelEventArgs) persistingEventArgs).Cancel = true;
        throw ex;
      }
    }
  }

  public static void VerifyControlAccount(Account account, string expectedModule)
  {
    if (account.IsCashAccount.GetValueOrDefault())
      throw new PXSetPropertyException("Cash accounts cannot be used as control accounts.", (PXErrorLevel) 4, new object[2]
      {
        (object) account.AccountCD,
        (object) account.ControlAccountModule
      });
    if (account.ControlAccountModule == null)
      throw new PXSetPropertyException("Account {0} is not a control account. Please select an account configured as a control account for {1}.", (PXErrorLevel) 2, new object[2]
      {
        (object) account.AccountCD,
        (object) expectedModule
      });
    if (account.ControlAccountModule != expectedModule)
      throw new PXSetPropertyException("Account {0} is a control account for {1}. Please select an account configured as a control account for {2}.", (PXErrorLevel) 4, new object[3]
      {
        (object) account.AccountCD,
        (object) account.ControlAccountModule,
        (object) expectedModule
      });
  }

  public static void VerifyAccountIsNotControl<T>(
    PXCache cache,
    EventArgs e,
    Account account = null,
    bool throwOnVerifying = false)
    where T : IBqlField
  {
    AccountAttribute.VerifyAccountIsNotControl(cache, typeof (T).Name, e, account, throwOnVerifying);
  }

  public static void VerifyAccountIsNotControl(
    PXCache cache,
    string fieldName,
    EventArgs e,
    Account account = null,
    bool throwOnVerifying = false)
  {
    if (cache == null || e == null || AccountAttribute.SkipControlAccountValidation(e))
      return;
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    object obj1 = verifyingEventArgs?.Row ?? updatedEventArgs?.Row ?? persistingEventArgs?.Row ?? selectedEventArgs?.Row;
    if (account == null)
    {
      object obj2 = verifyingEventArgs != null ? verifyingEventArgs.NewValue : cache.GetValue(obj1, fieldName);
      if (obj2 == null)
        return;
      account = (Account) PXSelectorAttribute.Select(cache, obj1, fieldName, obj2) ?? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountCD>(cache.Graph, obj2, Array.Empty<object>()));
    }
    if (account == null)
      return;
    try
    {
      AccountAttribute.VerifyAccountIsNotControl(account);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling(fieldName, obj1, (object) account.AccountCD, (Exception) ex);
      if (ex.ErrorLevel >= 4 && verifyingEventArgs != null)
      {
        ((CancelEventArgs) verifyingEventArgs).Cancel = true;
        if (throwOnVerifying)
        {
          if (cache.GetStateExt(obj1, fieldName) is PXFieldState stateExt)
            verifyingEventArgs.NewValue = stateExt.Value;
          throw ex;
        }
      }
      else if (ex.ErrorLevel >= 4 && persistingEventArgs != null)
      {
        ((CancelEventArgs) persistingEventArgs).Cancel = true;
        throw ex;
      }
    }
  }

  public static void VerifyAccountIsNotControl(Account account)
  {
    if (account == null || account.ControlAccountModule == null)
      return;
    if (account.AllowManualEntry.GetValueOrDefault())
      throw new PXSetPropertyException("Account {0} is a control account for {1}. Although posting to this account is allowed, we recommend that you select a non-control account.", (PXErrorLevel) 2, new object[2]
      {
        (object) account.AccountCD,
        (object) account.ControlAccountModule
      });
    throw new PXSetPropertyException("Account {0} is a control account for {1}, and posting to it is prohibited. Please select a non-control account.", (PXErrorLevel) 4, new object[2]
    {
      (object) account.AccountCD,
      (object) account.ControlAccountModule
    });
  }

  private static bool SkipControlAccountValidation(EventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    if (verifyingEventArgs == null && updatedEventArgs == null && persistingEventArgs == null && selectedEventArgs == null || verifyingEventArgs != null && ((CancelEventArgs) verifyingEventArgs).Cancel || persistingEventArgs != null && ((CancelEventArgs) persistingEventArgs).Cancel)
      return true;
    return persistingEventArgs != null && persistingEventArgs.Operation != 2 && persistingEventArgs.Operation != 1;
  }

  public static Account GetAccount(PXCache cache, string fieldName, EventArgs e)
  {
    if (cache == null || e == null)
      return (Account) null;
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    object obj1 = verifyingEventArgs?.Row ?? updatedEventArgs?.Row ?? persistingEventArgs?.Row ?? selectedEventArgs?.Row;
    object obj2 = verifyingEventArgs != null ? verifyingEventArgs.NewValue : cache.GetValue(obj1, fieldName);
    return obj2 == null ? (Account) null : (Account) PXSelectorAttribute.Select(cache, obj1, fieldName, obj2) ?? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountCD>(cache.Graph, obj2, Array.Empty<object>()));
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXView pxView = new PXView(sender.Graph, false, BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Select<GLSetup>)
    }));
    if (!((Dictionary<string, PXView>) sender.Graph.Views).ContainsKey("_GLSetup"))
      sender.Graph.Views.Add("_GLSetup", pxView);
    if (pxView.Cache.Current != null)
      return;
    pxView.Cache.Current = (object) PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.SelectWindowed(sender.Graph, 0, 1, Array.Empty<object>()));
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel || e.NewValue == null)
      return;
    PXDimensionAttribute attribute1 = ((PXAggregateAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).GetAttribute<PXDimensionAttribute>();
    // ISSUE: virtual method pointer
    new PXFieldUpdating((object) attribute1, __vmethodptr(attribute1, FieldUpdating)).Invoke(sender, e);
    ((CancelEventArgs) e).Cancel = false;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where2<Match<Current<AccessInfo.userName>>, And<Account.accountCD, Equal<Required<Account.accountCD>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      e.NewValue
    }));
    if (account == null)
    {
      PXSelectorAttribute attribute2 = ((PXAggregateAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).GetAttribute<PXSelectorAttribute>();
      // ISSUE: virtual method pointer
      new PXFieldUpdating((object) attribute2, __vmethodptr(attribute2, SubstituteKeyFieldUpdating)).Invoke(sender, e);
    }
    else
      e.NewValue = (object) account.AccountID;
  }

  private void CheckData(PXCache cache, object data)
  {
    if (cache.GetValue(data, ((PXEventSubscriberAttribute) this)._FieldName) == null)
      return;
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, data, (object) null, (Exception) null);
    try
    {
      Account account = (Account) PXSelectorAttribute.Select(cache, data, ((PXEventSubscriberAttribute) this)._FieldName);
      if (account == null)
        return;
      this.Verify(cache, account, data);
    }
    catch (AccountAttribute.CuryException ex)
    {
      object valueExt = cache.GetValueExt(data, this.FieldName);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, data, valueExt, (Exception) ex);
    }
    catch (PXSetPropertyException ex)
    {
    }
  }

  private void CurrencyInfoRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PX.Objects.CM.CurrencyInfo.curyID>(e.Row, e.OldRow))
      return;
    PXView view = CurrencyInfoAttribute.GetView(sender.Graph, BqlCommand.GetItemType(this._curyKeyField), this._curyKeyField);
    if (view == null)
      return;
    PXCache cache = view.Cache;
    foreach (object data in view.SelectMultiBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>()))
      this.CheckData(cache, data);
  }

  protected virtual void BranchFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PXFieldState valueExt = (PXFieldState) sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (valueExt == null || valueExt.Value == null)
      return;
    Account account = (Account) PXSelectorAttribute.Select(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (account != null)
    {
      bool? isCashAccount = account.IsCashAccount;
      bool flag = false;
      if (!(isCashAccount.GetValueOrDefault() == flag & isCashAccount.HasValue))
        return;
    }
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, valueExt.Value);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    this.VerifyControlAccountRules(sender, (EventArgs) e);
    if (!((CancelEventArgs) e).Cancel && e.NewValue != null)
    {
      Account account = (Account) PXSelectorAttribute.Select(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, e.NewValue);
      if (account != null)
      {
        if (!sender.Graph.UnattendedMode)
        {
          try
          {
            this.Verify(sender, account, e.Row);
          }
          catch (PXSetPropertyException ex)
          {
            e.NewValue = (object) account.AccountCD;
            ((CancelEventArgs) e).Cancel = true;
            throw;
          }
        }
      }
    }
    if (this._UIAttrIndex == -1)
      return;
    ((IPXFieldVerifyingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).FieldVerifying(sender, e);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    this.VerifyControlAccountRules(sender, (EventArgs) e);
    if (((CancelEventArgs) e).Cancel || !(this._curyKeyField != (Type) null) || (e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    this.CheckData(sender, e.Row);
  }

  protected virtual AccountAttribute.Definition Definitions
  {
    get
    {
      AccountAttribute.Definition definitions = PXContext.GetSlot<AccountAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<AccountAttribute.Definition>(PXDatabase.GetSlot<AccountAttribute.Definition>("Account.Definition", new Type[1]
        {
          typeof (Account)
        }));
      return definitions;
    }
  }

  public static Account GetAccount(PXGraph graph, int? accountID)
  {
    return (accountID.HasValue ? Account.PK.Find(graph, accountID) : throw new ArgumentNullException(nameof (accountID))) ?? throw new PXException("{0} with ID '{1}' does not exists", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName(typeof (Account)),
      (object) accountID
    });
  }

  public class CuryException : PXSetPropertyException
  {
    public CuryException(string accountCD)
      : base("The currency assigned to the denominated GL account ({0}) is different from the transaction currency.", new object[1]
      {
        (object) accountCD.Trim()
      })
    {
    }

    public CuryException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      ReflectionSerializer.RestoreObjectProps<AccountAttribute.CuryException>(this, info);
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ReflectionSerializer.GetObjectData<AccountAttribute.CuryException>(this, info);
      base.GetObjectData(info, context);
    }
  }

  public class dimensionName : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountAttribute.dimensionName>
  {
    public dimensionName()
      : base("ACCOUNT")
    {
    }
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    private int? _DefaultAccountID;

    public int? DefaultAccountID => this._DefaultAccountID;

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Account>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<Account.accountID>(),
        (PXDataField) new PXDataFieldOrder<Account.accountID>()
      }))
      {
        this._DefaultAccountID = new int?();
        if (pxDataRecord == null)
          return;
        this._DefaultAccountID = pxDataRecord.GetInt32(0);
      }
    }
  }
}
