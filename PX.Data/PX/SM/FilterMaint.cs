// Decompiled with JetBrains decompiler
// Type: PX.SM.FilterMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

public class FilterMaint : PXGraph<
#nullable disable
FilterMaint, FilterHeader>
{
  private const string _FILTERABLE_VIEWS_VIEW_NAME = "FilterableViewsSelectorView";
  public PXSelect<FilterHeader, Where<FilterHeader.isShared, Equal<PX.Data.True>, And<FilterHeader.isHidden, NotEqual<PX.Data.True>, And<FilterHeader.screenID, NotEqual<FilterHeader.selectorConst>>>>> Filter;
  public PXSelect<FilterRow, Where<FilterRow.filterID, Equal<Current<FilterHeader.filterID>>, And<Current<FilterHeader.isShared>, Equal<PX.Data.True>>>> FilterDetails;
  public PXAction<FilterHeader> MakeFilterNotShared;
  private readonly FiltersGraphHelper<FilterHeader> _filtersGraphHelper;
  public PXSelect<PX.SM.SiteMap> SiteMap;

  [PXButton]
  [PXUIField(DisplayName = "Make Filter Not Shared")]
  protected virtual void makeFilterNotShared()
  {
    if (!(this.Filter.Cache.Current is FilterHeader current) || this.Filter.Ask("Warning", "Do you want to make this filter not shared? You will not be able to manage it on this form.", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      return;
    bool isDirty = this.Filter.Cache.IsDirty;
    current.IsShared = new bool?(false);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.Filter.Cache.PersistUpdated((object) (this.Filter.Cache.Update((object) current) as FilterHeader));
      transactionScope.Complete();
    }
    this.Filter.Cache.IsDirty = isDirty;
  }

  public FilterMaint()
  {
    this.Filter.Cache.AutoSave = false;
    this.Views.Add("FilterableViewsSelectorView", new PXView((PXGraph) this, true, (BqlCommand) new PX.Data.Select<FilterMaint.ViewInfo>(), (Delegate) new PXSelectDelegate(this.GetViews)));
    this._filtersGraphHelper = new FiltersGraphHelper<FilterHeader>((PXGraph) this);
  }

  [PXDBSequentialGuid(IsKey = true)]
  [PXUIField(DisplayName = "Filter ID")]
  [FilterHeaderSelector]
  protected virtual void FilterHeader_FilterID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(Visible = false)]
  protected virtual void FilterHeader_UserName_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Screen ID")]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visibility", PXUIVisibility.SelectorVisible)]
  protected virtual void FilterHeader_ScreenID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(128 /*0x80*/, InputMask = "")]
  [PXUIField(DisplayName = "View", Visibility = PXUIVisibility.SelectorVisible)]
  protected virtual void FilterHeader_ViewName_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is Shared")]
  protected virtual void FilterHeader_IsShared_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is System", Enabled = false, Visible = false)]
  protected virtual void FilterHeader_IsSystem_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (FilterHeader.filterID))]
  [PXDBGuid(false, IsKey = true)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(Visible = false, Enabled = false)]
  protected virtual void FilterRow_FilterID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  protected virtual void FilterRow_IsUsed_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBByte]
  [PXUIField(DisplayName = "Condition")]
  protected virtual void FilterRow_Condition_CacheAttached(PXCache sender)
  {
  }

  protected virtual void FilterHeader_ViewName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, fieldName: "ViewName", displayName: "View", viewName: "FilterableViewsSelectorView");
  }

  protected virtual void FilterHeader_UserName_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((FilterHeader) e.Row).UserName = this.Accessinfo.UserName;
  }

  protected virtual void FilterHeader_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    FilterHeader row = e.Row as FilterHeader;
    FilterHeader newRow = e.NewRow as FilterHeader;
    if (row == null || newRow == null)
      return;
    PXSiteMapNode pxSiteMapNode = (PXSiteMapNode) null;
    if (newRow.ScreenID != null)
      pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNodeByScreenID(newRow.ScreenID);
    if (pxSiteMapNode == null)
    {
      cache.RaiseExceptionHandling<FilterHeader.screenID>(e.Row, (object) newRow.ScreenID, (Exception) FilterMaint.GetPropertyException("ScreenID"));
      newRow.ScreenID = (string) null;
    }
    if (pxSiteMapNode == null)
    {
      newRow.ViewName = (string) null;
    }
    else
    {
      string objA = (string) null;
      if (newRow.ViewName != null)
      {
        foreach (FilterMaint.ViewInfo view in FiltersGraphHelper<FilterHeader>.GetViews(pxSiteMapNode.GraphType))
        {
          if (newRow.ViewName.Equals(view.Name))
          {
            objA = view.Name;
            break;
          }
        }
      }
      if (object.Equals((object) objA, (object) newRow.ViewName))
        return;
      cache.RaiseExceptionHandling<FilterHeader.viewName>(e.Row, (object) newRow.ViewName, (Exception) FilterMaint.GetPropertyException("ViewName"));
      newRow.ViewName = (string) null;
    }
  }

  protected virtual void FilterHeader_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is FilterHeader row))
      return;
    PXEntryStatus status = sender.GetStatus((object) row);
    this.MakeFilterNotShared.SetVisible(status != PXEntryStatus.Inserted && status != PXEntryStatus.InsertedDeleted);
  }

  protected virtual void FilterHeader_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    FilterHeader row1 = e.Row as FilterHeader;
    FilterHeader oldRow = e.OldRow as FilterHeader;
    if (row1 == null || oldRow == null || object.Equals((object) row1.ScreenID, (object) oldRow.ScreenID) && object.Equals((object) row1.ViewName, (object) oldRow.ViewName))
      return;
    this._filtersGraphHelper.InvalidateCache();
    foreach (PXResult<FilterRow> pxResult in this.FilterDetails.Select())
    {
      FilterRow row2 = (FilterRow) pxResult;
      if (!this._filtersGraphHelper.CheckProperty((object) row2.DataField, this.Filter.Current))
      {
        row2.DataField = (string) null;
        row2.Condition = new byte?();
        this.FilterDetails.Cache.MarkUpdated((object) row2);
      }
    }
  }

  protected virtual void FilterHeader_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is FilterHeader row))
      return;
    bool? isDefault = row.IsDefault;
    bool flag = true;
    if (!(isDefault.GetValueOrDefault() == flag & isDefault.HasValue))
      return;
    PXCache cach = this.Caches[typeof (FilterMaint.FilterHeaderForDelete)];
    cach.Clear();
    foreach (PXResult<FilterHeader> pxResult in PXSelectBase<FilterHeader, PXSelect<FilterHeader, Where<FilterHeader.isDefault, Equal<PX.Data.True>, And<FilterHeader.screenID, Equal<Required<FilterHeader.screenID>>, And<FilterHeader.viewName, Equal<Required<FilterHeader.viewName>>, And<FilterHeader.filterID, NotEqual<Required<FilterHeader.filterID>>>>>>>.Config>.Select((PXGraph) this, (object) row.ScreenID, (object) row.ViewName, (object) row.FilterID))
    {
      PXCache pxCache = cach;
      FilterMaint.FilterHeaderForDelete filterHeaderForDelete = new FilterMaint.FilterHeaderForDelete();
      filterHeaderForDelete.FilterID = ((FilterHeader) pxResult).FilterID;
      pxCache.Delete((object) filterHeaderForDelete);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.Persist(typeof (FilterMaint.FilterHeaderForDelete), PXDBOperation.Delete);
      transactionScope.Complete();
    }
  }

  protected virtual void FilterRow_DataField_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), "DataField", new bool?(), new int?(), (string) null, this._filtersGraphHelper.GetPropertyNames(this.Filter.Current), this._filtersGraphHelper.GetPropertyLabels(this.Filter.Current), new bool?(true), (string) null);
  }

  protected virtual void FilterRow_DataField_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || this._filtersGraphHelper.CheckProperty(e.NewValue, this.Filter.Current))
      return;
    cache.RaiseExceptionHandling<FilterRow.dataField>(e.Row, e.NewValue, (Exception) FilterMaint.GetPropertyException("DataField"));
    e.NewValue = (object) null;
  }

  protected virtual void FilterRow_OpenBrackets_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    FilterRow row = e.Row as FilterRow;
    if (!this.IsImport || row == null)
      return;
    int? nullable = row.OpenBrackets;
    if (!nullable.HasValue && FilterRow.OpenBracketsAttribute.Values != null)
      row.OpenBrackets = new int?(((IEnumerable<int>) FilterRow.OpenBracketsAttribute.Values).First<int>());
    nullable = row.CloseBrackets;
    if (nullable.HasValue || FilterRow.CloseBracketsAttribute.Values == null)
      return;
    row.CloseBrackets = new int?(((IEnumerable<int>) FilterRow.CloseBracketsAttribute.Values).First<int>());
  }

  protected virtual void FilterRow_ValueSt_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.Values_FieldSelecting_Handler(e, "ValueSt", false);
  }

  protected virtual void FilterRow_ValueSt2_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.Values_FieldSelecting_Handler(e, "ValueSt2", true);
  }

  protected virtual void FilterRow_Condition_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    string[] labels = (string[]) null;
    int[] values = (int[]) null;
    if (e.Row is FilterRow row)
      this._filtersGraphHelper.GetConditions(row.DataField, out values, out labels, this.Filter.Current);
    string[] array = values != null ? ((IEnumerable<int>) values).Select<int, string>((Func<int, string>) (i => i.ToString())).ToArray<string>() : (string[]) null;
    string[] allowedLabels = FiltersGraphHelper<FilterHeader>.LocalizeFilterConditionLabels(labels);
    object obj = e.ReturnState ?? (object) (byte?) row?.Condition;
    e.ReturnState = (object) PXStringState.CreateInstance(obj, new int?(), new bool?(), "condition", new bool?(false), new int?(), (string) null, array, allowedLabels, new bool?(), (string) null);
  }

  protected virtual void FilterRow_Condition_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is FilterRow row) || e.NewValue == null || this._filtersGraphHelper.CheckCondition(e.NewValue, row.DataField, this.Filter.Current))
      return;
    e.NewValue = (object) null;
  }

  protected virtual void FilterRow_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    FilterRow row = e.Row as FilterRow;
    FilterRow oldRow = e.OldRow as FilterRow;
    if (row == null || oldRow == null)
      return;
    if (object.Equals((object) row.DataField, (object) oldRow.DataField))
    {
      byte? condition = row.Condition;
      if (FiltersGraphHelper<FilterHeader>.IsConditionWithValue(condition.HasValue ? new int?((int) condition.GetValueOrDefault()) : new int?()))
      {
        condition = row.Condition;
        if (FiltersGraphHelper<FilterHeader>.IsConditionWithTwoValue(condition.HasValue ? new int?((int) condition.GetValueOrDefault()) : new int?()))
          return;
        row.ValueSt2 = (string) null;
        return;
      }
    }
    row.ValueSt = (string) null;
    row.ValueSt2 = (string) null;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen Title")]
  protected virtual void SiteMap_Title_CacheAttached(PXCache sender)
  {
  }

  private void Values_FieldSelecting_Handler(
    PXFieldSelectingEventArgs e,
    string fieldName,
    bool secondValue)
  {
    if (!(e.Row is FilterRow row) || string.IsNullOrEmpty(row.DataField))
      return;
    bool flag = true;
    byte? condition = row.Condition;
    if (FiltersGraphHelper<FilterHeader>.IsConditionWithValue(condition.HasValue ? new int?((int) condition.GetValueOrDefault()) : new int?()))
    {
      if (secondValue)
      {
        condition = row.Condition;
        if (FiltersGraphHelper<FilterHeader>.IsConditionWithTwoValue(condition.HasValue ? new int?((int) condition.GetValueOrDefault()) : new int?()))
          goto label_6;
      }
      else
        goto label_6;
    }
    flag = false;
    e.ReturnValue = (object) null;
label_6:
    int index = Array.IndexOf<string>(this._filtersGraphHelper.GetPropertyNames(this.Filter.Current), row.DataField);
    if (index <= -1)
      return;
    PXFieldState instance = this._filtersGraphHelper.GetPropertyStates(this.Filter.Current)[index].CreateInstance((System.Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, fieldName, (string) null, (string) null, (string) null, PXErrorLevel.Undefined, new bool?(flag), new bool?(), new bool?(false), PXUIVisibility.Undefined, (string) null, (string[]) null, (string[]) null);
    instance.Value = e.ReturnValue;
    e.ReturnState = (object) instance;
  }

  private static PXSetPropertyException GetPropertyException(string field)
  {
    return new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
    {
      (object) $"[{field}]"
    });
  }

  private IEnumerable GetViews()
  {
    if (this.Filter.Current != null && !string.IsNullOrEmpty(this.Filter.Current.ScreenID))
    {
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.Filter.Current.ScreenID);
      if (mapNodeByScreenId != null)
      {
        foreach (object view in FiltersGraphHelper<FilterHeader>.GetViews(mapNodeByScreenId.GraphType))
          yield return view;
      }
    }
  }

  [Serializable]
  public class FilterHeaderForDelete : FilterHeader
  {
    public new abstract class filterID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      FilterMaint.FilterHeaderForDelete.filterID>
    {
    }
  }

  [Serializable]
  public class ViewInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Name;
    protected string _DisplayName;

    [PXString(IsKey = true)]
    [PXUIField(Visible = false)]
    public virtual string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Name")]
    public virtual string DisplayName
    {
      get => this._DisplayName;
      set => this._DisplayName = value;
    }

    public abstract class name : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FilterMaint.ViewInfo.name>
    {
    }

    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FilterMaint.ViewInfo.displayName>
    {
    }
  }
}
