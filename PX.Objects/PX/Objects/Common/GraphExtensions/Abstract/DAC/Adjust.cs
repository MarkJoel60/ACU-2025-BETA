// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.Adjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class Adjust : PXMappedCacheExtension, IFinAdjust, IAdjustment
{
  public virtual int? AdjgBranchID { get; set; }

  public virtual string AdjgFinPeriodID { get; set; }

  public virtual string AdjgTranPeriodID { get; set; }

  public virtual int? AdjdBranchID { get; set; }

  public virtual string AdjdFinPeriodID { get; set; }

  public virtual string AdjdTranPeriodID { get; set; }

  public long? AdjdCuryInfoID { get; set; }

  public long? AdjdOrigCuryInfoID { get; set; }

  public long? AdjgCuryInfoID { get; set; }

  public DateTime? AdjgDocDate { get; set; }

  public DateTime? AdjdDocDate { get; set; }

  public Decimal? CuryAdjgAmt { get; set; }

  public Decimal? CuryAdjgDiscAmt { get; set; }

  public Decimal? CuryAdjdAmt { get; set; }

  public Decimal? CuryAdjdDiscAmt { get; set; }

  public Decimal? AdjAmt { get; set; }

  public Decimal? AdjDiscAmt { get; set; }

  public Decimal? RGOLAmt { get; set; }

  public bool? Released { get; set; }

  public bool? Voided { get; set; }

  public bool? ReverseGainLoss { get; set; }

  public Decimal? CuryDocBal { get; set; }

  public Decimal? DocBal { get; set; }

  public Decimal? CuryDiscBal { get; set; }

  public Decimal? DiscBal { get; set; }

  public Decimal? CuryAdjgWhTaxAmt { get; set; }

  public Decimal? CuryAdjdWhTaxAmt { get; set; }

  public Decimal? AdjWhTaxAmt { get; set; }

  public Decimal? CuryWhTaxBal { get; set; }

  public Decimal? WhTaxBal { get; set; }

  public Decimal? CuryAdjgPPDAmt { get; set; }

  public bool? AdjdHasPPDTaxes { get; set; }

  public Decimal? AdjdCuryRate { get; set; }

  public Decimal? AdjPPDAmt { get; set; }

  public Decimal? CuryAdjdPPDAmt { get; set; }

  public bool? VoidAppl { get; set; }

  public abstract class adjgBranchID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjgFinPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjgTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdBranchID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdFinPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdCuryInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdOrigCuryInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjgCuryInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjgDocDate : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdDocDate : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjgAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjgDiscAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjdAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjdDiscAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class adjAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class adjDiscAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class rGOLAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class released : IBqlField, IBqlOperand
  {
  }

  public abstract class voided : IBqlField, IBqlOperand
  {
  }

  public abstract class reverseGainLoss : IBqlField, IBqlOperand
  {
  }

  public abstract class curyDocBal : IBqlField, IBqlOperand
  {
  }

  public abstract class docBal : IBqlField, IBqlOperand
  {
  }

  public abstract class curyDiscBal : IBqlField, IBqlOperand
  {
  }

  public abstract class discBal : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjgWhTaxAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjdWhTaxAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class adjWhTaxAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyWhTaxBal : IBqlField, IBqlOperand
  {
  }

  public abstract class whTaxBal : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjgPPDAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdHasPPDTaxes : IBqlField, IBqlOperand
  {
  }

  public abstract class adjdCuryRate : IBqlField, IBqlOperand
  {
  }

  public abstract class adjPPDAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAdjdPPDAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class voidAppl : IBqlField, IBqlOperand
  {
  }
}
