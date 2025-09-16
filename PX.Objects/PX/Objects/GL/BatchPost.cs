// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchPost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class BatchPost : PXGraph<BatchPost>
{
  public PXCancel<Batch> Cancel;
  [PXViewDetailsButton(typeof (Batch.batchNbr))]
  [PXFilterable(new Type[] {})]
  public PXProcessing<Batch, Where<Batch.status, Equal<statusU>>> BatchList;
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

  public BatchPost()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<Batch>) this.BatchList).SetProcessDelegate<PostGraph>(BatchPost.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (BatchPost.\u003C\u003Ec.\u003C\u003E9__4_0 = new PXProcessingBase<Batch>.ProcessItemDelegate<PostGraph>((object) BatchPost.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__4_0))));
    ((PXProcessing<Batch>) this.BatchList).SetProcessCaption("Post");
    ((PXProcessing<Batch>) this.BatchList).SetProcessAllCaption("Post All");
    PXNoteAttribute.ForcePassThrow<Batch.noteID>(((PXSelectBase) this.BatchList).Cache);
  }
}
