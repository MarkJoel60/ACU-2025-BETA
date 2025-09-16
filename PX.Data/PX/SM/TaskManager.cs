// Decompiled with JetBrains decompiler
// Type: PX.SM.TaskManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PX.Api;
using PX.Async;
using PX.BulkInsert;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Services.Interfaces;
using PX.Export.Authentication;
using PX.Licensing;
using PX.Logging.Sinks.SystemEventsDbSink;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Hosting;

#nullable enable
namespace PX.SM;

public class TaskManager : PXGraph<
#nullable disable
TaskManager>
{
  public PXFilter<TaskManagerRunningProcessFilter> Filter;
  private static readonly Regex ScreenRegex = new Regex("[a-z]{2,2}([0-9]{6,6})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  public PXSelectReadonly<SystemEvent, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  SystemEvent.date, 
  #nullable disable
  Between<BqlField<
  #nullable enable
  TaskManagerRunningProcessFilter.fromDate, IBqlDateTime>.FromCurrent, 
  #nullable disable
  BqlField<
  #nullable enable
  TaskManagerRunningProcessFilter.toDate, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<BqlOperand<
  #nullable enable
  SystemEvent.level, IBqlInt>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  TaskManagerRunningProcessFilter.level, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PX.Data.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  SystemEvent.tenantName, 
  #nullable disable
  In3<CurrentCompanies>>>>>.Or<BqlOperand<
  #nullable enable
  SystemEvent.tenantName, IBqlString>.IsNull>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<
  #nullable enable
  TaskManagerRunningProcessFilter.source>, 
  #nullable disable
  PX.Data.Contains<SystemEvent.source>>>>>.Or<BqlOperand<Current<
  #nullable enable
  TaskManagerRunningProcessFilter.source>, IBqlString>.Contains<
  #nullable disable
  AllCategories>>>>, OrderBy<Desc<SystemEvent.id>>> SystemEvents;
  public PXSelectReadonly<SystemEvent, Where<SystemEvent.id, Equal<Current<SystemEvent.id>>>, OrderBy<Desc<SystemEvent.id>>> CurrentSystemEvent;
  public PXAction<SystemEvent> RedirectToScreen;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<RowTaskInfo> Items;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<RowActiveUserInfo> ActiveUsers;
  public PXSelect<SiteMap, Where<Optional<SiteMap.screenID>, Equal<SiteMap.screenID>>> ViewSiteMap;
  public PXAction<TaskManagerRunningProcessFilter> ActionStop;
  public PXAction<TaskManagerRunningProcessFilter> ActionShow;
  public PXAction<TaskManagerRunningProcessFilter> ActionStackTrace;
  public PXFilter<TaskManager.RowMemoryDumpOptions> ViewMemoryDumpOptions;
  public PXAction<TaskManagerRunningProcessFilter> ActionCreateMemoryDump;
  public PXAction<TaskManagerRunningProcessFilter> ActionGC;
  public PXAction<TaskManagerRunningProcessFilter> ActionViewUser;
  public PXAction<TaskManagerRunningProcessFilter> ActionUpdateData;
  public PXFilter<PerformanceMonitorMaint.SMPerformanceFilterRow> CurrentThreadsPanel;
  public PXSelect<SMPerformanceInfo> Samples;
  public PXSelectReadonly<SMPerformanceInfoSQLWithTables> Sql;
  public PXFilter<SMPerformanceInfoSQLWithTables> SqlSummaryRowsPreview;
  public PXSelect<SMPerformanceInfoTraceWithMessages> TraceEvents;
  public PXSelect<SMPerformanceInfoTraceWithMessages> TraceExceptions;
  public PXFilter<SMPerformanceInfoTraceWithMessages> TraceEventWithMessage;
  private static readonly Regex InsertOrUpdatedStatement = new Regex("(INSERT\\s+\\w+)|(UPDATE\\s+\\w+)");
  public PXAction<TaskManagerRunningProcessFilter> ActionViewSql;
  public PXAction<SMPerformanceInfo> ActionViewExceptions;
  public PXAction<TaskManagerRunningProcessFilter> ActionViewTrace;

  [InjectDependency]
  private IOptionsSnapshot<FormsAuthenticationOptions> _formsAuthenticationOptions { get; set; }

  [InjectDependency]
  private IPXLicensePolicy _licensePolicy { get; set; }

  [InjectDependency]
  private ILicensingManager _licensingManager { get; set; }

  [InjectDependency]
  private ILegacyCompanyService _legacyCompanyService { get; set; }

  [InjectDependency]
  private IMemoryDumpService _memoryDumpService { get; set; }

  [InjectDependency]
  private INativeMethodsService _nativeMethodsService { get; set; }

  [InjectDependency]
  private ILongOperationTaskManager _longOperationTaskManager { get; set; }

  protected virtual void _(Events.FieldSelecting<SystemEvent.screenId> e)
  {
    if (!(e.Row is SystemEvent row))
      return;
    e.ReturnValue = (object) this.FormatScreenId(row);
  }

  protected virtual void _(Events.FieldSelecting<SystemEvent.linkToEntity> e)
  {
    if (!(e.Row is SystemEvent row))
      return;
    e.ReturnValue = (object) this.FormatLinkToEntity(row);
  }

  protected virtual void _(
    Events.FieldSelecting<SystemEvent.formattedProperties> e)
  {
    if (!(e.Row is SystemEvent row))
      return;
    e.ReturnValue = (object) this.FormatProperties(row);
  }

  /// <summary>
  /// Formats screen ID with a dot after every 2 characters except after the rightmost 2 characters.
  /// </summary>
  /// <param name="screenId">ScreenId without dots.</param>
  /// <returns></returns>
  private static string DottedScreenId(string screenId)
  {
    return new string(screenId.SelectMany<char, char>((Func<char, int, IEnumerable<char>>) ((c, i) => i % 2 != 0 || i <= 0 ? (IEnumerable<char>) new char[1]
    {
      c
    } : (IEnumerable<char>) new char[2]{ '.', c })).ToArray<char>());
  }

  private string FormatScreenId(SystemEvent record)
  {
    string input = record.ScreenId ?? string.Empty;
    Match match = TaskManager.ScreenRegex.Match(input);
    return match.Success ? TaskManager.DottedScreenId(match.Value) : record.ScreenId;
  }

  private string FormatLinkToEntity(SystemEvent record)
  {
    string str = record.ScreenId ?? string.Empty;
    Match match = TaskManager.ScreenRegex.Match(str);
    if (match.Success)
    {
      string entity = "~/Main?ScreenId=" + match.Value;
      if (!string.IsNullOrWhiteSpace(record.TenantName))
        entity = $"{entity}&CompanyID={record.TenantName}";
      try
      {
        JToken jtoken = JObject.Parse(record.Properties)["Properties"].SelectToken("Schedule.ScheduleID", false);
        if (jtoken != null)
          entity = $"{entity}&ScheduleID={JToken.op_Explicit(jtoken)}";
      }
      catch
      {
      }
      return entity;
    }
    if (string.Compare(Path.GetFileName(str), "login.aspx", StringComparison.OrdinalIgnoreCase) == 0)
      return "Frames/Login.aspx";
    string.Compare(Path.GetFileName(str), "getfile.ashx", StringComparison.OrdinalIgnoreCase);
    return record.LinkToEntity;
  }

  private string FormatProperties(SystemEvent record)
  {
    if (string.IsNullOrEmpty(record.Properties))
      return (string) null;
    JObject jobject = JObject.Parse(record.Properties);
    JToken source1 = jobject["Properties"];
    JToken jtoken1 = source1[(object) "SourceContext"];
    string key = jtoken1 != null ? Newtonsoft.Json.Linq.Extensions.Value<string>((IEnumerable<JToken>) jtoken1) : (string) null;
    if (key == null)
      return (string) null;
    string[] propsWhiteList;
    if (!PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.DisplayedPropertiesPerEventType.TryGetValue(key, out propsWhiteList))
      propsWhiteList = new string[0];
    ICollection<string> commonPropsWhiteList = (ICollection<string>) PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.DisplayedPropertiesPerEventType["All"];
    string newLine = Environment.NewLine;
    IEnumerable<\u003C\u003Ef__AnonymousType49<string, string, bool>> first = ((IEnumerable) source1).OfType<JProperty>().Where<JProperty>((Func<JProperty, bool>) (p => ((IEnumerable<string>) propsWhiteList).Contains<string>(p.Name) || commonPropsWhiteList.Contains(p.Name))).Select(p =>
    {
      string str = p.Value?.ToString();
      return new
      {
        Name = p.Name,
        Value = str,
        IsMultyLine = str != null && str.Contains("\n")
      };
    });
    \u003C\u003Ef__AnonymousType49<string, string, bool>[] source2 = new \u003C\u003Ef__AnonymousType49<string, string, bool>[1];
    JToken jtoken2 = jobject["Exception"];
    source2[0] = new
    {
      Name = "Exception",
      Value = jtoken2 != null ? Newtonsoft.Json.Linq.Extensions.Value<string>((IEnumerable<JToken>) jtoken2) : (string) null,
      IsMultyLine = true
    };
    IEnumerable<\u003C\u003Ef__AnonymousType49<string, string, bool>> second = source2.Where(p => p.Value != null);
    IEnumerable<string> values = first.Concat(second).OrderBy(p => p.IsMultyLine).ThenBy(p => p.Name).Select((p, i) =>
    {
      if (!p.IsMultyLine)
        return $"{p.Name} = {p.Value}";
      return $"{(i > 0 ? Environment.NewLine : (string) null)}{p.Name} ={Environment.NewLine}{p.Value}";
    });
    return string.Join(newLine, values);
  }

  public virtual void _(
    Events.RowUpdated<TaskManagerRunningProcessFilter> e)
  {
    TaskManagerRunningProcessFilter row = e.Row;
    if (row == null)
      return;
    TaskManagerRunningProcessFilter runningProcessFilter = row;
    System.DateTime? toDate = row.ToDate;
    ref System.DateTime? local = ref toDate;
    System.DateTime? nullable;
    if (!local.HasValue)
    {
      nullable = new System.DateTime?();
    }
    else
    {
      System.DateTime dateTime = local.GetValueOrDefault();
      dateTime = dateTime.Date;
      dateTime = dateTime.AddDays(1.0);
      nullable = new System.DateTime?(dateTime.AddTicks(-1L));
    }
    runningProcessFilter.ToDate = nullable;
  }

  [PXUIField]
  [PXButton]
  protected virtual void redirectToScreen()
  {
    throw new PXRedirectToUrlException(this.SystemEvents.GetValueExt<SystemEvent.linkToEntity>(this.SystemEvents.Current).ToString(), PXBaseRedirectException.WindowMode.New, true, string.Empty);
  }

  protected IEnumerable items()
  {
    IEnumerable<RowTaskInfo> source = this._longOperationTaskManager.GetTasks();
    bool? showAllUsers = this.Filter.Current.ShowAllUsers;
    bool flag = true;
    if (!(showAllUsers.GetValueOrDefault() == flag & showAllUsers.HasValue))
    {
      string user = PXContext.PXIdentity.User.Identity.Name;
      source = source.Where<RowTaskInfo>((Func<RowTaskInfo, bool>) (_ => _.User == user));
    }
    RowTaskInfo[] array = source.ToArray<RowTaskInfo>();
    foreach (RowTaskInfo rowTaskInfo in array)
    {
      SiteMap siteMap = this.GetSiteMap(rowTaskInfo.Screen);
      if (siteMap != null)
        rowTaskInfo.Title = siteMap.Title;
    }
    return (IEnumerable) array;
  }

  protected IEnumerable activeUsers()
  {
    int valueOrDefault = this.Filter.Current.LoginType.GetValueOrDefault();
    List<RowActiveUserInfo> source = new List<RowActiveUserInfo>();
    if (valueOrDefault == 0 || valueOrDefault == 1)
    {
      int authTimeout = ((IOptions<FormsAuthenticationOptions>) this._formsAuthenticationOptions).Value.Timeout;
      source.AddRange(this._licensingManager.GetCurrentUsers());
      if (source.Any<RowActiveUserInfo>())
      {
        if (PXAccess.GetCompanies().Length > 1)
        {
          foreach (string str in source.Select<RowActiveUserInfo, string>((Func<RowActiveUserInfo, string>) (it => it.Company)).Distinct<string>().ToArray<string>())
          {
            string company = str;
            using (TaskManager.GetCompanyScope(company))
            {
              string[] array = source.Where<RowActiveUserInfo>((Func<RowActiveUserInfo, bool>) (it => it.Company == company)).Select<RowActiveUserInfo, string>((Func<RowActiveUserInfo, string>) (it => it.User)).Distinct<string>().ToArray<string>();
              PXSelectBase<Users, PXSelect<Users, Where<Users.username, In<Required<Users.username>>>>.Config>.Clear((PXGraph) this);
              this.Caches[typeof (Users)].Clear();
              foreach (PXResult<Users> pxResult in PXSelectBase<Users, PXSelect<Users, Where<Users.username, In<Required<Users.username>>>>.Config>.Select((PXGraph) this, (object) array))
              {
                Users user = (Users) pxResult;
                System.DateTime currentDateTime = PXTimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), LocaleInfo.GetTimeZone());
                EnumerableExtensions.ForEach<RowActiveUserInfo>(source.Where<RowActiveUserInfo>((Func<RowActiveUserInfo, bool>) (it => it.User == user.Username && it.Company == company)), (System.Action<RowActiveUserInfo>) (it => it.LoginTimeSpan = new int?(Convert.ToInt32((currentDateTime - (user.LastLoginDate ?? currentDateTime)).TotalMinutes))));
              }
            }
          }
        }
        else
        {
          PXResultset<Users> pxResultset = PXSelectBase<Users, PXSelect<Users, Where<Users.username, In<Required<Users.username>>>>.Config>.Select((PXGraph) this, (object) source.Select<RowActiveUserInfo, string>((Func<RowActiveUserInfo, string>) (it => it.User)).Distinct<string>().ToArray<string>());
          System.DateTime currentDateTime = PXTimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Now.ToUniversalTime(), LocaleInfo.GetTimeZone());
          foreach (PXResult<Users> pxResult in pxResultset)
          {
            Users user = (Users) pxResult;
            EnumerableExtensions.ForEach<RowActiveUserInfo>(source.Where<RowActiveUserInfo>((Func<RowActiveUserInfo, bool>) (it => it.User == user.Username)), (System.Action<RowActiveUserInfo>) (it => it.LoginTimeSpan = new int?(Convert.ToInt32((currentDateTime - (user.LastLoginDate ?? currentDateTime)).TotalMinutes))));
          }
        }
        source = source.Where<RowActiveUserInfo>((Func<RowActiveUserInfo, bool>) (u =>
        {
          int? lastActivity = u.LastActivity;
          int num = authTimeout;
          return lastActivity.GetValueOrDefault() <= num & lastActivity.HasValue;
        })).ToList<RowActiveUserInfo>();
      }
    }
    if (valueOrDefault == 0 || valueOrDefault == 2)
      source.AddRange(this._licensePolicy.GetCurrentApiUsers());
    return (IEnumerable) source;
  }

  [PXButton]
  [PXUIField(DisplayName = "Abort")]
  protected void actionStop()
  {
    if (this.Items.Current == null)
      return;
    RowTaskInfo taskInfo = this._longOperationTaskManager.GetTasks(this.Items.Current.Key).FirstOrDefault<RowTaskInfo>();
    if (taskInfo == null)
      return;
    this._longOperationTaskManager.AbortTask(taskInfo);
  }

  [PXButton]
  [PXUIField(DisplayName = "View Screen")]
  protected void actionShow()
  {
    if (this.Items.Current == null)
      return;
    RowTaskInfo taskInfo = this._longOperationTaskManager.GetTasks(this.Items.Current.Key).FirstOrDefault<RowTaskInfo>();
    if (taskInfo == null)
      return;
    SiteMap siteMap = this.GetSiteMap(taskInfo.Screen);
    if (siteMap == null)
      return;
    System.Type type = PXBuildManager.GetType(siteMap.Graphtype, false);
    if (!(type == (System.Type) null))
    {
      PXGraph instance = PXGraph.CreateInstance(type);
      instance.UID = taskInfo.NativeKey;
      int sharedTaskStatus = (int) this._longOperationTaskManager.GetSharedTaskStatus(taskInfo);
      throw new PXRedirectRequiredException(instance, (string) null);
    }
  }

  [PXButton(Tooltip = "View Stack Traces of Running Threads")]
  [PXUIField(DisplayName = "Active Threads")]
  protected void actionStackTrace()
  {
    string str = PXPerformanceMonitor.EnumWorkingThreads();
    if (Str.IsNullOrEmpty(str))
      str = Messages.GetLocal("No Active Threads");
    this.CurrentThreadsPanel.Current.CurrentThreads = str;
    int num = (int) this.CurrentThreadsPanel.AskExt(true);
  }

  protected virtual void _(
    Events.RowSelected<TaskManager.RowMemoryDumpOptions> e)
  {
    TaskManager.RowMemoryDumpOptions current = this.ViewMemoryDumpOptions.Current;
    Decimal num1 = current.DumpContentType == 0 ? 52428800M : (Decimal) this._nativeMethodsService.ProcessWorkingSet;
    Decimal num2 = num1 / 1073741824M;
    TaskManager.RowMemoryDumpOptions memoryDumpOptions1 = current;
    object obj = HostingEnvironment.MapPath(this._memoryDumpService.DefaultRelativeDumpPath) == this._memoryDumpService.DumpFolder ? (object) this._memoryDumpService.DefaultRelativeDumpPath : (object) this._memoryDumpService.DumpFolder;
    int num3 = current.DumpContentType != 0 ? (!(num2 < 5M) ? (num2 < 7M ? 60 : 90) : (num2 < 2M ? 10 : 30)) : 15;
    TaskManager.RowMemoryDumpOptions memoryDumpOptions2 = memoryDumpOptions1;
    object[] objArray = new object[3]
    {
      obj,
      (object) num3,
      null
    };
    string str1;
    if (!(num2 >= 1M))
      str1 = PXMessages.LocalizeFormatNoPrefixNLA("{0:N0} MB", (object) (num1 / 1048576M));
    else
      str1 = PXMessages.LocalizeFormatNoPrefixNLA("{0:N1} GB", (object) num2);
    objArray[2] = (object) str1;
    string str2 = PXMessages.LocalizeFormatNoPrefixNLA("A DMP file will be created in the {0} folder. It takes approximately {1} seconds and the file size is about {2}.", objArray);
    memoryDumpOptions2.InformationMessage = str2;
    PXUIFieldAttribute.SetVisible<TaskManager.RowMemoryDumpOptions.warningMessage>(e.Cache, (object) e.Row, e.Row.DumpContentType == 1);
  }

  [PXButton]
  [PXUIField(DisplayName = "Create Memory Dump")]
  protected void actionCreateMemoryDump()
  {
    if (this.ViewMemoryDumpOptions.AskExt((PXView.InitializePanel) ((graph, viewName) => this.ViewMemoryDumpOptions.Cache.Clear())) != WebDialogResult.Yes)
      return;
    (string dumpFileName, string str) = this._memoryDumpService.TryCreateDump(this.ViewMemoryDumpOptions.Current.DumpContentType == 0);
    if (string.IsNullOrEmpty(str))
      throw new PXDialogRequiredException("ViewMemoryDumpOptions", (object) null, PXMessages.LocalizeNoPrefix("Create Memory Dump"), PXMessages.LocalizeFormatNoPrefixNLA("The memory dump file has been created: {0}.", (object) dumpFileName), MessageButtons.OK, MessageIcon.Information);
    throw new PXDialogRequiredException("ViewMemoryDumpOptions", (object) null, PXMessages.LocalizeNoPrefix("Create Memory Dump"), str, MessageButtons.OK, MessageIcon.Error);
  }

  [PXButton(VisibleOnDataSource = false)]
  [PXUIField(DisplayName = "Collect Memory")]
  protected void actionGC() => GCHelper.ForcedCollect(false);

  [PXButton(CommitChanges = true, DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "VIEW USER", Enabled = false)]
  protected void actionViewUser()
  {
    RowActiveUserInfo current = this.ActiveUsers.Current;
    if (current == null)
      return;
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.username, Equal<Required<Users.username>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], (object) this._legacyCompanyService.ExtractUsername(current.User)).FirstOrDefault<PXResult<Users>>();
    if (users != null)
    {
      System.Type graphType;
      PXPrimaryGraphAttribute.FindPrimaryGraph(this.Caches[typeof (Users)], out graphType);
      PXGraph instance = PXGraph.CreateInstance(graphType);
      instance.Caches[typeof (Users)].Current = (object) users;
      throw new PXRedirectRequiredException(instance, (string) null);
    }
  }

  [PXButton(VisibleOnDataSource = false)]
  [PXUIField(DisplayName = "Update Data")]
  protected void actionUpdateData()
  {
  }

  public TaskManager()
  {
    this.Items.Cache.AllowDelete = false;
    this.Items.Cache.AllowUpdate = false;
    this.Items.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetVisible<RowActiveUserInfo.company>(this.ActiveUsers.Cache, (object) null, PXDatabase.Companies.Length > 1);
    this.Samples.Cache.AllowInsert = false;
    this.Samples.Cache.AllowDelete = false;
    this.Sql.Cache.AllowInsert = false;
    this.Sql.Cache.AllowUpdate = false;
    this.Sql.Cache.AllowDelete = false;
    this.Sql.View.IsReadOnly = true;
    this.TraceEvents.Cache.AllowInsert = false;
    this.TraceEvents.Cache.AllowUpdate = false;
    this.TraceEvents.Cache.AllowDelete = false;
    this.TraceEvents.View.IsReadOnly = true;
  }

  private SiteMap GetSiteMap(string screen)
  {
    screen = (screen ?? string.Empty).Replace(".", "");
    return (SiteMap) this.ViewSiteMap.SelectWindowed(0, 1, (object) screen);
  }

  private static PXLoginScope GetCompanyScope(string companyName)
  {
    string userName = "admin";
    if (PXDatabase.Companies.Length != 0)
      userName = $"{userName}@{companyName}";
    return new PXLoginScope(userName, PXAccess.GetAdministratorRoles());
  }

  protected virtual void RowActiveUserInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RowActiveUserInfo row = (RowActiveUserInfo) e.Row;
    if (row == null)
      return;
    this.ActionViewUser.SetEnabled(row.Company == PXAccess.GetCompanyName());
  }

  protected virtual IEnumerable samples()
  {
    List<SMPerformanceInfo> smPerformanceInfoList = new List<SMPerformanceInfo>();
    int num = 1;
    foreach (PXPerformanceInfo pxPerformanceInfo in ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress).OrderByDescending<PXPerformanceInfo, System.DateTime>((Func<PXPerformanceInfo, System.DateTime>) (_ => _.StartTime)).ToList<PXPerformanceInfo>())
    {
      double totalMilliseconds1 = pxPerformanceInfo.ThreadTime.TotalMilliseconds;
      TimeSpan elapsed = pxPerformanceInfo.Timer.Elapsed;
      double totalMilliseconds2 = elapsed.TotalMilliseconds;
      elapsed = pxPerformanceInfo.SqlTimer.Elapsed;
      double totalMilliseconds3 = elapsed.TotalMilliseconds;
      string str = !string.IsNullOrEmpty(pxPerformanceInfo.InternalScreenId) ? $"{pxPerformanceInfo.InternalScreenId}@{TaskManager.GetUrlToScreen(pxPerformanceInfo)}" : (string) null;
      SMPerformanceInfo smPerformanceInfo1 = new SMPerformanceInfo();
      smPerformanceInfo1.RecordId = new int?(num++);
      smPerformanceInfo1.ID = pxPerformanceInfo.ID;
      smPerformanceInfo1.CommandName = pxPerformanceInfo.CommandName;
      smPerformanceInfo1.CommandTarget = pxPerformanceInfo.CommandTarget;
      smPerformanceInfo1.RequestStartTime = new System.DateTime?(pxPerformanceInfo.StartTimeLocal);
      smPerformanceInfo1.RequestTimeMs = new double?(totalMilliseconds2);
      smPerformanceInfo1.ScreenId = this.GetScreenUrl(pxPerformanceInfo);
      smPerformanceInfo1.InternalScreenId = pxPerformanceInfo.InternalScreenId;
      smPerformanceInfo1.UrlToScreen = str;
      smPerformanceInfo1.SqlCounter = new int?(pxPerformanceInfo.SqlCounter);
      smPerformanceInfo1.SqlRows = new int?(pxPerformanceInfo.SqlRows);
      smPerformanceInfo1.RequestType = pxPerformanceInfo.RequestType;
      smPerformanceInfo1.ExceptionCounter = new int?(pxPerformanceInfo.ExceptionCounter);
      smPerformanceInfo1.EventCounter = new int?(pxPerformanceInfo.TraceCounter);
      smPerformanceInfo1.SqlTimeMs = new double?(totalMilliseconds3);
      smPerformanceInfo1.SelectCounter = new int?(pxPerformanceInfo.SelectCount);
      elapsed = pxPerformanceInfo.SelectTimer.Elapsed;
      smPerformanceInfo1.SelectTimeMs = new double?(elapsed.TotalMilliseconds);
      smPerformanceInfo1.UserId = pxPerformanceInfo.UserId;
      smPerformanceInfo1.TenantId = pxPerformanceInfo.TenantId;
      smPerformanceInfo1.RequestCpuTimeMs = new double?(totalMilliseconds1);
      smPerformanceInfo1.MemBefore = new long?(pxPerformanceInfo.MemBefore);
      smPerformanceInfo1.MemBeforeMb = new double?((double) pxPerformanceInfo.MemBefore / 1000000.0);
      smPerformanceInfo1.MemoryWorkingSet = new double?(pxPerformanceInfo.MemWorkingSet);
      smPerformanceInfo1.InstallationId = PXLicenseHelper.InstallationID;
      elapsed = pxPerformanceInfo.SessionLoadTimer.Elapsed;
      smPerformanceInfo1.SessionLoadTimeMs = new double?(elapsed.TotalMilliseconds);
      smPerformanceInfo1.WaitTime = new double?(totalMilliseconds2 > totalMilliseconds1 + totalMilliseconds3 ? totalMilliseconds2 - totalMilliseconds1 - totalMilliseconds3 : 0.0);
      SMPerformanceInfo smPerformanceInfo2 = smPerformanceInfo1;
      smPerformanceInfoList.Add(smPerformanceInfo2);
    }
    return (IEnumerable) smPerformanceInfoList;
  }

  private static string GetUrlToScreen(PXPerformanceInfo info)
  {
    if (string.IsNullOrEmpty(info.InternalScreenId))
      return (string) null;
    string urlToScreen = "/Main?ScreenId=" + info.InternalScreenId;
    if (!string.IsNullOrEmpty(info.TenantId))
      urlToScreen = $"{urlToScreen}&CompanyID={info.TenantId}";
    return urlToScreen;
  }

  private string GetScreenUrl(PXPerformanceInfo pi)
  {
    string uriString = pi?.ScreenId;
    if (Str.IsNullOrEmpty(uriString))
      return (string) null;
    if (Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
      return uriString;
    if (uriString.StartsWith("~"))
      uriString = uriString.Substring(1);
    if (!uriString.StartsWith("/"))
      uriString = "/" + uriString;
    return uriString;
  }

  protected virtual IEnumerable sql()
  {
    List<SMPerformanceInfoSQLWithTables> infoSqlWithTablesList = new List<SMPerformanceInfoSQLWithTables>();
    SMPerformanceInfo c = this.Samples.Current;
    if (c == null)
      return (IEnumerable) infoSqlWithTablesList;
    PXPerformanceInfo pxPerformanceInfo = ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress).Where<PXPerformanceInfo>((Func<PXPerformanceInfo, bool>) (_ => _.ID.Equals(c.ID, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<PXPerformanceInfo>();
    if (pxPerformanceInfo == null || pxPerformanceInfo.SqlSamples == null)
      return (IEnumerable) infoSqlWithTablesList;
    List<PXProfilerSqlSample> list = pxPerformanceInfo.SqlSamples.ToList<PXProfilerSqlSample>();
    int num = 1;
    foreach (PXProfilerSqlSample profilerSqlSample in list)
    {
      SMPerformanceInfoSQLWithTables infoSqlWithTables1 = new SMPerformanceInfoSQLWithTables();
      infoSqlWithTables1.RecordId = new int?(num++);
      infoSqlWithTables1.ParentId = c.RecordId;
      infoSqlWithTables1.SqlId = new int?(profilerSqlSample.SqlTextId);
      infoSqlWithTables1.TableList = profilerSqlSample.Tables;
      infoSqlWithTables1.SQLText = profilerSqlSample.Text;
      infoSqlWithTables1.StackTrace = profilerSqlSample.StackTrace;
      infoSqlWithTables1.RequestStartTime = profilerSqlSample.StartTime;
      infoSqlWithTables1.NRows = profilerSqlSample.RowCount;
      infoSqlWithTables1.SqlTimeMs = new double?((double) profilerSqlSample.SqlTimer.ElapsedMilliseconds);
      infoSqlWithTables1.SQLParams = profilerSqlSample.Params;
      infoSqlWithTables1.QueryCache = new bool?(profilerSqlSample.QueryCache);
      SMPerformanceInfoSQLWithTables infoSqlWithTables2 = infoSqlWithTables1;
      infoSqlWithTablesList.Add(infoSqlWithTables2);
    }
    return (IEnumerable) infoSqlWithTablesList;
  }

  protected virtual IEnumerable traceExceptions()
  {
    List<SMPerformanceInfoTraceWithMessages> traceWithMessagesList = new List<SMPerformanceInfoTraceWithMessages>();
    SMPerformanceInfo c = this.Samples.Current;
    if (c == null)
      return (IEnumerable) traceWithMessagesList;
    PXPerformanceInfo pxPerformanceInfo = ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress).Where<PXPerformanceInfo>((Func<PXPerformanceInfo, bool>) (_ => _.ID.Equals(c.ID, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<PXPerformanceInfo>();
    if (pxPerformanceInfo == null || pxPerformanceInfo.TraceItems == null)
      return (IEnumerable) traceWithMessagesList;
    List<PXProfilerTraceItem> list = pxPerformanceInfo.TraceItemsInProgress.Where<PXProfilerTraceItem>((Func<PXProfilerTraceItem, bool>) (_ => _.EventType.Equals("FirstChanceException"))).ToList<PXProfilerTraceItem>();
    int num = 1;
    foreach (PXProfilerTraceItem profilerTraceItem in list)
    {
      SMPerformanceInfoTraceWithMessages traceWithMessages1 = new SMPerformanceInfoTraceWithMessages();
      traceWithMessages1.RecordId = new int?(num++);
      traceWithMessages1.ParentId = c.RecordId;
      traceWithMessages1.RequestStartTime = profilerTraceItem.StartTime;
      traceWithMessages1.Source = profilerTraceItem.Source;
      traceWithMessages1.TraceType = profilerTraceItem.EventType;
      traceWithMessages1.MessageText = profilerTraceItem.Message;
      traceWithMessages1.StackTrace = profilerTraceItem.StackTrace;
      traceWithMessages1.ExceptionType = profilerTraceItem.ExceptionType;
      SMPerformanceInfoTraceWithMessages traceWithMessages2 = traceWithMessages1;
      traceWithMessagesList.Add(traceWithMessages2);
    }
    return (IEnumerable) traceWithMessagesList;
  }

  protected virtual IEnumerable traceEvents()
  {
    List<SMPerformanceInfoTraceWithMessages> traceWithMessagesList = new List<SMPerformanceInfoTraceWithMessages>();
    SMPerformanceInfo c = this.Samples.Current;
    if (c == null)
      return (IEnumerable) traceWithMessagesList;
    PXPerformanceInfo pxPerformanceInfo = ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress).Where<PXPerformanceInfo>((Func<PXPerformanceInfo, bool>) (_ => _.ID.Equals(c.ID, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<PXPerformanceInfo>();
    if (pxPerformanceInfo == null || pxPerformanceInfo.TraceItems == null)
      return (IEnumerable) traceWithMessagesList;
    List<PXProfilerTraceItem> list = pxPerformanceInfo.TraceItemsInProgress.Where<PXProfilerTraceItem>((Func<PXProfilerTraceItem, bool>) (_ => !_.EventType.Equals("FirstChanceException"))).ToList<PXProfilerTraceItem>();
    int num = 0;
    foreach (PXProfilerTraceItem profilerTraceItem in list)
    {
      SMPerformanceInfoTraceWithMessages traceWithMessages1 = new SMPerformanceInfoTraceWithMessages();
      traceWithMessages1.RecordId = new int?(num++);
      traceWithMessages1.ParentId = c.RecordId;
      traceWithMessages1.RequestStartTime = profilerTraceItem.StartTime;
      traceWithMessages1.Source = profilerTraceItem.Source;
      traceWithMessages1.TraceType = profilerTraceItem.EventType;
      traceWithMessages1.MessageText = profilerTraceItem.Message;
      traceWithMessages1.StackTrace = profilerTraceItem.StackTrace;
      traceWithMessages1.ExceptionType = profilerTraceItem.ExceptionType;
      SMPerformanceInfoTraceWithMessages traceWithMessages2 = traceWithMessages1;
      traceWithMessagesList.Add(traceWithMessages2);
    }
    return (IEnumerable) traceWithMessagesList;
  }

  [PXButton]
  protected virtual IEnumerable actionViewSql(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.Sql.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual IEnumerable actionViewExceptions(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.TraceExceptions.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual IEnumerable actionViewTrace(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.TraceEvents.AskExt();
    }
    return adapter.Get();
  }

  protected void SMPerformanceInfoSQLWithTables_SQLWithStackTrace_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SMPerformanceInfoSQLWithTables row))
      return;
    string str = row.SQLWithParams;
    if (PXPerformanceMonitor.FormatSQLEnabled)
      str = SqlFormatter.FormatSQL(str, true);
    if (!PXPerformanceMonitor.SqlProfilerShowStackTrace || string.IsNullOrEmpty(row.StackTrace))
      e.ReturnValue = (object) str;
    else
      e.ReturnValue = (object) $"{str}\n\n/* Stack Trace:\n\n{row.StackTrace} */";
  }

  protected void SMPerformanceInfoSQLWithTables_TableList_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SMPerformanceInfoSQLWithTables row))
      return;
    string str = ((IEnumerable<string>) PXDatabase.ExtractTablesFromSqlWithCase(row.SQLText)).JoinToString<string>(",");
    if (Str.IsNullOrEmpty(str))
    {
      if (row.SQLText.StartsWith("SELECT "))
        str = row.SQLText.Substring(7);
      else if (row.SQLText.StartsWith("INSERT ") || row.SQLText.StartsWith("UPDATE "))
      {
        Match match = TaskManager.InsertOrUpdatedStatement.Match(row.SQLText);
        if (match.Success)
          str = match.ToString();
      }
    }
    e.ReturnValue = (object) str;
  }

  [PXHidden]
  [Serializable]
  public class RowMemoryDumpOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField]
    [PXInt]
    public int DumpContentType { get; set; }

    [PXUIField(IsReadOnly = true)]
    public string WarningMessage { get; } = Messages.GetLocal("The operation might take a long time so it is highly probable the instance will be restarted by IIS. The operation is not recommended.");

    [PXUIField(IsReadOnly = true)]
    public string InformationMessage { get; set; }

    public abstract class warningMessage : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TaskManager.RowMemoryDumpOptions.warningMessage>
    {
    }
  }
}
