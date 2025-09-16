// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentType : APDocType
{
  protected static readonly Dictionary<string, string> VoidingTypes = new Dictionary<string, string>()
  {
    {
      "CHK",
      "VCK"
    },
    {
      "PPM",
      "VCK"
    },
    {
      "QCK",
      "VQC"
    },
    {
      "REF",
      "VRF"
    }
  };
  protected static readonly Dictionary<string, HashSet<string>> VoidedTypes = APPaymentType.VoidingTypes.ReverseDictionary<string>();
  public static readonly HashSet<string> AllVoidingTypes = new HashSet<string>()
  {
    "VCK",
    "VQC",
    "VRF"
  };

  public static bool VoidAppl(string DocType) => DocType == "VCK" || DocType == "VRF";

  public static bool VoidEnabled(string docType)
  {
    return docType == "CHK" || docType == "PPM" || docType == "REF";
  }

  public static bool CanHaveBalance(string DocType)
  {
    return DocType == "ADR" || DocType == "PPM" || DocType == "PPI" || DocType == "QCK" || DocType == "VQC" || DocType == "RQC" || DocType == "VCK";
  }

  public static string DrCr(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          if (DocType == "VCK" || DocType == "QCK")
            break;
          goto label_11;
        case 'D':
          if (DocType == "ADR")
            break;
          goto label_11;
        case 'E':
          if (DocType == "REF")
            goto label_10;
          goto label_11;
        case 'H':
          if (DocType == "CHK")
            break;
          goto label_11;
        case 'P':
          if (DocType == "PPM")
            break;
          goto label_11;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_10;
          goto label_11;
        case 'R':
          if (DocType == "VRF")
            goto label_10;
          goto label_11;
        default:
          goto label_11;
      }
      return "C";
label_10:
      return "D";
    }
label_11:
    return (string) null;
  }

  public static string[] GetVoidedAPDocType(string docType)
  {
    return APPaymentType.VoidedTypes.ContainsKey(docType) ? APPaymentType.VoidedTypes[docType].ToArray<string>() : new string[0];
  }

  public static string GetVoidingAPDocType(string docType)
  {
    string voidingApDocType = (string) null;
    if (docType != null)
      APPaymentType.VoidingTypes.TryGetValue(docType, out voidingApDocType);
    return voidingApDocType;
  }

  /// <summary>
  /// Specialized selector for APPayment RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// APPayment.refNbr,APPayment.docDate, APPayment.finPeriodID, APPayment.vendorID,<br />
  /// APPayment.vendorID_Vendor_acctName, APPayment.vendorLocationID, APPayment.curyID,<br />
  /// APPayment.curyOrigDocAmt, APPayment.curyDocBal, APPayment.status, <br />
  /// APPayment.cashAccountID, APPayment.paymentMethodID, APPayment.extRefNbr <br />
  /// </summary>
  public class RefNbrAttribute(System.Type SearchType) : PXSelectorAttribute(SearchType, typeof (APRegister.refNbr), typeof (APPayment.extRefNbr), typeof (APRegister.docDate), typeof (APRegister.finPeriodID), typeof (APRegister.vendorID), typeof (APRegister.vendorID_Vendor_acctName), typeof (APRegister.vendorLocationID), typeof (APRegister.curyID), typeof (APRegister.curyOrigDocAmt), typeof (APRegister.curyDocBal), typeof (APRegister.status), typeof (APPayment.cashAccountID), typeof (APPayment.paymentMethodID), typeof (APInvoice.invoiceNbr), typeof (APRegister.docDesc))
  {
  }

  public class AdjgRefNbrAttribute(System.Type SearchType) : PXSelectorAttribute(SearchType, typeof (APPayment.refNbr), typeof (APPayment.docDate), typeof (APPayment.finPeriodID), typeof (APPayment.vendorID), typeof (APPayment.vendorLocationID), typeof (APPayment.curyID), typeof (APPayment.curyOrigDocAmt), typeof (APPayment.curyDocBal), typeof (APPayment.status), typeof (APPayment.cashAccountID), typeof (APPayment.paymentMethodID), typeof (APPayment.extRefNbr), typeof (APPayment.docDesc))
  {
  }

  /// <summary>
  /// Specialized for APPayments version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AP Payment. <br />
  /// References APPayment.docType and APPayment.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AP Setup and APPayment types:<br />
  /// namely - APSetup.checkNumberingID for all the types<br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (APPayment.docType), typeof (APPayment.docDate), APPaymentType.NumberingAttribute._DocTypes, APPaymentType.NumberingAttribute._SetupFields)
    {
    }

    private static string[] _DocTypes
    {
      get
      {
        return new string[7]
        {
          "CHK",
          "ADR",
          "PPM",
          "REF",
          "VRF",
          "VCK",
          "PPI"
        };
      }
    }

    private static System.Type[] _SetupFields
    {
      get
      {
        return new System.Type[7]
        {
          typeof (APSetup.checkNumberingID),
          null,
          typeof (APSetup.checkNumberingID),
          typeof (APSetup.checkNumberingID),
          null,
          null,
          null
        };
      }
    }

    public static System.Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, System.Type> tuple in EnumerableExtensions.Zip<string, System.Type>((IEnumerable<string>) APPaymentType.NumberingAttribute._DocTypes, (IEnumerable<System.Type>) APPaymentType.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == docType)
          return tuple.Item2;
      }
      return (System.Type) null;
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[7]
      {
        "CHK",
        "ADR",
        "PPM",
        "REF",
        "VRF",
        "VCK",
        "PPI"
      }, new string[7]
      {
        "Payment",
        "Debit Adj.",
        "Prepayment",
        "Refund",
        "Voided Refund",
        "Voided Payment",
        "Prepmt. Invoice"
      })
    {
    }
  }
}
