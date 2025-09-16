// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesAllocationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXHidden]
public class SalesAllocationStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AvailableQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BufferedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedSelectedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnallocatedQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDirty { get; set; }

  public abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  SalesAllocationStatus.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocationStatus.inventoryID>
  {
  }

  public abstract class availableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocationStatus.availableQty>
  {
  }

  public abstract class allocatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocationStatus.allocatedQty>
  {
  }

  public abstract class bufferedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocationStatus.bufferedQty>
  {
  }

  public abstract class allocatedSelectedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocationStatus.allocatedSelectedQty>
  {
  }

  public abstract class unallocatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocationStatus.unallocatedQty>
  {
  }

  public abstract class isDirty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesAllocationStatus.isDirty>
  {
  }
}
