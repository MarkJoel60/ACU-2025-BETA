// Decompiled with JetBrains decompiler
// Type: PX.Data.SMLongOperationMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Process.Automation;

#nullable disable
namespace PX.Data;

[PXHidden]
[PXDisableWorkflow]
public class SMLongOperationMaint : PXGraph<SMLongOperationMaint>
{
  public PXSelect<SMLongOperation> Operations;

  public static void ClearAbortedProcesses(string websiteid)
  {
    if (!PXDatabase.Provider.SchemaCache.TableExists(typeof (SMLongOperation).Name))
      return;
    SMLongOperationMaint.ClearOperationsForWebsiteId(websiteid);
  }

  private static void ClearOperationsForWebsiteId(string websiteId)
  {
    PXDatabase.Delete<SMLongOperation>((PXDataFieldRestrict) new PXDataFieldRestrict<SMLongOperation.websiteID>((object) websiteId));
  }

  internal static void ClearOperationsForThisWebsite()
  {
    SMLongOperationMaint.ClearOperationsForWebsiteId(WebsiteID.Key);
  }

  internal static void DeleteOperationByKey(object key)
  {
    PXDatabase.Delete<SMLongOperation>((PXDataFieldRestrict) new PXDataFieldRestrict<SMLongOperation.operationKey>((object) key.ToString()));
  }
}
