// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.KeyWithAlias
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Diagnostics;

#nullable disable
namespace PX.PushNotifications;

/// <exclude />
[PXInternalUseOnly]
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}", Name = "{FieldName}")]
public sealed class KeyWithAlias
{
  private const string _noteIdField = "_NoteIdFieldOfTheRecord_";
  private const string _entityTypeField = "_EntityTypeFieldOfTheRecord_";

  public string Alias { get; set; }

  [Obsolete("For deserialization only")]
  public KeyWithAlias()
  {
  }

  public KeyWithAlias(string fieldName, string alias)
  {
    this.Alias = alias;
    this.FieldName = fieldName;
  }

  public KeyWithAlias(string fieldName)
  {
    this.FieldName = fieldName;
    this.Alias = fieldName;
  }

  public string FieldName { get; set; }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string DebuggerDisplay => $"{this.FieldName ?? "null"}/{this.Alias ?? "null"}";

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case KeyWithAlias keyWithAlias:
        return StringComparer.OrdinalIgnoreCase.Equals(this.FieldName, keyWithAlias.FieldName);
      case string y:
        return StringComparer.OrdinalIgnoreCase.Equals(this.FieldName, y);
      default:
        return base.Equals(obj);
    }
  }

  public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(this.FieldName);

  internal static KeyWithAlias GetNoteIdFieldKey() => new KeyWithAlias("_NoteIdFieldOfTheRecord_");

  internal static KeyWithAlias GetEntityTypeFieldKey()
  {
    return new KeyWithAlias("_EntityTypeFieldOfTheRecord_");
  }

  internal static bool IsNoteIdFieldKey(KeyWithAlias key)
  {
    return string.Equals(key.FieldName, "_NoteIdFieldOfTheRecord_", StringComparison.Ordinal);
  }

  internal static bool IsEntityTypeFieldKey(KeyWithAlias key)
  {
    return string.Equals(key.FieldName, "_EntityTypeFieldOfTheRecord_", StringComparison.Ordinal);
  }
}
