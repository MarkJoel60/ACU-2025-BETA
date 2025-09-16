// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumnSetMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Localization;
using PX.Reports.ARm.Data;
using PX.Reports.ARm.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.CS;

[Serializable]
public class RMColumnSetMaint : PXGraph<
#nullable disable
RMColumnSetMaint, RMColumnSet>
{
  private const string _columnCodeAValue = "  A";
  private const short _rowSetFakeHeaderId = 32766;
  public PXSelect<RMColumnSet> ColumnSet;
  public PXSelect<RMColumnSet> ColumnSetOrdered;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>> StyleByID;
  public PXSelect<RMColumnSetMaint.RMHeaderStyle, Where<RMColumnSetMaint.RMHeaderStyle.styleID, Equal<Current<RMColumnHeader.styleID>>>> CurrentHeaderStyle;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Current<RMColumn.styleID>>>> CurrentColumnStyle;
  public PXSelect<RMColumnSetMaint.RMHeaderStyle> AllHeaderStyles;
  public PXSelect<RMStyle> AllColumnStyles;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<RMColumnSetMaint.ColumnCodeA>>>> Headers;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<RMColumnSetMaint.ColumnCodeA>>>> HeadersWithRowSet;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Current<RMColumn.columnCode>>>>> HeadersByCode;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>> Header;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Current<RMColumnSetMaint.ParamFilter.headerCell>>, And<RMColumnHeader.headerNbr, Equal<Current<RMColumnSetMaint.ParamFilter.headerNbr>>>>>> CurrentHeader;
  public PXSelectOrderBy<RMColumnSetMaint.ColumnProperty, OrderBy<Asc<RMColumnSetMaint.ColumnProperty.order>>> Properties;
  public PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>> ColumnsByCode;
  public PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>> Column;
  public PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>> AllColumns;
  public PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Current<RMColumnSetMaint.ParamFilter.columnCell>>>>> CurrentColumn;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>> DataSourceByID;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Current<RMColumn.dataSourceID>>>> CurrentDataSource;
  public PXSelectJoin<RMDataSource, InnerJoin<RMColumn, On<RMColumn.dataSourceID, Equal<RMDataSource.dataSourceID>>>, Where<RMColumn.columnSetCode, Equal<Current<RMColumn.columnSetCode>>>> DataSources;
  public PXSelectJoin<RMReport, LeftJoin<RMColumnSet, On<RMReport.columnSetCode, Equal<RMColumnSet.columnSetCode>>>, Where<RMColumnSet.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>> Reports;
  public PXSelect<RMReport, Where<RMReport.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>, OrderBy<Desc<RMReport.lastModifiedDateTime>>> LastLinkedReport;
  public PXSelect<RMReport, Where<RMReport.type, Equal<Current<RMColumnSet.type>>>, OrderBy<Desc<RMReport.lastModifiedDateTime>>> LastReport;
  public PXSelect<RMColumnSetMaint.RMHeaderStyle> HeaderStyle;
  public PXFilter<RMColumnSetMaint.ParamFilter> Parameter;
  public PXFilter<RMNewColumnSetPanel> NewColumnSetPanel;
  public PXAction<RMColumnSet> copyColumnSet;
  public PXAction<RMColumnSet> insertColumn;
  public PXAction<RMColumnSet> deleteHeader;
  public PXAction<RMColumnSet> deleteColumn;
  public PXAction<RMColumnSet> insertHeader;
  public PXAction<RMColumnSet> toLeftHeader;
  public PXAction<RMColumnSet> toRightHeader;
  public PXAction<RMColumnSet> toLeftColumn;
  public PXAction<RMColumnSet> toRightColumn;
  public PXAction<RMColumnSet> shiftLeft;
  public PXAction<RMColumnSet> shiftRight;
  public PXAction<RMColumnSet> toUpHeader;
  public PXAction<RMColumnSet> toDownHeader;
  public PXAction<RMColumnSet> toBufferHeader;
  public PXAction<RMColumnSet> toStyleHeader;
  public PXAction<RMColumnSet> toBufferColumn;
  public PXAction<RMColumnSet> toStyleColumn;
  public PXAction<RMColumnSet> moveColumn;
  public PXAction<RMColumnSet> moveHeader;
  public PXAction<RMColumnSet> pasteHeader;
  public PXAction<RMColumnSet> copyFormatting;
  public PXAction<RMColumnSet> pasteFormatting;
  public PXAction<RMColumnSet> copyColumnFormatting;
  public PXAction<RMColumnSet> pasteColumnFormatting;
  public PXAction<RMColumnSet> resetFormatting;
  public string LastCodeOnLoad;
  private bool isCopying;
  protected bool _BypassInsert;
  protected bool _BypassDelete;
  protected bool _ColumnDeleting;
  private static readonly object _lockObj = new object();
  public PXAction<RMColumnSet> CurrentHeaderChanged;

  [InjectDependency]
  private ILocalizableFieldService LocalizableFieldService { get; set; }

  [InjectDependency]
  private ISiteMapUITypeProvider SiteMapUITypeProvider { get; set; }

  public RMColumnSetMaint()
  {
    this.LastCodeOnLoad = this.ColumnSet.Current == null ? "  @" : this.ColumnSet.Current.LastColumn;
    this.Properties.Cache.AllowDelete = false;
    this.Properties.Cache.AllowInsert = false;
    this.Headers.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetVisible<RMColumn.columnCode>(this.ColumnsByCode.Cache, (object) null, false);
    if (this.Parameter.Current.ColumnCell != null)
      return;
    this.Parameter.Current.ColumnCell = "  A";
    this.Parameter.Current.HeaderCell = "  A";
    this.Parameter.Current.HeaderNbr = new short?((short) 1);
  }

  private bool IsNewUi()
  {
    return string.Equals(this.SiteMapUITypeProvider.GetUIByScreenId(PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(typeof (RMColumnSetMaint).FullName).ScreenID), "T", StringComparison.InvariantCultureIgnoreCase);
  }

  protected virtual IEnumerable properties()
  {
    bool found = false;
    foreach (RMColumnSetMaint.ColumnProperty columnProperty in this.Properties.Cache.Inserted)
    {
      found = true;
      yield return (object) columnProperty;
    }
    if (!found && this.ColumnSet.Current != null && !string.IsNullOrEmpty(this.ColumnSet.Current.LastColumn) && this.ColumnSet.Current.LastColumn != "  @")
    {
      short i = 0;
      foreach (string field in (List<string>) this.ColumnsByCode.Cache.Fields)
      {
        if (!field.StartsWith("CreatedByID", StringComparison.OrdinalIgnoreCase) && !field.StartsWith("Note", StringComparison.OrdinalIgnoreCase) && !field.StartsWith("LastModifiedByID", StringComparison.OrdinalIgnoreCase) && this.ColumnsByCode.Cache.GetStateExt((object) null, field) is PXFieldState stateExt && stateExt.Visible)
          yield return (object) this.Properties.Insert(new RMColumnSetMaint.ColumnProperty()
          {
            Name = stateExt.Name,
            DisplayName = stateExt.DisplayName,
            Order = new short?(i++)
          });
      }
      this.Properties.Cache.IsDirty = false;
    }
  }

  protected virtual IEnumerable headerstyle([PXInt] int? StyleID)
  {
    RMColumnSetMaint graph = this;
    RMColumnSetMaint.RMHeaderStyle rmHeaderStyle = (RMColumnSetMaint.RMHeaderStyle) PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelect<RMColumnSetMaint.RMHeaderStyle, Where<RMColumnSetMaint.RMHeaderStyle.styleID, Equal<Required<RMColumnSetMaint.RMHeaderStyle.styleID>>>>.Config>.Select((PXGraph) graph, (object) StyleID);
    if (rmHeaderStyle == null && StyleID.HasValue)
    {
      int? nullable = StyleID;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        rmHeaderStyle = new RMColumnSetMaint.RMHeaderStyle();
        using (new ReadOnlyScope(new PXCache[1]
        {
          graph.HeaderStyle.Cache
        }))
          rmHeaderStyle = graph.HeaderStyle.Insert(rmHeaderStyle);
      }
    }
    if (rmHeaderStyle != null)
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.styleID, Equal<Required<RMColumnHeader.styleID>>>>>.Config>.Select((PXGraph) graph, (object) StyleID);
      if (rmColumnHeader != null)
      {
        rmHeaderStyle.StartColumn = rmColumnHeader.StartColumn;
        rmHeaderStyle.EndColumn = rmColumnHeader.EndColumn;
      }
      yield return (object) rmHeaderStyle;
    }
  }

  public virtual IEnumerable columnSetOrdered()
  {
    if (PXView.Searches?[0] == null)
      return (IEnumerable) null;
    PXView pxView = new PXView((PXGraph) this, true, this.ColumnSetOrdered.View.BqlSelect);
    int num1 = 0;
    int num2 = 0;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    RMColumnSet rmColumnSet = pxView.Select(currents, parameters, searches, sortColumns, descendings, filters, ref local1, 1, ref local2).RowCast<RMColumnSet>().FirstOrDefault<RMColumnSet>();
    if (rmColumnSet == null || string.IsNullOrEmpty(rmColumnSet?.ColumnSetCode))
      return (IEnumerable) null;
    if (rmColumnSet?.ColumnSetCode == this.ColumnSetOrdered.Current?.ColumnSetCode)
      return (IEnumerable) new RMColumnSet[1]
      {
        this.ColumnSetOrdered.Current
      };
    this.ColumnSetOrdered.Current = rmColumnSet;
    this.DataSources.View.SelectMultiBound((object[]) null, (object[]) null);
    return (IEnumerable) new RMColumnSet[1]
    {
      this.ColumnSetOrdered.Current
    };
  }

  protected ARmReportNode GetReportNode()
  {
    string key = "0_DATAGRAPH_" + typeof (RMColumnSetMaint).FullName;
    lock (RMColumnSetMaint._lockObj)
    {
      ARmReportNode slot = PXContext.GetSlot<ARmReportNode>(key);
      if (slot != null)
        return slot;
      RMReport rmReport = this.LastLinkedReport.SelectSingle() ?? this.LastReport.SelectSingle();
      if (rmReport == null)
        return (ARmReportNode) null;
      RMReportReader instance = PXGraph.CreateInstance<RMReportReader>();
      instance.ReportCode = rmReport.ReportCode;
      ARmReportNode reportNode = new ARmReportNode((IARmDataSource) instance, instance.GetReport());
      PXContext.SetSlot<ARmReportNode>(key, reportNode);
      return reportNode;
    }
  }

  protected virtual IEnumerable headersWithRowSet()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = true
    };
    IEnumerable<RMColumnHeader> collection = (IEnumerable<RMColumnHeader>) ((object) this.Headers.View.SelectMultiBound((object[]) null, (object[]) null).RowCast<RMColumnHeader>() ?? (object) Array.Empty<RMColumnHeader>());
    foreach (RMColumnHeader rmColumnHeader in collection)
    {
      rmColumnHeader.SectionType = PX.SM.Messages.GetLocal("Header");
      rmColumnHeader.IsRowSet = new bool?(false);
    }
    pxDelegateResult.AddRange((IEnumerable<object>) collection);
    pxDelegateResult.Add((object) new RMColumnHeader()
    {
      ColumnCode = "  A",
      HeaderNbr = new short?((short) 32766),
      ColumnSetCode = this.ColumnSet.Current.ColumnSetCode,
      IsRowSet = new bool?(true),
      SectionType = PX.SM.Messages.GetLocal("Row Set")
    });
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable allHeaderStyles()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultTruncated = true;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.AddRange((IEnumerable<object>) ((object) PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelectJoin<RMColumnSetMaint.RMHeaderStyle, InnerJoin<RMColumnHeader, On<RMColumnHeader.styleID, Equal<RMColumnSetMaint.RMHeaderStyle.styleID>>>, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, (object[]) null).RowCast<RMColumnSetMaint.RMHeaderStyle>() ?? (object) Array.Empty<RMColumnSetMaint.RMHeaderStyle>()));
    pxDelegateResult.AddRange((IEnumerable<object>) this.HeaderStyle.Cache.Cached.RowCast<RMColumnSetMaint.RMHeaderStyle>().Where<RMColumnSetMaint.RMHeaderStyle>((Func<RMColumnSetMaint.RMHeaderStyle, bool>) (x =>
    {
      if (!EnumerableExtensions.IsNotIn<PXEntryStatus>(this.HeaderStyle.Cache.GetStatus((object) x), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return false;
      int? styleId = x.StyleID;
      int num = 0;
      return styleId.GetValueOrDefault() < num & styleId.HasValue;
    })));
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable allColumnStyles()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    IEnumerable<RMStyle> collection1 = (IEnumerable<RMStyle>) ((object) PXSelectBase<RMStyle, PXSelectJoin<RMStyle, InnerJoin<RMColumn, On<RMColumn.styleID, Equal<RMStyle.styleID>>>, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>>>.Config>.Select((PXGraph) this, new object[2]).RowCast<RMStyle>() ?? (object) Array.Empty<RMStyle>());
    pxDelegateResult.AddRange((IEnumerable<object>) collection1);
    IEnumerable<RMStyle> collection2 = this.AllColumnStyles.Cache.Cached.RowCast<RMStyle>().Where<RMStyle>((Func<RMStyle, bool>) (x =>
    {
      if (!EnumerableExtensions.IsNotIn<PXEntryStatus>(this.AllColumnStyles.Cache.GetStatus((object) x), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return false;
      int? styleId = x.StyleID;
      int num = 0;
      return styleId.GetValueOrDefault() < num & styleId.HasValue;
    }));
    pxDelegateResult.AddRange((IEnumerable<object>) collection2);
    return (IEnumerable) pxDelegateResult;
  }

  [PXInsertButton(Tooltip = "Add New Record (Ctrl+Ins)")]
  [PXUIField(DisplayName = "Add New Record (Ctrl+Ins)")]
  protected virtual IEnumerable Insert(PXAdapter adapter)
  {
    if (this.IsNewUi())
    {
      if (this.NewColumnSetPanel.AskExt() != WebDialogResult.OK)
        return adapter.Get();
      RMMaintHelper.CheckKeyAndDescription<RMColumnSet, RMColumnSet.columnSetCode, RMNewColumnSetPanel.columnSetCode, RMNewColumnSetPanel.description>((PXGraph) this, this.NewColumnSetPanel.Cache, (object) this.NewColumnSetPanel.Current);
      RMColumnSetMaint instance = PXGraph.CreateInstance<RMColumnSetMaint>();
      RMColumnSet data = instance.ColumnSetOrdered.Insert();
      string columnSetCode = this.NewColumnSetPanel.Current.ColumnSetCode;
      data.ColumnSetCode = columnSetCode;
      data.Type = this.NewColumnSetPanel.Current.Type;
      data.Description = this.NewColumnSetPanel.Current.Description;
      instance.Headers.Cache.SetValue((object) (NonGenericIEnumerableExtensions.FirstOrDefault_(instance.Headers.Cache.Inserted) as RMColumnHeader), "columnSetCode", (object) columnSetCode);
      instance.Column.Cache.SetValue((object) (NonGenericIEnumerableExtensions.FirstOrDefault_(instance.Column.Cache.Inserted) as RMColumn), "columnSetCode", (object) columnSetCode);
      instance.ColumnSetOrdered.Current = data;
      instance.ColumnSetOrdered.Cache.PlaceNotChanged((object) data);
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
    }
    else
      PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<RMColumnSetMaint>(), PXRedirectHelper.WindowMode.InlineWindow);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Copy Column Set", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(ConfirmationMessage = "Any unsaved changes will be discarded.", ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable CopyColumnSet(PXAdapter adapter)
  {
    RMColumnSetMaint graph = this;
    if (graph.Parameter.View.Answer == WebDialogResult.None)
      graph.Parameter.Current.Description = graph.ColumnSet.Current.Description;
    if (graph.ColumnSet.Current != null && graph.Parameter.AskExt() == WebDialogResult.OK)
    {
      RMMaintHelper.CheckKeyAndDescription<RMColumnSet, RMColumnSet.columnSetCode, RMColumnSetMaint.ParamFilter.newColumnSetCode, RMColumnSetMaint.ParamFilter.description>((PXGraph) graph, graph.Parameter.Cache, (object) graph.Parameter.Current);
      string newColumnSetCode = graph.Parameter.Current.NewColumnSetCode;
      string description = graph.Parameter.Current.Description;
      foreach (RMColumnSet rmColumnSet in graph.Cancel.Press(adapter))
        ;
      string columnSetCode = graph.ColumnSet.Current.ColumnSetCode;
      List<PXResult<RMColumn>> list = graph.ColumnsByCode.Select().ToList<PXResult<RMColumn>>();
      RMColumnSet copy1 = (RMColumnSet) graph.ColumnSet.Cache.CreateCopy((object) graph.ColumnSet.Current);
      copy1.ColumnSetCode = newColumnSetCode;
      if (!string.IsNullOrEmpty(description))
        copy1.Description = description;
      copy1.NoteID = new Guid?();
      copy1.LastColumn = (string) null;
      graph.isCopying = true;
      RMColumnSet rmColumnSet1 = graph.ColumnSet.Insert(copy1);
      graph.isCopying = false;
      if (rmColumnSet1 != null)
      {
        bool flag1 = graph.LocalizableFieldService.IsFieldEnabled("RMColumn", "Formula");
        bool flag2 = graph.LocalizableFieldService.IsFieldEnabled("RMColumn", "Description");
        string fieldName1 = "FormulaTranslations";
        string fieldName2 = "DescriptionTranslations";
        foreach (PXResult<RMColumn> pxResult in list)
        {
          RMColumn row1 = (RMColumn) pxResult;
          string[] strArray1 = (string[]) null;
          string[] strArray2 = (string[]) null;
          if (flag1)
            strArray1 = graph.ColumnsByCode.GetValueExt((object) row1, fieldName1) as string[];
          if (flag2)
            strArray2 = graph.ColumnsByCode.GetValueExt((object) row1, fieldName2) as string[];
          row1.ColumnSetCode = (string) null;
          row1.NoteID = new Guid?();
          RMColumn row2 = graph.ColumnsByCode.Insert(row1);
          if (row2 != null)
          {
            if (strArray1 != null)
              graph.ColumnsByCode.SetValueExt((object) row2, fieldName1, (object) strArray1);
            if (strArray2 != null)
              graph.ColumnsByCode.SetValueExt((object) row2, fieldName2, (object) strArray2);
            RMDataSource copy2 = (RMDataSource) graph.DataSourceByID.Select((object) row1.DataSourceID);
            if (copy2 != null && graph.DataSourceByID.Current != null)
            {
              graph.DataSourceByID.Cache.RestoreCopy((object) graph.DataSourceByID.Current, (object) copy2);
              graph.DataSourceByID.Current.DataSourceID = row2.DataSourceID;
            }
            RMStyle copy3 = (RMStyle) graph.StyleByID.Select((object) row1.StyleID);
            if (copy3 != null && graph.StyleByID.Current != null)
            {
              graph.StyleByID.Cache.RestoreCopy((object) graph.StyleByID.Current, (object) copy3);
              graph.StyleByID.Current.StyleID = row2.StyleID;
            }
          }
        }
        bool flag3 = graph.LocalizableFieldService.IsFieldEnabled("RMColumnHeader", "Formula");
        foreach (PXResult<RMColumnHeader> pxResult in PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Required<RMColumnHeader.columnSetCode>>>>.Config>.Select((PXGraph) graph, (object) columnSetCode))
        {
          RMColumnHeader row3 = (RMColumnHeader) pxResult;
          string[] strArray = (string[]) null;
          if (flag3)
            strArray = graph.Headers.GetValueExt((object) row3, "FormulaTranslations") as string[];
          row3.ColumnSetCode = (string) null;
          row3.NoteID = new Guid?();
          graph._BypassInsert = true;
          RMColumnHeader row4 = graph.Headers.Insert(row3);
          graph._BypassInsert = false;
          if (row4 != null)
          {
            if (strArray != null)
              graph.Headers.SetValueExt((object) row4, "FormulaTranslations", (object) strArray);
            RMColumnSetMaint.RMHeaderStyle rmHeaderStyle1 = (RMColumnSetMaint.RMHeaderStyle) PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelect<RMColumnSetMaint.RMHeaderStyle, Where<RMColumnSetMaint.RMHeaderStyle.styleID, Equal<Required<RMColumnSetMaint.RMHeaderStyle.styleID>>>>.Config>.Select((PXGraph) graph, (object) row3.StyleID);
            if (rmHeaderStyle1 != null)
            {
              rmHeaderStyle1.StyleID = new int?();
              RMColumnSetMaint.RMHeaderStyle rmHeaderStyle2 = graph.HeaderStyle.Insert(rmHeaderStyle1);
              row4.StyleID = rmHeaderStyle2 == null ? new int?() : rmHeaderStyle2.StyleID;
            }
            else
              row4.StyleID = new int?();
          }
        }
        yield return (object) rmColumnSet1;
        yield break;
      }
    }
    foreach (RMColumnSet rmColumnSet in adapter.Get())
      yield return (object) rmColumnSet;
  }

  [PXButton(Tooltip = "New")]
  [PXUIField]
  protected virtual IEnumerable InsertHeader(PXAdapter adapter)
  {
    this.Headers.Insert(new RMColumnHeader()
    {
      StartColumn = "  A",
      EndColumn = "  A"
    });
    this.Headers.View.RequestRefresh();
    return adapter.Get();
  }

  [PXButton(Tooltip = "New")]
  [PXUIField]
  protected virtual IEnumerable InsertColumn(PXAdapter adapter)
  {
    RMColumn rmColumn = this.ColumnsByCode.Insert(new RMColumn());
    this._BypassInsert = true;
    foreach (PXResult<RMColumnHeader> pxResult in this.Headers.Select())
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
      this.Headers.Insert(new RMColumnHeader()
      {
        ColumnCode = rmColumn.ColumnCode,
        StartColumn = rmColumn.ColumnCode,
        EndColumn = rmColumn.ColumnCode,
        HeaderNbr = rmColumnHeader.HeaderNbr,
        Height = rmColumnHeader.Height,
        GroupID = rmColumnHeader.GroupID
      });
    }
    this._BypassInsert = false;
    this.TryAddHandlers(rmColumn.ColumnCode);
    this.Headers.View.RequestRefresh();
    this.Properties.View.RequestRefresh();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Delete")]
  [PXUIField(DisplayName = "", Visible = false)]
  protected virtual IEnumerable DeleteHeader(PXAdapter adapter)
  {
    if (this.Parameter.Current.HeaderNbr.HasValue)
    {
      short? headerNbr = this.Parameter.Current.HeaderNbr;
      int? nullable = headerNbr.HasValue ? new int?((int) headerNbr.GetValueOrDefault()) : new int?();
      int num = 32766;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        this.Headers.Cache.Delete((object) this.CurrentHeader.SelectSingle());
        this.Headers.View.RequestRefresh();
        return adapter.Get();
      }
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Delete")]
  [PXUIField]
  protected virtual IEnumerable DeleteColumn(PXAdapter adapter)
  {
    if (string.IsNullOrEmpty(this.Parameter.Current.ColumnCell))
      return adapter.Get();
    RMColumn data1 = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.ColumnCell);
    RMColumn rmColumn1 = (RMColumn) this.ColumnsByCode.SelectWindowed(-1, 1);
    if (data1 != null && data1.ColumnCode != "  A")
    {
      for (string code = data1.ColumnCode; code != rmColumn1.ColumnCode; code = RMColumnCodeAttribute.ShiftCode(code, true))
      {
        List<RMColumnHeader> rmColumnHeaderList1 = new List<RMColumnHeader>();
        List<RMColumnHeader> rmColumnHeaderList2 = new List<RMColumnHeader>();
        PXSelectBase<RMColumnHeader> pxSelectBase = (PXSelectBase<RMColumnHeader>) new PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>((PXGraph) this);
        foreach (PXResult<RMColumnHeader> pxResult in pxSelectBase.Select((object) code))
        {
          RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
          rmColumnHeaderList1.Add(rmColumnHeader);
        }
        foreach (PXResult<RMColumnHeader> pxResult in pxSelectBase.Select((object) RMColumnCodeAttribute.ShiftCode(code, true)))
        {
          RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
          rmColumnHeaderList2.Add(rmColumnHeader);
        }
        for (int index1 = 0; index1 < rmColumnHeaderList1.Count && index1 < rmColumnHeaderList2.Count; ++index1)
        {
          for (int index2 = 0; index2 < this.Headers.Cache.BqlFields.Count; ++index2)
          {
            if (!this.Headers.Cache.Keys.Contains(this.Headers.Cache.Fields[index2]))
            {
              object obj = this.Headers.Cache.GetValue((object) rmColumnHeaderList2[index1], index2);
              this.Headers.Cache.SetValue((object) rmColumnHeaderList1[index1], index2, obj);
              if (this.Headers.Cache.GetStatus((object) rmColumnHeaderList1[index1]) != PXEntryStatus.Inserted)
                this.Headers.Cache.SetStatus((object) rmColumnHeaderList1[index1], PXEntryStatus.Updated);
            }
          }
        }
      }
      string code1 = data1.ColumnCode;
      RMColumn data2;
      for (; data1 != rmColumn1; data1 = data2)
      {
        code1 = RMColumnCodeAttribute.ShiftCode(code1, true);
        data2 = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) code1);
        if (data2 != null)
        {
          for (int index = 0; index < this.ColumnsByCode.Cache.Fields.Count; ++index)
          {
            if (!this.ColumnsByCode.Cache.Keys.Contains(this.ColumnsByCode.Cache.Fields[index]))
            {
              object obj = this.ColumnsByCode.Cache.GetValue((object) data2, this.ColumnsByCode.Cache.Fields[index]);
              this.ColumnsByCode.Cache.SetValue((object) data1, this.ColumnsByCode.Cache.Fields[index], obj);
            }
          }
          if (this.ColumnsByCode.Cache.GetStatus((object) data1) != PXEntryStatus.Inserted)
            this.ColumnsByCode.Cache.SetStatus((object) data1, PXEntryStatus.Updated);
        }
        else
          break;
      }
      RMColumn rmColumn2 = data1;
      RMColumn rmColumn3 = data1;
      int? nullable1 = new int?();
      int? nullable2 = nullable1;
      rmColumn3.DataSourceID = nullable2;
      int? nullable3 = nullable1;
      rmColumn2.StyleID = nullable3;
      this.ColumnsByCode.Delete(data1);
      this.Properties.Cache.Fields.Remove(data1.ColumnCode);
      this.Headers.Cache.Fields.Remove(data1.ColumnCode);
      this._ColumnDeleting = true;
      foreach (PXResult<RMColumnHeader> pxResult in this.Headers.Select())
      {
        RMColumnHeader rmColumnHeader1 = (RMColumnHeader) pxResult;
        RMColumnHeader rmColumnHeader2 = (RMColumnHeader) this.Header.Select((object) data1.ColumnCode, (object) rmColumnHeader1.HeaderNbr);
        if (rmColumnHeader2 != null)
          this.Headers.Delete(rmColumnHeader2);
      }
      this._ColumnDeleting = false;
      this.Headers.View.RequestRefresh();
      this.Properties.View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Shift Left")]
  [PXUIField(DisplayName = "Shift Left")]
  protected virtual IEnumerable ToLeftHeader(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Parameter.Current.HeaderCell) && string.Compare(this.Parameter.Current.HeaderCell, "  A", StringComparison.OrdinalIgnoreCase) > 0 && this.Parameter.Current.HeaderActiveCellIndex > this.Parameter.Current.HeaderCellPredAIndex)
      this.ProcessToLeftHeader();
    return adapter.Get();
  }

  protected virtual void ProcessToLeftHeader()
  {
    PXResultset<RMColumnHeader> pxResultset1 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.HeaderCell);
    PXResultset<RMColumnHeader> pxResultset2 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>.Config>.Select((PXGraph) this, (object) RMColumnCodeAttribute.ShiftCode(this.Parameter.Current.HeaderCell, false));
    if (pxResultset1.Count <= 0 || pxResultset1.Count != pxResultset2.Count)
      return;
    for (int index = 0; index < pxResultset1.Count; ++index)
    {
      RMColumnSetMaint.ShiftColumnRange((RMColumnHeader) pxResultset1[index], (RMColumnHeader) pxResultset2[index]);
      RMColumnSetMaint.SwapItems<RMColumnHeader>(this.Headers.Cache, (RMColumnHeader) pxResultset1[index], (RMColumnHeader) pxResultset2[index], "ColumnCode");
    }
    this.Headers.Cache.IsDirty = true;
    this.Headers.View.RequestRefresh();
  }

  [PXButton(Tooltip = "Shift Right")]
  [PXUIField(DisplayName = "Shift Right")]
  protected virtual IEnumerable ToRightHeader(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Parameter.Current.HeaderCell) && this.ColumnSet.Current != null && !string.IsNullOrEmpty(this.ColumnSet.Current.LastColumn) && string.Compare(this.Parameter.Current.HeaderCell, this.ColumnSet.Current.LastColumn, StringComparison.OrdinalIgnoreCase) < 0 && this.Parameter.Current.HeaderActiveCellIndex > this.Parameter.Current.HeaderCellPredAIndex)
      this.ProcessToRightHeader();
    return adapter.Get();
  }

  protected virtual void ProcessToRightHeader()
  {
    PXResultset<RMColumnHeader> pxResultset1 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.HeaderCell);
    PXResultset<RMColumnHeader> pxResultset2 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>.Config>.Select((PXGraph) this, (object) RMColumnCodeAttribute.ShiftCode(this.Parameter.Current.HeaderCell, true));
    if (pxResultset1.Count <= 0 || pxResultset1.Count != pxResultset2.Count)
      return;
    for (int index = 0; index < pxResultset1.Count; ++index)
    {
      RMColumnSetMaint.ShiftColumnRange((RMColumnHeader) pxResultset2[index], (RMColumnHeader) pxResultset1[index]);
      RMColumnSetMaint.SwapItems<RMColumnHeader>(this.Headers.Cache, (RMColumnHeader) pxResultset1[index], (RMColumnHeader) pxResultset2[index], "ColumnCode");
    }
    this.Headers.Cache.IsDirty = true;
    this.Headers.View.RequestRefresh();
  }

  [PXButton(Tooltip = "Shift Left")]
  [PXUIField(DisplayName = "Shift Left")]
  protected virtual IEnumerable ToLeftColumn(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Parameter.Current.ColumnCell) && string.Compare(this.Parameter.Current.ColumnCell, "  A", StringComparison.OrdinalIgnoreCase) > 0 && this.Parameter.Current.ColumnActiveCellIndex > this.Parameter.Current.ColumnCellPredAIndex)
      this.ProcessToLeftColumn();
    return adapter.Get();
  }

  protected virtual void ProcessToLeftColumn()
  {
    RMColumn first = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.ColumnCell);
    RMColumn second = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) RMColumnCodeAttribute.ShiftCode(this.Parameter.Current.ColumnCell, false));
    if (first == null || second == null)
      return;
    RMColumnSetMaint.SwapItems<RMColumn>(this.ColumnsByCode.Cache, first, second, "ColumnCode");
    this.ColumnsByCode.Cache.IsDirty = true;
    this.Properties.View.RequestRefresh();
  }

  [PXButton(Tooltip = "Shift Right")]
  [PXUIField(DisplayName = "Shift Right")]
  protected virtual IEnumerable ToRightColumn(PXAdapter adapter)
  {
    string columnCell = this.Parameter.Current.ColumnCell;
    string lastColumn = this.ColumnSet.Current.LastColumn;
    if (!string.IsNullOrEmpty(this.Parameter.Current.ColumnCell) && this.ColumnSet.Current != null && !string.IsNullOrEmpty(this.ColumnSet.Current.LastColumn) && string.Compare(columnCell.Trim(), lastColumn.Trim(), StringComparison.OrdinalIgnoreCase) < 0 && this.Parameter.Current.ColumnActiveCellIndex > this.Parameter.Current.ColumnCellPredAIndex)
      this.ProcessToRightColumn();
    return adapter.Get();
  }

  protected virtual void ProcessToRightColumn()
  {
    RMColumn first = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.ColumnCell);
    RMColumn second = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) RMColumnCodeAttribute.ShiftCode(this.Parameter.Current.ColumnCell, true));
    if (first == null || second == null)
      return;
    RMColumnSetMaint.SwapItems<RMColumn>(this.ColumnsByCode.Cache, first, second, "ColumnCode");
    this.ColumnsByCode.Cache.IsDirty = true;
    this.Properties.View.RequestRefresh();
  }

  [PXButton(Tooltip = "Shift Left")]
  [PXUIField(DisplayName = "Shift Left", Visible = false)]
  protected virtual IEnumerable ShiftLeft(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Parameter.Current.ColumnCell) && string.Compare(this.Parameter.Current.ColumnCell, "  A", StringComparison.OrdinalIgnoreCase) > 0)
    {
      this.ProcessToLeftHeader();
      this.ProcessToLeftColumn();
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Shift Right")]
  [PXUIField(DisplayName = "Shift Right", Visible = false)]
  protected virtual IEnumerable ShiftRight(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Parameter.Current.HeaderCell) && this.ColumnSet.Current != null && !string.IsNullOrEmpty(this.ColumnSet.Current.LastColumn) && string.Compare(this.Parameter.Current.HeaderCell, this.ColumnSet.Current.LastColumn, StringComparison.OrdinalIgnoreCase) < 0)
    {
      this.ProcessToRightHeader();
      this.ProcessToRightColumn();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "", Visible = false)]
  protected virtual IEnumerable PasteHeader(PXAdapter adapter)
  {
    short? nbr1 = this.Headers.Cache.RowsToMove[0]["HeaderNbr"] as short?;
    short? nullable1 = nbr1;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num1 = 32766;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      return adapter.Get();
    short? nbr2 = this.Headers.Cache.InsertPosition["HeaderNbr"] as short?;
    int? headerRow = this.findHeaderRow(nbr1);
    nullable1 = nbr1;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = nbr2;
    int? nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num2 = nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue ? 1 : 0;
    nullable1 = nbr2;
    int? nullable5 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num3 = 32766;
    int? nullable6;
    int? nullable7;
    if (!(nullable5.GetValueOrDefault() == num3 & nullable5.HasValue))
    {
      nullable6 = this.findHeaderRow(nbr2);
      int num4 = num2;
      nullable7 = nullable6.HasValue ? new int?(nullable6.GetValueOrDefault() - num4) : new int?();
    }
    else
    {
      nullable1 = this.ColumnSet.Current.HeaderCntr;
      nullable7 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    }
    int? nullable8 = nullable7;
    nullable6 = headerRow;
    int? nullable9 = nullable8;
    if (nullable6.GetValueOrDefault() < nullable9.GetValueOrDefault() & nullable6.HasValue & nullable9.HasValue)
    {
      int? row1 = headerRow;
      while (true)
      {
        nullable9 = row1;
        nullable6 = nullable8;
        if (nullable9.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable9.HasValue & nullable6.HasValue)
        {
          short? headerNbr1 = this.findHeaderNbr(row1);
          nullable9 = row1;
          int? row2;
          if (!nullable9.HasValue)
          {
            nullable6 = new int?();
            row2 = nullable6;
          }
          else
            row2 = new int?(nullable9.GetValueOrDefault() + 1);
          short? headerNbr2 = this.findHeaderNbr(row2);
          this.SwapHeaders(headerNbr1, headerNbr2);
          nullable9 = row1;
          int? nullable10;
          if (!nullable9.HasValue)
          {
            nullable6 = new int?();
            nullable10 = nullable6;
          }
          else
            nullable10 = new int?(nullable9.GetValueOrDefault() + 1);
          row1 = nullable10;
        }
        else
          break;
      }
    }
    else
    {
      int? row3 = headerRow;
      while (true)
      {
        nullable6 = row3;
        nullable9 = nullable8;
        if (nullable6.GetValueOrDefault() > nullable9.GetValueOrDefault() & nullable6.HasValue & nullable9.HasValue)
        {
          short? headerNbr3 = this.findHeaderNbr(row3);
          nullable6 = row3;
          int? row4;
          if (!nullable6.HasValue)
          {
            nullable9 = new int?();
            row4 = nullable9;
          }
          else
            row4 = new int?(nullable6.GetValueOrDefault() - 1);
          short? headerNbr4 = this.findHeaderNbr(row4);
          this.SwapHeaders(headerNbr3, headerNbr4);
          nullable6 = row3;
          int? nullable11;
          if (!nullable6.HasValue)
          {
            nullable9 = new int?();
            nullable11 = nullable9;
          }
          else
            nullable11 = new int?(nullable6.GetValueOrDefault() - 1);
          row3 = nullable11;
        }
        else
          break;
      }
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Shift Up")]
  [PXUIField]
  protected virtual IEnumerable ToUpHeader(PXAdapter adapter)
  {
    int? headerRow = this.Parameter.Current.HeaderRow;
    int? nullable1 = headerRow;
    int num = 1;
    if (nullable1.GetValueOrDefault() <= num & nullable1.HasValue)
      return adapter.Get();
    short? headerNbr1 = this.findHeaderNbr(headerRow);
    int? nullable2 = headerRow;
    short? headerNbr2 = this.findHeaderNbr(nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() - 1) : new int?());
    this.SwapHeaders(headerNbr1, headerNbr2);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Shift Down")]
  [PXUIField]
  protected virtual IEnumerable ToDownHeader(PXAdapter adapter)
  {
    int? headerRow = this.Parameter.Current.HeaderRow;
    if (headerRow.HasValue)
    {
      if (this.ColumnSet.Current == null)
      {
        int? nullable1 = headerRow;
        short? headerCntr = this.ColumnSet.Current.HeaderCntr;
        int? nullable2 = headerCntr.HasValue ? new int?((int) headerCntr.GetValueOrDefault()) : new int?();
        if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          goto label_3;
      }
      short? headerNbr1 = this.findHeaderNbr(headerRow);
      int? nullable = headerRow;
      short? headerNbr2 = this.findHeaderNbr(nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?());
      this.SwapHeaders(headerNbr1, headerNbr2);
      return adapter.Get();
    }
label_3:
    return adapter.Get();
  }

  protected virtual void SwapHeaders(short? from, short? to)
  {
    PXResultset<RMColumnHeader> pxResultset1 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>.Config>.Select((PXGraph) this, (object) from);
    PXResultset<RMColumnHeader> pxResultset2 = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>.Config>.Select((PXGraph) this, (object) to);
    if (pxResultset1.Count <= 0 || pxResultset1.Count != pxResultset2.Count)
      return;
    for (int index = 0; index < pxResultset1.Count; ++index)
      RMColumnSetMaint.SwapItems<RMColumnHeader>(this.Headers.Cache, (RMColumnHeader) pxResultset1[index], (RMColumnHeader) pxResultset2[index], "HeaderNbr");
    this.Headers.Cache.IsDirty = true;
    this.Headers.View.RequestRefresh();
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Style")]
  protected virtual IEnumerable ToBufferHeader(PXAdapter adapter)
  {
    this.Parameter.Current.CopiedStyle = new int?();
    if (this.Parameter.Current.HeaderRow.HasValue && !string.IsNullOrEmpty(this.Parameter.Current.HeaderCell))
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.HeaderCell, (object) this.findHeaderNbr(this.Parameter.Current.HeaderRow));
      if (rmColumnHeader != null)
        this.Parameter.Current.CopiedStyle = rmColumnHeader.StyleID;
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Style")]
  protected virtual IEnumerable ToStyleHeader(PXAdapter adapter)
  {
    if (this.Parameter.Current.CopiedStyle.HasValue && this.Parameter.Current.HeaderRow.HasValue && !string.IsNullOrEmpty(this.Parameter.Current.HeaderCell))
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.HeaderCell, (object) this.findHeaderNbr(this.Parameter.Current.HeaderRow));
      if (rmColumnHeader != null)
      {
        RMColumnSetMaint.RMHeaderStyle rmHeaderStyle1 = (RMColumnSetMaint.RMHeaderStyle) this.HeaderStyle.Select((object) this.Parameter.Current.CopiedStyle);
        if (rmHeaderStyle1 != null)
        {
          RMColumnSetMaint.RMHeaderStyle rmHeaderStyle2 = (RMColumnSetMaint.RMHeaderStyle) this.HeaderStyle.Select((object) rmColumnHeader.StyleID);
          if (rmHeaderStyle2 != null)
          {
            rmHeaderStyle2.BackColor = rmHeaderStyle1.BackColor;
            rmHeaderStyle2.Color = rmHeaderStyle1.Color;
            rmHeaderStyle2.FontName = rmHeaderStyle1.FontName;
            rmHeaderStyle2.FontSize = rmHeaderStyle1.FontSize;
            rmHeaderStyle2.FontSizeType = rmHeaderStyle1.FontSizeType;
            rmHeaderStyle2.FontStyle = rmHeaderStyle1.FontStyle;
            rmHeaderStyle2.TextAlign = rmHeaderStyle1.TextAlign;
            if (this.HeaderStyle.Cache.GetStatus((object) rmHeaderStyle2) == PXEntryStatus.Notchanged)
            {
              this.HeaderStyle.Cache.SetStatus((object) rmHeaderStyle2, PXEntryStatus.Updated);
              this.HeaderStyle.Cache.IsDirty = true;
            }
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Style")]
  protected virtual IEnumerable ToBufferColumn(PXAdapter adapter)
  {
    this.Parameter.Current.CopiedStyle = new int?();
    if (!string.IsNullOrEmpty(this.Parameter.Current.ColumnCell))
    {
      RMColumn rmColumn = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.ColumnCell);
      if (rmColumn != null)
        this.Parameter.Current.CopiedStyle = rmColumn.StyleID;
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Style")]
  protected virtual IEnumerable ToStyleColumn(PXAdapter adapter)
  {
    if (this.Parameter.Current.CopiedStyle.HasValue && !string.IsNullOrEmpty(this.Parameter.Current.ColumnCell))
    {
      RMColumn rmColumn = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.ColumnCell);
      if (rmColumn != null)
      {
        RMStyle rmStyle1 = (RMStyle) this.StyleByID.Select((object) this.Parameter.Current.CopiedStyle);
        if (rmStyle1 == null)
          rmStyle1 = (RMStyle) (RMColumnSetMaint.RMHeaderStyle) this.HeaderStyle.Select((object) this.Parameter.Current.CopiedStyle);
        if (rmStyle1 != null)
        {
          RMStyle rmStyle2 = (RMStyle) this.StyleByID.Select((object) rmColumn.StyleID);
          if (rmStyle2 != null)
          {
            rmStyle2.BackColor = rmStyle1.BackColor;
            rmStyle2.Color = rmStyle1.Color;
            rmStyle2.FontName = rmStyle1.FontName;
            rmStyle2.FontSize = rmStyle1.FontSize;
            rmStyle2.FontSizeType = rmStyle1.FontSizeType;
            rmStyle2.FontStyle = rmStyle1.FontStyle;
            rmStyle2.TextAlign = rmStyle1.TextAlign;
            if (this.StyleByID.Cache.GetStatus((object) rmStyle2) == PXEntryStatus.Notchanged)
            {
              this.StyleByID.Cache.SetStatus((object) rmStyle2, PXEntryStatus.Updated);
              this.StyleByID.Cache.IsDirty = true;
            }
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Formatting", Visible = false)]
  protected virtual IEnumerable CopyFormatting(PXAdapter adapter)
  {
    short? nullable1 = this.Parameter.Current.HeaderNbr;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num = 32766;
    bool flag = !(nullable2.GetValueOrDefault() == num & nullable2.HasValue);
    this.Parameter.Current.CopiedStyle = flag ? this.CurrentHeader.Current.StyleID : this.CurrentColumn.Current.StyleID;
    RMColumnSetMaint.ParamFilter current = this.Parameter.Current;
    short? nullable3;
    if (!flag)
    {
      nullable1 = new short?();
      nullable3 = nullable1;
    }
    else
      nullable3 = this.Parameter.Current.HeaderNbr;
    current.CopiedFormattingHeaderNbr = nullable3;
    this.Parameter.Current.CopiedFormattingColumn = flag ? this.Parameter.Current.HeaderCell : this.Parameter.Current.ColumnCell;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Cell Formatting", Visible = false)]
  protected virtual IEnumerable PasteFormatting(PXAdapter adapter)
  {
    bool hasValue = this.Parameter.Current.CopiedFormattingHeaderNbr.HasValue;
    RMColumnHeader current1 = this.Header.Current;
    RMColumn current2 = this.Column.Current;
    RMColumnHeader rmColumnHeader1;
    if (!hasValue)
      rmColumnHeader1 = (RMColumnHeader) null;
    else
      rmColumnHeader1 = this.Header.SelectSingle((object) this.Parameter.Current.CopiedFormattingColumn, (object) this.Parameter.Current.CopiedFormattingHeaderNbr);
    RMColumnHeader rmColumnHeader2 = rmColumnHeader1;
    RMColumn sourceColumn = this.Column.SelectSingle((object) this.Parameter.Current.CopiedFormattingColumn);
    RMStyle rmStyle;
    if (!hasValue)
      rmStyle = this.StyleByID.SelectSingle((object) this.Parameter.Current.CopiedStyle);
    else
      rmStyle = (RMStyle) (this.CurrentHeaderStyle.View.SelectSingleBound(new object[1]
      {
        (object) rmColumnHeader2
      }, (object[]) null) as RMColumnSetMaint.RMHeaderStyle);
    RMStyle sourceStyle = rmStyle;
    short? headerNbr = this.Parameter.Current.HeaderNbr;
    int? nullable = headerNbr.HasValue ? new int?((int) headerNbr.GetValueOrDefault()) : new int?();
    int num = 32766;
    RMStyle current3;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
    {
      this.CurrentHeaderStyle.Current = this.CurrentHeaderStyle.SelectSingle();
      current3 = (RMStyle) this.CurrentHeaderStyle.Current;
    }
    else
    {
      this.CurrentColumnStyle.Current = this.CurrentColumnStyle.SelectSingle();
      current3 = this.CurrentColumnStyle.Current;
    }
    if (sourceStyle == null || current3 == null)
      return adapter.Get();
    this.ProcessPasteFormatting(rmColumnHeader2 == null, sourceColumn, current2, sourceStyle, current3);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Column Formatting", Visible = false)]
  protected virtual IEnumerable PasteColumnFormatting(PXAdapter adapter)
  {
    RMColumn current1 = this.Column.Current;
    RMColumn sourceColumn = this.Column.SelectSingle((object) this.Parameter.Current.CopiedFormattingColumn);
    IEnumerable<RMColumnHeader> rmColumnHeaders1 = (IEnumerable<RMColumnHeader>) ((object) this.HeadersByCode.View.SelectMultiBound(new object[2]
    {
      (object) this.ColumnSet.Current,
      (object) sourceColumn
    }, (object[]) null).RowCast<RMColumnHeader>() ?? (object) Array.Empty<RMColumnHeader>());
    IEnumerable<RMColumnHeader> rmColumnHeaders2 = (IEnumerable<RMColumnHeader>) ((object) this.HeadersByCode.View.SelectMultiBound((object[]) null, (object[]) null).RowCast<RMColumnHeader>() ?? (object) Array.Empty<RMColumnHeader>());
    PXResultset<RMColumnSetMaint.RMHeaderStyle> pxResultset1 = PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelectJoin<RMColumnSetMaint.RMHeaderStyle, InnerJoin<RMColumnHeader, On<RMColumnHeader.styleID, Equal<RMColumnSetMaint.RMHeaderStyle.styleID>>>, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Current<RMColumnSetMaint.ParamFilter.copiedFormattingColumn>>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, (object[]) null);
    Dictionary<string, RMColumnSetMaint.RMHeaderStyle> dictionary1 = this.HeaderStyle.Cache.Cached.RowCast<RMColumnSetMaint.RMHeaderStyle>().Where<RMColumnSetMaint.RMHeaderStyle>((Func<RMColumnSetMaint.RMHeaderStyle, bool>) (x =>
    {
      if (!EnumerableExtensions.IsNotIn<PXEntryStatus>(this.HeaderStyle.Cache.GetStatus((object) x), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return false;
      int? styleId = x.StyleID;
      int num = 0;
      return styleId.GetValueOrDefault() < num & styleId.HasValue;
    })).ToDictionary<RMColumnSetMaint.RMHeaderStyle, string, RMColumnSetMaint.RMHeaderStyle>((Func<RMColumnSetMaint.RMHeaderStyle, string>) (x => x.StyleID.ToString()), (Func<RMColumnSetMaint.RMHeaderStyle, RMColumnSetMaint.RMHeaderStyle>) (x => x));
    Dictionary<int, RMColumnSetMaint.RMHeaderStyle> dictionary2 = new Dictionary<int, RMColumnSetMaint.RMHeaderStyle>();
    foreach (PXResult<RMColumnSetMaint.RMHeaderStyle, RMColumnHeader> pxResult in pxResultset1)
    {
      RMColumnSetMaint.RMHeaderStyle rmHeaderStyle = (RMColumnSetMaint.RMHeaderStyle) pxResult;
      RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
      dictionary2[(int) rmColumnHeader.HeaderNbr.GetValueOrDefault()] = rmHeaderStyle;
    }
    foreach (RMColumnHeader rmColumnHeader in rmColumnHeaders1)
    {
      int? styleId = rmColumnHeader.StyleID;
      int num = 0;
      if (!(styleId.GetValueOrDefault() > num & styleId.HasValue))
      {
        Dictionary<string, RMColumnSetMaint.RMHeaderStyle> dictionary3 = dictionary1;
        styleId = rmColumnHeader.StyleID;
        string str = styleId.ToString();
        RMColumnSetMaint.RMHeaderStyle valueOrDefault = NonGenericIDictionaryExtensions.GetValueOrDefault_<RMColumnSetMaint.RMHeaderStyle>((IDictionary) dictionary3, str, (RMColumnSetMaint.RMHeaderStyle) null);
        if (valueOrDefault != null)
          dictionary2[(int) rmColumnHeader.HeaderNbr.GetValueOrDefault()] = valueOrDefault;
      }
    }
    PXResultset<RMColumnSetMaint.RMHeaderStyle> pxResultset2 = PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelectJoin<RMColumnSetMaint.RMHeaderStyle, InnerJoin<RMColumnHeader, On<RMColumnHeader.styleID, Equal<RMColumnSetMaint.RMHeaderStyle.styleID>>>, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Current<RMColumnSetMaint.ParamFilter.headerCell>>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, (object[]) null);
    Dictionary<int, RMColumnSetMaint.RMHeaderStyle> dictionary4 = new Dictionary<int, RMColumnSetMaint.RMHeaderStyle>();
    foreach (PXResult<RMColumnSetMaint.RMHeaderStyle, RMColumnHeader> pxResult in pxResultset2)
    {
      RMColumnSetMaint.RMHeaderStyle rmHeaderStyle = (RMColumnSetMaint.RMHeaderStyle) pxResult;
      RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
      dictionary4[(int) rmColumnHeader.HeaderNbr.GetValueOrDefault()] = rmHeaderStyle;
    }
    foreach (RMColumnHeader rmColumnHeader in rmColumnHeaders2)
    {
      int? styleId = rmColumnHeader.StyleID;
      int num = 0;
      if (!(styleId.GetValueOrDefault() > num & styleId.HasValue))
      {
        Dictionary<string, RMColumnSetMaint.RMHeaderStyle> dictionary5 = dictionary1;
        styleId = rmColumnHeader.StyleID;
        string str = styleId.ToString();
        RMColumnSetMaint.RMHeaderStyle valueOrDefault = NonGenericIDictionaryExtensions.GetValueOrDefault_<RMColumnSetMaint.RMHeaderStyle>((IDictionary) dictionary5, str, (RMColumnSetMaint.RMHeaderStyle) null);
        if (valueOrDefault != null)
          dictionary4[(int) rmColumnHeader.HeaderNbr.GetValueOrDefault()] = valueOrDefault;
      }
    }
    foreach (int key in dictionary2.Keys)
    {
      RMColumnSetMaint.RMHeaderStyle sourceStyle = dictionary2[key];
      RMColumnSetMaint.RMHeaderStyle targetStyle = dictionary4[key];
      this.ProcessPasteFormatting(true, sourceColumn, current1, (RMStyle) sourceStyle, (RMStyle) targetStyle);
    }
    RMStyle sourceStyle1 = this.StyleByID.SelectSingle((object) sourceColumn.StyleID);
    this.CurrentColumnStyle.Current = this.CurrentColumnStyle.SelectSingle();
    RMStyle current2 = this.CurrentColumnStyle.Current;
    this.ProcessPasteFormatting(false, sourceColumn, current1, sourceStyle1, current2);
    current1.Width = sourceColumn.Width;
    return adapter.Get();
  }

  protected virtual void ProcessPasteFormatting(
    bool cellFormatting,
    RMColumn sourceColumn,
    RMColumn currentColumn,
    RMStyle sourceStyle,
    RMStyle targetStyle)
  {
    if (cellFormatting)
    {
      currentColumn.AutoHeight = sourceColumn.AutoHeight;
      currentColumn.Format = sourceColumn.Format;
      currentColumn.CellFormatOrder = sourceColumn.CellFormatOrder;
      currentColumn.Rounding = sourceColumn.Rounding;
      currentColumn.PrintControl = sourceColumn.PrintControl;
      currentColumn.PageBreak = sourceColumn.PageBreak;
      currentColumn.ExtraSpace = sourceColumn.ExtraSpace;
    }
    this.ColumnsByCode.Update(currentColumn);
    targetStyle.BackColor = sourceStyle.BackColor;
    targetStyle.Color = sourceStyle.Color;
    targetStyle.FontName = sourceStyle.FontName;
    targetStyle.FontSize = sourceStyle.FontSize;
    targetStyle.FontSizeType = sourceStyle.FontSizeType;
    targetStyle.FontStyle = sourceStyle.FontStyle;
    targetStyle.TextAlign = sourceStyle.TextAlign;
    if (targetStyle is RMColumnSetMaint.RMHeaderStyle rmHeaderStyle)
      this.AllHeaderStyles.Update(rmHeaderStyle);
    else
      this.AllColumnStyles.Update(targetStyle);
    this.StyleByID.Update(targetStyle);
  }

  [PXButton]
  [PXUIField(DisplayName = "Reset Cell Formatting", Visible = false)]
  protected virtual IEnumerable ResetFormatting(PXAdapter adapter)
  {
    short? headerNbr = this.Parameter.Current.HeaderNbr;
    int? nullable = headerNbr.HasValue ? new int?((int) headerNbr.GetValueOrDefault()) : new int?();
    int num1 = 32766;
    int num2 = !(nullable.GetValueOrDefault() == num1 & nullable.HasValue) ? 1 : 0;
    PXCache cache;
    if (num2 != 0)
    {
      this.CurrentHeaderStyle.Current = this.CurrentHeaderStyle.SelectSingle();
      cache = this.CurrentHeaderStyle.Cache;
    }
    else
    {
      this.CurrentColumnStyle.Current = this.CurrentColumnStyle.SelectSingle();
      cache = this.CurrentColumnStyle.Cache;
    }
    RMStyle current = cache.Current as RMStyle;
    cache.SetDefaultExt((object) current, "FontName");
    cache.SetDefaultExt((object) current, "FontSize");
    cache.SetDefaultExt((object) current, "FontStyle");
    cache.SetDefaultExt((object) current, "FontSizeType");
    cache.SetDefaultExt((object) current, "Color");
    cache.SetDefaultExt((object) current, "BackColor");
    cache.SetDefaultExt((object) current, "TextAlign");
    if (num2 == 0)
    {
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.autoHeight>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.format>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.cellFormatOrder>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.rounding>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.printControl>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.pageBreak>((object) this.CurrentColumn.Current);
      this.CurrentColumn.Cache.SetDefaultExt<RMColumn.extraSpace>((object) this.CurrentColumn.Current);
    }
    if (current is RMColumnSetMaint.RMHeaderStyle rmHeaderStyle)
      this.AllHeaderStyles.Update(rmHeaderStyle);
    else
      this.AllColumnStyles.Update(current);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Move")]
  protected virtual IEnumerable MoveColumn(PXAdapter adapter)
  {
    string[] strArray = (adapter.CommandArguments ?? string.Empty).Split(';');
    if (strArray.Length == 3)
    {
      string str1 = strArray[0];
      string str2 = strArray[1];
      bool forward = strArray[2].Trim().ToLower() == "true";
      RMColumn rmColumn = (RMColumn) PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, Equal<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) str1);
      List<string> stringList = new List<string>();
      string code = str1;
      int num = 10000;
      while ((code = RMColumnCodeAttribute.ShiftCode(code, forward)) != str2 && num-- > 0)
        stringList.Add(code);
      stringList.Add(code);
      Dictionary<string, RMColumn> dictionary = PXSelectBase<RMColumn, PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumn.columnCode, In<Required<RMColumn.columnCode>>>>>.Config>.Select((PXGraph) this, (object) stringList.ToArray()).ToDictionary<PXResult<RMColumn>, string, RMColumn>((Func<PXResult<RMColumn>, string>) (c => ((RMColumn) c).ColumnCode), (Func<PXResult<RMColumn>, RMColumn>) (c => (RMColumn) c));
      RMColumn first = rmColumn;
      if (rmColumn != null && dictionary.Count > 0)
      {
        foreach (string key in stringList)
        {
          RMColumn second = dictionary[key];
          RMColumnSetMaint.SwapItems<RMColumn>(this.ColumnsByCode.Cache, first, second, "ColumnCode");
          first = second;
        }
        this.ColumnsByCode.Cache.IsDirty = true;
        this.Properties.View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Move")]
  protected virtual IEnumerable MoveHeader(PXAdapter adapter)
  {
    string[] strArray = (adapter.CommandArguments ?? string.Empty).Split(';');
    if (strArray.Length == 3)
    {
      string str1 = strArray[0];
      string str2 = strArray[1];
      bool forward = strArray[2].Trim().ToLower() == "true";
      List<PXResult<RMColumnHeader>> list = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, Equal<Required<RMColumnHeader.columnCode>>>>>.Config>.Select((PXGraph) this, (object) str1).ToList<PXResult<RMColumnHeader>>();
      List<string> stringList = new List<string>();
      string code = str1;
      int num = 10000;
      while ((code = RMColumnCodeAttribute.ShiftCode(code, forward)) != str2 && num-- > 0)
        stringList.Add(code);
      stringList.Add(code);
      PXResultset<RMColumnHeader> pxResultset = PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.columnCode, In<Required<RMColumnHeader.columnCode>>>>, OrderBy<Asc<RMColumnHeader.columnCode, Asc<RMColumnHeader.headerNbr>>>>.Config>.Select((PXGraph) this, (object) stringList.ToArray());
      Dictionary<string, List<PXResult<RMColumnHeader>>> dictionary = new Dictionary<string, List<PXResult<RMColumnHeader>>>();
      string key1 = string.Empty;
      List<PXResult<RMColumnHeader>> pxResultList1 = new List<PXResult<RMColumnHeader>>();
      foreach (PXResult<RMColumnHeader> pxResult in pxResultset)
      {
        RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
        if (key1 != rmColumnHeader.ColumnCode && key1 != string.Empty)
        {
          dictionary[key1] = pxResultList1;
          pxResultList1 = new List<PXResult<RMColumnHeader>>();
        }
        key1 = rmColumnHeader.ColumnCode;
        pxResultList1.Add(pxResult);
      }
      if (key1 != string.Empty)
        dictionary[key1] = pxResultList1;
      if (list.Count > 0)
      {
        List<PXResult<RMColumnHeader>> pxResultList2 = list.Select<PXResult<RMColumnHeader>, PXResult<RMColumnHeader>>((Func<PXResult<RMColumnHeader>, PXResult<RMColumnHeader>>) (c => c)).ToList<PXResult<RMColumnHeader>>();
        foreach (string key2 in stringList)
        {
          List<PXResult<RMColumnHeader>> pxResultList3 = dictionary[key2];
          if (pxResultList2.Count != pxResultList3.Count)
            throw new ApplicationException("Column size mismatch");
          for (int index = 0; index < list.Count; ++index)
          {
            if (forward)
              RMColumnSetMaint.ShiftColumnRange((RMColumnHeader) pxResultList3[index], (RMColumnHeader) pxResultList2[index]);
            else
              RMColumnSetMaint.ShiftColumnRange((RMColumnHeader) pxResultList2[index], (RMColumnHeader) pxResultList3[index]);
            RMColumnSetMaint.SwapItems<RMColumnHeader>(this.Headers.Cache, (RMColumnHeader) pxResultList2[index], (RMColumnHeader) pxResultList3[index], "ColumnCode");
          }
          pxResultList2 = pxResultList3;
        }
        this.Headers.Cache.IsDirty = true;
        this.Headers.View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  private short? findHeaderNbr(int? row)
  {
    int num1 = 1;
    foreach (PXResult<RMColumnHeader> pxResult in this.Headers.Select())
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) pxResult;
      int num2 = num1;
      int? nullable = row;
      int valueOrDefault = nullable.GetValueOrDefault();
      if (num2 == valueOrDefault & nullable.HasValue)
        return rmColumnHeader.HeaderNbr;
      ++num1;
    }
    return new short?((short) 0);
  }

  private int? findHeaderRow(short? nbr)
  {
    int? nullable1 = new int?(1);
    foreach (PXResult<RMColumnHeader> pxResult in this.Headers.Select())
    {
      short? nullable2 = ((RMColumnHeader) pxResult).HeaderNbr;
      int? nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
      nullable2 = nbr;
      int? headerRow = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
      if (nullable3.GetValueOrDefault() == headerRow.GetValueOrDefault() & nullable3.HasValue == headerRow.HasValue)
      {
        headerRow = nullable1;
        return headerRow;
      }
      nullable3 = nullable1;
      nullable1 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
    }
    return new int?(0);
  }

  [PXButton]
  [PXUIField(DisplayName = "", Visible = false)]
  public IEnumerable currentHeaderChanged(PXAdapter adapter)
  {
    string[] strArray = adapter.CommandArguments.Split(',');
    this.Parameter.Current.HeaderNbr = new short?(short.Parse(strArray[0]));
    this.Parameter.Current.ColumnCell = strArray[1];
    this.Parameter.Current.HeaderCell = strArray[1];
    this.Parameter.Current.HeaderRow = new int?(int.Parse(strArray[2]));
    this.CurrentHeader.Current = this.CurrentHeader.SelectSingle();
    this.CurrentHeaderStyle.Current = this.CurrentHeaderStyle.SelectSingle();
    return adapter.Get();
  }

  public virtual void RMColumnHeader_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    if (row == null)
      return;
    if (string.IsNullOrEmpty(row.StartColumn))
      row.StartColumn = row.ColumnCode;
    if (string.IsNullOrEmpty(row.EndColumn))
      row.EndColumn = row.ColumnCode;
    if (row.Height.HasValue)
      return;
    RMColumnHeader rmColumnHeader = (RMColumnHeader) this.Header.Select((object) "  A", (object) row.HeaderNbr);
    if (rmColumnHeader == null)
      return;
    row.Height = rmColumnHeader.Height;
  }

  public virtual void RMColumnHeader_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.IsImport)
      PXUIFieldAttribute.SetEnabled<RMColumnHeader.headerNbr>(this.HeadersByCode.Cache, (object) null);
    PXAction<RMColumnSet> deleteHeader = this.deleteHeader;
    short? headerNbr = this.Parameter.Current.HeaderNbr;
    int? nullable;
    int num1;
    if (headerNbr.HasValue)
    {
      headerNbr = this.Parameter.Current.HeaderNbr;
      nullable = headerNbr.HasValue ? new int?((int) headerNbr.GetValueOrDefault()) : new int?();
      int num2 = 32766;
      num1 = !(nullable.GetValueOrDefault() == num2 & nullable.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    deleteHeader.SetEnabled(num1 != 0);
    RMColumnHeader row = (RMColumnHeader) e.Row;
    if (row == null)
      return;
    nullable = row.StyleID;
    int num3 = 0;
    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
      return;
    PXCache cache = this.HeaderStyle.Cache;
    RMColumnSetMaint.RMHeaderStyle rmHeaderStyle1 = new RMColumnSetMaint.RMHeaderStyle();
    rmHeaderStyle1.StyleID = row.StyleID;
    if (cache.Locate((object) rmHeaderStyle1) != null)
      return;
    bool? isRowSet = row.IsRowSet;
    bool flag = true;
    if (isRowSet.GetValueOrDefault() == flag & isRowSet.HasValue)
    {
      RMColumn rmColumn = (RMColumn) this.Column.Select((object) row.ColumnCode);
      row.StyleID = rmColumn.StyleID;
    }
    else
    {
      RMColumnSetMaint.RMHeaderStyle rmHeaderStyle2 = new RMColumnSetMaint.RMHeaderStyle();
      using (new ReadOnlyScope(new PXCache[1]
      {
        this.HeaderStyle.Cache
      }))
        rmHeaderStyle2 = this.HeaderStyle.Insert(rmHeaderStyle2);
      if (rmHeaderStyle2 == null)
        return;
      row.StyleID = rmHeaderStyle2.StyleID;
      this.Headers.Cache.MarkUpdated((object) row);
    }
  }

  public virtual void RMColumnHeader_Height_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is RMColumnHeader row1) || !e.ExternalCall)
      return;
    if (row1.ColumnCode != "  A")
    {
      int? height1 = ((RMColumnHeader) this.Header.Select((object) "  A", (object) row1.HeaderNbr)).Height;
      int? height2 = row1.Height;
      if (height1.GetValueOrDefault() == height2.GetValueOrDefault() & height1.HasValue == height2.HasValue || !row1.Height.HasValue)
        return;
    }
    foreach (PXResult<RMColumnHeader> pxResult in PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>>>>.Config>.Select((PXGraph) this, (object) ((RMColumnHeader) e.Row).HeaderNbr))
    {
      RMColumnHeader row2 = (RMColumnHeader) pxResult;
      if (!(row2.ColumnCode == ((RMColumnHeader) e.Row).ColumnCode))
      {
        row2.Height = ((RMColumnHeader) e.Row).Height;
        sender.MarkUpdated((object) row2);
      }
    }
    this.Headers.Cache.IsDirty = true;
  }

  public virtual void RMColumnHeader_EndColumn_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    RMColumnHeader row = e.Row as RMColumnHeader;
    string newValue = e.NewValue as string;
    if (row != null && !string.IsNullOrEmpty(newValue) && string.Compare(newValue, row.ColumnCode, StringComparison.OrdinalIgnoreCase) < 0)
      throw new PXSetPropertyException<RMColumnHeader.startColumn>(PXMessages.LocalizeFormat("The end column in Column Range of the column {0} can't be located to the left.", (object) row.ColumnCode));
  }

  public virtual void RMColumnHeader_GroupID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall || e.Row == null || !(((RMColumnHeader) e.Row).ColumnCode == "  A"))
      return;
    foreach (PXResult<RMColumnHeader> pxResult in PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnSet.columnSetCode>>, And<RMColumnHeader.headerNbr, Equal<Required<RMColumnHeader.headerNbr>>, And<RMColumnHeader.columnCode, NotEqual<RMColumnSetMaint.ColumnCodeA>>>>>.Config>.Select((PXGraph) this, (object) ((RMColumnHeader) e.Row).HeaderNbr))
    {
      RMColumnHeader row = (RMColumnHeader) pxResult;
      row.GroupID = ((RMColumnHeader) e.Row).GroupID;
      sender.MarkUpdated((object) row);
    }
    this.Headers.Cache.IsDirty = true;
  }

  public virtual void RMHeaderStyle_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RMColumnSetMaint.RMHeaderStyle row = (RMColumnSetMaint.RMHeaderStyle) e.Row;
    if (row == null || !row.StyleID.HasValue)
      return;
    RMColumnHeader rmColumnHeader = (RMColumnHeader) PXSelectBase<RMColumnHeader, PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Current<RMColumnHeader.columnSetCode>>, And<RMColumnHeader.styleID, Equal<Required<RMColumnHeader.styleID>>>>>.Config>.Select((PXGraph) this, (object) row.StyleID);
    if (rmColumnHeader == null)
      return;
    rmColumnHeader.StartColumn = row.StartColumn;
    rmColumnHeader.EndColumn = row.EndColumn;
    this.Headers.Update(rmColumnHeader);
  }

  protected virtual void RMColumnSet_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RMColumnSet row = (RMColumnSet) e.Row;
    if (row == null || this.isCopying || this.IsImport)
      return;
    row.LastColumn = "  @";
    RMColumn rmColumn = this.ColumnsByCode.Insert(new RMColumn());
    RMColumnHeader rmColumnHeader = new RMColumnHeader();
    rmColumnHeader.ColumnCode = rmColumn.ColumnCode;
    rmColumnHeader.StartColumn = rmColumn.ColumnCode;
    rmColumnHeader.EndColumn = rmColumn.ColumnCode;
    rmColumnHeader.HeaderNbr = new short?((short) 1);
    this._BypassInsert = true;
    this.Headers.Insert(rmColumnHeader);
    this._BypassInsert = false;
    row.HeaderCntr = new short?((short) 1);
    this.ColumnsByCode.Cache.IsDirty = false;
    this.Headers.Cache.IsDirty = false;
  }

  protected virtual void RMColumnSet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RMColumnSet row = (RMColumnSet) e.Row;
    if (row != null)
    {
      PXFormulaAttribute.CalcAggregate<RMColumn.columnCode>(this.Caches[typeof (RMColumn)], (object) row);
      if (string.IsNullOrEmpty(row.LastColumn))
        row.LastColumn = "  @";
      if (row.LastColumn != "  @")
      {
        foreach (PXResult<RMColumn> pxResult in this.ColumnsByCode.Select())
          this.TryAddHandlers(((RMColumn) pxResult).ColumnCode);
        if (this.LastCodeOnLoad != null && this.LastCodeOnLoad != row.LastColumn)
        {
          for (string str = RMColumnCodeAttribute.ShiftCode(row.LastColumn, true); string.Compare(str, this.LastCodeOnLoad, StringComparison.OrdinalIgnoreCase) <= 0; str = RMColumnCodeAttribute.ShiftCode(str, true))
          {
            this.Properties.Cache.Fields.Remove(str);
            this.Headers.Cache.Fields.Remove(str);
          }
          this.LastCodeOnLoad = row.LastColumn;
          this.Headers.View.RequestRefresh();
          this.Properties.View.RequestRefresh();
        }
      }
    }
    this.pasteFormatting.SetEnabled(this.Parameter.Current.CopiedStyle.HasValue);
    this.copyColumnSet.SetEnabled(this.ColumnSet.Current != null && this.ColumnSet.Cache.GetStatus((object) this.ColumnSet.Current) != PXEntryStatus.Inserted && !string.IsNullOrEmpty(this.ColumnSet.Current.ColumnSetCode));
  }

  protected virtual void RMColumnHeader_HeaderNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._BypassInsert || e.Row == null)
      return;
    RMColumnHeader row = (RMColumnHeader) e.Row;
    RMColumnHeader rmColumnHeader = row;
    RMColumnSet current = this.ColumnSet.Current;
    short? headerCntr = current.HeaderCntr;
    short? nullable1 = headerCntr.HasValue ? new short?((short) ((int) headerCntr.GetValueOrDefault() + 1)) : new short?();
    current.HeaderCntr = nullable1;
    short? nullable2 = nullable1;
    rmColumnHeader.HeaderNbr = nullable2;
    this.ColumnSet.Cache.MarkUpdated((object) this.ColumnSet.Current);
    if (sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) == null)
    {
      row.ColumnCode = "  A";
      this._BypassInsert = true;
      foreach (PXResult<RMColumn> pxResult in this.ColumnsByCode.Select())
      {
        RMColumn rmColumn = (RMColumn) pxResult;
        if (rmColumn.ColumnCode != "  A")
          this.Headers.Insert(new RMColumnHeader()
          {
            ColumnSetCode = this.ColumnSet.Current.ColumnSetCode,
            HeaderNbr = this.ColumnSet.Current.HeaderCntr,
            ColumnCode = rmColumn.ColumnCode,
            StartColumn = rmColumn.ColumnCode,
            EndColumn = rmColumn.ColumnCode
          });
      }
      this._BypassInsert = false;
    }
    else
    {
      if (this.Column.Current == null)
        return;
      row.ColumnCode = this.Column.Current.ColumnCode;
    }
  }

  protected virtual void RMColumn_StyleID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) == null)
      return;
    this.Headers.Cache.AllowInsert = true;
  }

  protected virtual void RMColumnHeader_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._ColumnDeleting || e.Row == null)
      return;
    RMColumnHeader row = (RMColumnHeader) e.Row;
    if (!this._BypassDelete)
    {
      short? headerNbr = row.HeaderNbr;
      int? nullable1 = headerNbr.HasValue ? new int?((int) headerNbr.GetValueOrDefault()) : new int?();
      short? headerCntr = this.ColumnSet.Current.HeaderCntr;
      int? nullable2 = headerCntr.HasValue ? new int?((int) headerCntr.GetValueOrDefault()) : new int?();
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        headerCntr = this.ColumnSet.Current.HeaderCntr;
        int? nullable3 = headerCntr.HasValue ? new int?((int) headerCntr.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
        {
          RMColumnSet current = this.ColumnSet.Current;
          headerCntr = current.HeaderCntr;
          short? nullable4 = headerCntr;
          current.HeaderCntr = nullable4.HasValue ? new short?((short) ((int) nullable4.GetValueOrDefault() - 1)) : new short?();
        }
      }
      this.ColumnSet.Cache.MarkUpdated((object) this.ColumnSet.Current);
      row.ColumnCode = "  A";
      this._BypassDelete = true;
      foreach (PXResult<RMColumn> pxResult in this.ColumnsByCode.Select())
      {
        RMColumn rmColumn = (RMColumn) pxResult;
        if (rmColumn.ColumnCode != "  A")
        {
          RMColumnHeader rmColumnHeader = (RMColumnHeader) this.Header.Select((object) rmColumn.ColumnCode, (object) row.HeaderNbr);
          if (rmColumnHeader != null)
            this.Headers.Delete(rmColumnHeader);
        }
      }
      this._BypassDelete = false;
    }
    if (!row.StyleID.HasValue)
      return;
    RMColumnSetMaint.RMHeaderStyle rmHeaderStyle = (RMColumnSetMaint.RMHeaderStyle) PXSelectBase<RMColumnSetMaint.RMHeaderStyle, PXSelect<RMColumnSetMaint.RMHeaderStyle, Where<RMColumnSetMaint.RMHeaderStyle.styleID, Equal<Required<RMColumnSetMaint.RMHeaderStyle.styleID>>>>.Config>.Select((PXGraph) this, (object) row.StyleID);
    if (rmHeaderStyle == null)
      return;
    this.HeaderStyle.Delete(rmHeaderStyle);
  }

  protected virtual void ColumnProperty_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void _(Events.RowSelected<RMColumn> e)
  {
    RMColumn row = e.Row;
    if (row == null)
      return;
    short? cellEvalOrder = row.CellEvalOrder;
    int? nullable1 = cellEvalOrder.HasValue ? new int?((int) cellEvalOrder.GetValueOrDefault()) : new int?();
    int num1 = 1;
    int num2;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      short? columnType = row.ColumnType;
      int? nullable2 = columnType.HasValue ? new int?((int) columnType.GetValueOrDefault()) : new int?();
      int num3 = 0;
      num2 = nullable2.GetValueOrDefault() == num3 & nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0;
    this.Column.Cache.RaiseExceptionHandling<RMColumn.cellEvalOrder>((object) row, (object) row.CellEvalOrder, flag ? (Exception) new PXSetPropertyException<RMColumn.cellEvalOrder>("You cannot use Cell Evaluation Order set to Column for columns of the GL type.", PXErrorLevel.Error) : (Exception) null);
    this.Column.Cache.RaiseExceptionHandling<RMColumn.columnType>((object) row, (object) row.ColumnType, flag ? (Exception) new PXSetPropertyException<RMColumn.columnType>("You cannot use Cell Evaluation Order set to Column for columns of the GL type.", PXErrorLevel.Error) : (Exception) null);
  }

  protected virtual void columnTextSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnSetMaint.ColumnProperty row = (RMColumnSetMaint.ColumnProperty) e.Row;
    if (row == null)
      return;
    if (row.Name == "DataSourceID")
    {
      RMColumn rmColumn = (RMColumn) this.Column.Select((object) code);
      if (rmColumn != null)
      {
        RMDataSource data = (RMDataSource) this.DataSourceByID.Select((object) rmColumn.DataSourceID);
        if (data != null)
          e.ReturnState = this.DataSourceByID.Cache.GetStateExt((object) data, "DataSourceIDText");
      }
    }
    else if (row.Name == "StyleID")
    {
      RMColumn rmColumn = (RMColumn) this.Column.Select((object) code);
      if (rmColumn != null)
      {
        RMStyle data = (RMStyle) this.StyleByID.Select((object) rmColumn.StyleID);
        if (data != null)
          e.ReturnState = this.StyleByID.Cache.GetStateExt((object) data, "StyleIDText");
      }
    }
    else
      this.columnFieldSelecting(sender, e, code);
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.SetFieldName(code + "_Text");
    if (!(returnState is PXIntState pxIntState) || pxIntState.AllowedValues == null || pxIntState.AllowedValues.Length == 0 || pxIntState.Value == null)
      return;
    int int32 = Convert.ToInt32(pxIntState.Value);
    for (int index = 0; index < pxIntState.AllowedValues.Length; ++index)
    {
      if (pxIntState.AllowedValues[index] == int32)
      {
        if (pxIntState.AllowedLabels == null || index >= pxIntState.AllowedLabels.Length)
          break;
        pxIntState.Value = (object) pxIntState.AllowedLabels[index];
        break;
      }
    }
  }

  protected virtual void columnFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnSetMaint.ColumnProperty row = (RMColumnSetMaint.ColumnProperty) e.Row;
    if (row == null || row.Name == null)
      return;
    object returnState = e.ReturnState;
    RMColumn data = (RMColumn) this.Column.Select((object) code);
    this.ColumnsByCode.Cache.RaiseRowSelected((object) data);
    e.ReturnState = this.ColumnsByCode.Cache.GetStateExt((object) data, row.Name);
  }

  protected virtual void columnFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    string code)
  {
    RMColumnSetMaint.ColumnProperty row = (RMColumnSetMaint.ColumnProperty) e.Row;
    if (row == null)
      return;
    RMColumn rmColumn = (RMColumn) this.Column.Select((object) code);
    if (rmColumn == null)
      return;
    this.ColumnsByCode.Cache.SetValueExt((object) rmColumn, row.Name, e.NewValue);
    this.ColumnsByCode.Cache.MarkUpdated((object) rmColumn);
    this.ColumnsByCode.Cache.IsDirty = true;
  }

  protected virtual void columnTranslationsFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnSetMaint.ColumnProperty row = (RMColumnSetMaint.ColumnProperty) e.Row;
    if (row != null && row.Name != null)
    {
      object returnState = e.ReturnState;
      RMColumn data = (RMColumn) this.Column.Select((object) code);
      this.ColumnsByCode.Cache.RaiseRowSelected((object) data);
      e.ReturnState = this.ColumnsByCode.Cache.GetStateExt((object) data, row.Name + "Translations");
    }
    else
      e.ReturnState = this.ColumnsByCode.Cache.GetStateExt((object) null, "FormulaTranslations");
  }

  protected virtual void columnTranslationsFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    string code)
  {
    RMColumnSetMaint.ColumnProperty row = (RMColumnSetMaint.ColumnProperty) e.Row;
    if (row == null)
      return;
    RMColumn rmColumn = (RMColumn) this.Column.Select((object) code);
    if (rmColumn == null)
      return;
    this.ColumnsByCode.Cache.SetValueExt((object) rmColumn, row.Name + "Translations", e.NewValue);
    this.ColumnsByCode.Cache.MarkUpdated((object) rmColumn);
    this.ColumnsByCode.Cache.IsDirty = true;
  }

  protected virtual void headerStyleIDSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    PXResultset<RMColumn> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMColumn>) null;
    else
      pxResultset = this.Column.Select((object) code);
    RMColumn data1 = (RMColumn) pxResultset;
    RMColumnHeader data2 = (RMColumnHeader) null;
    if (row != null)
    {
      data2 = (RMColumnHeader) this.Header.Select((object) code, (object) row.HeaderNbr);
      this.Headers.Cache.RaiseRowSelected((object) data2);
    }
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object stateExt;
    if (row != null)
    {
      bool? isRowSet = row.IsRowSet;
      bool flag = true;
      if (isRowSet.GetValueOrDefault() == flag & isRowSet.HasValue && data1 != null)
      {
        stateExt = this.Column.Cache.GetStateExt((object) data1, "StyleID");
        goto label_9;
      }
    }
    stateExt = this.Headers.Cache.GetStateExt((object) data2, "StyleID");
