// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.MailSendProcessingGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions.CRCaseCommitments;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXInternalUseOnly]
public class MailSendProcessingGraph : PXGraph<MailSendProcessingGraph>
{
  public MailSendProcessingGraph() => ((PXGraph) this).Defaults.Remove(typeof (AccessInfo));

  public virtual void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.ForceActivityStatisticsRecalculation();
      ((PXGraph) this).Persist();
      transactionScope.Complete();
    }
  }

  public virtual void ForceActivityStatisticsRecalculation()
  {
    GraphHelper.EnsureCachePersistence<CRActivityStatistics>((PXGraph) this);
    GraphHelper.EnsureCachePersistence<CRActivity>((PXGraph) this);
    if (!(((PXGraph) this).Caches[typeof (SMEmail)].Current is SMEmail current))
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (CRActivity)];
    CRActivity crActivity = CRActivity.PK.Find((PXGraph) this, current.RefNoteID);
    if (crActivity == null)
      return;
    crActivity.CompletedDate = PXFormulaAttribute.Evaluate<CRActivity.completedDate>(cach, (object) crActivity) as DateTime?;
    cach.Update((object) crActivity);
  }

  public class CRCaseCommitments : CRCaseCommitmentsExt<MailSendProcessingGraph, CRActivity>
  {
    public static bool IsActive()
    {
      return CRCaseCommitmentsExt<MailSendProcessingGraph>.IsExtensionActive();
    }

    public override CRActivity? TryGetActivity()
    {
      return ((PXGraph) this.Base).Caches[typeof (SMEmail)].Current is SMEmail current ? CRActivity.PK.Find((PXGraph) this.Base, current.RefNoteID) : (CRActivity) null;
    }
  }
}
