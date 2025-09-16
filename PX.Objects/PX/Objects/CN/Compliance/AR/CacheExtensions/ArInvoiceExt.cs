// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AR.CacheExtensions.ArInvoiceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Compliance.AR.CacheExtensions;

public sealed class ArInvoiceExt : PXCacheExtension<ARInvoice>
{
  [PXString]
  public string ClDisplayName
  {
    get
    {
      string docType = this.Base.DocType;
      if (docType != null && docType.Length == 3)
      {
        switch (docType[0])
        {
          case 'C':
            switch (docType)
            {
              case "CRM":
                return $"{"Credit Memo"}, {this.Base.RefNbr}";
              case "CSL":
                return $"{"Cash Sale"}, {this.Base.RefNbr}";
            }
            break;
          case 'D':
            if (docType == "DRM")
              return $"{"Debit Memo"}, {this.Base.RefNbr}";
            break;
          case 'F':
            if (docType == "FCH")
              return $"{"Overdue Charge"}, {this.Base.RefNbr}";
            break;
          case 'I':
            if (docType == "INV")
              return $"{"Invoice"}, {this.Base.RefNbr}";
            break;
          case 'R':
            if (docType == "RCS")
              return $"{"Cash Return"}, {this.Base.RefNbr}";
            break;
          case 'S':
            if (docType == "SMC")
              return $"{"Credit WO"}, {this.Base.RefNbr}";
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
