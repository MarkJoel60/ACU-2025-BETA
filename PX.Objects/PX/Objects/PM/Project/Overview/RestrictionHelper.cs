// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.RestrictionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

public static class RestrictionHelper
{
  public static Restriction CreateByScreenAccessRights(string screenID)
  {
    return new Restriction()
    {
      Key = screenID,
      Disabled = new bool?(!PXAccess.VerifyRights(screenID))
    };
  }

  public static Restriction CreateByScreenAccessRights<FeatureType>(string screenID) where FeatureType : IBqlField
  {
    return new Restriction()
    {
      Key = screenID,
      Disabled = new bool?(!PXAccess.FeatureInstalled<FeatureType>() || !PXAccess.VerifyRights(screenID))
    };
  }
}
