// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRevenueTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[ExcludeFromCodeCoverage]
[PXProjection(typeof (Select4<PMRevenueBudget, Where<PMRevenueBudget.type, Equal<AccountType.income>>, Aggregate<GroupBy<PMRevenueBudget.projectID, GroupBy<PMRevenueBudget.projectTaskID, GroupBy<PMRevenueBudget.inventoryID, Sum<PMRevenueBudget.curyRevisedAmount, Sum<PMRevenueBudget.curyInvoicedAmount>>>>>>>), Persistent = false)]
[Serializable]
public class PMRevenueTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMRevenueBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMRevenueBudget.revenueInventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMRevenueBudget.curyRevisedAmount))]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMRevenueBudget.curyInvoicedAmount))]
  public virtual Decimal? CuryInvoicedAmount { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXBaseCury]
  public virtual Decimal? CuryAmountToInvoiceProjected { get; set; }

  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMRevenueTotal.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueTotal.projectTaskID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueTotal.inventoryID>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueTotal.curyRevisedAmount>
  {
  }

  public abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueTotal.curyInvoicedAmount>
  {
  }

  public abstract class curyAmountToInvoiceProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueTotal.curyAmountToInvoiceProjected>
  {
  }
}
