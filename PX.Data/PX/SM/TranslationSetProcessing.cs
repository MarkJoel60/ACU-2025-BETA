// Decompiled with JetBrains decompiler
// Type: PX.SM.TranslationSetProcessing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile.Legacy;
using PX.Data;
using PX.Translation;
using System.Collections;
using System.Collections.Generic;
using System.Web.Hosting;

#nullable disable
namespace PX.SM;

/// <exclude />
public class TranslationSetProcessing : PXGraph<TranslationSetProcessing>
{
  public PXProcessing<LocalizationTranslationSet> TranslationSets;
  public PXCancel<LocalizationTranslationSet> Cancel;
  public PXAction<LocalizationTranslationSet> ViewTranslationSet;

  [PXButton]
  [PXUIField(DisplayName = "View Translation Set")]
  public virtual IEnumerable viewTranslationSet(PXAdapter adapter)
  {
    if (this.TranslationSets.Current != null)
    {
      TranslationSetMaint instance = PXGraph.CreateInstance<TranslationSetMaint>();
      instance.TranslationSet.Current = this.TranslationSets.Current;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
    return adapter.Get();
  }

  public TranslationSetProcessing()
  {
    Provider.PrepareSiteMap();
    this.TranslationSets.SetSelected<LocalizationTranslationSet.selected>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.TranslationSets.SetProcessDelegate(TranslationSetProcessing.\u003C\u003EO.\u003C0\u003E__ProcessTranslationSets ?? (TranslationSetProcessing.\u003C\u003EO.\u003C0\u003E__ProcessTranslationSets = new PXProcessingBase<LocalizationTranslationSet>.ProcessListDelegate(TranslationSetProcessing.ProcessTranslationSets)));
    this.TranslationSets.SetProcessCaption("Collect");
    this.TranslationSets.SetProcessAllCaption("Collect All");
  }

  public static void ProcessTranslationSets(List<LocalizationTranslationSet> list)
  {
    if (list == null)
      return;
    TranslationSetMaint instance = PXGraph.CreateInstance<TranslationSetMaint>();
    for (int index = 0; index < list.Count; ++index)
    {
      instance.TranslationSet.Current = list[index];
      TranslationSetMaint.CollectStrings(HostingEnvironment.ApplicationPhysicalPath, instance.GetSetItemsForCollecting(), instance.TranslationSet.Current);
      PXProcessing<LocalizationTranslationSet>.SetInfo(index, "The record has been processed successfully.");
    }
  }
}
