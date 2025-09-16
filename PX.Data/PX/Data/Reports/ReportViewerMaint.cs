// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportViewerMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.IO;
using PX.Common.Mail;
using PX.CS;
using PX.Data.Reports.DAC;
using PX.Data.Reports.Services;
using PX.Data.Services.Interfaces;
using PX.Data.Wiki.Parser;
using PX.Data.WorkflowAPI;
using PX.Reports;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Mail;
using PX.Reports.Render;
using PX.Reports.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data.Reports;

public class ReportViewerMaint : PXGraph<ReportViewerMaint>
{
  private WebReport _webReport;
  public PXSelect<PX.Data.Reports.DAC.Report> Report;
  public PXSelect<GroupNode> GroupsTree;
  public PXSelect<PX.SM.SiteMap, Where<PX.SM.SiteMap.url, Equal<Required<PX.SM.SiteMap.url>>, Or<PX.SM.SiteMap.url, Like<Required<PX.SM.SiteMap.url>>>>> ReportSiteMap;
  public PXAction<PX.Data.Reports.DAC.Report> EditParameters;
  public PXAction<PX.Data.Reports.DAC.Report> Refresh;
  public PXAction<PX.Data.Reports.DAC.Report> Groups;
  public PXAction<PX.Data.Reports.DAC.Report> ViewPdf;
  public PXAction<PX.Data.Reports.DAC.Report> HtmlMode;
  public PXAction<PX.Data.Reports.DAC.Report> GotoFirstPage;
  public PXAction<PX.Data.Reports.DAC.Report> GotoPreviousPage;
  public PXAction<PX.Data.Reports.DAC.Report> GotoNextPage;
  public PXAction<PX.Data.Reports.DAC.Report> GotoLastPage;
  public PXAction<PX.Data.Reports.DAC.Report> Print;
  public PXAction<PX.Data.Reports.DAC.Report> SendReport;
  public PXAction<PX.Data.Reports.DAC.Report> Export;
  public PXAction<PX.Data.Reports.DAC.Report> Navigate;
  protected internal const string EXPORT_TO_EXCEL = "EXPORT_TO_EXCEL";
  protected internal const string EXPORT_TO_PDF = "EXPORT_TO_PDF";
  private static readonly object _lockObj = new object();

  public ReportViewerMaint() => this.AttachActions();

  [InjectDependency]
  protected ISendReportService SendReportService { get; set; }

  [InjectDependency]
  private IReportFacade ReportFacade { get; set; }

  [InjectDependency]
  private IReportRenderer Renderer { get; set; }

  [InjectDependency]
  private IUrlResolver UrlResolver { get; set; }

  public static ReportViewerMaint CreateInstance(string instanceId, bool isNew = false)
  {
    ReportViewerMaint instance = PXGraph.CreateInstance<ReportViewerMaint>();
    instance._webReport = instance.ReportFacade.GetReport(instanceId);
    if (isNew)
    {
      instance.Report.Cache.Unload();
      PX.Data.Reports.DAC.Report report = instance.Report.Insert();
      report.ReportName = instance._webReport?.Report.ReportName;
      report.ScreenId = instance._webReport?.ScreenId;
      report.InstanceId = instanceId;
    }
    else
      instance.Report.Cache.LoadFromSession(true);
    instance.Navigate.SetVisible(false);
    return instance;
  }

  protected virtual void AttachActions()
  {
    this.Export.SetMenu(new List<ButtonMenu>()
    {
      new ButtonMenu("EXPORT_TO_EXCEL", "Excel", (string) null),
      new ButtonMenu("EXPORT_TO_PDF", "Pdf", (string) null)
    }.ToArray());
  }

  protected IEnumerable report()
  {
    return (IEnumerable) EnumerableExtensions.AsSingleEnumerable<PX.Data.Reports.DAC.Report>(this.Report.Current);
  }

  protected virtual IEnumerable groupsTree()
  {
    return (IEnumerable) (((Tuple<object, object>) this._webReport.Tag)?.Item2 is ARmReportNode armNode ? ReportViewerMaint.GetRootARmGroups(armNode) : this.GetReportGroups(this._webReport.ReportNode?.Groups)).OrderBy<GroupNode, string>((Func<GroupNode, string>) (group => group.Description));
  }

