// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INABCTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INABCTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _TotalABCPct;

  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Total ABC Code %", Enabled = false)]
  public virtual Decimal? TotalABCPct
  {
    get => this._TotalABCPct;
    set => this._TotalABCPct = value;
  }

  public abstract class totalABCPct : BqlType<IBqlDecimal, Decimal>.Field<
  #nullable disable
  INABCTotal.totalABCPct>
  {
  }
}
