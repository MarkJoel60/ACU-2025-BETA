// Decompiled with JetBrains decompiler
// Type: PX.CS.RMReportMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.Maintenance.SM.SiteMapHelpers;
using PX.DbServices.Model.DataSet;
using PX.DbServices.Model.ImportExport.Serialization;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CS;

public class RMReportMaint : 
  PXGraph<RMReportMaint, RMReport>,
  IGraphWithInitialization,
  ICanAlterSiteMap
{
  public const string BaseUrl = "~/Frames/RmLauncher.aspx";
  public PXSelect<RMReport> Report;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMReport.styleID>>>> StyleByID;
  public PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Current<RMReport.styleID>>>> CurrentStyle;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMReport.dataSourceID>>>> DataSourceByID;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Current<RMReport.dataSourceID>>>> CurrentDataSource;
  public PXSelectJoin<RMColumnHeader, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMColumnHeader.styleID>>>, Where<RMColumnHeader.columnSetCode, Equal<Current<RMReport.columnSetCode>>>, OrderBy<Asc<RMColumnHeader.headerNbr>>> ColumnHeaders;
  public PXSelectJoin<RMColumn, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMColumn.dataSourceID>>, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMColumn.styleID>>>>, Where<RMColumn.columnSetCode, Equal<Current<RMReport.columnSetCode>>>> Columns;
  public PXSelectJoin<RMRow, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMRow.dataSourceID>>, LeftJoin<RMStyle, On<RMStyle.styleID, Equal<RMRow.styleID>>>>, Where<RMRow.rowSetCode, Equal<Current<RMReport.rowSetCode>>>, OrderBy<Asc<RMRow.rowCode>>> Rows;
  public PXSelectJoin<RMUnit, CrossJoin<RMDataSource>> Units;
  public PXSelect<PX.SM.RolesInGraph, Where<PX.SM.RolesInGraph.screenID, Equal<Required<PX.SM.RolesInGraph.screenID>>, And<PX.SM.RolesInGraph.applicationName, Equal<Required<PX.SM.RolesInGraph.applicationName>>>>> GraphAccessRights;
  public PXSelect<PX.SM.RolesInCache, Where<PX.SM.RolesInCache.screenID, Equal<Required<PX.SM.RolesInCache.screenID>>, And<PX.SM.RolesInCache.applicationName, Equal<Required<PX.SM.RolesInCache.applicationName>>>>> CacheAccessRights;
  public PXSelect<PX.SM.RolesInMember, Where<PX.SM.RolesInMember.screenID, Equal<Required<PX.SM.RolesInMember.screenID>>, And<PX.SM.RolesInMember.applicationName, Equal<Required<PX.SM.RolesInMember.applicationName>>>>> MemberAccessRights;
  public PXFilter<RMReportMaint.ParamFilter> Parameter;
  public PXAction<RMReport> copyReport;
  public PXAction<RMReport> preview;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  protected PXSiteMapProvider SiteMapProvider { get; set; }

  protected virtual IEnumerable units(string parentCode)
  {
    if (parentCode == null)
      return (IEnumerable) PXSelectBase<RMUnit, PXSelectJoin<RMUnit, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMUnit.dataSourceID>>>, Where<RMUnit.unitSetCode, Equal<Current<RMReport.unitSetCode>>, And<RMUnit.parentCode, IsNull>>>.Config>.Select((PXGraph) this);
    return (IEnumerable) PXSelectBase<RMUnit, PXSelectJoin<RMUnit, LeftJoin<RMDataSource, On<RMDataSource.dataSourceID, Equal<RMUnit.dataSourceID>>>, Where<RMUnit.unitSetCode, Equal<Current<RMReport.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>>>>.Config>.Select((PXGraph) this, (object) parentCode);
  }

  [PXUIField(DisplayName = "Preview")]
  [PXButton]
  protected virtual IEnumerable Preview(PXAdapter adapter)
  {
    RMReportMaint rmReportMaint = this;
    rmReportMaint.Save.Press();
    if (!string.IsNullOrEmpty(rmReportMaint.Report.Current.ReportCode))
    {
      PXSiteMapNode pxSiteMapNode = rmReportMaint.ScreenToSiteMapHelper.FindNodes(rmReportMaint.Report.Current).FirstOrDefault<PXSiteMapNode>();
      bool flag;
      if (pxSiteMapNode != null)
      {
        flag = rmReportMaint.IsNewUI(pxSiteMapNode.SelectedUI);
      }
      else
      {
        PXSiteMapNode screenIdUnsecure = rmReportMaint.SiteMapProvider.FindSiteMapNodeByScreenIDUnsecure("CS206000");
        flag = rmReportMaint.IsNewUI(screenIdUnsecure?.SelectedUI);
      }
      throw new PXRedirectToUrlException(!flag ? $"~/Frames/RmLauncher.aspx?id={rmReportMaint.Report.Current.ReportCode}.rpx" : $"~/Scripts/Screens/ReportScreen.html?id={rmReportMaint.Report.Current.ReportCode}&IsARm=true", PXBaseRedirectException.WindowMode.NewWindow, string.Empty);
    }
    foreach (RMReport rmReport in adapter.Get())
      yield return (object) rmReport;
  }

  public override bool CanClipboardCopyPaste() => false;

  [PXUIField(DisplayName = "Copy Report", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(ConfirmationMessage = "Any unsaved changes will be discarded.", ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable CopyReport(PXAdapter adapter)
  {
    if (this.Report.Current == null || this.Parameter.AskExt() != WebDialogResult.OK || string.IsNullOrEmpty(this.Parameter.Current.NewReportCode))
      return adapter.Get();
    string newReportCode = this.Parameter.Current.NewReportCode;
    foreach (RMReport rmReport in this.Cancel.Press(adapter))
      ;
    RMReport current = this.Report.Current;
    RMReport copy1 = (RMReport) this.Report.Cache.CreateCopy((object) current);
    copy1.ReportCode = newReportCode;
    copy1.ReportUID = new Guid?(Guid.NewGuid());
    copy1.NoteID = new Guid?();
    RMReport newReport = this.Report.Insert(copy1);
    if (newReport == null)
      return adapter.Get();
    RMDataSource copy2 = (RMDataSource) this.DataSourceByID.Select((object) newReport.DataSourceID);
    if (copy2 != null && this.DataSourceByID.Current != null)
    {
      this.DataSourceByID.Cache.RestoreCopy((object) this.DataSourceByID.Current, (object) copy2);
      this.DataSourceByID.Current.DataSourceID = newReport.DataSourceID;
    }
    RMStyle copy3 = (RMStyle) this.StyleByID.Select((object) newReport.StyleID);
    if (copy3 != null && this.StyleByID.Current != null)
    {
      this.StyleByID.Cache.RestoreCopy((object) this.StyleByID.Current, (object) copy3);
      this.StyleByID.Current.StyleID = newReport.StyleID;
    }
    newReport.StyleID = (int?) copy3?.StyleID;
    this.CopyAccessRights(current, newReport);
    return (IEnumerable) EnumerableExtensions.AsSingleEnumerable<RMReport>(newReport);
  }

  private bool IsNewUI(string selectedUI)
  {
    switch (selectedUI)
    {
      case "T":
        return true;
      case "E":
        return false;
      default:
        return SitePolicy.DefaultUI == "T";
    }
  }

  private void CopyAccessRights(RMReport oldReport, RMReport newReport)
  {
    string siteMapScreenId1 = this.ScreenToSiteMapHelper.GetSiteMapScreenID(oldReport);
    string siteMapScreenId2 = this.ScreenToSiteMapHelper.GenerateSiteMapScreenID(newReport);
    if (string.IsNullOrEmpty(siteMapScreenId1) || string.IsNullOrEmpty(siteMapScreenId2))
      return;
    this.CopyAccessRights(siteMapScreenId1, siteMapScreenId2);
  }

  private void CopyAccessRights(string oldScreenID, string newScreenID)
  {
    foreach (PXResult<PX.SM.RolesInGraph> pxResult in this.GraphAccessRights.Select((object) oldScreenID, (object) "/"))
    {
      PX.SM.RolesInGraph copy = (PX.SM.RolesInGraph) this.GraphAccessRights.Cache.CreateCopy((object) (PX.SM.RolesInGraph) pxResult);
      copy.ScreenID = newScreenID;
      this.GraphAccessRights.Insert(copy);
    }
    foreach (PXResult<PX.SM.RolesInCache> pxResult in this.CacheAccessRights.Select((object) oldScreenID, (object) "/"))
    {
      PX.SM.RolesInCache copy = (PX.SM.RolesInCache) this.CacheAccessRights.Cache.CreateCopy((object) (PX.SM.RolesInCache) pxResult);
      copy.ScreenID = newScreenID;
      this.CacheAccessRights.Insert(copy);
    }
    foreach (PXResult<PX.SM.RolesInMember> pxResult in this.MemberAccessRights.Select((object) oldScreenID, (object) "/"))
    {
      PX.SM.RolesInMember copy = (PX.SM.RolesInMember) this.MemberAccessRights.Cache.CreateCopy((object) (PX.SM.RolesInMember) pxResult);
      copy.ScreenID = newScreenID;
      this.MemberAccessRights.Insert(copy);
    }
  }

  protected override PXCacheCollection CreateCacheCollection()
  {
    return (PXCacheCollection) new PXCacheUniqueForTypeCollection((PXGraph) this);
  }

  public bool IsSiteMapAltered { get; internal set; }

  [PXInternalUseOnly]
  internal RMReportScreenToSiteMapAddHelper ScreenToSiteMapHelper { get; private set; }

  public virtual void RMReport_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.copyReport.SetEnabled(this.Report.Current != null && this.Report.Cache.GetStatus((object) this.Report.Current) != PXEntryStatus.Inserted && !string.IsNullOrEmpty(this.Report.Current.ReportCode));
    if (this.Report.Current == null)
      return;
    foreach (string field in (List<string>) this.CurrentDataSource.Cache.Fields)
    {
      if (this.CurrentDataSource.Cache.GetStateExt((object) null, field) is PXFieldState)
      {
        bool isVisible = this.IsFieldVisible(field, this.Report.Current);
        PXUIFieldAttribute.SetVisible(this.CurrentDataSource.Cache, (object) this.CurrentDataSource.Current, field, isVisible);
      }
    }
    this.CurrentDataSource.Current = this.CurrentDataSource.SelectSingle();
    this.CurrentStyle.Current = this.CurrentStyle.SelectSingle();
    this.InsertChildObjects(this.Report.Current);
  }

  protected void InsertChildObjects(RMReport report)
  {
    if (!report.DataSourceID.HasValue)
    {
      using (new ReadOnlyScope(new PXCache[1]
      {
        this.DataSourceByID.Cache
      }))
      {
        RMDataSource rmDataSource = this.DataSourceByID.Cache.Insert() as RMDataSource;
        this.Report.Cache.SetValue((object) report, "dataSourceID", (object) rmDataSource.DataSourceID);
      }
    }
    if (report.StyleID.HasValue)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      this.StyleByID.Cache
    }))
    {
      RMStyle rmStyle = this.StyleByID.Cache.Insert() as RMStyle;
      this.Report.Cache.SetValue((object) report, "styleID", (object) rmStyle.StyleID);
    }
  }

  protected void _(Events.FieldSelecting<RMDataSource.amountType> e)
  {
    if (this.Report.Current == null)
      return;
    e.ReturnState = (object) this.CreateAmountTypeState(e.ReturnValue);
  }

  protected virtual PXFieldState CreateAmountTypeState(object returnValue) => (PXFieldState) null;

  public RMReportMaint()
  {
    foreach (string field1 in (List<string>) this.StyleByID.Cache.Fields)
    {
      if (this.StyleByID.Cache.GetStateExt((object) null, field1) is PXFieldState stateExt && stateExt.Visible)
      {
        string field = field1;
        string field2 = "Style" + field;
        this.Report.Cache.Fields.Add(field2);
        this.FieldSelecting.AddHandler(typeof (RMReport), field2, (PXFieldSelecting) ((sender, e) => this.styleFieldSelecting(e, field)));
        this.FieldUpdating.AddHandler(typeof (RMReport), field2, (PXFieldUpdating) ((sender, e) => this.styleFieldUpdating(e, field)));
      }
    }
    foreach (string field3 in (List<string>) this.DataSourceByID.Cache.Fields)
    {
      if (this.DataSourceByID.Cache.GetStateExt((object) null, field3) is PXFieldState stateExt && stateExt.Visible)
      {
        string field = field3;
        string field4 = "DataSource" + field;
        this.Report.Cache.Fields.Add(field4);
        this.FieldSelecting.AddHandler(typeof (RMReport), field4, (PXFieldSelecting) ((sender, e) => this.dataSourceFieldSelecting(e, field)));
        this.FieldUpdating.AddHandler(typeof (RMReport), field4, (PXFieldUpdating) ((sender, e) => this.dataSourceFieldUpdating(e, field)));
      }
    }
    if (this.CopyPaste.ImportXMLValidator == null)
    {
      this.CopyPaste.ImportXMLValidator = new System.Action<DataSetXmReader>(this.ValidateRowCodes);
    }
    else
    {
      System.Action<DataSetXmReader> existingValidator = this.CopyPaste.ImportXMLValidator;
      this.CopyPaste.ImportXMLValidator = (System.Action<DataSetXmReader>) (reader =>
      {
        existingValidator(reader);
        this.ValidateRowCodes(reader);
      });
    }
  }

  private void ValidateRowCodes(DataSetXmReader reader)
  {
    PxDataTable table = reader.DataSet.GetTable("RMRow");
    if (table == null)
      return;
    foreach (object[] row in ((PxDataRows) ((PxDataRows) table).MakeProjection((IEnumerable<string>) new string[4]
    {
      "RowSetCode",
      "RowCode",
      "LinkedRowCode",
      "BaseRowCode"
    })).Rows)
    {
      string str = row[0] as string;
      string rowCode1 = row[1] as string;
      string rowCode2 = row[2] as string;
      string rowCode3 = row[3] as string;
      if (string.IsNullOrEmpty(rowCode1) || !this.IsRowCodeCorrect(rowCode1))
        throw new PXException("An RMRow record with RowSetCode set to {0} contains an invalid RowCode value: {1}.", new object[2]
        {
          (object) str,
          (object) rowCode1
        });
      if (!this.IsRowCodeCorrect(rowCode2))
        throw new PXException("An RMRow record with RowSetCode set to {0} and RowCode set to {1} contains an invalid LinkedRowCode value: {2}.", new object[3]
        {
          (object) str,
          (object) rowCode1,
          (object) rowCode2
        });
      if (!this.IsRowCodeCorrect(rowCode3))
        throw new PXException("An RMRow record with RowSetCode set to {0} and RowCode set to {1} contains an invalid BaseRowCode value: {2}.", new object[3]
        {
          (object) str,
          (object) rowCode1,
          (object) rowCode3
        });
    }
  }

  private bool IsRowCodeCorrect(string rowCode)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return string.IsNullOrEmpty(rowCode) || rowCode.All<char>(RMReportMaint.\u003C\u003EO.\u003C0\u003E__IsLetterOrDigit ?? (RMReportMaint.\u003C\u003EO.\u003C0\u003E__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit)));
  }

  public void Initialize()
  {
    this.ScreenToSiteMapHelper = new RMReportScreenToSiteMapAddHelper((PXGraph) this, this.ScreenInfoCacheControl);
  }

  public override void Persist()
  {
    this.IsSiteMapAltered |= this.ScreenToSiteMapHelper.IsSiteMapAltered;
    base.Persist();
    if (!this.IsSiteMapAltered)
      return;
    PXDatabase.SelectTimeStamp();
    PXAccess.Clear();
    PXSiteMap.Provider.Clear();
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXIntList(new int[] {0, 1, 2, 4, 8}, new string[] {"Regular", "Bold", "Italic", "Underline", "Strikeout"})]
  protected virtual void RMStyle_FontStyle_CacheAttached(PXCache sender)
  {
  }

  protected void _(Events.RowInserted<RMReport> e)
  {
    RMReport row = e.Row;
    if (row == null)
      return;
    this.InsertChildObjects(row);
  }

  protected void RMReport_ReportUID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void RMReport_Visible_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is RMReport row) || this.ScreenToSiteMapHelper.IsVisible(row))
      return;
    cache.SetValue<RMReport.sitemapTitle>((object) row, (object) null);
  }

  protected virtual void styleFieldSelecting(PXFieldSelectingEventArgs e, string field)
  {
    RMReport row = (RMReport) e.Row;
    PXResultset<RMStyle> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMStyle>) null;
    else
      pxResultset = this.StyleByID.Select((object) row.StyleID);
    RMStyle data = (RMStyle) pxResultset;
    if (data == null)
    {
      object newValue;
      if (this.StyleByID.Cache.RaiseFieldDefaulting(field, (object) null, out newValue))
        this.StyleByID.Cache.RaiseFieldUpdating(field, (object) null, ref newValue);
      this.StyleByID.Cache.RaiseFieldSelecting(field, (object) null, ref newValue, true);
      e.ReturnState = newValue;
    }
    else
      e.ReturnState = this.StyleByID.Cache.GetStateExt((object) data, field);
    if (!(e.ReturnState is PXFieldState))
      return;
    ((PXFieldState) e.ReturnState).SetFieldName("Style" + field);
  }

  protected virtual void styleFieldUpdating(PXFieldUpdatingEventArgs e, string field)
  {
    RMReport row = (RMReport) e.Row;
    PXResultset<RMStyle> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMStyle>) null;
    else
      pxResultset = this.StyleByID.Select((object) row.StyleID);
    RMStyle rmStyle = (RMStyle) pxResultset;
    if (rmStyle == null)
    {
      object newValue1 = e.NewValue;
      this.StyleByID.Cache.RaiseFieldUpdating(field, (object) null, ref newValue1);
      object newValue2;
      if (this.StyleByID.Cache.RaiseFieldDefaulting(field, (object) null, out newValue2))
        this.StyleByID.Cache.RaiseFieldUpdating(field, (object) null, ref newValue2);
      if (!object.Equals(newValue1, newValue2))
      {
        rmStyle = this.StyleByID.Insert(new RMStyle());
        this.Report.Current.StyleID = rmStyle.StyleID;
        this.Report.Update(this.Report.Current);
      }
      this.StyleByID.Cache.SetValueExt((object) rmStyle, field, e.NewValue);
    }
    else
    {
      this.StyleByID.Cache.SetValueExt((object) rmStyle, field, e.NewValue);
      this.StyleByID.Cache.MarkUpdated((object) rmStyle);
    }
  }

  protected virtual void dataSourceFieldSelecting(PXFieldSelectingEventArgs e, string field)
  {
  }

  public virtual bool IsFieldVisible(string field, RMReport report) => false;

  protected virtual void dataSourceFieldUpdating(PXFieldUpdatingEventArgs e, string field)
  {
    RMReport row = (RMReport) e.Row;
    PXResultset<RMDataSource> pxResultset;
    if (row == null)
      pxResultset = (PXResultset<RMDataSource>) null;
    else
      pxResultset = this.DataSourceByID.Select((object) row.DataSourceID);
    RMDataSource rmDataSource = (RMDataSource) pxResultset;
    if (rmDataSource == null)
    {
      object newValue1 = e.NewValue;
      this.DataSourceByID.Cache.RaiseFieldUpdating(field, (object) null, ref newValue1);
      object newValue2;
      if (this.DataSourceByID.Cache.RaiseFieldDefaulting(field, (object) null, out newValue2))
        this.DataSourceByID.Cache.RaiseFieldUpdating(field, (object) null, ref newValue2);
      if (!object.Equals(newValue1, newValue2))
      {
        rmDataSource = this.DataSourceByID.Insert(new RMDataSource());
        this.Report.Current.DataSourceID = rmDataSource.DataSourceID;
        this.Report.Update(this.Report.Current);
      }
      this.DataSourceByID.Cache.Current = (object) rmDataSource;
      this.DataSourceByID.Cache.SetValueExt((object) rmDataSource, field, e.NewValue);
    }
    else
    {
      this.DataSourceByID.Cache.Current = (object) rmDataSource;
      this.DataSourceByID.Cache.SetValueExt((object) rmDataSource, field, e.NewValue);
      this.DataSourceByID.Cache.MarkUpdated((object) rmDataSource);
    }
  }

  [Serializable]
  public class ParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "New Code", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string NewReportCode { get; set; }
  }
}
