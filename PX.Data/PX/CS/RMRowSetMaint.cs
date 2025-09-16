// Decompiled with JetBrains decompiler
// Type: PX.CS.RMRowSetMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Localization;
using PX.Data.Reports.Utils;
using PX.Reports.ARm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.CS;

public class RMRowSetMaint : PXGraph<
#nullable disable
RMRowSetMaint, RMRowSet>
{
  public PXSelect<RMRowSet> RowSet;
  public PXSelect<RMRow, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>, OrderBy<Asc<RMRow.rowCode>>> Rows;
  public PXSelect<RMRowSet> RowSetOrdered;
  public RMRowSetMaint.RMRowsOrdered RowsOrdered;
  public PXSelectJoin<Note, InnerJoin<RMRow, On<RMRow.noteID, Equal<Note.noteID>>>, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>> Notes;
  public PXSelectJoin<RMStyle, InnerJoin<RMRow, On<RMRow.styleID, Equal<RMStyle.styleID>>>, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>> Styles;
  public PXSelectJoin<RMDataSource, InnerJoin<RMRow, On<RMRow.dataSourceID, Equal<RMDataSource.dataSourceID>>>, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>> DataSources;
  public PXSelect<RMRow, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>, And<RMRow.rowCode, Equal<Current<RMRow.rowCode>>>>> CurrentRow;
  public PXSelect<RMRow, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>, And<RMRow.rowCode, Equal<Required<RMRow.rowCode>>>>> RowByID;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Current<RMRow.dataSourceID>>>> CurrentRowDataSource;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Current<RMRow.styleID>>>> CurrentRowStyle;
  public PXSelectJoin<RMReport, LeftJoin<RMRowSet, On<RMReport.rowSetCode, Equal<RMRowSet.rowSetCode>>>, Where<RMRowSet.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>> Reports;
  public PXFilter<RMRowSetMaint.ParamFilter> Parameter;
  public PXFilter<RenumberingFilter> fltNumberingStep;
  public PXFilter<RMNewRowSetPanel> NewRowSetPanel;
  protected Dictionary<string, string> _sortOrderToRowCode = new Dictionary<string, string>();
  protected Dictionary<string, string> _rowCodeToSortOrder = new Dictionary<string, string>();
  internal const string FormulaTranslationsField = "FormulaTranslations";
  private Dictionary<short?, string> _oldRowCodes;
  private const string _patternRange = "(sum|sort|sortd|roundingdiff)\\s*\\(\\s*'(\\d+)'\\s*,\\s*'(\\d+)'";
  private bool _ignoreRowsDeleteing;
  public PXAction<RMRowSet> copyRowSet;
  public PXAction<RMRowSet> copyStyle;
  public PXAction<RMRowSet> pasteStyle;
  public PXAction<RMColumnSet> copyFormatting;
  public PXAction<RMColumnSet> pasteFormatting;
  public PXAction<RMColumnSet> resetFormatting;
  public PXAction<RMRowSet> Renumber;
  public PXAction<RMRow> CopyRows;
  public PXAction<RMRow> CutRows;
  public PXAction<RMRow> PasteRows;
  public PXAction<RMRowSet> InsertRow;

  [InjectDependency]
  private ILocalizableFieldService LocalizableFieldService { get; set; }

  [InjectDependency]
  private ISiteMapUITypeProvider SiteMapUITypeProvider { get; set; }

  private bool IsNewUi()
  {
    return string.Equals(this.SiteMapUITypeProvider.GetUIByScreenId(PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(typeof (RMRowSetMaint).FullName).ScreenID), "T", StringComparison.InvariantCultureIgnoreCase);
  }

  public virtual IEnumerable rowSetOrdered()
  {
    if (PXView.Searches?[0] == null)
      return (IEnumerable) null;
    this.Parameter.Current.AreRowsOrdered = new bool?(true);
    PXView pxView = new PXView((PXGraph) this, true, this.RowSetOrdered.View.BqlSelect);
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
    RMRowSet rmRowSet = pxView.Select(currents, parameters, searches, sortColumns, descendings, filters, ref local1, 1, ref local2).RowCast<RMRowSet>().FirstOrDefault<RMRowSet>();
    if (rmRowSet == null || string.IsNullOrEmpty(rmRowSet?.RowSetCode))
      return (IEnumerable) null;
    if (rmRowSet?.RowSetCode == this.RowSetOrdered.Current?.RowSetCode)
      return (IEnumerable) new RMRowSet[1]
      {
        this.RowSetOrdered.Current
      };
    this.RowSetOrdered.Current = rmRowSet;
    this.RowsOrdered.Cache.Clear();
    IEnumerable<RMRow> rmRows = (IEnumerable<RMRow>) ((object) this.RowsOrdered.View.SelectMultiBound((object[]) null, (object[]) null).RowCast<RMRow>() ?? (object) Array.Empty<RMRow>());
    HashSet<Guid> guidSet1 = new HashSet<Guid>();
    foreach (PXResult<Note, RMRow> pxResult in this.Notes.View.SelectMultiBound((object[]) null, (object[]) null))
    {
      Note note = (Note) pxResult;
      RMRow rmRow = (RMRow) pxResult;
      Guid? noteId = note.NoteID;
      if (noteId.HasValue)
      {
        PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.StoreResult((PXGraph) this, new List<object>()
        {
          (object) note
        }, PXQueryParameters.ExplicitParameters((object) note.NoteID));
        HashSet<Guid> guidSet2 = guidSet1;
        noteId = note.NoteID;
        Guid guid = noteId.Value;
        guidSet2.Add(guid);
      }
    }
    foreach (RMRow rmRow in rmRows)
    {
      Guid? noteId = rmRow.NoteID;
      if (noteId.HasValue)
      {
        HashSet<Guid> guidSet3 = guidSet1;
        noteId = rmRow.NoteID;
        Guid guid = noteId.Value;
        if (!guidSet3.Contains(guid))
        {
          List<object> selectResult = new List<object>();
          object[] objArray = new object[1];
          noteId = rmRow.NoteID;
          objArray[0] = (object) noteId.Value;
          PXQueryParameters queryParameters = PXQueryParameters.ExplicitParameters(objArray);
          PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.StoreResult((PXGraph) this, selectResult, queryParameters);
        }
      }
    }
    this.DataSources.View.SelectMultiBound((object[]) null, (object[]) null);
    IEnumerable<RMRow> cachedRows = this.GetCachedRows();
    if ((cachedRows.Count<RMRow>() == 0 ? 0 : cachedRows.Max<RMRow>((Func<RMRow, int>) (x => int.Parse(x.RowCode)))) != cachedRows.Count<RMRow>())
    {
      bool isDirty = this.RowsOrdered.Cache.IsDirty;
      this.ProcessRenumber(1, cachedRows.Count<RMRow>().ToString().Length, new int?(1));
      this.RowsOrdered.Cache.IsDirty = isDirty;
    }
    this.CheckCycles();
    return (IEnumerable) new RMRowSet[1]
    {
      this.RowSetOrdered.Current
    };
  }

  protected virtual IEnumerable styles()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = false;
    pxDelegateResult.IsResultTruncated = false;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.AddRange((IEnumerable<object>) new PXView((PXGraph) this, false, this.Styles.View.BqlSelect).SelectMultiBound((object[]) null, (object[]) null).RowCast<RMStyle>());
    pxDelegateResult.AddRange((IEnumerable<object>) this.Styles.Cache.Cached.RowCast<RMStyle>().Where<RMStyle>((Func<RMStyle, bool>) (x =>
    {
      if (!EnumerableExtensions.IsNotIn<PXEntryStatus>(this.Styles.Cache.GetStatus((object) x), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return false;
      int? styleId = x.StyleID;
      int num = 0;
      return styleId.GetValueOrDefault() < num & styleId.HasValue;
    })));
    return (IEnumerable) pxDelegateResult;
  }

  protected IEnumerable<RMRow> GetCachedRows()
  {
    return this.RowsOrdered.Cache.Cached.RowCast<RMRow>().Where<RMRow>((Func<RMRow, bool>) (x => EnumerableExtensions.IsNotIn<PXEntryStatus>(this.RowsOrdered.Cache.GetStatus((object) x), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted) && x.RowSetCode == this.RowSet.Current.RowSetCode));
  }

  protected virtual void CheckCycles()
  {
    IEnumerable<RMRow> cachedRows = this.GetCachedRows();
    Dictionary<int, RMRow> dictionary = cachedRows.ToDictionary<RMRow, int, RMRow>((Func<RMRow, int>) (x => int.Parse(x.RowCode)), (Func<RMRow, RMRow>) (x => x));
    ReportCycleDetector reportCycleDetector = new ReportCycleDetector();
    foreach (RMRow rmRow in cachedRows)
    {
      rmRow.InCycle = new bool?(false);
      PXUIFieldAttribute.SetError<RMRow.preview>(this.RowsOrdered.Cache, (object) rmRow, (string) null);
      reportCycleDetector.AddNode(int.Parse(rmRow.RowCode), this.ReferencesForRow(rmRow));
    }
    foreach (IEnumerable<int> cycle in reportCycleDetector.FindCycles())
    {
      foreach (int key in cycle)
      {
        dictionary[key].InCycle = new bool?(true);
        PXUIFieldAttribute.SetWarning<RMRow.preview>(this.RowsOrdered.Cache, (object) dictionary[key], "Circular Reference");
      }
    }
  }

  protected virtual IEnumerable<int> ReferencesForRow(RMRow row)
  {
    HashSet<int> intSet = new HashSet<int>();
    string formula = row.Formula;
    if (string.IsNullOrEmpty(formula))
      return (IEnumerable<int>) intSet;
    if ((row.RowType.HasValue ? (int) row.RowType.Value : 0) != 3)
      return (IEnumerable<int>) intSet;
    foreach (Match match in Regex.Matches(formula, "(sum|sort|sortd|roundingdiff)\\s*\\(\\s*'(\\d+)'\\s*,\\s*'(\\d+)'", RegexOptions.IgnoreCase))
    {
      for (int index = int.Parse(match.Groups[2].Value); index < int.Parse(match.Groups[3].Value); ++index)
        intSet.Add(index);
    }
    string pattern = "'(\\d+)'|@(\\d+)";
    foreach (Match match in Regex.Matches(formula, pattern))
      intSet.Add(int.Parse(match.Groups[match.Groups[1].Length > 0 ? 1 : 2].Value));
    return (IEnumerable<int>) intSet;
  }

  [PXInsertButton(Tooltip = "Add New Record (Ctrl+Ins)")]
  [PXUIField(DisplayName = "Add New Record (Ctrl+Ins)")]
  protected virtual IEnumerable Insert(PXAdapter adapter)
  {
    if (this.IsNewUi())
    {
      if (this.NewRowSetPanel.AskExt() != WebDialogResult.OK)
        return adapter.Get();
      RMMaintHelper.CheckKeyAndDescription<RMRowSet, RMRowSet.rowSetCode, RMNewRowSetPanel.rowSetCode, RMNewRowSetPanel.description>((PXGraph) this, this.NewRowSetPanel.Cache, (object) this.NewRowSetPanel.Current);
      RMRowSetMaint instance = PXGraph.CreateInstance<RMRowSetMaint>();
      RMRowSet rmRowSet = instance.RowSetOrdered.Insert();
      rmRowSet.RowSetCode = this.NewRowSetPanel.Current.RowSetCode;
      rmRowSet.Type = this.NewRowSetPanel.Current.Type;
      rmRowSet.Description = this.NewRowSetPanel.Current.Description;
      instance.RowSetOrdered.Current = rmRowSet;
      instance.RowSetOrdered.Cache.Update((object) rmRowSet);
      instance.RowSetOrdered.Cache.IsDirty = false;
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
    }
    else
      PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<RMRowSetMaint>(), PXRedirectHelper.WindowMode.InlineWindow);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Copy Row Set", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(ConfirmationMessage = "Any unsaved changes will be discarded.", ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable CopyRowSet(PXAdapter adapter)
  {
    RMRowSetMaint graph = this;
    if (graph.Parameter.View.Answer == WebDialogResult.None)
      graph.Parameter.Current.Description = graph.RowSet.Current.Description;
    if (graph.RowSet.Current != null && graph.Parameter.AskExt() == WebDialogResult.OK)
    {
      RMMaintHelper.CheckKeyAndDescription<RMRowSet, RMRowSet.rowSetCode, RMRowSetMaint.ParamFilter.newRowSetCode, RMRowSetMaint.ParamFilter.description>((PXGraph) graph, graph.Parameter.Cache, (object) graph.Parameter.Current);
      string newRowSetCode = graph.Parameter.Current.NewRowSetCode;
      string description = graph.Parameter.Current.Description;
      foreach (RMRowSet rmRowSet in graph.Cancel.Press(adapter))
        ;
      List<PXResult<RMRow>> list = graph.Rows.Select().ToList<PXResult<RMRow>>();
      RMRowSet copy1 = (RMRowSet) graph.RowSet.Cache.CreateCopy((object) graph.RowSet.Current);
      copy1.RowSetCode = newRowSetCode;
      if (!string.IsNullOrEmpty(description))
        copy1.Description = description;
      copy1.NoteID = new Guid?();
      RMRowSet rmRowSet1 = graph.RowSet.Insert(copy1);
      if (rmRowSet1 != null)
      {
        rmRowSet1.RowCntr = new short?((short) 0);
        PXSelectBase<RMDataSource> pxSelectBase1 = (PXSelectBase<RMDataSource>) new PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>>((PXGraph) graph);
        PXSelectBase<RMStyle> pxSelectBase2 = (PXSelectBase<RMStyle>) new PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>((PXGraph) graph);
        bool flag1 = graph.LocalizableFieldService.IsFieldEnabled("RMRow", "Formula");
        bool flag2 = graph.LocalizableFieldService.IsFieldEnabled("RMRow", "Description");
        string fieldName = "DescriptionTranslations";
        foreach (PXResult<RMRow> pxResult in list)
        {
          RMRow row1 = (RMRow) pxResult;
          string[] strArray1 = (string[]) null;
          string[] strArray2 = (string[]) null;
          if (flag1)
            strArray1 = graph.Rows.GetValueExt((object) row1, "FormulaTranslations") as string[];
          if (flag2)
            strArray2 = graph.Rows.GetValueExt((object) row1, fieldName) as string[];
          graph.Rows.Cache.Remove((object) row1);
          row1.RowSetCode = (string) null;
          row1.RowNbr = new short?();
          row1.NoteID = new Guid?();
          RMRow row2 = graph.Rows.Insert(row1);
          if (row2 != null)
          {
            if (strArray1 != null)
              graph.Rows.SetValueExt((object) row2, "FormulaTranslations", (object) strArray1);
            if (strArray2 != null)
              graph.Rows.SetValueExt((object) row2, fieldName, (object) strArray2);
            RMDataSource copy2 = (RMDataSource) pxSelectBase1.Select((object) row1.DataSourceID);
            if (copy2 != null && pxSelectBase1.Current != null)
            {
              pxSelectBase1.Cache.RestoreCopy((object) pxSelectBase1.Current, (object) copy2);
              pxSelectBase1.Current.DataSourceID = row2.DataSourceID;
            }
            RMStyle copy3 = (RMStyle) pxSelectBase2.Select((object) row1.StyleID);
            if (copy3 != null && pxSelectBase2.Current != null)
            {
              pxSelectBase2.Cache.RestoreCopy((object) pxSelectBase2.Current, (object) copy3);
              pxSelectBase2.Current.StyleID = row2.StyleID;
            }
          }
        }
        yield return (object) rmRowSet1;
        yield break;
      }
    }
    foreach (RMRowSet rmRowSet in adapter.Get())
      yield return (object) rmRowSet;
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Style")]
  protected virtual IEnumerable CopyStyle(PXAdapter adapter)
  {
    this.Parameter.Current.CopiedStyle = new int?();
    if (this.Rows.Current != null)
      this.Parameter.Current.CopiedStyle = this.Rows.Current.StyleID;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Style")]
  protected virtual IEnumerable PasteStyle(PXAdapter adapter)
  {
    this.ProcessPasteStyle();
    return adapter.Get();
  }

  protected virtual void ProcessPasteStyle()
  {
    if (!this.Parameter.Current.CopiedStyle.HasValue)
      return;
    RMRow current = this.Rows.Current;
    if ((current != null ? (!current.StyleID.HasValue ? 1 : 0) : 1) != 0)
      return;
    RMStyle rmStyle1 = (RMStyle) PXSelectBase<RMStyle, PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>.Config>.Select((PXGraph) this, (object) this.Parameter.Current.CopiedStyle);
    if (rmStyle1 == null)
      return;
    RMStyle rmStyle2 = (RMStyle) PXSelectBase<RMStyle, PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>.Config>.Select((PXGraph) this, (object) this.Rows.Current.StyleID);
    if (rmStyle2 == null)
      return;
    rmStyle2.BackColor = rmStyle1.BackColor;
    rmStyle2.Color = rmStyle1.Color;
    rmStyle2.FontName = rmStyle1.FontName;
    rmStyle2.FontSize = rmStyle1.FontSize;
    rmStyle2.FontSizeType = rmStyle1.FontSizeType;
    rmStyle2.FontStyle = rmStyle1.FontStyle;
    rmStyle2.TextAlign = rmStyle1.TextAlign;
    if (this.Caches[typeof (RMStyle)].GetStatus((object) rmStyle2) != PXEntryStatus.Notchanged)
      return;
    this.Caches[typeof (RMStyle)].SetStatus((object) rmStyle2, PXEntryStatus.Updated);
    this.Caches[typeof (RMStyle)].IsDirty = true;
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Formatting", Visible = false)]
  protected virtual IEnumerable CopyFormatting(PXAdapter adapter)
  {
    this.CurrentRow.Current = this.CurrentRow.SelectSingle();
    this.Parameter.Current.CopiedRow = this.CurrentRow.Current.RowCode;
    this.Parameter.Current.CopiedStyle = this.CurrentRow.Current.StyleID;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Paste Formatting", Visible = false)]
  protected virtual IEnumerable PasteFormatting(PXAdapter adapter)
  {
    this.ProcessPasteStyle();
    RMRow rmRow = this.RowByID.SelectSingle((object) this.Parameter.Current.CopiedRow);
    this.CurrentRow.Current = this.CurrentRow.SelectSingle();
    this.CurrentRow.Current.LineStyle = rmRow.LineStyle;
    this.CurrentRow.Current.Format = rmRow.Format;
    this.CurrentRow.Current.PrintControl = rmRow.PrintControl;
    this.CurrentRow.Current.PageBreak = rmRow.PageBreak;
    this.CurrentRow.Current.Height = rmRow.Height;
    this.CurrentRow.Current.Indent = rmRow.Indent;
    this.Rows.Cache.Update((object) this.CurrentRow.Current);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Reset Formatting")]
  protected virtual IEnumerable ResetFormatting(PXAdapter adapter)
  {
    RMStyle data = this.CurrentRowStyle.SelectSingle();
    RMRow rmRow = this.CurrentRow.SelectSingle();
    if (data == null || rmRow == null)
      return adapter.Get();
    this.Styles.Cache.SetDefaultExt((object) data, "BackColor");
    this.Styles.Cache.SetDefaultExt((object) data, "Color");
    this.Styles.Cache.SetDefaultExt((object) data, "FontName");
    this.Styles.Cache.SetDefaultExt((object) data, "FontSize");
    this.Styles.Cache.SetDefaultExt((object) data, "FontSizeType");
    this.Styles.Cache.SetDefaultExt((object) data, "FontStyle");
    this.Styles.Cache.SetDefaultExt((object) data, "TextAlign");
    this.Styles.Cache.Update((object) data);
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "LineStyle");
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "Format");
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "PrintControl");
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "PageBreak");
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "Height");
    this.Rows.Cache.SetDefaultExt((object) this.CurrentRow.Current, "Indent");
    this.Rows.Cache.Update((object) this.CurrentRow.Current);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Renumber rows.", ImageKey = "Renumber")]
  [PXUIField(DisplayName = "Renumber")]
  protected virtual IEnumerable renumber(PXAdapter adapter)
  {
    int valueOrDefault1 = this.fltNumberingStep.Current.Step.GetValueOrDefault();
    int valueOrDefault2 = this.fltNumberingStep.Current.MaskLength.GetValueOrDefault();
    if (valueOrDefault1 == 0)
      return adapter.Get();
    this.ProcessRenumber(valueOrDefault1, valueOrDefault2);
    return adapter?.Get();
  }

  protected void ProcessRenumber(int step, int maskLength, int? start = null)
  {
    this.Styles.View.SelectMultiBound((object[]) null, (object[]) null);
    this.DataSources.View.SelectMultiBound((object[]) null, (object[]) null);
    Dictionary<string, string> renumbered = new Dictionary<string, string>();
    int? nullable1 = start;
    foreach (PXResult<RMRow> pxResult in this.Rows.Select())
    {
      RMRow rmRow = (RMRow) pxResult;
      string rowCode = rmRow.RowCode;
      if (!nullable1.HasValue)
      {
        int result;
        if (!int.TryParse(rmRow.RowCode, out result))
          throw new PXException("The Code column contains at least one non-integer value.");
        nullable1 = new int?(result);
      }
      rmRow.RowCode = nullable1?.ToString($"D{maskLength}");
      renumbered[rowCode] = rmRow.RowCode;
      this.Rows.Update(rmRow);
      int? nullable2 = nullable1;
      int num = step;
      nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + num) : new int?();
    }
    this.UpdateValues(renumbered);
  }

  private void UpdateValues(Dictionary<string, string> renumbered)
  {
    PXCache cach = this.Caches[typeof (RMRow)];
    SortedList<string, string> renumbered1 = new SortedList<string, string>((IDictionary<string, string>) renumbered);
    foreach (RMRow cachedRow in this.GetCachedRows())
    {
      string formula1 = cachedRow.Formula;
      string linkedRowCode = cachedRow.LinkedRowCode;
      string baseRowCode = cachedRow.BaseRowCode;
      cachedRow.Formula = this.UpdateFormula(renumbered1, cachedRow.Formula);
      string formula2 = cachedRow.Formula;
      bool flag = formula1 != formula2;
      if (this.Rows.GetValueExt((object) cachedRow, "FormulaTranslations") is string[] valueExt)
      {
        string[] strArray = new string[valueExt.Length];
        for (int index = 0; index < valueExt.Length; ++index)
        {
          strArray[index] = this.UpdateFormula(renumbered1, valueExt[index]);
          if (valueExt[index] != strArray[index])
            flag = true;
        }
        if (flag)
          cach.SetValueExt((object) cachedRow, "FormulaTranslations", (object) strArray);
      }
      cachedRow.LinkedRowCode = this.SearchMappingOrBound(renumbered1, cachedRow.LinkedRowCode, RMRowSetMaint.BoundType.NA);
      cachedRow.BaseRowCode = this.SearchMappingOrBound(renumbered1, cachedRow.BaseRowCode, RMRowSetMaint.BoundType.NA);
      if (flag || linkedRowCode != cachedRow.LinkedRowCode || baseRowCode != cachedRow.BaseRowCode)
        cach.Update((object) cachedRow);
    }
  }

  private string SearchMappingOrBound(
    SortedList<string, string> renumbered,
    string item,
    RMRowSetMaint.BoundType bound)
  {
    if (string.IsNullOrEmpty(item))
      return item;
    if (bound == RMRowSetMaint.BoundType.NA)
    {
      string str;
      return renumbered.TryGetValue(item, out str) ? str : item;
    }
    int num1 = 0;
    int num2 = renumbered.Count - 1;
    int num3 = 0;
    int index1 = 0;
    while (num1 <= num2)
    {
      index1 = num1 + (num2 - num1) / 2;
      num3 = string.Compare(renumbered.Keys[index1], item, StringComparison.Ordinal);
      if (num3 == 0)
        return renumbered.Values[index1];
      if (num3 < 0)
        num1 = index1 + 1;
      else
        num2 = index1 - 1;
    }
    if (bound == RMRowSetMaint.BoundType.Lower)
    {
      int index2 = num3 < 0 ? index1 : System.Math.Max(0, index1 - 1);
      return renumbered.Values[index2];
    }
    int index3 = num3 > 0 ? index1 : System.Math.Min(renumbered.Count - 1, index1 + 1);
    return renumbered.Values[index3];
  }

  private string UpdateFormula(SortedList<string, string> renumbered, string formula)
  {
    if (string.IsNullOrEmpty(formula))
      return formula;
    StringBuilder stringBuilder = new StringBuilder();
    SortedList<int, RMRowSetMaint.NumChunks> sortedList1 = new SortedList<int, RMRowSetMaint.NumChunks>();
    foreach (Match match in Regex.Matches(formula, "(sum|sort|sortd|roundingdiff)\\s*\\(\\s*'(\\d+)'\\s*,\\s*'(\\d+)'", RegexOptions.IgnoreCase))
    {
      SortedList<int, RMRowSetMaint.NumChunks> sortedList2 = sortedList1;
      int index1 = match.Groups[2].Index;
      RMRowSetMaint.NumChunks numChunks1 = new RMRowSetMaint.NumChunks();
      numChunks1.Length = match.Groups[2].Length;
      numChunks1.BoundType = RMRowSetMaint.BoundType.Upper;
      RMRowSetMaint.NumChunks numChunks2 = numChunks1;
      sortedList2.Add(index1, numChunks2);
      SortedList<int, RMRowSetMaint.NumChunks> sortedList3 = sortedList1;
      int index2 = match.Groups[3].Index;
      numChunks1 = new RMRowSetMaint.NumChunks();
      numChunks1.Length = match.Groups[3].Length;
      numChunks1.BoundType = RMRowSetMaint.BoundType.Lower;
      RMRowSetMaint.NumChunks numChunks3 = numChunks1;
      sortedList3.Add(index2, numChunks3);
    }
    string pattern = "'(\\d+)'|@(\\d+)|[A-Z]+(\\d+)";
    foreach (Match match in Regex.Matches(formula, pattern))
    {
      int[] numArray = new int[3]{ 1, 2, 3 };
      foreach (int groupnum in numArray)
      {
        Group group = match.Groups[groupnum];
        if (!sortedList1.ContainsKey(group.Index))
          sortedList1.Add(group.Index, new RMRowSetMaint.NumChunks()
          {
            Length = group.Length,
            BoundType = RMRowSetMaint.BoundType.NA
          });
      }
    }
    for (int index = 0; index < sortedList1.Count; ++index)
    {
      int key1 = sortedList1.Keys[index];
      int key2 = index == 0 ? 0 : sortedList1.Keys[index - 1];
      int startIndex = index == 0 ? 0 : key2 + sortedList1[key2].Length;
      stringBuilder.Append(formula.Substring(startIndex, key1 - startIndex));
      string str1 = formula.Substring(key1, sortedList1[key1].Length);
      string str2 = this.SearchMappingOrBound(renumbered, str1, sortedList1[key1].BoundType);
      stringBuilder.Append(str2);
    }
    if (sortedList1.Count > 0)
      stringBuilder.Append(formula.Substring(sortedList1.Keys[sortedList1.Count - 1] + sortedList1.Values[sortedList1.Count - 1].Length));
    else
      stringBuilder.Append(formula);
    return stringBuilder.ToString();
  }

  [PXButton(ImageKey = "Copy", Tooltip = "Copy selected records.")]
  [PXUIField(DisplayName = "Copy", Enabled = false, Visible = false)]
  public IEnumerable copyRows(PXAdapter adapter) => adapter.Get();

  [PXButton(ImageKey = "Cut", Tooltip = "Cut selected records.")]
  [PXUIField(DisplayName = "Cut", Enabled = false, Visible = false)]
  internal IEnumerable cutRows(PXAdapter adapter) => adapter.Get();

  [PXButton(ImageKey = "Paste", Tooltip = "Paste records.")]
  [PXUIField(DisplayName = "Paste", Enabled = false, Visible = false)]
  internal IEnumerable pasteRows(PXAdapter adapter) => adapter.Get();

  protected virtual void RMRowSet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.copyRowSet.SetEnabled(this.RowSet.Current != null && this.RowSet.Cache.GetStatus((object) this.RowSet.Current) != PXEntryStatus.Inserted && !string.IsNullOrEmpty(this.RowSet.Current.RowSetCode));
    this.pasteFormatting.SetEnabled(this.Parameter.Current.CopiedStyle.HasValue);
  }

  protected virtual void RMRow_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.CutRows.SetEnabled(false);
    this.CopyRows.SetEnabled(false);
    this.PasteRows.SetEnabled(false);
    this.UpdateRowFieldsVisibility();
  }

  protected virtual void RMRow_RowCode_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    foreach (PXResult<RMRow> pxResult in this.Rows.Select())
    {
      RMRow rmRow = (RMRow) pxResult;
      if (rmRow != e.Row && string.Compare(rmRow.RowCode, (string) e.NewValue, true) == 0)
        cache.RaiseExceptionHandling<RMRow.rowCode>(e.Row, e.NewValue, (Exception) new PXException("A record with the same value of the Code field already exists."));
    }
  }

  protected virtual void RMRow_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (e.Operation == PXDBOperation.Delete)
      return;
    RMRow row = (RMRow) e.Row;
    foreach (PXResult<RMRow> pxResult in this.Rows.Select())
    {
      RMRow rmRow = (RMRow) pxResult;
      if (rmRow != row && string.Compare(rmRow.RowCode, row.RowCode, true) == 0)
        throw new PXException("A record with the same value of the Code field already exists.");
    }
  }

  protected virtual void RMRow_RMType_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this.RowSet.Current == null)
      return;
    e.ReturnValue = (object) this.RowSet.Current.Type;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnValue, typeof (string), new bool?(false), new bool?(false), new int?(0), new int?(0), new int?(0), (object) 0, "RMType", enabled: new bool?(false), visible: new bool?(true), readOnly: new bool?(true), visibility: PXUIVisibility.Visible);
  }

  protected virtual void UpdateRowFieldsVisibility()
  {
    RMRow current1 = this.CurrentRow.Current;
    if (current1 == null)
      return;
    short? nullable = current1.RowType;
    int num1;
    if (!nullable.HasValue)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.RowType;
      num1 = (int) nullable.Value;
    }
    ARmRowType armRowType = (ARmRowType) num1;
    RMDataSource current2 = this.CurrentRowDataSource.Current;
    bool isVisible = armRowType == null || armRowType == 3;
    bool flag = armRowType == 2;
    this.RowsOrdered.Current.DataVisible = new bool?(isVisible);
    this.RowsOrdered.Current.DataSourceVisible = new bool?(armRowType == 0);
    if (current2 != null)
      PXUIFieldAttribute.SetVisible<RMDataSource.rowDescription>(this.CurrentRowDataSource.Cache, (object) current2, current2.Expand != "N");
    PXCache cache = this.CurrentRow.Cache;
    RMRow data = current1;
    nullable = current1.PrintControl;
    int num2;
    if (nullable.HasValue)
    {
      nullable = current1.PrintControl;
      num2 = nullable.Value == (short) 0 ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetVisible<RMRow.pageBreak>(cache, (object) data, num2 != 0);
    PXUIFieldAttribute.SetVisible<RMRow.suppressEmpty>(this.CurrentRow.Cache, (object) current1, isVisible);
    PXUIFieldAttribute.SetVisible<RMRow.hideZero>(this.CurrentRow.Cache, (object) current1, isVisible);
    PXUIFieldAttribute.SetVisible<RMRow.format>(this.CurrentRow.Cache, (object) current1, isVisible);
    PXUIFieldAttribute.SetVisible<RMRow.unitGroupID>(this.CurrentRow.Cache, (object) current1, !flag);
    PXUIFieldAttribute.SetVisible<RMRow.printControl>(this.CurrentRow.Cache, (object) current1, !flag);
  }

  protected virtual void _(Events.RowDeleting<RMRowSet> e) => this._ignoreRowsDeleteing = true;

  protected virtual void _(Events.RowDeleting<RMRow> e)
  {
    if (this._ignoreRowsDeleteing)
      return;
    this._oldRowCodes = this.Rows.Select().RowCast<RMRow>().ToDictionary<RMRow, short?, string>((Func<RMRow, short?>) (x => x.RowNbr), (Func<RMRow, string>) (x => x.RowCode));
  }

  protected virtual void _(Events.RowDeleted<RMRow> e)
  {
    if (this._ignoreRowsDeleteing)
      return;
    this.UpdateValues(this.GetCachedRows().Where<RMRow>((Func<RMRow, bool>) (x => x.RowNbr.HasValue && this._oldRowCodes.ContainsKey(x.RowNbr))).ToDictionary<RMRow, string, string>((Func<RMRow, string>) (x => this._oldRowCodes[x.RowNbr]), (Func<RMRow, string>) (x => x.RowCode)));
    this.CheckCycles();
    this.Rows.View.RequestRefresh();
    this.RowsOrdered.View.RequestRefresh();
  }

  [PXButton(ImageKey = "AddNew", Tooltip = "Insert")]
  [PXUIField(DisplayName = "New Row (Ctrl+Ins)", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Visible = false)]
  public virtual IEnumerable insertRow(PXAdapter adapter)
  {
    RMRow current = this.RowsOrdered.Current;
    IEnumerable<RMRow> source = this.RowsOrdered.Select().RowCast<RMRow>();
    Dictionary<short?, string> oldRowCodes = source.ToDictionary<RMRow, short?, string>((Func<RMRow, short?>) (x => x.RowNbr), (Func<RMRow, string>) (x => x.RowCode));
    foreach (RMRow rmRow in source)
    {
      int? sortOrder1 = rmRow.SortOrder;
      int? sortOrder2 = current.SortOrder;
      if (!(sortOrder1.GetValueOrDefault() <= sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue))
      {
        PXCache cache = this.RowsOrdered.Cache;
        RMRow data = rmRow;
        sortOrder2 = rmRow.SortOrder;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) (sortOrder2.HasValue ? new int?(sortOrder2.GetValueOrDefault() + 1) : new int?());
        cache.SetValue((object) data, "SortOrder", (object) local);
      }
    }
    RMRow rmRow1 = new RMRow();
    rmRow1.RowSetCode = this.RowSetOrdered.Current.RowSetCode;
    rmRow1.RowType = new short?((short) 0);
    rmRow1.RowCode = current?.RowCode ?? "1";
    rmRow1.SortOrder = new int?(((int?) current?.SortOrder).GetValueOrDefault() + 1);
    this.RowsOrdered.Current = this.RowsOrdered.Insert(rmRow1);
    int digitsNum = System.Math.Max((source.Count<RMRow>() + 1).ToString().Length, rmRow1.RowCode.Length);
    Dictionary<string, string> dictionary = source.ToDictionary<RMRow, string, string>((Func<RMRow, string>) (x => oldRowCodes[x.RowNbr]), (Func<RMRow, string>) (x =>
    {
      int? sortOrder = x.SortOrder;
      ref int? local = ref sortOrder;
      return !local.HasValue ? (string) null : local.GetValueOrDefault().ToString($"D{digitsNum}");
    }));
    foreach (RMRow rmRow2 in source)
    {
      this.RowsOrdered.Cache.SetValue((object) rmRow2, "RowCode", (object) dictionary[oldRowCodes[rmRow2.RowNbr]]);
      this.RowsOrdered.Cache.MarkUpdated((object) rmRow2);
    }
    this.UpdateValues(dictionary);
    this.CheckCycles();
    return adapter.Get();
  }

  [Serializable]
  public class ParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "New Code", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
    public virtual string NewRowSetCode { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
    public virtual string Description { get; set; }

    [PXString]
    public virtual string CopiedRow { get; set; }

    [PXBool]
    public virtual bool? AreRowsOrdered { get; set; }

    public virtual int? CopiedStyle { get; set; }

    public abstract class newRowSetCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMRowSetMaint.ParamFilter.newRowSetCode>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMRowSetMaint.ParamFilter.description>
    {
    }

    public abstract class copiedRow : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMRowSetMaint.ParamFilter.copiedRow>
    {
    }

    public abstract class areRowsOrdered : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RMRowSetMaint.ParamFilter.areRowsOrdered>
    {
    }
  }

  private enum BoundType
  {
    Lower,
    Upper,
    NA,
  }

  private struct NumChunks
  {
    public RMRowSetMaint.BoundType BoundType;
    public int Length;
  }

  [PXDynamicButton(new string[] {"RowsPasteLine"}, new string[] {"Paste Line"})]
  public class RMRowsOrdered : 
    PXOrderedSelect<RMRowSet, RMRow, Where<RMRow.rowSetCode, Equal<Current<RMRowSet.rowSetCode>>>, OrderBy<Asc<RMRow.rowCode>>>
  {
    public const string RowsPasteLineCommand = "RowsPasteLine";

    public RMRowsOrdered(PXGraph graph)
      : base(graph)
    {
      this.RenumberAllBeforePersist = false;
    }

    public RMRowsOrdered(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
      this.RenumberAllBeforePersist = false;
    }

    protected override void AddActions(PXGraph graph)
    {
      this.AddAction(graph, "RowsPasteLine", "Paste Line", new PXButtonDelegate(this.ReorderRows));
    }

    protected override void OnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
    {
      bool? areRowsOrdered = (this.View.Graph as RMRowSetMaint).Parameter.Current.AreRowsOrdered;
      bool flag = true;
      if (!(areRowsOrdered.GetValueOrDefault() == flag & areRowsOrdered.HasValue))
        return;
      base.OnRowInserted(sender, e);
    }

    protected override void OnRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      bool? areRowsOrdered = (this.View.Graph as RMRowSetMaint).Parameter.Current.AreRowsOrdered;
      bool flag = true;
      if (!(areRowsOrdered.GetValueOrDefault() == flag & areRowsOrdered.HasValue))
        return;
      base.OnRowDeleted(sender, e);
    }

    [PXButton]
    protected virtual IEnumerable ReorderRows(PXAdapter adapter)
    {
      RMRowSetMaint graph = this.View.Graph as RMRowSetMaint;
      IEnumerable<RMRow> source = graph.RowsOrdered.Select().RowCast<RMRow>();
      Dictionary<short?, string> oldRowCodes = source.ToDictionary<RMRow, short?, string>((Func<RMRow, short?>) (x => x.RowNbr), (Func<RMRow, string>) (x => x.RowCode));
      IEnumerable enumerable = this.PasteLine(adapter);
      Dictionary<string, string> dictionary = source.ToDictionary<RMRow, string, string>((Func<RMRow, string>) (x => oldRowCodes[x.RowNbr]), (Func<RMRow, string>) (x => x.RowCode));
      graph.UpdateValues(dictionary);
      graph.CheckCycles();
      this.View.RequestRefresh();
      return enumerable;
    }
  }
}
