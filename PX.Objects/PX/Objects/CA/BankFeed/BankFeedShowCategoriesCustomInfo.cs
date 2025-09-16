// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedShowCategoriesCustomInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class BankFeedShowCategoriesCustomInfo : IPXCustomInfo
{
  private IEnumerable<BankFeedCategory> categoryList;

  public BankFeedShowCategoriesCustomInfo(IEnumerable<BankFeedCategory> categoryList)
  {
    this.categoryList = categoryList;
  }

  public void Complete(PXLongRunStatus status, PXGraph graph)
  {
    if (!(graph is CABankFeedMaint caBankFeedMaint) || status != 2 || ((PXSelectBase) caBankFeedMaint.BankFeedCategories).Cache.Cached.Count() > 0L)
      return;
    foreach (BankFeedCategory category in this.categoryList)
      GraphHelper.Hold(((PXSelectBase) caBankFeedMaint.BankFeedCategories).Cache, (object) category);
    ((PXSelectBase<BankFeedCategory>) caBankFeedMaint.BankFeedCategories).AskExt(true);
  }
}
