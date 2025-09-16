// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXEntitySearchEnriched
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
public class PXEntitySearchEnriched : PXEntitySearch, IEntitySearchService
{
  protected override EntitySearchResult InstantiateEntitySearchResult(
    string screenId,
    SearchIndex index,
    string title,
    string path,
    string line1,
    string line2)
  {
    Guid noteID = index.NoteID.Value;
    string entityType = index.EntityType;
    string content = index.Content;
    return new EntitySearchResult(screenId, noteID, title, path, line1, line2, entityType, content);
  }

  public string GetSearchIndexContent(Guid entityNoteId)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (SearchIndex), (PXDataField) new PXDataField<SearchIndex.content>(), (PXDataField) new PXDataFieldValue<SearchIndex.noteID>((object) entityNoteId)))
      return pxDataRecord?.GetString(0);
  }

  public List<EntitySearchResult> Search(string query, int first, int count, string entityType)
  {
    this.EntityType = entityType;
    return this.Search(query, first, count);
  }
}
