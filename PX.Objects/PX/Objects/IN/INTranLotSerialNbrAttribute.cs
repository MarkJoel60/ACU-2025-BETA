// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN;

public class INTranLotSerialNbrAttribute : INLotSerialNbrAttribute
{
  public INTranLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type costCenterType)
    : base(InventoryType, SubItemType, LocationType, ParentLotSerialNbrType, costCenterType)
  {
  }

  public INTranLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type costCenterType)
    : base(InventoryType, SubItemType, LocationType, costCenterType)
  {
  }

  protected override bool IsTracked(
    ILSMaster row,
    INLotSerClass lotSerClass,
    string tranType,
    int? invMult)
  {
    return (!(tranType == "III") || !(lotSerClass.LotSerAssign == "U") || (!(row is INTran inTran) || !(inTran.OrigModule == "PO")) && (!(row is INTranSplit inTranSplit) || !(inTranSplit.OrigModule == "PO"))) && base.IsTracked(row, lotSerClass, tranType, invMult);
  }
}
