// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiNotificationTemplateMaintenanceNoRefresh
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiNotificationTemplateMaintenanceNoRefresh : WikiNotificationTemplateMaintenance
{
  public WikiNotificationTemplateMaintenanceNoRefresh()
  {
    this.Actions["Cancel"] = (PXAction) new PXAction<WikiNotificationTemplate>((PXGraph) this, (Delegate) new PXButtonDelegate(this.Handler));
  }

  [PXSuppressActionValidation]
  [PXCancelButton(ShortcutChar = '\0')]
  private IEnumerable Handler(PXAdapter adapter)
  {
    yield break;
  }
}
