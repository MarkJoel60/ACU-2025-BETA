// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryQtyByPlanType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[Serializable]
public class InventoryQtyByPlanType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string PlanType { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Qty { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Included { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsTotal { get; set; }

  [PXInt]
  [PXDefault(0)]
  public virtual int? SortOrder { get; set; }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryQtyByPlanType.planType>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryQtyByPlanType.qty>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryQtyByPlanType.included>
  {
  }

  public abstract class isTotal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryQtyByPlanType.isTotal>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryQtyByPlanType.sortOrder>
  {
  }
}
