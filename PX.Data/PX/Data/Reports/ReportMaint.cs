// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.CS;
using PX.Data.Localization;
using PX.Data.Maintenance.SM.SendRecurringNotifications;
using PX.Data.Reports.DAC;
using PX.Data.WorkflowAPI;
using PX.Metadata;
using PX.Reports;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Web;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace PX.Data.Reports;

public class ReportMaint : PXGraph<ReportMaint>
{
  private const string timeStampFormat = "yyyy-MM-dd HH:mm:ss";
  internal PX.Reports.Controls.Report ReportDescription;
  private WebReport _webReport;
  private RMReportReader _rmReportReader;
  private IDataNavigator _navigator;
  private PX.Data.Reports.DAC.ReportSettings _defaultSettings;
  private PX.Data.Reports.DAC.ReportSettings _currentSettings;
  private string _reportID;
  private bool _isARmReport;
  private bool _manualDeactivation;
  protected internal const string EDIT_REPORT = "Edit";
  protected internal const string SAVE_TEMPLATE = "TemplateSave";
  protected internal const string SAVE_TEMPLATE_AS = "SaveTemplateAs";
  protected internal const string EDIT_TEMPLATE = "EditTemplate";
  protected internal const string REMOVE_TEMPLATE = "TemplateRemove";
  public PXSelect<PX.Data.Reports.DAC.Report> Report;
  public PXSelect<ReportParameter> Parameters;
  public PXSelect<SortExp> Sorting;
  public PXSelect<FilterExp> Conditions;
  public PXSelect<UserReport, Where<UserReport.reportFileName, Equal<Required<UserReport.reportFileName>>>> UserReports;
  public PXSelect<UserReport> ReportVersion;
  public PXSelect<UserReport, Where<UserReport.reportFileName, Equal<Required<UserReport.reportFileName>>, And<UserReport.version, Equal<Required<UserReport.version>>>>> UserVersionReport;
  public PXSelect<PX.Data.Reports.DAC.ReportSettings, Where<PXSettingProvider.ReportSettings.screenID, Equal<Required<PXSettingProvider.ReportSettings.screenID>>, And<Where<PX.Data.Reports.DAC.ReportSettings.username, Equal<Required<PX.Data.Reports.DAC.ReportSettings.username>>, Or<PXSettingProvider.ReportSettings.isShared, Equal<PX.Data.Reports.DAC.ReportSettings.shared>>>>>> ReportSettings;
  public PXSelect<PX.Data.Reports.DAC.ReportSettings, Where<PXSettingProvider.ReportSettings.screenID, Equal<Required<PXSettingProvider.ReportSettings.screenID>>, And<Where<PX.Data.Reports.DAC.ReportSettings.username, Equal<Required<PX.Data.Reports.DAC.ReportSettings.username>>, And<PXSettingProvider.ReportSettings.name, Equal<Required<PXSettingProvider.ReportSettings.name>>>>>>> ExistingReportSettings;
  public PXSelect<PX.Data.Reports.DAC.ReportSettings, Where<PXSettingProvider.ReportSettings.screenID, Equal<Required<PXSettingProvider.ReportSettings.screenID>>, And<PX.Data.Reports.DAC.ReportSettings.username, Equal<Required<PX.Data.Reports.DAC.ReportSettings.username>>, And<PXSettingProvider.ReportSettings.isDefault, Equal<PX.Data.True>>>>> DefaultReportSettings;
  public PXSelectJoin<Notification, InnerJoin<NotificationReport, On<Notification.notificationID, Equal<NotificationReport.notificationID>>, LeftJoin<PX.Data.Reports.DAC.ReportSettings, On<PXSettingProvider.ReportSettings.settingsID, Equal<NotificationReport.reportTemplateID>>>>, Where<NotificationReport.screenID, Equal<Required<NotificationReport.screenID>>>> ReportNotifications;
  public PXSelectJoin<Notification, InnerJoin<NotificationReport, On<Notification.notificationID, Equal<NotificationReport.notificationID>>, LeftJoin<PX.Data.Reports.DAC.ReportSettings, On<PXSettingProvider.ReportSettings.settingsID, Equal<NotificationReport.reportTemplateID>>>>, Where<NotificationReport.screenID, Equal<Required<NotificationReport.screenID>>>> CurrentReportNotifications;
  public PXSelect<RMReport, Where<RMReport.reportCode, Equal<Required<RMReport.reportCode>>>> RmReport;
  public PXSelect<RMColumnSet, Where<RMColumnSet.columnSetCode, Equal<Required<RMColumnSet.columnSetCode>>>> RmColumnSet;
  public PXSelect<RMColumn, Where<RMColumn.columnSetCode, Equal<Required<RMColumn.columnSetCode>>>> RmColumn;
  public PXSelect<RMColumnHeader, Where<RMColumnHeader.columnSetCode, Equal<Required<RMColumnHeader.columnSetCode>>>> RmColumnHeader;
  public PXSelect<RMRowSet, Where<RMRowSet.rowSetCode, Equal<Required<RMRowSet.rowSetCode>>>> RmRowSet;
  public PXSelect<RMRow, Where<RMRow.rowSetCode, Equal<Required<RMRow.rowSetCode>>>> RmRow;
  public PXSelect<RMUnitSet, Where<RMUnitSet.unitSetCode, Equal<Required<RMUnitSet.unitSetCode>>>> RmUnitSet;
  public PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Required<RMUnit.unitSetCode>>>> RmUnit;
  public PXSelect<PX.SM.SiteMap, Where<PX.SM.SiteMap.url, Equal<Required<PX.SM.SiteMap.url>>, Or<PX.SM.SiteMap.url, Like<Required<PX.SM.SiteMap.url>>>>> ReportSiteMap;
  public PXSelect<PX.SM.SiteMap, Where<PX.SM.SiteMap.url, Equal<Required<PX.SM.SiteMap.url>>>> ClassicUiReportSiteMap;
  public PXAction<PX.Data.Reports.DAC.Report> Cancel;
  public PXAction<PX.Data.Reports.DAC.Report> RunReport;
  public PXAction<PX.Data.Reports.DAC.Report> SelectTemplate;
  public PXAction<UserReport> editVersion;
  public PXAction<UserReport> activateVersion;
  public PXAction<UserReport> deactivateVersion;
  public PXAction<PX.Data.Reports.DAC.Report> ReportMenu;
  public PXAction<Notification> ScheduleReport;
  public PXAction<Notification> ViewNotification;
  public PXFilter<TemplateProperties> EditTemplateDialog;
  private static readonly object _lockObj = new object();

  [InjectDependency]
  protected SettingsProvider SettingsProvider { get; set; }

  [InjectDependency]
  protected LocalizationProvider LocalizationProvider { get; set; }

  [InjectDependency]
  protected ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [InjectDependency]
  protected IReportDataBinder ReportDataBinder { get; set; }

  [InjectDependency]
  protected ILocalizationFeaturesService LocalizationFeaturesService { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  protected IOptions<PX.Data.ReportOptions> ReportOptions { get; set; }

  [InjectDependency]
  private IReportFacade ReportFacade { get; set; }

  [InjectDependency]
  protected IHttpContextAccessor HttpContextAccessor { get; set; }

  public ReportMaint() => this.AttachActions();

  protected virtual void AttachActions()
  {
    this.ReportMenu.SetMenu(new ButtonMenu[5]
    {
      new ButtonMenu("TemplateSave", "Save Template", (string) null)
      {
        ImageKey = "Save"
      },
      new ButtonMenu("SaveTemplateAs", "Save Template As", (string) null)
      {
        ImageKey = "Save"
      },
      new ButtonMenu("Edit", "Edit Report", (string) null)
      {
        ImageKey = "Settings"
      },
      new ButtonMenu("EditTemplate", "Edit Template", (string) null)
      {
        ImageKey = "RecordEdit"
      },
      new ButtonMenu("TemplateRemove", "Delete Template", (string) null)
      {
        ImageKey = "Remove"
      }
    });
  }

  protected override PXCacheCollection CreateCacheCollection()
  {
    return (PXCacheCollection) new ReportCacheCollection((PXGraph) this);
  }

  public static string GetRedirectUrlToReport(
    string reportID,
    bool isARmReport,
    Dictionary<string, string> parameters = null)
  {
    ReportMaint instance = PXGraph.CreateInstance<ReportMaint>();
    instance._reportID = reportID;
    instance._isARmReport = isARmReport;
    instance.PrepareReport(parameters);
    string instanceId = instance._webReport.InstanceID;
    ReportViewerMaint.CreateInstance(instanceId, true);
    return "~/Scripts/Screens/ReportViewer.html?instanceId=" + instanceId;
  }

  private void PrepareReport(Dictionary<string, string> parameters)
  {
    ARmReport armReport = (ARmReport) null;
    if (this._isARmReport)
    {
      armReport = this.GetARmReport(this._reportID);
      this.ReportDescription = ARmProcessor.CreateReport(armReport);
    }
    else
    {
      this.ReportDescription = new PX.Reports.Controls.Report();
      this.ReportDescription.LoadByName(this._reportID + ".rpx", this.LocalizationProvider);
    }
    this.ReportDescription.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
    this.FillReportSettingsFromRedirect(parameters);
    string instanceId = ReportMaint.GenerateInstanceId();
    this._webReport = new WebReport(this.ReportDescription, instanceId);
    this._webReport.ScreenId = this.GetReportScreenId();
    this.WriteReportSettings(true);
    if (this._isARmReport)
    {
      this._webReport.Tag = (object) new Tuple<object, object>((object) armReport, (object) null);
      this.RunARmReport();
    }
    else
    {
      this.ReportDescription.DataSource = (object) this.GetNavigator();
      this.ReportDescription.Title = this.GetReportSiteMap()?.Title ?? PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.GetReportScreenId()).Title;
    }
    this._webReport.TimeStamp = this.GetTimeStamp();
    this.ReportFacade.SetReport(instanceId, this._webReport);
  }

  private void FillReportSettingsFromRedirect(Dictionary<string, string> parametersValues)
  {
    if (parametersValues == null)
      return;
    this.FillReportParameters(ref parametersValues);
    if (!parametersValues.Any<KeyValuePair<string, string>>())
      return;
    this.FillReportFilters(parametersValues);
  }

