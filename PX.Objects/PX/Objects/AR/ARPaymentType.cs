// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentType
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
namespace PX.Objects.AR;

public class ARPaymentType : ARDocType
{
  protected static readonly Dictionary<string, string> VoidingTypes = new Dictionary<string, string>()
  {
    {
      "PMT",
      "RPM"
    },
    {
      "CSL",
      (string) null
    },
    {
      "PPM",
      "RPM"
    },
    {
      "REF",
      "VRF"
    }
  };
  protected static readonly Dictionary<string, HashSet<string>> VoidedTypes = ARPaymentType.VoidingTypes.ReverseDictionary<string>();
  public static readonly HashSet<string> AllVoidingTypes = new HashSet<string>()
  {
    "RPM",
    "VRF"
  };

  public static bool VoidAppl(string DocType) => DocType == "RPM" || DocType == "VRF";

  public static bool VoidEnabled(ARPayment payment)
  {
    return payment.DocType == "PMT" || payment.DocType == "PPM" || payment.DocType == "REF" || payment.SelfVoidingDoc.GetValueOrDefault();
  }

  public static bool CanHaveBalance(string DocType)
  {
    return DocType == "CRM" || DocType == "PMT" || DocType == "RPM" || DocType == "PPM" || DocType == "PPI" || DocType == "REF" || DocType == "VRF";
  }

  public static string DrCr(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            break;
          goto label_10;
        case 'F':
          if (DocType == "REF" || DocType == "VRF")
            goto label_9;
          goto label_10;
        case 'L':
          if (DocType == "CSL")
            break;
          goto label_10;
        case 'M':
          if (DocType == "RPM" || DocType == "CRM" || DocType == "PPM")
            break;
          goto label_10;
        case 'S':
          if (DocType == "RCS")
            goto label_9;
          goto label_10;
        case 'T':
          if (DocType == "PMT")
            break;
          goto label_10;
        default:
          goto label_10;
      }
      return "D";
label_9:
      return "C";
    }
