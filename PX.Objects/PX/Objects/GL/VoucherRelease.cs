// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.VoucherRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class VoucherRelease : PXGraph<VoucherRelease>
{
  public PXCancel<GLDocBatch> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<GLDocBatch> EditDetail;
  [PXFilterable(new Type[] {})]
  public PXProcessing<GLDocBatch, Where<GLDocBatch.hold, Equal<boolFalse>, And<GLDocBatch.released, Equal<boolFalse>>>> Documents;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(Events.CacheAttached<GLDocBatch.branchID> e)
  {
  }

  public static void ReleaseVoucher(GLBatchDocRelease graph, GLDocBatch batch)
  {
    graph.ReleaseBatchProc(batch, false);
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<GLDocBatch>) this.Documents).Current != null)
    {
      JournalWithSubEntry instance = PXGraph.CreateInstance<JournalWithSubEntry>();
      ((PXSelectBase<GLDocBatch>) instance.BatchModule).Current = PXResultset<GLDocBatch>.op_Implicit(((PXSelectBase<GLDocBatch>) instance.BatchModule).Search<GLDocBatch.batchNbr>((object) ((PXSelectBase<GLDocBatch>) this.Documents).Current.BatchNbr, new object[1]
      {
        (object) ((PXSelectBase<GLDocBatch>) this.Documents).Current.Module
      }));
      if (((PXSelectBase<GLDocBatch>) instance.BatchModule).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewBatch");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public VoucherRelease()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<GLDocBatch>) this.Documents).SetProcessDelegate<GLBatchDocRelease>(new PXProcessingBase<GLDocBatch>.ProcessItemDelegate<GLBatchDocRelease>((object) null, __methodptr(ReleaseVoucher)));
    ((PXProcessing<GLDocBatch>) this.Documents).SetProcessCaption("Release");
    ((PXProcessing<GLDocBatch>) this.Documents).SetProcessAllCaption("Release All");
  }
}
