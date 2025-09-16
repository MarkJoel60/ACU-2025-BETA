// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.SingleTagSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.Wiki.Tags;

public class SingleTagSelectorAttribute : PXCustomSelectorAttribute
{
  public virtual bool IncludeAllTags { get; set; }

  public virtual bool IgnoreAccessRights { get; set; }

  public SingleTagSelectorAttribute(bool hasSubstitute = false)
    : base(typeof (Tag.tagID))
  {
    if (hasSubstitute)
    {
      this.SubstituteKey = typeof (Tag.tagCD);
    }
    else
    {
      this.DescriptionField = typeof (Tag.tagCD);
      this.SelectorMode = PXSelectorMode.DisplayModeText;
    }
  }

  public virtual IEnumerable GetRecords()
  {
    SingleTagSelectorAttribute selectorAttribute = this;
    if (selectorAttribute.IncludeAllTags)
      yield return (object) CommonTags.AllTags;
    Dictionary<Guid, short> accessRightsMap = TagsSlot.GetTagsAccessForUser(PXContext.PXIdentity.AuthUser);
    foreach (PXResult<Tag> pxResult in PXSelectBase<Tag, PXSelectReadonly<Tag>.Config>.Select(selectorAttribute._Graph))
    {
      Tag record = (Tag) pxResult;
      if (selectorAttribute.IgnoreAccessRights || TagsSlot.CanViewTag(record.TagID, (IDictionary<Guid, short>) accessRightsMap))
        yield return (object) record;
    }
  }
}
