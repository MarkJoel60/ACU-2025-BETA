// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.TableWithKeys
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

/// <summary>
/// Represents one side of a referencing relationship (i.e. <see cref="T:PX.Data.ReferentialIntegrity.Reference" />) between two <see cref="T:PX.Data.IBqlTable" />s
/// </summary>
[ImmutableObject(true)]
[DebuggerDisplay("{ToString()}")]
[PXInternalUseOnly]
public struct TableWithKeys : IEquatable<TableWithKeys>
{
  internal TableWithKeys(System.Type table, IEnumerable<System.Type> keyFields)
  {
    this.Table = table;
    this.KeyFields = (IReadOnlyCollection<System.Type>) new List<System.Type>(keyFields ?? Enumerable.Empty<System.Type>()).AsReadOnly();
  }

  public System.Type Table { get; }

  public IReadOnlyCollection<System.Type> KeyFields { get; }

  public string KeyFieldsToString
  {
    get
    {
      System.Type table = this.Table;
      return string.Join(", ", this.KeyFields.Select<System.Type, string>((Func<System.Type, string>) (t => !(t.DeclaringType != (System.Type) null) || !(t.DeclaringType != table) ? t.Name : $"{t.DeclaringType.Name}+{t.Name}")));
    }
  }

  public override string ToString() => $"{this.Table.Name}({this.KeyFieldsToString})";

  public bool Equals(TableWithKeys other)
  {
    if (!(this.Table == other.Table))
      return false;
    if (this.KeyFields == null && other.KeyFields == null)
      return true;
    return this.KeyFields != null && other.KeyFields != null && this.KeyFields.SequenceEqual<System.Type>((IEnumerable<System.Type>) other.KeyFields);
  }

  public override bool Equals(object obj)
  {
    return obj != null && obj is TableWithKeys other && this.Equals(other);
  }

  public override int GetHashCode()
  {
    System.Type table = this.Table;
    int num = ((object) table != null ? table.GetHashCode() : 0) * 397;
    IReadOnlyCollection<System.Type> keyFields = this.KeyFields;
    int hashCodeOfSequence = keyFields != null ? EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) keyFields) : 0;
    return num ^ hashCodeOfSequence;
  }

  public static bool operator ==(TableWithKeys left, TableWithKeys right)
  {
    return object.Equals((object) left, (object) right);
  }

  public static bool operator !=(TableWithKeys left, TableWithKeys right)
  {
    return !object.Equals((object) left, (object) right);
  }
}
