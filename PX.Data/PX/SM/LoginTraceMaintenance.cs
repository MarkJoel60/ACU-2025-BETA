// Decompiled with JetBrains decompiler
// Type: PX.SM.LoginTraceMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable disable
namespace PX.SM;

public class LoginTraceMaintenance : PXGraph<LoginTraceMaintenance>
{
  private const int DeleteHistoryBatchSize = 500;
  public PXCancel<LoginTraceFilter> Cancel;
  public PXAction<LoginTraceFilter> PreviousPeriod;
  public PXAction<LoginTraceFilter> NextPeriod;
  public PXFilter<LoginTraceFilter> TraceFilter;
  public PXAction<LoginTraceFilter> DeleteHistory;
  public PXSelectReadonly3<LoginTrace, OrderBy<Desc<LoginTrace.date>>> LoginTraces;

  [InjectDependency]
  private IActiveDirectoryProvider _activeDirectoryProvider { get; set; }

  [InjectDependency]
  private IOptions<AuditJournalOptions> _auditJournalOptions { get; set; }

  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public IEnumerable previousperiod(PXAdapter adapter)
  {
    LoginTraceMaintenance graph = this;
    LoginTraceFilter filter = graph.TraceFilter.Current;
    Users next = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.username, Less<Current<LoginTraceFilter.username>>, And<Users.isHidden, Equal<False>>>, OrderBy<Desc<Users.username>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1);
    if (next == null)
    {
      next = (Users) PXSelectBase<Users, PXSelectOrderBy<Users, OrderBy<Desc<Users.username>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1);
      if (next == null)
        yield return (object) filter;
    }
    if (next != null)
      filter.Username = next.Username;
    yield return (object) filter;
  }

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public IEnumerable nextperiod(PXAdapter adapter)
  {
    LoginTraceMaintenance graph = this;
    LoginTraceFilter filter = graph.TraceFilter.Current;
    Users next = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.username, Greater<Current<LoginTraceFilter.username>>, And<Users.isHidden, Equal<False>>>, OrderBy<Asc<Users.username>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1);
    if (next == null)
    {
      next = (Users) PXSelectBase<Users, PXSelectOrderBy<Users, OrderBy<Asc<Users.username>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1);
      if (next == null)
        yield return (object) filter;
    }
    if (next != null)
      filter.Username = next.Username;
    yield return (object) filter;
  }

  [PXUIField(DisplayName = "Delete History", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  [PXButton(Tooltip = "Delete audit history.")]
  public IEnumerable deletehistory(PXAdapter adapter)
  {
    if (SitePolicy.AuditMonthsKeep != 0 && this.LoginTraces.View.Ask((string) null, (object) null, "Delete History", $"All audit history records older than {SitePolicy.AuditMonthsKeep} month(s) will be deleted. Are you sure?", MessageButtons.OKCancel, MessageIcon.None) == WebDialogResult.OK)
    {
      string applicationName = this._auditJournalOptions.Value.ApplicationName;
      System.DateTime date = System.DateTime.Today.AddMonths(-SitePolicy.AuditMonthsKeep);
      this.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => LoginTraceMaintenance.DeleteHistoryForPeriod(applicationName, date, cancellationToken)));
    }
    return adapter.Get();
  }

  private static void DeleteHistoryForPeriod(
    string applicationName,
    System.DateTime date,
    CancellationToken cancellationToken)
  {
    int num = PXDatabase.Select<LoginTrace>().Where<LoginTrace>((Expression<Func<LoginTrace, bool>>) (t => t.ApplicationName == applicationName && t.Date < (System.DateTime?) date)).Count<LoginTrace>() / 500 + 1;
    CmdRepeatableDelete repeatableDelete1 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("LoginTrace"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete1).Condition = Yaql.and(Yaql.eq<string>((YaqlScalar) Yaql.column<LoginTrace.applicationName>((string) null), applicationName), Yaql.lt<System.DateTime>((YaqlScalar) Yaql.column<LoginTrace.date>((string) null), date));
    ((CmdDelete) repeatableDelete1).LimitRows = 500;
    repeatableDelete1.RepeatCount = num;
    CmdRepeatableDelete repeatableDelete2 = repeatableDelete1;
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null)
    {
      TimeoutMultiplier = 10,
      CancellationToken = cancellationToken
    };
    PXDatabase.Provider.CreateDbServicesPoint().executeSingleCommand((CommandBase) repeatableDelete2, executionContext, false);
  }

  protected void LoginTraceFilter_UserName_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected IEnumerable loginTraces()
  {
    LoginTraceFilter current = this.TraceFilter.Current;
    BqlCommand select = (BqlCommand) new PX.Data.Select<LoginTrace, Where<LoginTrace.applicationName, Equal<Required<LoginTrace.applicationName>>>, OrderBy<Desc<LoginTrace.date>>>();
    ArrayList arrayList = new ArrayList()
    {
      (object) this._auditJournalOptions.Value.ApplicationName
    };
    if (current.Username != null)
    {
      List<string> c = new List<string>();
      string username = current.Username;
      char[] separator = new char[1]{ ';' };
      foreach (string str1 in username.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        int length = str1.IndexOf('\\');
        if (length > -1 && length < str1.Length - 1 && str1.Substring(0, length) == this._activeDirectoryProvider.DefaultDomainComponent)
        {
          string str2 = str1.Substring(length + 1);
          c.Add(str2);
        }
        c.Add(str1);
      }
      select = select.WhereAnd(InHelper<LoginTrace.username>.Create(c.Count));
      arrayList.AddRange((ICollection) c);
    }
    System.DateTime? nullable = current.DateFrom;
    if (nullable.HasValue)
      select = select.WhereAnd<Where<LoginTrace.date, GreaterEqual<Current<LoginTraceFilter.dateFrom>>>>();
    nullable = current.DateTo;
    if (nullable.HasValue)
      select = select.WhereAnd<Where<LoginTrace.date, LessEqual<Current<LoginTraceFilter.dateTo>>>>();
    if (current.Operation.HasValue)
      select = select.WhereAnd<Where<LoginTrace.operation, Equal<Current<LoginTraceFilter.operation>>>>();
    if ((16 /*0x10*/ & SitePolicy.AuditOperationMask) == 0)
    {
      select = select.WhereAnd<Where<LoginTrace.operation, NotEqual<Required<LoginTrace.operation>>>>();
      arrayList.Add((object) PXAuditJournal.Operation.AccessScreen);
    }
    PXView pxView = new PXView((PXGraph) this, true, select);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] array = arrayList.ToArray();
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select((object[]) null, array, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }
}
