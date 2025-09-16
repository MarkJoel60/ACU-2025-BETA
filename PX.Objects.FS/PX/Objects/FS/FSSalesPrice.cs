// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSalesPrice
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSSalesPrice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? SalesPriceID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual int? InventoryID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Line Type", Enabled = false)]
  [ListField_LineType_SalesPrices.ListAtrribute]
  public virtual 
  #nullable disable
  string LineType { get; set; }

  [PXDBPriceCost]
  [PXDefault]
  public virtual Decimal? UnitPrice { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the sales unit for the Inventory Item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.FS.FSScheduleDet.UOM">UOM</see> associated with the <see cref="P:PX.Objects.FS.FSSalesPrice.InventoryID">Inventory Item</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [INUnit(DisplayName = "UOM", Enabled = false, IsKey = true)]
  public virtual string UOM { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? Mem_UnitPrice { get; set; }

  public abstract class salesPriceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSalesPrice.salesPriceID>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSalesPrice.serviceContractID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSalesPrice.inventoryID>
  {
  }

  public abstract class lineType : ListField_LineType_SalesPrices
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSalesPrice.unitPrice>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSalesPrice.uOM>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSalesPrice.curyID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSalesPrice.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSalesPrice.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSalesPrice.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSalesPrice.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSalesPrice.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSalesPrice.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSalesPrice.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSalesPrice.noteID>
  {
  }

  public abstract class mem_UnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSalesPrice.mem_UnitPrice>
  {
  }
}
