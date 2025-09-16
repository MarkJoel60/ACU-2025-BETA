// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SO
{
  /// <summary>
  /// Specialized selector for SOOrder RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// SOOrder.orderNbr,SOOrder.orderDate, SOOrder.customerID,<br />
  /// SOOrder.customerID_Customer_acctName, SOOrder.customerLocationID,<br />
  /// SOOrder.curyID, SOOrder.curyOrderTotal, SOOrder.status,SOOrder.invoiceNbr<br />
  /// </summary>
  public class RefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[10]
  {
    typeof (SOOrder.orderNbr),
    typeof (SOOrder.customerOrderNbr),
    typeof (SOOrder.orderDate),
    typeof (SOOrder.customerID),
    typeof (SOOrder.customerID_Customer_acctName),
    typeof (SOOrder.customerLocationID),
    typeof (SOOrder.curyID),
    typeof (SOOrder.curyOrderTotal),
    typeof (SOOrder.status),
    typeof (SOOrder.invoiceNbr)
  })
  {
  }

  /// <summary>
  /// Specialized for SOOrder version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the SO Order. <br />
  /// References SOOrder.orderDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in SO Order Type: namely SOOrderType.orderNumberingID. <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (Search<SOOrderType.orderNumberingID, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderType.active, Equal<True>>>>), typeof (SOOrder.orderDate))
    {
    }
  }
}