  private static IEnumerable<GroupNode> GetRootARmGroups(ARmReportNode armNode)
  {
    yield return new GroupNode()
    {
      GroupId = "-1",
      Description = "Print All"
    };
    if (armNode.Unit != null)
      yield return new GroupNode()
      {
        GroupId = armNode.Unit.Code,
        Description = armNode.Unit.Description
      };
    foreach (GroupNode armGroupsFromItem in ReportViewerMaint.GetARmGroupsFromItems(armNode.Items))
      yield return armGroupsFromItem;
  }

  private static IEnumerable<GroupNode> GetARmGroupsFromItems(
    ARmReportNodeCollection items,
    string parentId = null)
  {
    foreach (ARmReportNode armReportNode in (CollectionBase) items)
    {
      ARmReportNode node = armReportNode;
      if (node.Unit != null)
      {
        string groupId = node.Unit.Code;
        yield return new GroupNode()
        {
          GroupId = groupId,
          Description = node.Unit.Description,
          ParentGroupId = parentId
        };
        if (((CollectionBase) node.Items).Count > 0)
        {
          foreach (GroupNode armGroupsFromItem in ReportViewerMaint.GetARmGroupsFromItems(node.Items, groupId))
            yield return armGroupsFromItem;
          groupId = (string) null;
          node = (ARmReportNode) null;
        }
      }
    }
  }

  protected IEnumerable<GroupNode> GetReportGroups(
    List<GroupNode> groups,
    int depth = 1,
    string parentId = null)
  {
    if (groups != null && groups.Count != 0)
    {
      int navigationDepth = this._webReport.Report.NavigationDepth;
      bool fillChild = navigationDepth == 0 || navigationDepth > depth;
      foreach (GroupNode group1 in groups)
      {
        GroupNode group = group1;
        int num = group.PageIndex;
        string str1 = num.ToString();
        num = group.GetHashCode();
        string str2 = num.ToString();
        string groupId = $"{str1}|{str2}";
        yield return new GroupNode()
        {
          GroupId = groupId,
          Description = group.Description,
          ParentGroupId = parentId
        };
        if (fillChild && group.SubGroups.Count > 0)
        {
          foreach (GroupNode reportGroup in this.GetReportGroups(group.SubGroups, depth + 1, groupId))
            yield return reportGroup;
          groupId = (string) null;
          group = (GroupNode) null;
        }
      }
    }
  }

