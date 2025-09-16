// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.CacheExtensions.AddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;

public sealed class AddressExt : PXCacheExtension<Address>
{
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "County")]
  public string County { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class county : IBqlField, IBqlOperand
  {
  }
}
