// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class BatchRelease : PXGraph<BatchRelease>
{
  public PXCancel<Batch> Cancel;
  [PXViewDetailsButton(typeof (Batch.batchNbr))]
  [PXFilterable(new Type[] {})]
  public PXProcessing<Batch, Where<Batch.released, Equal<boolFalse>, And<Batch.scheduled, Equal<boolFalse>, And<Batch.voided, Equal<boolFalse>, And<Batch.approved, Equal<boolTrue>, And<Batch.hold, Equal<boolFalse>>>>>>> BatchList;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  [PXDBBaseCury(typeof (Batch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  protected virtual void Batch_ControlTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(Events.CacheAttached<Batch.branchID> e)
  {
  }

  public BatchRelease()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<Batch>) this.BatchList).SetProcessDelegate<PostGraph>(new PXProcessingBase<Batch>.ProcessItemDelegate<PostGraph>((object) null, __methodptr(ReleaseBatch)));
    ((PXProcessing<Batch>) this.BatchList).SetProcessCaption("Release");
    ((PXProcessing<Batch>) this.BatchList).SetProcessAllCaption("Release All");
    PXNoteAttribute.ForcePassThrow<Batch.noteID>(((PXSelectBase) this.BatchList).Cache);
  }

  public static void ReleaseBatch(PostGraph pg, Batch batch)
  {
    ((PXGraph) pg).Clear();
    pg.ReleaseBatchProc(batch);
    if (batch.AutoReverse.Value)
    {
      Batch b = pg.ReverseBatchProc(batch);
      if (pg.AutoPost)
        pg.PostBatchProc(batch);
      ((PXGraph) pg).Clear();
      ((PXGraph) pg).TimeStamp = b.tstamp;
      pg.ReleaseBatchProc(b);
      if (!pg.AutoPost)
        return;
      pg.PostBatchProc(b);
    }
    else
    {
      if (!pg.AutoPost)
        return;
      pg.PostBatchProc(batch);
    }
  }
}
