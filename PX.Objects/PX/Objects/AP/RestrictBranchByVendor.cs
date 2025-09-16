// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RestrictBranchByVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public class RestrictBranchByVendor(System.Type source, System.Type vOrgBAccountID = null) : 
  RestrictBranchBySource(BqlCommand.Compose(typeof (Where<,,>), typeof (PX.Objects.GL.Branch.branchID), typeof (Inside<>), typeof (Current<>), vOrgBAccountID, typeof (Or<,>), typeof (Current<>), vOrgBAccountID, typeof (Equal<>), typeof (Zero)), source, typeof (PX.Objects.GL.Branch.branchCD), "The usage of the {0} vendor is restricted in the {1} branch.")
{
  public override void CacheAttached(PXCache sender)
  {
    sender.Graph.RowSelected.AddHandler(sender.GetItemType(), new PXRowSelected(((RestrictBranchBySource) this).OnRowSelected));
    base.CacheAttached(sender);
  }
}
