// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionEntry_Workflow_CbApi_Adapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting;

public class CostProjectionEntry_Workflow_CbApi_Adapter : PXGraphExtension<CostProjectionEntry>
{
  public static bool IsActive() => true;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    if (!((PXGraph) this.Base).IsContractBasedAPI)
      return;
    // ISSUE: method pointer
    ((PXGraph) this.Base).RowUpdated.AddHandler<PMCostProjection>(new PXRowUpdated((object) this, __methodptr(\u003CInitialize\u003Eg__RowUpdated\u007C1_0)));
  }
}
