// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditExplore
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

[Serializable]
public class AUAuditExplore : PXGraph<AUAuditExplore>
{
  public PXSelectRedirect<AUAuditSetup, AUAuditSetup> Audit;

  [PXUIField(DisplayName = "Audited Screen Name")]
  [PXString]
  protected virtual void AUAuditSetup_ScreenName_CacheAttached(PXCache sender)
  {
  }

  protected virtual void AUAuditSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AUAuditSetup row = (AUAuditSetup) e.Row;
    if (row == null || !string.IsNullOrEmpty(row.ScreenName) || PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.ScreenID) != null)
      return;
    sender.RaiseExceptionHandling<AUAuditSetup.screenID>((object) row, (object) row.ScreenID, (Exception) new PXSetPropertyException<AUAuditSetup.screenID>("This form cannot be found in the site map.", PXErrorLevel.Error));
  }
}
