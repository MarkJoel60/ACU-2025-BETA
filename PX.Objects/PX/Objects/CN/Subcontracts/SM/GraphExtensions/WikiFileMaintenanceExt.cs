// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SM.GraphExtensions.WikiFileMaintenanceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SM.GraphExtensions;

public class WikiFileMaintenanceExt : PXGraphExtension<WikiFileMaintenance>
{
  public PXSelect<NoteDoc> Entities;
  public PXAction<UploadFileWithIDSelector> ViewEntityGraph;
  private EntityHelper entityHelper;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize() => this.entityHelper = new EntityHelper((PXGraph) this.Base);

  protected internal IEnumerable entitiesRecords()
  {
    WikiFileMaintenanceExt fileMaintenanceExt = this;
    foreach (NoteDoc entitiesRecord in fileMaintenanceExt.Base.entitiesRecords())
    {
      fileMaintenanceExt.UpdateNoteDocumentForSubcontract(entitiesRecord);
      yield return (object) entitiesRecord;
    }
  }

  [PXOverride]
  public virtual IEnumerable viewEntity(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseHandler)
  {
    if (((PXSelectBase<NoteDoc>) this.Entities).Current != null)
    {
      POOrder commitment = this.GetCommitment(((PXSelectBase<NoteDoc>) this.Entities).Current);
      if (commitment?.OrderType == "RS")
        WikiFileMaintenanceExt.RedirectToSubcontractEntry(commitment);
    }
    return baseHandler(adapter);
  }

  private static void RedirectToSubcontractEntry(POOrder subcontract)
  {
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = subcontract;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  private void UpdateNoteDocumentForSubcontract(NoteDoc noteDocument)
  {
    POOrder commitment = this.GetCommitment(noteDocument);
    if (!(commitment?.OrderType == "RS"))
      return;
    noteDocument.EntityName = "Subcontracts";
    noteDocument.EntityRowValues = this.GetRewrittenEntityRowValues(commitment);
  }

  private string GetRewrittenEntityRowValues(POOrder subcontract)
  {
    PXCache<POOrder> pxCache = GraphHelper.Caches<POOrder>((PXGraph) this.Base);
    PXUIFieldAttribute.SetVisibility<POOrder.orderType>((PXCache) pxCache, (object) null, (PXUIVisibility) 1);
    string rewrittenEntityRowValues = this.entityHelper.DescriptionEntity(typeof (POOrder), (object) subcontract);
    PXUIFieldAttribute.SetVisibility<POOrder.orderType>((PXCache) pxCache, (object) null, (PXUIVisibility) 7);
    return rewrittenEntityRowValues;
  }

  private POOrder GetCommitment(NoteDoc noteDocument)
  {
    return ((PXSelectBase<POOrder>) new PXSelect<POOrder, Where<POOrder.noteID, Equal<Required<POOrder.noteID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) noteDocument.NoteID
    });
  }
}
