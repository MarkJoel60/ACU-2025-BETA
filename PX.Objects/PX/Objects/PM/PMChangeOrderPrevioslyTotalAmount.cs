// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderPrevioslyTotalAmount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PXProjection(typeof (Select4<PMChangeOrder, Where<PMChangeOrder.refNbr, Less<Current<PMChangeOrderPrevioslyTotalAmount.refNbr>>, And<PMChangeOrder.released, Equal<True>, And<PMChangeOrder.reverseStatus, NotEqual<ChangeOrderReverseStatus.reversed>, And<PMChangeOrder.reverseStatus, NotEqual<ChangeOrderReverseStatus.reversal>>>>>, Aggregate<GroupBy<PMChangeOrder.projectID, Sum<PMChangeOrder.revenueTotal>>>>))]
[Serializable]
public class PMChangeOrderPrevioslyTotalAmount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, BqlField = typeof (PMChangeOrder.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrder.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMChangeOrder.revenueTotal))]
  public virtual Decimal? RevenueTotal { get; set; }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderPrevioslyTotalAmount.refNbr>
  {
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderPrevioslyTotalAmount.projectID>
  {
  }

  public abstract class revenueTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderPrevioslyTotalAmount.revenueTotal>
  {
  }
}
