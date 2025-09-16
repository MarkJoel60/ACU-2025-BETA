// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.CacheExtensions.ContractExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.CT;

#nullable disable
namespace PX.Objects.CN.CacheExtensions;

public sealed class ContractExt : PXCacheExtension<Contract>
{
  [PXDBBool]
  [PXUIField(DisplayName = "Allow Adding New Items on the Fly")]
  public bool? AllowNonProjectAccountGroups { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class allowNonProjectAccountGroups : IBqlField, IBqlOperand
  {
  }
}
