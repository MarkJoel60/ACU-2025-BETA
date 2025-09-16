// Decompiled with JetBrains decompiler
// Type: PX.Data.DummyLicensePolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.LicensingService;
using PX.Licensing;
using PX.SM;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Data;

internal class DummyLicensePolicy : IPXLicensePolicy, IApiLicensePolicy
{
  public void OnDataRowInserted(string tableName)
  {
  }

  public void RegisterErpTransaction(PXGraph pxGraph, bool all)
  {
  }

  public bool HasConstraintsViolations(PXLicense license) => false;

  public InstanceStatistics GetStatistics()
  {
    return new InstanceStatistics()
    {
      Statistics = new DailyStatistic[0],
      CommerceTransactions = new DailyCommerceTran[0],
      Constraints = new LicenseConstraint[0],
      Violations = new LicenseViolation[0]
    };
  }

  public LicenseCustInfo[] GetCustProjects() => (LicenseCustInfo[]) null;

  public void RemoveSession(ILicensingSession session)
  {
  }

  public IEnumerable<RowActiveUserInfo> GetCurrentApiUsers()
  {
    return (IEnumerable<RowActiveUserInfo>) new RowActiveUserInfo[0];
  }

  public void CheckApiUsersLimits(HttpContextBase httpContext)
  {
  }

  Task IApiLicensePolicy.ApplyRequestThrottling(HttpContextBase httpContext) => Task.CompletedTask;

  Task IApiLicensePolicy.CheckUsersLimits(HttpContextBase httpContext) => Task.CompletedTask;

  public bool RegisterErpTransactionToPersistenceQueue(
    PXTransactionScope.ErpTranInfo erpTranInfo,
    out PXTransactionScope.ErpTranInfo storedErpTranInfo)
  {
    storedErpTranInfo = new PXTransactionScope.ErpTranInfo();
    return false;
  }

  void IPXLicensePolicy.SetCurrentActionData(ActionData actionData)
  {
  }
}
