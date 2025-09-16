// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.INSiteStatusFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

/// <summary>
/// DAC extension of INSiteStatusFilter to add additional attributes
/// </summary>
public sealed class INSiteStatusFilterExt : PXCacheExtension<
#nullable disable
INSiteStatusFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  /// <summary>Show Project Information</summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Project Information")]
  public bool? ProjectInformation { get; set; }

  public abstract class projectInformation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSiteStatusFilterExt.projectInformation>
  {
  }
}
