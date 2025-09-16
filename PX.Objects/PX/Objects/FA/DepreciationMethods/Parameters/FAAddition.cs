// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.FAAddition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

/// <exclude />
public class FAAddition
{
  protected int Precision;
  public AdditionParameters CalculatedAdditionParameters;

  public FAAddition(
    Decimal amount,
    string finPeriodID,
    DateTime date,
    int precision,
    Decimal businessUse)
  {
    this.Amount = amount;
    this.PeriodID = finPeriodID;
    this.Date = date;
    this.Precision = precision;
    this.BusinessUse = businessUse * 0.01M;
  }

  public string PeriodID { get; protected set; }

  public DateTime Date { get; protected set; }

  public Decimal Amount { get; set; }

  public bool IsOriginal { get; protected set; }

  public Decimal SalvageAmount { get; protected set; }

  public Decimal Section179Amount { get; protected set; }

  public Decimal BonusAmount { get; protected set; }

  public Decimal BusinessUse { get; protected set; } = 1M;

  public Decimal DepreciationBasis
  {
    get
    {
      return PXRounder.Round(this.Amount * this.BusinessUse, this.Precision) - this.Section179Amount - this.BonusAmount - this.SalvageAmount;
    }
  }

  public void MarkOriginal(FABookBalance bookBalance)
  {
    this.IsOriginal = true;
    Decimal? businessUse = bookBalance.BusinessUse;
    Decimal num = 0.01M;
    this.BusinessUse = (businessUse.HasValue ? new Decimal?(businessUse.GetValueOrDefault() * num) : new Decimal?()) ?? 1M;
    this.SalvageAmount = bookBalance.SalvageAmount.GetValueOrDefault();
    Decimal? nullable = bookBalance.Tax179Amount;
    this.Section179Amount = nullable.GetValueOrDefault();
    nullable = bookBalance.BonusAmount;
    this.BonusAmount = nullable.GetValueOrDefault();
  }

  public void MarkOriginal() => this.IsOriginal = true;
}
