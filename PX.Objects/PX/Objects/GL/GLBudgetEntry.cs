// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.Standalone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class GLBudgetEntry : 
  PXGraph<GLBudgetEntry>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXFilter<BudgetFilter> Filter;
  public PXFilter<BudgetDistributeFilter> DistrFilter;
  public PXFilter<BudgetPreloadFilter> PreloadFilter;
  public PXFilter<ManageBudgetDialog> ManageDialog;
  public PXSelect<GLBudget, Where<GLBudget.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudget.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudget.finYear, Equal<Current<BudgetFilter.finYear>>>>>> Budget;
  [PXImport(typeof (BudgetFilter))]
  public PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.subID, Like<Current<BudgetFilter.subCDWildcard>>>>>>, OrderBy<Asc<GLBudgetLine.sortOrder>>> BudgetArticles;
  public PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLineDetail.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLineDetail.finYear, Equal<Optional<BudgetFilter.finYear>>, And<GLBudgetLineDetail.groupID, Equal<Required<GLBudgetLineDetail.groupID>>>>>>> Allocations;
  public PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Argument<Guid?>>>, OrderBy<Asc<GLBudgetLine.treeSortOrder>>> Tree;
  public PXSelect<PX.SM.Neighbour> Neighbour;
  private bool SubEnabled = PXAccess.FeatureInstalled<FeaturesSet.subAccount>();
  protected GLBudgetEntry.GLBudgetEntryActionType _CurrentAction;
  private bool IndexesIsPrepared;
  private GLBudgetEntry.GLBudgetLineIdx _ArticleIndex;
  private GLBudgetEntry.GLBudgetLineDetailIdx _AllocationIndex;
  private PXResultset<GLBudgetLine> _ArticleGroups;
  private GLBudgetEntry.AccountIndex _accountIndex;
  private GLBudgetEntry.SubIndex _subIndex;
  private GLBudgetEntry.LedgerIndex _ledgerIndex;
  private bool? _needSubDimensionValidate;
  private readonly int _Periods;
  private readonly string _PrefixPeriodField;
  private int _PeriodsInCurrentYear;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  private GLBudgetEntry.GLBudgetLineIdx _BudgetArticlesIndex;
  protected bool suppressIDs;
  private bool isImportStarted;
  public PXSave<BudgetFilter> Save;
  public PXCancel<BudgetFilter> Cancel;
  public PXDelete<BudgetFilter> Delete;
  public PXAction<BudgetFilter> First;
  public PXAction<BudgetFilter> Prev;
  public PXAction<BudgetFilter> Next;
  public PXAction<BudgetFilter> WNext;
  public PXAction<BudgetFilter> Last;
  public PXAction<BudgetFilter> ShowPreload;
  public PXAction<BudgetFilter> Preload;
  public PXAction<BudgetFilter> Distribute;
  public PXAction<BudgetFilter> DistributeOK;
  public PXAction<BudgetFilter> ShowManage;
  public PXAction<BudgetFilter> ManageOK;

  private bool IsNullOrEmpty(Guid? _guid) => !_guid.HasValue || Guid.Empty.Equals((object) _guid);

  private bool IsEmpty(Guid? _guid) => Guid.Empty.Equals((object) _guid);

  protected void HoldNotchanged(PXCache cache)
  {
    foreach (object obj in cache.Cached)
    {
      if (cache.GetStatus(obj) == null)
        cache.SetStatus(obj, (PXEntryStatus) 5);
    }
  }

  protected void HoldNotchanged(PXCache cache, object o)
  {
    if (cache.GetStatus(o) != null)
      return;
    cache.SetStatus(o, (PXEntryStatus) 5);
  }

  private GLBudgetEntry.GLBudgetLineIdx ArticleIndex
  {
    get
    {
      if (!this.IndexesIsPrepared && ((PXGraph) this).IsImport)
        this.PrepareIndexes();
      return this._ArticleIndex;
    }
  }

  private GLBudgetEntry.GLBudgetLineDetailIdx AllocationIndex
  {
    get
    {
      if (!this.IndexesIsPrepared && ((PXGraph) this).IsImport)
        this.PrepareIndexes();
      return this._AllocationIndex;
    }
  }

  private PXResultset<GLBudgetLine> ArticleGroups
  {
    get
    {
      if (!this.IndexesIsPrepared && ((PXGraph) this).IsImport)
        this.PrepareIndexes();
      return this._ArticleGroups;
    }
  }

  private GLBudgetEntry.AccountIndex accountIndex
  {
    get
    {
      return this._accountIndex ?? (this._accountIndex = new GLBudgetEntry.AccountIndex((PXGraph) this));
    }
  }

  private GLBudgetEntry.SubIndex subIndex
  {
    get => this._subIndex ?? (this._subIndex = new GLBudgetEntry.SubIndex((PXGraph) this));
  }

  private GLBudgetEntry.LedgerIndex ledgerIndex
  {
    get => this._ledgerIndex ?? (this._ledgerIndex = new GLBudgetEntry.LedgerIndex((PXGraph) this));
  }

  private bool needSubDimensionValidate
  {
    get
    {
      if (!this._needSubDimensionValidate.HasValue)
      {
        PXView pxView = new PXView((PXGraph) this, true, PXSelectBase<Dimension, PXViewOf<Dimension>.BasedOn<SelectFromBase<Dimension, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Dimension.dimensionID, IBqlString>.IsEqual<SubAccountAttribute.dimensionName>>>.ReadOnly.Config>.GetCommand());
        using (new PXFieldScope(pxView, new Type[1]
        {
          typeof (Dimension.validate)
        }))
          this._needSubDimensionValidate = ((Dimension) pxView.SelectSingle(Array.Empty<object>())).Validate;
      }
      return this._needSubDimensionValidate.Value;
    }
  }

  public GLBudgetEntry()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    this._PrefixPeriodField = "Period";
    this._Periods = (int) ((short?) PXResultset<MasterFinYear>.op_Implicit(PXSelectBase<MasterFinYear, PXSelectOrderBy<MasterFinYear, OrderBy<Desc<MasterFinYear.finPeriods>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()))?.FinPeriods).GetValueOrDefault();
    OrganizationFinYear organizationFinYear = PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelect<OrganizationFinYear, Where<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear,
      (object) PXAccess.GetParentOrganizationID(((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID)
    }));
    this._PeriodsInCurrentYear = organizationFinYear == null ? this._Periods : (int) organizationFinYear.FinPeriods.Value;
    for (int index = 1; index <= this._Periods; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GLBudgetEntry.\u003C\u003Ec__DisplayClass41_0 cDisplayClass410 = new GLBudgetEntry.\u003C\u003Ec__DisplayClass41_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass410.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass410.j = index;
      string str = this._PrefixPeriodField + index.ToString();
      ((PXSelectBase) this.BudgetArticles).Cache.Fields.Add(str);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (GLBudgetLine), str, new PXFieldSelecting((object) cDisplayClass410, __methodptr(\u003C\u002Ector\u003Eb__0)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdating.AddHandler(typeof (GLBudgetLine), str, new PXFieldUpdating((object) cDisplayClass410, __methodptr(\u003C\u002Ector\u003Eb__1)));
    }
    if (!((PXGraph) this).IsImport)
      return;
    ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree = new bool?(false);
    this.PrepareIndexes();
    this._BudgetArticlesIndex = (GLBudgetEntry.GLBudgetLineIdx) null;
  }

  private SelectedGroup CurrentSelected
  {
    get
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (SelectedGroup)];
      if (cach.Current == null)
      {
        cach.Insert();
        cach.IsDirty = false;
      }
      return (SelectedGroup) cach.Current;
    }
  }

  protected virtual IEnumerable tree([PXGuid] Guid? GroupID)
  {
    GLBudgetEntry glBudgetEntry1 = this;
    if (!GroupID.HasValue)
      yield return (object) new GLBudgetLine()
      {
        GroupID = new Guid?(Guid.Empty),
        Description = PXSiteMap.RootNode.Title
      };
    GLBudgetEntry glBudgetEntry2 = glBudgetEntry1;
    object[] objArray = new object[1]{ (object) GroupID };
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.isGroup, Equal<True>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>>>.Config>.Select((PXGraph) glBudgetEntry2, objArray))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      if (glBudgetEntry1.SearchForMatchingGroupMask(glBudgetLine.GroupID))
      {
        if (((PXSelectBase<BudgetFilter>) glBudgetEntry1.Filter).Current.TreeNodeFilter != null)
        {
          if (glBudgetLine.Description.Contains(((PXSelectBase<BudgetFilter>) glBudgetEntry1.Filter).Current.TreeNodeFilter))
            yield return (object) glBudgetLine;
          else if (glBudgetEntry1.SearchForMatchingChild(glBudgetLine.GroupID))
            yield return (object) glBudgetLine;
          else if (glBudgetEntry1.SearchForMatchingParent(glBudgetLine.ParentGroupID))
            yield return (object) glBudgetLine;
        }
        else
          yield return (object) glBudgetLine;
      }
    }
  }

  protected virtual IEnumerable budgetarticles([PXGuid] Guid? groupID)
  {
    bool? showTree;
    Guid? nullable1;
    int num;
    if (((PXGraph) this).IsImport)
    {
      showTree = ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree;
      bool flag = false;
      if (showTree.GetValueOrDefault() == flag & showTree.HasValue)
      {
        if (groupID.HasValue)
        {
          nullable1 = groupID;
          Guid empty = Guid.Empty;
          num = nullable1.HasValue ? (nullable1.GetValueOrDefault() == empty ? 1 : 0) : 0;
          goto label_6;
        }
        num = 1;
        goto label_6;
      }
    }
    num = 0;
label_6:
    bool flag1 = num != 0;
    if (flag1 && this._BudgetArticlesIndex != null)
      return (IEnumerable) this._BudgetArticlesIndex.GetAll();
    if (!groupID.HasValue)
    {
      nullable1 = this.CurrentSelected.Group;
      if (!nullable1.HasValue)
      {
        ref Guid? local = ref groupID;
        nullable1 = (Guid?) ((PXSelectBase<GLBudgetLine>) this.Tree).Current?.GroupID;
        Guid guid = nullable1 ?? Guid.Empty;
        local = new Guid?(guid);
        SelectedGroup currentSelected = this.CurrentSelected;
        showTree = ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree;
        Guid? nullable2 = showTree.GetValueOrDefault() ? groupID : new Guid?(Guid.Empty);
        currentSelected.Group = nullable2;
      }
      else
        groupID = this.CurrentSelected.Group;
    }
    else
    {
      SelectedGroup currentSelected = this.CurrentSelected;
      showTree = ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree;
      Guid? nullable3 = showTree.GetValueOrDefault() ? groupID : new Guid?(Guid.Empty);
      currentSelected.Group = nullable3;
    }
    SelectedGroup currentSelected1 = this.CurrentSelected;
    showTree = ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree;
    Guid? nullable4 = showTree ?? true ? groupID : new Guid?(Guid.Empty);
    currentSelected1.Group = nullable4;
    GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetLine != null)
    {
      this.CurrentSelected.AccountID = glBudgetLine.AccountID.HasValue ? glBudgetLine.AccountID : new int?(int.MinValue);
      this.CurrentSelected.SubID = glBudgetLine.SubID.HasValue ? glBudgetLine.SubID : new int?(int.MinValue);
    }
    else
    {
      this.CurrentSelected.AccountID = new int?(int.MinValue);
      this.CurrentSelected.SubID = new int?(int.MinValue);
    }
    this.CurrentSelected.AccountMaskWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetLine != null ? glBudgetLine.AccountMask : string.Empty, "ACCOUNT");
    this.CurrentSelected.AccountMask = glBudgetLine != null ? glBudgetLine.AccountMask : string.Empty;
    this.CurrentSelected.SubMaskWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetLine != null ? glBudgetLine.SubMask : string.Empty, "SUBACCOUNT");
    this.CurrentSelected.SubMask = glBudgetLine != null ? glBudgetLine.SubMask : string.Empty;
    ((PXSelectBase) this.BudgetArticles).Cache.AllowInsert = this.IsFilterSet() && !this.IsCompareToSetOnFilter();
    ((PXSelectBase) this.BudgetArticles).Cache.AllowDelete = (this.IsFilterSet() || ((PXGraph) this).IsImport) && !this.IsCompareToSetOnFilter();
    showTree = ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree;
    IEnumerable<GLBudgetLine> source = showTree.GetValueOrDefault() ? this.GetTreeArticle(groupID) : this.GetListArticle();
    if (!flag1)
      return (IEnumerable) source;
    List<GLBudgetLine> list = source.ToList<GLBudgetLine>();
    this._BudgetArticlesIndex = new GLBudgetEntry.GLBudgetLineIdx((IEnumerable<GLBudgetLine>) list);
    return (IEnumerable) list;
  }

  protected IEnumerable<GLBudgetLine> GetTreeArticle(Guid? groupID)
  {
    GLBudgetEntry glBudgetEntry1 = this;
    int SortOrder = 0;
    GLBudgetEntry glBudgetEntry2 = glBudgetEntry1;
    object[] objArray = new object[1]{ (object) groupID };
    foreach (PXResult<GLBudgetLine, Account, Sub> pxResult in PXSelectBase<GLBudgetLine, PXSelectJoin<GLBudgetLine, LeftJoin<Account, On<Account.accountID, Equal<GLBudgetLine.accountID>>, LeftJoin<Sub, On<Sub.subID, Equal<GLBudgetLine.subID>, And<Sub.subCD, Like<Current<BudgetFilter.subCDWildcard>>>>>>, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And2<Where<GLBudgetLine.accountID, IsNull, Or<Where<Account.accountID, IsNotNull, And<Sub.subID, IsNotNull>>>>, And<Match<Current<AccessInfo.userName>>>>>>>>, OrderBy<Asc<Account.accountCD, Asc<GLBudgetLine.subID, Asc<GLBudgetLine.treeSortOrder>>>>>.Config>.Select((PXGraph) glBudgetEntry2, objArray))
    {
      GLBudgetLine article = PXResult<GLBudgetLine, Account, Sub>.op_Implicit(pxResult);
      bool? comparison = article.Comparison;
      if (comparison.HasValue)
      {
        comparison = article.Comparison;
        if (comparison.GetValueOrDefault())
          break;
      }
      article.SortOrder = new int?(SortOrder++);
      yield return article;
      if (((PXSelectBase<BudgetFilter>) glBudgetEntry1.Filter).Current.CompareToBranchID.HasValue && ((PXSelectBase<BudgetFilter>) glBudgetEntry1.Filter).Current.CompareToLedgerId.HasValue && !string.IsNullOrEmpty(((PXSelectBase<BudgetFilter>) glBudgetEntry1.Filter).Current.CompareToFinYear))
        yield return glBudgetEntry1.ComparisonRow(article, SortOrder++);
      article = (GLBudgetLine) null;
    }
  }

  protected IEnumerable<GLBudgetLine> GetListArticle()
  {
    GLBudgetEntry glBudgetEntry = this;
    int SortOrder = 0;
    foreach (PXResult<GLBudgetLine, Account, Sub> pxResult in PXSelectBase<GLBudgetLine, PXSelectJoin<GLBudgetLine, InnerJoin<Account, On<Account.accountID, Equal<GLBudgetLine.accountID>>, LeftJoin<Sub, On<Sub.subID, Equal<GLBudgetLine.subID>>>>, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And2<Where<Sub.subCD, Like<Current<BudgetFilter.subCDWildcard>>, Or<Sub.subID, IsNull>>, And<Match<Current<AccessInfo.userName>>>>>>>, OrderBy<Asc<Account.accountCD, Asc<GLBudgetLine.subID, Asc<GLBudgetLine.treeSortOrder>>>>>.Config>.Select((PXGraph) glBudgetEntry, Array.Empty<object>()))
    {
      GLBudgetLine article = PXResult<GLBudgetLine, Account, Sub>.op_Implicit(pxResult);
      bool? nullable = article.Comparison;
      if (nullable.HasValue)
      {
        nullable = article.Comparison;
        if (nullable.GetValueOrDefault())
          break;
      }
      nullable = article.Rollup;
      if (nullable.HasValue)
      {
        nullable = article.Rollup;
        if (nullable.Value)
          goto label_9;
      }
      article.SortOrder = new int?(SortOrder++);
      yield return article;
      if (((PXSelectBase<BudgetFilter>) glBudgetEntry.Filter).Current.CompareToBranchID.HasValue && ((PXSelectBase<BudgetFilter>) glBudgetEntry.Filter).Current.CompareToLedgerId.HasValue && !string.IsNullOrEmpty(((PXSelectBase<BudgetFilter>) glBudgetEntry.Filter).Current.CompareToFinYear))
        yield return glBudgetEntry.ComparisonRow(article, SortOrder++);
label_9:
      article = (GLBudgetLine) null;
    }
  }

  private bool IsFilterSet()
  {
    if (((PXSelectBase<BudgetFilter>) this.Filter).Current != null)
    {
      int? nullable = ((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID;
      if (nullable.HasValue)
      {
        nullable = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
        if (nullable.HasValue)
          return ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear != null;
      }
    }
    return false;
  }

  private bool IsCompareToSetOnFilter()
  {
    return ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToBranchID.HasValue && ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear != null && ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToLedgerId.HasValue;
  }

  protected virtual Decimal ParseAmountValue(object obj)
  {
    if (string.IsNullOrWhiteSpace(obj?.ToString()))
      return 0M;
    int digits = (int) (((PXSelectBase<BudgetFilter>) this.Filter).Current.Precision ?? (short) 2);
    return (Decimal) Math.Round(Convert.ToDouble(obj), digits, MidpointRounding.AwayFromZero);
  }

  private bool SearchForMatchingGroupMask(Guid? GroupID)
  {
    PXResultset<GLBudgetLine> pxResultset = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.isGroup, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    });
    if (PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) GroupID
    }).Count != 0)
      return true;
    if (PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) GroupID
    }).Count != 0)
      return true;
    foreach (PXResult<GLBudgetLine> pxResult in pxResultset)
    {
      if (this.SearchForMatchingGroupMask(PXResult<GLBudgetLine>.op_Implicit(pxResult).GroupID))
        return true;
    }
    return false;
  }

  private bool SearchForMatchingChild(Guid? GroupID)
  {
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.isGroup, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      if (glBudgetLine.Description.Contains(((PXSelectBase<BudgetFilter>) this.Filter).Current.TreeNodeFilter) || this.SearchForMatchingChild(glBudgetLine.GroupID))
        return true;
    }
    return false;
  }

  private bool SearchForMatchingParent(Guid? ParentGroupID)
  {
    GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ParentGroupID
    }));
    return glBudgetLine != null && (glBudgetLine.Description.Contains(((PXSelectBase<BudgetFilter>) this.Filter).Current.TreeNodeFilter) || this.SearchForMatchingParent(glBudgetLine.ParentGroupID));
  }

  protected virtual bool MatchMask(string accountCD, string mask)
  {
    if (mask.Length == 0 && accountCD.Length > 0)
    {
      for (int index = 0; index == accountCD.Length; ++index)
        mask += "?";
    }
    if (mask.Length > 0 && accountCD.Length > 0 && mask.Length > accountCD.Length)
      mask = mask.Substring(0, accountCD.Length);
    for (int index = 0; index < mask.Length; ++index)
    {
      if (index >= accountCD.Length || mask[index] != '?' && (int) mask[index] != (int) accountCD[index])
        return false;
    }
    return true;
  }

  private GLBudgetLine ComparisonRow(GLBudgetLine article, int sortOrder)
  {
    GLBudgetLine o = new GLBudgetLine()
    {
      IsGroup = article.IsGroup,
      Released = new bool?(false),
      WasReleased = new bool?(false),
      BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToBranchID,
      LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToLedgerId,
      FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear,
      AccountID = article.AccountID,
      SubID = article.SubID,
      GroupID = article.GroupID,
      ParentGroupID = article.ParentGroupID,
      Description = PXLocalizer.Localize("Compared"),
      Rollup = article.Rollup,
      Comparison = new bool?(true),
      Compared = article.Compared,
      Amount = new Decimal?(0M),
      SortOrder = new int?(sortOrder)
    };
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BudgetArticles).Cache, (object) o, false);
    if (article.Compared != null)
    {
      foreach (Decimal num1 in article.Compared)
      {
        GLBudgetLine glBudgetLine = o;
        Decimal? amount = glBudgetLine.Amount;
        Decimal num2 = num1;
        glBudgetLine.Amount = amount.HasValue ? new Decimal?(amount.GetValueOrDefault() + num2) : new Decimal?();
      }
    }
    o.AllocatedAmount = o.Amount;
    o.ReleasedAmount = o.Amount;
    this.HoldNotchanged(((PXSelectBase) this.BudgetArticles).Cache, (object) o);
    return o;
  }

  protected virtual void UpdateAlloc(Decimal value, GLBudgetLine article, int fieldNbr)
  {
    GLBudgetLineDetail alloc = this.GetAlloc(article, fieldNbr);
    Decimal? delta;
    if (alloc != null)
    {
      Decimal num1 = value;
      Decimal? amount = alloc.Amount;
      delta = amount.HasValue ? new Decimal?(num1 - amount.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable = delta;
      Decimal num2 = 0M;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        alloc.Amount = new Decimal?(value);
        ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(alloc);
        article.Allocated = (Decimal[]) null;
      }
    }
    else
    {
      delta = new Decimal?(value);
      Decimal? nullable = delta;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finYear, Equal<Required<OrganizationFinPeriod.finYear>>, And<OrganizationFinPeriod.periodNbr, Equal<Required<OrganizationFinPeriod.periodNbr>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) article.FinYear,
          (object) fieldNbr.ToString("00"),
          (object) PXAccess.GetParentOrganizationID(article.BranchID)
        }));
        if (organizationFinPeriod != null)
        {
          ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(new GLBudgetLineDetail()
          {
            GroupID = article.GroupID,
            BranchID = article.BranchID,
            LedgerID = article.LedgerID,
            FinYear = article.FinYear,
            AccountID = article.AccountID,
            SubID = article.SubID,
            FinPeriodID = organizationFinPeriod.FinPeriodID,
            Amount = new Decimal?(value)
          });
          article.Allocated = (Decimal[]) null;
        }
      }
    }
    this.RollupAllocation(article, fieldNbr, delta);
  }

  protected virtual GLBudgetLineDetail GetAlloc(GLBudgetLine article, int fieldNbr)
  {
    string FinPeriodId = article.FinYear + fieldNbr.ToString("00");
    if (!article.GroupID.HasValue)
      return (GLBudgetLineDetail) null;
    if (this.AllocationIndex != null)
      return this.AllocationIndex.Get(article.GroupID.Value, FinPeriodId);
    foreach (PXResult<GLBudgetLineDetail> pxResult in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
    {
      (object) article.FinYear,
      (object) article.GroupID
    }))
    {
      GLBudgetLineDetail alloc = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
      if (alloc.FinPeriodID == FinPeriodId)
        return alloc;
    }
    return (GLBudgetLineDetail) null;
  }

  protected virtual void EnsureAlloc(GLBudgetLine article)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree || article.Allocated != null)
      return;
    article.Allocated = new Decimal[this._Periods];
    if (this.AllocationIndex != null)
    {
      Guid? groupId = article.GroupID;
      if (!groupId.HasValue)
        return;
      GLBudgetEntry.GLBudgetLineDetailIdx allocationIndex = this.AllocationIndex;
      groupId = article.GroupID;
      Guid GroupID = groupId.Value;
      foreach (GLBudgetLineDetail budgetLineDetail in allocationIndex.GetList(GroupID))
      {
        int index = int.Parse(budgetLineDetail.FinPeriodID.Substring(4)) - 1;
        article.Allocated[index] = budgetLineDetail.Amount.GetValueOrDefault();
      }
    }
    else
    {
      foreach (PXResult<GLBudgetLineDetail> pxResult in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
      {
        (object) article.FinYear,
        (object) article.GroupID
      }))
      {
        GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
        int index = int.Parse(budgetLineDetail.FinPeriodID.Substring(4)) - 1;
        article.Allocated[index] = budgetLineDetail.Amount.GetValueOrDefault();
      }
    }
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    IEnumerable enumerable = ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    this.suppressIDs = viewName == "BudgetArticles";
    if (((PXGraph) this).IsImport && viewName == "Tree" && ((PXSelectBase<BudgetFilter>) this.Filter).Current.ShowTree.GetValueOrDefault() && enumerable is IList && ((ICollection) enumerable).Count == 1)
      this.CurrentSelected.Group = ((PXSelectBase<GLBudgetLine>) this.Tree).Current.GroupID;
    return enumerable;
  }

  protected virtual GLBudgetLine GetPrevArticle(GLBudgetLine article)
  {
    PXResultset<GLBudgetLine> pxResultset;
    if (article.FinYear == null)
      pxResultset = (PXResultset<GLBudgetLine>) null;
    else
      pxResultset = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<GLBudgetLine.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<GLBudgetLine.ledgerID>>, And<GLBudgetLine.accountID, Equal<Current<GLBudgetLine.accountID>>, And<GLBudgetLine.subID, Equal<Current<GLBudgetLine.subID>>, And<GLBudgetLine.finYear, Equal<Required<GLBudgetLine.finYear>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) article
      }, new object[1]
      {
        (object) (int.Parse(article.FinYear) - 1).ToString((IFormatProvider) CultureInfo.InvariantCulture)
      });
    return PXResultset<GLBudgetLine>.op_Implicit(pxResultset);
  }

  protected virtual void PopulateComparison(int? branchID, int? ledgerID, string finYear)
  {
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      GLBudgetLine o = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      o.Compared = new Decimal[this._Periods];
      this.HoldNotchanged(((PXSelectBase) this.BudgetArticles).Cache, (object) o);
    }
    if (!branchID.HasValue || !ledgerID.HasValue || string.IsNullOrEmpty(finYear))
      return;
    Ledger ledger = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<BudgetFilter.compareToLedgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledgerID
    }));
    bool isDirty = ((PXSelectBase) this.BudgetArticles).Cache.IsDirty;
    Dictionary<Guid, GLBudgetLine> dictionary1 = GraphHelper.RowCast<GLBudgetLine>((IEnumerable) PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<True>, And2<Exists<Select<GLBudgetLine2, Where<GLBudgetLine2.parentGroupID, Equal<GLBudgetLine.groupID>>>>, And<Match<Current<AccessInfo.userName>>>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<GLBudgetLine, Guid, GLBudgetLine>((Func<GLBudgetLine, Guid>) (_ => _.GroupID.Value), (Func<GLBudgetLine, GLBudgetLine>) (_ => _));
    PXSelectBase<GLBudgetLine> pxSelectBase1 = (PXSelectBase<GLBudgetLine>) new PXSelectReadonly2<GLBudgetLine, InnerJoin<MasterFinPeriod, On<True, Equal<True>>, InnerJoin<Account, On<Account.accountID, Equal<GLBudgetLine.accountID>>, LeftJoin<GLHistory, On<GLHistory.accountID, Equal<GLBudgetLine.accountID>, And<GLHistory.subID, Equal<GLBudgetLine.subID>, And<GLHistory.finPeriodID, Equal<MasterFinPeriod.finPeriodID>, And<GLHistory.ledgerID, Equal<Required<BudgetFilter.compareToLedgerID>>, And<GLHistory.branchID, Equal<Required<BudgetFilter.compareToBranchID>>>>>>>>>>, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<MasterFinPeriod.finYear, Equal<Required<BudgetFilter.compareToFinYear>>, And<Match<Current<AccessInfo.userName>>>>>>>>((PXGraph) this);
    PXSelectBase<GLHistory> pxSelectBase2 = (PXSelectBase<GLHistory>) new PXSelectReadonly2<GLHistory, LeftJoin<Account, On<GLHistory.accountID, Equal<Account.accountID>>, LeftJoin<Sub, On<GLHistory.subID, Equal<Sub.subID>>, InnerJoin<MasterFinPeriod, On<True, Equal<True>, And<GLHistory.finPeriodID, Equal<MasterFinPeriod.finPeriodID>, And<GLHistory.ledgerID, Equal<Required<BudgetFilter.compareToLedgerID>>, And<GLHistory.branchID, Equal<Required<BudgetFilter.compareToBranchID>>>>>>>>>, Where<MasterFinPeriod.finYear, Equal<Required<BudgetFilter.compareToFinYear>>, And<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>, And<Account.accountCD, Like<Required<Account.accountCD>>, And<Sub.subCD, Like<Required<Sub.subCD>>>>>>>((PXGraph) this);
    PXCache<GLBudgetLine> pxCache = GraphHelper.Caches<GLBudgetLine>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectBase1).View, (IEnumerable<Type>) new List<Type>()
    {
      typeof (GLBudgetLine),
      typeof (GLHistory.curyFinPtdDebit),
      typeof (GLHistory.curyFinPtdCredit),
      typeof (GLHistory.curyFinPtdCredit),
      typeof (GLHistory.curyFinPtdDebit),
      typeof (Account.type),
      typeof (MasterFinPeriod.finPeriodID)
    }, true))
    {
      foreach (PXResult<GLBudgetLine, MasterFinPeriod, Account, GLHistory> pxResult1 in pxSelectBase1.Select(new object[3]
      {
        (object) ledgerID,
        (object) branchID,
        (object) finYear
      }))
      {
        GLBudgetLine glBudgetLine = PXResult<GLBudgetLine, MasterFinPeriod, Account, GLHistory>.op_Implicit(pxResult1);
        GLBudgetLine article = pxCache.Locate(glBudgetLine);
        GLHistory glHistory1 = PXResult<GLBudgetLine, MasterFinPeriod, Account, GLHistory>.op_Implicit(pxResult1);
        Account account = PXResult<GLBudgetLine, MasterFinPeriod, Account, GLHistory>.op_Implicit(pxResult1);
        MasterFinPeriod masterFinPeriod = PXResult<GLBudgetLine, MasterFinPeriod, Account, GLHistory>.op_Implicit(pxResult1);
        int period = int.Parse(masterFinPeriod.FinPeriodID.Substring(4)) - 1;
        if (period + 1 <= this._Periods)
        {
          Decimal? nullable = glHistory1.CuryFinPtdDebit;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = glHistory1.CuryFinPtdCredit;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          Decimal num1 = valueOrDefault1 - valueOrDefault2;
          nullable = glHistory1.CuryFinPtdCredit;
          Decimal valueOrDefault3 = nullable.GetValueOrDefault();
          nullable = glHistory1.CuryFinPtdDebit;
          Decimal valueOrDefault4 = nullable.GetValueOrDefault();
          Decimal num2 = valueOrDefault3 - valueOrDefault4;
          if (article.AccountID.HasValue && article.SubID.HasValue && (article.AccountMask.Contains<char>('?') || article.SubMask.Contains<char>('?')) && ledger.BalanceType != "B")
          {
            num1 = 0M;
            num2 = 0M;
            using (new PXFieldScope(((PXSelectBase) pxSelectBase2).View, new Type[4]
            {
              typeof (GLHistory.curyFinPtdDebit),
              typeof (GLHistory.curyFinPtdCredit),
              typeof (GLHistory.curyFinPtdCredit),
              typeof (GLHistory.curyFinPtdDebit)
            }))
            {
              foreach (PXResult<GLHistory, Account, Sub, MasterFinPeriod> pxResult2 in pxSelectBase2.Select(new object[6]
              {
                (object) ledgerID,
                (object) branchID,
                (object) finYear,
                (object) masterFinPeriod.FinPeriodID,
                (object) SubCDUtils.CreateSubCDWildcard(article.AccountMask, "ACCOUNT"),
                (object) SubCDUtils.CreateSubCDWildcard(article.SubMask, "SUBACCOUNT")
              }))
              {
                GLHistory glHistory2 = PXResult<GLHistory, Account, Sub, MasterFinPeriod>.op_Implicit(pxResult2);
                Decimal num3 = num1;
                nullable = glHistory2.CuryFinPtdDebit;
                Decimal valueOrDefault5 = nullable.GetValueOrDefault();
                nullable = glHistory2.CuryFinPtdCredit;
                Decimal valueOrDefault6 = nullable.GetValueOrDefault();
                Decimal num4 = valueOrDefault5 - valueOrDefault6;
                num1 = num3 + num4;
                Decimal num5 = num2;
                nullable = glHistory2.CuryFinPtdCredit;
                Decimal valueOrDefault7 = nullable.GetValueOrDefault();
                nullable = glHistory2.CuryFinPtdDebit;
                Decimal valueOrDefault8 = nullable.GetValueOrDefault();
                Decimal num6 = valueOrDefault7 - valueOrDefault8;
                num2 = num5 + num6;
              }
            }
          }
          if (article.Compared == null)
            article.Compared = new Decimal[this._Periods];
          Guid? groupId = article.GroupID;
          if (groupId.HasValue)
          {
            Dictionary<Guid, GLBudgetLine> dictionary2 = dictionary1;
            groupId = article.GroupID;
            Guid key = groupId.Value;
            if (dictionary2.ContainsKey(key))
              this.SaveRolledUpBudgetLines(dictionary1);
          }
          if (account.Type == "A" || account.Type == "E")
          {
            article.Compared[period] = num1;
            this.RollupComparison(article, period, new Decimal?(num1), dictionary1);
          }
          else
          {
            article.Compared[period] = num2;
            this.RollupComparison(article, period, new Decimal?(num2), dictionary1);
          }
        }
        else
          break;
      }
    }
    this.SaveRolledUpBudgetLines(dictionary1);
    if (isDirty)
      return;
    ((PXSelectBase) this.BudgetArticles).Cache.IsDirty = false;
  }

  private void RollupComparison(
    GLBudgetLine article,
    int period,
    Decimal? delta,
    Dictionary<Guid, GLBudgetLine> parentNodes)
  {
    if (this.IsNullOrEmpty(article.ParentGroupID))
      return;
    GLBudgetLine parentNode = parentNodes[article.ParentGroupID.Value];
    if (parentNode == null)
      return;
    if (parentNode.Compared == null)
      parentNode.Compared = new Decimal[this._Periods];
    parentNode.Compared[period] += delta.Value;
    parentNode.IsRolledUp = new bool?(true);
    this.RollupComparison(parentNode, period, delta, parentNodes);
  }

  private void SaveRolledUpBudgetLines(Dictionary<Guid, GLBudgetLine> parentNodes)
  {
    foreach (GLBudgetLine glBudgetLine in parentNodes.Values.Where<GLBudgetLine>((Func<GLBudgetLine, bool>) (_ => _.IsRolledUp.GetValueOrDefault())))
    {
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(glBudgetLine);
      glBudgetLine.IsRolledUp = new bool?(false);
    }
  }

  private void RollupArticleAmount(GLBudgetLine article, Decimal? delta)
  {
    if (this.IsNullOrEmpty(article.ParentGroupID))
      return;
    Decimal? nullable1 = delta;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    GLBudgetLine article1 = this.GetArticle(article.BranchID, article.LedgerID, article.FinYear, article.ParentGroupID);
    if (article1 == null)
      return;
    GLBudgetLine glBudgetLine1 = article1;
    nullable1 = article1.Amount;
    Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
    glBudgetLine1.Amount = nullable2;
    GLBudgetLine glBudgetLine2 = article1;
    nullable1 = glBudgetLine2.Amount;
    Decimal? nullable3 = delta;
    glBudgetLine2.Amount = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(article1);
  }

  protected virtual void RollupAllocation(GLBudgetLine article, int fieldNbr, Decimal? delta)
  {
    if (this.IsNullOrEmpty(article.ParentGroupID))
      return;
    Decimal? nullable1 = delta;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    GLBudgetLine articleByCurrentFilter = this.GetArticleByCurrentFilter(article.ParentGroupID);
    if (articleByCurrentFilter == null)
      return;
    GLBudgetLineDetail alloc1 = this.GetAlloc(articleByCurrentFilter, fieldNbr);
    GLBudgetLineDetail budgetLineDetail1;
    if (alloc1 != null && alloc1.LedgerID.HasValue)
    {
      GLBudgetLineDetail budgetLineDetail2 = alloc1;
      nullable1 = budgetLineDetail2.Amount;
      Decimal? nullable2 = delta;
      budgetLineDetail2.Amount = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      budgetLineDetail1 = ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(alloc1);
    }
    else
    {
      GLBudgetLineDetail alloc2 = new GLBudgetLineDetail();
      alloc2.GroupID = articleByCurrentFilter.GroupID;
      alloc2.BranchID = articleByCurrentFilter.BranchID;
      alloc2.LedgerID = articleByCurrentFilter.LedgerID;
      alloc2.FinYear = articleByCurrentFilter.FinYear;
      alloc2.AccountID = articleByCurrentFilter.AccountID;
      alloc2.SubID = articleByCurrentFilter.SubID;
      alloc2.FinPeriodID = articleByCurrentFilter.FinYear + fieldNbr.ToString("00");
      alloc2.Amount = delta;
      if (this.AllocationIndex != null)
        this.AllocationIndex.Add(alloc2);
      budgetLineDetail1 = ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(alloc2);
    }
    articleByCurrentFilter.Allocated = (Decimal[]) null;
    articleByCurrentFilter.Released = new bool?(false);
    articleByCurrentFilter.WasReleased = new bool?(false);
    this.RollupAllocation(articleByCurrentFilter, fieldNbr, delta);
  }

  private GLBudgetLine PutInGroup(string AccountID, string SubID, GLBudgetLine parentArticle)
  {
    GLBudgetLine glBudgetLine1 = new GLBudgetLine();
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<True>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) parentArticle.GroupID
    }))
    {
      GLBudgetLine parentArticle1 = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      if (this.MatchMask(SubID, parentArticle1.SubMask ?? string.Empty) && this.MatchMask(AccountID, parentArticle1.AccountMask ?? string.Empty))
      {
        GLBudgetLine glBudgetLine2 = this.PutInGroup(AccountID, SubID, parentArticle1);
        Guid? nullable1 = glBudgetLine2.GroupID;
        if (!nullable1.HasValue)
        {
          nullable1 = glBudgetLine2.ParentGroupID;
          if (!nullable1.HasValue)
          {
            GLBudgetLine glBudgetLine3 = glBudgetLine1;
            nullable1 = parentArticle1.GroupID;
            Guid? nullable2 = new Guid?(nullable1.Value);
            glBudgetLine3.ParentGroupID = nullable2;
            glBudgetLine1.GroupMask = parentArticle1.GroupMask;
            continue;
          }
        }
        glBudgetLine1 = glBudgetLine2;
      }
    }
    return glBudgetLine1;
  }

  private GLBudgetLine PutIntoInnerGroup(GLBudgetLine newArticle, GLBudgetLine parentArticle)
  {
    GLBudgetLine glBudgetLine = parentArticle;
    if (parentArticle.IsGroup.GetValueOrDefault())
    {
      foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) parentArticle.GroupID
      }))
      {
        GLBudgetLine parentArticle1 = PXResult<GLBudgetLine>.op_Implicit(pxResult);
        this.GetArticleByCurrentFilter(parentArticle.GroupID);
        if (this.MatchMask(PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) newArticle.AccountID
        })).AccountCD, parentArticle1.AccountMask ?? string.Empty))
        {
          if (this.MatchMask(PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) newArticle.SubID
          })).SubCD, parentArticle1.SubMask ?? string.Empty))
          {
            glBudgetLine = parentArticle1;
            glBudgetLine = this.PutIntoInnerGroup(newArticle, parentArticle1);
          }
        }
      }
    }
    return glBudgetLine;
  }

  private GLBudgetLine LocateInBudgetArticlesCache(
    List<GLBudgetLine> existingArticles,
    GLBudgetLine line,
    bool IncludeGroups)
  {
    foreach (GLBudgetLine existingArticle in existingArticles)
    {
      PXEntryStatus status = ((PXSelectBase) this.BudgetArticles).Cache.GetStatus((object) existingArticle);
      if (status != 3 && status != 4)
      {
        Guid? groupId1 = existingArticle.GroupID;
        Guid? groupId2 = line.GroupID;
        if ((groupId1.HasValue == groupId2.HasValue ? (groupId1.HasValue ? (groupId1.GetValueOrDefault() != groupId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          int? nullable = existingArticle.BranchID;
          if (nullable.Equals((object) line.BranchID))
          {
            nullable = existingArticle.LedgerID;
            if (nullable.Equals((object) line.LedgerID) && existingArticle.FinYear.Equals(line.FinYear))
            {
              nullable = existingArticle.AccountID;
              if (nullable.Equals((object) line.AccountID))
              {
                nullable = existingArticle.SubID;
                if (nullable.Equals((object) line.SubID) && (IncludeGroups || !existingArticle.IsGroup.Value))
                  return existingArticle;
              }
            }
          }
        }
      }
    }
    return (GLBudgetLine) null;
  }

  protected GLBudgetLine GetArticle(int? branchID, int? ledgerID, string finyear, Guid? groupID)
  {
    if (this.IsNullOrEmpty(groupID))
      return (GLBudgetLine) null;
    GLBudgetLine article = (GLBudgetLine) ((PXSelectBase) this.BudgetArticles).Cache.Locate((object) new GLBudgetLine()
    {
      BranchID = branchID,
      LedgerID = ledgerID,
      FinYear = finyear,
      GroupID = groupID
    });
    if (article != null)
      return article;
    return PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>, And<GLBudgetLine.branchID, Equal<Required<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Required<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Required<BudgetFilter.finYear>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) groupID,
      (object) branchID,
      (object) ledgerID,
      (object) finyear
    }));
  }

  protected GLBudgetLine GetArticleByCurrentFilter(Guid? groupID)
  {
    return this.GetArticle(((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID, ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID, ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear, groupID);
  }

  private bool setArticleToParentGroup(
    Dictionary<Guid, HashSet<GLBudgetLine>> articlesByParentGroup,
    GLBudgetLine article)
  {
    HashSet<GLBudgetLine> source = (HashSet<GLBudgetLine>) null;
    if (!articlesByParentGroup.TryGetValue(article.ParentGroupID.Value, out source))
    {
      source = new HashSet<GLBudgetLine>();
      articlesByParentGroup.Add(article.ParentGroupID.Value, source);
    }
    if (source.Contains<GLBudgetLine>(article))
      return false;
    source.Add(article);
    return true;
  }

  private Guid PutIntoNewInnerGroup(
    Guid groupID,
    GLBudgetLine article,
    IEnumerable<GLBudgetLine> articleGroups)
  {
    Guid groupID1 = groupID;
    foreach (GLBudgetLine glBudgetLine in groupID == Guid.Empty ? articleGroups : articleGroups.Where<GLBudgetLine>((Func<GLBudgetLine, bool>) (x =>
    {
      Guid? parentGroupId = x.ParentGroupID;
      Guid guid = groupID;
      return parentGroupId.HasValue && parentGroupId.GetValueOrDefault() == guid;
    })))
    {
      Guid? groupId1 = article.GroupID;
      Guid? groupId2 = glBudgetLine.GroupID;
      if ((groupId1.HasValue == groupId2.HasValue ? (groupId1.HasValue ? (groupId1.GetValueOrDefault() != groupId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && glBudgetLine.IsGroup.Value && glBudgetLine.AccountMask != null && glBudgetLine.SubMask != null && this.MatchMask(article.AccountMask, glBudgetLine.AccountMask) && this.MatchMask(article.SubMask, glBudgetLine.SubMask))
      {
        groupID1 = glBudgetLine.GroupID.Value;
        groupID1 = this.PutIntoNewInnerGroup(groupID1, article, articleGroups);
      }
    }
    return groupID1;
  }

  private PXResultset<GLBudgetLine> collectChildNodes(Guid? GroupID)
  {
    PXResultset<GLBudgetLine> pxResultset = new PXResultset<GLBudgetLine>();
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<Match<Current<AccessInfo.userName>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      pxResultset.Add(pxResult);
      if (glBudgetLine.IsGroup.GetValueOrDefault())
        pxResultset.AddRange((IEnumerable<PXResult<GLBudgetLine>>) this.collectChildNodes(glBudgetLine.GroupID));
    }
    return pxResultset;
  }

  private void CheckBudgetLinesForDuplicates()
  {
    bool flag = false;
    foreach (GLBudgetLine duplicate in this.FindDuplicates())
    {
      flag = true;
      ((PXSelectBase) this.BudgetArticles).Cache.RaiseExceptionHandling<GLBudgetLine.accountID>((object) duplicate, (object) duplicate.AccountID, (Exception) new PXSetPropertyException("Duplicate GL Account/Sub Entry", (PXErrorLevel) 5));
    }
    if (flag)
      throw new PXException("Duplicate GL Account/Sub Entry");
  }

  private IEnumerable<GLBudgetLine> FindDuplicates()
  {
    GLBudgetEntry graph = this;
    Dictionary<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine> listCached = new Dictionary<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>();
    List<GLBudgetEntry.BudgetKey> budgetsInIndex = new List<GLBudgetEntry.BudgetKey>();
    foreach (GLBudgetLine line in (IEnumerable<GLBudgetLine>) GraphHelper.RowCast<GLBudgetLine>(((PXSelectBase) graph.BudgetArticles).Cache.Cached).OrderBy<GLBudgetLine, PXEntryStatus>(new Func<GLBudgetLine, PXEntryStatus>(((PXSelectBase) graph.BudgetArticles).Cache.GetStatus)))
    {
      bool? nullable = line.IsGroup;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) graph.BudgetArticles).Cache.GetStatus((object) line), (PXEntryStatus) 3, (PXEntryStatus) 4))
      {
        nullable = line.Comparison;
        if (!nullable.GetValueOrDefault())
        {
          GLBudgetEntry.GLBudgetLineKey glBudgetLineKey = GLBudgetEntry.GLBudgetLineKey.Create(line);
          if (!budgetsInIndex.Any<GLBudgetEntry.BudgetKey>(new Func<GLBudgetEntry.BudgetKey, bool>(glBudgetLineKey.Match)))
          {
            GLBudgetEntry.BudgetKey budgetKey = new GLBudgetEntry.BudgetKey(glBudgetLineKey);
            IEnumerable<GLBudgetLine> glBudgetLines = GraphHelper.RowCast<GLBudgetLine>((IEnumerable) budgetKey.SelectLines((PXGraph) graph));
            Func<GLBudgetLine, PXEntryStatus> func = new Func<GLBudgetLine, PXEntryStatus>(((PXSelectBase) graph.BudgetArticles).Cache.GetStatus);
            PXEntryStatus[] pxEntryStatusArray = new PXEntryStatus[1]
            {
              (PXEntryStatus) 5
            };
            EnumerableExtensions.AddRange<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>((IDictionary<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>) listCached, (IEnumerable<KeyValuePair<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>>) EnumerableExtensions.OrderBy<GLBudgetLine, PXEntryStatus>(glBudgetLines, func, pxEntryStatusArray).GroupBy<GLBudgetLine, GLBudgetEntry.GLBudgetLineKey>(new Func<GLBudgetLine, GLBudgetEntry.GLBudgetLineKey>(GLBudgetEntry.GLBudgetLineKey.Create)).ToDictionary<IGrouping<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>, GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>((Func<IGrouping<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>, GLBudgetEntry.GLBudgetLineKey>) (_ => _.Key), (Func<IGrouping<GLBudgetEntry.GLBudgetLineKey, GLBudgetLine>, GLBudgetLine>) (_ => _.First<GLBudgetLine>())));
            budgetsInIndex.Add(budgetKey);
          }
          GLBudgetLine glBudgetLine;
          if (listCached.TryGetValue(glBudgetLineKey, out glBudgetLine))
          {
            if (glBudgetLine != line)
              yield return line;
          }
          else
            listCached.Add(glBudgetLineKey, line);
        }
      }
    }
  }

  protected void RemoveDuplicates()
  {
    EnumerableExtensions.ForEach<GLBudgetLine>(this.FindDuplicates(), (Action<GLBudgetLine>) (_ => ((PXSelectBase) this.BudgetArticles).Cache.Delete((object) _)));
  }

  private void CheckBudgetLinesForComparisonData()
  {
    foreach (PXResult<GLBudgetLine> pxResult in ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Select(Array.Empty<object>()))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      bool? comparison = glBudgetLine.Comparison;
      if (comparison.HasValue)
      {
        comparison = glBudgetLine.Comparison;
        if (comparison.Value)
        {
          ((PXSelectBase) this.BudgetArticles).Cache.RaiseExceptionHandling<GLBudgetLine.accountID>((object) glBudgetLine, (object) glBudgetLine.AccountID, (Exception) new PXSetPropertyException("Budget articles cannot be preloaded because the table already contains data for comparison. To proceed, clear the Compare to Year box.", (PXErrorLevel) 5));
          throw new PXException("Budget articles cannot be preloaded because the table already contains data for comparison. To proceed, clear the Compare to Year box.");
        }
      }
    }
  }

  public void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.None;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    this.isImportStarted = this.isImportStarted || !((PXGraph) this).IsDirty ? true : throw new PXException("Save or cancel data modifications before import.");
    if (!this.IndexesIsPrepared)
      this.PrepareIndexes();
    GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check = (GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine) ((line, sourceLine) =>
    {
      if (line == null)
        return (GLBudgetLine) null;
      if (this.IsNullOrEmpty(line.GroupID))
        return (GLBudgetLine) null;
      PXEntryStatus status = ((PXSelectBase) this.BudgetArticles).Cache.GetStatus((object) line);
      return status == 3 || status == 4 ? (GLBudgetLine) null : line;
    });
    int? nullable1 = new int?();
    string cd = values[(object) "AccountID"] as string;
    if (!string.IsNullOrEmpty(cd))
      nullable1 = this.accountIndex.GetID(cd);
    GLBudgetEntry.SubIndex subIndex = this.subIndex;
    if (!(values[(object) "SubID"] is string defaultCd))
      defaultCd = this.subIndex.DefaultCD;
    int? id = subIndex.GetID(defaultCd);
    if ((!nullable1.HasValue || !id.HasValue && this.needSubDimensionValidate || !this.SubEnabled) && (!nullable1.HasValue || this.SubEnabled))
      return false;
    GLBudgetLine byKey = this.ArticleIndex.GetByKey(GLBudgetEntry.GLBudgetLineKey.Create(values, this), check);
    if (byKey != null && byKey.SubID.HasValue)
    {
      if (byKey.IsGroup.Value)
        return false;
      values.Add((object) "IsUploaded", (object) true);
      values.Add((object) "GroupID", (object) byKey.GroupID);
      values.Add((object) "ParentGroupID", (object) byKey.ParentGroupID);
      values.Add((object) "GroupMask", (object) byKey.GroupMask);
      keys[(object) "GroupID"] = (object) byKey.GroupID;
      keys[(object) "ParentGroupID"] = (object) byKey.ParentGroupID;
    }
    else
    {
      Guid guid1 = Guid.NewGuid();
      values.Add((object) "GroupID", (object) guid1);
      keys[(object) "GroupID"] = (object) guid1;
      Guid? nullable2;
      ref Guid? local1 = ref nullable2;
      Guid? nullable3 = (Guid?) this.CurrentSelected?.Group;
      Guid guid2 = nullable3 ?? Guid.Empty;
      local1 = new Guid?(guid2);
      values.Add((object) "ParentGroupID", (object) nullable2);
      keys[(object) "ParentGroupID"] = (object) nullable2;
      values.Add((object) "IsUploaded", (object) true);
      foreach (PXResult<GLBudgetLine> articleGroup in this.ArticleGroups)
      {
        GLBudgetLine parentArticle = PXResult<GLBudgetLine>.op_Implicit(articleGroup);
        if ((this.SubEnabled && parentArticle.AccountMask != null && parentArticle.AccountMask != string.Empty && parentArticle.SubMask != null && parentArticle.SubMask != string.Empty || !this.SubEnabled && parentArticle.AccountMask != null && parentArticle.AccountMask != string.Empty && parentArticle.SubMask != null) && (this.SubEnabled && this.MatchMask(values[(object) "AccountID"].ToString().Trim(), parentArticle.AccountMask) && this.MatchMask(values[(object) "SubID"].ToString().Trim(), parentArticle.SubMask) || !this.SubEnabled && this.MatchMask(values[(object) "AccountID"].ToString().Trim(), parentArticle.AccountMask)))
        {
          GLBudgetLine glBudgetLine = this.PutInGroup(values[(object) "AccountID"].ToString().Trim(), (values[(object) "SubID"] ?? (object) string.Empty).ToString().Trim(), parentArticle);
          IDictionary dictionary = values;
          nullable3 = glBudgetLine.ParentGroupID;
          // ISSUE: variable of a boxed type
          __Boxed<Guid?> local2 = (ValueType) (nullable3 ?? parentArticle.GroupID);
          dictionary[(object) "ParentGroupID"] = (object) local2;
          keys[(object) "ParentGroupID"] = values[(object) "ParentGroupID"];
          if (parentArticle.GroupMask != null)
          {
            if (!values.Contains((object) "GroupMask"))
              values.Add((object) "GroupMask", (object) (glBudgetLine.GroupMask ?? parentArticle.GroupMask));
            else
              values[(object) "GroupMask"] = (object) (glBudgetLine.GroupMask ?? parentArticle.GroupMask);
          }
        }
      }
    }
    for (int index = 1; index <= this._Periods; ++index)
    {
      string key = this._PrefixPeriodField + index.ToString();
      if (values.Contains((object) key))
      {
        try
        {
          values[(object) key] = (object) this.ParseAmountValue(values[(object) key]);
        }
        catch (Exception ex)
        {
        }
      }
    }
    if (values.Contains((object) "Amount"))
    {
      try
      {
        values[(object) "Amount"] = (object) this.ParseAmountValue(values[(object) "Amount"]);
      }
      catch (Exception ex)
      {
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [PXUIField]
  [PXCancelButton]
  public virtual IEnumerable cancel(PXAdapter adapter)
  {
    BudgetFilter current1 = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
    BudgetPreloadFilter current2 = ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current;
    current1.CompareToFinYear = (string) null;
    current1.SubIDFilter = (string) null;
    ((PXGraph) this).Clear();
    ((PXSelectBase) this.Filter).Cache.RestoreCopy((object) ((PXSelectBase<BudgetFilter>) this.Filter).Current, (object) current1);
    ((PXSelectBase) this.PreloadFilter).Cache.RestoreCopy((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) current2);
    return adapter.Get();
  }

  [PXUIField]
  [PXDeleteButton]
  public virtual IEnumerable delete(PXAdapter adapter)
  {
    bool flag = true;
    PXResultset<GLBudgetLine> pxResultset = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    foreach (PXResult<GLBudgetLine> pxResult in pxResultset)
    {
      Decimal? releasedAmount = PXResult<GLBudgetLine>.op_Implicit(pxResult).ReleasedAmount;
      Decimal num = 0M;
      if (releasedAmount.GetValueOrDefault() > num & releasedAmount.HasValue)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      using (new SuppressDeletingDialogBoxScope())
      {
        using (new SuppressDeletingDialogBoxScope())
        {
          foreach (PXResult<GLBudgetLine> pxResult in pxResultset)
            ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(PXResult<GLBudgetLine>.op_Implicit(pxResult));
        }
      }
      ((PXAction) this.Save).Press();
      ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = (string) null;
    }
    else
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Delete Budget", "The budget cannot be deleted because at least one of its budget articles has been released.", (MessageButtons) 0);
    return adapter.Get();
  }

  [PXUIField]
  [PXPreviousButton(ConfirmationMessage = null)]
  public virtual IEnumerable prev(PXAdapter adapter)
  {
    if (!((PXSelectBase) this.BudgetArticles).Cache.IsDirty)
    {
      GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelectGroupBy<GLBudgetLine, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Less<Current<BudgetFilter.finYear>>>>, Aggregate<Max<GLBudgetLine.finYear>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (glBudgetLine == null || glBudgetLine.FinYear == null)
        return ((PXAction) this.Last).Press(adapter);
      ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = glBudgetLine.FinYear;
      ((PXSelectBase<BudgetFilter>) this.Filter).Update(((PXSelectBase<BudgetFilter>) this.Filter).Current);
    }
    else
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Pending Changes", "The budget has pending changes. Review the budget and then save or discard your changes before selecting another budget.", (MessageButtons) 0);
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton(ConfirmationMessage = null)]
  public virtual IEnumerable next(PXAdapter adapter)
  {
    if (!((PXSelectBase) this.BudgetArticles).Cache.IsDirty)
    {
      GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelectGroupBy<GLBudgetLine, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Greater<Required<BudgetFilter.finYear>>>>, Aggregate<Min<GLBudgetLine.finYear>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) (((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear ?? "")
      }));
      if (glBudgetLine == null || glBudgetLine.FinYear == null)
        return ((PXAction) this.First).Press(adapter);
      ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = glBudgetLine.FinYear;
      ((PXSelectBase<BudgetFilter>) this.Filter).Update(((PXSelectBase<BudgetFilter>) this.Filter).Current);
    }
    else
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Pending Changes", "The budget has pending changes. Review the budget and then save or discard your changes before selecting another budget.", (MessageButtons) 0);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable wnext(PXAdapter adapter)
  {
    bool flag = false;
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID.HasValue)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.ledgerID>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear == null)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.finYear>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent.HasValue)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.changePercent>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (!this.MatchMask(((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter ?? string.Empty, this.CurrentSelected.AccountMask ?? string.Empty))
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.accountIDFilter>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Selected account is not allowed in this group (Account mask: {0})"), (object) this.CurrentSelected.AccountMask)));
      flag = true;
    }
    if (!this.MatchMask(((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.SubIDFilter ?? string.Empty, this.CurrentSelected.SubMask ?? string.Empty))
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.subIDFilter>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.SubIDFilter, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Selected subaccount is not allowed in this group or does not exist (Subaccount mask: {0})"), (object) this.CurrentSelected.SubMask)));
      flag = true;
    }
    int num = flag ? 1 : 0;
    return adapter.Get();
  }

  [PXUIField]
  [PXFirstButton(ConfirmationMessage = null)]
  public virtual IEnumerable first(PXAdapter adapter)
  {
    if (!((PXSelectBase) this.BudgetArticles).Cache.IsDirty)
    {
      GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelectGroupBy<GLBudgetLine, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>>, Aggregate<Min<GLBudgetLine.finYear>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (glBudgetLine != null && glBudgetLine.FinYear != null)
      {
        ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = glBudgetLine.FinYear;
        ((PXSelectBase<BudgetFilter>) this.Filter).Update(((PXSelectBase<BudgetFilter>) this.Filter).Current);
      }
    }
    else
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Pending Changes", "The budget has pending changes. Review the budget and then save or discard your changes before selecting another budget.", (MessageButtons) 0);
    return adapter.Get();
  }

  [PXUIField]
  [PXLastButton(ConfirmationMessage = null)]
  public virtual IEnumerable last(PXAdapter adapter)
  {
    if (!((PXSelectBase) this.BudgetArticles).Cache.IsDirty)
    {
      GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelectGroupBy<GLBudgetLine, Where<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>>, Aggregate<Max<GLBudgetLine.finYear>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (glBudgetLine != null && glBudgetLine.FinYear != null)
      {
        ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = glBudgetLine.FinYear;
        ((PXSelectBase<BudgetFilter>) this.Filter).Update(((PXSelectBase<BudgetFilter>) this.Filter).Current);
      }
    }
    else
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Pending Changes", "The budget has pending changes. Review the budget and then save or discard your changes before selecting another budget.", (MessageButtons) 0);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void distribute()
  {
    ((PXSelectBase<BudgetDistributeFilter>) this.DistrFilter).AskExt();
  }

  [PXUIField]
  [PXButton]
  public virtual void distributeOK()
  {
    BudgetDistributeFilter current = ((PXSelectBase<BudgetDistributeFilter>) this.DistrFilter).Current;
    OrganizationFinYear organizationFinYear = PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelect<OrganizationFinYear, Where<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear,
      (object) PXAccess.GetParentOrganizationID(((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID)
    }));
    int num1 = this._Periods > (int) organizationFinYear.FinPeriods.Value ? (int) organizationFinYear.FinPeriods.Value : this._Periods;
    int num2 = num1;
    bool flag = false;
    foreach (PXResult<OrganizationFinPeriod> pxResult in PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finYear, Equal<Required<OrganizationFinPeriod.finYear>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear,
      (object) PXAccess.GetParentOrganizationID(((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID)
    }))
    {
      OrganizationFinPeriod organizationFinPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
      DateTime? endDate = organizationFinPeriod.EndDate;
      DateTime? startDate = organizationFinPeriod.StartDate;
      if ((endDate.HasValue == startDate.HasValue ? (endDate.HasValue ? (endDate.GetValueOrDefault() == startDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && int.Parse(organizationFinPeriod.PeriodNbr) == num1)
      {
        --num1;
        flag = true;
        break;
      }
    }
    bool? nullable1 = current.ApplyToAll;
    PXResultset<GLBudgetLine> pxResultset;
    if (nullable1.HasValue)
    {
      nullable1 = current.ApplyToAll;
      if (nullable1.Value)
      {
        pxResultset = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.parentGroupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.ParentGroupID
        });
        nullable1 = current.ApplyToSubGroups;
        if (nullable1.HasValue)
        {
          nullable1 = current.ApplyToSubGroups;
          if (nullable1.Value)
          {
            pxResultset = this.collectChildNodes(((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.ParentGroupID);
            goto label_14;
          }
          goto label_14;
        }
        goto label_14;
      }
    }
    pxResultset = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<False>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.GroupID
    });
label_14:
    foreach (PXResult<GLBudgetLine> pxResult in pxResultset)
    {
      GLBudgetLine article = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      nullable1 = article.IsGroup;
      if (!nullable1.Value)
      {
        int? nullable2 = article.AccountID;
        if (nullable2.HasValue)
        {
          GLBudgetEntry.AccountIndex accountIndex = this.accountIndex;
          nullable2 = article.AccountID;
          int id1 = nullable2.Value;
          if (accountIndex.IsActive(id1))
          {
            nullable2 = article.SubID;
            if (nullable2.HasValue)
            {
              GLBudgetEntry.SubIndex subIndex = this.subIndex;
              nullable2 = article.SubID;
              int id2 = nullable2.Value;
              if (subIndex.IsActive(id2))
              {
                Decimal? nullable3 = article.Amount;
                Decimal valueOrDefault = nullable3.GetValueOrDefault();
                int num3 = (int) (((PXSelectBase<BudgetFilter>) this.Filter).Current.Precision ?? (short) 2);
                Decimal num4 = (Decimal) (1.0 / Math.Pow(10.0, (double) num3));
                article.Released = new bool?(false);
                Decimal? nullable4;
                Decimal? nullable5;
                switch (current.Method)
                {
                  case "E":
                    Decimal num5 = (Decimal) Math.Round((double) valueOrDefault / (double) num1, num3, MidpointRounding.AwayFromZero);
                    Decimal num6 = valueOrDefault - num5 * (Decimal) num1;
                    if (num6 < 0M)
                    {
                      num5 -= num4;
                      num6 = valueOrDefault - num5 * (Decimal) num1;
                    }
                    this.PrepareIndexes();
                    for (int index = 0; index < num2; ++index)
                    {
                      Decimal num7 = num5;
                      if (num6 > 0M)
                      {
                        num7 += num4;
                        num6 -= num4;
                      }
                      if (flag && index + 1 == num2)
                        num7 = 0M;
                      this.UpdateAlloc(num7, article, index + 1);
                    }
                    continue;
                  case "P":
                    GLBudgetLine prevArticle = this.GetPrevArticle(article);
                    if (prevArticle != null)
                    {
                      Decimal num8 = 0M;
                      int index1 = 0;
                      prevArticle.Allocated = (Decimal[]) null;
                      this.EnsureAlloc(prevArticle);
                      this.PrepareIndexes();
                      for (int index2 = 0; index2 < num2; ++index2)
                      {
                        nullable4 = article.Amount;
                        Decimal num9 = prevArticle.Allocated[index2];
                        nullable3 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * num9) : new Decimal?();
                        nullable5 = prevArticle.AllocatedAmount;
                        Decimal? nullable6;
                        if (!(nullable3.HasValue & nullable5.HasValue))
                        {
                          nullable4 = new Decimal?();
                          nullable6 = nullable4;
                        }
                        else
                          nullable6 = new Decimal?(nullable3.GetValueOrDefault() / nullable5.GetValueOrDefault());
                        nullable4 = nullable6;
                        Decimal num10 = (Decimal) Math.Round((double) nullable4.Value, num3, MidpointRounding.AwayFromZero);
                        num8 += num10;
                        if (prevArticle.Allocated[index2] > prevArticle.Allocated[index1])
                          index1 = index2;
                        this.UpdateAlloc(num10, article, index2 + 1);
                      }
                      GLBudgetLineDetail alloc = this.GetAlloc(article, index1 + 1);
                      if (alloc != null)
                      {
                        nullable3 = alloc.Amount;
                        this.UpdateAlloc(nullable3.GetValueOrDefault() + valueOrDefault - num8, article, index1 + 1);
                      }
                      this.ClearIndices();
                      continue;
                    }
                    continue;
                  case "C":
                    Decimal num11 = 0M;
                    int index3 = 0;
                    Decimal num12 = ((IEnumerable<Decimal>) article.Compared).Sum();
                    if (!(num12 == 0M))
                    {
                      this.PrepareIndexes();
                      for (int index4 = 0; index4 < num2; ++index4)
                      {
                        nullable5 = article.Amount;
                        Decimal num13 = article.Compared[index4];
                        Decimal? nullable7;
                        if (!nullable5.HasValue)
                        {
                          nullable4 = new Decimal?();
                          nullable7 = nullable4;
                        }
                        else
                          nullable7 = new Decimal?(nullable5.GetValueOrDefault() * num13);
                        nullable3 = nullable7;
                        Decimal num14 = num12;
                        Decimal? nullable8;
                        if (!nullable3.HasValue)
                        {
                          nullable5 = new Decimal?();
                          nullable8 = nullable5;
                        }
                        else
                          nullable8 = new Decimal?(nullable3.GetValueOrDefault() / num14);
                        nullable5 = nullable8;
                        Decimal num15 = (Decimal) Math.Round((double) nullable5.Value, num3, MidpointRounding.AwayFromZero);
                        num11 += num15;
                        if (article.Compared[index4] > article.Compared[index3])
                          index3 = index4;
                        this.UpdateAlloc(num15, article, index4 + 1);
                      }
                      GLBudgetLineDetail alloc = this.GetAlloc(article, index3 + 1);
                      if (alloc != null)
                      {
                        nullable3 = alloc.Amount;
                        this.UpdateAlloc(nullable3.GetValueOrDefault() + valueOrDefault - num11, article, index3 + 1);
                        continue;
                      }
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
        }
      }
    }
    this.ClearIndices();
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField]
  public virtual IEnumerable showPreload(PXAdapter adapter)
  {
    this.CheckBudgetLinesForComparisonData();
    ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Select(Array.Empty<object>());
    GLBudgetLine glBudgetLine = PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    string str1 = new string('?', ((PXStringState) ((PXSelectBase) this.PreloadFilter).Cache.GetStateExt((object) null, typeof (BudgetPreloadFilter.fromAccount).Name)).InputMask.Length - 1);
    string str2 = new string('?', ((PXStringState) ((PXSelectBase) this.BudgetArticles).Cache.GetStateExt((object) null, typeof (GLBudgetLine.subID).Name)).InputMask.Length - 1);
    if (glBudgetLine != null)
    {
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetLine != null ? glBudgetLine.AccountMask : string.Empty, "ACCOUNT");
      if (glBudgetLine.AccountMask != null)
      {
        Account account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.active, Equal<True>, And<Account.accountCD, Like<Required<SelectedGroup.accountMaskWildcard>>>>, OrderBy<Asc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard
        }));
        Account account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.active, Equal<True>, And<Account.accountCD, Like<Required<BudgetPreloadFilter.accountCDWildcard>>>>, OrderBy<Desc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard
        }));
        ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FromAccount = (int?) account1?.AccountID;
        ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ToAccount = (int?) account2?.AccountID;
      }
      else
      {
        ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FromAccount = new int?();
        ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ToAccount = new int?();
      }
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter = glBudgetLine.AccountMask ?? str1;
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.SubIDFilter = glBudgetLine.SubMask ?? str2;
    }
    else
    {
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FromAccount = new int?();
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ToAccount = new int?();
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter = str1;
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.SubIDFilter = str2;
    }
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.BranchID.HasValue)
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToBranchID;
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID.HasValue)
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToLedgerId;
    if (((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear == null)
      ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear;
    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).AskExt();
    return adapter.Get();
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField]
  public virtual IEnumerable showManage(PXAdapter adapter)
  {
    ((PXSelectBase<ManageBudgetDialog>) this.ManageDialog).AskExt();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void manageOK()
  {
    ((PXSelectBase<ManageBudgetDialog>) this.ManageDialog).Select(Array.Empty<object>());
    switch (((PXSelectBase<ManageBudgetDialog>) this.ManageDialog).Current.Method)
    {
      case "R":
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GLBudgetEntry.\u003C\u003Ec__DisplayClass111_0 displayClass1110 = new GLBudgetEntry.\u003C\u003Ec__DisplayClass111_0();
        this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.RollbackBudget;
        // ISSUE: reference to a compiler-generated field
        displayClass1110.process = this.PrepareGraphForLongOperation();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1110, __methodptr(\u003CmanageOK\u003Eb__0)));
        break;
      case "C":
        if (((PXSelectBase<BudgetFilter>) this.Filter).Ask("Convert Budget", "Warning: This action is irreversible. Are you sure you want to convert budget?", (MessageButtons) 1) != 1)
          break;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GLBudgetEntry.\u003C\u003Ec__DisplayClass111_1 displayClass1111 = new GLBudgetEntry.\u003C\u003Ec__DisplayClass111_1();
        this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.ConvertBudget;
        // ISSUE: reference to a compiler-generated field
        displayClass1111.process = this.PrepareGraphForLongOperation();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1111, __methodptr(\u003CmanageOK\u003Eb__1)));
        break;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable preload(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GLBudgetEntry.\u003C\u003Ec__DisplayClass112_0 displayClass1120 = new GLBudgetEntry.\u003C\u003Ec__DisplayClass112_0();
    bool flag = false;
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID.HasValue)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.ledgerID>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.LedgerID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear == null)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.finYear>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.FinYear, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (!((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent.HasValue)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<BudgetPreloadFilter.changePercent>((object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      flag = true;
    }
    if (flag)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    displayClass1120.process = this.PrepareGraphForLongOperation();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1120, __methodptr(\u003Cpreload\u003Eb__0)));
    return adapter.Get();
  }

  protected GLBudgetEntry PrepareGraphForLongOperation()
  {
    if (((PXGraph) this).IsDirty)
      ((PXGraph) this).Persist();
    this.HoldNotchanged((PXCache) GraphHelper.Caches<GLBudget>((PXGraph) this));
    this.HoldNotchanged((PXCache) GraphHelper.Caches<GLBudgetLine>((PXGraph) this));
    this.HoldNotchanged((PXCache) GraphHelper.Caches<GLBudgetLineDetail>((PXGraph) this));
    GLBudgetEntry glBudgetEntry = GraphHelper.Clone<GLBudgetEntry>(this);
    ((PXCache) GraphHelper.Caches<SelectedGroup>((PXGraph) glBudgetEntry)).RestoreCopy((object) glBudgetEntry.CurrentSelected, (object) this.CurrentSelected);
    return glBudgetEntry;
  }

  private void PreloadBudgetTree()
  {
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree;
    foreach (PXResult<PX.SM.Neighbour> pxResult in ((PXSelectBase<PX.SM.Neighbour>) this.Neighbour).Select(Array.Empty<object>()))
    {
      PX.SM.Neighbour neighbour = PXResult<PX.SM.Neighbour>.op_Implicit(pxResult);
      if (neighbour.LeftEntityType.Contains("GLBudgetTree") || neighbour.RightEntityType.Contains("GLBudgetTree"))
      {
        neighbour.LeftEntityType = neighbour.LeftEntityType.Replace("GLBudgetTree", "GLBudgetLine");
        neighbour.RightEntityType = neighbour.RightEntityType.Replace("GLBudgetTree", "GLBudgetLine");
        ((PXSelectBase<PX.SM.Neighbour>) this.Neighbour).Update(neighbour);
      }
    }
    this.CurrentSelected.Group = new Guid?(Guid.Empty);
    PXResultset<GLBudgetTree> pxResultset = PXSelectBase<GLBudgetTree, PXSelectJoinOrderBy<GLBudgetTree, LeftJoin<Account, On<GLBudgetTree.accountID, Equal<Account.accountID>>, LeftJoin<Sub, On<GLBudgetTree.subID, Equal<Sub.subID>>>>, OrderBy<Desc<GLBudgetTree.isGroup, Asc<GLBudgetTree.sortOrder>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<GLBudgetLine> glBudgetLineList1 = new List<GLBudgetLine>(pxResultset.RowCount ?? 10);
    Dictionary<Guid, GLBudgetLine> dictionary1 = new Dictionary<Guid, GLBudgetLine>();
    List<GLBudgetLine> glBudgetLineList2 = new List<GLBudgetLine>(pxResultset.RowCount ?? 10);
    List<GLBudgetLine> glBudgetLineList3 = new List<GLBudgetLine>();
    Dictionary<Guid, HashSet<GLBudgetLine>> articlesByParentGroup = new Dictionary<Guid, HashSet<GLBudgetLine>>();
    foreach (PXResult<GLBudgetTree, Account, Sub> pxResult in pxResultset)
    {
      GLBudgetTree glBudgetTree = PXResult<GLBudgetTree, Account, Sub>.op_Implicit(pxResult);
      Account account = PXResult<GLBudgetTree, Account, Sub>.op_Implicit(pxResult);
      Sub sub = PXResult<GLBudgetTree, Account, Sub>.op_Implicit(pxResult);
      Guid? groupId = glBudgetTree.GroupID;
      Guid? nullable1 = glBudgetTree.ParentGroupID;
      if ((groupId.HasValue == nullable1.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        int? nullable2;
        int num1;
        if (glBudgetTree.IsGroup.Value)
        {
          nullable2 = glBudgetTree.AccountID;
          num1 = !nullable2.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        bool flag1 = num1 != 0;
        nullable2 = glBudgetTree.AccountID;
        int num2;
        if (nullable2.HasValue)
        {
          nullable2 = glBudgetTree.SubID;
          num2 = nullable2.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        bool flag2 = num2 != 0;
        if (flag2 | flag1)
        {
          GLBudgetLine article = new GLBudgetLine();
          article.BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID;
          article.LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
          article.FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear;
          article.GroupID = glBudgetTree.GroupID;
          article.ParentGroupID = glBudgetTree.ParentGroupID;
          article.IsGroup = glBudgetTree.IsGroup;
          article.IsPreloaded = new bool?(true);
          article.AccountID = glBudgetTree.AccountID;
          nullable2 = article.AccountID;
          if (nullable2.HasValue)
            PXSelectorAttribute.StoreCached<GLBudgetLine.accountID>(((PXGraph) this).Caches[typeof (GLBudgetLine)], (object) article, (object) account);
          article.SubID = glBudgetTree.SubID;
          nullable2 = article.SubID;
          if (nullable2.HasValue)
            PXSelectorAttribute.StoreCached<GLBudgetLine.subID>(((PXGraph) this).Caches[typeof (GLBudgetLine)], (object) article, (object) sub);
          article.TreeSortOrder = glBudgetTree.SortOrder;
          article.Description = glBudgetTree.Description;
          article.Rollup = glBudgetTree.Rollup;
          article.AccountMask = glBudgetTree.AccountMask;
          article.SubMask = glBudgetTree.SubMask;
          article.GroupMask = glBudgetTree.GroupMask;
          glBudgetLineList1.Add(article);
          if (flag1)
          {
            Dictionary<Guid, GLBudgetLine> dictionary2 = dictionary1;
            nullable1 = article.GroupID;
            Guid key = nullable1.Value;
            GLBudgetLine glBudgetLine = article;
            dictionary2.Add(key, glBudgetLine);
          }
          if (flag2)
            glBudgetLineList2.Add(article);
          this.setArticleToParentGroup(articlesByParentGroup, article);
        }
      }
    }
    foreach (GLBudgetLine glBudgetLine in glBudgetLineList1)
    {
      Guid? parentGroupId = glBudgetLine.ParentGroupID;
      Guid empty = Guid.Empty;
      if ((parentGroupId.HasValue ? (parentGroupId.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0)
      {
        Dictionary<Guid, GLBudgetLine> dictionary3 = dictionary1;
        parentGroupId = glBudgetLine.ParentGroupID;
        Guid key1 = parentGroupId.Value;
        if (!dictionary3.ContainsKey(key1))
        {
          glBudgetLine.Rollup = new bool?(true);
          glBudgetLineList3.Add(glBudgetLine);
          Dictionary<Guid, HashSet<GLBudgetLine>> dictionary4 = articlesByParentGroup;
          parentGroupId = glBudgetLine.ParentGroupID;
          Guid key2 = parentGroupId.Value;
          dictionary4.Remove(key2);
        }
      }
    }
    foreach (GLBudgetLine article in glBudgetLineList3)
    {
      int? nullable = article.AccountID;
      if (nullable.HasValue)
      {
        nullable = article.SubID;
        if (nullable.HasValue && !article.IsGroup.Value)
        {
          article.ParentGroupID = new Guid?(this.PutIntoNewInnerGroup(Guid.Empty, article, (IEnumerable<GLBudgetLine>) dictionary1.Values));
          this.setArticleToParentGroup(articlesByParentGroup, article);
        }
      }
    }
    HashSet<GLBudgetLine> glBudgetLineSet1 = (HashSet<GLBudgetLine>) null;
    Guid empty1 = Guid.Empty;
    articlesByParentGroup.TryGetValue(Guid.Empty, out glBudgetLineSet1);
    if (glBudgetLineSet1 != null)
    {
      foreach (GLBudgetLine glBudgetLine in glBudgetLineSet1)
      {
        bool? isGroup = glBudgetLine.IsGroup;
        bool flag = false;
        if (isGroup.GetValueOrDefault() == flag & isGroup.HasValue)
          glBudgetLine.GroupID = new Guid?(Guid.NewGuid());
      }
    }
    foreach (GLBudgetLine glBudgetLine1 in dictionary1.Values)
    {
      HashSet<GLBudgetLine> glBudgetLineSet2 = (HashSet<GLBudgetLine>) null;
      articlesByParentGroup.TryGetValue(glBudgetLine1.GroupID.Value, out glBudgetLineSet2);
      Guid guid = Guid.NewGuid();
      glBudgetLine1.GroupID = new Guid?(guid);
      if (glBudgetLineSet2 != null)
      {
        foreach (GLBudgetLine glBudgetLine2 in glBudgetLineSet2)
        {
          glBudgetLine2.ParentGroupID = new Guid?(guid);
          bool? isGroup = glBudgetLine2.IsGroup;
          bool flag = false;
          if (isGroup.GetValueOrDefault() == flag & isGroup.HasValue)
            glBudgetLine2.GroupID = new Guid?(Guid.NewGuid());
        }
      }
    }
    foreach (GLBudgetLine glBudgetLine in glBudgetLineList1)
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(glBudgetLine);
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.None;
  }

  protected virtual void RollbackBudget()
  {
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.RollbackBudget;
    PXResultset<GLBudgetLine> lines = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    GLBudgetEntry.GLBudgetLineDetailIdx budgetLineDetailIdx = new GLBudgetEntry.GLBudgetLineDetailIdx(PXSelectBase<GLBudgetLineDetail, PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLineDetail.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLineDetail.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    this._AllocationIndex = budgetLineDetailIdx;
    this._ArticleIndex = new GLBudgetEntry.GLBudgetLineIdx(lines);
    foreach (PXResult<GLBudgetLine> pxResult in lines)
    {
      GLBudgetLine article1 = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      Decimal? nullable1 = article1.ReleasedAmount;
      Decimal num = 0M;
      bool? nullable2;
      Decimal? nullable3;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      {
        nullable2 = article1.IsGroup;
        if (!nullable2.Value)
        {
          nullable2 = article1.IsPreloaded;
          if (!nullable2.Value)
          {
            nullable2 = article1.Rollup;
            if (!nullable2.Value)
            {
              GLBudgetLine article2 = article1;
              nullable1 = article1.Amount;
              Decimal? delta1;
              if (!nullable1.HasValue)
              {
                nullable3 = new Decimal?();
                delta1 = nullable3;
              }
              else
                delta1 = new Decimal?(-nullable1.GetValueOrDefault());
              this.RollupArticleAmount(article2, delta1);
              foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx.GetList(article1.GroupID.Value))
              {
                GLBudgetLine article3 = article1;
                int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
                nullable1 = budgetLineDetail.Amount;
                Decimal? delta2;
                if (!nullable1.HasValue)
                {
                  nullable3 = new Decimal?();
                  delta2 = nullable3;
                }
                else
                  delta2 = new Decimal?(-nullable1.GetValueOrDefault());
                this.RollupAllocation(article3, fieldNbr, delta2);
              }
              ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(article1);
            }
          }
        }
      }
      else
      {
        nullable1 = article1.ReleasedAmount;
        nullable3 = article1.AllocatedAmount;
        if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
        {
          nullable3 = article1.ReleasedAmount;
          nullable1 = article1.Amount;
          if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
          {
            nullable3 = article1.ReleasedAmount;
            nullable1 = article1.AllocatedAmount;
            if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
            {
              nullable1 = article1.ReleasedAmount;
              nullable3 = article1.Amount;
              if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
              {
                nullable2 = article1.Released;
                if (!nullable2.GetValueOrDefault())
                {
                  bool flag = true;
                  foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx.GetList(article1.GroupID.Value))
                  {
                    nullable3 = budgetLineDetail.ReleasedAmount;
                    nullable1 = budgetLineDetail.Amount;
                    if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
                      flag = false;
                  }
                  if (flag)
                  {
                    article1.Released = new bool?(true);
                    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(article1);
                    continue;
                  }
                  continue;
                }
                continue;
              }
              continue;
            }
            continue;
          }
        }
        foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx.GetList(article1.GroupID.Value))
        {
          nullable1 = budgetLineDetail.ReleasedAmount;
          nullable3 = budgetLineDetail.Amount;
          Decimal? delta = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          budgetLineDetail.Amount = budgetLineDetail.ReleasedAmount;
          ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
          this.RollupAllocation(article1, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), delta);
        }
        article1.Amount = article1.ReleasedAmount;
        article1.Released = new bool?(true);
        article1.Allocated = (Decimal[]) null;
        this.EnsureAlloc(article1);
        ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(article1);
      }
    }
    ((PXSelectBase) this.BudgetArticles).View.RequestRefresh();
  }

  private void ConvertBudget()
  {
    PXResultset<GLBudgetLine> lines = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.released, Equal<False>, And<GLBudgetLine.isGroup, Equal<False>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    this._ArticleIndex = new GLBudgetEntry.GLBudgetLineIdx(lines);
    PXSelectBase<GLBudgetLineDetail, PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLineDetail.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLineDetail.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    foreach (PXResult<GLBudgetLine> pxResult1 in lines)
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult1);
      Decimal num = 0M;
      foreach (PXResult<GLBudgetLineDetail> pxResult2 in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
      {
        (object) glBudgetLine.FinYear,
        (object) glBudgetLine.GroupID
      }))
      {
        GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult2);
        num += budgetLineDetail.Amount.Value;
      }
      glBudgetLine.AllocatedAmount = new Decimal?(num);
      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(glBudgetLine);
    }
    this.CurrentSelected.AccountMaskWildcard = "%";
    this.CurrentSelected.AccountMask = "";
    this.CurrentSelected.SubMask = "";
    this.CurrentSelected.SubMaskWildcard = "%";
    List<GLBudgetLine> glBudgetLineList = new List<GLBudgetLine>();
    List<GLBudgetLineDetail> source = new List<GLBudgetLineDetail>();
    foreach (PXResult<GLBudgetLine> pxResult3 in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult3);
      bool? isGroup = glBudgetLine.IsGroup;
      if (isGroup.Value)
      {
        isGroup = glBudgetLine.IsGroup;
        if (isGroup.Value)
        {
          int? nullable = glBudgetLine.AccountID;
          if (nullable.HasValue)
          {
            nullable = glBudgetLine.SubID;
            if (!nullable.HasValue)
              continue;
          }
          else
            continue;
        }
        else
          continue;
      }
      Guid guid = Guid.NewGuid();
      foreach (PXResult<GLBudgetLineDetail> pxResult4 in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
      {
        (object) ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear,
        (object) glBudgetLine.GroupID
      }))
      {
        GLBudgetLineDetail copy = ((PXSelectBase) this.Allocations).Cache.CreateCopy((object) PXResult<GLBudgetLineDetail>.op_Implicit(pxResult4)) as GLBudgetLineDetail;
        copy.GroupID = new Guid?(guid);
        source.Add(copy);
      }
      GLBudgetLine copy1 = ((PXSelectBase) this.BudgetArticles).Cache.CreateCopy((object) glBudgetLine) as GLBudgetLine;
      copy1.GroupID = new Guid?(guid);
      copy1.NoteID = new Guid?();
      glBudgetLineList.Add(copy1);
    }
    List<GLBudgetTree> list1 = GraphHelper.RowCast<GLBudgetTree>((IEnumerable) PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<GLBudgetTree>();
    foreach (GLBudgetLine glBudgetLine in glBudgetLineList)
    {
      foreach (GLBudgetTree glBudgetTree in list1)
      {
        if (glBudgetTree.IsGroup.Value)
        {
          int? nullable = glBudgetTree.AccountID;
          if (nullable.HasValue)
          {
            nullable = glBudgetTree.SubID;
            if (nullable.HasValue)
            {
              GLBudgetEntry.AccountIndex accountIndex1 = this.accountIndex;
              nullable = glBudgetLine.AccountID;
              int id1 = nullable.Value;
              string cd1 = accountIndex1.GetCD(id1);
              GLBudgetEntry.AccountIndex accountIndex2 = this.accountIndex;
              nullable = glBudgetTree.AccountID;
              int id2 = nullable.Value;
              string cd2 = accountIndex2.GetCD(id2);
              GLBudgetEntry.SubIndex subIndex1 = this.subIndex;
              nullable = glBudgetLine.SubID;
              int id3 = nullable.Value;
              string cd3 = subIndex1.GetCD(id3);
              GLBudgetEntry.SubIndex subIndex2 = this.subIndex;
              nullable = glBudgetTree.SubID;
              int id4 = nullable.Value;
              string cd4 = subIndex2.GetCD(id4);
              if (this.MatchMask(cd1, cd2) && this.MatchMask(cd3, cd4))
                throw new PXException("One or more existing Budget articles are conflicting with the current Budget Configuration. First conflicting line: {0} - {1}. Budget cannot be converted.", new object[2]
                {
                  (object) cd1,
                  (object) cd3
                });
            }
          }
        }
      }
    }
    using (new SuppressDeletingDialogBoxScope())
    {
      foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
        ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(PXResult<GLBudgetLine>.op_Implicit(pxResult));
    }
    this.PreloadBudgetTree();
    List<GLBudgetLine> list2 = GraphHelper.RowCast<GLBudgetLine>((IEnumerable) PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<GLBudgetLine>();
    foreach (GLBudgetLine newArticle in glBudgetLineList)
    {
      bool flag = false;
      foreach (GLBudgetLine parentArticle in list2)
      {
        if (parentArticle.AccountMask != null && (parentArticle.AccountMask != string.Empty || parentArticle.SubMask != string.Empty) && this.MatchMask(this.accountIndex.GetCD(newArticle.AccountID.Value), parentArticle.AccountMask ?? string.Empty) && this.MatchMask(this.subIndex.GetCD(newArticle.SubID.Value), parentArticle.SubMask ?? string.Empty))
        {
          flag = true;
          if (parentArticle.IsGroup.Value)
          {
            newArticle.ParentGroupID = parentArticle.GroupID;
            newArticle.GroupMask = parentArticle.GroupMask;
            newArticle.ParentGroupID = this.PutIntoInnerGroup(newArticle, parentArticle).GroupID;
            if (parentArticle.AccountID.HasValue && parentArticle.SubID.HasValue)
              newArticle.Rollup = new bool?(true);
          }
        }
      }
      if (!flag)
        newArticle.ParentGroupID = new Guid?(Guid.Empty);
    }
    foreach (GLBudgetLine glBudgetLine1 in glBudgetLineList)
    {
      GLBudgetLine oldArticle = glBudgetLine1;
      GLBudgetLine article = this.LocateInBudgetArticlesCache(list2, oldArticle, true);
      bool? nullable1 = oldArticle.IsGroup;
      if (!nullable1.Value && article == null)
      {
        foreach (GLBudgetLineDetail budgetLineDetail in source.Where<GLBudgetLineDetail>((Func<GLBudgetLineDetail, bool>) (x =>
        {
          Guid? groupId1 = x.GroupID;
          Guid? groupId2 = oldArticle.GroupID;
          if (groupId1.HasValue != groupId2.HasValue)
            return false;
          return !groupId1.HasValue || groupId1.GetValueOrDefault() == groupId2.GetValueOrDefault();
        })))
        {
          ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
          this.RollupAllocation(oldArticle, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
        }
        ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(oldArticle);
      }
      else
      {
        nullable1 = oldArticle.IsGroup;
        if (!nullable1.Value && article != null)
        {
          foreach (GLBudgetLineDetail budgetLineDetail in source.Where<GLBudgetLineDetail>((Func<GLBudgetLineDetail, bool>) (x =>
          {
            Guid? groupId3 = x.GroupID;
            Guid? groupId4 = oldArticle.GroupID;
            if (groupId3.HasValue != groupId4.HasValue)
              return false;
            return !groupId3.HasValue || groupId3.GetValueOrDefault() == groupId4.GetValueOrDefault();
          })))
          {
            budgetLineDetail.GroupID = article.GroupID;
            ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
            this.RollupAllocation(article, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
          }
          article.Amount = oldArticle.Amount;
          article.Released = oldArticle.Released;
          article.ReleasedAmount = oldArticle.ReleasedAmount;
          GLBudgetLine glBudgetLine2 = article;
          nullable1 = article.WasReleased;
          int num;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = oldArticle.WasReleased;
            num = nullable1.GetValueOrDefault() ? 1 : 0;
          }
          else
            num = 1;
          bool? nullable2 = new bool?(num != 0);
          glBudgetLine2.WasReleased = nullable2;
          ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(article);
        }
      }
    }
    foreach (GLBudgetLine line in glBudgetLineList)
    {
      GLBudgetLine glBudgetLine3 = this.LocateInBudgetArticlesCache(list2, line, true);
      bool? nullable3 = line.IsGroup;
      if (nullable3.Value && glBudgetLine3 != null)
      {
        glBudgetLine3.Released = line.Released;
        GLBudgetLine glBudgetLine4 = glBudgetLine3;
        nullable3 = glBudgetLine3.WasReleased;
        int num;
        if (!nullable3.GetValueOrDefault())
        {
          nullable3 = line.WasReleased;
          num = nullable3.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 1;
        bool? nullable4 = new bool?(num != 0);
        glBudgetLine4.WasReleased = nullable4;
        ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(glBudgetLine3);
      }
    }
  }

  protected virtual void DoPreload()
  {
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.Preload;
    Account account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<BudgetPreloadFilter.fromAccount>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    Account account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<BudgetPreloadFilter.toAccount>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (account1 == null)
      account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelectOrderBy<Account, OrderBy<Asc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (account2 == null)
      account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelectOrderBy<Account, OrderBy<Desc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    Guid? nullable3 = new Guid?();
    List<GLBudgetLine> glBudgetLineList = new List<GLBudgetLine>();
    List<GLBudgetLineDetail> allocations1 = new List<GLBudgetLineDetail>();
    foreach (PXResult<GLHistory, Branch, OrganizationFinPeriod, Sub, Account> pxResult in PXSelectBase<GLHistory, PXSelectJoin<GLHistory, InnerJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.finPeriodID, Equal<GLHistory.finPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<Branch.organizationID>>>, InnerJoin<Sub, On<Sub.subID, Equal<GLHistory.subID>>, InnerJoin<Account, On<Account.accountID, Equal<GLHistory.accountID>>>>>>, Where<GLHistory.ledgerID, Equal<Current<BudgetPreloadFilter.ledgerID>>, And<GLHistory.branchID, Equal<Current<BudgetPreloadFilter.branchID>>, And<OrganizationFinPeriod.finYear, Equal<Current<BudgetPreloadFilter.finYear>>, And<Account.accountCD, GreaterEqual<Required<Account.accountCD>>, And<Account.accountCD, LessEqual<Required<Account.accountCD>>, And<Account.accountCD, Like<Required<Account.accountCD>>, And<Sub.subCD, Like<Current<BudgetPreloadFilter.subCDWildcard>>, And<GLHistory.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>>>>>>>, OrderBy<Asc<Account.accountID>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) account1.AccountCD,
      (object) account2.AccountCD,
      (object) SubCDUtils.CreateSubCDWildcard(((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter != null ? ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.AccountIDFilter : string.Empty, "ACCOUNT")
    }))
    {
      GLHistory glHistory = PXResult<GLHistory, Branch, OrganizationFinPeriod, Sub, Account>.op_Implicit(pxResult);
      Account account3 = PXResult<GLHistory, Branch, OrganizationFinPeriod, Sub, Account>.op_Implicit(pxResult);
      OrganizationFinPeriod organizationFinPeriod = PXResult<GLHistory, Branch, OrganizationFinPeriod, Sub, Account>.op_Implicit(pxResult);
      if (organizationFinPeriod.PeriodNbr != null && int.Parse(organizationFinPeriod.PeriodNbr) <= this._PeriodsInCurrentYear)
      {
        int? nullable4 = glHistory.AccountID;
        int? nullable5 = nullable1;
        if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
        {
          nullable5 = glHistory.SubID;
          nullable4 = nullable2;
          if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
            goto label_10;
        }
        GLBudgetLine glBudgetLine = new GLBudgetLine();
        glBudgetLine.BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID;
        glBudgetLine.LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
        glBudgetLine.FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear;
        glBudgetLine.AccountID = glHistory.AccountID;
        glBudgetLine.SubID = glHistory.SubID;
        glBudgetLine.Released = new bool?(false);
        glBudgetLine.WasReleased = new bool?(false);
        glBudgetLine.ParentGroupID = new Guid?(this.CurrentSelected.Group ?? Guid.Empty);
        glBudgetLine.GroupID = new Guid?(Guid.NewGuid());
        glBudgetLine.Amount = new Decimal?(0M);
        nullable1 = glHistory.AccountID;
        nullable2 = glHistory.SubID;
        nullable3 = glBudgetLine.GroupID;
        glBudgetLineList.Add(glBudgetLine);
label_10:
        GLBudgetLineDetail budgetLineDetail1 = new GLBudgetLineDetail();
        budgetLineDetail1.GroupID = nullable3;
        budgetLineDetail1.BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID;
        budgetLineDetail1.LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
        budgetLineDetail1.FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear;
        budgetLineDetail1.AccountID = glHistory.AccountID;
        budgetLineDetail1.SubID = glHistory.SubID;
        budgetLineDetail1.FinPeriodID = glHistory.FinPeriodID.Replace(glHistory.FinYear, ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear);
        Decimal? nullable6;
        Decimal? nullable7;
        Decimal? nullable8;
        Decimal? nullable9;
        if (account3.Type == "A" || account3.Type == "E")
        {
          GLBudgetLineDetail budgetLineDetail2 = budgetLineDetail1;
          nullable6 = glHistory.CuryFinPtdDebit;
          nullable7 = glHistory.CuryFinPtdCredit;
          nullable8 = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
          short? changePercent = ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent;
          Decimal? nullable10;
          if (!changePercent.HasValue)
          {
            nullable7 = new Decimal?();
            nullable10 = nullable7;
          }
          else
            nullable10 = new Decimal?((Decimal) changePercent.GetValueOrDefault());
          nullable9 = nullable10;
          Decimal? nullable11;
          if (!(nullable8.HasValue & nullable9.HasValue))
          {
            nullable7 = new Decimal?();
            nullable11 = nullable7;
          }
          else
            nullable11 = new Decimal?(nullable8.GetValueOrDefault() * nullable9.GetValueOrDefault());
          Decimal? nullable12 = nullable11;
          Decimal num = (Decimal) 100;
          Decimal? nullable13;
          if (!nullable12.HasValue)
          {
            nullable9 = new Decimal?();
            nullable13 = nullable9;
          }
          else
            nullable13 = new Decimal?(nullable12.GetValueOrDefault() / num);
          nullable9 = nullable13;
          Decimal? nullable14 = new Decimal?(Math.Round(nullable9.Value, (int) (((PXSelectBase<BudgetFilter>) this.Filter).Current.Precision ?? (short) 2)));
          budgetLineDetail2.Amount = nullable14;
        }
        else
        {
          GLBudgetLineDetail budgetLineDetail3 = budgetLineDetail1;
          nullable7 = glHistory.CuryFinPtdCredit;
          nullable6 = glHistory.CuryFinPtdDebit;
          nullable9 = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
          short? changePercent = ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.ChangePercent;
          Decimal? nullable15;
          if (!changePercent.HasValue)
          {
            nullable6 = new Decimal?();
            nullable15 = nullable6;
          }
          else
            nullable15 = new Decimal?((Decimal) changePercent.GetValueOrDefault());
          nullable8 = nullable15;
          Decimal? nullable16;
          if (!(nullable9.HasValue & nullable8.HasValue))
          {
            nullable6 = new Decimal?();
            nullable16 = nullable6;
          }
          else
            nullable16 = new Decimal?(nullable9.GetValueOrDefault() * nullable8.GetValueOrDefault());
          Decimal? nullable17 = nullable16;
          Decimal num = (Decimal) 100;
          Decimal? nullable18;
          if (!nullable17.HasValue)
          {
            nullable8 = new Decimal?();
            nullable18 = nullable8;
          }
          else
            nullable18 = new Decimal?(nullable17.GetValueOrDefault() / num);
          nullable8 = nullable18;
          Decimal? nullable19 = new Decimal?(Math.Round(nullable8.Value, (int) (((PXSelectBase<BudgetFilter>) this.Filter).Current.Precision ?? (short) 2)));
          budgetLineDetail3.Amount = nullable19;
        }
        allocations1.Add(budgetLineDetail1);
      }
    }
    IEnumerable<PXResult<GLBudgetLine>> pxResults1 = ((IEnumerable<PXResult<GLBudgetLine>>) PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<Match<Current<AccessInfo.userName>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) 0
    })).AsEnumerable<PXResult<GLBudgetLine>>();
    IEnumerable<PXResult<GLBudgetLine>> pxResults2 = ((IEnumerable<PXResult<GLBudgetLine>>) PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<Not<Match<Current<AccessInfo.userName>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) 0
    })).AsEnumerable<PXResult<GLBudgetLine>>();
    PXResultset<GLBudgetLineDetail> allocations2 = PXSelectBase<GLBudgetLineDetail, PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLineDetail.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLineDetail.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    PXResultset<GLBudgetLine> pxResultset1 = new PXResultset<GLBudgetLine>();
    Guid? group = this.CurrentSelected.Group;
    Guid empty1 = Guid.Empty;
    if ((group.HasValue ? (group.GetValueOrDefault() != empty1 ? 1 : 0) : 1) != 0)
      pxResultset1.Add(PXResultset<GLBudgetLine>.op_Implicit(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Required<GLBudgetLine.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) this.CurrentSelected.Group
      })));
    pxResultset1.AddRange((IEnumerable<PXResult<GLBudgetLine>>) this.collectChildNodes(this.CurrentSelected.Group));
    PXResultset<GLBudgetLine> pxResultset2 = new PXResultset<GLBudgetLine>();
    pxResultset2.AddRange(pxResults1.Except<PXResult<GLBudgetLine>>((IEnumerable<PXResult<GLBudgetLine>>) pxResultset1, (IEqualityComparer<PXResult<GLBudgetLine>>) new GLBudgetEntry.PXResultGLBudgetLineComparer()));
    this._ArticleIndex = new GLBudgetEntry.GLBudgetLineIdx(pxResults1.Count<PXResult<GLBudgetLine>>() + pxResults2.Count<PXResult<GLBudgetLine>>(), true);
    this._ArticleIndex.Add(pxResults1);
    this._ArticleIndex.Add(pxResults2);
    GLBudgetEntry.GLBudgetLineIdx glBudgetLineIdx1 = new GLBudgetEntry.GLBudgetLineIdx(pxResults1);
    GLBudgetEntry.GLBudgetLineIdx glBudgetLineIdx2 = new GLBudgetEntry.GLBudgetLineIdx(pxResults2);
    GLBudgetEntry.GLBudgetLineIdx glBudgetLineIdx3 = new GLBudgetEntry.GLBudgetLineIdx(pxResultset1);
    GLBudgetEntry.GLBudgetLineDetailIdx budgetLineDetailIdx1 = new GLBudgetEntry.GLBudgetLineDetailIdx((IEnumerable<GLBudgetLineDetail>) allocations1);
    GLBudgetEntry.GLBudgetLineDetailIdx budgetLineDetailIdx2 = new GLBudgetEntry.GLBudgetLineDetailIdx(allocations2);
    this._AllocationIndex = budgetLineDetailIdx2;
    GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check1 = (GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine) ((line, sourceLine) =>
    {
      if (line == null)
        return (GLBudgetLine) null;
      GLBudgetLine glBudgetLine = (GLBudgetLine) null;
      PXEntryStatus status = ((PXSelectBase) this.BudgetArticles).Cache.GetStatus((object) line);
      if (status != 3 && status != 4)
      {
        glBudgetLine = line;
        if (sourceLine != null)
        {
          Guid? groupId1 = line.GroupID;
          Guid? groupId2 = sourceLine.GroupID;
          glBudgetLine = (groupId1.HasValue == groupId2.HasValue ? (groupId1.HasValue ? (groupId1.GetValueOrDefault() != groupId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 ? line : (GLBudgetLine) null;
        }
      }
      return glBudgetLine;
    });
    GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check2 = (GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine) ((line, sourceLine) =>
    {
      if (line == null)
        return (GLBudgetLine) null;
      GLBudgetLine glBudgetLine1 = (GLBudgetLine) null;
      PXEntryStatus status = ((PXSelectBase) this.BudgetArticles).Cache.GetStatus((object) line);
      if (status != 3 && status != 4)
      {
        GLBudgetLine glBudgetLine2 = line;
        if (sourceLine != null)
        {
          Guid? groupId3 = line.GroupID;
          Guid? groupId4 = sourceLine.GroupID;
          glBudgetLine2 = (groupId3.HasValue == groupId4.HasValue ? (groupId3.HasValue ? (groupId3.GetValueOrDefault() != groupId4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 ? line : (GLBudgetLine) null;
        }
        glBudgetLine1 = !line.IsGroup.GetValueOrDefault() ? line : (GLBudgetLine) null;
      }
      return glBudgetLine1;
    });
    GLBudgetEntry.GLBudgetLineDetailIdx.checkGLBudgetLineDetail check3 = (GLBudgetEntry.GLBudgetLineDetailIdx.checkGLBudgetLineDetail) ((alloc, sourceAlloc) =>
    {
      if (alloc == null)
        return (GLBudgetLineDetail) null;
      GLBudgetLineDetail budgetLineDetail4 = (GLBudgetLineDetail) null;
      PXEntryStatus status = ((PXSelectBase) this.Allocations).Cache.GetStatus((object) alloc);
      if (status != 3 && status != 4)
      {
        if (sourceAlloc != null)
        {
          Guid? groupId5 = alloc.GroupID;
          Guid? groupId6 = sourceAlloc.GroupID;
          GLBudgetLineDetail budgetLineDetail5 = (groupId5.HasValue == groupId6.HasValue ? (groupId5.HasValue ? (groupId5.GetValueOrDefault() != groupId6.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 ? alloc : (GLBudgetLineDetail) null;
        }
        budgetLineDetail4 = alloc;
      }
      return budgetLineDetail4;
    });
    foreach (GLBudgetLine newArticle in glBudgetLineList)
    {
      bool flag = false;
      foreach (PXResult<GLBudgetLine> pxResult in pxResultset2)
      {
        GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
        if (glBudgetLine.AccountMask != null && this.MatchMask(this.accountIndex.GetCD(newArticle.AccountID.Value), glBudgetLine.AccountMask ?? string.Empty) && this.MatchMask(this.subIndex.GetCD(newArticle.SubID.Value), glBudgetLine.SubMask ?? string.Empty))
          flag = true;
      }
      foreach (PXResult<GLBudgetLine> pxResult in pxResultset1)
      {
        GLBudgetLine parentArticle = PXResult<GLBudgetLine>.op_Implicit(pxResult);
        if (parentArticle.AccountMask != null && this.MatchMask(this.accountIndex.GetCD(newArticle.AccountID.Value), parentArticle.AccountMask ?? string.Empty) && this.MatchMask(this.subIndex.GetCD(newArticle.SubID.Value), parentArticle.SubMask ?? string.Empty))
        {
          flag = false;
          if (parentArticle.IsGroup.Value || !string.IsNullOrEmpty(parentArticle.AccountMask) || !string.IsNullOrEmpty(parentArticle.SubMask))
          {
            GLBudgetLine glBudgetLine = this.PutIntoInnerGroup(newArticle, parentArticle);
            int? accountId1 = glBudgetLine.AccountID;
            int? accountId2 = newArticle.AccountID;
            if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
            {
              int? subId1 = glBudgetLine.SubID;
              int? subId2 = newArticle.SubID;
              if (subId1.GetValueOrDefault() == subId2.GetValueOrDefault() & subId1.HasValue == subId2.HasValue)
                goto label_55;
            }
            if (string.IsNullOrEmpty(glBudgetLine.AccountMask) && string.IsNullOrEmpty(glBudgetLine.SubMask))
              goto label_56;
label_55:
            newArticle.ParentGroupID = glBudgetLine.GroupID;
            newArticle.GroupMask = glBudgetLine.GroupMask;
label_56:
            int? nullable20 = glBudgetLine.AccountID;
            if (nullable20.HasValue)
            {
              nullable20 = glBudgetLine.SubID;
              if (nullable20.HasValue)
                newArticle.Rollup = new bool?(true);
            }
          }
          else
          {
            newArticle.ParentGroupID = parentArticle.ParentGroupID;
            newArticle.GroupMask = parentArticle.GroupMask;
          }
        }
      }
      if (flag)
      {
        foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx1.GetList(newArticle.GroupID.Value))
        {
          budgetLineDetailIdx1.Delete(budgetLineDetail.GroupID.Value, budgetLineDetail.FinPeriodID);
          budgetLineDetail.GroupID = new Guid?(Guid.Empty);
        }
        newArticle.GroupID = new Guid?(Guid.Empty);
      }
    }
    glBudgetLineList.RemoveAll((Predicate<GLBudgetLine>) (x =>
    {
      Guid? groupId = x.GroupID;
      Guid empty2 = Guid.Empty;
      return groupId.HasValue && groupId.GetValueOrDefault() == empty2;
    }));
    allocations1.RemoveAll((Predicate<GLBudgetLineDetail>) (x =>
    {
      Guid? groupId = x.GroupID;
      Guid empty3 = Guid.Empty;
      return groupId.HasValue && groupId.GetValueOrDefault() == empty3;
    }));
    short? preloadAction = ((PXSelectBase<BudgetPreloadFilter>) this.PreloadFilter).Current.PreloadAction;
    if (preloadAction.HasValue)
    {
      switch (preloadAction.GetValueOrDefault())
      {
        case 0:
          foreach (PXResult<GLBudgetLine> pxResult in pxResultset1)
          {
            GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
            if (!glBudgetLine.IsPreloaded.Value)
            {
              Decimal? releasedAmount = glBudgetLine.ReleasedAmount;
              Decimal num = 0M;
              if (releasedAmount.GetValueOrDefault() == num & releasedAmount.HasValue)
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(glBudgetLine);
            }
          }
          using (List<GLBudgetLine>.Enumerator enumerator = glBudgetLineList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GLBudgetLine current = enumerator.Current;
              if (this._ArticleIndex.GetByKey(current, check1) == null)
              {
                foreach (GLBudgetLineDetail keySource in budgetLineDetailIdx1.GetList(current.GroupID.Value))
                {
                  if (budgetLineDetailIdx2.GetByKey(keySource, check3) == null)
                  {
                    GLBudgetLine glBudgetLine = current;
                    Decimal? amount1 = glBudgetLine.Amount;
                    Decimal? amount2 = keySource.Amount;
                    glBudgetLine.Amount = amount1.HasValue & amount2.HasValue ? new Decimal?(amount1.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(keySource);
                    this.RollupAllocation(current, int.Parse(keySource.FinPeriodID.Substring(4)), keySource.Amount);
                  }
                }
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(current);
              }
            }
            break;
          }
        case 1:
          foreach (GLBudgetLine glBudgetLine3 in glBudgetLineList)
          {
            GLBudgetLine byKey1 = glBudgetLineIdx3.GetByKey(glBudgetLine3, check2);
            bool? nullable21;
            if (byKey1 != null)
            {
              nullable21 = byKey1.IsGroup;
              if (!nullable21.Value && !byKey1.AccountMask.Contains<char>('?') && !byKey1.SubMask.Contains<char>('?'))
              {
                byKey1.Allocated = (Decimal[]) null;
                glBudgetLine3.ParentGroupID = byKey1.ParentGroupID;
                foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx2.GetList(byKey1.GroupID.Value))
                {
                  GLBudgetLine article = byKey1;
                  int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
                  Decimal? amount = budgetLineDetail.Amount;
                  Decimal? delta = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
                  this.RollupAllocation(article, fieldNbr, delta);
                  budgetLineDetail.Amount = new Decimal?(0M);
                  ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
                }
                foreach (GLBudgetLineDetail keySource in budgetLineDetailIdx1.GetList(glBudgetLine3.GroupID.Value))
                {
                  GLBudgetLineDetail byKey2 = budgetLineDetailIdx2.GetByKey(keySource, check3);
                  if (byKey2 != null)
                  {
                    byKey2.Amount = keySource.Amount;
                    GLBudgetLine glBudgetLine4 = glBudgetLine3;
                    Decimal? amount3 = glBudgetLine4.Amount;
                    Decimal? amount4 = byKey2.Amount;
                    glBudgetLine4.Amount = amount3.HasValue & amount4.HasValue ? new Decimal?(amount3.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(byKey2);
                    this.RollupAllocation(byKey1, int.Parse(keySource.FinPeriodID.Substring(4)), keySource.Amount);
                  }
                  else
                  {
                    keySource.GroupID = byKey1.GroupID;
                    GLBudgetLine glBudgetLine5 = glBudgetLine3;
                    Decimal? amount5 = glBudgetLine5.Amount;
                    Decimal? amount6 = keySource.Amount;
                    glBudgetLine5.Amount = amount5.HasValue & amount6.HasValue ? new Decimal?(amount5.GetValueOrDefault() + amount6.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(keySource);
                    this.RollupAllocation(glBudgetLine3, int.Parse(keySource.FinPeriodID.Substring(4)), keySource.Amount);
                  }
                }
                byKey1.Amount = glBudgetLine3.Amount;
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(byKey1);
                continue;
              }
            }
            nullable21 = glBudgetLine3.Rollup;
            if (nullable21.HasValue)
            {
              nullable21 = glBudgetLine3.Rollup;
              if (nullable21.Value)
              {
                GLBudgetLine articleByCurrentFilter = this.GetArticleByCurrentFilter(glBudgetLine3.ParentGroupID);
                if (articleByCurrentFilter != null)
                {
                  nullable21 = articleByCurrentFilter.Cleared;
                  if (!nullable21.HasValue)
                  {
                    articleByCurrentFilter.Amount = new Decimal?(0M);
                    articleByCurrentFilter.Cleared = new bool?(true);
                    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(articleByCurrentFilter);
                    foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx2.GetList(articleByCurrentFilter.GroupID.Value))
                    {
                      GLBudgetLine article = articleByCurrentFilter;
                      int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
                      Decimal? amount = budgetLineDetail.Amount;
                      Decimal? delta = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
                      this.RollupAllocation(article, fieldNbr, delta);
                      budgetLineDetail.Amount = new Decimal?(0M);
                      ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
                    }
                  }
                }
                foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx1.GetList(glBudgetLine3.GroupID.Value))
                {
                  GLBudgetLine glBudgetLine6 = glBudgetLine3;
                  Decimal? amount7 = glBudgetLine6.Amount;
                  Decimal? amount8 = budgetLineDetail.Amount;
                  glBudgetLine6.Amount = amount7.HasValue & amount8.HasValue ? new Decimal?(amount7.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
                  ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
                  this.RollupAllocation(glBudgetLine3, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
                }
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(glBudgetLine3);
              }
            }
          }
          IEnumerator enumerator1 = ((PXSelectBase) this.BudgetArticles).Cache.Cached.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
            {
              GLBudgetLine current = (GLBudgetLine) enumerator1.Current;
              bool? nullable22 = current.Rollup;
              if (nullable22.HasValue)
              {
                nullable22 = current.Rollup;
                if (nullable22.Value)
                  ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(current);
              }
              nullable22 = current.Cleared;
              if (nullable22.HasValue)
              {
                GLBudgetLine glBudgetLine = current;
                nullable22 = new bool?();
                bool? nullable23 = nullable22;
                glBudgetLine.Cleared = nullable23;
                current.Amount = current.AllocatedAmount;
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(current);
              }
            }
            break;
          }
          finally
          {
            if (enumerator1 is IDisposable disposable)
              disposable.Dispose();
          }
        case 2:
          foreach (GLBudgetLine glBudgetLine7 in glBudgetLineList)
          {
            GLBudgetLine byKey3 = glBudgetLineIdx3.GetByKey(glBudgetLine7, check1);
            bool? nullable24;
            if (byKey3 != null && !byKey3.AccountMask.Contains<char>('?') && !byKey3.SubMask.Contains<char>('?'))
            {
              nullable24 = byKey3.IsGroup;
              bool flag = false;
              if (nullable24.GetValueOrDefault() == flag & nullable24.HasValue)
              {
                nullable24 = byKey3.Rollup;
                if (nullable24.HasValue)
                {
                  nullable24 = byKey3.Rollup;
                  if (nullable24.Value)
                  {
                    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(byKey3);
                    goto label_167;
                  }
                }
                byKey3.Allocated = (Decimal[]) null;
                glBudgetLine7.ParentGroupID = byKey3.ParentGroupID;
                foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx2.GetList(byKey3.GroupID.Value))
                {
                  GLBudgetLine article = byKey3;
                  int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
                  Decimal? amount = budgetLineDetail.Amount;
                  Decimal? delta = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
                  this.RollupAllocation(article, fieldNbr, delta);
                  budgetLineDetail.Amount = new Decimal?(0M);
                  ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
                }
label_167:
                foreach (GLBudgetLineDetail keySource in budgetLineDetailIdx1.GetList(glBudgetLine7.GroupID.Value))
                {
                  GLBudgetLineDetail byKey4 = budgetLineDetailIdx2.GetByKey(keySource, check3);
                  if (byKey4 != null)
                  {
                    byKey4.Amount = keySource.Amount;
                    GLBudgetLine glBudgetLine8 = glBudgetLine7;
                    Decimal? amount9 = glBudgetLine8.Amount;
                    Decimal? amount10 = byKey4.Amount;
                    glBudgetLine8.Amount = amount9.HasValue & amount10.HasValue ? new Decimal?(amount9.GetValueOrDefault() + amount10.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(byKey4);
                    this.RollupAllocation(byKey3, int.Parse(keySource.FinPeriodID.Substring(4)), keySource.Amount);
                  }
                  else
                  {
                    keySource.GroupID = byKey3.GroupID;
                    GLBudgetLine glBudgetLine9 = glBudgetLine7;
                    Decimal? amount11 = glBudgetLine9.Amount;
                    Decimal? amount12 = keySource.Amount;
                    glBudgetLine9.Amount = amount11.HasValue & amount12.HasValue ? new Decimal?(amount11.GetValueOrDefault() + amount12.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(keySource);
                    this.RollupAllocation(glBudgetLine7, int.Parse(keySource.FinPeriodID.Substring(4)), keySource.Amount);
                  }
                }
                byKey3.Amount = glBudgetLine7.Amount;
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(byKey3);
              }
            }
            else
            {
              nullable24 = glBudgetLine7.Rollup;
              if (nullable24.HasValue)
              {
                nullable24 = glBudgetLine7.Rollup;
                if (nullable24.Value)
                {
                  GLBudgetLine glBudgetLine10 = this.ArticleIndex.Get(glBudgetLine7.ParentGroupID);
                  if (glBudgetLine10 != null)
                  {
                    nullable24 = glBudgetLine10.Cleared;
                    if (!nullable24.HasValue)
                    {
                      glBudgetLine10.Amount = new Decimal?(0M);
                      glBudgetLine10.Cleared = new bool?(true);
                      ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(glBudgetLine10);
                      foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx2.GetList(glBudgetLine10.GroupID.Value))
                      {
                        GLBudgetLine article = glBudgetLine10;
                        int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
                        Decimal? amount = budgetLineDetail.Amount;
                        Decimal? delta = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
                        this.RollupAllocation(article, fieldNbr, delta);
                        budgetLineDetail.Amount = new Decimal?(0M);
                        ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
                      }
                    }
                  }
                  foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx1.GetList(glBudgetLine7.GroupID.Value))
                  {
                    GLBudgetLine glBudgetLine11 = glBudgetLine7;
                    Decimal? amount13 = glBudgetLine11.Amount;
                    Decimal? amount14 = budgetLineDetail.Amount;
                    glBudgetLine11.Amount = amount13.HasValue & amount14.HasValue ? new Decimal?(amount13.GetValueOrDefault() + amount14.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
                    this.RollupAllocation(glBudgetLine7, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
                  }
                  ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(glBudgetLine7);
                  continue;
                }
              }
              if (glBudgetLineIdx2.GetByKey(glBudgetLine7, check1) == null)
              {
                foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx1.GetList(glBudgetLine7.GroupID.Value))
                {
                  GLBudgetLine glBudgetLine12 = glBudgetLine7;
                  Decimal? amount15 = glBudgetLine12.Amount;
                  Decimal? amount16 = budgetLineDetail.Amount;
                  glBudgetLine12.Amount = amount15.HasValue & amount16.HasValue ? new Decimal?(amount15.GetValueOrDefault() + amount16.GetValueOrDefault()) : new Decimal?();
                  ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
                  this.RollupAllocation(glBudgetLine7, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
                }
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(glBudgetLine7);
              }
            }
          }
          IEnumerator enumerator2 = ((PXSelectBase) this.BudgetArticles).Cache.Cached.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              GLBudgetLine current = (GLBudgetLine) enumerator2.Current;
              bool? nullable25 = current.Rollup;
              if (nullable25.HasValue)
              {
                nullable25 = current.Rollup;
                if (nullable25.Value)
                  ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Delete(current);
              }
              nullable25 = current.Cleared;
              if (nullable25.HasValue)
              {
                GLBudgetLine glBudgetLine = current;
                nullable25 = new bool?();
                bool? nullable26 = nullable25;
                glBudgetLine.Cleared = nullable26;
                current.Amount = current.AllocatedAmount;
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Update(current);
              }
            }
            break;
          }
          finally
          {
            if (enumerator2 is IDisposable disposable)
              disposable.Dispose();
          }
        case 3:
          using (List<GLBudgetLine>.Enumerator enumerator3 = glBudgetLineList.GetEnumerator())
          {
            while (enumerator3.MoveNext())
            {
              GLBudgetLine current = enumerator3.Current;
              if (this._ArticleIndex.GetByKey(current, check1) == null)
              {
                bool? rollup = current.Rollup;
                if (rollup.HasValue)
                {
                  rollup = current.Rollup;
                  if (rollup.Value)
                    continue;
                }
                foreach (GLBudgetLineDetail budgetLineDetail in budgetLineDetailIdx1.GetList(current.GroupID.Value))
                {
                  GLBudgetLine glBudgetLine = current;
                  Decimal? amount17 = glBudgetLine.Amount;
                  Decimal? amount18 = budgetLineDetail.Amount;
                  glBudgetLine.Amount = amount17.HasValue & amount18.HasValue ? new Decimal?(amount17.GetValueOrDefault() + amount18.GetValueOrDefault()) : new Decimal?();
                  ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Insert(budgetLineDetail);
                  this.RollupAllocation(current, int.Parse(budgetLineDetail.FinPeriodID.Substring(4)), budgetLineDetail.Amount);
                }
                ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Insert(current);
              }
            }
            break;
          }
      }
    }
    this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.None;
  }

  public virtual void Persist()
  {
    this.SetCurrentBudget();
    if (((PXSelectBase<GLBudget>) this.Budget).Current != null && ((PXGraph) this).IsDirty)
      GraphHelper.MarkUpdated(((PXSelectBase) this.Budget).Cache, (object) ((PXSelectBase<GLBudget>) this.Budget).Current);
    switch (this._CurrentAction)
    {
      case GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree:
      case GLBudgetEntry.GLBudgetEntryActionType.Preload:
      case GLBudgetEntry.GLBudgetEntryActionType.ConvertBudget:
      case GLBudgetEntry.GLBudgetEntryActionType.RollbackBudget:
        this.RemoveDuplicates();
        break;
      default:
        this.CheckBudgetLinesForDuplicates();
        break;
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void AllocationFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    int fieldNbr)
  {
    GLBudgetLine row = (GLBudgetLine) e.Row;
    object returnState = e.ReturnState;
    short? precision = (short?) ((PXSelectBase<BudgetFilter>) this.Filter).Current?.Precision;
    int? nullable1 = precision.HasValue ? new int?((int) precision.GetValueOrDefault()) : new int?();
    string str = this._PrefixPeriodField + fieldNbr.ToString();
    bool? nullable2 = new bool?(false);
    int? nullable3 = new int?(0);
    Decimal? nullable4 = new Decimal?(Decimal.MinValue);
    Decimal? nullable5 = new Decimal?(Decimal.MaxValue);
    PXDecimalState instance = (PXDecimalState) PXDecimalState.CreateInstance(returnState, nullable1, str, nullable2, nullable3, nullable4, nullable5);
    e.ReturnState = (object) instance;
    ((PXFieldState) instance).DisplayName = string.Format(PXMessages.LocalizeNoPrefix("Period {0:00}"), (object) fieldNbr);
    ((PXFieldState) instance).Enabled = true;
    ((PXFieldState) instance).Visible = true;
    ((PXFieldState) instance).Visibility = (PXUIVisibility) 3;
    if (row != null)
    {
      bool? nullable6 = row.IsGroup;
      if (nullable6.HasValue)
      {
        nullable6 = row.IsGroup;
        if (nullable6.Value)
          ((PXFieldState) instance).Enabled = false;
      }
      nullable6 = row.Comparison;
      Decimal num1;
      if (nullable6.GetValueOrDefault() && row.Compared != null && fieldNbr <= row.Compared.Length)
      {
        ((PXFieldState) instance).Enabled = false;
        num1 = row.Compared[fieldNbr - 1];
      }
      else
      {
        this.EnsureAlloc(row);
        num1 = row.Allocated[fieldNbr - 1];
      }
      PXDecimalState pxDecimalState = instance;
      int num2;
      if (((PXFieldState) instance).Enabled)
      {
        int? nullable7 = row.AccountID;
        if (nullable7.HasValue)
        {
          GLBudgetEntry.AccountIndex accountIndex = this.accountIndex;
          nullable7 = row.AccountID;
          int id1 = nullable7.Value;
          if (accountIndex.IsActive(id1))
          {
            nullable7 = row.SubID;
            if (nullable7.HasValue)
            {
              GLBudgetEntry.SubIndex subIndex = this.subIndex;
              nullable7 = row.SubID;
              int id2 = nullable7.Value;
              num2 = subIndex.IsActive(id2) ? 1 : 0;
              goto label_13;
            }
          }
        }
      }
      num2 = 0;
label_13:
      ((PXFieldState) pxDecimalState).Enabled = num2 != 0;
      e.ReturnValue = (object) num1;
    }
    if (fieldNbr <= this._PeriodsInCurrentYear)
      return;
    ((PXFieldState) instance).Visible = false;
  }

  protected virtual void AllocationFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    int fieldNbr)
  {
    GLBudgetLine row = e.Row as GLBudgetLine;
    object obj = e.NewValue;
    Decimal result = 0M;
    if (e.NewValue is string)
    {
      if (Decimal.TryParse((string) e.NewValue, out result))
        obj = (object) result;
      else if (!Decimal.TryParse(((string) e.NewValue).Replace(',', '.'), out result))
      {
        PXCache cache = ((PXSelectBase) this.Allocations).Cache;
        GLBudgetLineDetail budgetLineDetail = new GLBudgetLineDetail();
        budgetLineDetail.GroupID = row.GroupID;
        budgetLineDetail.LedgerID = row.LedgerID;
        budgetLineDetail.BranchID = row.BranchID;
        budgetLineDetail.FinYear = row.FinYear;
        ref object local = ref obj;
        cache.RaiseFieldUpdating<GLBudgetLineDetail.amount>((object) budgetLineDetail, ref local);
      }
    }
    if (((PXGraph) this).IsImport)
    {
      if (!row.GroupID.HasValue)
        row.GroupID = new Guid?(Guid.NewGuid());
      if (this.IsNullOrEmpty(row.ParentGroupID))
        row.ParentGroupID = this.CurrentSelected.Group;
    }
    this.UpdateAlloc((obj as Decimal?).GetValueOrDefault(), row, fieldNbr);
    row.Released = new bool?(false);
  }

  protected virtual void BudgetDistributeFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    BudgetFilter current1 = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
    GLBudgetLine current2 = ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current;
    if (current2 == null)
      return;
    Dictionary<string, string> valueLabelDic = new BudgetDistributeFilter.method.ListAttribute().ValueLabelDic;
    if (current1 != null)
    {
      int? nullable = current1.CompareToBranchID;
      if (nullable.HasValue)
      {
        nullable = current1.CompareToLedgerId;
        if (nullable.HasValue && !string.IsNullOrEmpty(current1.CompareToFinYear) && current2.Compared != null && !(((IEnumerable<Decimal>) current2.Compared).Sum() == 0M))
          goto label_6;
      }
    }
    valueLabelDic.Remove("C");
label_6:
    GLBudgetLine prevArticle = this.GetPrevArticle(current2);
    if (prevArticle != null)
    {
      Decimal? allocatedAmount = prevArticle.AllocatedAmount;
      Decimal num = 0M;
      if (!(allocatedAmount.GetValueOrDefault() == num & allocatedAmount.HasValue))
        goto label_9;
    }
    valueLabelDic.Remove("P");
label_9:
    PXStringListAttribute.SetList<BudgetDistributeFilter.method>(sender, e.Row, valueLabelDic.Keys.ToArray<string>(), valueLabelDic.Values.ToArray<string>());
    BudgetDistributeFilter row = e.Row as BudgetDistributeFilter;
    PXCache pxCache = sender;
    BudgetDistributeFilter distributeFilter = row;
    bool? applyToAll;
    int num1;
    if (row.ApplyToAll.HasValue)
    {
      applyToAll = row.ApplyToAll;
      num1 = applyToAll.Value ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<BudgetDistributeFilter.applyToSubGroups>(pxCache, (object) distributeFilter, num1 != 0);
    applyToAll = row.ApplyToAll;
    if (applyToAll.HasValue)
    {
      applyToAll = row.ApplyToAll;
      if (applyToAll.Value)
        return;
    }
    row.ApplyToSubGroups = new bool?(false);
  }

  protected virtual void BudgetFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    BudgetFilter row = (BudgetFilter) e.Row;
    PXAction<BudgetFilter> showPreload = this.ShowPreload;
    int? nullable1;
    int num1;
    if (row != null)
    {
      nullable1 = row.BranchID;
      if (nullable1.HasValue)
      {
        nullable1 = row.LedgerID;
        if (nullable1.HasValue)
        {
          num1 = row.FinYear != null ? 1 : 0;
          goto label_5;
        }
      }
    }
    num1 = 0;
label_5:
    ((PXAction) showPreload).SetEnabled(num1 != 0);
    PXAction<BudgetFilter> showManage = this.ShowManage;
    int num2;
    if (row != null)
    {
      nullable1 = row.BranchID;
      if (nullable1.HasValue)
      {
        nullable1 = row.LedgerID;
        if (nullable1.HasValue)
        {
          num2 = row.FinYear != null ? 1 : 0;
          goto label_10;
        }
      }
    }
    num2 = 0;
label_10:
    ((PXAction) showManage).SetEnabled(num2 != 0);
    PXCache cache = ((PXSelectBase) this.BudgetArticles).Cache;
    int num3;
    if (row != null)
    {
      nullable1 = row.BranchID;
      if (nullable1.HasValue)
      {
        nullable1 = row.LedgerID;
        if (nullable1.HasValue)
        {
          num3 = row.FinYear != null ? 1 : 0;
          goto label_15;
        }
      }
    }
    num3 = 0;
label_15:
    cache.AllowInsert = num3 != 0;
    if (row != null && !row.Precision.HasValue)
    {
      PX.Objects.CM.Currency currency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelectJoin<PX.Objects.CM.Currency, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<BudgetFilter.ledgerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      row.Precision = currency != null ? currency.DecimalPlaces : new short?((short) 2);
    }
    PXUIFieldAttribute.SetVisible(sender, typeof (BudgetFilter.showTree).Name, this.IsBudgetTree());
    if (row != null)
    {
      if (!this.IsBudgetTree())
        row.ShowTree = new bool?(false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.BudgetArticles).Cache, typeof (GLBudgetLine.isGroup).Name, row.ShowTree.GetValueOrDefault());
    }
    nullable1 = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
    if (nullable1.HasValue)
      ((PXSelectBase<BudgetFilter>) this.Filter).Current.BaseCuryID = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Current<BudgetFilter.ledgerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).BaseCuryID;
    nullable1 = row.LedgerID;
    if (nullable1.HasValue)
    {
      nullable1 = row.CompareToLedgerId;
      if (nullable1.HasValue)
      {
        if (row.BaseCuryID != PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<BudgetFilter.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.CompareToLedgerId
        })).BaseCuryID)
        {
          if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
          {
            BudgetFilter current1 = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
            nullable1 = new int?();
            int? nullable2 = nullable1;
            current1.CompareToBranchID = nullable2;
            BudgetFilter current2 = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
            nullable1 = new int?();
            int? nullable3 = nullable1;
            current2.CompareToLedgerId = nullable3;
            return;
          }
          PXUIFieldAttribute.SetWarning<BudgetFilter.compareToLedgerID>(sender, (object) row, "The ledger currency is different from the budget currency.");
          return;
        }
      }
    }
    PXUIFieldAttribute.SetWarning<BudgetFilter.compareToLedgerID>(sender, (object) row, (string) null);
  }

  protected virtual void BudgetFilter_SubIDFilter_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void BudgetFilter_FinYear_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    BudgetFilter row = (BudgetFilter) e.Row;
    if (row == null)
      return;
    this._PeriodsInCurrentYear = (int) ((short?) PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelect<OrganizationFinYear, Where<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) row.FinYear,
      (object) PXAccess.GetParentOrganizationID(row.BranchID)
    }))?.FinPeriods).GetValueOrDefault();
  }

  protected virtual void BudgetFilter_ShowTree_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    e.NewValue = (object) false;
  }

  protected virtual void BudgetFilter_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    BudgetFilter row = (BudgetFilter) e.Row;
    BudgetFilter newRow = (BudgetFilter) e.NewRow;
    int? branchId1 = newRow.BranchID;
    int? branchId2 = row.BranchID;
    if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
    {
      int? ledgerId1 = newRow.LedgerID;
      int? ledgerId2 = row.LedgerID;
      if (ledgerId1.GetValueOrDefault() == ledgerId2.GetValueOrDefault() & ledgerId1.HasValue == ledgerId2.HasValue && !(newRow.FinYear != row.FinYear))
        return;
    }
    if (!((PXSelectBase) this.BudgetArticles).Cache.IsDirty)
      return;
    newRow.BranchID = row.BranchID;
    newRow.LedgerID = row.LedgerID;
    newRow.FinYear = row.FinYear;
    ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Pending Changes", "The budget has pending changes. Review the budget and then save or discard your changes before selecting another budget.", (MessageButtons) 0);
  }

  protected virtual void BudgetFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    BudgetFilter row = (BudgetFilter) e.Row;
    BudgetFilter oldRow = (BudgetFilter) e.OldRow;
    if (row == null)
      return;
    if (((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID.HasValue && ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID.HasValue && ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear != null && this.IsBudgetTree() && !((IQueryable<PXResult<GLBudgetLine>>) PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) null)).Any<PXResult<GLBudgetLine>>())
    {
      WebDialogResult webDialogResult = ((PXSelectBase<BudgetFilter>) this.Filter).Ask("Preload from Budget Configuration", "Do you want to create budget for this year? Budget tree will be preloaded from budget configuration.", (MessageButtons) 4);
      ((PXSelectBase<BudgetFilter>) this.Filter).ClearDialog();
      if (webDialogResult == 6)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GLBudgetEntry.\u003C\u003Ec__DisplayClass127_0 displayClass1270 = new GLBudgetEntry.\u003C\u003Ec__DisplayClass127_0();
        this._CurrentAction = GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree;
        // ISSUE: reference to a compiler-generated field
        displayClass1270.process = this.PrepareGraphForLongOperation();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1270, __methodptr(\u003CBudgetFilter_RowUpdated\u003Eb__0)));
      }
      else
        ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear = (string) null;
    }
    int? nullable1;
    if (((PXGraph) this).IsImport)
    {
      nullable1 = row.BranchID;
      if (nullable1.HasValue && row.FinYear != null)
      {
        nullable1 = row.LedgerID;
        if (nullable1.HasValue)
        {
          nullable1 = row.BranchID;
          int? branchId = oldRow.BranchID;
          if (nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue && !(row.FinYear != oldRow.FinYear))
          {
            int? ledgerId = row.LedgerID;
            nullable1 = oldRow.LedgerID;
            if (ledgerId.GetValueOrDefault() == nullable1.GetValueOrDefault() & ledgerId.HasValue == nullable1.HasValue)
              goto label_12;
          }
          this.IndexesIsPrepared = false;
          this.PrepareIndexes();
          this._BudgetArticlesIndex = (GLBudgetEntry.GLBudgetLineIdx) null;
        }
      }
    }
