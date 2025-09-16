// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyByVendorEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class APGLDiscrepancyByVendorEnqResult : 
  APVendorBalanceEnq.APHistoryResult,
  IDiscrepancyEnqResult
{
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "GL Turnover")]
  public virtual Decimal? GLTurnover { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "AP Turnover")]
  public virtual Decimal? XXTurnover { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discrepancy")]
  public virtual Decimal? Discrepancy
  {
    get
    {
      Decimal? glTurnover = this.GLTurnover;
      Decimal? xxTurnover = this.XXTurnover;
      return !(glTurnover.HasValue & xxTurnover.HasValue) ? new Decimal?() : new Decimal?(glTurnover.GetValueOrDefault() - xxTurnover.GetValueOrDefault());
    }
  }

  public abstract class gLTurnover : 
    BqlType<IBqlDecimal, Decimal>.Field<
    #nullable disable
    APGLDiscrepancyByVendorEnqResult.gLTurnover>
  {
  }

  public abstract class xXTurnover : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APGLDiscrepancyByVendorEnqResult.xXTurnover>
  {
  }

  public abstract class discrepancy : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APGLDiscrepancyByVendorEnqResult.discrepancy>
  {
  }
}
