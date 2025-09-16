// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreatePOOrdersFromFSDemandsExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class CreatePOOrdersFromFSDemandsExtension : 
  PXGraphExtension<CreatePOOrdersFromDemandsExtension, POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.MakeDemandSourceInfo(PX.Objects.PO.POFixedDemand,PX.Objects.PO.POCreate.POCreateFilter)" />
  [PXOverride]
  public PODemandSourceInfo MakeDemandSourceInfo(
    POFixedDemand demand,
    POCreate.POCreateFilter processingSettings,
    Func<POFixedDemand, POCreate.POCreateFilter, PODemandSourceInfo> base_MakeDemandSourceInfo)
  {
    PODemandSourceInfo target = base_MakeDemandSourceInfo(demand, processingSettings);
    if (demand.PlanType == "F6")
    {
      PXResult<FSSODetSplit, FSSODet> pxResult = (PXResult<FSSODetSplit, FSSODet>) PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXSelectJoin<FSSODetSplit, InnerJoin<FSSODet, On<FSSODet.lineNbr, Equal<FSSODetSplit.lineNbr>, And<FSSODet.srvOrdType, Equal<FSSODetSplit.srvOrdType>, And<FSSODet.refNbr, Equal<FSSODetSplit.refNbr>>>>>, Where<FSSODetSplit.planID, Equal<Required<FSSODetSplit.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
      {
        (object) demand.PlanID
      }));
      if (pxResult != null)
      {
        CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo demandSourceInfo = CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo.Of(target);
        demandSourceInfo.Line = PXResult<FSSODetSplit, FSSODet>.op_Implicit(pxResult);
        demandSourceInfo.Split = PXResult<FSSODetSplit, FSSODet>.op_Implicit(pxResult);
        target.ProjectID = demandSourceInfo.Line.ProjectID;
        target.TaskID = demandSourceInfo.Line.TaskID;
        target.CostCodeID = demandSourceInfo.Line.CostCodeID;
      }
    }
    return target;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.FillNewPOLineFromDemand(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POLine FillNewPOLineFromDemand(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POLine> base_FillNewPOLineFromDemand)
  {
    if (demand.PlanType == "F6")
    {
      CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo demandSourceInfo = CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo.Of(demandSource);
      poLine.LineType = demandSourceInfo.Split == null || !(demandSourceInfo.Split.LineType != "GI") ? "GF" : "NF";
      FSSODet line = demandSourceInfo.Line;
      if ((line != null ? (line.ManualCost.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        poLine.CuryUnitCost = demandSourceInfo.Line.CuryUnitCost;
    }
    return base_FillNewPOLineFromDemand(poLine, demand, demandSource);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.CopyNoteAndFilesToNewPOLine(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public void CopyNoteAndFilesToNewPOLine(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Action<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo> base_CopyNoteAndFilesToNewPOLine)
  {
    base_CopyNoteAndFilesToNewPOLine(poLine, demand, demandSource);
    FSSODet line = CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo.Of(demandSource).Line;
    if (line == null)
      return;
    poLine.TranDesc = line.TranDesc;
    bool? fromServiceOrder = ((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineNotesFromServiceOrder;
    if (fromServiceOrder.GetValueOrDefault())
      PXNoteAttribute.SetNote(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) poLine, PXNoteAttribute.GetNote((PXCache) GraphHelper.Caches<FSSODet>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base), (object) line));
    fromServiceOrder = ((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineAttachmentsFromServiceOrder;
    if (!fromServiceOrder.GetValueOrDefault())
      return;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes((PXCache) GraphHelper.Caches<FSSODet>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base), (object) line);
    if (fileNotes == null || !((IEnumerable<Guid>) fileNotes).Any<Guid>())
      return;
    PXNoteAttribute.SetFileNotes(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) poLine, fileNotes);
  }

  /// <exclude />
  public sealed class FSxPODemandSourceInfo : PXCacheExtension<PODemandSourceInfo>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    }

    public FSSODet Line { get; set; }

    public FSSODetSplit Split { get; set; }

    public static CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo Of(
      PODemandSourceInfo target)
    {
      return target == null ? (CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo) null : PXCacheEx.GetExtension<PODemandSourceInfo, CreatePOOrdersFromFSDemandsExtension.FSxPODemandSourceInfo>(target);
    }
  }
}
