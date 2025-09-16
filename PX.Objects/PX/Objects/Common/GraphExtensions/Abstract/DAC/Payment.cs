// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.Payment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class Payment : PXMappedCacheExtension
{
  public virtual int? BranchID { get; set; }

  public virtual DateTime? AdjDate { get; set; }

  public virtual string AdjFinPeriodID { get; set; }

  public virtual string AdjTranPeriodID { get; set; }

  public virtual string CuryID { get; set; }

  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjDate : IBqlField, IBqlOperand
  {
  }

  public abstract class adjFinPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class curyID : IBqlField, IBqlOperand
  {
  }
}
