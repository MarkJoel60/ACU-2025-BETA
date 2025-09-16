// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.CR.Helpers.SubcontractEntityDescriptionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.CN.Subcontracts.CR.Helpers;

public static class SubcontractEntityDescriptionHelper
{
  public const string StatusFieldName = "Status";
  public static string SubcontractDescription = $"<b>Subcontract</b><table width=\"100%\" cellspacing=\"0\" style=\"margin-left: 7px;\" ><tr><td><font color=\"Gray\">Order Nbr.:</font></td><td>{{0}}</td></tr><tr><td><font color=\"Gray\">Vendor:</font></td><td>{{1}}</td></tr><tr><td><font color=\"Gray\">Location:</font></td><td>Primary Location</td></tr><tr><td><font color=\"Gray\">Date:</font></td><td>{{2}}</td></tr><tr><td><font color=\"Gray\">Status:</font></td><td>{{3}}</td></tr></table>";

  public static string GetDescription(CRActivity activity, PXGraph graph)
  {
    EntityHelper entityHelper = new EntityHelper(graph);
    return !((entityHelper.GetEntityRow(activity.RefNoteID) is PX.Objects.PO.POOrder entityRow ? entityRow.OrderType : (string) null) == "RS") ? (string) null : SubcontractEntityDescriptionHelper.GetEntityDescription(entityHelper, entityRow, graph);
  }

  private static string GetEntityDescription(
    EntityHelper entityHelper,
    PX.Objects.PO.POOrder commitment,
    PXGraph graph)
  {
    string fieldString = entityHelper.GetFieldString((object) commitment, ((object) commitment).GetType(), "Status");
    string vendorName = SubcontractEntityDescriptionHelper.GetVendorName(commitment.VendorID, graph);
    return string.Format(SubcontractEntityDescriptionHelper.SubcontractDescription, (object) commitment.OrderNbr, (object) vendorName, (object) commitment.OrderDate, (object) fieldString);
  }

  private static string GetVendorName(int? vendorId, PXGraph graph)
  {
    return ((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>(graph)).SelectSingle(new object[1]
    {
      (object) vendorId
    }).AcctName;
  }
}
