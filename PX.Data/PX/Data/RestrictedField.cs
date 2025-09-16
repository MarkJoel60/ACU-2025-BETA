// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictedField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// This class represents an item of a fields set used by the <tt>PXFieldScope</tt> class.
/// </summary>
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
public class RestrictedField : IEquatable<RestrictedField>
{
  /// <summary>Returns the table to which the field belongs.</summary>
  public System.Type Table { get; }

  /// <summary>Returns the field name.</summary>
  public string Field { get; }

  /// <exclude />
  public RestrictedField(System.Type table, string field)
  {
    if (table == (System.Type) null)
      throw new ArgumentNullException(nameof (table));
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    this.Table = table;
    this.Field = field;
  }

  /// <exclude />
  public RestrictedField(string field)
    : this(typeof (RestrictedField.AnyType), field)
  {
  }

  /// <exclude />
  public bool Equals(RestrictedField other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return this.Table == other.Table && string.Equals(this.Field, other.Field, StringComparison.OrdinalIgnoreCase);
  }

  /// <exclude />
  public override int GetHashCode()
  {
    return (17 * 23 + this.Table.GetHashCode()) * 23 + StringComparer.OrdinalIgnoreCase.GetHashCode(this.Field);
  }

  /// <exclude />
  public override bool Equals(object obj) => obj is RestrictedField other && this.Equals(other);

  private string DebuggerDisplay
  {
    get
    {
      string str = this.Table.Name;
      if (this.Table == typeof (RestrictedField.AnyType))
        str = "Any Type";
      return $"{str} | {this.Field}";
    }
  }

  private class AnyType
  {
  }
}
