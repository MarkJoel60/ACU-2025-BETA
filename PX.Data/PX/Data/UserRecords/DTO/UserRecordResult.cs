// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.DTO.UserRecordResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.UserRecords.DTO;

[PXInternalUseOnly]
public class UserRecordResult
{
  public string EntityName { get; set; }

  public string EntityType { get; set; }

  public string EntityDescription { get; set; }

  public Guid NoteID { get; set; }

  public string ScreenID { get; set; }

  public List<KeyValuePair<string, string>> SearchableInfo { get; } = new List<KeyValuePair<string, string>>(8);
}