  private void FillReportParameters(ref Dictionary<string, string> parametersValues)
  {
    ReportParameter data = this.Parameters.SelectSingle();
    PXCache cache = this.Parameters.Cache;
    foreach (ReportParameter parameter in (List<ReportParameter>) this.ReportDescription.Parameters)
    {
      string str;
      if (parametersValues.TryGetValue(parameter.Name, out str))
      {
        parameter.Value = (object) str;
        cache.SetValue((object) data, parameter.Name, (object) str);
        parametersValues.Remove(parameter.Name);
      }
    }
    cache.Unload();
  }

  private void FillReportFilters(Dictionary<string, string> parametersValues)
  {
    this.WriteReportConditions(true);
    PXCache cache = this.Conditions.Cache;
    bool flag = false;
    foreach (ViewerField viewerField in (List<ViewerField>) this.ReportDescription.ViewerFields)
    {
      string str1;
      if (!viewerField.Name.StartsWith("Row"))
      {
        str1 = viewerField.Name;
      }
      else
      {
        string name = viewerField.Name;
        str1 = name.Substring(3, name.Length - 3);
      }
      string str2 = str1;
      string str3;
      if (parametersValues.TryGetValue(str2, out str3))
      {
        if (!flag)
        {
          this.AddBracketsToFilters();
          flag = true;
        }
        this.ResetEqualsFiltersWithFieldName(str2);
        ((List<FilterExp>) this.ReportDescription.DynamicFilters).Add(new FilterExp(str2, (FilterCondition) 0)
        {
          Value = str3
        });
        FilterExp filterExp = this.Conditions.Insert();
        object newValue = (object) str3;
        cache.RaiseFieldUpdating<FilterExp.value>((object) filterExp, ref newValue);
        cache.SetValue<FilterExp.fieldName>((object) filterExp, (object) str2);
      }
    }
    cache.Unload();
  }

  private void AddBracketsToFilters()
  {
    if (((List<FilterExp>) this.ReportDescription.Filters).Count > 0)
    {
      ++((List<FilterExp>) this.ReportDescription.Filters)[0].OpenBraces;
      FilterExp filterExp = ((IEnumerable<FilterExp>) this.ReportDescription.Filters).Last<FilterExp>();
      ++filterExp.CloseBraces;
      filterExp.Operator = (FilterOperator) 0;
    }
    if (((List<FilterExp>) this.ReportDescription.DynamicFilters).Count <= 0)
      return;
    ++((List<FilterExp>) this.ReportDescription.DynamicFilters)[0].OpenBraces;
    FilterExp filterExp1 = ((IEnumerable<FilterExp>) this.ReportDescription.DynamicFilters).Last<FilterExp>();
    ++filterExp1.CloseBraces;
    filterExp1.Operator = (FilterOperator) 0;
    FilterExp[] source = this.Conditions.Select<FilterExp>();
    FilterExp filterExp2 = source[0];
    FilterExp data1 = ((IEnumerable<FilterExp>) source).Last<FilterExp>();
    PXCache cache = this.Conditions.Cache;
    FilterExp data2 = filterExp2;
    int? nullable = filterExp2.OpenBraces;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local1 = (ValueType) (nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?());
    cache.SetValue<FilterExp.openBraces>((object) data2, (object) local1);
    FilterExp data3 = data1;
    nullable = data1.CloseBraces;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local2 = (ValueType) (nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?());
    cache.SetValue<FilterExp.closeBraces>((object) data3, (object) local2);
    cache.SetValue<FilterExp.operation>((object) data1, (object) 0);
  }

  private void ResetEqualsFiltersWithFieldName(string fieldName)
  {
    foreach (FilterExp filterExp in ((IEnumerable<FilterExp>) this.ReportDescription.Filters).Where<FilterExp>((Func<FilterExp, bool>) (filter => filter.DataField.OrdinalIgnoreCaseEquals(fieldName) && filter.Condition == 0)))
    {
      filterExp.Condition = (FilterCondition) 12;
      filterExp.Value = string.Empty;
      filterExp.Value2 = string.Empty;
    }
  }

  public static ReportMaint CreateInstance(string reportID, bool isARmReport)
  {
    ReportMaint instance = PXGraph.CreateInstance<ReportMaint>();
    instance.CreateReport(reportID, isARmReport);
    instance.ReportDescription.ApplyRules((ReportItem) instance.ReportDescription);
    instance.SetLocalizationSettings();
    instance.SetVisibleAndEnableProperties();
    return instance;
  }

