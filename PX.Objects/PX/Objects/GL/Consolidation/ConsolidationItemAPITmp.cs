// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolidationItemAPITmp
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL.Consolidation;

internal class ConsolidationItemAPITmp
{
  public virtual ApiProperty<string> AccountCD { get; set; }

  public virtual ApiProperty<Decimal?> ConsolAmtCredit { get; set; }

  public virtual ApiProperty<Decimal?> ConsolAmtDebit { get; set; }

  public virtual ApiProperty<string> FinPeriodID { get; set; }

  public virtual ApiProperty<string> MappedValue { get; set; }

  public virtual ApiProperty<int?> MappedValueLength { get; set; }

  public ConsolidationItemAPI ToApiItem()
  {
    return new ConsolidationItemAPI()
    {
      AccountCD = this.AccountCD.value,
      ConsolAmtCredit = this.ConsolAmtCredit.value,
      ConsolAmtDebit = this.ConsolAmtDebit.value,
      FinPeriodID = string.IsNullOrEmpty(this.FinPeriodID.value) ? string.Empty : this.FinPeriodID.value.Substring(2, 4) + this.FinPeriodID.value.Substring(0, 2),
      MappedValue = this.MappedValue.value,
      MappedValueLength = this.MappedValueLength.value
    };
  }
}
