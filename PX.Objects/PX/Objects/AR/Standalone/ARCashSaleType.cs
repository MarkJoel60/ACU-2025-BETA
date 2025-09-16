// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARCashSaleType
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
namespace PX.Objects.AR.Standalone;

public class ARCashSaleType : ARDocType
{
  [Obsolete("Obsoilete. Will be removed in Acumatica ERP 2019R1")]
  public static bool VoidAppl(string DocType) => DocType == "RPM";

  /// <summary>
  /// Specialized selector for ARCashSale RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// ARCashSale.refNbr,ARCashSale.docDate, ARCashSale.finPeriodID,<br />
  /// ARCashSale.customerID, ARCashSale.customerID_Customer_acctName,<br />
  /// ARCashSale.customerLocationID, ARCashSale.curyID, ARCashSale.curyOrigDocAmt,<br />
  /// ARCashSale.curyDocBal,ARCashSale.status, ARCashSale.dueDate, ARCashSale.invoiceNbr<br />
  /// </summary>
  /// <summary>Ctor</summary>
  /// <param name="SearchType">Must be IBqlSearch, returning ARCashSale.refNbr</param>
  public class RefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[13]
  {
    typeof (ARCashSale.refNbr),
    typeof (ARCashSale.extRefNbr),
    typeof (ARCashSale.docDate),
    typeof (ARCashSale.finPeriodID),
    typeof (ARCashSale.customerID),
    typeof (ARCashSale.customerID_Customer_acctName),
    typeof (ARCashSale.customerLocationID),
    typeof (ARCashSale.curyID),
    typeof (ARCashSale.curyOrigDocAmt),
    typeof (ARCashSale.curyDocBal),
    typeof (ARCashSale.status),
    typeof (ARCashSale.cashAccountID),
    typeof (ARCashSale.pMInstanceID_CustomerPaymentMethod_descr)
  })
  {
  }

  /// <summary>
  /// Specialized for AR CashSales version of the of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /> <br />
  /// It defines how the new numbers are generated for the AR CashSales. <br />
  /// References ARInvoice.docType and ARInvoice.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AR Setup and ARInvoice types:<br />
  /// namely ARSetup.paymentNumberingID - for AR CashSale and CashReturn
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (ARCashSale.docType), typeof (ARCashSale.docDate), ARCashSaleType.NumberingAttribute._DocTypes, ARCashSaleType.NumberingAttribute._SetupFields)
    {
    }

    private static string[] _DocTypes
    {
      get => new string[2]{ "CSL", "RCS" };
    }

    private static Type[] _SetupFields
    {
      get
      {
        return new Type[2]
        {
          typeof (ARSetup.invoiceNumberingID),
          typeof (ARSetup.invoiceNumberingID)
        };
      }
    }

    public static Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, Type> tuple in EnumerableExtensions.Zip<string, Type>((IEnumerable<string>) ARCashSaleType.NumberingAttribute._DocTypes, (IEnumerable<Type>) ARCashSaleType.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == docType)
          return tuple.Item2;
      }
      return (Type) null;
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "CSL", "RCS" }, new string[2]
      {
        "Cash Sale",
        "Cash Return"
      })
    {
    }
  }
}
