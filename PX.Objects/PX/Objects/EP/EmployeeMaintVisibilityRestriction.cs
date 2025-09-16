// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.EP;

public class EmployeeMaintVisibilityRestriction : PXGraphExtension<EmployeeMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [Branch(null, null, true, true, false)]
  public void Location_VBranchID_CacheAttached(PXCache sender)
  {
  }
}
