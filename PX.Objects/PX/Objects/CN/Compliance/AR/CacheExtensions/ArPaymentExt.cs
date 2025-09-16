// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AR.CacheExtensions.ArPaymentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Compliance.AR.CacheExtensions;

public sealed class ArPaymentExt : PXCacheExtension<ARPayment>
{
  [PXString]
  public string ClDisplayName
  {
    get
    {
      string docType = this.Base.DocType;
      if (docType != null && docType.Length == 3)
      {
        switch (docType[2])
        {
          case 'B':
            if (docType == "SMB")
              return $"{"Balance WO"}, {this.Base.RefNbr}";
            break;
          case 'F':
            switch (docType)
            {
              case "REF":
                return $"{"Refund"}, {this.Base.RefNbr}";
              case "VRF":
                return $"{"Voided Refund"}, {this.Base.RefNbr}";
            }
            break;
          case 'L':
            if (docType == "CSL")
              return $"{"Cash Sale"}, {this.Base.RefNbr}";
            break;
          case 'M':
            switch (docType)
            {
              case "CRM":
                return $"{"Credit Memo"}, {this.Base.RefNbr}";
              case "PPM":
                return $"{"Prepayment"}, {this.Base.RefNbr}";
              case "RPM":
                return $"{"Voided Payment"}, {this.Base.RefNbr}";
            }
            break;
          case 'S':
            if (docType == "RCS")
              return $"{"Cash Return"}, {this.Base.RefNbr}";
            break;
          case 'T':
            if (docType == "PMT")
              return $"{"Payment"}, {this.Base.RefNbr}";
            break;
        }
      }
      return $"{this.Base.DocType}, {this.Base.RefNbr}";
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