  [PXButton(MenuAutoOpen = true, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Export")]
  public virtual IEnumerable export(PXAdapter adapter)
  {
    PX.SM.FileInfo fileInfo1;
    switch (adapter.Menu)
    {
      case "EXPORT_TO_EXCEL":
        fileInfo1 = this.GetReportFileInfo("Excel");
        break;
      case "EXPORT_TO_PDF":
        fileInfo1 = this.GetReportFileInfo("PDF");
        break;
      default:
        fileInfo1 = (PX.SM.FileInfo) null;
        break;
    }
    PX.SM.FileInfo fileInfo2 = fileInfo1;
    if (fileInfo2 != null)
      throw new PXRedirectToFileException(fileInfo2, true);
    yield return (object) this.Report.Current;
  }

  private PX.SM.FileInfo GetReportFileInfo(string format)
  {
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    Hashtable deviceInfo = this._webReport.DeviceInfo;
    deviceInfo[(object) "PageNumber"] = (object) current.PageIndex;
    deviceInfo[(object) "StartPage"] = (object) -1;
    deviceInfo[(object) "EndPage"] = (object) -1;
    string str1;
    string str2;
    Encoding encoding;
    byte[] data = this.Renderer.Render(format, this._webReport.Report, deviceInfo, ref str1, ref str2, ref encoding);
    return data == null ? (PX.SM.FileInfo) null : new PX.SM.FileInfo($"{this.GetReportTitle()}.{str2}", (string) null, data);
  }

  private string GetReportTitle()
  {
    return Str.NullIfWhitespace(this._webReport.ReportNode.ExportFileName) ?? $"{this._webReport.Report.Title} ({System.DateTime.Today.ToString("yyyy-MM-dd")})";
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  public virtual IEnumerable navigate(PXAdapter adapter)
  {
    string commandArguments = adapter.CommandArguments;
    if (string.IsNullOrEmpty(commandArguments))
      return adapter.Get();
    if (!(((Tuple<object, object>) this._webReport.Tag)?.Item2 is ARmReportNode armReportNode))
    {
      this.NavigateFromReport(commandArguments);
      return adapter.Get();
    }
    long result;
    if (!long.TryParse(commandArguments, out result))
      return adapter.Get();
    this.RedirectToDrilldownUrl(!armReportNode.ActiveNode.IsDummyRoot || !armReportNode.ActiveNode.HasChildren ? armReportNode.ActiveNode.NavigateItems[result] : ReportViewerMaint.FindArmNodeByHash(armReportNode.ActiveNode.Items, result));
    return adapter.Get();
  }

  private static string GetDataGraphContextKey(System.Type type) => "0_DATAGRAPH_" + type.FullName;

  private static TextBox FindArmNodeByHash(ARmReportNodeCollection reports, long hash)
  {
    foreach (ARmReportNode report in (CollectionBase) reports)
    {
      TextBox armNodeByHash;
      if (report.NavigateItems.TryGetValue(hash, out armNodeByHash) || report.HasChildren && (armNodeByHash = ReportViewerMaint.FindArmNodeByHash(report.Items, hash)) != null)
        return armNodeByHash;
    }
    return (TextBox) null;
  }

  private void NavigateFromReport(string data)
  {
    long result;
    if (long.TryParse(data, out result))
      throw new PXRedirectToUrlException(this.GetNavigateUrl(this._webReport.ReportNode.NavigateItems[result]), string.Empty);
  }

  private string GetNavigateUrl(TextNode node)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (string.IsNullOrEmpty(node.NavigateReport))
      return this.GetRedirectUrl(((ItemNode) node).Navigator, ((ItemNode) node).DataItem, node.DataField);
    string absolute = this.UrlResolver.ToAbsolute(PXSiteMap.Provider.FindSiteMapNodeByScreenID(this._webReport.ScreenId.Replace(".", "")).Url);
    stringBuilder.Append(absolute);
    stringBuilder.Append("?id=").Append(node.NavigateReport);
    ExternalParameterCollection navigateParams = node.NavigateParams;
    if (navigateParams == null || ((List<ExternalParameter>) navigateParams).Count <= 0)
      return stringBuilder.ToString();
    foreach (ExternalParameter navigateParam in (List<ExternalParameter>) node.NavigateParams)
    {
      string str = navigateParam.Value?.ToString() ?? string.Empty;
      stringBuilder.Append("&").Append(navigateParam.Name).Append("=").Append(str);
    }
    return stringBuilder.ToString();
  }

  public string GetRedirectUrl(IDataNavigator navigator, object row, string dataField)
  {
    if (!(navigator is SoapNavigator soapNavigator))
      return (string) null;
    object incoming = row;
    PXCache cache;
    string[] fieldList;
    row = soapNavigator.GetItem(row, dataField, out cache, out fieldList, true);
    if (row == null)
      return (string) null;
    object verifyRow = cache._Clone(row);
    if (fieldList != null)
    {
      for (int index = 0; index < fieldList.Length - soapNavigator.FieldCount; ++index)
      {
        int num = fieldList[index].IndexOf('.');
        PXCache pxCache = cache;
        object data = verifyRow;
        string fieldName;
        if (num == -1)
        {
          fieldName = fieldList[index];
        }
        else
        {
          string str = fieldList[index];
          int startIndex = num + 1;
          fieldName = str.Substring(startIndex, str.Length - startIndex);
        }
        pxCache.SetValue(data, fieldName, (object) null);
      }
    }
    try
    {
      ReportViewerMaint.RedirectToGraph(soapNavigator, cache, row, incoming, verifyRow, dataField);
    }
    catch (TargetInvocationException ex)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
    return (string) null;
  }

  private static void RedirectToGraph(
    SoapNavigator soapNavigator,
    PXCache cache,
    object row,
    object incoming,
    object verifyRow,
    string dataField)
  {
    object obj = verifyRow;
    System.Type graphType;
    System.Type declaredType;
    PXPrimaryGraphBaseAttribute primaryGraph = PXPrimaryGraphAttribute.FindPrimaryGraph(cache, true, ref verifyRow, out graphType, out declaredType, out cache);
    if (!(graphType == (System.Type) null))
    {
      PXGraph instance = PXGraph.CreateInstance(graphType);
      System.Type filter = primaryGraph is PXPrimaryGraphAttribute primaryGraphAttribute ? primaryGraphAttribute.Filter : (System.Type) null;
      if (filter == (System.Type) null)
      {
        if (obj != verifyRow)
        {
          row = verifyRow;
          declaredType = verifyRow.GetType();
        }
        ReportViewerMaint.RedirectToGraphWithoutFilter(instance, row, declaredType);
      }
      else
        ReportViewerMaint.RedirectToGraphWithFilter(soapNavigator, cache, incoming, verifyRow, dataField, filter, instance);
      throw new PXRedirectRequiredException(instance, true, string.Empty);
    }
  }

  private static void RedirectToGraphWithoutFilter(PXGraph graph, object row, System.Type dacType)
  {
    PXCache primary = graph.Caches[dacType];
    System.Type itemType = primary.GetItemType();
    if (!itemType.IsAssignableFrom(row.GetType()))
      throw new PXRedirectRequiredException(graph, true, string.Empty);
    long num = 1;
    primary.Current = row;
    if (graph.PrimaryItemType == itemType && primary.Keys.Count > 0)
    {
      List<string> stringList = new List<string>((IEnumerable<string>) primary.Keys);
      List<object> objectList = new List<object>(primary.Keys.Select<string, object>((Func<string, object>) (key => PXFieldState.UnwrapValue(primary.GetValueExt(row, key)))));
      int startRow = 0;
      int totalRows = 0;
      num = graph.ExecuteSelect(graph.PrimaryView, (object[]) null, objectList.ToArray(), stringList.ToArray(), (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).Count();
    }
    if (num == 0L)
    {
      object stateExt = primary.GetStateExt(row, primary.Keys.Last<string>());
      throw new PXSetPropertyException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        stateExt is PXFieldState pxFieldState ? (object) pxFieldState.DisplayName : (object) primary.Keys.Last<string>(),
        PXFieldState.UnwrapValue(stateExt)
      });
    }
  }

  private static void RedirectToGraphWithFilter(
    SoapNavigator soapNavigator,
    PXCache cache,
    object incoming,
    object row,
    string dataField,
    System.Type filterType,
    PXGraph graph)
  {
    PXCache cache1;
    object data1 = soapNavigator.GetItem(incoming, dataField, out cache1, out string[] _, false);
    PXCache cach = graph.Caches[filterType];
    object data2 = cach._Clone(cach.Current);
    foreach (string field in (List<string>) cach.Fields)
      cach.SetValueExt(data2, field, (object) null);
    cach.Update(data2);
    foreach (string field in (List<string>) cache.Fields)
    {
      object valueExt1 = cache.GetValueExt(row, field);
      if (valueExt1 is PXFieldState pxFieldState1)
        valueExt1 = pxFieldState1.Value;
      if (valueExt1 != null)
      {
        if (data1 != null && cache == cache1)
        {
          object valueExt2 = cache.GetValueExt(data1, field);
          if (valueExt2 is PXFieldState pxFieldState2)
            valueExt2 = pxFieldState2.Value;
          if (!object.Equals(valueExt1, valueExt2))
            continue;
        }
        cach.SetValueExt(data2, field, valueExt1);
      }
    }
    cach.Update(data2);
    foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) graph.Views.Values))
    {
      if (pxView.GetItemType() == filterType)
        pxView.SelectSingle();
    }
  }

  private void RedirectToDrilldownUrl(TextBox text)
  {
    IARmDataSource ds2 = (IARmDataSource) this.GetRmReportReader();
    if (ds2 != null && ((ReportItem) text).Tag is ARmCellNode[] tag)
    {
      ARmDataSet[] array = ((IEnumerable<ARmCellNode>) tag).Select<ARmCellNode, ARmDataSet>((Func<ARmCellNode, ARmDataSet>) (cell => this.GetDataSet(ds2, cell))).ToArray<ARmDataSet>();
      string str = ds2.GetDrilldownUrl(((IEnumerable<ARmDataSet>) array).ToArray<ARmDataSet>());
      if (!string.IsNullOrEmpty(str) && !this.UrlResolver.IsAbsolute(str))
        str = this.UrlResolver.ToAbsolute(str);
      throw new PXRedirectToUrlException(str, string.Empty);
    }
  }

  private RMReportReader GetRmReportReader()
  {
    string dataGraphContextKey = ReportViewerMaint.GetDataGraphContextKey(typeof (RMReportReader));
    lock (ReportViewerMaint._lockObj)
    {
      RMReportReader slot = PXContext.GetSlot<RMReportReader>(dataGraphContextKey);
      if (slot != null)
        return slot;
      RMReportReader instance = PXGraph.CreateInstance<RMReportReader>();
      PXContext.SetSlot<RMReportReader>(dataGraphContextKey, instance);
      return instance;
    }
  }

  private ARmDataSet GetDataSet(IARmDataSource dataSource, ARmCellNode cell)
  {
    ARmDataSetValue.Item dataValue = cell.DataValue;
    ARmReportNode report = cell.Report;
    if (report != null && report.DataSource == null)
    {
      IARmDataSource rmReportReader = (IARmDataSource) this.GetRmReportReader();
      if (rmReportReader != null)
        cell.Report.DataSource = rmReportReader.GetUnderlyingDataSource();
    }
    ARmDataSet dataSet1 = cell.GetDataSet();
    if (!string.IsNullOrEmpty(dataValue?.Code))
    {
      ARmRow row = cell.Row;
      if (row != null && row.DataSet != null)
      {
        ARmDataSet dataSet2 = new ARmDataSet(dataSet1)
        {
          Expand = cell.Row.DataSet.Expand
        };
        dataSource.GetDrilldownUrl(new ARmDataSet[1]
        {
          dataSet2
        });
        if (!((dataSource.GetUnderlyingDataSource() is RMReportReader underlyingDataSource ? underlyingDataSource.GetHistoryValue(dataSet2, false) : (object) null) is List<object[]> historyValue))
          return dataSet1;
        object[] objArray = historyValue.FirstOrDefault<object[]>((Func<object[], bool>) (h => (string) h[0] == dataValue.Code));
        return objArray != null ? (ARmDataSet) objArray[3] : dataSet1;
      }
    }
    return dataSet1;
  }

  [PXButton(Connotation = ActionConnotation.Success)]
  [PXUIField(DisplayName = "Email")]
  protected IEnumerable sendReport(PXAdapter adapter)
  {
    GroupMessage groupMessage = this.GetGroupMessage();
    List<PX.SM.FileInfo> reportAttachments = this.GetReportAttachments(groupMessage);
    this.Report.Cache.IsDirty = false;
    this.SendReportService.SendEmail(groupMessage, (IList<PX.SM.FileInfo>) reportAttachments);
    yield return (object) this.Report.Current;
  }

  [PXButton]
  [PXUIField(DisplayName = "Print")]
  protected IEnumerable print(PXAdapter adapter)
  {
    yield return (object) this.Report.Current;
  }

  [PXButton(ImageKey = "Pdf", IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "View PDF")]
  protected IEnumerable viewPdf(PXAdapter adapter)
  {
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    current.ViewPdf = new bool?(true);
    this.Report.Update(current);
    yield return (object) this.Report.Current;
  }

  [PXButton(ImageKey = "Html", IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "View HTML")]
  protected IEnumerable htmlMode(PXAdapter adapter)
  {
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    current.ViewPdf = new bool?(false);
    this.Report.Update(current);
    yield return (object) this.Report.Current;
  }

  [PXButton(ImageKey = "LevelDown", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Groups")]
  protected IEnumerable groups(PXAdapter adapter) => adapter.Get();

  [PXButton(ImageKey = "Refresh", SpecialType = PXSpecialButtonType.Refresh, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Refresh")]
  protected IEnumerable refresh(PXAdapter adapter)
  {
    yield return (object) this.Report.Current;
  }

  [PXButton(ImageKey = "RecordEdit", IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Edit Report Parameters")]
  protected IEnumerable editParameters(PXAdapter adapter)
  {
    string screenID = this._webReport.ScreenId.Replace(".", "");
    if (!string.IsNullOrEmpty(screenID) && screenID.EndsWith(".rpx", StringComparison.OrdinalIgnoreCase))
    {
      string str = screenID;
      screenID = str.Substring(0, str.Length - 4);
    }
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
    bool isARmReport = screenIdUnsecure != null && PXSiteMap.IsARmReport(screenIdUnsecure.Url);
    ReportMaint instance = ReportMaint.CreateInstance(Str.NullIfWhitespace(((ReportItem) this._webReport.Report).Name) ?? this._webReport.Report.ReportName, isARmReport);
    throw new PXRedirectRequiredException(screenIdUnsecure.Url, (PXGraph) instance, PXBaseRedirectException.WindowMode.InlineWindow, string.Empty);
  }

  [PXButton(ImageKey = "PageFirst", SpecialType = PXSpecialButtonType.First, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "First")]
  protected IEnumerable gotoFirstPage(PXAdapter adapter)
  {
    ReportViewerMaint reportViewerMaint1 = this;
    PX.Data.Reports.DAC.Report current = reportViewerMaint1.Report.Current;
    int? pageIndex = current.PageIndex;
    int num = 0;
    if (pageIndex.GetValueOrDefault() > num & pageIndex.HasValue)
    {
      current.PageIndex = new int?(0);
      reportViewerMaint1.Report.Update(current);
      ReportViewerMaint reportViewerMaint2 = reportViewerMaint1;
      pageIndex = current.PageIndex;
      int valueOrDefault = pageIndex.GetValueOrDefault();
      reportViewerMaint2.CheckNavigationEnabled(valueOrDefault);
    }
    reportViewerMaint1.Unload();
    yield return (object) current;
  }

  [PXButton(ImageKey = "PagePrev", SpecialType = PXSpecialButtonType.Prev, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Previous")]
  protected IEnumerable gotoPreviousPage(PXAdapter adapter)
  {
    ReportViewerMaint reportViewerMaint1 = this;
    PX.Data.Reports.DAC.Report current = reportViewerMaint1.Report.Current;
    int? pageIndex = current.PageIndex;
    int num = 0;
    if (pageIndex.GetValueOrDefault() > num & pageIndex.HasValue)
    {
      PX.Data.Reports.DAC.Report report = current;
      pageIndex = report.PageIndex;
      int? nullable = pageIndex;
      report.PageIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() - 1) : new int?();
      reportViewerMaint1.Report.Update(current);
      ReportViewerMaint reportViewerMaint2 = reportViewerMaint1;
      pageIndex = current.PageIndex;
      int valueOrDefault = pageIndex.GetValueOrDefault();
      reportViewerMaint2.CheckNavigationEnabled(valueOrDefault);
    }
    reportViewerMaint1.Unload();
    yield return (object) current;
  }

  [PXButton(ImageKey = "PageNext", SpecialType = PXSpecialButtonType.Next, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Next")]
  protected IEnumerable gotoNextPage(PXAdapter adapter)
  {
    ReportViewerMaint reportViewerMaint1 = this;
    PX.Data.Reports.DAC.Report current = reportViewerMaint1.Report.Current;
    int? pageIndex = current.PageIndex;
    int num = reportViewerMaint1.PageCount - 1;
    if (pageIndex.GetValueOrDefault() < num & pageIndex.HasValue)
    {
      PX.Data.Reports.DAC.Report report = current;
      pageIndex = report.PageIndex;
      int? nullable = pageIndex;
      report.PageIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
      reportViewerMaint1.Report.Update(current);
      ReportViewerMaint reportViewerMaint2 = reportViewerMaint1;
      pageIndex = current.PageIndex;
      int valueOrDefault = pageIndex.GetValueOrDefault();
      reportViewerMaint2.CheckNavigationEnabled(valueOrDefault);
    }
    reportViewerMaint1.Unload();
    yield return (object) current;
  }

  [PXButton(ImageKey = "PageLast", SpecialType = PXSpecialButtonType.Last, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Last")]
  protected IEnumerable gotoLastPage(PXAdapter adapter)
  {
    ReportViewerMaint reportViewerMaint = this;
    PX.Data.Reports.DAC.Report current = reportViewerMaint.Report.Current;
    int num1 = reportViewerMaint.PageCount - 1;
    int? pageIndex = current.PageIndex;
    int num2 = num1;
    if (pageIndex.GetValueOrDefault() < num2 & pageIndex.HasValue)
    {
      current.PageIndex = new int?(num1);
      reportViewerMaint.Report.Update(current);
      reportViewerMaint.CheckNavigationEnabled(current.PageIndex.GetValueOrDefault());
    }
    reportViewerMaint.Unload();
    yield return (object) current;
  }

  protected void _(Events.RowSelected<GroupNode> e)
  {
    GroupNode row = e.Row;
    if (row == null || !(((Tuple<object, object>) this._webReport.Tag)?.Item2 is ARmReportNode armReportNode))
      return;
    armReportNode.ActiveNode = row.GroupId == "-1" ? armReportNode : armReportNode.Items.Find(row.GroupId, true);
    this.ReportFacade.SetReport(this._webReport.InstanceID, this._webReport);
  }

  protected void _(Events.RowSelected<PX.Data.Reports.DAC.Report> e)
  {
    PX.Data.Reports.DAC.Report row = e.Row;
    bool isVisible = row.ViewPdf.HasValue && row.ViewPdf.Value;
    this.HtmlMode.SetVisible(isVisible);
    this.ViewPdf.SetVisible(!isVisible);
    this.Print.SetVisible(!isVisible);
    this.GotoFirstPage.SetVisible(!isVisible);
    this.GotoLastPage.SetVisible(!isVisible);
    this.GotoNextPage.SetVisible(!isVisible);
    this.GotoPreviousPage.SetVisible(!isVisible);
    ARmReportNode armReportNode = ((Tuple<object, object>) this._webReport.Tag)?.Item2 as ARmReportNode;
    PX.Reports.Controls.Report report = this._webReport.Report;
    this.Groups.SetVisible((armReportNode != null && ((CollectionBase) armReportNode.Items).Count > 0 || report != null && report.NavigationTree && ((CollectionBase) report.Groups).Count > 0) && !isVisible && ((IEnumerable<ReportParameter>) this._webReport.Report.Parameters).Any<ReportParameter>());
    this.CheckNavigationEnabled(row.PageIndex.GetValueOrDefault());
  }

  private void CheckNavigationEnabled(int pageIndex)
  {
    this.GotoFirstPage.SetEnabled(pageIndex > 0);
    this.GotoLastPage.SetEnabled(pageIndex < this.PageCount - 1);
    this.GotoNextPage.SetEnabled(pageIndex < this.PageCount - 1);
    this.GotoPreviousPage.SetEnabled(pageIndex > 0);
  }

  private int PageCount
  {
    get
    {
      return !(this._webReport.DeviceInfo[(object) "Pages"] is ReportPageCollection reportPageCollection) ? 0 : __nonvirtual (((CollectionBase) reportPageCollection).Count);
    }
  }

  private GroupMessage GetGroupMessage()
  {
    GroupMessage groupMessage1 = (GroupMessage) null;
    ReportNode reportNode = this._webReport.ReportNode;
    object obj;
    if (reportNode == null)
    {
      obj = (object) null;
    }
    else
    {
      List<GroupNode> groups = reportNode.Groups;
      obj = groups != null ? (object) groups.Select<GroupNode, MailSettings>((Func<GroupNode, MailSettings>) (g => g.MailSettings)) : (object) null;
    }
    if (obj == null)
      obj = (object) Array.Empty<MailSettings>();
    foreach (MailSettings mailSettings in (IEnumerable<MailSettings>) obj)
    {
      if (mailSettings != null && mailSettings.ShouldSerialize())
      {
        groupMessage1 = MailSettings.op_Implicit(mailSettings);
        break;
      }
    }
    GroupMessage groupMessage2 = groupMessage1 ?? GroupMessage.Empty;
    string str1 = groupMessage2.Report.Name;
    if (str1 != null)
      str1 = FilenameValidator.GetValidFilename(str1.Trim());
    PX.Reports.Controls.Report report = this._webReport.Report;
    string str2 = string.IsNullOrEmpty(report.ViewMailSettings.EMail) ? ((MailSender.MailMessageT) groupMessage2).From : report.ViewMailSettings.EMail;
    string reply = string.IsNullOrEmpty(report.ViewMailSettings.EMail) ? ((MailSender.MailMessageT) groupMessage2).Addressee.Reply : report.ViewMailSettings.EMail;
    string subject = string.IsNullOrEmpty(report.ViewMailSettings.Subject) ? ((MailSender.MailMessageT) groupMessage2).Content.Subject : report.ViewMailSettings.Subject;
    string str3 = string.IsNullOrEmpty(report.ViewMailSettings.Format) ? report.MailSettings.Format : report.ViewMailSettings.Format;
    string cc = string.IsNullOrEmpty(report.ViewMailSettings.Cc) ? ((MailSender.MailMessageT) groupMessage2).Addressee.Cc : report.ViewMailSettings.Cc;
    string bcc = string.IsNullOrEmpty(report.ViewMailSettings.Bcc) ? ((MailSender.MailMessageT) groupMessage2).Addressee.Bcc : report.ViewMailSettings.Bcc;
    MailSender.MessageAddressee messageAddressee = new MailSender.MessageAddressee(string.IsNullOrEmpty(((MailSender.MailMessageT) groupMessage2).Addressee.To) ? str2 ?? string.Empty : ((MailSender.MailMessageT) groupMessage2).Addressee.To, reply, cc, bcc);
    MailSender.MessageContent messageContent = new MailSender.MessageContent(subject, ((MailSender.MailMessageT) groupMessage2).Content.IsHtml, ((MailSender.MailMessageT) groupMessage2).Content.Body);
    return new GroupMessage(str2, ((MailSender.MailMessageT) groupMessage2).UID, messageAddressee, messageContent, groupMessage2.TemplateID, str3, groupMessage2.Relationship, new ReportAttachment((object) reportNode, str3, str1));
  }

  private List<PX.SM.FileInfo> GetReportAttachments(GroupMessage groupMessage)
  {
    Message message = new Message(groupMessage, this._webReport.ReportNode, (GroupMessage) null);
    List<PX.SM.FileInfo> reportAttachments = new List<PX.SM.FileInfo>();
    List<ReportStream> attachments = message.Attachments;
    if (attachments == null || attachments.Count <= 0)
      return reportAttachments;
    foreach (ReportStream reportStream in attachments)
    {
      string name1 = reportStream.Name;
      string[] strArray1 = name1.Split('\\');
      Guid uid;
      if (strArray1.Length != 0)
      {
        string[] strArray2 = strArray1;
        if (GUID.TryParse(strArray2[strArray2.Length - 1], ref uid))
          goto label_7;
      }
      uid = Guid.NewGuid();
label_7:
      string str1 = name1;
      if (!Path.HasExtension(name1))
      {
        string extension = MimeTypes.GetExtension(reportStream.MimeType);
        if (extension != null)
          str1 += extension;
      }
      string str2 = this._webReport.Report.ReportName;
      int length = str2.LastIndexOf('.');
      if (length > -1)
        str2 = str2.Substring(0, length);
      string name2 = $"{str2}\\{str1}";
      reportAttachments.Add(new PX.SM.FileInfo(uid, name2, (string) null, ((MemoryStream) reportStream).ToArray()));
    }
    return reportAttachments;
  }

  public override string PrimaryView => "Report";

  public string GetTimeStamp() => this._webReport.TimeStamp;
}
