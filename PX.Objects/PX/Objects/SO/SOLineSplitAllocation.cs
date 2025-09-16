// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSplitAllocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
[PXProjection(typeof (SelectFromBase<SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineSplit.lineType, Equal<SOLineType.inventory>>>>, And<BqlOperand<SOLineSplit.operation, IBqlString>.IsEqual<SOOperation.issue>>>, And<BqlOperand<SOLineSplit.completed, IBqlBool>.IsNotEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineSplit.pOCreate, NotEqual<True>>>>>.And<BqlOperand<SOLineSplit.pOCompleted, IBqlBool>.IsNotEqual<True>>>>, And<BqlOperand<SOLineSplit.childLineCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlOperand<SOLineSplit.pOReceiptNbr, IBqlString>.IsNull>>), Persistent = false)]
public class SOLineSplitAllocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLineSplit.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLineSplit.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplit.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplit.splitLineNbr))]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBInt(BqlField = typeof (SOLineSplit.siteID))]
  public virtual int? SiteID { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<SOLineSplit.isAllocated, IBqlBool>.IsEqual<True>>, SOLineSplit.baseQty>, decimal0>), typeof (Decimal))]
  public virtual Decimal? QtyAllocated { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<SOLineSplit.isAllocated, IBqlBool>.IsNotEqual<True>>, SOLineSplit.baseQty>, decimal0>), typeof (Decimal))]
  public virtual Decimal? QtyUnallocated { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineSplit.isAllocated, Equal<True>>>>>.And<BqlOperand<StrLen<SOLineSplit.lotSerialNbr>, IBqlLong>.IsGreater<Zero>>>, SOLineSplit.baseQty>, decimal0>), typeof (Decimal))]
  public virtual Decimal? LotSerialQtyAllocated { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplitAllocation.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplitAllocation.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplitAllocation.lineNbr>
  {
  }

  public abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineSplitAllocation.splitLineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplitAllocation.siteID>
  {
  }

  public abstract class qtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplitAllocation.qtyAllocated>
  {
  }

  public abstract class qtyUnallocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplitAllocation.qtyUnallocated>
  {
  }

  public abstract class lotSerialQtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplitAllocation.lotSerialQtyAllocated>
  {
  }
}
