// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.InventoryItemLotSerialAttributes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN.Attributes;

#nullable enable
namespace PX.Objects.IN.DAC.Projections;

/// <exclude />
[PXProjection(typeof (Select<PX.Objects.IN.InventoryItem>))]
[PXCacheName]
public class InventoryItemLotSerialAttributes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Database identity.
  /// The unique identifier of the Inventory Item.
  /// </summary>
  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXUIField]
  [InventoryItemWithLotSerialAttributes]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// Array to store attribute identifiers (CSAttribute.attributeID) of lot/serial attributes.
  /// </summary>
  public virtual 
  #nullable disable
  string[] AttributeIdentifiers { get; set; }

  /// <summary>
  /// Array to store attribute required flag (INItemLotSerialAttribute.required) of lot/serial attributes.
  /// </summary>
  public virtual bool[] AttributeRequired { get; set; }

  /// <summary>
  /// Array to store attribute required flag (INItemLotSerialAttribute.isActive) of lot/serial attributes.
  /// </summary>
  public virtual bool[] AttributeIsActive { get; set; }

  /// <summary>
  /// Array to store attribute required flag (INItemLotSerialAttribute.sortOrder) of lot/serial attributes.
  /// </summary>
  public virtual short[] AttributeSortOrder { get; set; }

  public class PK : 
    PrimaryKeyOf<InventoryItemLotSerialAttributes>.By<InventoryItemLotSerialAttributes.inventoryID>
  {
    public static InventoryItemLotSerialAttributes Find(
      PXGraph graph,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (InventoryItemLotSerialAttributes) PrimaryKeyOf<InventoryItemLotSerialAttributes>.By<InventoryItemLotSerialAttributes.inventoryID>.FindBy(graph, (object) inventoryID, options);
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemLotSerialAttributes.inventoryID>
  {
  }

  public abstract class attributeIdentifiers : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemLotSerialAttributes.attributeIdentifiers>
  {
  }

  public abstract class attributeRequired : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemLotSerialAttributes.attributeRequired>
  {
  }

  public abstract class attributeIsActive : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemLotSerialAttributes.attributeIsActive>
  {
  }

  public abstract class attributeSortOrder : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemLotSerialAttributes.attributeSortOrder>
  {
  }
}