label_12:
    nullable1 = oldRow.CompareToBranchID;
    int? nullable2 = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToBranchID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = oldRow.CompareToLedgerId;
      nullable1 = ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToLedgerId;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(oldRow.CompareToFinYear != ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear) && ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear == null)
        return;
    }
    this.PopulateComparison(((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToBranchID, ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToLedgerId, ((PXSelectBase<BudgetFilter>) this.Filter).Current.CompareToFinYear);
  }

  protected virtual bool IsBudgetTree()
  {
    return ((IQueryable<PXResult<GLBudgetTree>>) PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) null)).Any<PXResult<GLBudgetTree>>();
  }

  protected virtual void BudgetFilter_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((PXGraph) this).IsImport)
      return;
    sender.SetDefaultExt<BudgetFilter.ledgerID>(e.Row);
  }

  protected virtual void BudgetPreloadFilter_AccountIDFilter_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    PXStringState stateExt = (PXStringState) sender.GetStateExt((object) null, typeof (BudgetPreloadFilter.fromAccount).Name);
    PXDBStringAttribute.SetInputMask(sender, typeof (BudgetPreloadFilter.accountIDFilter).Name, stateExt.InputMask.Replace('#', 'C'));
  }

  protected virtual void BudgetPreloadFilter_AccountIDFilter_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    PXStringState stateExt = (PXStringState) sender.GetStateExt((object) null, typeof (BudgetPreloadFilter.fromAccount).Name);
    e.NewValue = (object) ((string) e.NewValue).PadRight(stateExt.InputMask.Length - 1, '?').Replace(' ', '?');
  }

  protected virtual void BudgetPreloadFilter_SubIDFilter_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void BudgetPreloadFilter_SubIDFilter_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PXStringState stateExt = (PXStringState) sender.GetStateExt((object) null, typeof (BudgetPreloadFilter.subIDFilter).Name);
    if (e.NewValue != null)
    {
      e.NewValue = (object) ((string) e.NewValue).PadRight(stateExt.InputMask.Length - 1, '?').Replace(' ', '?');
    }
    else
    {
      e.NewValue = (object) string.Empty;
      e.NewValue = (object) ((string) e.NewValue).PadRight(stateExt.InputMask.Length - 1, '?').Replace(' ', '?');
    }
  }

  protected virtual void BudgetPreloadFilter_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetValueExt<BudgetPreloadFilter.ledgerID>(e.Row, (object) null);
  }

  protected virtual void BudgetPreloadFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    BudgetPreloadFilter row = (BudgetPreloadFilter) e.Row;
    if (row == null)
      return;
    int? ledgerId = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID;
    if (ledgerId.HasValue)
    {
      ledgerId = row.LedgerID;
      if (ledgerId.HasValue)
      {
        if (PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Current<BudgetFilter.ledgerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).BaseCuryID != PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<BudgetPreloadFilter.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.LedgerID
        })).BaseCuryID)
        {
          PXUIFieldAttribute.SetWarning<BudgetPreloadFilter.ledgerID>(cache, (object) row, "The ledger currency is different from the budget currency.");
          return;
        }
      }
    }
    PXUIFieldAttribute.SetWarning<BudgetPreloadFilter.ledgerID>(cache, (object) row, (string) null);
  }

  protected virtual GLBudget SetCurrentBudget()
  {
    BudgetFilter current = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
    int? nullable;
    int num1;
    if (current == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = current.BranchID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 == 0)
    {
      int num2;
      if (current == null)
      {
        num2 = 1;
      }
      else
      {
        nullable = current.LedgerID;
        num2 = !nullable.HasValue ? 1 : 0;
      }
      if (num2 == 0 && current != null && current.FinYear != null)
      {
        ((PXSelectBase<GLBudget>) this.Budget).Current = ((PXSelectBase<GLBudget>) this.Budget).SelectSingle(Array.Empty<object>());
        bool flag = ((PXSelectBase<GLBudgetLine>) new PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>()) != null;
        if (((PXSelectBase<GLBudget>) this.Budget).Current == null)
        {
          if (flag)
            ((PXSelectBase<GLBudget>) this.Budget).Current = ((PXSelectBase<GLBudget>) this.Budget).Insert(new GLBudget()
            {
              BranchID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.BranchID,
              LedgerID = ((PXSelectBase<BudgetFilter>) this.Filter).Current.LedgerID,
              FinYear = ((PXSelectBase<BudgetFilter>) this.Filter).Current.FinYear
            });
        }
        else if (!flag)
          ((PXSelectBase<GLBudget>) this.Budget).Delete(((PXSelectBase<GLBudget>) this.Budget).Current);
        return ((PXSelectBase<GLBudget>) this.Budget).Current;
      }
    }
    return (GLBudget) null;
  }

  private bool CheckReleasedInGroup(GLBudgetLine article)
  {
    if (!article.IsGroup.GetValueOrDefault())
      return false;
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.parentGroupID, Equal<Required<GLBudgetLine.groupID>>, And<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) article.GroupID
    }))
    {
      GLBudgetLine article1 = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      bool? nullable = article1.IsGroup;
      if (nullable.GetValueOrDefault() && this.CheckReleasedInGroup(article1))
        return true;
      nullable = article1.IsGroup;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = article1.Released;
        if (nullable.GetValueOrDefault())
          return true;
      }
    }
    return false;
  }

  protected virtual void GLBudgetLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree)
      return;
    GLBudgetLine row = (GLBudgetLine) e.Row;
    if (row == null)
      return;
    ((PXAction) this.Distribute).SetEnabled(!row.Comparison.HasValue);
    bool? nullable1 = row.WasReleased;
    bool flag1 = !nullable1.GetValueOrDefault();
    nullable1 = row.IsGroup;
    bool flag2 = !nullable1.GetValueOrDefault();
    nullable1 = row.IsPreloaded;
    bool flag3 = !nullable1.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<GLBudgetLine.accountID>(sender, (object) row, flag1 & flag2 & flag3);
    PXUIFieldAttribute.SetEnabled<GLBudgetLine.subID>(sender, (object) row, flag1 & flag2 & flag3);
    PXUIFieldAttribute.SetEnabled<GLBudgetLine.description>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLBudgetLine.amount>(sender, (object) row, flag2);
    PXCache pxCache1 = sender;
    GLBudgetLine glBudgetLine1 = row;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> allocatedAmount1 = (ValueType) row.AllocatedAmount;
    Decimal? amount = row.Amount;
    Decimal? allocatedAmount2 = row.AllocatedAmount;
    PXSetPropertyException propertyException = !(amount.GetValueOrDefault() == allocatedAmount2.GetValueOrDefault() & amount.HasValue == allocatedAmount2.HasValue) ? new PXSetPropertyException("Distributed Amount is not equal to Amount. The budget article cannot be released.", (PXErrorLevel) 3) : (PXSetPropertyException) null;
    pxCache1.RaiseExceptionHandling<GLBudgetLine.allocatedAmount>((object) glBudgetLine1, (object) allocatedAmount1, (Exception) propertyException);
    int? nullable2 = row.AccountID;
    if (!nullable2.HasValue)
      return;
    nullable2 = row.SubID;
    if (!nullable2.HasValue)
      return;
    PXCache pxCache2 = sender;
    GLBudgetLine glBudgetLine2 = row;
    GLBudgetEntry.AccountIndex accountIndex = this.accountIndex;
    nullable2 = row.AccountID;
    int id1 = nullable2.Value;
    int num;
    if (accountIndex.IsActive(id1))
    {
      GLBudgetEntry.SubIndex subIndex = this.subIndex;
      nullable2 = row.SubID;
      int id2 = nullable2.Value;
      num = !subIndex.IsActive(id2) ? 1 : 0;
    }
    else
      num = 1;
    PXUIFieldAttribute.SetReadOnly(pxCache2, (object) glBudgetLine2, num != 0);
  }

  protected virtual void GLBudgetLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLBudgetLine row = (GLBudgetLine) e.Row;
    if (row == null)
      return;
    PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.groupID, Equal<Required<GLBudgetLineDetail.groupID>>>> pxSelect = new PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.groupID, Equal<Required<GLBudgetLineDetail.groupID>>>>((PXGraph) this);
    Guid? parentGroupId = row.ParentGroupID;
    Guid empty = Guid.Empty;
    if ((parentGroupId.HasValue ? (parentGroupId.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0)
    {
      GLBudgetLine oldRow = (GLBudgetLine) e.OldRow;
      GLBudgetLine article = row;
      Decimal? amount1 = row.Amount;
      Decimal? amount2 = oldRow.Amount;
      Decimal? delta = amount1.HasValue & amount2.HasValue ? new Decimal?(amount1.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      this.RollupArticleAmount(article, delta);
    }
    if (row.IsGroup.Value)
      this.EnsureAlloc(row);
    if (sender.ObjectsEqual<GLBudgetLine.accountID, GLBudgetLine.subID>((object) row, e.OldRow))
      return;
    if (this.AllocationIndex != null)
    {
      foreach (GLBudgetLineDetail budgetLineDetail in this.AllocationIndex.GetList(row.GroupID.Value))
      {
        int? nullable1 = budgetLineDetail.AccountID;
        int? nullable2 = row.AccountID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = budgetLineDetail.SubID;
          nullable1 = row.SubID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            continue;
        }
        budgetLineDetail.AccountID = row.AccountID;
        budgetLineDetail.SubID = row.SubID;
        ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
      }
    }
    else
    {
      foreach (PXResult<GLBudgetLineDetail> pxResult in ((PXSelectBase<GLBudgetLineDetail>) pxSelect).Select(new object[1]
      {
        (object) row.GroupID
      }))
      {
        GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
        int? accountId = budgetLineDetail.AccountID;
        int? nullable = row.AccountID;
        if (accountId.GetValueOrDefault() == nullable.GetValueOrDefault() & accountId.HasValue == nullable.HasValue)
        {
          nullable = budgetLineDetail.SubID;
          int? subId = row.SubID;
          if (nullable.GetValueOrDefault() == subId.GetValueOrDefault() & nullable.HasValue == subId.HasValue)
            continue;
        }
        budgetLineDetail.AccountID = row.AccountID;
        budgetLineDetail.SubID = row.SubID;
        ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Update(budgetLineDetail);
      }
    }
  }

  protected virtual void GLBudgetLine_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLBudgetLine row = (GLBudgetLine) e.Row;
    if (e.Operation != 3)
      this.GLBudgetLine_VerifyBranchLedgerLink(sender, row);
    bool? nullable1 = row.Comparison;
    if (nullable1.HasValue)
    {
      nullable1 = row.Comparison;
      if (nullable1.Value)
      {
        ((CancelEventArgs) e).Cancel = true;
        ((PXSelectBase) this.BudgetArticles).Cache.SetStatus((object) row, (PXEntryStatus) 5);
      }
    }
    nullable1 = row.IsGroup;
    if (nullable1.Value)
    {
      PXDefaultAttribute.SetPersistingCheck<GLBudgetLine.accountID>(sender, e.Row, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<GLBudgetLine.subID>(sender, e.Row, (PXPersistingCheck) 2);
    }
    if (row.AccountMask != null || row.SubMask != null)
      return;
    int? nullable2 = row.AccountID;
    if (!nullable2.HasValue)
      return;
    nullable2 = row.SubID;
    if (!nullable2.HasValue)
      return;
    GLBudgetEntry.AccountIndex accountIndex = this.accountIndex;
    nullable2 = row.AccountID;
    int id1 = nullable2.Value;
    string cd1 = accountIndex.GetCD(id1);
    GLBudgetEntry.SubIndex subIndex = this.subIndex;
    nullable2 = row.SubID;
    int id2 = nullable2.Value;
    string cd2 = subIndex.GetCD(id2);
    row.AccountMask = cd1;
    row.SubMask = cd2;
  }

  protected virtual void GLBudgetLine_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree || !(e.Row is GLBudgetLine row))
      return;
    bool? nullable1 = row.IsGroup;
    int? nullable2;
    if (!nullable1.Value)
    {
      nullable1 = row.IsUploaded;
      if (nullable1.HasValue)
      {
        nullable2 = row.AccountID;
        if (!nullable2.HasValue)
          ((CancelEventArgs) e).Cancel = true;
      }
    }
    GLBudgetLine glBudgetLine1 = row;
    Guid? nullable3 = row.GroupID;
    Guid? nullable4 = new Guid?(nullable3 ?? Guid.NewGuid());
    glBudgetLine1.GroupID = nullable4;
    nullable2 = this.CurrentSelected.AccountID;
    if (nullable2.HasValue)
    {
      nullable2 = this.CurrentSelected.AccountID;
      if (nullable2.GetValueOrDefault() != int.MinValue)
      {
        nullable2 = this.CurrentSelected.SubID;
        if (nullable2.HasValue)
        {
          nullable2 = this.CurrentSelected.SubID;
          if (nullable2.GetValueOrDefault() != int.MinValue)
            row.Rollup = new bool?(true);
        }
      }
    }
    if (this.IsNullOrEmpty(row.ParentGroupID))
    {
      GLBudgetLine glBudgetLine2 = row;
      nullable3 = this.CurrentSelected.Group;
      Guid? nullable5 = new Guid?(nullable3 ?? Guid.Empty);
      glBudgetLine2.ParentGroupID = nullable5;
    }
    if (row.GroupMask == null)
      row.GroupMask = new byte[0];
    if (this.IsEmpty(row.ParentGroupID) || row.GroupMask.Length != 0)
      return;
    GLBudgetLine article = this.GetArticle(row.BranchID, row.LedgerID, row.FinYear, row.ParentGroupID);
    if (article == null)
      return;
    row.GroupMask = article.GroupMask;
  }

  protected virtual void GLBudgetLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree)
      return;
    GLBudgetLine row = e.Row as GLBudgetLine;
    PXFormulaAttribute.CalcAggregate<GLBudgetLineDetail.amount>(((PXSelectBase) this.Allocations).Cache, (object) row);
    if (this.ArticleIndex != null)
      this.ArticleIndex.Add(row);
    if (this._BudgetArticlesIndex != null)
      this._BudgetArticlesIndex.Add(row);
    Guid? parentGroupId = row.ParentGroupID;
    Guid empty = Guid.Empty;
    if ((parentGroupId.HasValue ? (parentGroupId.GetValueOrDefault() != empty ? 1 : 0) : 1) == 0)
      return;
    this.RollupArticleAmount(row, row.Amount);
    sender.Current = (object) row;
  }

  protected virtual void GLBudgetLine_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is GLBudgetLine row))
      return;
    Guid? groupId = row.GroupID;
    Guid? parentGroupId = row.ParentGroupID;
    if ((groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != parentGroupId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && !FlaggedModeScopeBase<SuppressDeletingDialogBoxScope>.IsActive)
    {
      if (((IQueryable<PXResult<GLBudgetLine>>) PXSelectBase<GLBudgetLine, PXViewOf<GLBudgetLine>.BasedOn<SelectFromBase<GLBudgetLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<GLBudgetLine.parentGroupID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.GroupID
      })).Any<PXResult<GLBudgetLine>>())
      {
        if (FlaggedModeScopeBase<SuppressDeletingDialogBoxScope>.IsActive || ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Ask("Delete Group", "The child records will be deleted. Are you sure you want to delete the group?", (MessageButtons) 4) == 6)
        {
          this.deleteChildBudgetRecords(row.GroupID);
        }
        else
        {
          ((CancelEventArgs) e).Cancel = true;
          ((PXSelectBase) this.BudgetArticles).View.RequestRefresh();
          return;
        }
      }
    }
    if (e.ExternalCall)
    {
      if (this.AllocationIndex != null)
      {
        foreach (GLBudgetLineDetail budgetLineDetail in this.AllocationIndex.GetList(row.GroupID.Value))
        {
          Decimal? releasedAmount = budgetLineDetail.ReleasedAmount;
          Decimal num = 0M;
          if (!(releasedAmount.GetValueOrDefault() == num & releasedAmount.HasValue))
            throw new PXException("Budget Articles with non-zero Released amount cannot be deleted.");
        }
      }
      else
      {
        foreach (PXResult<GLBudgetLineDetail> pxResult in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
        {
          (object) row.FinYear,
          (object) row.GroupID
        }))
        {
          Decimal? releasedAmount = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult).ReleasedAmount;
          Decimal num = 0M;
          if (!(releasedAmount.GetValueOrDefault() == num & releasedAmount.HasValue))
            throw new PXException("Budget Articles with non-zero Released amount cannot be deleted.");
        }
      }
    }
    bool? nullable1 = row.Comparison;
    if (nullable1.HasValue)
    {
      nullable1 = row.Comparison;
      if (nullable1.Value)
        throw new PXException("Comparison line cannot be deleted.");
    }
    if (e.ExternalCall)
    {
      Guid? nullable2 = row.ParentGroupID;
      Guid empty = Guid.Empty;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0)
      {
        GLBudgetLine article1 = row;
        Decimal? amount = row.Amount;
        Decimal? delta1 = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
        this.RollupArticleAmount(article1, delta1);
        if (this.AllocationIndex != null)
        {
          GLBudgetEntry.GLBudgetLineDetailIdx allocationIndex = this.AllocationIndex;
          nullable2 = row.GroupID;
          Guid GroupID = nullable2.Value;
          foreach (GLBudgetLineDetail budgetLineDetail in allocationIndex.GetList(GroupID))
          {
            GLBudgetLine article2 = row;
            int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
            amount = budgetLineDetail.Amount;
            Decimal? delta2 = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
            this.RollupAllocation(article2, fieldNbr, delta2);
          }
        }
        else
        {
          foreach (PXResult<GLBudgetLineDetail> pxResult in ((PXSelectBase<GLBudgetLineDetail>) this.Allocations).Select(new object[2]
          {
            (object) row.FinYear,
            (object) row.GroupID
          }))
          {
            GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
            GLBudgetLine article3 = row;
            int fieldNbr = int.Parse(budgetLineDetail.FinPeriodID.Substring(4));
            amount = budgetLineDetail.Amount;
            Decimal? delta3 = amount.HasValue ? new Decimal?(-amount.GetValueOrDefault()) : new Decimal?();
            this.RollupAllocation(article3, fieldNbr, delta3);
          }
        }
      }
    }
    if (!e.ExternalCall)
      return;
    nullable1 = row.IsGroup;
    if (nullable1.GetValueOrDefault() && this.CheckReleasedInGroup(row))
      throw new PXException("The article cannot be deleted because it has at least one line released.");
  }

  private void deleteChildBudgetRecords(Guid? groupID)
  {
    foreach (PXResult<GLBudgetLine> pxResult in PXSelectBase<GLBudgetLine, PXViewOf<GLBudgetLine>.BasedOn<SelectFromBase<GLBudgetLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<GLBudgetLine.parentGroupID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) groupID
    }))
    {
      GLBudgetLine glBudgetLine = PXResult<GLBudgetLine>.op_Implicit(pxResult);
      if (glBudgetLine.IsGroup.GetValueOrDefault())
        this.deleteChildBudgetRecords(glBudgetLine.GroupID);
      ((PXSelectBase) this.BudgetArticles).Cache.Delete((object) glBudgetLine);
    }
  }

  protected virtual void GLBudgetLine_VerifyBranchLedgerLink(PXCache cache, GLBudgetLine budgetLine)
  {
    if (PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelectJoin<Ledger, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<Ledger.balanceType, Equal<LedgerBalanceType.budget>, And<Branch.branchID, Equal<Required<BudgetFilter.branchID>>, And<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) budgetLine.BranchID,
      (object) budgetLine.LedgerID
    })) == null)
      throw new PXSetPropertyException("The {0} branch or branches have not been associated with the {1} ledger on the Ledgers (GL201500) form.", new object[2]
      {
        (object) PXAccess.GetBranchCD(budgetLine.BranchID),
        (object) budgetLine.LedgerID
      });
  }

  protected virtual void GLBudgetLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLBudgetLine row = e.Row as GLBudgetLine;
    if (this.ArticleIndex != null)
      this.ArticleIndex.Delete(row);
    if (this._BudgetArticlesIndex == null)
      return;
    this._BudgetArticlesIndex.Delete(row);
  }

  protected virtual void GLBudgetLine_AccountID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    GLBudgetLine row = e.Row as GLBudgetLine;
    if (!this.suppressIDs || row == null || !row.Comparison.GetValueOrDefault())
      return;
    e.ReturnValue = (object) null;
  }

  protected virtual void GLBudgetLine_AccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLBudgetLine.description>(e.Row);
  }

  protected virtual void GLBudgetLine_SubID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    GLBudgetLine row = e.Row as GLBudgetLine;
    if (!this.suppressIDs || row == null || !row.Comparison.GetValueOrDefault())
      return;
    e.ReturnValue = (object) null;
  }

  protected virtual void GLBudgetLine_Amount_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    GLBudgetLine row = (GLBudgetLine) e.Row;
    if (row == null)
      return;
    row.Released = new bool?(false);
  }

  protected virtual void GLBudgetLine_AccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree || !(e.Row is GLBudgetLine row))
      return;
    bool? nullable = row.IsGroup;
    if (nullable.HasValue)
    {
      nullable = row.IsGroup;
      if (nullable.Value)
        goto label_6;
    }
    nullable = row.IsPreloaded;
    if (!nullable.HasValue)
      return;
    nullable = row.IsPreloaded;
    if (!nullable.Value)
      return;
