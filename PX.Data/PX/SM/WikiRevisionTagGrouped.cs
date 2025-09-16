// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiRevisionTagGrouped
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select5<WikiRevisionTag, InnerJoin<WikiTag, On<WikiTag.tagID, Equal<WikiRevisionTag.tagID>, And<WikiTag.wikiID, Equal<WikiRevisionTag.wikiID>>>>, Aggregate<GroupBy<WikiRevisionTag.wikiID, GroupBy<WikiRevisionTag.tagID, GroupBy<WikiRevisionTag.pageID, Max<WikiRevisionTag.pageRevisionID>>>>>>))]
[Serializable]
public class WikiRevisionTagGrouped : WikiRevisionTag
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiPage.pageID))]
  public override Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBInt(IsKey = true)]
  public override int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  public new abstract class pageID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevisionTagGrouped.pageID>
  {
  }

  public new abstract class pageRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiRevisionTagGrouped.pageRevisionID>
  {
  }
}
