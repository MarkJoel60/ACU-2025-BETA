// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.IUserRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>Interface for user records of entities.</summary>
[PXInternalUseOnly]
public interface IUserRecord
{
  Guid? RefNoteId { get; }

  string EntityType { get; }

  Guid? UserID { get; }

  string RecordContent { get; }

  System.DateTime? CreatedDateTime { get; }

  string CreatedByScreenID { get; }
}
