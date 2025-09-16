// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployeeVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;

#nullable disable
namespace PX.Objects.EP;

public sealed class EPEmployeeVisibilityRestriction : PXCacheExtension<EPEmployee>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Employee Class", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<EPEmployeeClass.vendorClassID, Where<VendorClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  public string VendorClassID { get; set; }
}
