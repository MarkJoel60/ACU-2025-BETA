// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocale
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Represents a single locale available in the system.</summary>
public class PXLocale
{
  public readonly string Name;
  public readonly string DisplayName;
  public readonly short Number;
  public readonly bool? ShowValidationWarnings;

  public PXLocale(string name, string displayName, short number, bool? showValidationWarnings)
  {
    this.Name = name;
    this.DisplayName = displayName;
    this.Number = number;
    this.ShowValidationWarnings = showValidationWarnings;
  }
}
