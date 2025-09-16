// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.Data.TaxYearWithPeriods`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX.Data;

public class TaxYearWithPeriods<TTaxYear, TTaxPeriod>
  where TTaxYear : PX.Objects.TX.TaxYear
  where TTaxPeriod : TaxPeriod
{
  public TTaxYear TaxYear { get; set; }

  public List<TTaxPeriod> TaxPeriods { get; set; }

  public TaxYearWithPeriods() => this.TaxPeriods = new List<TTaxPeriod>();
}
