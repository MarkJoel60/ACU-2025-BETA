// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.Adjust2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class Adjust2 : PXMappedCacheExtension
{
  public virtual int? AdjgBranchID { get; set; }

  public virtual string AdjgFinPeriodID { get; set; }

  public virtual string AdjgTranPeriodID { get; set; }

  public virtual int? AdjdBranchID { get; set; }

  public virtual string AdjdFinPeriodID { get; set; }

  public virtual string AdjdTranPeriodID { get; set; }

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
}
