// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SubcontractReportInformation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts;

[PXCacheName("Subcontract Report Information")]
public class SubcontractReportInformation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<POOrderType.regularSubcontract>>, OrderBy<Desc<POOrder.orderNbr>>>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.vendorID), typeof (POOrder.vendorLocationID), typeof (POOrder.orderDate), typeof (POOrder.status), typeof (POOrder.curyID), typeof (POOrder.vendorRefNbr), typeof (POOrder.curyOrderTotal), typeof (POOrder.lineTotal), typeof (POOrder.sOOrderType), typeof (POOrder.sOOrderNbr), typeof (POOrder.orderDesc), typeof (POOrder.ownerID)}, Headers = new string[] {"Subcontract Nbr.", "Vendor", "Location", "Date", "Status", "Currency", "Vendor Ref.", "Subcontract Total", "Line Total", "Sales Order Type", "Sales Order Nbr.", "Description", "Owner"})]
  public virtual int? SubcontractNumber { get; set; }
}
