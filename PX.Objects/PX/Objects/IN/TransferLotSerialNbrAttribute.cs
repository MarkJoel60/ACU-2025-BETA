// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.TransferLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN;

public class TransferLotSerialNbrAttribute : INLotSerialNbrAttribute
{
  public TransferLotSerialNbrAttribute(
    Type inventoryType,
    Type subItemType,
    Type locationType,
    Type costCenterType,
    Type tranType,
    Type transferNbrType,
    Type transferLineNbrType)
    : this(inventoryType, subItemType, locationType, costCenterType, tranType, transferNbrType, transferLineNbrType, (Type) null)
  {
  }

  public TransferLotSerialNbrAttribute(
    Type inventoryType,
    Type subItemType,
    Type locationType,
    Type costCenterType,
    Type tranType,
    Type transferNbrType,
    Type transferLineNbrType,
    Type parentLotSerialNbrType)
  {
    this.InitializeSelector(this.GetIntransitLotSerialSearch(inventoryType, subItemType, locationType, costCenterType, tranType, transferNbrType, transferLineNbrType), typeof (INLotSerialStatusByCostCenter.lotSerialNbr), typeof (INLotSerialStatusByCostCenter.qtyOnHand), typeof (INLotSerialStatusByCostCenter.qtyAvail), typeof (INLotSerialStatusByCostCenter.expireDate));
    if (!(parentLotSerialNbrType != (Type) null))
      return;
    this.InitializeDefault(parentLotSerialNbrType);
  }
}
