// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class TranslationRelease : PXGraph<TranslationRelease>
{
  public PXAction<TranslationHistory> cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (TranslationHistory.referenceNbr))]
  public PXProcessing<TranslationHistory, Where<TranslationHistory.released, Equal<False>>> TranslationReleaseList;
  public PXSetup<PX.Objects.CM.CMSetup> CMSetup;

  public TranslationRelease()
  {
    PX.Objects.CM.CMSetup current = ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<TranslationHistory>) this.TranslationReleaseList).SetProcessDelegate(TranslationRelease.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (TranslationRelease.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXProcessingBase<TranslationHistory>.ProcessItemDelegate((object) TranslationRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__3_0))));
    ((PXProcessing<TranslationHistory>) this.TranslationReleaseList).SetProcessCaption("Release");
    ((PXProcessing<TranslationHistory>) this.TranslationReleaseList).SetProcessAllVisible(false);
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.TranslationReleaseList).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }
}
