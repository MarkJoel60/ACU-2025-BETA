// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.ArticleUpdater
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.SM;

public class ArticleUpdater : PXGraph<ArticleUpdater>
{
  public PXSelect<WikiRevision, Where<WikiRevision.pageID, Equal<Required<WikiRevision.pageID>>, And<WikiRevision.pageRevisionID, Equal<Required<WikiRevision.pageRevisionID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>>>>> Revision;
}