  [PXButton(Connotation = ActionConnotation.Success, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Run Report")]
  protected IEnumerable runReport(PXAdapter adapter)
  {
    this.ValidateReportParameters();
    this.FillReport();
    this.Parameters.Cache.Unload();
    this.Sorting.Cache.Unload();
    this.Conditions.Cache.Unload();
    this.Report.Cache.Unload();
    if (this._isARmReport)
      this.RunARmReport();
    else if (this.ReportDescription != null)
    {
      this.ReportDescription.DataSource = (object) this.GetNavigator();
      this.ReportDescription.Title = this.GetReportSiteMap()?.Title ?? PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.GetReportScreenId()).Title;
    }
    this._webReport.TimeStamp = this.GetTimeStamp();
    string instanceId = this.Report.Current.InstanceId;
    this.ReportFacade.SetReport(instanceId, this._webReport);
    ReportViewerMaint instance = ReportViewerMaint.CreateInstance(instanceId, true);
    throw new PXRedirectRequiredException("~/Scripts/Screens/ReportViewer.html?instanceId=" + instanceId, (PXGraph) instance, PXBaseRedirectException.WindowMode.InlineWindow, string.Empty);
  }

  [PXButton]
  [PXUIField(Visible = false, DisplayName = "")]
  protected IEnumerable selectTemplate(PXAdapter adapter)
  {
    string commandArguments = adapter.CommandArguments;
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    this.Report.Cache.SetValueExt<PX.Data.Reports.DAC.Report.template>((object) current, (object) commandArguments);
    yield return (object) current;
  }

  [PXButton(ImageSet = "svg:main", ImageKey = "dots", MenuAutoOpen = true, IsLockedOnToolbar = true)]
  [PXUIField(Enabled = false, DisplayName = "")]
  public virtual IEnumerable reportMenu(PXAdapter adapter)
  {
    bool flag = !string.IsNullOrEmpty(this.Report.Current.Template);
    IEnumerable enumerable;
    switch (adapter.Menu)
    {
      case "TemplateSave":
        enumerable = this.processSaveTemplate(adapter, !flag, !flag);
        break;
      case "SaveTemplateAs":
        enumerable = this.processSaveTemplate(adapter, true, true);
        break;
      case "EditTemplate":
        enumerable = this.processSaveTemplate(adapter, false, true);
        break;
      case "Edit":
        enumerable = this.EditReport(adapter);
        break;
      case "TemplateRemove":
        enumerable = this.RemoveTemplate(adapter);
        break;
      default:
        enumerable = adapter.Get();
        break;
    }
    return enumerable;
  }

  protected IEnumerable EditReport(PXAdapter adapter)
  {
    this.DownloadReportInfoFile();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Edit Version")]
  public IEnumerable EditVersion(PXAdapter adapter)
  {
    this.DownloadReportInfoFile(true);
    return adapter.Get();
  }

  private void DownloadReportInfoFile(bool withVersion = false)
  {
    string version = withVersion ? this.ReportVersion.Current?.Version.ToString() : (string) null;
    if (withVersion && string.IsNullOrEmpty(version))
      return;
    PX.SM.FileInfo reportFileInfo = this.GetReportFileInfo(version);
    if (reportFileInfo != null)
      throw new PXRedirectToFileException(reportFileInfo, true);
  }

  private PX.SM.FileInfo GetReportFileInfo(string version)
  {
    string serverUrl = this.GetServerUrl();
    if (string.IsNullOrEmpty(serverUrl))
      return (PX.SM.FileInfo) null;
    string reportName = this.Report.Current.ReportName;
    string s = $"ServiceUrl|{serverUrl}\r\nReportName|{reportName}\r\nUser|{PXContext.PXIdentity.IdentityName}";
    if (!string.IsNullOrEmpty(version))
      s = $"{s}\r\nVersion|{version}";
    ReportStream stream = (this.ReportFacade.GetStreamManager(this.Report.Current.InstanceId) ?? new StreamManager()).CreateStream("ReportSchema", string.Empty, Encoding.UTF8, "application/rps");
    byte[] bytes1 = Encoding.UTF8.GetBytes(s);
    ((Stream) stream).Write(bytes1, 0, bytes1.Length);
    byte[] bytes2 = stream.GetBytes();
    return new PX.SM.FileInfo(reportName.Replace("rpx", "rps"), (string) null, bytes2);
  }

  private string GetServerUrl()
  {
    HttpContext httpContext = this.HttpContextAccessor.HttpContext;
    if (httpContext == null)
      return (string) null;
    HttpRequest request = httpContext.Request;
    UriBuilder uriBuilder1 = new UriBuilder();
    uriBuilder1.Scheme = request.Scheme;
    HostString host1 = request.Host;
    uriBuilder1.Host = ((HostString) ref host1).Host;
    uriBuilder1.Path = PathString.op_Implicit(request.PathBase);
    UriBuilder uriBuilder2 = uriBuilder1;
    HostString host2 = request.Host;
    if (((HostString) ref host2).Port.HasValue)
    {
      UriBuilder uriBuilder3 = uriBuilder2;
      HostString host3 = request.Host;
      int num = ((HostString) ref host3).Port.Value;
      uriBuilder3.Port = num;
    }
    return uriBuilder2.ToString();
  }

  private void ValidateReportParameters()
  {
    ReportParameter reportParameter = this.Parameters.SelectSingle();
    PXCache cache = this.Parameters.Cache;
    foreach (ReportParameter parameter in (List<ReportParameter>) this.ReportDescription.Parameters)
    {
      if (cache.GetValue((object) reportParameter, parameter.Name) == null)
      {
        bool? required = parameter.Required;
        bool flag = true;
        if (required.GetValueOrDefault() == flag & required.HasValue)
          throw new PXSetPropertyException((IBqlTable) reportParameter, "'{0}' cannot be empty.", PXErrorLevel.Error, new object[1]
          {
            (object) parameter.Prompt
          });
      }
    }
  }

  private void RunARmReport()
  {
    ARmReport armReport = this.GetARmReport(this._reportID);
    ARmProcessor.CopyParameters(this.ReportDescription, armReport);
    this._webReport.Tag = (object) new Tuple<object, object>((object) armReport, ((Tuple<object, object>) this._webReport.Tag).Item2);
    string locale = this.Report.Current.Locale;
    string reportCode = this._rmReportReader.ReportCode;
    this._webReport.ProcessMethod = new Func<WebReport, ReportNode>(WebReportProcessMethod);

    ReportNode WebReportProcessMethod(WebReport webReport)
    {
      ARmReport report = ((Tuple<object, object>) webReport.Tag).Item1 as ARmReport;
      ARmReportNode armReportNode = ((Tuple<object, object>) webReport.Tag).Item2 as ARmReportNode;
      string name = Thread.CurrentThread.CurrentCulture.Name;
      if (!string.IsNullOrEmpty(locale) && !string.Equals(locale, name))
      {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
        this.LocalizationProvider.SetSlotDictionary(locale);
      }
      this._rmReportReader.ReportCode = reportCode;
      this._rmReportReader.LocalizeReport(report);
      if (armReportNode == null || !armReportNode.activeNodeChanged && webReport.InvalidateCached)
      {
        string code = armReportNode?.ActiveNode.Unit?.Code;
        armReportNode = ARmProcessor.ProcessReport((IARmDataSource) this._rmReportReader, report);
        if (!Str.IsNullOrEmpty(code) && armReportNode.ActiveNode?.Unit?.Code != code)
          armReportNode.ActiveNode = armReportNode.Items.Find(code, true);
      }
      if (!armReportNode.ActiveNode.Processed)
        armReportNode.ActiveNode.Process();
      if (webReport.InvalidateCached)
        armReportNode.activeNodeChanged = false;
      if (webReport.Tag is Tuple<object, object> tag)
        webReport.Tag = (object) new Tuple<object, object>(tag.Item1, (object) armReportNode);
      ARmProcessor.Render(armReportNode.ActiveNode, webReport.Report);
      ReportNode reportNode = this.ReportDataBinder.ProcessReportDataBinding(webReport.Report);
      if (!string.IsNullOrEmpty(locale) && !string.Equals(Thread.CurrentThread.CurrentCulture.Name, name))
      {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(name);
        this.LocalizationProvider.SetSlotDictionary(name);
      }
      reportNode.Tag = webReport.Tag;
      return reportNode;
    }
  }

  private void FillReport()
  {
    this.FillReportSettings();
    this.FillReportParameters();
    this.FillReportSortings();
    this.FillReportFilters();
  }

  private void FillReportSettings()
  {
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    this.ReportDescription.CommonSettings.ViewPdf = current.ViewPdf.GetValueOrDefault();
    this.ReportDescription.CommonSettings.AlternativeEngine = current.AlternativeEngine.GetValueOrDefault();
    this.ReportDescription.CommonSettings.ArchivedRecords = current.ArchivedRecords;
    this.ReportDescription.CommonSettings.DeletedRecords = current.DeletedRecords;
    this.ReportDescription.CommonSettings.PdfCompressed = current.PdfCompressed.GetValueOrDefault();
    this.ReportDescription.CommonSettings.PdfFontEmbedded = current.PdfFontEmbedded.GetValueOrDefault();
    this.ReportDescription.CommonSettings.PrintAllPages = current.PrintAllPages.GetValueOrDefault();
    this.ReportDescription.ViewMailSettings.Bcc = current.Bcc;
    this.ReportDescription.ViewMailSettings.Cc = current.Cc;
    this.ReportDescription.ViewMailSettings.EMail = current.Email;
    this.ReportDescription.ViewMailSettings.Format = current.Format;
    this.ReportDescription.ViewMailSettings.Subject = current.Subject;
    this.ReportDescription.Locale = current.Locale;
    this.ReportDescription.Localization = current.Localization;
  }

  private void FillReportParameters()
  {
    ReportParameter current = this.Parameters.Current;
    if (current == null || !current.Values.Any<KeyValuePair<string, object>>())
      return;
    PXCache cache = this.Parameters.Cache;
    foreach (ReportParameter parameter in (List<ReportParameter>) this.ReportDescription.Parameters)
      parameter.Value = cache.GetValue((object) current, parameter.Name);
  }

  private void FillReportSortings()
  {
    SortExp[] array = ((IEnumerable<SortExp>) this.Sorting.Select<SortExp>()).Select<SortExp, SortExp>((Func<SortExp, SortExp>) (sort => new SortExp(sort.FieldName, (SortOrder) sort.SortOrder.Value))).ToArray<SortExp>();
    ((List<SortExp>) this.ReportDescription.DynamicSorting).Clear();
    ((List<SortExp>) this.ReportDescription.DynamicSorting).AddRange((IEnumerable<SortExp>) array);
  }

  private void FillReportFilters()
  {
    FilterExp[] array = ((IEnumerable<FilterExp>) this.Conditions.Select<FilterExp>()).Select<FilterExp, FilterExp>((Func<FilterExp, FilterExp>) (filter =>
    {
      FilterExp filterExp = new FilterExp(filter.FieldName, !filter.Condition.HasValue ? (FilterCondition) 0 : (FilterCondition) filter.Condition.Value);
      filterExp.CloseBraces = filter.CloseBraces.Value;
      int? nullable = filter.OpenBraces;
      filterExp.OpenBraces = nullable.Value;
      nullable = filter.Operation;
      filterExp.Operator = (FilterOperator) nullable.Value;
      filterExp.Value = filter.Value ?? string.Empty;
      filterExp.Value2 = filter.Value2 ?? string.Empty;
      return filterExp;
    })).ToArray<FilterExp>();
    ((List<FilterExp>) this.ReportDescription.DynamicFilters).Clear();
    ((List<FilterExp>) this.ReportDescription.DynamicFilters).AddRange((IEnumerable<FilterExp>) array);
    ((List<FilterExp>) this.ReportDescription.Filters).Clear();
    foreach (ICloneable originalFilter in (List<FilterExp>) this.ReportDescription.OriginalFilters)
      ((List<FilterExp>) this.ReportDescription.Filters).Add((FilterExp) originalFilter.Clone());
  }

  public IDataNavigator GetNavigator()
  {
    if (this._navigator != null)
      return this._navigator;
    string key = "/Scripts/Screens/ReportScreen.html";
    object obj = PXContext.Session.PageInfo[key];
    if (!(obj is IPXResultset sessionReportData))
    {
      PXReportsRedirectList source = obj as PXReportsRedirectList;
      this._navigator = source != null ? (IDataNavigator) new SoapNavigator((PXGraph) this, source.Select<KeyValuePair<string, object>, KeyValuePair<string, IPXResultset>>((Func<KeyValuePair<string, object>, KeyValuePair<string, IPXResultset>>) (c => new KeyValuePair<string, IPXResultset>(c.Key, source.Any<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (c => c.Value is PXReportRedirectParameters)) ? (c.Value is PXReportRedirectParameters redirectParameters ? redirectParameters.ResultSet : (IPXResultset) null) : c.Value as IPXResultset))).ToList<KeyValuePair<string, IPXResultset>>()) : (IDataNavigator) new SoapNavigator((PXGraph) this);
    }
    else
      this._navigator = (IDataNavigator) new SoapNavigator((PXGraph) this, PXReportRedirectParameters.UnwrapSet((object) sessionReportData));
    return this._navigator;
  }

  internal IEnumerable<ReportFieldInfo> GetFieldsSchema()
  {
    List<ReportFieldInfo> fieldsSchema = new List<ReportFieldInfo>();
    ICollection<object> viewerFields = this.ReportDescription.GetViewerFields();
    HashSet<string> stringSet;
    if (viewerFields.Count > 0)
    {
      stringSet = new HashSet<string>();
      foreach (object obj in (IEnumerable<object>) viewerFields)
      {
        if (obj is string str)
          stringSet.Add(str);
        else if (obj is ViewerField viewerField)
          stringSet.Add(viewerField.Name);
      }
      if (this.ReportDescription.ViewerFieldsMode == null)
      {
        foreach (string usedField in this.ReportDescription.GetUsedFields())
        {
          if (!stringSet.Contains(usedField))
            stringSet.Add(usedField);
        }
      }
    }
    else
      stringSet = new HashSet<string>((IEnumerable<string>) this.ReportDescription.GetUsedFields());
    Dictionary<string, ReportField> schemaFields = this.ReportDescription.GetSchemaFields();
    SoapNavigator navigator = this.GetNavigator() as SoapNavigator;
    ReportLauncherHelper.LoadParameters(this.ReportDescription, (object) null, navigator);
    Dictionary<string, string> dictionary = SoapNavigator.FilterDBFields(viewerFields, this.GetAliasMap(), new ReportSelectArguments()
    {
      Tables = this.ReportDescription.Tables,
      Relations = this.ReportDescription.Relations
    });
    foreach (KeyValuePair<string, ReportField> keyValuePair in schemaFields)
    {
      if (stringSet.Contains(keyValuePair.Key))
      {
        PXFieldState fieldSchema = navigator.GetFieldSchema((object) keyValuePair.Key) as PXFieldState;
        string str;
        dictionary.TryGetValue(keyValuePair.Key, out str);
        if (fieldSchema == null)
        {
          fieldsSchema.Add(new ReportFieldInfo()
          {
            Name = keyValuePair.Key,
            Caption = str,
            Type = keyValuePair.Value.DataType
          });
        }
        else
        {
          ReportFieldInfo reportFieldInfo = new ReportFieldInfo()
          {
            Name = keyValuePair.Key,
            Caption = str ?? fieldSchema.DisplayName,
            Type = System.Type.GetTypeCode(fieldSchema.DataType),
            ViewName = fieldSchema.ViewName,
            Precision = fieldSchema.Precision,
            State = fieldSchema
          };
          PXIntState pxIntState = fieldSchema as PXIntState;
          PXStringState pxStringState = fieldSchema as PXStringState;
          List<(string, string)> valueTupleList = new List<(string, string)>();
          if (pxIntState != null && pxIntState.AllowedValues != null)
          {
            for (int index = 0; index < pxIntState.AllowedValues.Length; ++index)
              valueTupleList.Add((pxIntState.AllowedValues[index].ToString((IFormatProvider) CultureInfo.InvariantCulture), pxIntState.AllowedLabels[index]));
          }
          else if (pxStringState != null && pxStringState.AllowedValues != null)
          {
            for (int index = 0; index < pxStringState.AllowedValues.Length; ++index)
            {
              string allowedValue = pxStringState.AllowedValues[index];
              if (allowedValue != null)
                allowedValue = allowedValue.ToString((IFormatProvider) CultureInfo.InvariantCulture);
              valueTupleList.Add((allowedValue, pxStringState.AllowedLabels[index]));
            }
          }
          if (valueTupleList.Count > 0)
            reportFieldInfo.Values = (IEnumerable<(string, string)>) valueTupleList.ToArray();
          fieldsSchema.Add(reportFieldInfo);
        }
      }
    }
    return (IEnumerable<ReportFieldInfo>) fieldsSchema;
  }

  private void SetLocalizationSettings()
  {
    PXCache cache = this.Report.Cache;
    PX.Data.Reports.DAC.Report current = (PX.Data.Reports.DAC.Report) cache.Current;
    IList<SettingsProvider.LocaleModel> locales = this.SettingsProvider.Locales;
    if (locales == null || locales.Count < 2)
    {
      PXUIFieldAttribute.SetVisible<PX.Data.Reports.DAC.Report.locale>(cache, (object) current, false);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<PX.Data.Reports.DAC.Report.locale>(cache, (object) current);
      PXStringListAttribute.SetList<PX.Data.Reports.DAC.Report.locale>(cache, (object) current, locales.Select<SettingsProvider.LocaleModel, string>((Func<SettingsProvider.LocaleModel, string>) (l => l.Language)).ToArray<string>(), locales.Select<SettingsProvider.LocaleModel, string>((Func<SettingsProvider.LocaleModel, string>) (l => l.Description)).ToArray<string>());
    }
    List<SettingsProvider.LocalizationModel> localizations = this.GetLocalizations(current);
    if (localizations.Count < 2)
    {
      PXUIFieldAttribute.SetVisible<PX.Data.Reports.DAC.Report.localization>(cache, (object) current, false);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<PX.Data.Reports.DAC.Report.localization>(cache, (object) current);
      PXStringListAttribute.SetList<PX.Data.Reports.DAC.Report.localization>(cache, (object) current, localizations.Select<SettingsProvider.LocalizationModel, string>((Func<SettingsProvider.LocalizationModel, string>) (l => l.LocalizationCode)).ToArray<string>(), localizations.Select<SettingsProvider.LocalizationModel, string>((Func<SettingsProvider.LocalizationModel, string>) (l => l.Description)).ToArray<string>());
      if (!string.IsNullOrEmpty(current.Localization))
        return;
      this.SetDefaultLocalization(localizations);
    }
  }

  private void SetDefaultLocalization(
    List<SettingsProvider.LocalizationModel> localizationSource)
  {
    PXAccess.MasterCollection.Organization organization = PXAccess.GetOrganizationByID(PXAccess.GetParentOrganizationID(PXAccess.GetBranchID()));
    string str = "00";
    if (organization != null && localizationSource.Any<SettingsProvider.LocalizationModel>((Func<SettingsProvider.LocalizationModel, bool>) (model => model.LocalizationCode == organization.OrganizationLocalizationCode)))
      str = organization.OrganizationLocalizationCode;
    this.Report.Cache.SetValue<PX.Data.Reports.DAC.Report.localization>(this.Report.Cache.Current, (object) str);
  }

  private List<SettingsProvider.LocalizationModel> GetLocalizations(PX.Data.Reports.DAC.Report report)
  {
    List<SettingsProvider.LocalizationModel> localizations = new List<SettingsProvider.LocalizationModel>()
    {
      new SettingsProvider.LocalizationModel("00", "None")
    };
    IEnumerable<string> strings = this.LocalizationFeaturesService.GetEnabledLocalizations() ?? Enumerable.Empty<string>();
    string reportName = report.ReportName;
    if (!reportName.EndsWith(".rpx", StringComparison.OrdinalIgnoreCase))
      reportName += ".rpx";
    foreach (string name in strings)
    {
      RegionInfo regionInfo;
      try
      {
        regionInfo = new RegionInfo(name);
      }
      catch (Exception ex)
      {
        object[] objArray = new object[1]{ (object) name };
        throw new PXException(ex, "Incorrect country code ({0}) for the Localization attribute. Please check the correctness of the Features.xml file.", objArray);
      }
      if (!string.IsNullOrEmpty(reportName))
      {
        string str = reportName.Insert(reportName.IndexOf(".rpx", StringComparison.OrdinalIgnoreCase), "." + name);
        if (!string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(str)) || !string.IsNullOrEmpty(ReportDbStorage.ReportGetXmlFromDb(str)))
          localizations.Add(new SettingsProvider.LocalizationModel(name, regionInfo.DisplayName));
      }
    }
    return localizations;
  }

