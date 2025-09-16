// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.CacheExtensions.ApPaymentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Compliance.AP.CacheExtensions;

public sealed class ApPaymentExt : PXCacheExtension<APPayment>
{
  [PXString]
  public string ClDisplayName
  {
    get
    {
      switch (this.Base.DocType)
      {
        case "CHK":
          return $"{"Payment"}, {this.Base.RefNbr}";
        case "ADR":
          return $"{"Debit Adj."}, {this.Base.RefNbr}";
        case "PPM":
          return $"{"Prepayment"}, {this.Base.RefNbr}";
        case "REF":
          return $"{"Refund"}, {this.Base.RefNbr}";
        case "VRF":
          return $"{"Voided Refund"}, {this.Base.RefNbr}";
        case "VCK":
          return $"{"Voided Payment"}, {this.Base.RefNbr}";
        default:
          return $"{this.Base.DocType}, {this.Base.RefNbr}";
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
