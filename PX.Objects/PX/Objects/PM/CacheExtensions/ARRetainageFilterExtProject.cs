// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.ARRetainageFilterExtProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

/// <summary>
/// Extension that activates Project-related functionality for the <see cref="T:PX.Objects.AR.ARRetainageFilter" /> when Project accounting module is enabled.
/// </summary>
public sealed class ARRetainageFilterExtProject : PXCacheExtension<
#nullable disable
ARRetainageFilter>
{
  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the extension is active.
  /// </summary>
  public static bool IsActive()
  {
    return !PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.ProjectID" />
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (NonProjectBaseAttribute))]
  [ActiveProjectBase(typeof (ARRetainageFilter.customerID), FieldClass = "PROJECT")]
  public int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.ProjectID" />
  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRetainageFilterExtProject.projectID>
  {
  }
}
