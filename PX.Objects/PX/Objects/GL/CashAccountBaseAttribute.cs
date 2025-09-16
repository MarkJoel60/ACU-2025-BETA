// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.CashAccountBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Represents CashAccount Field with Selector that shows all Cash Accounts.
/// </summary>
[PXUIField]
public abstract class CashAccountBaseAttribute : PXEntityAttribute, IPXFieldUpdatingSubscriber
{
  private Type[] _selectorCols = new Type[6]
  {
    typeof (CashAccount.cashAccountCD),
    typeof (CashAccount.accountID),
    typeof (CashAccount.descr),
    typeof (CashAccount.curyID),
    typeof (CashAccount.subID),
    typeof (CashAccount.branchID)
  };
  private Type _branchID;
  public const string DimensionName = "CASHACCOUNT";

  public bool SuppressCurrencyValidation { get; set; }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object with all default parameters.
  /// Doesn't filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification
  /// </summary>
  public CashAccountBaseAttribute()
    : this(false)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object with all default parameters except the <paramref name="search" />.
  /// Doesn't filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification
  /// </summary>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountBaseAttribute(Type search)
    : this(false, search: search)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object. Doesn't filter by branch.
  /// </summary>
  /// <param name="suppressActiveVerification">True to suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> verification.</param>
  /// <param name="branchID">(Optional) Identifier for the branch.</param>
  /// <param name="search">(Optional) The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountBaseAttribute(bool suppressActiveVerification, Type branchID = null, Type search = null)
  {
    this.InitAttribute(search, branchID, false, suppressActiveVerification);
  }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object.
  /// </summary>
  /// <param name="suppressActiveVerification">True to suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> verification.</param>
  /// <param name="branchID">(Optional) Identifier for the branch.</param>
  /// <param name="search">(Optional) The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountBaseAttribute(
    bool suppressActiveVerification,
    bool filterBranch,
    Type branchID = null,
    Type search = null)
  {
    this.InitAttribute(search, branchID, filterBranch, suppressActiveVerification);
  }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object. Filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification.
  /// </summary>
  /// <param name="branchID">Identifier for the branch.</param>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountBaseAttribute(Type branchID, Type search)
  {
    this.InitAttribute(search, branchID, true, false);
  }

  /// <summary>
  /// Constructor of the new CashAccountBaseAttribute object. Filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification.
  /// </summary>
  /// <param name="branchID">Identifier for the branch.</param>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  /// <param name="showClearingColumn"> If True, shows the Clearing Account column in a cash account selector</param>
  public CashAccountBaseAttribute(Type branchID, Type search, bool showClearingColumn)
  {
    this.InitAttribute(search, branchID, true, false, showClearingColumn);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!(this._branchID != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = BqlCommand.GetItemType(this._branchID);
    string name = this._branchID.Name;
    CashAccountBaseAttribute accountBaseAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) accountBaseAttribute, __vmethodptr(accountBaseAttribute, BranchFieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  private void InitAttribute(
    Type search,
    Type branchID,
    bool filterBranch,
    bool suppressActiveVerify,
    bool showClearingColumn = false)
  {
    this._branchID = branchID;
    Type type = search;
    if ((object) type == null)
      type = typeof (Search<CashAccount.cashAccountID>);
    search = type;
    Type newSearch = this.generateNewSearch(search.GetGenericArguments(), this._branchID, new bool?(filterBranch));
    if (newSearch == (Type) null)
      throw new PXArgumentException(nameof (search), "An invalid argument has been specified.");
    if (showClearingColumn)
      this._selectorCols = new Type[7]
      {
        typeof (CashAccount.cashAccountCD),
        typeof (CashAccount.accountID),
        typeof (CashAccount.descr),
        typeof (CashAccount.curyID),
        typeof (CashAccount.subID),
        typeof (CashAccount.branchID),
        typeof (CashAccount.clearingAccount)
      };
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CASHACCOUNT", newSearch, typeof (CashAccount.cashAccountCD), this._selectorCols)
    {
      CacheGlobal = true,
      DescriptionField = typeof (CashAccount.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
    if (!suppressActiveVerify)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<CashAccount.active, Equal<True>>), "The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", new Type[1]
      {
        typeof (CashAccount.cashAccountCD)
      }));
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() || !(this._branchID != (Type) null))
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[5]
    {
      typeof (Where<,>),
      typeof (CashAccount.baseCuryID),
      typeof (EqualBaseCuryID<>),
      typeof (Current<>),
      this._branchID
    }), "The {0} cash account is associated with the {1} branch whose base currency differs from the base currency of the originating branch.", new Type[2]
    {
      typeof (CashAccount.cashAccountCD),
      typeof (CashAccount.branchID)
    }));
  }

  private Type generateNewSearch(Type[] searchArgs, Type branchID, bool? filterBranch)
  {
    Type type1 = branchID;
    if ((object) type1 == null)
      type1 = typeof (AccessInfo.branchID);
    branchID = type1;
    CashAccountBaseAttribute.SearchParamTypes searchParamTypes = (CashAccountBaseAttribute.SearchParamTypes) 0;
    CashAccountBaseAttribute.SearchTypes searchTypes = CashAccountBaseAttribute.SearchTypes.Search23;
    Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>();
    foreach (Type searchArg in searchArgs)
    {
      if (typeof (IBqlJoin).IsAssignableFrom(searchArg))
      {
        dictionary[typeof (IBqlJoin)] = searchArg;
        searchParamTypes |= CashAccountBaseAttribute.SearchParamTypes.Join;
      }
      else if (typeof (IBqlWhere).IsAssignableFrom(searchArg))
      {
        dictionary[typeof (IBqlWhere)] = searchArg;
        searchParamTypes |= CashAccountBaseAttribute.SearchParamTypes.Where;
      }
      else if (typeof (IBqlAggregate).IsAssignableFrom(searchArg))
      {
        dictionary[typeof (IBqlAggregate)] = searchArg;
        searchParamTypes |= CashAccountBaseAttribute.SearchParamTypes.Aggregate;
      }
      else if (typeof (IBqlOrderBy).IsAssignableFrom(searchArg))
      {
        dictionary[typeof (IBqlOrderBy)] = searchArg;
        searchParamTypes |= CashAccountBaseAttribute.SearchParamTypes.OrderBy;
      }
    }
    Type newSearch = (Type) null;
    Type type2 = (Type) null;
    switch (searchParamTypes & (CashAccountBaseAttribute.SearchParamTypes.Aggregate | CashAccountBaseAttribute.SearchParamTypes.OrderBy))
    {
      case (CashAccountBaseAttribute.SearchParamTypes) 0:
        type2 = typeof (Search2<,,>);
        searchTypes = CashAccountBaseAttribute.SearchTypes.Search23;
        break;
      case CashAccountBaseAttribute.SearchParamTypes.Aggregate:
        type2 = typeof (Search5<,,,>);
        searchTypes = CashAccountBaseAttribute.SearchTypes.Search54;
        break;
      case CashAccountBaseAttribute.SearchParamTypes.OrderBy:
        type2 = typeof (Search2<,,,>);
        searchTypes = CashAccountBaseAttribute.SearchTypes.Search24;
        break;
      case CashAccountBaseAttribute.SearchParamTypes.Aggregate | CashAccountBaseAttribute.SearchParamTypes.OrderBy:
        type2 = typeof (Search5<,,,,>);
        searchTypes = CashAccountBaseAttribute.SearchTypes.Search55;
        break;
    }
    if (type2 != (Type) null)
    {
      if (dictionary.ContainsKey(typeof (IBqlJoin)))
        dictionary[typeof (IBqlJoin)] = BqlCommand.Compose(new Type[16 /*0x10*/]
        {
          typeof (InnerJoin<,,>),
          typeof (Account),
          typeof (On<,,>),
          typeof (Account.accountID),
          typeof (Equal<CashAccount.accountID>),
          typeof (And2<,>),
          typeof (Match<Account, Current<AccessInfo.userName>>),
          typeof (And<>),
          typeof (Match<,>),
          typeof (Account),
          typeof (Optional<>),
          branchID,
          typeof (InnerJoin<,,>),
          typeof (Sub),
          typeof (On<Sub.subID, Equal<CashAccount.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>),
          dictionary[typeof (IBqlJoin)]
        });
      else
        dictionary[typeof (IBqlJoin)] = typeof (InnerJoin<Account, On<Account.accountID, Equal<CashAccount.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<Sub.subID, Equal<CashAccount.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>>>);
      List<Type> filterConditionsList = this.GetFilterConditionsList(branchID, filterBranch);
      if (dictionary.ContainsKey(typeof (IBqlWhere)))
      {
        filterConditionsList.Insert(0, typeof (Where2<,>));
        filterConditionsList.AddRange((IEnumerable<Type>) new Type[2]
        {
          typeof (And<>),
          dictionary[typeof (IBqlWhere)]
        });
      }
      dictionary[typeof (IBqlWhere)] = BqlCommand.Compose(filterConditionsList.ToArray());
      List<Type> typeList = new List<Type>()
      {
        type2,
        typeof (CashAccount.cashAccountID),
        dictionary[typeof (IBqlJoin)],
        dictionary[typeof (IBqlWhere)]
      };
      if (searchTypes == CashAccountBaseAttribute.SearchTypes.Search54 || searchTypes == CashAccountBaseAttribute.SearchTypes.Search55)
        typeList.Add(dictionary[typeof (IBqlAggregate)]);
      if (searchTypes == CashAccountBaseAttribute.SearchTypes.Search24 || searchTypes == CashAccountBaseAttribute.SearchTypes.Search55)
        typeList.Add(dictionary[typeof (IBqlOrderBy)]);
      newSearch = BqlCommand.Compose(typeList.ToArray());
    }
    return newSearch;
  }

  protected virtual List<Type> GetFilterConditionsList(Type branchID, bool? filterBranch)
  {
    List<Type> filterConditionsList = new List<Type>()
    {
      typeof (Where<,,>),
      typeof (CashAccount.restrictVisibilityWithBranch),
      typeof (Equal<>),
      typeof (False),
      filterBranch.GetValueOrDefault() ? typeof (Or<,,>) : typeof (Or<,>),
      typeof (CashAccount.branchID)
    };
    Type[] typeArray;
    if (!filterBranch.GetValueOrDefault())
      typeArray = new Type[1]{ typeof (IsNotNull) };
    else
      typeArray = new Type[7]
      {
        typeof (Equal<>),
        typeof (Current<>),
        branchID,
        typeof (Or<,>),
        typeof (Current<>),
        branchID,
        typeof (IsNull)
      };
    Type[] collection = typeArray;
    filterConditionsList.AddRange((IEnumerable<Type>) collection);
    return filterConditionsList;
  }

  protected virtual void BranchFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PXFieldState valueExt = (PXFieldState) sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (valueExt == null || valueExt.Value == null)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, valueExt.Value);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    this.VerifyAccount(sender, e);
    this.VerifySubaccount(sender, e);
  }

  private void VerifyAccount(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXViewOf<CashAccount>.BasedOn<SelectFromBase<CashAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CashAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(sender.Graph, new object[1]
    {
      e.NewValue
    }));
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXViewOf<Account>.BasedOn<SelectFromBase<Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) (int?) cashAccount?.AccountID
    }));
    if (account == null)
      return;
    bool? active = account.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The {0} GL account used with this cash account is inactive. To use this GL account, activate the account on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 4, new object[2]
    {
      (object) cashAccount.CashAccountCD,
      (object) account.AccountCD
    });
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) cashAccount.CashAccountCD, (Exception) propertyException);
  }

  private void VerifySubaccount(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXViewOf<CashAccount>.BasedOn<SelectFromBase<CashAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CashAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(sender.Graph, new object[1]
    {
      e.NewValue
    }));
    Sub dac = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXViewOf<Sub>.BasedOn<SelectFromBase<Sub, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Sub.subID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) (int?) cashAccount?.SubID
    }));
    if (dac == null)
      return;
    bool? active = dac.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The {0} subaccount used with this cash account is inactive. To use this cash account, activate the subaccount on the Subaccounts (GL203000) form.", (PXErrorLevel) 4, new object[1]
    {
      (object) sender.Graph.Caches[typeof (Sub)].GetFormatedMaskField<Sub.subCD>((IBqlTable) dac)
    });
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) cashAccount.CashAccountCD, (Exception) propertyException);
  }

  [Flags]
  private enum SearchParamTypes
  {
    Join = 1,
    Where = 2,
    Aggregate = 4,
    OrderBy = 8,
  }

  private enum SearchTypes
  {
    Search23,
    Search24,
    Search54,
    Search55,
  }
}
