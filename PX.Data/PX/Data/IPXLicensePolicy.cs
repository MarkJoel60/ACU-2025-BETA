// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXLicensePolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.LicensingService;
using PX.Licensing;
using PX.SM;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Data;

internal interface IPXLicensePolicy : IApiLicensePolicy
{
  void OnDataRowInserted(string tableName);

  void RegisterErpTransaction(PXGraph pxGraph, bool all);

  bool RegisterErpTransactionToPersistenceQueue(
    PXTransactionScope.ErpTranInfo erpTranInfo,
    out PXTransactionScope.ErpTranInfo storedErpTranInfo);

  void SetCurrentActionData(ActionData actionData);

  bool HasConstraintsViolations(PXLicense license);

  InstanceStatistics GetStatistics();

  LicenseCustInfo[] GetCustProjects();

  void RemoveSession(ILicensingSession session);

  IEnumerable<RowActiveUserInfo> GetCurrentApiUsers();

  void CheckApiUsersLimits(HttpContextBase httpContext);
}
