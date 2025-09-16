// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class AssetTypeMaint : PXGraph<AssetTypeMaint>
{
  public PXSavePerRow<FAType, FAType.assetTypeID> Save;
  public PXCancel<FAType> Cancel;
  public PXSelect<FAType> AssetTypes;

  public AssetTypeMaint()
  {
    ((PXSelectBase) this.AssetTypes).Cache.AllowInsert = true;
    ((PXSelectBase) this.AssetTypes).Cache.AllowUpdate = true;
    ((PXSelectBase) this.AssetTypes).Cache.AllowDelete = true;
  }

  protected virtual void FAType_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FAType row = (FAType) e.Row;
    if (row != null && this.IsUsed(row.AssetTypeID))
      throw new PXSetPropertyException("You cannot delete that Asset Type because it is specified for some fixed asset or fixed asset class.", (PXErrorLevel) 5);
  }

  protected virtual void FAType_IsTangible_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FAType row = (FAType) e.Row;
    if (row == null || !this.IsUsed(row.AssetTypeID))
      return;
    sender.RaiseExceptionHandling<FAType.isTangible>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Changes will affect only newly created fixed asset or fixed asset class.", (PXErrorLevel) 3));
  }

  protected virtual void FAType_Depreciable_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FAType row = (FAType) e.Row;
    if (row == null || !this.IsUsed(row.AssetTypeID))
      return;
    sender.RaiseExceptionHandling<FAType.depreciable>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Changes will affect only newly created fixed asset or fixed asset class.", (PXErrorLevel) 3));
  }

  protected virtual void FAType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FAType row = (FAType) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<FAType.assetTypeID>(sender, (object) row, sender.GetStatus((object) row) == 2);
  }

  private bool IsUsed(string assetTypeID)
  {
    if (assetTypeID == null)
      return false;
    return PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetTypeID, IsNotNull, And<FixedAsset.assetTypeID, Equal<Required<FixedAsset.assetTypeID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) assetTypeID
    }).Count > 0;
  }
}
