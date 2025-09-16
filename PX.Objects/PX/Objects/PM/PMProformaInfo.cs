// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Virtual Table used in Report</summary>
[PXCacheName("Proforma Info for AIA Report")]
public class PMProformaInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Gets or sets Proforma Reference Number</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>Gets or sets Original Contract total</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Contract Total")]
  public virtual Decimal? OriginalContractTotal { get; set; }

  /// <summary>Gets or sets Change Order total</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Total")]
  public virtual Decimal? ChangeOrderTotal { get; set; }

  /// <summary>Gets or sets Revised Contract total</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Contract Total")]
  public virtual Decimal? RevisedContractTotal { get; set; }

  /// <summary>
  /// Gets or sets Proforma Line total for Previous Proforma
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prior Proforma LineTotal")]
  public virtual Decimal? PriorProformaLineTotal { get; set; }

  /// <summary>Gets or sets completed to date line total</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed to date Line Total")]
  public virtual Decimal? CompletedToDateLineTotal { get; set; }

  /// <summary>Gets or sets retainage held to date total</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Held To Date Total")]
  public virtual Decimal? RetainageHeldToDateTotal { get; set; }

  /// <summary>
  /// Gets or sets change order additions (positive changes) for current period
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Additions")]
  public virtual Decimal? ChangeOrderAdditions { get; set; }

  /// <summary>
  /// Gets or sets change order additions (positive changes) for previous period
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Additions Previous")]
  public virtual Decimal? ChangeOrderAdditionsPrevious { get; set; }

  /// <summary>
  /// Gets or sets change order deductions (negative changes) for current period
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Deduction")]
  public virtual Decimal? ChangeOrderDeduction { get; set; }

  /// <summary>
  /// Gets or sets change order deductions (negative changes) for previous period
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Deduction Previous")]
  public virtual Decimal? ChangeOrderDeductionPrevious { get; set; }

  /// <summary>Gets or sets Progress Billing Base</summary>
  [PXString]
  [PXUIField(DisplayName = "Progress Billing Basis", Enabled = false)]
  [PX.Objects.PM.ProgressBillingBase.List]
  public string ProgressBillingBase { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaInfo.refNbr>
  {
  }

  public abstract class originalContractTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.originalContractTotal>
  {
  }

  public abstract class changeOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.changeOrderTotal>
  {
  }

  public abstract class revisedContractTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.revisedContractTotal>
  {
  }

  public abstract class priorProformaLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.priorProformaLineTotal>
  {
  }

  public abstract class completedToDateLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.completedToDateLineTotal>
  {
  }

  public abstract class retainageHeldToDateTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.retainageHeldToDateTotal>
  {
  }

  public abstract class changeOrderAdditions : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.changeOrderAdditions>
  {
  }

  public abstract class changeOrderAdditionsPrevious : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.changeOrderAdditionsPrevious>
  {
  }

  public abstract class changeOrderDeduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.changeOrderDeduction>
  {
  }

  public abstract class changeOrderDeductionPrevious : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.changeOrderDeductionPrevious>
  {
  }

  public abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaInfo.progressBillingBase>
  {
  }
}
