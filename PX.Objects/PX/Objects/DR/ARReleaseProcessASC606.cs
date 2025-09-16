// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ARReleaseProcessASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class ARReleaseProcessASC606 : PXGraphExtension<ARReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  [PXOverride]
  public virtual DRProcess CreateDRProcess(
    ARReleaseProcessASC606.CreateDRProcessDelegate baseMethod)
  {
    return (DRProcess) PXGraph.CreateInstance<DRSingleProcess>();
  }

  public delegate DRProcess CreateDRProcessDelegate();
}
