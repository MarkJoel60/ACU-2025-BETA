// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.FullTextIndexRebuild
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Search;
using PX.Objects.BQLConstants;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.SM;

public class FullTextIndexRebuild : PXGraph<FullTextIndexRebuild>
{
  public PXCancel<FullTextIndexRebuildProc.RecordType> Cancel;
  public PXProcessing<FullTextIndexRebuildProc.RecordType> Items;
  public PXSelectJoin<WikiPage, InnerJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiRevision, On<WikiRevision.pageID, Equal<WikiPage.pageID>>>>, Where<WikiRevision.plainText, Equal<EmptyString>, And<WikiRevision.pageRevisionID, Equal<WikiPageLanguage.lastRevisionID>>>> WikiArticles;
  public PXAction<FullTextIndexRebuildProc.RecordType> clearAllIndexes;
  public PXAction<FullTextIndexRebuildProc.RecordType> indexCustomArticles;
  public PXAction<FullTextIndexRebuildProc.RecordType> restartFts;

  [InjectDependency]
  private ISearchManagementService SearchManagementService { get; set; }

  public virtual IEnumerable items()
  {
    FullTextIndexRebuild textIndexRebuild = this;
    bool found = false;
    foreach (FullTextIndexRebuildProc.RecordType recordType in ((PXSelectBase) textIndexRebuild.Items).Cache.Inserted)
    {
      found = true;
      yield return (object) recordType;
    }
    if (!found)
    {
      foreach (Type searchableEntity in PXSearchableAttribute.GetAllSearchableEntities((PXGraph) textIndexRebuild))
        yield return (object) ((PXSelectBase<FullTextIndexRebuildProc.RecordType>) textIndexRebuild.Items).Insert(new FullTextIndexRebuildProc.RecordType()
        {
          Entity = searchableEntity.FullName,
          Name = searchableEntity.Name,
          DisplayName = ((PXGraph) textIndexRebuild).Caches[searchableEntity].DisplayName
        });
      ((PXSelectBase) textIndexRebuild.Items).Cache.IsDirty = false;
    }
  }

  public FullTextIndexRebuild()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<FullTextIndexRebuildProc.RecordType>) this.Items).SetProcessDelegate<FullTextIndexRebuildProc>(new PXProcessingBase<FullTextIndexRebuildProc.RecordType>.ProcessItemDelegate<FullTextIndexRebuildProc>((object) null, __methodptr(BuildIndex)));
  }

  [PXUIField]
  [PXButton(Tooltip = "Clears all indexes in the system.", Category = "Actions")]
  public virtual IEnumerable ClearAllIndexes(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, FullTextIndexRebuild.\u003C\u003Ec.\u003C\u003E9__10_0 ?? (FullTextIndexRebuild.\u003C\u003Ec.\u003C\u003E9__10_0 = new PXToggleAsyncDelegate((object) FullTextIndexRebuild.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CClearAllIndexes\u003Eb__10_0))));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Index Custom Articles", Category = "Actions")]
  public virtual IEnumerable IndexCustomArticles(PXAdapter adapter)
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CIndexCustomArticles\u003Eb__12_0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Restart Full-Text Search", Category = "Actions")]
  public virtual IEnumerable RestartFts(PXAdapter adapter)
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRestartFts\u003Eb__14_0)));
    return adapter.Get();
  }
}
