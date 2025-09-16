// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Unbound.EntryMatrix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Unbound;

[PXCacheName("Entity Matrix")]
public class EntryMatrix : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  [PXString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string RowAttributeValue { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Attribute Value", Enabled = false)]
  public virtual string RowAttributeValueDescr { get; set; }

  public virtual string[] ColAttributeValues { get; set; }

  public virtual string[] ColAttributeValueDescrs { get; set; }

  public virtual int?[] InventoryIDs { get; set; }

  public virtual Decimal?[] Quantities { get; set; }

  /// <exclude />
  public virtual string[] UOMs { get; set; }

  /// <exclude />
  public virtual Decimal?[] BaseQuantities { get; set; }

  /// <exclude />
  [PXString]
  public virtual string BaseUOM { get; set; }

  public virtual string[] Errors { get; set; }

  [PXBool]
  public virtual bool? AllSelected { get; set; }

  public virtual bool?[] Selected { get; set; }

  [PXBool]
  public virtual bool? IsPreliminary { get; set; }

  [PXBool]
  public virtual bool? IsTotal { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string MatrixAvailability { get; set; }

  [PXInt]
  public virtual int? SelectedColumn { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntryMatrix.lineNbr>
  {
  }

  public abstract class rowAttributeValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryMatrix.rowAttributeValue>
  {
  }

  public abstract class rowAttributeValueDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryMatrix.rowAttributeValueDescr>
  {
  }

  public abstract class colAttributeValues : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    EntryMatrix.colAttributeValues>
  {
  }

  public abstract class colAttributeValueDescrs : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    EntryMatrix.colAttributeValueDescrs>
  {
  }

  public abstract class inventoryIDs : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EntryMatrix.inventoryIDs>
  {
  }

  public abstract class quantities : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EntryMatrix.quantities>
  {
  }

  public abstract class uOMs : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EntryMatrix.uOMs>
  {
  }

  public abstract class baseQuantities : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    EntryMatrix.baseQuantities>
  {
  }

  public abstract class baseUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntryMatrix.baseUOM>
  {
  }

  public abstract class errors : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EntryMatrix.errors>
  {
  }

  public abstract class allSelected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EntryMatrix.allSelected>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EntryMatrix.selected>
  {
  }

  public abstract class isPreliminary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EntryMatrix.isPreliminary>
  {
  }

  public abstract class isTotal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EntryMatrix.isTotal>
  {
  }

  public abstract class matrixAvailability : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EntryMatrix.matrixAvailability>
  {
  }

  public abstract class selectedColumn : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntryMatrix.selectedColumn>
  {
  }
}
