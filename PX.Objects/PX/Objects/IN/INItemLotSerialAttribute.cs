// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemLotSerialAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.IN;

/// <exclude />
[DebuggerDisplay("[{AttributeID}, {InventoryID}]")]
[PXCacheName("Inventory Item Lot/Serial Attribute")]
public class INItemLotSerialAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (INItemLotSerialAttribute.FK.InventoryItem))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CSAttribute.attributeID, Where<CSAttribute.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>>))]
  public virtual 
  #nullable disable
  string AttributeID { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order", Visible = false)]
  public virtual short? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INLotSerClassAttribute.required, Where<INLotSerClassAttribute.attributeID, Equal<Current<INItemLotSerialAttribute.attributeID>>, And<INLotSerClassAttribute.lotSerClassID, Equal<Current<InventoryItem.lotSerClassID>>>>>))]
  [PXUIField]
  public virtual bool? Required { get; set; }

  [PXDBBool]
  [PXDefault(true, typeof (Search<INLotSerClassAttribute.isActive, Where<INLotSerClassAttribute.attributeID, Equal<Current<INItemLotSerialAttribute.attributeID>>, And<INLotSerClassAttribute.lotSerClassID, Equal<Current<InventoryItem.lotSerClassID>>>>>))]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXString(100, IsUnicode = true)]
  public virtual string LotSerialNbr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemLotSerialAttribute>.By<INItemLotSerialAttribute.inventoryID, INItemLotSerialAttribute.attributeID>
  {
    public static INItemLotSerialAttribute Find(
      PXGraph graph,
      int? inventoryID,
      string attributeID,
      PKFindOptions options = 0)
    {
      return (INItemLotSerialAttribute) PrimaryKeyOf<INItemLotSerialAttribute>.By<INItemLotSerialAttribute.inventoryID, INItemLotSerialAttribute.attributeID>.FindBy(graph, (object) inventoryID, (object) attributeID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemLotSerialAttribute>.By<INItemLotSerialAttribute.inventoryID>
    {
    }

    public class Attribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INItemLotSerialAttribute>.By<INItemLotSerialAttribute.attributeID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemLotSerialAttribute.inventoryID>
  {
  }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttribute.attributeID>
  {
  }

  public abstract class sortOrder : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INItemLotSerialAttribute.sortOrder>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemLotSerialAttribute.required>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemLotSerialAttribute.isActive>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttribute.lotSerialNbr>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INItemLotSerialAttribute.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemLotSerialAttribute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttribute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerialAttribute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemLotSerialAttribute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttribute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerialAttribute.lastModifiedDateTime>
  {
  }
}
