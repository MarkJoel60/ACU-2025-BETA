// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PO;

public class PO
{
  /// <summary>Default Ctor</summary>
  /// <param name="SearchType"> Must be IBqlSearch type, pointing to POOrder.refNbr</param>
  public class RefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[12]
  {
    typeof (POOrder.orderType),
    typeof (POOrder.orderNbr),
    typeof (POOrder.vendorRefNbr),
    typeof (POOrder.orderDate),
    typeof (POOrder.status),
    typeof (POOrder.vendorID),
    typeof (POOrder.vendorID_Vendor_acctName),
    typeof (POOrder.vendorLocationID),
    typeof (POOrder.curyID),
    typeof (POOrder.curyOrderTotal),
    typeof (POOrder.sOOrderType),
    typeof (POOrder.sOOrderNbr)
  })
  {
  }

  /// <summary>
  /// Specialized version of the AutoNumber attribute for POOrders<br />
  /// It defines how the new numbers are generated for the PO Order. <br />
  /// References POOrder.docType and POOrder.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in PO Setup and POOrder types:<br />
  /// namely POSetup.regularPONumberingID, POSetup.regularPONumberingID for POOrderType.RegularOrder, POOrderType.DropShip, and POOrderType.StandardBlanket,<br />
  /// and POSetup.standardPONumberingID for POOrderType.Blanket and POOrderType.Transfer<br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (POOrder.orderType), typeof (POOrder.orderDate), new string[5]
      {
        "RO",
        "DP",
        "PD",
        "SB",
        "BL"
      }, new Type[5]
      {
        typeof (POSetup.regularPONumberingID),
        typeof (POSetup.regularPONumberingID),
        typeof (POSetup.regularPONumberingID),
        typeof (POSetup.regularPONumberingID),
        typeof (POSetup.standardPONumberingID)
      })
    {
    }
  }
}
