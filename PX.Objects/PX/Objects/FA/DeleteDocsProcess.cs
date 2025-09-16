// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DeleteDocsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.FA;

public class DeleteDocsProcess : PXGraph<DeleteDocsProcess>
{
  public PXCancel<FARegister> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (FARegister.refNbr))]
  public PXProcessing<FARegister, Where<FARegister.released, NotEqual<True>>> Docs;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(Events.CacheAttached<FARegister.branchID> e)
  {
  }

  public DeleteDocsProcess()
  {
    ((PXProcessing<FARegister>) this.Docs).SetProcessCaption("Delete");
    ((PXProcessing<FARegister>) this.Docs).SetProcessAllCaption("Delete All");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<FARegister>) this.Docs).SetProcessDelegate(DeleteDocsProcess.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (DeleteDocsProcess.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXProcessingBase<FARegister>.ProcessListDelegate((object) DeleteDocsProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__3_0))));
  }
}
