// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.EP.GraphExtensions.EpApprovalProcessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.EP.Attributes;
using PX.Objects.CN.Subcontracts.PO.Extensions;
using PX.Objects.CN.Subcontracts.SC.DAC;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CN.Subcontracts.EP.GraphExtensions;

public class EpApprovalProcessExt : PXGraphExtension<EPApprovalProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<EPApprovalProcess.EPOwned>) this.Base.Records).SetProcessDelegate(new PXProcessingBase<EPApprovalProcess.EPOwned>.ProcessListDelegate((object) null, __methodptr(Approve)));
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXFormula(typeof (ApprovalDocTypeExt))]
  public virtual void _(
    PX.Data.Events.CacheAttached<EPApprovalProcess.EPOwned.docType> args)
  {
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    POOrder subcontractEntity = ((PXSelectBase<EPApprovalProcess.EPOwned>) this.Base.Records).Current.GetSubcontractEntity((PXGraph) this.Base);
    if (subcontractEntity == null)
      return this.Base.editDetail(adapter);
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = subcontractEntity;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  private static void Approve(List<EPApprovalProcess.EPOwned> approvals)
  {
    Dictionary<EPApprovalProcess.EPOwned, POOrder> subcontracts = EpApprovalProcessExt.ExtractSubcontracts((ICollection<EPApprovalProcess.EPOwned>) approvals);
    bool flag1 = false;
    if (approvals.Any<EPApprovalProcess.EPOwned>())
      flag1 = EpApprovalProcessExt.ApproveNonSubcontractEntities((IReadOnlyCollection<EPApprovalProcess.EPOwned>) approvals);
    bool flag2 = false;
    if (subcontracts.Any<KeyValuePair<EPApprovalProcess.EPOwned, POOrder>>())
      flag2 = EpApprovalProcessExt.ApproveSubcontracts(PXGraph.CreateInstance<SubcontractEntry>(), subcontracts);
    EpApprovalProcessExt.ThrowErrorMessageIfRequired(flag1 | flag2);
  }

  private static bool ApproveNonSubcontractEntities(
    IReadOnlyCollection<EPApprovalProcess.EPOwned> approvals)
  {
    try
    {
      EpApprovalProcessExt.ApproveEntities(approvals);
      return false;
    }
    catch
    {
      return true;
    }
  }

  private static void ApproveEntities(
    IReadOnlyCollection<EPApprovalProcess.EPOwned> approvals)
  {
    typeof (EPApprovalProcess).GetMethod("Approve", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke((object) null, new object[2]
    {
      (object) approvals,
      (object) true
    });
  }

  private static bool ApproveSubcontracts(
    SubcontractEntry graph,
    Dictionary<EPApprovalProcess.EPOwned, POOrder> subcontractApprovalDictionary)
  {
    return subcontractApprovalDictionary.Select<KeyValuePair<EPApprovalProcess.EPOwned, POOrder>, bool>((Func<KeyValuePair<EPApprovalProcess.EPOwned, POOrder>, bool>) (x => EpApprovalProcessExt.ApproveSubcontract(graph, x))).ToList<bool>().Any<bool>((Func<bool, bool>) (x => x));
  }

  private static bool ApproveSubcontract(
    SubcontractEntry graph,
    KeyValuePair<EPApprovalProcess.EPOwned, POOrder> subcontractApprovalPair)
  {
    try
    {
      PXProcessing<EPApproval>.SetCurrentItem((object) subcontractApprovalPair.Key);
      EpApprovalProcessExt.ApproveSingleSubcontract(graph, subcontractApprovalPair.Value);
      PXProcessing<EPApproval>.SetProcessed();
      return false;
    }
    catch (Exception ex)
    {
      PXProcessing<EPApproval>.SetError(ex);
      return true;
    }
  }

  private static void ThrowErrorMessageIfRequired(bool errorOccured)
  {
    if (errorOccured)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  private static Dictionary<EPApprovalProcess.EPOwned, POOrder> ExtractSubcontracts(
    ICollection<EPApprovalProcess.EPOwned> approvals)
  {
    PXGraph instance = PXGraph.CreateInstance<PXGraph>();
    Dictionary<EPApprovalProcess.EPOwned, POOrder> subcontracts = new Dictionary<EPApprovalProcess.EPOwned, POOrder>();
    foreach (EPApprovalProcess.EPOwned commitmentApproval in EpApprovalProcessExt.GetCommitmentApprovals((IEnumerable<EPApprovalProcess.EPOwned>) approvals))
    {
      POOrder commitment = EpApprovalProcessExt.GetCommitment(instance, commitmentApproval.RefNoteID);
      if (commitment.OrderType == "RS")
      {
        subcontracts.Add(commitmentApproval, commitment);
        approvals.Remove(commitmentApproval);
      }
    }
    return subcontracts;
  }

  private static void ApproveSingleSubcontract(SubcontractEntry graph, POOrder subcontract)
  {
    EpApprovalProcessExt.SetupGraphForApproval(graph, subcontract);
    EpApprovalProcessExt.CheckActionExisting(graph);
    EpApprovalProcessExt.PressApprove(graph, subcontract);
    ((PXGraph) graph).Persist();
  }

  private static void SetupGraphForApproval(SubcontractEntry graph, POOrder subcontract)
  {
    ((PXGraph) graph).Clear();
    ((PXSelectBase<POOrder>) graph.Document).Current = subcontract;
  }

  private static void PressApprove(SubcontractEntry graph, POOrder subcontract)
  {
    PXAction action = ((PXGraph) graph).Actions["Action"];
    PXAdapter pxAdapter = new PXAdapter((PXView) new PXView.Dummy((PXGraph) graph, ((PXGraph) graph).Views[((PXGraph) graph).PrimaryView].BqlSelect, new List<object>()
    {
      (object) subcontract
    }))
    {
      Menu = "Approve"
    };
    foreach (object obj in action.Press(pxAdapter))
      ;
  }

  private static void CheckActionExisting(SubcontractEntry graph)
  {
    if (!((OrderedDictionary) ((PXGraph) graph).Actions).Contains((object) "Action"))
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Automation for screen/graph {0} exists but is not configured properly. Failed to find action - 'Action'", new object[1]
      {
        (object) graph
      }));
  }

  private static IEnumerable<EPApprovalProcess.EPOwned> GetCommitmentApprovals(
    IEnumerable<EPApprovalProcess.EPOwned> approvals)
  {
    return (IEnumerable<EPApprovalProcess.EPOwned>) approvals.Where<EPApprovalProcess.EPOwned>((Func<EPApprovalProcess.EPOwned, bool>) (x => x.EntityType == typeof (Subcontract).FullName)).ToList<EPApprovalProcess.EPOwned>();
  }

  private static POOrder GetCommitment(PXGraph graph, Guid? noteId)
  {
    return ((PXSelectBase<POOrder>) new PXSelect<POOrder, Where<POOrder.noteID, Equal<Required<POOrder.noteID>>>>(graph)).SelectSingle(new object[1]
    {
      (object) noteId
    });
  }
}
