// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressWorksheetEntry_Workflow_CbApi_Adapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class ProgressWorksheetEntry_Workflow_CbApi_Adapter : PXGraphExtension<ProgressWorksheetEntry>
{
  public static bool IsActive() => true;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    if (!((PXGraph) this.Base).IsContractBasedAPI)
      return;
    // ISSUE: method pointer
    ((PXGraph) this.Base).RowUpdated.AddHandler<PMProgressWorksheet>(new PXRowUpdated((object) this, __methodptr(\u003CInitialize\u003Eg__RowUpdated\u007C1_0)));
  }
}