  private void SetVisibleAndEnableProperties()
  {
    PXUIFieldAttribute.SetVisible<SortExp.fieldName>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<SortExp.sortOrder>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.openBraces>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.fieldName>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.condition>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.value>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.value2>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.closeBraces>(this.Sorting.Cache, (object) null, !this._isARmReport);
    PXUIFieldAttribute.SetVisible<FilterExp.operation>(this.Sorting.Cache, (object) null, !this._isARmReport);
    bool isVisible = !this._isARmReport & PXContext.PXIdentity.AuthUser.IsInRole(this.ReportOptions.Value.DesignerRole);
    PXUIFieldAttribute.SetVisible<UserReport.version>(this.ReportVersion.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<UserReport.description>(this.ReportVersion.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<UserReport.isActive>(this.ReportVersion.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<UserReport.dateCreated>(this.ReportVersion.Cache, (object) null, isVisible);
    this.SetTemplateButtonsState(!string.IsNullOrEmpty(this.Report.Current.Template));
  }

  private void SetTemplateButtonsState(bool canDelete)
  {
    this.ReportMenu.SetEnabled("TemplateRemove", canDelete);
    this.ReportMenu.SetEnabled("EditTemplate", canDelete);
  }

  private IEnumerable processSaveTemplate(PXAdapter adapter, bool createNew, bool showDialog)
  {
    PX.Data.Reports.DAC.Report report = this.Report.Current;
    PXCache templateCache = this.EditTemplateDialog.Cache;
    string currentTemplateName = report.Template;
    PXView.InitializePanel initializeHandler = (PXView.InitializePanel) ((_1, _2) =>
    {
      TemplateProperties templateProperties = new TemplateProperties()
      {
        Template = currentTemplateName,
        IsDefault = report.IsDefault,
        Shared = report.Shared
      };
      templateCache.Update((object) templateProperties);
      templateCache.SetStatus((object) templateProperties, PXEntryStatus.Held);
      templateCache.Unload();
    });
    if (showDialog)
    {
      if (this.EditTemplateDialog.AskExt(initializeHandler) != WebDialogResult.OK)
        return adapter.Get();
    }
    else
      initializeHandler((PXGraph) null, (string) null);
    TemplateProperties current = this.EditTemplateDialog.Current;
    string template = current.Template;
    if (string.IsNullOrEmpty(template))
      return adapter.Get();
    this.FillReport();
    PXReportSettings pxReportSettings = new PXReportSettings(template, this.ReportDescription.CommonSettings, ScreenUtils.ConvertMailSettings(this.ReportDescription.MailSettings), this.ReportDescription.Parameters, this.ReportDescription.DynamicSorting, this.ReportDescription.DynamicFilters, (FilterExpCollection) null)
    {
      IsShared = current.Shared.GetValueOrDefault(),
      IsDefault = current.IsDefault.GetValueOrDefault()
    };
    string reportScreenId = this.GetReportScreenId();
    bool flag = !template.Equals(currentTemplateName, StringComparison.InvariantCulture);
    if (flag)
    {
      if (this.ExistingReportSettings.SelectSingle((object) reportScreenId, (object) this.CurrentUserInformationProvider.GetUserName(), (object) template) != null)
        throw new PXException("A report template with the name {0} already exists. Please change the name of the template.", new object[1]
        {
          (object) template
        });
    }
    if (((createNew ? 0 : (!string.IsNullOrEmpty(currentTemplateName) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      this.SettingsProvider.Delete(currentTemplateName, reportScreenId);
    this.SettingsProvider.Save(pxReportSettings, reportScreenId);
    this.SetTemplateButtonsState(true);
    report.Template = template;
    return adapter.Get();
  }

  private string GetReportScreenId()
  {
    return this.GetReportSiteMap()?.ScreenID ?? PXContext.GetScreenID().Replace(".", "");
  }

  private PX.SM.SiteMap GetReportSiteMap()
  {
    string str1 = "~/Scripts/Screens/ReportScreen.html?id=" + this._reportID;
    string str2 = this._isARmReport ? $"~/Frames/RmLauncher.aspx?id={this._reportID}.rpx" : $"~/Frames/ReportLauncher.aspx?id={this._reportID}.rpx";
    PX.SM.SiteMap reportSiteMap = this.ReportSiteMap.SelectSingle((object) str1, (object) (str1 + "&%"));
    if (reportSiteMap != null)
      return reportSiteMap;
    return this.ClassicUiReportSiteMap.SelectSingle((object) str2);
  }

  protected IEnumerable RemoveTemplate(PXAdapter adapter)
  {
    this.SettingsProvider.Delete(this.Report.Current.Template, this.GetReportScreenId());
    this.Report.Cache.SetValueExt<PX.Data.Reports.DAC.Report.template>((object) this.Report.Current, (object) null);
    this.ExistingReportSettings.Cache.Clear();
    this.ExistingReportSettings.Cache.Unload();
    this.CreateReport();
    return adapter.Get();
  }

  protected void _(Events.RowSelected<PX.Data.Reports.DAC.Report> e)
  {
    this.SetLocalizationSettings();
  }

  protected virtual void _(Events.FieldUpdated<PX.Data.Reports.DAC.Report.template> e)
  {
    string newValue = (string) e.NewValue;
    this.CreateReportDescription();
    PXCache cache = this.Report.Cache;
    object row = e.Row;
    if (string.IsNullOrEmpty(newValue))
    {
      this.SetTemplateButtonsState(false);
      cache.SetValue<PX.Data.Reports.DAC.Report.shared>(row, (object) false);
      cache.SetValue<PX.Data.Reports.DAC.Report.isDefault>(row, (object) false);
      this.ExistingReportSettings.Cache.Clear();
      this.ExistingReportSettings.Cache.Unload();
    }
    else
    {
      this.SetTemplateButtonsState(true);
      string userName = this.CurrentUserInformationProvider.GetUserName();
      string reportScreenId = this.GetReportScreenId();
      PXReportSettings settings = this.SettingsProvider.Read(newValue, userName, reportScreenId);
      this.LoadReportFromTemplate(settings);
      this.WriteReportToViews(true);
      this._currentSettings = this.ExistingReportSettings.SelectSingle((object) reportScreenId, (object) userName, (object) newValue);
      this.ExistingReportSettings.Current = this._currentSettings;
      this.ExistingReportSettings.Cache.Unload();
      cache.SetValue<PX.Data.Reports.DAC.Report.shared>(row, (object) settings.IsShared);
      cache.SetValue<PX.Data.Reports.DAC.Report.isDefault>(row, (object) settings.IsDefault);
      cache.Unload();
    }
  }

  private void CreateReportDescription(bool writeReportToViews = false)
  {
    ARmReport armReport = (ARmReport) null;
    if (this._isARmReport)
    {
      armReport = this.GetARmReport(this._reportID);
      this.ReportDescription = ARmProcessor.CreateReport(armReport);
    }
    else
    {
      this.ReportVersion.Cache.LoadFromSession(true);
      this.ReportDescription = new PX.Reports.Controls.Report();
      this.ReportDescription.LoadByName(this._reportID + ".rpx", (int?) this.ReportVersion.Current?.Version, this.LocalizationProvider);
    }
    this.ReportDescription.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
    if (writeReportToViews)
    {
      string userName = this.CurrentUserInformationProvider.GetUserName();
      string reportScreenId = this.GetReportScreenId();
      this.DefaultReportSettings.Cache.LoadFromSession(true);
      this._currentSettings = this.DefaultReportSettings.Current;
      this._defaultSettings = this.DefaultReportSettings.SelectSingle((object) reportScreenId, (object) userName);
      PX.Data.Reports.DAC.ReportSettings reportSettings = this._currentSettings ?? this._defaultSettings;
      if (reportSettings != null)
      {
        this.LoadReportFromTemplate(this.SettingsProvider.Read(reportSettings.SettingsID));
        this.WriteReportToViews(true);
        PX.Data.Reports.DAC.Report current = this.Report.Current;
        current.Template = reportSettings.Name;
        current.Shared = reportSettings.IsShared;
        current.IsDefault = reportSettings.IsDefault;
        this.Report.Cache.Unload();
        this.DefaultReportSettings.Cache.Unload();
      }
      else
        this.WriteReportToViews(true);
    }
    string instanceId = this.Report.Current.InstanceId;
    if (instanceId == null)
      return;
    this._webReport = new WebReport(this.ReportDescription, instanceId);
    if (!this._isARmReport)
      return;
    this._webReport.Tag = (object) new Tuple<object, object>((object) armReport, (object) null);
  }

  private void LoadReportFromTemplate(PXReportSettings settings)
  {
    settings.ParameterValues.UpdateParameters(this.ReportDescription.Parameters);
    ((Settings) settings.CommonSettings).UpdateParameters((Settings) this.ReportDescription.CommonSettings);
    ((Settings) settings.Mail).UpdateParameters((Settings) this.ReportDescription.ViewMailSettings);
    this.ReportDescription.ResetSorting();
    ((List<SortExp>) this.ReportDescription.Sorting).AddRange((IEnumerable<SortExp>) settings.Sorting);
    this.ReportDescription.ResetFilters();
    ((List<FilterExp>) this.ReportDescription.Filters).AddRange((IEnumerable<FilterExp>) settings.Filters);
    this.ReportFacade.SetReport(this.Report.Current?.InstanceId ?? ReportMaint.GenerateInstanceId(), this._webReport);
  }

  private static string GenerateInstanceId() => Guid.NewGuid().ToString("N");

  private void CreateReport(string reportID, bool isARmReport)
  {
    this._reportID = reportID.EndsWith(".rpx", StringComparison.Ordinal) ? reportID.Substring(0, reportID.IndexOf(".rpx", StringComparison.Ordinal)) : reportID;
    this._isARmReport = isARmReport;
    this.CreateReport();
  }

  private void CreateReport()
  {
    this.ReportDescription = (PX.Reports.Controls.Report) null;
    if (this.TryLoadReportFromSession())
    {
      this.FillFieldsDisplayName();
    }
    else
    {
      this.CreateReportDescription(true);
      this.FillFieldsDisplayName();
      this.EditTemplateDialog.Cache.LoadFromSession(true);
      this.ReportFacade.SetReport(this.Report.Current.InstanceId, this._webReport);
    }
  }

  private bool TryLoadReportFromSession()
  {
    this.Report.Cache.LoadFromSession(true);
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    if (string.IsNullOrEmpty(current?.InstanceId) || !this._reportID.OrdinalIgnoreCaseEquals(current.ReportName) && !(this._reportID + ".rpx").OrdinalIgnoreCaseEquals(current.ReportName) || !current.ScreenId.OrdinalIgnoreCaseEquals(this.GetReportScreenId()))
      return false;
    this._webReport = this.ReportFacade.GetReport(current.InstanceId);
    if (this._webReport == null)
      return false;
    this.ReportDescription = this._webReport.Report;
    string userName = this.CurrentUserInformationProvider.GetUserName();
    string reportScreenId = this.GetReportScreenId();
    this.DefaultReportSettings.Cache.LoadFromSession(true);
    this._currentSettings = this.DefaultReportSettings.Current;
    this._defaultSettings = this.DefaultReportSettings.SelectSingle((object) reportScreenId, (object) userName);
    this.Conditions.Cache.LoadFromSession(true);
    this.Sorting.Cache.LoadFromSession(true);
    this.Parameters.Cache.LoadFromSession(true);
    this.ReportVersion.Cache.LoadFromSession(true);
    this.EditTemplateDialog.Cache.LoadFromSession(true);
    return true;
  }

  private void FillFieldsDisplayName()
  {
    ReportSelectArguments selectArguments = new ReportSelectArguments();
    SoapNavigator navigator = (SoapNavigator) this.GetNavigator();
    ReportLauncherHelper.LoadParameters(this.ReportDescription, (object) null, navigator);
    selectArguments.Tables = this.ReportDescription.Tables;
    selectArguments.Relations = this.ReportDescription.Relations;
    ICollection<object> viewerFields = this.ReportDescription.GetViewerFields();
    Dictionary<string, string> dictionary = SoapNavigator.FilterDBFields(viewerFields, this.GetAliasMap(), selectArguments);
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    foreach (object obj in (IEnumerable<object>) viewerFields)
    {
      string str;
      if (obj is ViewerField viewerField && dictionary.TryGetValue(viewerField.Name, out str))
      {
        viewerField.Process((IDataNavigator) navigator, this.ReportDescription);
        tupleList.Add(new Tuple<string, string>(viewerField.Name, Str.NullIfWhitespace(viewerField.ProcessedDescription) ?? str));
      }
      else
      {
        string key = obj.ToString();
        if (dictionary.TryGetValue(key, out str))
          tupleList.Add(new Tuple<string, string>(key, str));
      }
    }
    PXStringListAttribute.SetList<SortExp.fieldName>(this.Sorting.Cache, (object) null, tupleList.ToArray());
    PXStringListAttribute.SetList<FilterExp.fieldName>(this.Conditions.Cache, (object) null, tupleList.ToArray());
  }

  private Dictionary<string, string> GetAliasMap()
  {
    Dictionary<string, string> aliasMap = new Dictionary<string, string>();
    foreach (ReportRelation relation in (CollectionBase) this.ReportDescription.Relations)
    {
      if (!string.IsNullOrEmpty(relation.ParentName) && !string.IsNullOrEmpty(relation.ParentAlias))
        aliasMap[relation.ParentAlias] = relation.ParentName;
      if (!string.IsNullOrEmpty(relation.ChildName) && !string.IsNullOrEmpty(relation.ChildAlias))
        aliasMap[relation.ChildAlias] = relation.ChildName;
    }
    return aliasMap;
  }

  private ARmReport GetARmReport(string reportCode)
  {
    return this._webReport == null || !(((Tuple<object, object>) this._webReport.Tag).Item1 is ARmReport armReport) || this._rmReportReader == null ? this.CreateARmReport(reportCode) : armReport;
  }

  private ARmReport CreateARmReport(string reportCode)
  {
    this._rmReportReader = this.GetRmReportReader();
    this._rmReportReader.ReportCode = reportCode;
    return this._rmReportReader.GetReport();
  }

  private void WriteReportToViews(bool forceWrite = false)
  {
    this.WriteReportSettings(forceWrite);
    this.WriteReportSorting(forceWrite);
    this.WriteReportParameters(forceWrite);
    this.WriteReportConditions(forceWrite);
  }

  private void WriteReportSettings(bool forceWrite)
  {
    PXCache cache = this.Report.Cache;
    PX.Data.Reports.DAC.Report data;
    if (!forceWrite)
    {
      cache.LoadFromSession(true);
      if (!string.IsNullOrEmpty(this.Report.Current?.InstanceId))
        return;
      cache.Clear();
      data = this.Report.Insert();
    }
    else
      data = this.Report.Current ?? this.Report.Insert();
    ReportCommonSettings commonSettings = this.ReportDescription.CommonSettings;
    ReportMailSettings viewMailSettings = this.ReportDescription.ViewMailSettings;
    cache.SetValue<PX.Data.Reports.DAC.Report.reportName>((object) data, (object) this.ReportDescription.ReportName);
    cache.SetValue<PX.Data.Reports.DAC.Report.screenId>((object) data, (object) this.GetReportScreenId());
    cache.SetValue<PX.Data.Reports.DAC.Report.instanceId>((object) data, (object) (this._webReport?.InstanceID ?? ReportMaint.GenerateInstanceId()));
    cache.SetValue<PX.Data.Reports.DAC.Report.locale>((object) data, (object) this.ReportDescription.Locale);
    cache.SetValue<PX.Data.Reports.DAC.Report.localization>((object) data, (object) this.ReportDescription.Localization);
    cache.SetValue<PX.Data.Reports.DAC.Report.viewPdf>((object) data, (object) commonSettings.ViewPdf);
    cache.SetValue<PX.Data.Reports.DAC.Report.alternativeEngine>((object) data, (object) commonSettings.AlternativeEngine);
    cache.SetValue<PX.Data.Reports.DAC.Report.archivedRecords>((object) data, (object) commonSettings.ArchivedRecords);
    cache.SetValue<PX.Data.Reports.DAC.Report.deletedRecords>((object) data, (object) commonSettings.DeletedRecords);
    cache.SetValue<PX.Data.Reports.DAC.Report.pdfCompressed>((object) data, (object) commonSettings.PdfCompressed);
    cache.SetValue<PX.Data.Reports.DAC.Report.pdfFontEmbedded>((object) data, (object) commonSettings.PdfFontEmbedded);
    cache.SetValue<PX.Data.Reports.DAC.Report.printAllPages>((object) data, (object) commonSettings.PrintAllPages);
    cache.SetValue<PX.Data.Reports.DAC.Report.bcc>((object) data, (object) viewMailSettings.Bcc);
    cache.SetValue<PX.Data.Reports.DAC.Report.cc>((object) data, (object) viewMailSettings.Cc);
    cache.SetValue<PX.Data.Reports.DAC.Report.email>((object) data, (object) viewMailSettings.EMail);
    cache.SetValue<PX.Data.Reports.DAC.Report.format>((object) data, (object) viewMailSettings.Format);
    cache.SetValue<PX.Data.Reports.DAC.Report.subject>((object) data, (object) viewMailSettings.Subject);
    cache.Unload();
  }

  private void WriteReportSorting(bool forceWrite = false)
  {
    PXCache cache = this.Sorting.Cache;
    if (!forceWrite)
    {
      cache.LoadFromSession(true);
      if (this.Sorting.Current != null)
        return;
    }
    cache.Clear();
    foreach (SortExp sortExp in (List<SortExp>) this.ReportDescription.Sorting)
    {
      SortExp data = this.Sorting.Insert();
      cache.SetValue<SortExp.fieldName>((object) data, (object) sortExp.DataField);
      cache.SetValue<SortExp.sortOrder>((object) data, (object) (int) sortExp.SortOrder);
    }
    cache.Unload();
  }

  private void WriteReportParameters(bool forceWrite = false)
  {
    ReportParameterCache cache = (ReportParameterCache) this.Parameters.Cache;
    if (!forceWrite)
    {
      cache.LoadFromSession(true);
      if (this.Parameters.Current != null)
        return;
    }
    cache.Clear();
    ReportParameter data = this.Parameters.Insert();
    IDataNavigator navigator = this.GetNavigator();
    foreach (ReportParameter parameter in (List<ReportParameter>) this.ReportDescription.Parameters)
    {
      parameter.Process(navigator, this.ReportDescription);
      cache.SetValue((object) data, parameter.Name, parameter.Value);
    }
    cache.Unload();
  }

  private void WriteReportConditions(bool forceWrite = false)
  {
    PXCache cache = this.Conditions.Cache;
    if (!forceWrite)
    {
      cache.LoadFromSession(true);
      if (this.Conditions.Current != null)
        return;
    }
    cache.Clear();
    foreach (FilterExp dynamicFilter in (List<FilterExp>) this.ReportDescription.DynamicFilters)
    {
      FilterExp filterExp = this.Conditions.Insert();
      object newValue1 = (object) dynamicFilter.Value;
      object newValue2 = (object) dynamicFilter.Value2;
      cache.RaiseFieldUpdating<FilterExp.value>((object) filterExp, ref newValue1);
      cache.RaiseFieldUpdating<FilterExp.value2>((object) filterExp, ref newValue2);
      cache.SetValue<FilterExp.fieldName>((object) filterExp, (object) dynamicFilter.DataField);
      cache.SetValue<FilterExp.openBraces>((object) filterExp, (object) dynamicFilter.OpenBraces);
      cache.SetValue<FilterExp.closeBraces>((object) filterExp, (object) dynamicFilter.CloseBraces);
      cache.SetValue<FilterExp.operation>((object) filterExp, (object) (int) dynamicFilter.Operator);
    }
    cache.Unload();
  }

  private RMReportReader GetRmReportReader()
  {
    string dataGraphContextKey = this.GetDataGraphContextKey(typeof (RMReportReader));
    lock (ReportMaint._lockObj)
    {
      RMReportReader slot = PXContext.GetSlot<RMReportReader>(dataGraphContextKey);
      if (slot != null)
        return slot;
      RMReportReader instance = PXGraph.CreateInstance<RMReportReader>();
      PXContext.SetSlot<RMReportReader>(dataGraphContextKey, instance);
      return instance;
    }
  }

  private string GetDataGraphContextKey(System.Type type) => "0_DATAGRAPH_" + type.FullName;

  protected void _(Events.RowSelected<UserReport> e)
  {
    UserReport row = e.Row;
    PXUIFieldAttribute.SetEnabled<UserReport.isActive>(e.Cache, (object) row, false);
  }

  protected void _(Events.RowDeleting<UserReport> e)
  {
    UserReport row = e.Row;
    if (row.IsActive.Value && string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(row.ReportFileName)))
      throw new PXException("The active version cannot be deleted because there's no report file found on disk.");
  }

  protected void _(Events.FieldVerifying<UserReport.isActive> e)
  {
    if (this._manualDeactivation)
      return;
    UserReport row = e.Row as UserReport;
    if (row.IsActive.Value && !(bool) e.NewValue && string.IsNullOrEmpty(ReportFileManager.GetReportPathByName(row.ReportFileName)))
      throw new PXException("The active version cannot be disabled because there's no report file found on disk.");
  }

  protected void _(Events.FieldSelecting<FilterExp.value> e)
  {
    FilterExp row = (FilterExp) e.Row ?? this.Conditions.Current;
    PXFieldState filterValueState = this.GetFilterValueState<FilterExp.value>(row);
    if (filterValueState == null)
      return;
    object obj = this.RunFieldUpdatingOnFilterValue(row.FieldName, (object) row.Value);
    filterValueState.Value = obj;
    e.ReturnState = (object) filterValueState;
    e.ReturnValue = obj;
  }

  protected void _(Events.FieldSelecting<FilterExp.value2> e)
  {
    FilterExp row = (FilterExp) e.Row ?? this.Conditions.Current;
    PXFieldState filterValueState = this.GetFilterValueState<FilterExp.value2>(row);
    if (filterValueState == null)
      return;
    object obj = this.RunFieldUpdatingOnFilterValue(row.FieldName, (object) row.Value2);
    filterValueState.Value = obj;
    e.ReturnState = (object) filterValueState;
    e.ReturnValue = obj;
  }

  public override object GetStateExt(string viewName, object data, string fieldName)
  {
    if (!viewName.OrdinalIgnoreCaseEquals("Conditions") || !fieldName.Equals("Value") && !fieldName.Equals("Value2"))
      return base.GetStateExt(viewName, data, fieldName);
    object returnValue = this.Conditions.Cache.GetValue(data, fieldName);
    this.Conditions.Cache.RaiseFieldSelecting(fieldName, data, ref returnValue, true);
    return returnValue;
  }

  protected void _(Events.FieldUpdating<FilterExp.value> e)
  {
    FilterExp row = (FilterExp) e.Row;
    e.NewValue = this.RunFieldUpdatingOnFilterValue(row.FieldName, e.NewValue);
    FilterExp filterExp = row;
    object newValue = e.NewValue;
    string str = newValue != null ? Str.NullIfWhitespace(newValue.ToString()) : (string) null;
    filterExp.Value = str;
    e.Cancel = true;
  }

  protected void _(Events.FieldUpdating<FilterExp.value2> e)
  {
    FilterExp row = (FilterExp) e.Row;
    e.NewValue = this.RunFieldUpdatingOnFilterValue(row.FieldName, e.NewValue);
    FilterExp filterExp = row;
    object newValue = e.NewValue;
    string str = newValue != null ? Str.NullIfWhitespace(newValue.ToString()) : (string) null;
    filterExp.Value2 = str;
    e.Cancel = true;
  }

  protected object RunFieldUpdatingOnFilterValue(string dataField, object newValue)
  {
    SoapNavigator navigator = this.GetNavigator() as SoapNavigator;
    if (string.IsNullOrEmpty(dataField) || navigator == null)
      return (object) null;
    string str1;
    string str2;
    ArrayDeconstruct.Deconstruct<string>(dataField.Split(new char[1]
    {
      '.'
    }, 2), ref str1, ref str2);
    string tname = str1;
    string name = str2;
    ReportTableCollection tables = navigator.SelectArguments.Tables;
    PXCache cach = this.Caches[ServiceManager.ReportNameResolver.ResolveTable(tname, tables)];
    cach.RaiseFieldUpdating(dataField, (object) null, ref newValue);
    PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, newValue);
    foreach (PXEventSubscriberAttribute attribute in cach.GetAttributes(name))
    {
      if (attribute is IPXFieldUpdatingSubscriber updatingSubscriber)
        updatingSubscriber.FieldUpdating(cach, e);
    }
    return newValue;
  }

  private PXFieldState GetFilterValueState<ValueField>(FilterExp row) where ValueField : IBqlField
  {
    if (row == null)
      return (PXFieldState) null;
    if (!((this.GetNavigator() is SoapNavigator navigator ? navigator.GetFieldSchema((object) row.FieldName) : (object) null) is PXFieldState fieldSchema))
      return (PXFieldState) null;
    fieldSchema.Value = this.Conditions.Cache.GetValue<ValueField>((object) row);
    if (fieldSchema is PXStringState pxStringState)
    {
      string[] allowedValues = pxStringState.AllowedValues;
      if (allowedValues != null && allowedValues.Length > 0)
        pxStringState.MultiSelect = true;
    }
    return fieldSchema;
  }

  protected void _(Events.RowInserting<FilterExp> e)
  {
    e.Row.LineNbr = new int?((e.Cache.Inserted.Cast<FilterExp>().Max<FilterExp>((Func<FilterExp, int?>) (filter => filter.LineNbr)) ?? -1) + 1);
  }

  protected void _(Events.RowDeleted<FilterExp> e) => e.Cache.Unload();

  protected void _(Events.RowInserting<SortExp> e)
  {
    e.Row.LineNbr = new int?((e.Cache.Inserted.Cast<SortExp>().Max<SortExp>((Func<SortExp, int?>) (filter => filter.LineNbr)) ?? -1) + 1);
  }

  protected void _(Events.RowDeleted<SortExp> e) => e.Cache.Unload();

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.format> e)
  {
    e.NewValue = (object) this.ReportDescription.ViewMailSettings.Format;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.email> e)
  {
    e.NewValue = (object) this.ReportDescription.ViewMailSettings.EMail;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.cc> e)
  {
    e.NewValue = (object) this.ReportDescription.ViewMailSettings.Cc;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.bcc> e)
  {
    e.NewValue = (object) this.ReportDescription.ViewMailSettings.Bcc;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.subject> e)
  {
    e.NewValue = (object) this.ReportDescription.ViewMailSettings.Subject;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.deletedRecords> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.DeletedRecords;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.archivedRecords> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.ArchivedRecords;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.printAllPages> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.PrintAllPages;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.viewPdf> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.ViewPdf;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.pdfCompressed> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.PdfCompressed;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.pdfFontEmbedded> e)
  {
    e.NewValue = (object) this.ReportDescription.CommonSettings.PdfFontEmbedded;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.instanceId> e)
  {
    if (!string.IsNullOrEmpty(e.NewValue as string))
      return;
    e.NewValue = (object) this._webReport?.InstanceID;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.reportName> e)
  {
    if (!string.IsNullOrEmpty(e.NewValue as string))
      return;
    Events.FieldDefaulting<PX.Data.Reports.DAC.Report.reportName> fieldDefaulting = e;
    PX.Reports.Controls.Report reportDescription = this.ReportDescription;
    string str = (reportDescription != null ? Str.NullIfWhitespace(reportDescription.ReportName) : (string) null) ?? ((ReportItem) this.ReportDescription)?.Name;
    fieldDefaulting.NewValue = (object) str;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.screenId> e)
  {
    if (!string.IsNullOrEmpty(e.NewValue as string))
      return;
    e.NewValue = (object) this.GetReportScreenId();
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.template> e)
  {
    PX.Data.Reports.DAC.ReportSettings reportSettings = this._currentSettings ?? this._defaultSettings;
    if (reportSettings == null)
      return;
    e.NewValue = (object) reportSettings.Name;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.shared> e)
  {
    if (this._defaultSettings == null)
      return;
    e.NewValue = (object) this._defaultSettings.IsShared;
  }

  protected void _(Events.FieldDefaulting<PX.Data.Reports.DAC.Report.isDefault> e)
  {
    if (this._defaultSettings == null)
      return;
    e.NewValue = (object) this._defaultSettings.IsDefault;
  }

  public IEnumerable report()
  {
    yield return (object) this.Report.Current;
  }

  public IEnumerable parameters()
  {
    if (this.Parameters.Current == null)
      this.WriteReportParameters();
    yield return (object) this.Parameters.Current;
  }

  public IEnumerable sorting()
  {
    this.Sorting.Cache.LoadFromSession(true);
    SortExp[] array = this.Sorting.Cache.Inserted.Cast<SortExp>().ToArray<SortExp>();
    if (((IEnumerable<SortExp>) array).Any<SortExp>())
      return (IEnumerable) array;
    this.WriteReportSorting();
    return this.Sorting.Cache.Inserted;
  }

  public IEnumerable conditions()
  {
    this.Conditions.Cache.LoadFromSession(true);
    FilterExp[] array = this.Conditions.Cache.Inserted.Cast<FilterExp>().ToArray<FilterExp>();
    if (((IEnumerable<FilterExp>) array).Any<FilterExp>())
      return (IEnumerable) array;
    this.WriteReportConditions();
    return this.Conditions.Cache.Inserted;
  }

  protected IEnumerable reportVersion()
  {
    string enabledLocalization = this.LocalizationFeaturesService.GetEnabledLocalization();
    string str = this._reportID + ".rpx";
    if (string.IsNullOrEmpty(enabledLocalization))
      return (IEnumerable) this.UserReports.Select((object) str);
    PXResultset<UserReport> source = this.UserReports.Select((object) $"{this._reportID}.{enabledLocalization}.rpx");
    if (source.Any<PXResult<UserReport>>())
      return (IEnumerable) source;
    return (IEnumerable) this.UserReports.Select((object) str);
  }

  protected IEnumerable reportNotifications()
  {
    return (IEnumerable) this.CurrentReportNotifications.Select((object) this.GetReportScreenId());
  }

  [PXButton]
  [PXUIField(DisplayName = "Activate")]
  public IEnumerable ActivateVersion(PXAdapter adapter)
  {
    this.ActivateReportVersion();
    throw new PXRefreshException();
  }

  [PXButton]
  [PXUIField(DisplayName = "Deactivate")]
  public IEnumerable DeactivateVersion(PXAdapter adapter)
  {
    this.DeactivateReportVersion();
    throw new PXRefreshException();
  }

  private void ActivateReportVersion()
  {
    UserReport current = this.ReportVersion.Current;
    if (current == null)
      return;
    PXCache cache = this.ReportVersion.Cache;
    cache.SetValue<UserReport.isActive>((object) current, (object) true);
    List<UserReport> userReportList = new List<UserReport>(1)
    {
      current
    };
    try
    {
      this._manualDeactivation = true;
      foreach (PXResult<UserReport> pxResult in this.UserReports.Select((object) current.ReportFileName))
      {
        UserReport userReport = (UserReport) pxResult;
        int? version1 = userReport.Version;
        int? version2 = current.Version;
        if (!(version1.GetValueOrDefault() == version2.GetValueOrDefault() & version1.HasValue == version2.HasValue))
        {
          bool? isActive = userReport.IsActive;
          bool flag = true;
          if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
          {
            UserReport copy = (UserReport) cache.CreateCopy((object) userReport);
            copy.IsActive = new bool?(false);
            this.ReportVersion.Update(copy);
            userReportList.Add(userReport);
          }
        }
      }
    }
    finally
    {
      this._manualDeactivation = false;
    }
    string reportScreenId = this.GetReportScreenId();
    if (!string.IsNullOrEmpty(reportScreenId))
      this.ScreenInfoCacheControl.InvalidateCache(reportScreenId);
    ReportDbStorage.Update(userReportList.ToArray());
    this.ReportDescription.ReloadByName(this._reportID, current.Version, this.LocalizationProvider);
    this.WriteReportToViews();
    cache.Unload();
    this.ReportVersion.View.RequestRefresh();
  }

  private void DeactivateReportVersion()
  {
    UserReport current = this.ReportVersion.Current;
    if (current == null)
      return;
    PXCache cache = this.ReportVersion.Cache;
    cache.SetValueExt<UserReport.isActive>((object) current, (object) false);
    ReportDbStorage.Update(current);
    this.ReportDescription.ReloadByName(this._reportID, new int?(), this.LocalizationProvider);
    this.WriteReportToViews();
    cache.Unload();
    this.ReportVersion.View.RequestRefresh();
  }

  [PXButton]
  [PXUIField(DisplayName = "Schedule Report")]
  public IEnumerable scheduleReport(PXAdapter adapter)
  {
    this.FillReport();
    SMNotificationMaint instance = PXGraph.CreateInstance<SMNotificationMaint>();
    ReportMailSettings viewMailSettings = this.ReportDescription.ViewMailSettings;
    Notification notification1 = new Notification()
    {
      NTo = viewMailSettings.EMail,
      NCc = viewMailSettings.Cc,
      NBcc = viewMailSettings.Bcc,
      Subject = viewMailSettings.Subject
    };
    Notification notification2 = instance.Notifications.Insert(notification1);
    instance.Notifications.Cache.GetExtension<NotificationExtension>((object) notification2).CreatedFromReport = new bool?(true);
    string str = Str.NullIfWhitespace(viewMailSettings.Format) ?? "PDF";
    NotificationReport notificationReport1 = new NotificationReport()
    {
      ScreenID = this.GetReportScreenId(),
      Format = new byte?((byte) str.Equals("HTML", StringComparison.OrdinalIgnoreCase))
    };
    NotificationReport notificationReport2 = instance.NotificationReports.Insert(notificationReport1);
    PX.Data.Reports.DAC.Report current = this.Report.Current;
    if (!string.IsNullOrEmpty(current.Template))
    {
      bool? shared = current.Shared;
      if (shared.HasValue && shared.GetValueOrDefault())
      {
        PX.Data.Reports.DAC.ReportSettings reportSettings = this.ExistingReportSettings.SelectSingle((object) this.GetReportScreenId(), (object) this.CurrentUserInformationProvider.GetUserName(), (object) current.Template);
        notificationReport2.ReportTemplateID = reportSettings.SettingsID;
        instance.NotificationReports.Update(notificationReport2);
      }
    }
    throw new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
  }

  [PXUIField(DisplayName = "View Notification")]
  [PXButton]
  public void viewNotification()
  {
    Notification current = this.ReportNotifications.Current;
    if (current != null)
    {
      SMNotificationMaint instance = PXGraph.CreateInstance<SMNotificationMaint>();
      instance.Notifications.Current = current;
      throw new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
    }
  }

  [PXUIField(DisplayName = "")]
  [PXButton(ImageKey = "Cancel")]
  public IEnumerable cancel(PXAdapter adapter)
  {
    this.CreateReportDescription(true);
    this.ReportFacade.SetReport(this.Report.Current.InstanceId, this._webReport);
    return (IEnumerable) EnumerableExtensions.AsSingleEnumerable<PX.Data.Reports.DAC.Report>(this.Report.Current);
  }

  internal string GetTimeStamp()
  {
    return !this._isARmReport ? this.GetReportTimeStamp() : this.GetARmReportTimeStamp();
  }

  public override string PrimaryView => "Report";

  private string GetARmReportTimeStamp()
  {
    RMReport rmReport = this.RmReport.SelectSingle((object) this._reportID);
    string columnSetCode = rmReport.ColumnSetCode;
    RMColumnSet rmColumnSet = this.RmColumnSet.SelectSingle((object) columnSetCode);
    RMColumn[] source1 = this.RmColumn.Select<RMColumn>((object) columnSetCode);
    RMColumnHeader[] source2 = this.RmColumnHeader.Select<RMColumnHeader>((object) columnSetCode);
    string rowSetCode = rmReport.RowSetCode;
    RMRowSet rmRowSet = this.RmRowSet.SelectSingle((object) rowSetCode);
    RMRow[] source3 = this.RmRow.Select<RMRow>((object) rowSetCode);
    string unitSetCode = rmReport.UnitSetCode;
    RMUnitSet rmUnitSet = this.RmUnitSet.SelectSingle((object) unitSetCode);
    RMUnit[] source4 = this.RmUnit.Select<RMUnit>((object) unitSetCode);
    IEnumerable<System.DateTime> first = ((IEnumerable<RMColumn>) source1).Select<RMColumn, System.DateTime>((Func<RMColumn, System.DateTime>) (col => col.LastModifiedDateTime.Value)).Union<System.DateTime>(((IEnumerable<RMColumnHeader>) source2).Select<RMColumnHeader, System.DateTime>((Func<RMColumnHeader, System.DateTime>) (ch => ch.LastModifiedDateTime.Value))).Union<System.DateTime>(((IEnumerable<RMRow>) source3).Select<RMRow, System.DateTime>((Func<RMRow, System.DateTime>) (row => row.LastModifiedDateTime.Value))).Union<System.DateTime>(((IEnumerable<RMUnit>) source4).Select<RMUnit, System.DateTime>((Func<RMUnit, System.DateTime>) (unit => unit.LastModifiedDateTime.Value)));
    System.DateTime[] items = new System.DateTime[4];
    items[0] = rmReport.LastModifiedDateTime.Value;
    items[1] = rmColumnSet.LastModifiedDateTime.Value;
    System.DateTime? modifiedDateTime = rmRowSet.LastModifiedDateTime;
    items[2] = modifiedDateTime.Value;
    System.DateTime minValue;
    if (rmUnitSet == null)
    {
      minValue = System.DateTime.MinValue;
    }
    else
    {
      modifiedDateTime = rmUnitSet.LastModifiedDateTime;
      minValue = modifiedDateTime.Value;
    }
    items[3] = minValue;
    \u003C\u003Ez__ReadOnlyArray<System.DateTime> second = new \u003C\u003Ez__ReadOnlyArray<System.DateTime>(items);
    return first.Union<System.DateTime>((IEnumerable<System.DateTime>) second).Max<System.DateTime>().ToString("yyyy-MM-dd HH:mm:ss");
  }

  private string GetReportTimeStamp()
  {
    string enabledLocalization = this.LocalizationFeaturesService.GetEnabledLocalization();
    return string.IsNullOrEmpty(enabledLocalization) ? this.GetReportTimeStamp(this._reportID) : Str.NullIfWhitespace(this.GetReportTimeStamp($"{this._reportID}.{enabledLocalization}")) ?? this.GetReportTimeStamp(this._reportID);
  }

  private string GetReportTimeStamp(string reportID)
  {
    string name = reportID + ".rpx";
    UserReport[] source = this.UserReports.Select<UserReport>((object) name);
    if (((IEnumerable<UserReport>) source).Any<UserReport>())
      return ((IEnumerable<UserReport>) source).Max<UserReport, System.DateTime>((Func<UserReport, System.DateTime>) (ur => ur.LastModifiedDateTime.Value)).ToString("yyyy-MM-dd HH:mm:ss");
    string reportPathByName = ReportFileManager.GetReportPathByName(name);
    return string.IsNullOrEmpty(reportPathByName) ? string.Empty : File.GetLastWriteTime(reportPathByName).ToString("yyyy-MM-dd HH:mm:ss");
  }

  public override int ExecuteDelete(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName != "Sorting")
      return base.ExecuteDelete(viewName, keys, values, parameters);
    string fieldName = keys[(object) "fieldName"] as string;
    this.Sorting.Cache.MarkDeleted((object) ((IEnumerable<SortExp>) this.Sorting.Select<SortExp>()).FirstOrDefault<SortExp>((Func<SortExp, bool>) (sort => string.Equals(fieldName, sort.FieldName, StringComparison.OrdinalIgnoreCase))));
    return 1;
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    int num;
    switch (viewName)
    {
      case "Parameters":
        num = this.UpdateParameter(values);
        break;
      case "Sorting":
        num = this.UpdateSorting(values);
        break;
      case "Conditions":
        num = this.UpdateCondition(values);
        break;
      case "EditTemplateDialog":
        num = this.UpdateTemplate(values);
        break;
      case "ReportVersion":
        num = this.UpdateReportVersion(keys, values);
        break;
      case "Report":
        num = this.UpdateReport(keys, values);
        break;
      default:
        num = base.ExecuteUpdate(viewName, keys, values, parameters);
        break;
    }
    return num;
  }

  private int UpdateParameter(IDictionary values)
  {
    Dictionary<string, object> dictionary = (Dictionary<string, object>) values;
    foreach (string key in dictionary.Keys)
      this.Parameters.Cache.SetValue((object) this.Parameters.Current, key, dictionary[key]);
    this.Parameters.Cache.Unload();
    return 1;
  }

  private int UpdateSorting(IDictionary values)
  {
    int lineNbr = (int) values[(object) "LineNbr"];
    string str = values[(object) "FieldName"] as string;
    int? nullable = values[(object) "SortOrder"] as int?;
    SortExp data = ((IEnumerable<SortExp>) this.Sorting.Select<SortExp>()).FirstOrDefault<SortExp>((Func<SortExp, bool>) (sort =>
    {
      int num = lineNbr;
      int? lineNbr1 = sort.LineNbr;
      int valueOrDefault = lineNbr1.GetValueOrDefault();
      return num == valueOrDefault & lineNbr1.HasValue;
    })) ?? this.Sorting.Insert();
    if (!string.IsNullOrEmpty(str))
      this.Sorting.Cache.SetValueExt<SortExp.fieldName>((object) data, (object) str);
    if (nullable.HasValue)
      this.Sorting.Cache.SetValueExt<SortExp.sortOrder>((object) data, (object) nullable);
    this.Sorting.Cache.Unload();
    return 1;
  }

  private int UpdateCondition(IDictionary values)
  {
    string str = values[(object) "FieldName"] as string;
    object obj1 = values[(object) "Condition"];
    object obj2 = values[(object) "Operation"];
    object obj3 = values[(object) "OpenBraces"];
    object obj4 = values[(object) "CloseBraces"];
    object newValue1 = values[(object) "Value"];
    object newValue2 = values[(object) "Value2"];
    object lineNbr = values[(object) "LineNbr"];
    FilterExp filterExp = ((IEnumerable<FilterExp>) this.Conditions.Select<FilterExp>()).FirstOrDefault<FilterExp>((Func<FilterExp, bool>) (filter =>
    {
      if (!(lineNbr is int num2))
        return false;
      int? lineNbr1 = filter.LineNbr;
      int valueOrDefault = lineNbr1.GetValueOrDefault();
      return num2 == valueOrDefault & lineNbr1.HasValue;
    })) ?? this.Conditions.Insert();
    PXCache cache = this.Conditions.Cache;
    if (!string.IsNullOrEmpty(str))
      cache.SetValueExt<FilterExp.fieldName>((object) filterExp, (object) str);
    bool flag1;
    switch (obj1)
    {
      case int _:
      case null:
        flag1 = true;
        break;
      default:
        flag1 = false;
        break;
    }
    if (flag1)
      cache.SetValueExt<FilterExp.condition>((object) filterExp, obj1);
    bool flag2;
    switch (obj2)
    {
      case int _:
      case null:
        flag2 = true;
        break;
      default:
        flag2 = false;
        break;
    }
    if (flag2)
      cache.SetValueExt<FilterExp.operation>((object) filterExp, obj2);
    bool flag3;
    switch (obj3)
    {
      case int _:
      case null:
        flag3 = true;
        break;
      default:
        flag3 = false;
        break;
    }
    if (flag3)
      cache.SetValueExt<FilterExp.openBraces>((object) filterExp, obj3);
    bool flag4;
    switch (obj4)
    {
      case int _:
      case null:
        flag4 = true;
        break;
      default:
        flag4 = false;
        break;
    }
    if (flag4)
      cache.SetValueExt<FilterExp.closeBraces>((object) filterExp, obj4);
    cache.RaiseFieldUpdating<FilterExp.value>((object) filterExp, ref newValue1);
    PXFieldState filterValueState1 = this.GetFilterValueState<FilterExp.value>(filterExp);
    if (filterValueState1 != null)
      values[(object) "Value"] = (object) filterValueState1;
    cache.RaiseFieldUpdating<FilterExp.value2>((object) filterExp, ref newValue2);
    PXFieldState filterValueState2 = this.GetFilterValueState<FilterExp.value2>(filterExp);
    if (filterValueState2 != null)
      values[(object) "Value2"] = (object) filterValueState2;
    cache.Unload();
    return 1;
  }

  private int UpdateTemplate(IDictionary values)
  {
    string str = values[(object) "Template"] as string;
    object obj1 = values[(object) "Shared"];
    object obj2 = values[(object) "IsDefault"];
    PXCache cache = this.EditTemplateDialog.Cache;
    TemplateProperties current = this.EditTemplateDialog.Current;
    if (!string.IsNullOrEmpty(str))
      cache.SetValueExt<TemplateProperties.template>((object) current, (object) str);
    bool flag1;
    switch (obj1)
    {
      case bool _:
      case null:
        flag1 = true;
        break;
      default:
        flag1 = false;
        break;
    }
    if (flag1)
      cache.SetValueExt<TemplateProperties.shared>((object) current, obj1);
    bool flag2;
    switch (obj2)
    {
      case bool _:
      case null:
        flag2 = true;
        break;
      default:
        flag2 = false;
        break;
    }
    if (flag2)
      cache.SetValueExt<TemplateProperties.isDefault>((object) current, obj2);
    return 1;
  }

  private int UpdateReportVersion(IDictionary keys, IDictionary values)
  {
    UserReport data = this.UserVersionReport.SelectSingle((object) (this._reportID + ".rpx"), (object) (keys[(object) "Version"] as int?));
    if (data == null)
      return 0;
    object obj = values[(object) "Description"];
    if (obj is string)
    {
      this.UserReports.Cache.SetValueExt<UserReport.description>((object) data, obj);
      ReportDbStorage.Update(data);
    }
    return 1;
  }

  private int UpdateReport(IDictionary keys, IDictionary values)
  {
    int num = base.ExecuteUpdate("Report", keys, values);
    if (num != 0)
      return num;
    this.Report.Cache.Unload();
    return num;
  }
}
