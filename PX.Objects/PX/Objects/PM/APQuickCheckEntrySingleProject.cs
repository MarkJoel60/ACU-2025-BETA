// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APQuickCheckEntrySingleProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;

#nullable enable
namespace PX.Objects.PM;

public class APQuickCheckEntrySingleProject : 
  SingleProjectExtension<APQuickCheckEntry, APQuickCheck, PX.Objects.AP.APRegister.projectID, APQuickCheck.hasMultipleProjects, APRegisterSingleProject, APTran, APTran.projectID>
{
  public static bool IsActive()
  {
    return SingleProjectExtension<APQuickCheckEntry, APQuickCheck, PX.Objects.AP.APRegister.projectID, APQuickCheck.hasMultipleProjects, APRegisterSingleProject, APTran, APTran.projectID>.IsExtensionEnabled();
  }

  protected override PXSelectBase<APQuickCheck> Document
  {
    get => (PXSelectBase<APQuickCheck>) this.Base.Document;
  }

  protected override PXSelectBase<APTran> Details => (PXSelectBase<APTran>) this.Base.Transactions;

  protected override bool IsDetailLineIgnored(APTran? detail) => detail?.LineType == "DS";
}
