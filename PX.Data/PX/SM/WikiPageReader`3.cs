// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageReader`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiPageReader<Base, Primary, Where> : 
  WikiPageReader<Base, WikiPage.pageID, WikiPage.wikiID, WikiPage.name, Primary, Where>
  where Base : WikiPage, new()
  where Primary : class, IBqlTable, new()
  where Where : class, IBqlWhere, new()
{
}
