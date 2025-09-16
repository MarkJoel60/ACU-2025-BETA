// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPersistingCheck
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines different ways the <tt>PXDefault</tt> attribute
/// checks the field value before a data record with this field is saved
/// to the database.</summary>
public enum PXPersistingCheck
{
  /// <summary>Check that the field value is not <tt>null</tt>.</summary>
  /// <remarks>Note that the user interface (UI) trims string values, so for
  /// fields displayed in the UI, values containing only whitespace
  /// characters will also be rejected.</remarks>
  Null,
  /// <summary>Check that the field value is not <tt>null</tt> and is not a
  /// string that contains only whitespace characters.</summary>
  NullOrBlank,
  /// <summary>Do not check the field value.</summary>
  Nothing,
}
