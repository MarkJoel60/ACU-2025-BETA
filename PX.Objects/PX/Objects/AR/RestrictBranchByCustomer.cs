// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RestrictBranchByCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AR;

public class RestrictBranchByCustomer(System.Type source, System.Type cOrgBAccountID = null) : 
  RestrictBranchBySource(BqlCommand.Compose(new System.Type[10]
  {
    typeof (Where<,,>),
    typeof (PX.Objects.GL.Branch.branchID),
    typeof (Inside<>),
    typeof (Current<>),
    cOrgBAccountID,
    typeof (Or<,>),
    typeof (Current<>),
    cOrgBAccountID,
    typeof (Equal<>),
    typeof (Zero)
  }), source, typeof (PX.Objects.GL.Branch.branchCD), "The usage of the {0} customer is restricted in the {1} branch.")
{
  public override void CacheAttached(PXCache sender)
  {
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    System.Type itemType = sender.GetItemType();
    RestrictBranchByCustomer branchByCustomer = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) branchByCustomer, __vmethodptr(branchByCustomer, OnRowSelected));
    rowSelected.AddHandler(itemType, pxRowSelected);
    base.CacheAttached(sender);
  }
}
