// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiDefaultUrlAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXWikiDefaultUrlAttribute : PXCustomSelectorAttribute
{
  public PXWikiDefaultUrlAttribute()
    : base(typeof (WikiPageSimple.pageID))
  {
    this.DescriptionField = typeof (WikiPageSimple.title);
  }

  public IEnumerable GetRecords()
  {
    WikiPageSimple record = (WikiPageSimple) PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<Current<WikiDescriptor.defaultUrl>>>>.Config>.Select(this._Graph);
    if ((record != null ? (!record.PageID.HasValue ? 1 : 0) : 1) == 0)
    {
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(record.PageID.Value);
      record.Title = siteMapNodeFromKey == null ? record.Name : siteMapNodeFromKey.Title;
      yield return (object) record;
    }
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
