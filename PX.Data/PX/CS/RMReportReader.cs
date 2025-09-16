// Decompiled with JetBrains decompiler
// Type: PX.CS.RMReportReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Reports;
using PX.Reports;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using PX.Reports.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.CS;

public class RMReportReader : PXGraph<RMReportReader, RMReport>, IARmDataSource
{
  public PXSelect<RMReport> Report;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMReport.styleID>>>> StyleByID;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMReport.dataSourceID>>>> DataSourceByID;
  public PXSelectJoin<RMColumnHeader, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMColumnHeader.styleID>>>, Where<RMColumnHeader.columnSetCode, Equal<Current<RMReport.columnSetCode>>>, OrderBy<Asc<RMColumnHeader.headerNbr>>> ColumnHeaders;
  public PXSelectJoin<RMColumn, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMColumn.dataSourceID>>, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMColumn.styleID>>>>, Where<RMColumn.columnSetCode, Equal<Current<RMReport.columnSetCode>>>> Columns;
  public PXSelectJoin<RMRow, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMRow.dataSourceID>>, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMRow.styleID>>>>, Where<RMRow.rowSetCode, Equal<Current<RMReport.rowSetCode>>>, OrderBy<Asc<RMRow.rowCode>>> Rows;
  public PXSelectJoin<RMUnit, CrossJoin<RMDataSource>> Units;
  private int _drilldownNumber;
  private RMDataSource rmDataSourceCopy;
  private SoapNavigator.DATA _Data;
  private string _ReportCode;

  public RMReportReader() => this._ForceUnattended = true;

