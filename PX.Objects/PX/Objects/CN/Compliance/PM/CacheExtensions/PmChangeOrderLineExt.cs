// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.CacheExtensions.PmChangeOrderLineExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.CacheExtensions;

public sealed class PmChangeOrderLineExt : PXCacheExtension<PMChangeOrderLine>
{
  [PXBool]
  [PXUIField(DisplayName = "Expired Compliance", Visible = false, IsReadOnly = true)]
  public bool? HasExpiredComplianceDocuments { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class hasExpiredComplianceDocuments : IBqlField, IBqlOperand
  {
  }
}
