// Decompiled with JetBrains decompiler
// Type: PX.SM.PerformanceMonitorUserMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Utility;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

#nullable enable
namespace PX.SM;

[Serializable]
public class PerformanceMonitorUserMaint : PXGraph<
#nullable disable
PerformanceMonitorUserMaint>
{
  public PXFilter<PerformanceMonitorUserMaint.ProfilerInfo> ProfilerInfoView;
  public PXAction<PerformanceMonitorUserMaint.ProfilerInfo> ActionStartProfiler;
  public PXAction<PerformanceMonitorUserMaint.ProfilerInfo> ActionStopProfiler;
  public PXAction<PerformanceMonitorUserMaint.ProfilerInfo> ActionLastRequest;
  public PXAction<PerformanceMonitorUserMaint.ProfilerInfo> ActionProfiler;

  public PerformanceMonitorUserMaint()
  {
    string str = (string) null;
    try
    {
      str = PXAccess.GetUserName();
    }
    catch
    {
    }
    if ((HttpContext.Current == null || !PXPerformanceMonitor.SaveRequestsToDb || string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) ? 0 : (PXPerformanceMonitor.UserProfilerName.Equals(str, StringComparison.OrdinalIgnoreCase) ? 1 : 0)) != 0)
    {
      this.ActionStartProfiler.SetVisible(false);
      this.ActionStopProfiler.SetVisible(true);
      this.ActionLastRequest.SetVisible(false);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.started>(this.ProfilerInfoView.Cache, (object) null, true);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.requestsLogged>(this.ProfilerInfoView.Cache, (object) null, true);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.startText>(this.ProfilerInfoView.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.exportText>(this.ProfilerInfoView.Cache, (object) null, false);
    }
    else
    {
      this.ActionStartProfiler.SetVisible(true);
      this.ActionStopProfiler.SetVisible(false);
      this.ActionLastRequest.SetVisible(true);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.started>(this.ProfilerInfoView.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.requestsLogged>(this.ProfilerInfoView.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.startText>(this.ProfilerInfoView.Cache, (object) null, true);
      PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.exportText>(this.ProfilerInfoView.Cache, (object) null, true);
      PXPerformanceMonitor.StopUserProfiler();
      PXSharedUserSession.CurrentUser["UserProfilerStarted"] = (object) null;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Start Logging")]
  protected void actionStartProfiler()
  {
    if (PXPerformanceMonitor.IsProfilerDataSizeOverLimits)
      throw new PXException($"The data size limit ({SpaceUsageMaint.FormatSize((double) WebConfig.ProfilerDataSizeLimit)}) for the request profiler has been exceeded. The profiler has been disabled. Clean up collected data to enable the profiler again.");
    this.ActionStartProfiler.SetVisible(false);
    this.ActionStopProfiler.SetVisible(true);
    this.ActionLastRequest.SetVisible(false);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.started>(this.ProfilerInfoView.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.requestsLogged>(this.ProfilerInfoView.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.startText>(this.ProfilerInfoView.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.exportText>(this.ProfilerInfoView.Cache, (object) null, false);
    PXSharedUserSession.CurrentUser["UserProfilerStarted"] = (object) true;
    PXPerformanceMonitor.StartUserProfiler();
  }

  [PXButton]
  [PXUIField(DisplayName = "Stop And Export")]
  protected void actionStopProfiler()
  {
    this.ActionStartProfiler.SetVisible(true);
    this.ActionStopProfiler.SetVisible(false);
    this.ActionLastRequest.SetVisible(true);
    this.ActionProfiler.SetVisible(true);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.started>(this.ProfilerInfoView.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.requestsLogged>(this.ProfilerInfoView.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.startText>(this.ProfilerInfoView.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PerformanceMonitorUserMaint.ProfilerInfo.exportText>(this.ProfilerInfoView.Cache, (object) null, true);
    PXSharedUserSession.CurrentUser["UserProfilerStarted"] = (object) false;
    HttpContextHelper.RegisterJavaScript($"window.open(\"{"./apiweb/telemetry/profilerData?action=exportResults"}\",\"{"Profiler"}\");");
  }

  [PXButton]
  [PXUIField(DisplayName = "Export Last")]
  protected void actionLastRequest()
  {
    throw new PXRedirectToUrlException("~/apiweb/telemetry/profilerData?action=exportLastRequest", "")
    {
      TargetFrame = "Profiler"
    };
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Request Profiler")]
  protected void actionProfiler()
  {
    PXRedirectToUrlException redirectToUrlException = new PXRedirectToUrlException(PXSiteMap.Provider.FindSiteMapNodeByScreenID("SM205070").Url, PXBaseRedirectException.WindowMode.New, "Filtered by user");
    if (redirectToUrlException != null)
    {
      PXBaseRedirectException.Filter filter = new PXBaseRedirectException.Filter("Samples", new List<PXFilterRow>()
      {
        new PXFilterRow("UserId", PXCondition.EQ, (object) PXAccess.GetUserName())
      }.ToArray());
      redirectToUrlException.Filters.Add(filter);
      throw redirectToUrlException;
    }
  }

  [Serializable]
  public class ProfilerInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "StartText", Enabled = false)]
    [PXString]
    public string StartText
    {
      get
      {
        return Messages.GetLocal("Turn on the request profiler. The subsequent requests will be stored and can be viewed or exported later.");
      }
    }

    [PXUIField(DisplayName = "ExportText:", Enabled = false)]
    [PXString]
    public string ExportText
    {
      get
      {
        return Messages.GetLocal("Export last requests for the current user's session even if the request profiler was not enabled before.");
      }
    }

    [PXUIField(DisplayName = "Started", Enabled = false)]
    [PXDate(UseTimeZone = false, DisplayMask = "g")]
    public System.DateTime? Started
    {
      get
      {
        string str = (string) null;
        try
        {
          str = PXAccess.GetUserName();
        }
        catch
        {
        }
        return !string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) && PXPerformanceMonitor.UserProfilerName.Equals(str, StringComparison.OrdinalIgnoreCase) ? new System.DateTime?(PXPerformanceMonitor.UserProfilerDate) : new System.DateTime?();
      }
    }

    [PXUIField(DisplayName = "Requests Logged", Enabled = false)]
    [PXInt]
    public int? RequestsLogged
    {
      get
      {
        string userName = (string) null;
        try
        {
          userName = PXAccess.GetUserName();
        }
        catch
        {
        }
        if (string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) || !PXPerformanceMonitor.UserProfilerName.Equals(userName, StringComparison.OrdinalIgnoreCase))
          return new int?();
        if (string.IsNullOrEmpty(userName))
          return new int?(0);
        System.DateTime date = PXPerformanceMonitor.UserProfilerDate;
        PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
        CmdScalar<int> cmdScalar = new CmdScalar<int>();
        YaqlScalarQuery yaqlScalarQuery = new YaqlScalarQuery((YaqlTable) new YaqlSchemaTable(typeof (SMPerformanceInfo).Name, (string) null), (List<YaqlJoin>) null);
        ((YaqlVectorQuery) yaqlScalarQuery).Column = new YaqlScalarAlilased(Yaql.count(Yaql.constant<int>(1, SqlDbType.Variant)), (string) null);
        ((YaqlQueryBase) yaqlScalarQuery).Condition = Yaql.and(Yaql.eq<string>((YaqlScalar) Yaql.column<SMPerformanceInfo.userId>((string) null), userName), Yaql.gte<System.DateTime>((YaqlScalar) Yaql.column<SMPerformanceInfo.requestStartTime>((string) null), date));
        ((CmdScalar) cmdScalar).Query = yaqlScalarQuery;
        int? requestsLogged = dbServicesPoint.executeScalar<int>(cmdScalar);
        if (PXPerformanceMonitor.Samples == null)
          return requestsLogged;
        int num1 = PXPerformanceMonitor.Samples.Where<PXPerformanceInfo>((System.Func<PXPerformanceInfo, bool>) (_ => !string.IsNullOrEmpty(_.UserId) && _.UserId.Equals(userName, StringComparison.OrdinalIgnoreCase) && _.StartTimeLocal >= date)).Count<PXPerformanceInfo>();
        int? nullable = requestsLogged;
        int num2 = num1;
        return !nullable.HasValue ? new int?() : new int?(nullable.GetValueOrDefault() + num2);
      }
    }

    public abstract class startText : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PerformanceMonitorUserMaint.ProfilerInfo.startText>
    {
    }

    public abstract class exportText : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PerformanceMonitorUserMaint.ProfilerInfo.exportText>
    {
    }

    public abstract class started : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PerformanceMonitorUserMaint.ProfilerInfo.started>
    {
    }

    public abstract class requestsLogged : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorUserMaint.ProfilerInfo.requestsLogged>
    {
    }
  }
}