  public override void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    base.InitCacheMapping(map);
    this.Caches<RMDataSource>();
  }

  protected virtual IEnumerable units(string parentCode)
  {
    if (parentCode == null)
      return (IEnumerable) PXSelectBase<RMUnit, PXSelectJoin<RMUnit, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMUnit.dataSourceID>>>, Where<RMUnit.unitSetCode, Equal<Current<RMReport.unitSetCode>>, And<RMUnit.parentCode, IsNull>>>.Config>.Select((PXGraph) this);
    return (IEnumerable) PXSelectBase<RMUnit, PXSelectJoin<RMUnit, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMUnit.dataSourceID>>>, Where<RMUnit.unitSetCode, Equal<Current<RMReport.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>>>>.Config>.Select((PXGraph) this, (object) parentCode);
  }

  public override void Clear() => base.Clear();

  public int DrilldownNumber
  {
    get => this._drilldownNumber;
    set => this._drilldownNumber = value;
  }

  public override bool IsDirty => false;

  public IARmDataSource GetUnderlyingDataSource() => (IARmDataSource) this;

  public virtual bool IsParameter(ARmDataSet ds, string name, ValueBucket value) => false;

  public virtual ARmDataSet MergeDataSet(
    IEnumerable<ARmDataSet> list,
    string expand,
    MergingMode mode)
  {
    return new ARmDataSet();
  }

  public virtual string MergeMask(string mask1, string mask2)
  {
    if (string.IsNullOrEmpty(mask1))
      return mask2;
    if (string.IsNullOrEmpty(mask2))
      return mask1;
    StringBuilder stringBuilder = new StringBuilder();
    int num = System.Math.Max(mask1.Length, mask2.Length);
    for (int index = 0; index < num; ++index)
    {
      char ch1 = index < mask1.Length ? mask1[index] : char.MinValue;
      char ch2 = index < mask2.Length ? mask2[index] : char.MinValue;
      if ((ch1 == char.MinValue || ch1 == '?' || ch1 == ' ') && ch2 != char.MinValue)
        stringBuilder.Append(ch2);
      else
        stringBuilder.Append(ch1);
    }
    return stringBuilder.ToString();
  }

  public virtual void LocalizeReport(ARmReport report)
  {
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    this.Clear(PXClearOption.ClearQueriesOnly);
    ARmReport report1 = this.GetReport();
    for (int index1 = 0; index1 < ((List<ARmHeadersSet>) report1.Columns.Headers).Count && index1 < ((List<ARmHeadersSet>) report.Columns.Headers).Count; ++index1)
    {
      for (int index2 = 0; index2 < ((CollectionBase) ((List<ARmHeadersSet>) report1.Columns.Headers)[index1].Items).Count && index2 < ((CollectionBase) ((List<ARmHeadersSet>) report.Columns.Headers)[index1].Items).Count; ++index2)
        ((List<ARmHeadersSet>) report.Columns.Headers)[index1].Items[index2].Text = ((List<ARmHeadersSet>) report1.Columns.Headers)[index1].Items[index2].Text;
    }
    for (int index = 0; index < ((CollectionBase) report1.Columns.Items).Count && index < ((CollectionBase) report.Columns.Items).Count; ++index)
    {
      report.Columns.Items[index].Description = report1.Columns.Items[index].Description;
      report.Columns.Items[index].Formula = report1.Columns.Items[index].Formula;
    }
    for (int index = 0; index < ((CollectionBase) report1.Rows.Items).Count && index < ((CollectionBase) report.Rows.Items).Count; ++index)
    {
      report.Rows.Items[index].Description = report1.Rows.Items[index].Description;
      report.Rows.Items[index].Formula = report1.Rows.Items[index].Formula;
    }
  }

  public virtual ARmReport GetReport()
  {
    this.Report.Current = (RMReport) this.Report.Search<RMReport.reportCode>((object) this.ReportCode);
    if (this.Report.Current == null)
      throw new PXException($"The report with name {this.ReportCode} is not found.");
    ARmReport report = new ARmReport();
    report.Type = this.Report.Current.Type;
    report.Code = this.Report.Current.ReportCode;
    report.Description = this.Report.Current.Description;
    report.PageSettings.Landscape = this.Report.Current.Landscape.Value;
    object paperKind = (object) this.Report.Current.PaperKind;
    if (paperKind != null)
      report.PageSettings.PaperKind = (PaperKind) (short) paperKind;
    object height = (object) this.Report.Current.Height;
    object heightType = (object) this.Report.Current.HeightType;
    SizeU paperSize = report.PageSettings.PaperSize;
    if (height != null && heightType != null)
      ((SizeU) ref paperSize).Height = new ReportUnit((double) height, (UnitType) (int) (short) heightType);
    object width = (object) this.Report.Current.Width;
    object widthType = (object) this.Report.Current.WidthType;
    if (width != null && widthType != null)
      ((SizeU) ref paperSize).Width = new ReportUnit((double) width, (UnitType) (int) (short) widthType);
    object marginLeft = (object) this.Report.Current.MarginLeft;
    object marginLeftType = (object) this.Report.Current.MarginLeftType;
    if (marginLeft != null && marginLeftType != null)
      report.PageSettings.Margins.Left = new ReportUnit((double) marginLeft, (UnitType) (int) (short) marginLeftType);
    object marginRight = (object) this.Report.Current.MarginRight;
    object marginRightType = (object) this.Report.Current.MarginRightType;
    if (marginRight != null && marginRightType != null)
      report.PageSettings.Margins.Right = new ReportUnit((double) marginRight, (UnitType) (int) (short) marginRightType);
    object marginTop = (object) this.Report.Current.MarginTop;
    object marginTopType = (object) this.Report.Current.MarginTopType;
    if (marginTop != null && marginTopType != null)
      report.PageSettings.Margins.Top = new ReportUnit((double) marginTop, (UnitType) (int) (short) marginTopType);
    object marginBottom = (object) this.Report.Current.MarginBottom;
    object marginBottomType = (object) this.Report.Current.MarginBottomType;
    if (marginBottom != null && marginBottomType != null)
      report.PageSettings.Margins.Bottom = new ReportUnit((double) marginBottom, (UnitType) (int) (short) marginBottomType);
    report.Columns.Code = this.Report.Current.ColumnSetCode;
    ARmHeadersSet armHeadersSet = (ARmHeadersSet) null;
    object objB = (object) null;
    foreach (PXResult<RMColumnHeader, RMStyle> src in this.ColumnHeaders.Select())
    {
      RMColumnHeader rmColumnHeader = src[typeof (RMColumnHeader)] as RMColumnHeader;
      short? headerNbr = rmColumnHeader.HeaderNbr;
      if (!object.Equals((object) headerNbr, objB))
      {
        armHeadersSet = new ARmHeadersSet();
        armHeadersSet.Height = rmColumnHeader.Height.Value;
        armHeadersSet.PrintingGroup = rmColumnHeader.GroupID;
        ((List<ARmHeadersSet>) report.Columns.Headers).Add(armHeadersSet);
        objB = (object) headerNbr;
      }
      ARmHeader armHeader = new ARmHeader();
      armHeader.StartColumn = string.IsNullOrEmpty(rmColumnHeader.StartColumn) ? rmColumnHeader.ColumnCode : rmColumnHeader.StartColumn;
      if (armHeader.StartColumn != null)
        armHeader.StartColumn = armHeader.StartColumn.Trim();
      armHeader.EndColumn = string.IsNullOrEmpty(rmColumnHeader.EndColumn) ? rmColumnHeader.ColumnCode : rmColumnHeader.EndColumn;
      if (armHeader.EndColumn != null)
        armHeader.EndColumn = armHeader.EndColumn.Trim();
      armHeader.Text = rmColumnHeader.Formula ?? "";
      this.fillStyle((RMStyle) src, armHeader.Style);
      armHeadersSet.Items.Add(armHeader);
    }
    foreach (PXResult<RMColumn, RMDataSource, RMStyle> pxResult in this.Columns.Select())
    {
      RMColumn rmColumn = pxResult[typeof (RMColumn)] as RMColumn;
      ARmColumn armColumn1 = new ARmColumn();
      this.FillDataSource(pxResult[typeof (RMDataSource)] as RMDataSource, armColumn1.DataSet, report.Type);
      this.fillStyle(pxResult[typeof (RMStyle)] as RMStyle, armColumn1.Style);
      armColumn1.Description = rmColumn.Description;
      ARmColumn armColumn2 = armColumn1;
      int? nullable1 = rmColumn.ExtraSpace;
      int num1 = nullable1.Value;
      armColumn2.ExtraSpace = num1;
      ARmColumn armColumn3 = armColumn1;
      bool? nullable2 = rmColumn.SuppressEmpty;
      int num2 = nullable2.Value ? 1 : 0;
      armColumn3.SuppressEmpty = num2 != 0;
      ARmColumn armColumn4 = armColumn1;
      nullable2 = rmColumn.HideZero;
      int num3 = nullable2.Value ? 1 : 0;
      armColumn4.HideZero = num3 != 0;
      ARmColumn armColumn5 = armColumn1;
      nullable2 = rmColumn.SuppressLine;
      int num4 = nullable2.Value ? 1 : 0;
      armColumn5.SuppressLine = num4 != 0;
      ARmColumn armColumn6 = armColumn1;
      short? nullable3 = rmColumn.Rounding;
      int num5 = (int) nullable3.Value;
      armColumn6.Rounding = (ARmColumnRounding) num5;
      armColumn1.Format = rmColumn.Format;
      armColumn1.Formula = rmColumn.Formula;
      armColumn1.VisibleFormula = rmColumn.VisibleFormula;
      ARmColumn armColumn7 = armColumn1;
      nullable3 = rmColumn.PrintControl;
      int num6 = (int) nullable3.Value;
      armColumn7.PrintControl = (ColPrintControl) num6;
      ARmColumn armColumn8 = armColumn1;
      nullable2 = rmColumn.PageBreak;
      int num7 = nullable2.Value ? 1 : 0;
      armColumn8.PageBreak = num7 != 0;
      armColumn1.PrintingGroup = rmColumn.GroupID;
      ARmColumn armColumn9 = armColumn1;
      nullable3 = rmColumn.ColumnType;
      int num8 = (int) nullable3.Value;
      armColumn9.Type = (ARmColumnType) num8;
      armColumn1.UnitsGroup = rmColumn.UnitGroupID;
      ARmColumn armColumn10 = armColumn1;
      nullable1 = rmColumn.Width;
      int num9 = nullable1.Value;
      armColumn10.Width = num9;
      ARmColumn armColumn11 = armColumn1;
      nullable2 = rmColumn.AutoHeight;
      int num10 = nullable2.Value ? 1 : 0;
      armColumn11.AutoHeight = num10 != 0;
      ARmColumn armColumn12 = armColumn1;
      nullable3 = rmColumn.CellEvalOrder;
      int num11 = (int) nullable3.Value;
      armColumn12.CellEvalOrder = (ARmCellEvalOrder) num11;
      ARmColumn armColumn13 = armColumn1;
      nullable3 = rmColumn.CellFormatOrder;
      int num12 = (int) nullable3.Value;
      armColumn13.CellFormatOrder = (ARmCellFormatOrder) num12;
      report.Columns.Items.Add(armColumn1);
    }
    report.Rows.Code = this.Report.Current.RowSetCode;
    foreach (PXResult<RMRow, RMDataSource, RMStyle> pxResult in this.Rows.Select())
    {
      RMRow rmRow = pxResult[typeof (RMRow)] as RMRow;
      ARmRow armRow1 = new ARmRow();
      this.FillDataSource(pxResult[typeof (RMDataSource)] as RMDataSource, armRow1.DataSet, report.Type);
      this.fillStyle(pxResult[typeof (RMStyle)] as RMStyle, armRow1.Style);
      armRow1.Code = rmRow.RowCode;
      armRow1.ColumnsGroup = rmRow.ColumnGroupID;
      armRow1.Description = rmRow.Description;
      ARmRow armRow2 = armRow1;
      short? nullable = rmRow.RowType;
      int num13 = (int) nullable.Value;
      armRow2.Type = (ARmRowType) num13;
      armRow1.Formula = rmRow.Formula;
      armRow1.Format = rmRow.Format;
      armRow1.Height = rmRow.Height.Value;
      armRow1.Indent = rmRow.Indent.Value;
      armRow1.HideZero = rmRow.HideZero.Value;
      ARmRow armRow3 = armRow1;
      nullable = rmRow.LineStyle;
      int num14 = (int) nullable.Value;
      armRow3.LineStyle = (BorderType) num14;
      armRow1.LinkedRow = rmRow.LinkedRowCode;
      armRow1.BaseRow = rmRow.BaseRowCode;
      ARmRow armRow4 = armRow1;
      nullable = rmRow.PrintControl;
      int num15 = (int) nullable.Value;
      armRow4.PrintControl = (RowPrintControl) num15;
      armRow1.PageBreak = rmRow.PageBreak.Value;
      armRow1.SuppressEmpty = rmRow.SuppressEmpty.Value;
      armRow1.UnitsGroup = rmRow.UnitGroupID;
      report.Rows.Items.Add(armRow1);
    }
    report.Units.Code = this.Report.Current.UnitSetCode;
    this.LoadUnits((ARmUnit) null, report.Units.Items, report.Type);
    report.StartUnit = this.Report.Current.StartUnitCode;
    return report;
  }

  public virtual object GetHistoryValue(ARmDataSet dataSet, bool drilldown)
  {
    return (object) new ARmDataSetValue(0.0);
  }

  /// <summary>
  /// Gets configuration for parallel cell value calculation - the number of worker threads and an indicator if all processors should be used.
  /// </summary>
  /// <returns>
  /// The number of threads for parallel cell value calculation and an indicator if all processors should be used.
  /// </returns>
  public (int WorkerThreadsCount, bool UseAllProcessors) GetThreadsCountForCellValueParallelCalculation()
  {
    int num = WebConfig.ParallelArmReportsCalculationMaxThreads ?? -1;
    if (num < -1 || num == 0)
      throw new InvalidOperationException("The value of ParallelArmReportsCalculationMaxThreads setting in the web.config file can not be 0 or less than -1. Please specify a positive integer number or -1.");
    int processorCount = Environment.ProcessorCount;
    if (num == -1)
      return (processorCount, true);
    return num >= processorCount ? (processorCount, true) : (num, false);
  }

  public virtual IEnumerable GetItemsInRange(System.Type table, ARmDataSet dataSet)
  {
    yield break;
  }

  public virtual string GetUrl() => (string) null;

  private void InitRMDataSource()
  {
    if (this.rmDataSourceCopy == null)
    {
      this.rmDataSourceCopy = this.DataSourceByID.Insert();
      this.rmDataSourceCopy = (RMDataSource) this.DataSourceByID.Cache.CreateCopy((object) this.rmDataSourceCopy);
    }
    this.DataSourceByID.Cache.RestoreCopy((object) this.DataSourceByID.Current, (object) this.rmDataSourceCopy);
  }

  public virtual ARmDataSetValue GetValue(ARmDataSet dataSet)
  {
    this.EnsureBranches();
    this.InitRMDataSource();
    RMDataSource current = this.DataSourceByID.Current;
    current.Expand = dataSet.Expand;
    current.RowDescription = dataSet.RowDescription;
    object historyValue = this.GetHistoryValue(dataSet, false);
    if (historyValue == null)
      return new ARmDataSetValue(0.0);
    if (!(historyValue is List<object[]> objArrayList))
      return (Decimal) historyValue == Decimal.MinValue ? (ARmDataSetValue) null : new ARmDataSetValue((double) (Decimal) historyValue);
    Decimal num = 0M;
    for (int index = 0; index < objArrayList.Count; ++index)
      num += (Decimal) objArrayList[index][2];
    ARmDataSetValue armDataSetValue = new ARmDataSetValue((double) num);
    for (int index = 0; index < objArrayList.Count; ++index)
      armDataSetValue.Items.Add(new ARmDataSetValue.Item((string) objArrayList[index][0], (string) objArrayList[index][5], (string) objArrayList[index][1], (double) (Decimal) objArrayList[index][2], (string) objArrayList[index][4]));
    return armDataSetValue;
  }

  public virtual string GetDrilldownUrl(params ARmDataSet[] dataSet)
  {
    IPXResultset pxResultset = (IPXResultset) null;
    dataSet = ((IEnumerable<ARmDataSet>) dataSet).Distinct<ARmDataSet>().ToArray<ARmDataSet>();
    for (int index1 = 0; index1 < dataSet.Length; ++index1)
    {
      this.InitRMDataSource();
      this.DataSourceByID.Current.Expand = dataSet[index1].Expand;
      if (!(this.GetHistoryValue(dataSet[index1], true) is IPXResultset historyValue) && dataSet[index1].HasChildren)
        historyValue = this.GetHistoryValue(this.MergeDataSet(dataSet[index1].Units.Prepend<ARmDataSet>(dataSet[index1]), dataSet[index1].Expand, (MergingMode) 1), true) as IPXResultset;
      if (historyValue != null)
      {
        if (pxResultset == null)
        {
          pxResultset = historyValue;
        }
        else
        {
          for (int index2 = 0; index2 < ((ICollection) historyValue).Count; ++index2)
            ((IList) pxResultset).Add(((IList) historyValue)[index2]);
        }
      }
    }
    if (pxResultset != null)
    {
      string url = this.GetUrl();
      if (url != null)
      {
        int length = url.IndexOf('?');
        if (length != -1)
          PXContext.SessionTyped<PXSessionStatePXData>().DrilldownReport[url.Substring(0, length).ToRelativeUrl()] = pxResultset;
        else
          PXContext.SessionTyped<PXSessionStatePXData>().DrilldownReport[url.ToRelativeUrl()] = pxResultset;
        return url;
      }
    }
    return (string) null;
  }

  public void fillStyle(RMStyle src, ARmItemStyle dst)
  {
    if (src == null || !src.StyleID.HasValue)
      return;
    string backColor = src.BackColor;
    if (!string.IsNullOrEmpty(backColor))
    {
      try
      {
        dst.BackColor = Color.FromArgb(Convert.ToInt32(backColor, 16 /*0x10*/));
      }
      catch
      {
      }
    }
    string color = src.Color;
    if (!string.IsNullOrEmpty(color))
    {
      try
      {
        dst.Color = Color.FromArgb(Convert.ToInt32(color, 16 /*0x10*/));
      }
      catch
      {
      }
    }
    string fontName = src.FontName;
    if (!string.IsNullOrEmpty(fontName))
      dst.Font.Name = fontName;
    object fontSize = (object) src.FontSize;
    object fontSizeType = (object) src.FontSizeType;
    if (fontSize != null && fontSizeType != null)
      dst.Font.Size = new ReportUnit((double) fontSize, (UnitType) (int) (short) fontSizeType);
    object fontStyle = (object) src.FontStyle;
    if (fontStyle != null)
      dst.Font.Style = (FontStyle) (short) fontStyle;
    dst.TextAlign = (HorizontalAlign) (int) src.TextAlign.Value;
  }

  public virtual void FillDataSource(RMDataSource ds, ARmDataSet dst, string rmType)
  {
  }

  public virtual List<ARmUnit> ExpandUnit(RMDataSource ds, ARmUnit unit) => (List<ARmUnit>) null;

  public void LoadUnits(ARmUnit parent, ARmUnitCollection items, string rmType)
  {
    PXResultset<RMUnit> pxResultset;
    if (parent == null)
      pxResultset = this.Units.Select();
    else
      pxResultset = this.Units.Select((object) parent.Code);
    foreach (PXResult<RMUnit, RMDataSource> pxResult in pxResultset)
    {
      RMUnit rmUnit = pxResult[typeof (RMUnit)] as RMUnit;
      ARmUnit ac = new ARmUnit();
      RMDataSource ds = pxResult[typeof (RMDataSource)] as RMDataSource;
      this.FillDataSource(ds, ac.DataSet, rmType);
      ac.Code = rmUnit.UnitCode;
      ac.Description = rmUnit.Description;
      ac.Formula = rmUnit.Formula;
      ac.PrintingGroup = rmUnit.GroupID;
      items.Add(ac);
      this.ExpandUnit(ds, ac)?.ForEach((System.Action<ARmUnit>) (u => ac.Items.Add(u)));
      this.LoadUnits(ac, ac.Items, rmType);
    }
    if (parent == null || !string.IsNullOrEmpty(parent.Formula) || ((CollectionBase) parent.Items).Count <= 0)
      return;
    ARmDataSet dst = new ARmDataSet();
    PXCache cach = this.Caches[typeof (RMDataSource)];
    RMDataSource instance = (RMDataSource) cach.CreateInstance();
    foreach (string field in (List<string>) cach.Fields)
    {
      object newValue;
      cach.RaiseFieldDefaulting(field, (object) instance, out newValue);
      cach.SetValue((object) instance, field, newValue);
    }
    this.FillDataSource(instance, dst, rmType);
    if (!dst.Equals(parent.DataSet))
      return;
    ((Dictionary<object, object>) parent.DataSet).Clear();
    EnumerableExtensions.AddRange<object, object>((IDictionary<object, object>) parent.DataSet, (IEnumerable<KeyValuePair<object, object>>) this.MergeDataSet(RMReportReader.Traverse<ARmUnit, ARmDataSet>(parent, (Func<ARmUnit, IEnumerable<ARmUnit>>) (it => (IEnumerable<ARmUnit>) it.Items), (Func<ARmUnit, ARmDataSet>) (it => it.DataSet)), parent.DataSet.Expand, (MergingMode) 1));
  }

  public static IEnumerable<I> Traverse<T, I>(
    T item,
    Func<T, IEnumerable<T>> traverser,
    Func<T, I> selector)
  {
    Stack<T> stack = new Stack<T>();
    stack.Push(item);
    while (stack.Any<T>())
    {
      T next = stack.Pop();
      yield return selector(next);
      foreach (T obj in traverser(next))
        stack.Push(obj);
      next = default (T);
    }
  }

  public void CreateParameter(
    object key,
    string name,
    string title,
    bool defValue,
    bool Request,
    int colSpan,
    string viewName,
    string inputMask,
    List<ARmReport.ARmReportParameter> aRmPl)
  {
    if (!Request)
      return;
    ARmReport.ARmReportParameter armReportParameter = new ARmReport.ARmReportParameter(key, name, (ParameterType) 0);
    ((ReportParameter) armReportParameter).Prompt = title;
    ((ReportParameter) armReportParameter).ColumnSpan = colSpan;
    ((ReportParameter) armReportParameter).DefaultValue = defValue.ToString();
    ((ReportParameter) armReportParameter).ViewName = viewName;
    ((ReportParameter) armReportParameter).InputMask = inputMask;
    ((ReportParameter) armReportParameter).Required = new bool?(true);
    aRmPl.Add(armReportParameter);
  }

  public void CreateParameter(
    object key,
    string name,
    string title,
    string defValue,
    bool Request,
    int colSpan,
    string viewName,
    string inputMask,
    List<ARmReport.ARmReportParameter> aRmPl)
  {
    if (!Request)
      return;
    ARmReport.ARmReportParameter armReportParameter = new ARmReport.ARmReportParameter(key, name, (ParameterType) 4);
    ((ReportParameter) armReportParameter).Prompt = title;
    ((ReportParameter) armReportParameter).ColumnSpan = colSpan;
    ((ReportParameter) armReportParameter).DefaultValue = defValue;
    ((ReportParameter) armReportParameter).ViewName = viewName;
    ((ReportParameter) armReportParameter).InputMask = inputMask;
    ((ReportParameter) armReportParameter).Required = new bool?(true);
    aRmPl.Add(armReportParameter);
  }

  public virtual object GetExprContext()
  {
    if (this._Data == null)
      this._Data = new SoapNavigator.DATA();
    return (object) this._Data;
  }

  public string ReportCode
  {
    get => this._ReportCode;
    set => this._ReportCode = value;
  }

  private void EnsureBranches()
  {
    PXDatabase.ReadBranchRestricted = false;
    PXDatabase.BranchIDs = (List<int>) null;
  }
}
