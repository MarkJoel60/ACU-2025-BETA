// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PO.CacheExtensions.PoOrderExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.CN.Compliance.PO.CacheExtensions;

public sealed class PoOrderExt : PXCacheExtension<POOrder>
{
  [PXString]
  public string ClDisplayName
  {
    get
    {
      switch (this.Base.OrderType)
      {
        case "RO":
          return $"{"Normal"}, {this.Base.OrderNbr}";
        case "RS":
          return this.Base.OrderNbr;
        case "DP":
          return $"{"Drop-Ship"}, {this.Base.OrderNbr}";
        case "BL":
          return $"{"Blanket"}, {this.Base.OrderNbr}";
        case "SB":
          return $"{"Standard"}, {this.Base.OrderNbr}";
        default:
          return $"{this.Base.OrderType}, {this.Base.OrderNbr}";
      }
    }
    set
    {
    }
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class clDisplayName : IBqlField, IBqlOperand
  {
  }
}