label_10:
    return (string) null;
  }

  public static string[] GetVoidedARDocType(string docType)
  {
    return ARPaymentType.VoidedTypes.ContainsKey(docType) ? ARPaymentType.VoidedTypes[docType].ToArray<string>() : new string[0];
  }

  public static string GetVoidingARDocType(string docType)
  {
    string voidingArDocType = (string) null;
    if (docType != null)
      ARPaymentType.VoidingTypes.TryGetValue(docType, out voidingArDocType);
    return voidingArDocType;
  }

  /// <summary>
  /// Specialized selector for ARPayment RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// ARPayment.refNbr,ARPayment.docDate, ARPayment.finPeriodID,<br />
  /// ARPayment.customerID, ARPayment.customerID_Customer_acctName,<br />
  /// ARPayment.customerLocationID, ARPayment.curyID, ARPayment.curyOrigDocAmt,<br />
  /// ARPayment.curyDocBal,ARPayment.status, ARPayment.cashAccountID, ARPayment.pMInstanceID, ARPayment.extRefNbr<br />
  /// </summary>
  public class RefNbrAttribute : PXSelectorAttribute
  {
    /// <summary>Ctor</summary>
    /// <param name="SearchType">Must be IBqlSearch, returning ARPayment.refNbr</param>
    public RefNbrAttribute(Type SearchType)
      : base(SearchType, new Type[13]
      {
        typeof (ARRegister.refNbr),
        typeof (ARPayment.extRefNbr),
        typeof (ARRegister.docDate),
        typeof (ARRegister.finPeriodID),
        typeof (ARRegister.customerID),
        typeof (ARRegister.customerID_Customer_acctName),
        typeof (ARRegister.customerLocationID),
        typeof (ARRegister.curyID),
        typeof (ARRegister.curyOrigDocAmt),
        typeof (ARRegister.curyDocBal),
        typeof (ARRegister.status),
        typeof (ARPayment.cashAccountID),
        typeof (ARPayment.pMInstanceID_CustomerPaymentMethod_descr)
      })
    {
    }

    public RefNbrAttribute(Type SearchType, params Type[] fieldList)
      : base(SearchType, fieldList)
    {
    }
  }

  /// <summary>
  /// Specialized selector for ARPayment RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// ARPayment.refNbr,ARPayment.docDate, ARPayment.finPeriodID,<br />
  /// ARPayment.customerID, ARPayment.customerID_Customer_acctName,<br />
  /// ARPayment.customerLocationID, ARPayment.curyID, ARPayment.curyOrigDocAmt,<br />
  /// ARPayment.curyDocBal,ARPayment.status, ARPayment.cashAccountID, ARPayment.pMInstanceID, ARPayment.extRefNbr<br />
  /// </summary>
  /// <summary>Ctor</summary>
  /// <param name="SearchType">Must be IBqlSearch, returning ARPayment.refNbr</param>
  public class AdjgRefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[13]
  {
    typeof (ARPayment.refNbr),
    typeof (ARPayment.docDate),
    typeof (ARPayment.finPeriodID),
    typeof (ARPayment.customerID),
    typeof (ARPayment.customerLocationID),
    typeof (ARPayment.curyID),
    typeof (ARPayment.curyOrigDocAmt),
    typeof (ARPayment.curyDocBal),
    typeof (ARPayment.status),
    typeof (ARPayment.cashAccountID),
    typeof (ARPayment.pMInstanceID),
    typeof (ARPayment.extRefNbr),
    typeof (ARRegister.docDesc)
  })
  {
  }

  /// <summary>
  /// Specialized for ARPayments version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AR Payment. <br />
  /// References ARInvoice.docType and ARInvoice.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AR Setup and ARInvoice types:<br />
  /// namely ARSetup.paymentNumberingID - for ARPayment, ARPrepayment, AR Refund
  /// and null for others.
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (ARPayment.docType), typeof (ARPayment.docDate), ARPaymentType.NumberingAttribute._DocTypes, ARPaymentType.NumberingAttribute._SetupFields)
    {
    }

    private static string[] _DocTypes
    {
      get
      {
        return new string[8]
        {
          "PMT",
          "CRM",
          "PPM",
          "REF",
          "VRF",
          "RPM",
          "SMB",
          "PPI"
        };
      }
    }

    private static Type[] _SetupFields
    {
      get
      {
        return new Type[8]
        {
          typeof (ARSetup.paymentNumberingID),
          null,
          typeof (ARSetup.paymentNumberingID),
          typeof (ARSetup.paymentNumberingID),
          null,
          null,
          null,
          typeof (ARSetup.prepaymentInvoiceNumberingID)
        };
      }
    }

    public static Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, Type> tuple in EnumerableExtensions.Zip<string, Type>((IEnumerable<string>) ARPaymentType.NumberingAttribute._DocTypes, (IEnumerable<Type>) ARPaymentType.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == docType)
          return tuple.Item2;
      }
      return (Type) null;
    }

    public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      bool valueOrDefault = this.UserNumbering.GetValueOrDefault();
      base.RowPersisting(sender, e);
      string str1 = sender.GetValue<ARPayment.docType>(e.Row) as string;
      string str2 = (string) null;
      switch (str1)
      {
        case "PMT":
          str2 = "PPM";
          break;
        case "PPM":
          str2 = "PMT";
          break;
      }
      PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<Where<ARRegister.docType, Equal<Required<ARRegister.docType>>>>>> pxSelect = new PXSelect<ARRegister, Where<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<Where<ARRegister.docType, Equal<Required<ARRegister.docType>>>>>>(sender.Graph);
      string str3 = valueOrDefault ? (e.Row as ARRegister).RefNbr : this.NewNumber;
      if (str2 == null)
        return;
      if (((PXSelectBase<ARRegister>) pxSelect).Select(new object[2]
      {
        (object) str3,
        (object) str2
      }).Count <= 0)
        return;
      if (!valueOrDefault)
        throw new PXLockViolationException(typeof (ARRegister), (PXDBOperation) 2, (object[]) null);
      sender.RaiseExceptionHandling<ARRegister.refNbr>(e.Row, (object) str3, (Exception) new PXSetPropertyException("A {0} with this Reference Nbr. already exists in the system. Please, specify another reference number.", (PXErrorLevel) 4, new object[1]
      {
        str2 == "PMT" ? (object) "Payment" : (object) "Prepayment"
      }));
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[8]
      {
        "PMT",
        "CRM",
        "PPM",
        "REF",
        "VRF",
        "RPM",
        "SMB",
        "PPI"
      }, new string[8]
      {
        "Payment",
        "Credit Memo",
        "Prepayment",
        "Refund",
        "Voided Refund",
        "Voided Payment",
        "Balance WO",
        "Prepmt. Invoice"
      })
    {
    }
  }

  public class ListExAttribute : PXStringListAttribute
  {
    public ListExAttribute()
      : base(new string[10]
      {
        "PMT",
        "CRM",
        "PPM",
        "REF",
        "VRF",
        "RPM",
        "SMB",
        "CSL",
        "RCS",
        "PPI"
      }, new string[10]
      {
        "Payment",
        "Credit Memo",
        "Prepayment",
        "Refund",
        "Voided Refund",
        "Voided Payment",
        "Balance WO",
        "Cash Sale",
        "Cash Return",
        "Prepmt. Invoice"
      })
    {
    }
  }

  public new class SOListAttribute : PXStringListAttribute
  {
    public SOListAttribute()
      : base(new string[5]
      {
        "PMT",
        "CRM",
        "PPM",
        "REF",
        "PPI"
      }, new string[5]
      {
        "Payment",
        "Credit Memo",
        "Prepayment",
        "Refund",
        "Prepmt. Invoice"
      })
    {
    }

    public static void SetPaymentList<Field>(PXCache cache, bool refund) where Field : IBqlField
    {
      PXCache pxCache = cache;
      string[] strArray1;
      if (!refund)
        strArray1 = new string[4]
        {
          "PMT",
          "CRM",
          "PPM",
          "PPI"
        };
      else
        strArray1 = new string[1]{ "REF" };
      string[] strArray2;
      if (!refund)
        strArray2 = new string[4]
        {
          "Payment",
          "Credit Memo",
          "Prepayment",
          "Prepmt. Invoice"
        };
      else
        strArray2 = new string[1]{ "Refund" };
      PXStringListAttribute.SetList<Field>(pxCache, (object) null, strArray1, strArray2);
    }
  }
}