label_6:
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLBudgetLine_SubID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (this._CurrentAction == GLBudgetEntry.GLBudgetEntryActionType.PreloadBudgetTree || !(e.Row is GLBudgetLine row))
      return;
    bool? isGroup = row.IsGroup;
    if (isGroup.HasValue)
    {
      isGroup = row.IsGroup;
      if (isGroup.Value)
        ((CancelEventArgs) e).Cancel = true;
    }
    string accountCD = (string) null;
    if (e.NewValue is int)
      accountCD = this.subIndex.GetCD((int) e.NewValue);
    if (accountCD != null && !this.MatchMask(accountCD, this.CurrentSelected.SubMask ?? string.Empty))
      throw new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Selected subaccount is not allowed in this group or does not exist (Subaccount mask: {0})"), (object) this.CurrentSelected.SubMask));
  }

  protected virtual void GLBudgetLineDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    GLBudgetLineDetail row = (GLBudgetLineDetail) e.Row;
    if (row.AccountID.HasValue && row.SubID.HasValue)
      return;
    PXDefaultAttribute.SetPersistingCheck<GLBudgetLineDetail.accountID>(sender, e.Row, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLBudgetLineDetail.subID>(sender, e.Row, (PXPersistingCheck) 2);
  }

  protected virtual void GLBudgetLineDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    object row = e.Row;
  }

  protected virtual void GLBudgetLineDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    GLBudgetLineDetail row = e.Row as GLBudgetLineDetail;
    if (this.AllocationIndex == null || !row.GroupID.HasValue)
      return;
    this.AllocationIndex.Add(row);
  }

  protected virtual void GLBudgetLineDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLBudgetLineDetail row = e.Row as GLBudgetLineDetail;
    if (this.AllocationIndex == null)
      return;
    Guid? groupId = row.GroupID;
    if (!groupId.HasValue)
      return;
    GLBudgetEntry.GLBudgetLineDetailIdx allocationIndex = this.AllocationIndex;
    groupId = row.GroupID;
    Guid GroupID = groupId.Value;
    string finPeriodId = row.FinPeriodID;
    allocationIndex.Delete(GroupID, finPeriodId);
  }

  protected virtual void ManageBudgetDialog_Message_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) PXMessages.LocalizeNoPrefix("All budget articles will be rolled back to the last released values. All changes will be lost.");
  }

  protected virtual void ManageBudgetDialog_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ManageBudgetDialog row = (ManageBudgetDialog) e.Row;
    if (row == null)
      return;
    switch (row.Method)
    {
      case "R":
        row.Message = PXMessages.LocalizeNoPrefix("All budget articles will be rolled back to the last released values. All changes will be lost.");
        break;
      case "C":
        row.Message = PXMessages.LocalizeNoPrefix("Current budget will be converted using the budget tree from the Budget Configuration form.");
        break;
    }
  }

  private void PrepareIndexes()
  {
    BudgetFilter current = ((PXSelectBase<BudgetFilter>) this.Filter).Current;
    if (current.BranchID.HasValue && current.FinYear != null && current.LedgerID.HasValue)
    {
      GLBudgetEntry.GLBudgetLineIdx articleIndex = this._ArticleIndex;
      if ((articleIndex != null ? (articleIndex.IsEmpty ? 1 : 0) : 1) != 0 || !this.IndexesIsPrepared)
      {
        this._ArticleGroups = PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<True>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
        this._ArticleIndex = new GLBudgetEntry.GLBudgetLineIdx(PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        foreach (GLBudgetLine glBudgetLine in this._ArticleIndex.GetAll())
          glBudgetLine.Allocated = (Decimal[]) null;
        this._AllocationIndex = new GLBudgetEntry.GLBudgetLineDetailIdx(PXSelectBase<GLBudgetLineDetail, PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Current<BudgetFilter.ledgerID>>, And<GLBudgetLineDetail.branchID, Equal<Current<BudgetFilter.branchID>>, And<GLBudgetLineDetail.finYear, Equal<Current<BudgetFilter.finYear>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      }
    }
    else
    {
      this._ArticleIndex = new GLBudgetEntry.GLBudgetLineIdx();
      this._AllocationIndex = new GLBudgetEntry.GLBudgetLineDetailIdx();
    }
    this.IndexesIsPrepared = true;
  }

  private void ClearIndices()
  {
    this._ArticleGroups = (PXResultset<GLBudgetLine>) null;
    this._ArticleIndex = (GLBudgetEntry.GLBudgetLineIdx) null;
    this._AllocationIndex = (GLBudgetEntry.GLBudgetLineDetailIdx) null;
    this.IndexesIsPrepared = false;
  }

  protected enum GLBudgetEntryActionType
  {
    None,
    PreloadBudgetTree,
    Preload,
    ImportExcel,
    ConvertBudget,
    RollbackBudget,
  }

  protected class PXResultGLBudgetLineComparer : IEqualityComparer<PXResult<GLBudgetLine>>
  {
    public bool Equals(PXResult<GLBudgetLine> x, PXResult<GLBudgetLine> y)
    {
      GLBudgetLine glBudgetLine1 = PXResult<GLBudgetLine>.op_Implicit(x);
      GLBudgetLine glBudgetLine2 = PXResult<GLBudgetLine>.op_Implicit(y);
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      Guid? groupId1 = glBudgetLine1.GroupID;
      Guid? groupId2 = glBudgetLine2.GroupID;
      if (groupId1.HasValue != groupId2.HasValue)
        return false;
      return !groupId1.HasValue || groupId1.GetValueOrDefault() == groupId2.GetValueOrDefault();
    }

    public int GetHashCode(PXResult<GLBudgetLine> x)
    {
      return x == null ? 0 : PXResult<GLBudgetLine>.op_Implicit(x).GroupID.GetHashCode();
    }
  }

  protected struct GLBudgetLineDetailKey
  {
    public int BranchID;
    public int LedgerID;
    public int AccountID;
    public int SubID;
    public string FinYear;
    public string FinPeriodID;

    public override bool Equals(object o)
    {
      return o is GLBudgetEntry.GLBudgetLineDetailKey budgetLineDetailKey && this.BranchID.Equals(budgetLineDetailKey.BranchID) && this.LedgerID.Equals(budgetLineDetailKey.LedgerID) && this.FinYear.Equals(budgetLineDetailKey.FinYear) && this.FinPeriodID.Equals(budgetLineDetailKey.FinPeriodID) && this.AccountID.Equals(budgetLineDetailKey.AccountID) && this.SubID.Equals(budgetLineDetailKey.SubID);
    }

    public override int GetHashCode()
    {
      return (((((37 * 397 + this.BranchID.GetHashCode()) * 397 + this.LedgerID.GetHashCode()) * 397 + this.FinYear.GetHashCode()) * 397 + this.FinPeriodID.GetHashCode()) * 397 + this.AccountID.GetHashCode()) * 397 + this.SubID.GetHashCode();
    }
  }

  /// <summary>
  /// Index for GLBudgetLineDetail by (GroupID, FinPeriodID) and keyfields (BranchID, LedgerID, FinYear, AccountID, SubID, FinPeriodID)
  /// </summary>
  protected class GLBudgetLineDetailIdx : IDisposable
  {
    private Dictionary<Guid, Dictionary<string, GLBudgetLineDetail>> all;
    private Dictionary<GLBudgetEntry.GLBudgetLineDetailKey, GLBudgetLineDetail> keys;

    public GLBudgetLineDetailIdx()
      : this(100)
    {
    }

    public GLBudgetLineDetailIdx(int capacity)
      : this(capacity, false)
    {
    }

    public GLBudgetLineDetailIdx(bool withKeys)
      : this(100, withKeys)
    {
    }

    public GLBudgetLineDetailIdx(int capacity, bool withKeys)
    {
      this.all = new Dictionary<Guid, Dictionary<string, GLBudgetLineDetail>>(capacity);
      if (!withKeys)
        return;
      this.keys = new Dictionary<GLBudgetEntry.GLBudgetLineDetailKey, GLBudgetLineDetail>(capacity);
    }

    public GLBudgetLineDetailIdx(PXResultset<GLBudgetLineDetail> allocations)
      : this(allocations.Count, true)
    {
      this.Add(allocations);
    }

    public GLBudgetLineDetailIdx(IEnumerable<GLBudgetLineDetail> allocations)
      : this(allocations.Count<GLBudgetLineDetail>(), true)
    {
      this.Add(allocations);
    }

    public bool Add(GLBudgetLineDetail alloc)
    {
      Guid key1 = alloc.GroupID.Value;
      string finPeriodId = alloc.FinPeriodID;
      int num = 0;
      Dictionary<string, GLBudgetLineDetail> dictionary = (Dictionary<string, GLBudgetLineDetail>) null;
      if (!this.all.TryGetValue(key1, out dictionary))
      {
        dictionary = new Dictionary<string, GLBudgetLineDetail>();
        this.all.Add(key1, dictionary);
      }
      dictionary[finPeriodId] = alloc;
      if (this.keys == null)
        return num != 0;
      GLBudgetEntry.GLBudgetLineDetailKey key2 = new GLBudgetEntry.GLBudgetLineDetailKey();
      key2.BranchID = alloc.BranchID.GetValueOrDefault();
      key2.LedgerID = alloc.LedgerID.GetValueOrDefault();
      key2.FinYear = alloc.FinYear;
      key2.FinPeriodID = alloc.FinPeriodID;
      ref GLBudgetEntry.GLBudgetLineDetailKey local1 = ref key2;
      int? nullable = alloc.AccountID;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      local1.AccountID = valueOrDefault1;
      ref GLBudgetEntry.GLBudgetLineDetailKey local2 = ref key2;
      nullable = alloc.SubID;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      local2.SubID = valueOrDefault2;
      this.keys[key2] = alloc;
      return num != 0;
    }

    public void Add(IEnumerable<GLBudgetLineDetail> allocations)
    {
      foreach (GLBudgetLineDetail allocation in allocations)
        this.Add(allocation);
    }

    public void Add(PXResultset<GLBudgetLineDetail> allocations)
    {
      foreach (PXResult<GLBudgetLineDetail> allocation in allocations)
        this.Add(PXResult<GLBudgetLineDetail>.op_Implicit(allocation));
    }

    public bool Delete(GLBudgetLineDetail alloc)
    {
      return alloc != null && this.Delete(alloc.GroupID.Value, alloc.FinPeriodID);
    }

    public bool Delete(Guid GroupID, string FinPeriodId)
    {
      Dictionary<string, GLBudgetLineDetail> dictionary = (Dictionary<string, GLBudgetLineDetail>) null;
      bool flag = false;
      if (this.all.TryGetValue(GroupID, out dictionary))
      {
        flag = dictionary.Remove(FinPeriodId);
        if (dictionary.Count == 0)
          this.all.Remove(GroupID);
      }
      return flag;
    }

    public void Clear()
    {
      this.all.Clear();
      if (this.keys == null)
        return;
      this.keys.Clear();
    }

    public IEnumerable<GLBudgetLineDetail> Get(Guid GroupID)
    {
      Dictionary<string, GLBudgetLineDetail> dictionary;
      return this.all.TryGetValue(GroupID, out dictionary) ? (IEnumerable<GLBudgetLineDetail>) dictionary.Values : (IEnumerable<GLBudgetLineDetail>) null;
    }

    public IEnumerable<GLBudgetLineDetail> GetList(Guid GroupID)
    {
      Dictionary<string, GLBudgetLineDetail> dictionary;
      return this.all.TryGetValue(GroupID, out dictionary) ? (IEnumerable<GLBudgetLineDetail>) dictionary.Values : (IEnumerable<GLBudgetLineDetail>) new List<GLBudgetLineDetail>();
    }

    public GLBudgetLineDetail Get(
      Guid GroupID,
      string FinPeriodId,
      GLBudgetEntry.GLBudgetLineDetailIdx.checkGLBudgetLineDetail check = null)
    {
      Dictionary<string, GLBudgetLineDetail> dictionary = (Dictionary<string, GLBudgetLineDetail>) null;
      GLBudgetLineDetail line = (GLBudgetLineDetail) null;
      if (this.all.TryGetValue(GroupID, out dictionary) && dictionary.TryGetValue(FinPeriodId, out line) || line != null || check == null)
        return line;
      line = check(line);
      return line;
    }

    public GLBudgetLineDetail GetByKey(
      GLBudgetEntry.GLBudgetLineDetailKey key,
      GLBudgetEntry.GLBudgetLineDetailIdx.checkGLBudgetLineDetail check = null)
    {
      if (this.keys == null)
        return (GLBudgetLineDetail) null;
      GLBudgetLineDetail line = (GLBudgetLineDetail) null;
      if (!this.keys.TryGetValue(key, out line))
        return (GLBudgetLineDetail) null;
      return line != null && check != null ? check(line) : line;
    }

    public GLBudgetLineDetail GetByKey(
      GLBudgetLineDetail keySource,
      GLBudgetEntry.GLBudgetLineDetailIdx.checkGLBudgetLineDetail check = null)
    {
      if (this.keys == null)
        return (GLBudgetLineDetail) null;
      GLBudgetEntry.GLBudgetLineDetailKey key = new GLBudgetEntry.GLBudgetLineDetailKey();
      ref GLBudgetEntry.GLBudgetLineDetailKey local1 = ref key;
      int? nullable = keySource.BranchID;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      local1.BranchID = valueOrDefault1;
      ref GLBudgetEntry.GLBudgetLineDetailKey local2 = ref key;
      nullable = keySource.LedgerID;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      local2.LedgerID = valueOrDefault2;
      key.FinYear = keySource.FinYear;
      key.FinPeriodID = keySource.FinPeriodID;
      ref GLBudgetEntry.GLBudgetLineDetailKey local3 = ref key;
      nullable = keySource.AccountID;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      local3.AccountID = valueOrDefault3;
      ref GLBudgetEntry.GLBudgetLineDetailKey local4 = ref key;
      nullable = keySource.SubID;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      local4.SubID = valueOrDefault4;
      GLBudgetLineDetail byKey = this.GetByKey(key);
      return byKey != null && check != null ? check(byKey, keySource) : byKey;
    }

    public void Dispose() => this.Clear();

    public delegate GLBudgetLineDetail checkGLBudgetLineDetail(
      GLBudgetLineDetail line,
      GLBudgetLineDetail sourceLine = null);
  }

  /// <summary>
  /// Index for GLBudgetLine by GroupID and keyfields (BranchID, LedgerID, FinYear, AccountID, SubID)
  /// </summary>
  protected class GLBudgetLineIdx
  {
    private Dictionary<Guid, GLBudgetLine> all;
    private Dictionary<Guid, GLBudgetLine> groups;
    private Dictionary<GLBudgetEntry.GLBudgetLineKey, Dictionary<Guid, GLBudgetLine>> keys;

    public Dictionary<Guid, GLBudgetLine> All => this.all;

    public Dictionary<Guid, GLBudgetLine> Groups => this.groups;

    public bool IsEmpty => this.all.Count == 0;

    public GLBudgetLineIdx()
      : this(100)
    {
    }

    public GLBudgetLineIdx(int capacity)
      : this(capacity, false)
    {
    }

    public GLBudgetLineIdx(bool withKeys)
      : this(100, withKeys)
    {
    }

    public GLBudgetLineIdx(int capacity, bool withKeys)
    {
      this.all = new Dictionary<Guid, GLBudgetLine>(capacity);
      this.groups = new Dictionary<Guid, GLBudgetLine>();
      if (!withKeys)
        return;
      this.keys = new Dictionary<GLBudgetEntry.GLBudgetLineKey, Dictionary<Guid, GLBudgetLine>>(capacity);
    }

    public GLBudgetLineIdx(IEnumerable<PXResult<GLBudgetLine>> lines)
      : this(lines.Count<PXResult<GLBudgetLine>>(), true)
    {
      this.Add(lines);
    }

    public GLBudgetLineIdx(PXResultset<GLBudgetLine> lines)
      : this(lines.Count, true)
    {
      this.Add(lines);
    }

    public GLBudgetLineIdx(IEnumerable<GLBudgetLine> lines)
      : this(lines.Count<GLBudgetLine>(), false)
    {
      this.Add(lines);
    }

    public bool Add(GLBudgetLine line)
    {
      Guid? groupId;
      int num;
      if (line == null)
      {
        num = 1;
      }
      else
      {
        groupId = line.GroupID;
        num = !groupId.HasValue ? 1 : 0;
      }
      if (num != 0)
        return false;
      groupId = line.GroupID;
      Guid key1 = groupId.Value;
      this.all[key1] = line;
      if (line.IsGroup.GetValueOrDefault())
        this.groups[key1] = line;
      if (this.keys != null)
      {
        GLBudgetEntry.GLBudgetLineKey key2 = GLBudgetEntry.GLBudgetLineKey.Create(line);
        Dictionary<Guid, GLBudgetLine> dictionary;
        if (!this.keys.TryGetValue(key2, out dictionary))
        {
          dictionary = new Dictionary<Guid, GLBudgetLine>();
          this.keys.Add(key2, dictionary);
        }
        dictionary[key1] = line;
      }
      return true;
    }

    public int Add(IEnumerable<GLBudgetLine> lines)
    {
      int num = 0;
      foreach (GLBudgetLine line in lines)
      {
        if (this.Add(line))
          ++num;
      }
      return num;
    }

    public int Add(IEnumerable<PXResult<GLBudgetLine>> lines)
    {
      return this.Add(GraphHelper.RowCast<GLBudgetLine>((IEnumerable) lines));
    }

    public int Add(PXResultset<GLBudgetLine> lines)
    {
      return this.Add(GraphHelper.RowCast<GLBudgetLine>((IEnumerable) lines));
    }

    public bool Delete(Guid? GroupID)
    {
      if (!GroupID.HasValue)
        return false;
      Guid key1 = GroupID.Value;
      if (this.keys != null)
      {
        GLBudgetLine line;
        this.all.TryGetValue(key1, out line);
        if (line != null)
        {
          GLBudgetEntry.GLBudgetLineKey key2 = GLBudgetEntry.GLBudgetLineKey.Create(line);
          Dictionary<Guid, GLBudgetLine> dictionary;
          if (this.keys.TryGetValue(key2, out dictionary))
          {
            dictionary.Remove(key1);
            if (dictionary.Count == 0)
              this.keys.Remove(key2);
          }
        }
      }
      this.groups.Remove(key1);
      return this.all.Remove(key1);
    }

    public bool Delete(GLBudgetLine line)
    {
      Guid? groupId;
      int num;
      if (line == null)
      {
        num = 1;
      }
      else
      {
        groupId = line.GroupID;
        num = !groupId.HasValue ? 1 : 0;
      }
      if (num != 0)
        return false;
      groupId = line.GroupID;
      return this.Delete(new Guid?(groupId.Value));
    }

    public void Clear()
    {
      this.all.Clear();
      this.groups.Clear();
      if (this.keys == null)
        return;
      this.keys.Clear();
    }

    public GLBudgetLine GetGroup(
      Guid? GroupID,
      GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check = null)
    {
      if (!GroupID.HasValue)
        return (GLBudgetLine) null;
      GLBudgetLine line;
      this.groups.TryGetValue(GroupID.Value, out line);
      return line != null && check != null ? check(line) : line;
    }

    public IEnumerable<GLBudgetLine> GetGroups() => (IEnumerable<GLBudgetLine>) this.groups.Values;

    public IEnumerable<GLBudgetLine> GetAll() => (IEnumerable<GLBudgetLine>) this.all.Values;

    public GLBudgetLine Get(
      Guid? GroupID,
      GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check = null)
    {
      if (!GroupID.HasValue)
        return (GLBudgetLine) null;
      GLBudgetLine line;
      this.all.TryGetValue(GroupID.Value, out line);
      return line != null && check != null ? check(line) : line;
    }

    public GLBudgetLine GetByKey(
      GLBudgetLine keySource,
      GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check = null)
    {
      GLBudgetLine byKey = this.GetByKey(GLBudgetEntry.GLBudgetLineKey.Create(keySource));
      return byKey != null && check != null ? check(byKey, keySource) : byKey;
    }

    public GLBudgetLine GetByKey(
      GLBudgetEntry.GLBudgetLineKey key,
      GLBudgetEntry.GLBudgetLineIdx.checkGLBudgetLine check = null)
    {
      if (this.keys == null)
        return (GLBudgetLine) null;
      Dictionary<Guid, GLBudgetLine> dictionary;
      if (!this.keys.TryGetValue(key, out dictionary))
        return (GLBudgetLine) null;
      GLBudgetLine line = dictionary.Values.FirstOrDefault<GLBudgetLine>();
      return line != null && check != null ? check(line) : line;
    }

    public IEnumerable<GLBudgetLine> GetList(GLBudgetEntry.GLBudgetLineKey key)
    {
      if (this.keys == null)
        return (IEnumerable<GLBudgetLine>) null;
      Dictionary<Guid, GLBudgetLine> dictionary;
      this.keys.TryGetValue(key, out dictionary);
      return (IEnumerable<GLBudgetLine>) dictionary.Values;
    }

    public delegate GLBudgetLine checkGLBudgetLine(GLBudgetLine line, GLBudgetLine sourceLine = null);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  protected class SubDefinition : IPrefetchable, IPXCompanyDependent
  {
    private Dictionary<int, string> _IDCD = new Dictionary<int, string>();
    private Dictionary<string, int> _CDID = new Dictionary<string, int>();
    private Dictionary<int, bool> _IDActive = new Dictionary<int, bool>();
    private static int? _defaultID;
    private static string _defaultCD;

    public static int? DefaultID
    {
      get
      {
        PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, Array.Empty<Type>());
        return GLBudgetEntry.SubDefinition._defaultID;
      }
    }

    public static string DefaultCD
    {
      get
      {
        PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, Array.Empty<Type>());
        return GLBudgetEntry.SubDefinition._defaultCD;
      }
    }

    public void Prefetch()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Sub>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<Sub.subID>(),
        (PXDataField) new PXDataField<Sub.subCD>(),
        (PXDataField) new PXDataField<Sub.active>()
      }))
      {
        int? int32 = pxDataRecord.GetInt32(0);
        string key = pxDataRecord.GetString(1);
        bool? boolean = pxDataRecord.GetBoolean(2);
        this._IDCD.Add(int32.GetValueOrDefault(), key);
        this._CDID.Add(key, int32.GetValueOrDefault());
        this._IDActive.Add(int32.GetValueOrDefault(), boolean.GetValueOrDefault());
      }
      GLBudgetEntry.SubDefinition._defaultID = SubAccountAttribute.TryGetDefaultSubID();
      this._IDCD.TryGetValue(GLBudgetEntry.SubDefinition._defaultID.GetValueOrDefault(), out GLBudgetEntry.SubDefinition._defaultCD);
    }

    public static void Fill()
    {
      PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, new Type[1]
      {
        typeof (Sub)
      });
    }

    public static string getCD(int id)
    {
      string cd = (string) null;
      PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, new Type[1]
      {
        typeof (Sub)
      })._IDCD.TryGetValue(id, out cd);
      return cd;
    }

    public static int? getID(string cd)
    {
      int num = -1;
      PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, new Type[1]
      {
        typeof (Sub)
      })._CDID.TryGetValue(cd, out num);
      return num < 0 ? new int?() : new int?(num);
    }

    public static bool IsActive(int id)
    {
      bool flag = false;
      PXDatabase.GetSlot<GLBudgetEntry.SubDefinition>(typeof (GLBudgetEntry.SubDefinition).FullName, new Type[1]
      {
        typeof (Sub)
      })._IDActive.TryGetValue(id, out flag);
      return flag;
    }
  }

  private abstract class Index
  {
    protected readonly Dictionary<int, string> CDs = new Dictionary<int, string>();
    protected readonly Dictionary<string, int> IDs = new Dictionary<string, int>();
    protected readonly Dictionary<int, bool> isActiveFlags = new Dictionary<int, bool>();

    public int? GetID(string cd)
    {
      int num;
      return !this.IDs.TryGetValue(cd, out num) ? new int?() : new int?(num);
    }

    public string GetCD(int id)
    {
      string str;
      return !this.CDs.TryGetValue(id, out str) ? (string) null : str;
    }

    public bool IsActive(int id)
    {
      bool flag;
      return this.isActiveFlags.TryGetValue(id, out flag) && flag;
    }
  }

  private class AccountIndex : GLBudgetEntry.Index
  {
    public AccountIndex(PXGraph graph)
    {
      PXView pxView = new PXView(graph, true, PXSelectBase<Account, PXViewOf<Account>.BasedOn<SelectFromBase<Account, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly.Config>.GetCommand());
      using (new PXFieldScope(pxView, new Type[3]
      {
        typeof (Account.accountID),
        typeof (Account.accountCD),
        typeof (Account.active)
      }))
      {
        foreach (Account account in pxView.SelectMulti(Array.Empty<object>()))
        {
          Dictionary<int, string> cds = this.CDs;
          int? accountId = account.AccountID;
          int key = accountId.Value;
          string accountCd1 = account.AccountCD;
          cds.Add(key, accountCd1);
          Dictionary<string, int> ids = this.IDs;
          string accountCd2 = account.AccountCD;
          accountId = account.AccountID;
          int num = accountId.Value;
          ids.Add(accountCd2, num);
          this.isActiveFlags.Add(account.AccountID.Value, account.Active.GetValueOrDefault());
        }
      }
    }
  }

  private class SubIndex : GLBudgetEntry.Index
  {
    public readonly int? DefaultID;
    public readonly string DefaultCD;

    public SubIndex(PXGraph graph)
    {
      PXView pxView = new PXView(graph, true, PXSelectBase<Sub, PXViewOf<Sub>.BasedOn<SelectFromBase<Sub, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly.Config>.GetCommand());
      using (new PXFieldScope(pxView, new Type[3]
      {
        typeof (Sub.subID),
        typeof (Sub.subCD),
        typeof (Sub.active)
      }))
      {
        foreach (Sub sub in pxView.SelectMulti(Array.Empty<object>()))
        {
          Dictionary<int, string> cds = this.CDs;
          int? subId = sub.SubID;
          int key = subId.Value;
          string subCd1 = sub.SubCD;
          cds.Add(key, subCd1);
          Dictionary<string, int> ids = this.IDs;
          string subCd2 = sub.SubCD;
          subId = sub.SubID;
          int num = subId.Value;
          ids.Add(subCd2, num);
          this.isActiveFlags.Add(sub.SubID.Value, sub.Active.GetValueOrDefault());
        }
        this.DefaultID = SubAccountAttribute.TryGetDefaultSubID();
        this.CDs.TryGetValue(this.DefaultID.GetValueOrDefault(), out this.DefaultCD);
      }
    }
  }

  private class LedgerIndex : GLBudgetEntry.Index
  {
    public LedgerIndex(PXGraph graph)
    {
      PXView pxView = new PXView(graph, true, PXSelectBase<Ledger, PXViewOf<Ledger>.BasedOn<SelectFromBase<Ledger, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly.Config>.GetCommand());
      using (new PXFieldScope(pxView, new Type[3]
      {
        typeof (Ledger.ledgerID),
        typeof (Ledger.ledgerID),
        typeof (Ledger.balanceType)
      }))
      {
        foreach (Ledger ledger in pxView.SelectMulti(Array.Empty<object>()))
        {
          Dictionary<int, string> cds = this.CDs;
          int? ledgerId = ledger.LedgerID;
          int key = ledgerId.Value;
          string ledgerCd1 = ledger.LedgerCD;
          cds.Add(key, ledgerCd1);
          Dictionary<string, int> ids = this.IDs;
          string ledgerCd2 = ledger.LedgerCD;
          ledgerId = ledger.LedgerID;
          int num = ledgerId.Value;
          ids.Add(ledgerCd2, num);
          this.isActiveFlags.Add(ledger.LedgerID.Value, ledger.BalanceType == "B");
        }
      }
    }
  }

  protected struct BudgetKey
  {
    public int? BranchID { get; }

    public int? LedgerID { get; }

    public string FinYear { get; }

    public BudgetKey(GLBudgetEntry.GLBudgetLineKey lineKey)
    {
      this.BranchID = new int?(lineKey.BranchID);
      this.LedgerID = new int?(lineKey.LedgerID);
      this.FinYear = lineKey.FinYear;
    }

    public BudgetKey(BudgetFilter filter)
    {
      this.BranchID = filter.BranchID;
      this.LedgerID = filter.LedgerID;
      this.FinYear = filter.FinYear;
    }

    public PXResultset<GLBudgetLine> SelectLines(PXGraph graph)
    {
      return PXSelectBase<GLBudgetLine, PXSelect<GLBudgetLine, Where<GLBudgetLine.branchID, Equal<Required<BudgetFilter.branchID>>, And<GLBudgetLine.ledgerID, Equal<Required<BudgetFilter.ledgerID>>, And<GLBudgetLine.finYear, Equal<Required<BudgetFilter.finYear>>, And<GLBudgetLine.isGroup, Equal<False>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) this.BranchID,
        (object) this.LedgerID,
        (object) this.FinYear
      });
    }
  }

  protected struct GLBudgetLineKey
  {
    public int BranchID;
    public int LedgerID;
    public int AccountID;
    public int SubID;
    public string FinYear;

    public static GLBudgetEntry.GLBudgetLineKey Create(GLBudgetLine line)
    {
      return new GLBudgetEntry.GLBudgetLineKey()
      {
        BranchID = line.BranchID.GetValueOrDefault(),
        LedgerID = line.LedgerID.GetValueOrDefault(),
        FinYear = line.FinYear,
        AccountID = line.AccountID.GetValueOrDefault(),
        SubID = line.SubID.GetValueOrDefault()
      };
    }

    public static GLBudgetEntry.GLBudgetLineKey Create(IDictionary keys, GLBudgetEntry graph)
    {
      return new GLBudgetEntry.GLBudgetLineKey()
      {
        BranchID = GLBudgetEntry.GLBudgetLineKey.ExtractBranch(keys[(object) "BranchID"] as string, graph),
        LedgerID = GLBudgetEntry.GLBudgetLineKey.ExtractLedger(keys[(object) "LedgerID"] as string, graph),
        FinYear = GLBudgetEntry.GLBudgetLineKey.ExtractFinYear(keys[(object) "FinYear"] as string, graph),
        AccountID = GLBudgetEntry.GLBudgetLineKey.ExtractAccountID(keys[(object) "AccountID"] as string, graph.accountIndex),
        SubID = GLBudgetEntry.GLBudgetLineKey.ExtractSubID(keys[(object) "SubID"] as string, graph.subIndex)
      };
    }

    private static int ExtractAccountID(string value, GLBudgetEntry.AccountIndex accountIndex)
    {
      return string.IsNullOrEmpty(value) ? 0 : accountIndex.GetID(value).GetValueOrDefault();
    }

    private static int ExtractSubID(string value, GLBudgetEntry.SubIndex subIndex)
    {
      return subIndex.GetID(string.IsNullOrEmpty(value) ? subIndex.DefaultCD : value) ?? -1;
    }

    private static int ExtractBranch(string value, GLBudgetEntry graph)
    {
      return string.IsNullOrEmpty(value) ? ((PXSelectBase<BudgetFilter>) graph.Filter).Current.BranchID.GetValueOrDefault() : PXAccess.GetBranchID(value).GetValueOrDefault();
    }

    private static int ExtractLedger(string value, GLBudgetEntry graph)
    {
      return string.IsNullOrEmpty(value) ? ((PXSelectBase<BudgetFilter>) graph.Filter).Current.LedgerID.GetValueOrDefault() : graph.ledgerIndex.GetID(value).GetValueOrDefault();
    }

    private static string ExtractFinYear(string value, GLBudgetEntry graph)
    {
      return string.IsNullOrEmpty(value) ? ((PXSelectBase<BudgetFilter>) graph.Filter).Current.FinYear : value;
    }

    public bool Match(GLBudgetEntry.BudgetKey filter)
    {
      return this.BranchID.Equals((object) filter.BranchID) && this.LedgerID.Equals((object) filter.LedgerID) && this.FinYear.Equals(filter.FinYear);
    }

    public override bool Equals(object o)
    {
      return o is GLBudgetEntry.GLBudgetLineKey glBudgetLineKey && this.BranchID.Equals(glBudgetLineKey.BranchID) && this.LedgerID.Equals(glBudgetLineKey.LedgerID) && this.FinYear.Equals(glBudgetLineKey.FinYear) && this.AccountID.Equals(glBudgetLineKey.AccountID) && this.SubID.Equals(glBudgetLineKey.SubID);
    }

    public override int GetHashCode()
    {
      return ((((37 * 397 + this.BranchID.GetHashCode()) * 397 + this.LedgerID.GetHashCode()) * 397 + this.FinYear.GetHashCode()) * 397 + this.AccountID.GetHashCode()) * 397 + this.SubID.GetHashCode();
    }
  }
}
