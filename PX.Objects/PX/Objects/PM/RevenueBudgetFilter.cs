// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RevenueBudgetFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("Revenue Budget Filter")]
[ExcludeFromCodeCoverage]
[Serializable]
public class RevenueBudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectTaskID;

  [ProjectTask(typeof (PMProject.contractID), AlwaysEnabled = true, DirtyRead = true)]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Task")]
  public virtual bool? GroupByTask { get; set; }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (RevenueBudgetFilter.amountToInvoiceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Amount Total")]
  public virtual Decimal? CuryAmountToInvoiceTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Amount Total in Base Currency")]
  public virtual Decimal? AmountToInvoiceTotal { get; set; }

  public abstract class projectTaskID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    RevenueBudgetFilter.projectTaskID>
  {
  }

  public abstract class groupByTask : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RevenueBudgetFilter.groupByTask>
  {
  }

  public abstract class curyAmountToInvoiceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevenueBudgetFilter.curyAmountToInvoiceTotal>
  {
  }

  public abstract class amountToInvoiceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevenueBudgetFilter.amountToInvoiceTotal>
  {
  }
}
