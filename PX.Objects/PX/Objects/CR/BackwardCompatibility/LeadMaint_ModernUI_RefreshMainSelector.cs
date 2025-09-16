// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.LeadMaint_ModernUI_RefreshMainSelector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.LeadMaint_Extensions;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
public class LeadMaint_ModernUI_RefreshMainSelector : 
  PXGraphExtension<LeadMaint_LinkContactExt, LeadMaint>
{
  protected virtual void _(Events.RowPersisting<CRLead> e, PXRowPersisting del)
  {
    e.Row.MemberName = !string.IsNullOrEmpty(e.Row.DisplayName) ? e.Row.DisplayName : e.Row.FullName;
    del?.Invoke(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRLead>>) e).Cache, ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRLead>>) e).Args);
  }
}