label_9:
    selectingEventArgs.ReturnState = stateExt;
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.SetFieldName(code + "_StyleID");
    returnState.Visibility = PXUIVisibility.Dynamic;
    returnState.Visible = false;
  }

  protected virtual void headerFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    PXResultset<RMColumn> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMColumn>) null;
    else
      pxResultset = this.Column.Select((object) code);
    RMColumn data1 = (RMColumn) pxResultset;
    RMColumnHeader data2 = (RMColumnHeader) null;
    if (row != null)
    {
      data2 = (RMColumnHeader) this.Header.Select((object) code, (object) row.HeaderNbr);
      this.Headers.Cache.RaiseRowSelected((object) data2);
    }
    PXFieldState stateExt = this.Headers.Cache.GetStateExt((object) data2, "Formula") as PXFieldState;
    string str = stateExt.Value as string;
    e.ReturnState = (object) stateExt;
    bool? isRowSet;
    if (row != null)
    {
      isRowSet = row.IsRowSet;
      bool flag = false;
      if (isRowSet.GetValueOrDefault() == flag & isRowSet.HasValue && str != null)
      {
        if (this.Caches[typeof (RMReport)] != null)
        {
          try
          {
            ARmReportNode reportNode = this.GetReportNode();
            e.ReturnValue = ARmExprParser.Eval(str, reportNode);
            if (e.ReturnValue is string)
            {
              if (!string.IsNullOrEmpty(e.ReturnValue as string))
                goto label_18;
            }
            e.ReturnValue = ARmExprParser.Eval(str, reportNode);
            e.ReturnValue = (object) str;
            goto label_18;
          }
          catch (Exception ex)
          {
            if (this.Parameter.View.Answer == WebDialogResult.None)
            {
              if (this.NewColumnSetPanel.View.Answer == WebDialogResult.None)
              {
                stateExt.ErrorLevel = PXErrorLevel.Error;
                stateExt.Error = ex.Message;
                goto label_18;
              }
              goto label_18;
            }
            goto label_18;
          }
        }
      }
    }
    if (row != null)
    {
      isRowSet = row.IsRowSet;
      bool flag = true;
      if (isRowSet.GetValueOrDefault() == flag & isRowSet.HasValue && data1 != null)
        e.ReturnState = this.Column.Cache.GetStateExt((object) data1, "Preview");
    }
