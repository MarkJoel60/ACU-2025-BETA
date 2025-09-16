// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacDescriptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable enable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>
/// The DAC descriptor with main info regarding the DAC instance.
/// </summary>
[Serializable]
public readonly struct DacDescriptor : IEquatable<DacDescriptor>, IComparable<DacDescriptor>
{
  public string Value { get; }

  public bool IsNonTrivial => !Str.IsNullOrWhiteSpace(this.Value);

  public DacDescriptor(string value)
  {
    this.Value = ExceptionExtensions.CheckIfNull<string>(Str.NullIfWhitespace(value)?.Trim(), nameof (value), (string) null);
  }

  public static bool Equals(DacDescriptor x, DacDescriptor y) => x.Equals(y);

  public static int Compare(DacDescriptor x, DacDescriptor y) => x.CompareTo(y);

  public override bool Equals(object obj) => obj is DacDescriptor other && this.Equals(other);

  public bool Equals(DacDescriptor other)
  {
    return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
  }

  public int CompareTo(DacDescriptor other)
  {
    return string.Compare(this.Value, other.Value, StringComparison.Ordinal);
  }

  public override int GetHashCode()
  {
    string str = this.Value;
    return str == null ? 0 : str.GetHashCode();
  }

  public override string ToString() => Str.NullIfWhitespace(this.Value) ?? string.Empty;

  public static bool operator ==(DacDescriptor x, DacDescriptor y) => x.Equals(y);

  public static bool operator !=(DacDescriptor x, DacDescriptor y) => !x.Equals(y);
}
