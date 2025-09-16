// Decompiled with JetBrains decompiler
// Type: PX.SM.KBArticleMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

/// <exclude />
public class KBArticleMaint : WikiArticleMaintenance
{
  public PXSelectJoin<KBResponse, InnerJoin<Users, On<Users.pKID, Equal<KBResponse.userID>>>, Where<KBResponse.pageID, Equal<Current<WikiArticle.pageID>>>, OrderBy<Asc<KBResponse.date>>> Responses;
  [PXHidden]
  public PXSelect<KBResponseSummary, Where<KBResponseSummary.pageID, Equal<Required<KBResponseSummary.pageID>>>> ResponsesSummary;

  public KBArticleMaint()
  {
    PXUIFieldAttribute.SetReadOnly(this.Responses.Cache, (object) null);
    PXUIFieldAttribute.SetDisplayName<KBResponse.userID>(this.Responses.Cache, "Username");
  }

  protected virtual void WikiArticle_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is WikiArticle row))
      return;
    foreach (PXResult<KBResponse> pxResult in PXSelectBase<KBResponse, PXSelect<KBResponse, Where<KBResponse.pageID, Equal<Required<KBResponse.pageID>>>>.Config>.Select((PXGraph) this, (object) row.PageID))
      this.Responses.Delete((KBResponse) pxResult);
  }

  protected virtual void WikiArticle_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is WikiArticle row))
      return;
    row.CreatedByID = new Guid?(this.Accessinfo.UserID);
  }
}
