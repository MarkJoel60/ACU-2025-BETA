// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PrimaryItemRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class PrimaryItemRestrictorAttribute : PXRestrictorAttribute
{
  public bool IsWarning;
  protected Type _InventoryType;
  protected Type _IsReceiptType;
  protected Type _IsSalesType;
  protected Type _IsTransferType;

  public PrimaryItemRestrictorAttribute(
    Type InventoryType,
    Type IsReceiptType,
    Type IsSalesType,
    Type IsTransferType)
    : base(typeof (Where<True>), string.Empty, Array.Empty<Type>())
  {
    this._InventoryType = InventoryType;
    this._IsReceiptType = IsReceiptType;
    this._IsSalesType = IsSalesType;
    this._IsTransferType = IsTransferType;
  }

  protected virtual BqlCommand WhereAnd(PXCache sender, PXSelectorAttribute selattr, Type Where)
  {
    return selattr.PrimarySelect.WhereAnd(Where);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    INLocation inLocation = (INLocation) null;
    try
    {
      inLocation = INLocation.PK.Find(sender.Graph, (int?) e.NewValue);
    }
    catch (FormatException ex)
    {
    }
    if (this._AlteredCmd == null || inLocation == null || !(inLocation.PrimaryItemValid != "N"))
      return;
    bool? nullable1 = this.VerifyExpr(sender, e.Row, this._IsReceiptType);
    this.VerifyExpr(sender, e.Row, this._IsSalesType);
    bool? nullable2 = this.VerifyExpr(sender, e.Row, this._IsTransferType);
    if (!nullable1.GetValueOrDefault() && !nullable2.GetValueOrDefault())
      return;
    int? nullable3 = (int?) sender.GetValue(e.Row, this._InventoryType.Name);
    if (!nullable3.HasValue)
      return;
    switch (inLocation.PrimaryItemValid)
    {
      case "I":
        if (object.Equals((object) nullable3, (object) inLocation.PrimaryItemID))
          break;
        this.ThrowErrorItem("Selected item is not allowed in this location.", e, (object) inLocation.LocationCD);
        break;
      case "C":
        InventoryItem inventoryItem1 = InventoryItem.PK.Find(sender.Graph, nullable3);
        if (inventoryItem1 == null)
          break;
        int? itemClassId1 = inventoryItem1.ItemClassID;
        int? primaryItemClassId1 = inLocation.PrimaryItemClassID;
        if (itemClassId1.GetValueOrDefault() == primaryItemClassId1.GetValueOrDefault() & itemClassId1.HasValue == primaryItemClassId1.HasValue)
          break;
        this.ThrowErrorItem("Selected item is not allowed in this location.", e, (object) inLocation.LocationCD);
        break;
      case "X":
        if (object.Equals((object) nullable3, (object) inLocation.PrimaryItemID))
          break;
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException("Selected item is not allowed in this location.", (PXErrorLevel) 2));
        break;
      case "Y":
        InventoryItem inventoryItem2 = InventoryItem.PK.Find(sender.Graph, nullable3);
        if (inventoryItem2 == null)
          break;
        int? itemClassId2 = inventoryItem2.ItemClassID;
        int? primaryItemClassId2 = inLocation.PrimaryItemClassID;
        if (itemClassId2.GetValueOrDefault() == primaryItemClassId2.GetValueOrDefault() & itemClassId2.HasValue == primaryItemClassId2.HasValue)
          break;
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException("Selected item is not allowed in this location.", (PXErrorLevel) 2));
        break;
    }
  }

  public virtual void ThrowErrorItem(
    string message,
    PXFieldVerifyingEventArgs e,
    object ErrorValue)
  {
    e.NewValue = ErrorValue;
    throw new PXSetPropertyException(message);
  }

  protected bool? VerifyExpr(PXCache cache, object data, Type whereType)
  {
    object obj = (object) null;
    bool? nullable = new bool?();
    ((IBqlUnary) Activator.CreateInstance(whereType)).Verify(cache, data, new List<object>(), ref nullable, ref obj);
    return nullable;
  }
}
