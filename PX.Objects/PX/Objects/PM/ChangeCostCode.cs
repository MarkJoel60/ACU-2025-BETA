// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeCostCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class ChangeCostCode : PXChangeID<PMCostCode, PMCostCode.costCodeCD>
{
  public ChangeCostCode(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public ChangeCostCode(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual IEnumerable Handler(PXAdapter adapter)
  {
    if (((PXAction) this).Graph.Views["ChangeIDDialog"].Answer == null)
      ((PXAction) this).Graph.Views["ChangeIDDialog"].Cache.Clear();
    if (adapter.View.Cache.Current != null && adapter.View.Cache.GetStatus(adapter.View.Cache.Current) != 2)
    {
      WebDialogResult webDialogResult = adapter.View.Cache.Graph.Views["ChangeIDDialog"].AskExt();
      string newCd;
      if ((webDialogResult == 1 || webDialogResult == 6 && ((PXAction) this).Graph.IsExport) && !string.IsNullOrWhiteSpace(newCd = PXChangeID<PMCostCode, PMCostCode.costCodeCD>.GetNewCD(adapter)))
        PXChangeID<PMCostCode, PMCostCode.costCodeCD>.ChangeCD(adapter.View.Cache, PXChangeID<PMCostCode, PMCostCode.costCodeCD>.GetOldCD(adapter), newCd);
    }
    if (((PXAction) this).Graph.IsContractBasedAPI)
      ((PXAction) this).Graph.Actions.PressSave();
    return adapter.Get();
  }

  protected virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    this.DuplicatedKeyMessage = "The cost code with the {0} identifier already exists. Specify another cost code ID.";
    base.FieldVerifying(sender, e);
  }
}
