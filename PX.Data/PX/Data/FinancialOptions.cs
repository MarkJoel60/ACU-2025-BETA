// Decompiled with JetBrains decompiler
// Type: PX.Data.FinancialOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class FinancialOptions : ConfiguredValueBase
{
  public string FinancialSupervisorRole { get; private set; } = "Financial Supervisor";

  protected override void SetConfiguredValue(string value) => this.FinancialSupervisorRole = value;

  protected override string ConfigurationKey { get; } = "FinancialRole";
}
