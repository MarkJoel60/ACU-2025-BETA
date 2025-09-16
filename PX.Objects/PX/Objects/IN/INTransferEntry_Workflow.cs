// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Objects.IN;

public class INTransferEntry_Workflow : 
  INRegisterEntryBase_Workflow<INTransferEntry, INDocType.transfer>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    INRegisterEntryBase_Workflow<INTransferEntry, INDocType.transfer>.ConfigureCommon(config.GetScreenConfigurationContext<INTransferEntry, INRegister>());
  }
}
