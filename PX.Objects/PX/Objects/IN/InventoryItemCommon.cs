// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemCommon
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Attributes;
using PX.Objects.IN.Matrix.Graphs;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// The DAC provides only common properties of the <see cref="T:PX.Objects.IN.InventoryItem" /> DAC
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (InventoryItemMaint), typeof (NonStockItemMaint), typeof (TemplateInventoryItemMaint)}, new Type[] {typeof (Select<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Current<InventoryItemCommon.inventoryCD>>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.stkItem, Equal<True>>>>>), typeof (Select<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Current<InventoryItemCommon.inventoryCD>>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.stkItem, NotEqual<True>>>>>), typeof (Select<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Current<InventoryItemCommon.inventoryCD>>, And<InventoryItem.isTemplate, Equal<True>>>>)})]
[PXCacheName]
[PXProjection(typeof (Select<InventoryItem>), Persistent = false)]
public class InventoryItemCommon : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.Selected" />
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  [PXFormula(typeof (False))]
  public virtual bool? Selected { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.InventoryID" />
  [PXDBInt(BqlField = typeof (InventoryItem.inventoryID))]
  [PXUIField]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.InventoryCD" />
  [PXDBString(InputMask = "", IsUnicode = true, IsKey = true, BqlField = typeof (InventoryItem.inventoryCD))]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItemCommon.inventoryCD>), typeof (InventoryItemCommon.inventoryCD))]
  [PXUIField]
  public virtual 
  #nullable disable
  string InventoryCD { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.Descr" />
  [PXUIField(DisplayName = "Description")]
  [PXDBString(BqlField = typeof (InventoryItem.descr))]
  public virtual string Descr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.ItemStatus" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (InventoryItem.itemStatus))]
  [InventoryItemStatus.List]
  public virtual string ItemStatus { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.ItemClassID" />
  [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
  public virtual int? ItemClassID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.LotSerClassID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (InventoryItem.lotSerClassID))]
  public virtual string LotSerClassID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.StkItem" />
  [PXDBBool(BqlField = typeof (InventoryItem.stkItem))]
  public virtual bool? StkItem { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.IsTemplate" />
  [PXDBBool(BqlField = typeof (InventoryItem.isTemplate))]
  public virtual bool? IsTemplate { get; set; }

  [BorrowedNote(typeof (InventoryItem), typeof (InventoryItemMaint), BqlField = typeof (InventoryItem.noteID))]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItemCommon.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItemCommon.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemCommon.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItemCommon.descr>
  {
  }

  public abstract class itemStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemCommon.itemStatus>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItemCommon.itemClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemCommon.lotSerClassID>
  {
  }

  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItemCommon.stkItem>
  {
  }

  public abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItemCommon.isTemplate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  InventoryItemCommon.noteID>
  {
  }
}
