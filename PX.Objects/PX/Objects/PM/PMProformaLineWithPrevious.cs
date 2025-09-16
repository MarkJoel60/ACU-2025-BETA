// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaLineWithPrevious
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Pro Forma Line")]
[PXBreakInheritance]
public class PMProformaLineWithPrevious : PMProformaLine
{
  [PXDBScalar(typeof (Search4<PMProformaLine.curyLineTotal, Where<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.refNbr, Less<PMProformaLineWithPrevious.refNbr>, And<PMProformaLine.projectID, Equal<PMProformaLineWithPrevious.projectID>, And<PMProformaLine.taskID, Equal<PMProformaLineWithPrevious.taskID>, And<PMProformaLine.accountGroupID, Equal<PMProformaLineWithPrevious.accountGroupID>, And<PMProformaLine.costCodeID, Equal<PMProformaLineWithPrevious.costCodeID>, And<PMProformaLine.inventoryID, Equal<PMProformaLineWithPrevious.inventoryID>, And<PMProformaLine.released, Equal<True>, And<PMProformaLine.corrected, NotEqual<True>>>>>>>>>>, Aggregate<Sum<PMProformaLine.curyLineTotal>>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced", Enabled = false)]
  public override Decimal? CuryPreviouslyInvoiced { get; set; }

  [PXDBScalar(typeof (Search4<PMProformaLine.lineTotal, Where<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.refNbr, Less<PMProformaLineWithPrevious.refNbr>, And<PMProformaLine.projectID, Equal<PMProformaLineWithPrevious.projectID>, And<PMProformaLine.taskID, Equal<PMProformaLineWithPrevious.taskID>, And<PMProformaLine.accountGroupID, Equal<PMProformaLineWithPrevious.accountGroupID>, And<PMProformaLine.costCodeID, Equal<PMProformaLineWithPrevious.costCodeID>, And<PMProformaLine.inventoryID, Equal<PMProformaLineWithPrevious.inventoryID>, And<PMProformaLine.released, Equal<True>, And<PMProformaLine.corrected, NotEqual<True>>>>>>>>>>, Aggregate<Sum<PMProformaLine.lineTotal>>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced in Base Currency", Enabled = false)]
  public override Decimal? PreviouslyInvoiced { get; set; }

  /// <summary>
  /// The running total of the Quantity to Invoice column
  /// for all the lines of preceding pro forma invoices that refer to the same revenue budget line.
  /// </summary>
  [PXDBScalar(typeof (Search4<PMProformaLine.qty, Where<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.refNbr, Less<PMProformaLineWithPrevious.refNbr>, And<PMProformaLine.projectID, Equal<PMProformaLineWithPrevious.projectID>, And<PMProformaLine.taskID, Equal<PMProformaLineWithPrevious.taskID>, And<PMProformaLine.accountGroupID, Equal<PMProformaLineWithPrevious.accountGroupID>, And<PMProformaLine.costCodeID, Equal<PMProformaLineWithPrevious.costCodeID>, And<PMProformaLine.inventoryID, Equal<PMProformaLineWithPrevious.inventoryID>, And<PMProformaLine.uOM, Equal<PMProformaLineWithPrevious.uOM>, And<PMProformaLine.released, Equal<True>, And<PMProformaLine.corrected, NotEqual<True>>>>>>>>>>>, Aggregate<Sum<PMProformaLine.qty>>>))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced Quantity", Enabled = false)]
  public override Decimal? PreviouslyInvoicedQty { get; set; }

  [PXDecimal]
  [ProgressCompleted]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Completed (%)", Enabled = false)]
  public Decimal? QuantityBaseCompletedPct { get; set; }

  public new abstract class refNbr : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    PMProformaLineWithPrevious.refNbr>
  {
  }

  public new abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLineWithPrevious.revisionID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLineWithPrevious.lineNbr>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLineWithPrevious.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLineWithPrevious.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLineWithPrevious.accountGroupID>
  {
  }

  public new abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLineWithPrevious.costCodeID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLineWithPrevious.inventoryID>
  {
  }

  public new abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineWithPrevious.progressBillingBase>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLineWithPrevious.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLineWithPrevious.qty>
  {
  }

  public new abstract class curyPreviouslyInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineWithPrevious.curyPreviouslyInvoiced>
  {
  }

  public new abstract class previouslyInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineWithPrevious.previouslyInvoiced>
  {
  }

  public new abstract class previouslyInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineWithPrevious.previouslyInvoicedQty>
  {
  }

  public abstract class quantityBaseCompletedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineWithPrevious.quantityBaseCompletedPct>
  {
  }
}
