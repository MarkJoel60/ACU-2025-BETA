// Decompiled with JetBrains decompiler
// Type: PX.Data.Hosting.RegularOperations.DeletedRecordsHistoryClearingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using PX.Common;
using PX.Data.DeletedRecordsTracking.DAC;
using PX.Data.DependencyInjection;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model;
using PX.DbServices.Points;
using PX.DbServices.QueryObjectModel;
using PX.Hosting;
using PX.Translation;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Data.Hosting.RegularOperations;

internal class DeletedRecordsHistoryClearingService : IBackgroundHostedService
{
  private readonly ILogger _logger;
  private readonly ILoginService _loginService;
  private const string OperationName = "Clearing the history of deleted entries";

  private TimeSpan OperationDelay => TimeSpan.FromHours(1.0);

  public DeletedRecordsHistoryClearingService(ILogger logger, ILoginService loginService)
  {
    this._logger = logger;
    this._loginService = loginService;
  }

  private Task ActionAsync(CancellationToken token)
  {
    System.DateTime dateTime = System.DateTime.UtcNow.AddDays((double) -((int?) PXDatabase.SelectSingle<ODataPreferences>((PXDataField) new PXDataField<ODataPreferences.daysRetentionHistoryDeletedRecords>())?.GetInt32(0) ?? 10));
    CmdDeleteAll cmdDeleteAll = new CmdDeleteAll(new YaqlSchemaTable("SMDeletedRecordsTrackingHistory", (string) null), Yaql.and((YaqlCondition) Yaql.companyIdEq(PXDatabase.Provider.getCompanyID(typeof (LocalizationResource).Name, out companySetting _), (List<CompanyHeader>) null, false, (string) null, false), Yaql.lt<System.DateTime>((YaqlScalar) Yaql.column<SMDeletedRecordsTrackingHistory.deleteDate>((string) null), dateTime)), 1000, (List<YaqlJoin>) null);
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null)
    {
      TimeoutMultiplier = 60,
      CancellationToken = token
    };
    PXDatabase.Provider.CreateDbServicesPoint().executeSingleCommand((CommandBase) cmdDeleteAll, executionContext, false);
    return Task.CompletedTask;
  }

  async Task IBackgroundHostedService.ExecuteAsync(CancellationToken token)
  {
    while (!token.IsCancellationRequested)
    {
      System.DateTime nextRunDateTime = System.DateTime.UtcNow.Add(this.OperationDelay);
      using (LogContext.PushProperty("BackgroundOperationId", (object) Guid.NewGuid(), false))
      {
        using (Operation op = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 2, new LogEventLevel?((LogEventLevel) 4)).Begin("Running regular operation {OperationName}", new object[1]
        {
          (object) "Clearing the history of deleted entries"
        }))
        {
          try
          {
            using (LifetimeScopeHelper.BeginLifetimeScope())
              await this._loginService.ExecuteAsAdminForAllTenantsAsync((Func<CancellationToken, Task>) (async _ =>
              {
                using (Operation innerOp = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?((LogEventLevel) 4)).Begin("Running regular operation {OperationName} on tenant {TenantName}", new object[2]
                {
                  (object) "Clearing the history of deleted entries",
                  (object) this._loginService.GetCurrentCompany()
                }))
                {
                  if (token.IsCancellationRequested)
                    return;
                  try
                  {
                    await this.ActionAsync(token);
                    innerOp.Complete();
                  }
                  catch (Exception ex)
                  {
                    OperationExtensions.Abandon(innerOp, ex);
                  }
                }
              }), token);
          }
          catch (Exception ex)
          {
            OperationExtensions.Abandon(op, ex);
          }
          finally
          {
            PXContext.ClearAllSlots();
          }
          op.Complete();
        }
      }
      this._logger.Verbose<string, System.DateTime>("Regular operation {OperationName} has completed. The next run is expected at {NextRunDateTime}", "Clearing the history of deleted entries", nextRunDateTime);
      TimeSpan timeSpan = nextRunDateTime - System.DateTime.UtcNow;
      await Task.Delay(timeSpan.TotalMilliseconds > 0.0 ? timeSpan : TimeSpan.Zero, token);
    }
  }
}
