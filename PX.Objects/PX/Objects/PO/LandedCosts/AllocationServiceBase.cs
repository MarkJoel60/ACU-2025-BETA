// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.AllocationServiceBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public abstract class AllocationServiceBase : PXGraph<AllocationServiceBase>
{
  public virtual Decimal AllocateOverRCTLine(
    PXGraph graph,
    List<AllocationServiceBase.POReceiptLineAdjustment> result,
    AllocationServiceBase.AllocationItem allocationItem,
    Decimal toDistribute,
    int? branchID)
  {
    AllocationServiceBase.AllocationItem allocationItem1 = new AllocationServiceBase.AllocationItem()
    {
      LandedCostCode = new LandedCostCode()
      {
        AllocationMethod = "Q"
      },
      ReceiptLine = allocationItem.ReceiptLine
    };
    bool flag1 = PX.Objects.IN.InventoryItem.PK.Find(graph, new int?(allocationItem.ReceiptLine.InventoryID.Value)).ValMethod == "S";
    List<Type> typeList = new List<Type>(16 /*0x10*/)
    {
      typeof (Select4<,,>),
      typeof (POReceiptLineSplit),
      typeof (Where<POReceiptLineSplit.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<POReceiptLineSplit.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<POReceiptLineSplit.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>),
      typeof (Aggregate<>),
      typeof (GroupBy<,>),
      typeof (POReceiptLineSplit.locationID),
      typeof (GroupBy<,>),
      typeof (POReceiptLineSplit.subItemID),
      typeof (Sum<>),
      typeof (POReceiptLineSplit.baseQty)
    };
    if (flag1)
    {
      typeList.Insert(typeList.Count - 2, typeof (GroupBy<,>));
      typeList.Insert(typeList.Count - 2, typeof (POReceiptLineSplit.lotSerialNbr));
    }
    List<object> objectList = new PXView(graph, false, BqlCommand.CreateInstance(typeList.ToArray())).SelectMulti(new object[3]
    {
      (object) allocationItem.ReceiptLine.ReceiptType,
      (object) allocationItem.ReceiptLine.ReceiptNbr,
      (object) allocationItem.ReceiptLine.LineNbr
    });
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = graph.FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(allocationItem.ReceiptLine.CuryInfoID);
    bool flag2 = false;
    Decimal baseValue = this.GetBaseValue(allocationItem1);
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    POReceiptLineSplit receiptLineSplit1 = (POReceiptLineSplit) null;
    foreach (POReceiptLineSplit receiptLineSplit2 in objectList)
    {
      flag2 = true;
      Decimal num4 = num2;
      Decimal? baseQty1 = receiptLineSplit2.BaseQty;
      Decimal valueOrDefault = baseQty1.GetValueOrDefault();
      num2 = num4 + valueOrDefault;
      Decimal val = baseValue == 0M ? 0M : (num1 + num2) * toDistribute / baseValue;
      Decimal qtyToAssign = currencyInfo.RoundBase(val) - num3;
      if (receiptLineSplit1 != null)
      {
        baseQty1 = receiptLineSplit1.BaseQty;
        Decimal? baseQty2 = receiptLineSplit2.BaseQty;
        if (!(baseQty1.GetValueOrDefault() < baseQty2.GetValueOrDefault() & baseQty1.HasValue & baseQty2.HasValue))
          goto label_7;
      }
      receiptLineSplit1 = receiptLineSplit2;
label_7:
      if (qtyToAssign != 0M)
      {
        PX.Objects.PO.POReceiptLine copy = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(allocationItem.ReceiptLine);
        copy.LocationID = receiptLineSplit1.LocationID;
        copy.SiteID = receiptLineSplit1.SiteID;
        copy.SubItemID = receiptLineSplit1.SubItemID;
        copy.LotSerialNbr = flag1 ? receiptLineSplit1.LotSerialNbr : (string) null;
        AllocationServiceBase.POReceiptLineAdjustment receiptLineAdjustment = this.CreateReceiptLineAdjustment(allocationItem, copy, qtyToAssign, branchID);
        result.Add(receiptLineAdjustment);
        num3 += qtyToAssign;
        num1 += num2;
        num2 = 0M;
        receiptLineSplit1 = (POReceiptLineSplit) null;
      }
    }
    if (!flag2)
    {
      Decimal val = toDistribute;
      Decimal qtyToAssign = currencyInfo.RoundBase(val);
      if (qtyToAssign != 0M)
      {
        AllocationServiceBase.POReceiptLineAdjustment receiptLineAdjustment = this.CreateReceiptLineAdjustment(allocationItem, (PX.Objects.PO.POReceiptLine) null, qtyToAssign, branchID);
        result.Add(receiptLineAdjustment);
      }
      num3 = qtyToAssign;
    }
    return num3;
  }

  public virtual void AllocateRestOverRCTLines(
    IList<AllocationServiceBase.POReceiptLineAdjustment> aLines,
    Decimal rest)
  {
    if (!(rest != 0M))
      return;
    if (aLines.Count == 0)
    {
      AllocationServiceBase.POReceiptLineAdjustment receiptLineAdjustment = this.CreateReceiptLineAdjustment((AllocationServiceBase.AllocationItem) null, (PX.Objects.PO.POReceiptLine) null, rest, new int?());
      aLines.Add(receiptLineAdjustment);
    }
    else
    {
      Decimal num1 = -1M;
      int index1 = 0;
      for (int index2 = 0; index2 < aLines.Count; ++index2)
      {
        Decimal num2 = Math.Abs(aLines[index2].AllocatedAmt);
        if (num1 < num2)
        {
          num1 = num2;
          index1 = index2;
        }
      }
      aLines[index1].AllocatedAmt += rest;
    }
  }

  public virtual INTran GetOriginalInTran(
    PXGraph graph,
    string receiptType,
    string receiptNbr,
    int? lineNbr)
  {
    if (receiptType == null || receiptNbr == null || !lineNbr.HasValue)
      return (INTran) null;
    return PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXSelectReadonly<INTran, Where<INTran.docType, NotEqual<INDocType.adjustment>, And<INTran.docType, NotEqual<INDocType.transfer>, And<INTran.pOReceiptType, Equal<Required<INTran.pOReceiptType>>, And<INTran.pOReceiptNbr, Equal<Required<INTran.pOReceiptNbr>>, And<INTran.pOReceiptLineNbr, Equal<Required<INTran.pOReceiptLineNbr>>>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) receiptType,
      (object) receiptNbr,
      (object) lineNbr
    }));
  }

  public virtual void GetProjectsAndTasks(
    PXGraph graph,
    List<AllocationServiceBase.POReceiptLineAdjustment> pOLinesToProcess,
    out Dictionary<int?, PX.Objects.PM.Lite.PMProject> projects,
    out Dictionary<int?, PX.Objects.PM.Lite.PMTask> tasks)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      projects = (Dictionary<int?, PX.Objects.PM.Lite.PMProject>) null;
      tasks = (Dictionary<int?, PX.Objects.PM.Lite.PMTask>) null;
    }
    HashSet<\u003C\u003Ef__AnonymousType89<int?, int?>> hashSet = pOLinesToProcess.Select(m => new
    {
      ProjectID = m.ReceiptLine.ProjectID,
      TaskID = m.ReceiptLine.TaskID
    }).Where(m =>
    {
      if (!m.ProjectID.HasValue)
        return false;
      int? projectId = m.ProjectID;
      int? nullable = ProjectDefaultAttribute.NonProject();
      return !(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue);
    }).ToHashSet();
    int?[] array1 = hashSet.Select(m => m.ProjectID).ToArray<int?>();
    int?[] array2 = hashSet.Select(m => m.TaskID).Where<int?>((Func<int?, bool>) (m => m.HasValue)).ToArray<int?>();
    projects = ((IEnumerable<PXResult<PX.Objects.PM.Lite.PMProject>>) PXSelectBase<PX.Objects.PM.Lite.PMProject, PXSelectReadonly<PX.Objects.PM.Lite.PMProject, Where<PX.Objects.PM.Lite.PMProject.contractID, In<Required<PX.Objects.PM.Lite.PMProject.contractID>>>>.Config>.Select(graph, new object[1]
    {
      (object) array1
    })).ToDictionary<PXResult<PX.Objects.PM.Lite.PMProject>, int?, PX.Objects.PM.Lite.PMProject>((Func<PXResult<PX.Objects.PM.Lite.PMProject>, int?>) (m => ((PXResult) m).GetItem<PX.Objects.PM.Lite.PMProject>().ContractID), (Func<PXResult<PX.Objects.PM.Lite.PMProject>, PX.Objects.PM.Lite.PMProject>) (m => ((PXResult) m).GetItem<PX.Objects.PM.Lite.PMProject>()));
    tasks = ((IEnumerable<PXResult<PX.Objects.PM.Lite.PMTask>>) PXSelectBase<PX.Objects.PM.Lite.PMTask, PXSelectReadonly<PX.Objects.PM.Lite.PMTask, Where<PX.Objects.PM.Lite.PMTask.taskID, In<Required<PX.Objects.PM.Lite.PMTask.taskID>>>>.Config>.Select(graph, new object[1]
    {
      (object) array2
    })).ToDictionary<PXResult<PX.Objects.PM.Lite.PMTask>, int?, PX.Objects.PM.Lite.PMTask>((Func<PXResult<PX.Objects.PM.Lite.PMTask>, int?>) (m => ((PXResult) m).GetItem<PX.Objects.PM.Lite.PMTask>().TaskID), (Func<PXResult<PX.Objects.PM.Lite.PMTask>, PX.Objects.PM.Lite.PMTask>) (m => ((PXResult) m).GetItem<PX.Objects.PM.Lite.PMTask>()));
  }

  public virtual bool NeedsApplicabilityChecking(LandedCostCode aCode)
  {
    return aCode.AllocationMethod != "N";
  }

  public virtual Decimal GetBaseValue(
    AllocationServiceBase.AllocationItem allocationItem)
  {
    switch (allocationItem.LandedCostCode.AllocationMethod)
    {
      case "C":
        return allocationItem.TranCostFinal.GetValueOrDefault();
      case "Q":
        return allocationItem.BaseQty.GetValueOrDefault();
      case "W":
        return allocationItem.Weight.GetValueOrDefault();
      case "V":
        return allocationItem.Volume.GetValueOrDefault();
      case "N":
        return 1M;
      default:
        throw new PXException("Unknown Landed Cost Allocation Method for Landed Cost Code '{0}'", new object[1]
        {
          (object) allocationItem.LandedCostCode.LandedCostCodeID
        });
    }
  }

  public virtual Decimal CalcAllocationValue(
    PXGraph graph,
    AllocationServiceBase.AllocationItem allocationItem,
    Decimal aBaseTotal,
    Decimal aAllocationTotal)
  {
    return aAllocationTotal;
  }

  public virtual Decimal CalcAllocationValue(
    PXGraph graph,
    AllocationServiceBase.AllocationItem allocationItem,
    POReceiptLineSplit aSplit,
    Decimal aBaseTotal,
    Decimal aAllocationTotal)
  {
    Decimal num1 = this.CalcAllocationValue(graph, allocationItem, aBaseTotal, aAllocationTotal);
    Decimal? baseQty1 = aSplit.BaseQty;
    Decimal? baseQty2 = allocationItem.BaseQty;
    Decimal num2 = 0M;
    Decimal? nullable = baseQty2.GetValueOrDefault() == num2 & baseQty2.HasValue ? new Decimal?() : allocationItem.BaseQty;
    Decimal num3 = (baseQty1.HasValue & nullable.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() / nullable.GetValueOrDefault()) : new Decimal?()) ?? 1M;
    return num1 * num3;
  }

  public virtual AllocationServiceBase.POReceiptLineAdjustment CreateReceiptLineAdjustment(
    AllocationServiceBase.AllocationItem allocationItem,
    PX.Objects.PO.POReceiptLine receiptLine,
    Decimal qtyToAssign,
    int? branchID)
  {
    return (allocationItem == null || allocationItem.ReceiptLine == null) && receiptLine == null ? new AllocationServiceBase.POReceiptLineAdjustment(new PX.Objects.PO.POReceiptLine(), qtyToAssign, branchID) : new AllocationServiceBase.POReceiptLineAdjustment(receiptLine ?? allocationItem.ReceiptLine, qtyToAssign, branchID);
  }

  public class AllocationItem
  {
    public LandedCostCode LandedCostCode { get; set; }

    public PX.Objects.PO.POReceiptLine ReceiptLine { get; set; }

    public virtual Decimal? TranCostFinal => this.ReceiptLine?.TranCostFinal;

    public virtual Decimal? BaseQty => this.ReceiptLine?.BaseQty;

    public virtual Decimal? Weight => this.ReceiptLine?.ExtWeight;

    public virtual Decimal? Volume => this.ReceiptLine?.ExtVolume;
  }

  public class POReceiptLineAdjustment
  {
    public PX.Objects.PO.POReceiptLine ReceiptLine { get; protected set; }

    public Decimal AllocatedAmt { get; protected internal set; }

    public int? BranchID { get; protected set; }

    public POReceiptLineAdjustment(PX.Objects.PO.POReceiptLine receiptLine, Decimal allocatedAmt, int? branchID)
    {
      this.ReceiptLine = receiptLine;
      this.AllocatedAmt = allocatedAmt;
      this.BranchID = branchID;
    }
  }
}
