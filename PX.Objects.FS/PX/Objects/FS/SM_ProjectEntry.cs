// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ProjectEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class SM_ProjectEntry : PXGraphExtension<ProjectEntry>
{
  [PXHidden]
  public PXSelect<FSSetup> SetupRecord;
  [PXHidden]
  public PXSetup<PMSetup> PMSetupRecord;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();
}
