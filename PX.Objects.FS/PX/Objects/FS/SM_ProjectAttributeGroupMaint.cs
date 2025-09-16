// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ProjectAttributeGroupMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class SM_ProjectAttributeGroupMaint : PXGraphExtension<ProjectAttributeGroupMaint>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.routeManagementModule>();
  }

  [PXOverride]
  public string getEntityName(string classid)
  {
    return classid == "Service Contract" ? typeof (FSServiceContract).FullName : ProjectAttributeGroupMaint.getEntityNameStatic(classid);
  }
}
