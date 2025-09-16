// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.DiscrepancyByAccountEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class DiscrepancyByAccountEnqResult : GLTranR, IDiscrepancyEnqResult
{
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "GL Turnover")]
  public virtual Decimal? GLTurnover { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Module Turnover")]
  public virtual Decimal? XXTurnover { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Non-Module Transactions")]
  public virtual Decimal? NonXXTrans { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discrepancy")]
  public virtual Decimal? Discrepancy
  {
    get
    {
      Decimal? glTurnover = this.GLTurnover;
      Decimal? discrepancy = this.XXTurnover;
      Decimal? nullable = glTurnover.HasValue & discrepancy.HasValue ? new Decimal?(glTurnover.GetValueOrDefault() - discrepancy.GetValueOrDefault()) : new Decimal?();
      Decimal? nonXxTrans = this.NonXXTrans;
      if (nullable.HasValue & nonXxTrans.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - nonXxTrans.GetValueOrDefault());
      discrepancy = new Decimal?();
      return discrepancy;
    }
  }

  public abstract class gLTurnover : 
    BqlType<IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyByAccountEnqResult.gLTurnover>
  {
  }

  public abstract class aPTurnover : IBqlField, IBqlOperand
  {
  }

  public abstract class nonXXTrans : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyByAccountEnqResult.nonXXTrans>
  {
  }

  public abstract class discrepancy : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyByAccountEnqResult.discrepancy>
  {
  }
}
