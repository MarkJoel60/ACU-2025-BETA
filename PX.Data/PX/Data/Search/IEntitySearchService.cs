// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.IEntitySearchService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

[PXInternalUseOnly]
public interface IEntitySearchService
{
  List<EntitySearchResult> Search(string query, int first, int count, string entityType);

  string GetSearchIndexContent(Guid entityNoteId);
}
