// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSubItemMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INSubItemMaint : PXGraph<INSubItemMaint>
{
  public PXCancel<INSubItem> Cancel;
  public PXSavePerRow<INSubItem, INSubItem.subItemID> Save;
  [PXImport(typeof (INSubItem))]
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<INSubItem, OrderBy<Asc<INSubItem.subItemCD>>> SubItemRecords;

  [SubItemRaw(IsKey = true, DisplayName = "Subitem")]
  [PXDefault]
  protected virtual void INSubItem_SubItemCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  protected virtual void INSubItem_Descr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void INSubItem_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is INSubItem row))
      return;
    if (PXResultset<INSiteStatusByCostCenter>.op_Implicit(PXSelectBase<INSiteStatusByCostCenter, PXSelect<INSiteStatusByCostCenter, Where<INSiteStatusByCostCenter.subItemID, Equal<Required<INSubItem.subItemID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.SubItemID
    })) != null)
      throw new PXSetPropertyException("You cannot delete Subitem because it is already in use.");
    if (PXResultset<INItemXRef>.op_Implicit(PXSelectBase<INItemXRef, PXSelect<INItemXRef, Where<INItemXRef.subItemID, Equal<Required<INSubItem.subItemID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.SubItemID
    })) != null)
      throw new PXSetPropertyException("You cannot delete Subitem because it is already in use.");
  }
}