label_18:
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.SetFieldName(code);
    returnState.DisplayName = code;
    returnState.Visibility = PXUIVisibility.Dynamic;
  }

  protected virtual void headerTranslationsFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string code)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    RMColumnHeader data = (RMColumnHeader) null;
    if (row != null)
    {
      data = (RMColumnHeader) this.Header.Select((object) code, (object) row.HeaderNbr);
      this.Headers.Cache.RaiseRowSelected((object) data);
    }
    e.ReturnState = this.Headers.Cache.GetStateExt((object) data, "FormulaTranslations");
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.SetFieldName(code + "Translations");
    returnState.Visibility = PXUIVisibility.Dynamic;
  }

  protected virtual void headerFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    string code)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    if (row == null)
      return;
    if (code == row.ColumnCode)
    {
      this.Headers.Cache.SetValueExt((object) row, "Formula", e.NewValue);
    }
    else
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) this.Header.Select((object) code, (object) row.HeaderNbr);
      if (rmColumnHeader != null)
      {
        this.Headers.Cache.SetValueExt((object) rmColumnHeader, "Formula", e.NewValue);
        this.Headers.Cache.MarkUpdated((object) rmColumnHeader);
      }
    }
    this.Headers.Cache.IsDirty = true;
  }

  protected virtual void headerTranslationsFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    string code)
  {
    RMColumnHeader row = (RMColumnHeader) e.Row;
    if (row == null)
      return;
    if (code == row.ColumnCode)
    {
      this.Headers.Cache.SetValueExt((object) row, "FormulaTranslations", e.NewValue);
    }
    else
    {
      RMColumnHeader rmColumnHeader = (RMColumnHeader) this.Header.Select((object) code, (object) row.HeaderNbr);
      if (rmColumnHeader != null)
      {
        this.Headers.Cache.SetValueExt((object) rmColumnHeader, "FormulaTranslations", e.NewValue);
        this.Headers.Cache.MarkUpdated((object) rmColumnHeader);
      }
    }
    this.Headers.Cache.IsDirty = true;
  }

  private static object GetProperty(object obj, string property)
  {
    PropertyInfo property1 = obj.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
    return property1 != (PropertyInfo) null ? property1.GetValue(obj, (object[]) null) : (object) null;
  }

  private static void SetProperty(object obj, string property, object value)
  {
    PropertyInfo property1 = obj.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
    if (!(property1 != (PropertyInfo) null))
      return;
    property1.SetValue(obj, value, (object[]) null);
  }

  private static void SwapItems<T>(PXCache cache, T first, T second, string fieldNameChange) where T : IBqlTable
  {
    object copy = cache.CreateCopy((object) first);
    cache.RestoreCopy((object) first, (object) second);
    RMColumnSetMaint.SetProperty((object) first, fieldNameChange, RMColumnSetMaint.GetProperty(copy, fieldNameChange));
    RMColumnSetMaint.SetProperty((object) first, cache._NoteIDName, RMColumnSetMaint.GetProperty(copy, cache._NoteIDName));
    object property1 = RMColumnSetMaint.GetProperty((object) second, fieldNameChange);
    object property2 = RMColumnSetMaint.GetProperty((object) second, cache._NoteIDName);
    cache.RestoreCopy((object) second, copy);
    RMColumnSetMaint.SetProperty((object) second, fieldNameChange, property1);
    RMColumnSetMaint.SetProperty((object) second, cache._NoteIDName, property2);
    if (PXDBLocalizableStringAttribute.IsEnabled)
    {
      foreach (string localizableField in RMColumnSetMaint.GetLocalizableFields(cache))
      {
        string fieldName = localizableField + "Translations";
        string[] valueExt1 = cache.GetValueExt((object) first, fieldName) as string[];
        string[] valueExt2 = cache.GetValueExt((object) second, fieldName) as string[];
        cache.SetValueExt((object) second, fieldName, (object) valueExt1);
        cache.SetValueExt((object) first, fieldName, (object) valueExt2);
      }
    }
    cache.Update((object) first);
    cache.Update((object) second);
  }

  private static IEnumerable<string> GetLocalizableFields(PXCache cache)
  {
    return cache.BqlFields.Where<System.Type>((Func<System.Type, bool>) (f => cache.GetAttributes(f.Name).OfType<PXDBLocalizableStringAttribute>().Any<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (a => a.MultiLingual)))).Select<System.Type, string>((Func<System.Type, string>) (t => t.Name));
  }

  private static void ShiftColumnRange(RMColumnHeader left, RMColumnHeader right)
  {
    left.StartColumn = RMColumnCodeAttribute.ShiftCode(left.StartColumn, false);
    left.EndColumn = RMColumnCodeAttribute.ShiftCode(left.EndColumn, false);
    right.StartColumn = RMColumnCodeAttribute.ShiftCode(right.StartColumn, true);
    right.EndColumn = RMColumnCodeAttribute.ShiftCode(right.EndColumn, true);
  }

  public override bool IsDirty => base.IsDirty;

  protected virtual bool TryAddHandlers(string columnCode)
  {
    if (this.Properties.Cache.Fields.Contains(columnCode))
      return false;
    this.Properties.Cache.Fields.Add(columnCode);
    this.FieldSelecting.AddHandler(typeof (RMColumnSetMaint.ColumnProperty), columnCode, (PXFieldSelecting) ((dsender, de) => this.columnFieldSelecting(dsender, de, columnCode)));
    this.FieldUpdating.AddHandler(typeof (RMColumnSetMaint.ColumnProperty), columnCode, (PXFieldUpdating) ((dsender, de) => this.columnFieldUpdating(dsender, de, columnCode)));
    this.Properties.Cache.Fields.Add(columnCode + "_Text");
    this.FieldSelecting.AddHandler(typeof (RMColumnSetMaint.ColumnProperty), columnCode + "_Text", (PXFieldSelecting) ((dsender, de) => this.columnTextSelecting(dsender, de, columnCode)));
    if (PXDBLocalizableStringAttribute.IsEnabled)
    {
      this.Properties.Cache.Fields.Add(columnCode + "Translations");
      this.FieldSelecting.AddHandler(typeof (RMColumnSetMaint.ColumnProperty), columnCode + "Translations", (PXFieldSelecting) ((dsender, de) => this.columnTranslationsFieldSelecting(dsender, de, columnCode)));
      this.FieldUpdating.AddHandler(typeof (RMColumnSetMaint.ColumnProperty), columnCode + "Translations", (PXFieldUpdating) ((dsender, de) => this.columnTranslationsFieldUpdating(dsender, de, columnCode)));
    }
    if (!this.Headers.Cache.Fields.Contains(columnCode))
    {
      this.Headers.Cache.Fields.Add(columnCode);
      this.FieldSelecting.AddHandler(typeof (RMColumnHeader), columnCode, (PXFieldSelecting) ((dsender, de) => this.headerFieldSelecting(dsender, de, columnCode)));
      this.FieldUpdating.AddHandler(typeof (RMColumnHeader), columnCode, (PXFieldUpdating) ((dsender, de) => this.headerFieldUpdating(dsender, de, columnCode)));
      this.Headers.Cache.Fields.Add(columnCode + "_StyleID");
      this.FieldSelecting.AddHandler(typeof (RMColumnHeader), columnCode + "_StyleID", (PXFieldSelecting) ((dsender, de) => this.headerStyleIDSelecting(dsender, de, columnCode)));
      if (PXDBLocalizableStringAttribute.IsEnabled)
      {
        this.Headers.Cache.Fields.Add(columnCode + "Translations");
        this.FieldSelecting.AddHandler(typeof (RMColumnHeader), columnCode + "Translations", (PXFieldSelecting) ((dsender, de) => this.headerTranslationsFieldSelecting(dsender, de, columnCode)));
        this.FieldUpdating.AddHandler(typeof (RMColumnHeader), columnCode + "Translations", (PXFieldUpdating) ((dsender, de) => this.headerTranslationsFieldUpdating(dsender, de, columnCode)));
      }
    }
    return true;
  }

  [Serializable]
  public class ColumnProperty : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Name;
    protected string _DisplayName;
    protected short? _Order;

    [PXString(IsKey = true)]
    [PXUIField(Visible = false, Enabled = false)]
    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXString]
    [PXUIField(Enabled = false, DisplayName = "Attributes")]
    public string DisplayName
    {
      get => this._DisplayName;
      set => this._DisplayName = value;
    }

    [PXShort]
    [PXUIField(Visible = false, Enabled = false)]
    public virtual short? Order
    {
      get => this._Order;
      set => this._Order = value;
    }

    public abstract class order : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      RMColumnSetMaint.ColumnProperty.order>
    {
    }
  }

  public class ColumnCodeA : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RMColumnSetMaint.ColumnCodeA>
  {
    public ColumnCodeA()
      : base("  A")
    {
    }
  }

  [Serializable]
  public class RMHeaderStyle : RMStyle
  {
    protected string _StartColumn;
    protected string _EndColumn;

    [PXDBIdentity(IsKey = true)]
    [PXUIField]
    public override int? StyleID
    {
      get => this._StyleID;
      set => this._StyleID = value;
    }

    [RMShiftCodeString(3, InputMask = ">aaa")]
    [PXUIField(DisplayName = "Column Range")]
    public virtual string StartColumn
    {
      get => this._StartColumn;
      set => this._StartColumn = value;
    }

    [RMShiftCodeString(3, InputMask = ">aaa")]
    [PXUIField(DisplayName = "End Column")]
    public virtual string EndColumn
    {
      get => this._EndColumn;
      set => this._EndColumn = value;
    }

    public new abstract class styleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RMColumnSetMaint.RMHeaderStyle.styleID>
    {
    }

    public abstract class startColumn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.RMHeaderStyle.startColumn>
    {
    }

    public abstract class endColumn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.RMHeaderStyle.endColumn>
    {
    }
  }

  [Serializable]
  public class ParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "New Code", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
    public virtual string NewColumnSetCode { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
    public virtual string Description { get; set; }

    [PXString]
    public virtual string HeaderCell { get; set; }

    [PXString]
    public virtual string ColumnCell { get; set; }

    [PXInt]
    public virtual int? HeaderRow { get; set; }

    [PXShort]
    public virtual short? HeaderNbr { get; set; }

    [PXString]
    public virtual string CopiedFormattingColumn { get; set; }

    public virtual int? CopiedStyle { get; set; }

    public virtual short? CopiedFormattingHeaderNbr { get; set; }

    public virtual int HeaderActiveCellIndex { get; set; }

    public virtual int ColumnActiveCellIndex { get; set; }

    public virtual int HeaderCellPredAIndex { get; set; }

    public virtual int ColumnCellPredAIndex { get; set; }

    public abstract class newColumnSetCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.newColumnSetCode>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.description>
    {
    }

    public abstract class headerCell : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.headerCell>
    {
    }

    public abstract class columnCell : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.columnCell>
    {
    }

    public abstract class headerRow : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.headerRow>
    {
    }

    public abstract class headerNbr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.headerNbr>
    {
    }

    public abstract class copiedFormattingColumn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMColumnSetMaint.ParamFilter.copiedFormattingColumn>
    {
    }
  }
}
