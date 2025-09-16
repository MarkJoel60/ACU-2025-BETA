// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.ModifiedDacEntryForUserRecordsUpdate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// An entry with info regarding a modified DAC for user records update.
/// </summary>
internal class ModifiedDacEntryForUserRecordsUpdate
{
  /// <summary>Gets the type of the DAC modification.</summary>
  /// <value>The type of the DAC modification.</value>
  public DacModificationType ModificationType { get; private set; }

  /// <summary>Gets the NoteID value of the modified DAC.</summary>
  /// <value>The NoteID value of the modified DAC.</value>
  public Guid NoteID { get; }

  /// <summary>Gets the type of the modified DAC.</summary>
  /// <value>The type of the modified DAC.</value>
  public System.Type EntityType { get; }

  /// <summary>Gets the cached content.</summary>
  /// <value>The cached content.</value>
  public string CachedContent { get; private set; }

  public ModifiedDacEntryForUserRecordsUpdate(
    DacModificationType dacModificationType,
    Guid noteID,
    System.Type entityType,
    string cachedContent)
  {
    this.ModificationType = dacModificationType == DacModificationType.Update || dacModificationType == DacModificationType.Delete ? dacModificationType : throw new ArgumentOutOfRangeException(nameof (dacModificationType), (object) dacModificationType, $"Value {dacModificationType} is not supported");
    this.NoteID = noteID;
    this.EntityType = ExceptionExtensions.CheckIfNull<System.Type>(entityType, nameof (entityType), (string) null);
    this.CachedContent = Str.NullIfWhitespace(cachedContent);
  }

  public void Update(DacModificationType dacModificationType, string cachedContent)
  {
    if (this.ModificationType == DacModificationType.Delete)
      return;
    this.ModificationType = dacModificationType;
    this.CachedContent = Str.NullIfWhitespace(cachedContent);
  }
}
