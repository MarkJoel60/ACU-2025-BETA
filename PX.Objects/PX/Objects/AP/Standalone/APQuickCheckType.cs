// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APQuickCheckType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.Standalone;

public class APQuickCheckType : APDocType
{
  [Obsolete("Obsoilete. Will be removed in Acumatica ERP 2019R1")]
  public static bool VoidAppl(string DocType) => DocType == "VCK";

  public class RefNbrAttribute(System.Type SearchType) : PXSelectorAttribute(SearchType, typeof (APQuickCheck.refNbr), typeof (APQuickCheck.docDate), typeof (APQuickCheck.finPeriodID), typeof (APQuickCheck.vendorID), typeof (APQuickCheck.vendorID_Vendor_acctName), typeof (APQuickCheck.vendorLocationID), typeof (APQuickCheck.curyID), typeof (APQuickCheck.curyOrigDocAmt), typeof (APQuickCheck.curyDocBal), typeof (APQuickCheck.status), typeof (APQuickCheck.cashAccountID), typeof (APQuickCheck.paymentMethodID), typeof (APQuickCheck.extRefNbr))
  {
  }

  /// <summary>
  /// Specialized for APQuickCheck version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AP Payment. <br />
  /// References APQuickCheck.docType and APQuickCheck.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AP Setup and APQuickCheck types:<br />
  /// namely - APSetup.checkNumberingID for QuickCheck and null for VoidQuickCheck<br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (APQuickCheck.docType), typeof (APQuickCheck.docDate), APQuickCheckType.NumberingAttribute._DocTypes, APQuickCheckType.NumberingAttribute._SetupFields)
    {
    }

    private static string[] _DocTypes
    {
      get => new string[3]{ "QCK", "RQC", "VQC" };
    }

    private static System.Type[] _SetupFields
    {
      get
      {
        return new System.Type[3]
        {
          typeof (APSetup.checkNumberingID),
          typeof (APSetup.checkNumberingID),
          null
        };
      }
    }

    public static System.Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, System.Type> tuple in EnumerableExtensions.Zip<string, System.Type>((IEnumerable<string>) APQuickCheckType.NumberingAttribute._DocTypes, (IEnumerable<System.Type>) APQuickCheckType.NumberingAttribute._SetupFields))
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
      : base(new string[3]{ "QCK", "RQC", "VQC" }, new string[3]
      {
        "Cash Purchase",
        "Cash Return",
        "Voided Cash Purchase"
      })
    {
    }
  }
}
