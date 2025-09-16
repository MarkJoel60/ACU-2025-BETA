// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <summary>
/// Allow show acticles of certain wiki.
/// Permissions for this records are validated.
/// </summary>
/// <example>[PXWikiSelector]</example>
public class PXWikiSelectorAttribute : PXCustomSelectorAttribute
{
  private bool checkRights = true;
  private int wikiArticleType = -1;

  public PXWikiSelectorAttribute()
    : base(typeof (WikiDescriptor.pageID))
  {
  }

  public PXWikiSelectorAttribute(int wikiArticleType)
    : this()
  {
    this.wikiArticleType = wikiArticleType;
  }

  public PXWikiSelectorAttribute(System.Type type)
    : base(type)
  {
  }

  public PXWikiSelectorAttribute(int wikiArticleType, System.Type type)
    : this(type)
  {
    this.wikiArticleType = wikiArticleType;
  }

  public bool CheckRights
  {
    get => this.checkRights;
    set => this.checkRights = value;
  }

  protected IEnumerable GetRecords()
  {
    PXWikiSelectorAttribute selectorAttribute = this;
    foreach (PXResult<WikiDescriptor> pxResult in PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor>.Config>.Select(selectorAttribute._Graph))
    {
      WikiDescriptor record = (WikiDescriptor) pxResult;
      if (selectorAttribute.wikiArticleType != -1)
      {
        int? wikiArticleType1 = record.WikiArticleType;
        int wikiArticleType2 = selectorAttribute.wikiArticleType;
        if (!(wikiArticleType1.GetValueOrDefault() == wikiArticleType2 & wikiArticleType1.HasValue))
          continue;
      }
      if (record.PageID.HasValue && !string.IsNullOrEmpty(record.Name) && (!selectorAttribute.CheckRights || PXSiteMap.WikiProvider.GetAccessRights(record.PageID.Value) >= PXWikiRights.Select))
      {
        record.Title = record.WikiTitle;
        yield return (object) record;
      }
    }
  }
}
