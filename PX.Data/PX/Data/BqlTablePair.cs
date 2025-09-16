// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlTablePair
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
internal class BqlTablePair : ICloneable
{
  public IBqlTable Unchanged;
  public IBqlTable LastModified;
  public SessionUnmodifiedPair SessionUnmodified;
  /// <summary>
  /// CopyOfItem - contains prev version of record. If Null - record was inserted after version was created
  /// Status - status of record in moment when version was created
  /// Was Changed - indicates if it was any Update on record
  /// </summary>
  public VersionedModifiedPair VersionedModified;
  public int? FilesCount;
  public int? ActivitiesCount;
  public string NoteText;
  public string NotePopupText;
  public List<object> Slots;
  public List<object> SlotsOriginal;

  /// <summary>
  /// This field is for Push Notifications. It should be set only if this is record from temporal table.
  /// </summary>
  public bool? IsInserted { get; set; }

  public bool? IsArchived { get; set; }

  public bool? IsDeletedRecord { get; set; }

  public BqlTablePair Clone()
  {
    BqlTablePair bqlTablePair = new BqlTablePair()
    {
      LastModified = this.LastModified,
      Unchanged = this.Unchanged,
      FilesCount = this.FilesCount,
      ActivitiesCount = this.ActivitiesCount,
      NoteText = this.NoteText,
      NotePopupText = this.NotePopupText,
      IsInserted = this.IsInserted,
      IsArchived = this.IsArchived
    };
    if (this.Slots != null)
      bqlTablePair.Slots = new List<object>((IEnumerable<object>) this.Slots);
    if (this.SlotsOriginal != null)
      bqlTablePair.SlotsOriginal = new List<object>((IEnumerable<object>) this.SlotsOriginal);
    VersionedModifiedPair versionedModified = this.VersionedModified;
    if (versionedModified != null)
      bqlTablePair.VersionedModified = new VersionedModifiedPair()
      {
        Status = versionedModified.Status,
        WasChanged = versionedModified.WasChanged,
        CopyOfItem = versionedModified.CopyOfItem
      };
    SessionUnmodifiedPair sessionUnmodified = this.SessionUnmodified;
    if (sessionUnmodified != null)
      bqlTablePair.SessionUnmodified = new SessionUnmodifiedPair()
      {
        Status = sessionUnmodified.Status,
        Item = sessionUnmodified.Item
      };
    return bqlTablePair;
  }

  object ICloneable.Clone() => (object) this.Clone();
}
