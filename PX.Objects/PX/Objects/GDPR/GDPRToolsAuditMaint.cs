// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRToolsAuditMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Process;
using System.Collections;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPRToolsAuditMaint : PXGraph<GDPRToolsAuditMaint>
{
  public PXSelectOrderBy<SMPersonalDataLog, OrderBy<Desc<SMPersonalDataLog.createdDateTime>>> Log;
  public PXCancel<SMPersonalDataLog> Cancel;
  public PXAction<SMPersonalDataLog> OpenContact;

  [PXButton]
  [PXUIField(DisplayName = "Open Contact", Visible = false)]
  public virtual IEnumerable openContact(PXAdapter adapter)
  {
    if (!(((PXGraph) this).Caches[typeof (SMPersonalDataLog)].Current is SMPersonalDataLog current))
      return adapter.Get();
    new EntityHelper((PXGraph) this).NavigateToRow(current.TableName, (object[]) current.CombinedKey.Split(PXAuditHelper.SEPARATOR), (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  [PXIntList(new int[] {0, 1, 3}, new string[] {"Restored", "Pseudonymized", "Erased"})]
  [PXUIField(DisplayName = "Status", Visible = true)]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<SMPersonalDataLog.pseudonymizationStatus> e)
  {
  }
}
