// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerialAttributesHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.DAC;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[PXAccumulator]
[PXBreakInheritance]
public class ItemLotSerialAttributesHeader : INItemLotSerialAttributesHeader
{
  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ItemLotSerialAttributesHeader.inventoryID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerialAttributesHeader.lotSerialNbr>
  {
  }

  public new abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ItemLotSerialAttributesHeader.noteID>
  {
  }
}
