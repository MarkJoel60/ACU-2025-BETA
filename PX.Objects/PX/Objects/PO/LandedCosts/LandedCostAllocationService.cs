// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.LandedCostAllocationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class LandedCostAllocationService : AllocationServiceBase
{
  public static LandedCostAllocationService Instance
  {
    get
    {
      return PXContext.GetSlot<LandedCostAllocationService>() ?? PXContext.SetSlot<LandedCostAllocationService>(PXGraph.CreateInstance<LandedCostAllocationService>());
    }
  }

  public virtual POLandedCostSplit[] GetLandedCostSplits(
    POLandedCostDoc doc,
    LandedCostAllocationService.POLandedCostReceiptLineAdjustment[] adjustments)
  {
    return ((IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>) adjustments).Where<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>((Func<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, bool>) (t => t.LandedCostReceiptLine != null && t.LandedCostDetail != null)).GroupBy(t => new
    {
      LandedCostReceiptLineLineNbr = t.LandedCostReceiptLine.LineNbr,
      LandedCostDetailLineNbr = t.LandedCostDetail.LineNbr
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType90<int?, int?>, LandedCostAllocationService.POLandedCostReceiptLineAdjustment>, POLandedCostSplit>(t => new POLandedCostSplit()
    {
      DocType = doc.DocType,
      RefNbr = doc.RefNbr,
      ReceiptLineNbr = t.Key.LandedCostReceiptLineLineNbr,
      DetailLineNbr = t.Key.LandedCostDetailLineNbr,
      LineAmt = new Decimal?(t.Sum<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>((Func<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, Decimal>) (m => m.AllocatedAmt)))
    }).ToArray<POLandedCostSplit>();
  }

  public virtual LandedCostAllocationService.POLandedCostReceiptLineAdjustment[] Allocate(
    PXGraph graph,
    POLandedCostDoc doc,
    IEnumerable<POLandedCostReceiptLine> landedCostReceiptLines,
    IEnumerable<POLandedCostDetail> details,
    IEnumerable<PXResult<POLandedCostTax, PX.Objects.TX.Tax>> taxes)
  {
    List<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> receiptLineAdjustmentList = new List<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>();
    if (!landedCostReceiptLines.Any<POLandedCostReceiptLine>() || !details.Any<POLandedCostDetail>())
      return receiptLineAdjustmentList.ToArray();
    PX.Objects.PO.POReceiptLine[] receiptLines = this.GetReceiptLines(graph, landedCostReceiptLines);
    PX.Objects.PO.POReceipt[] receipts = this.GetReceipts(graph, landedCostReceiptLines);
    LandedCostCode[] landedCostCodes = this.GetLandedCostCodes(graph, details);
    foreach (POLandedCostDetail detail in details)
    {
      if (detail.LandedCostCodeID != null)
      {
        List<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> collection = this.AllocateLCOverRCTLines(graph, detail, landedCostReceiptLines, taxes, (IEnumerable<PX.Objects.PO.POReceiptLine>) receiptLines, (IEnumerable<LandedCostCode>) landedCostCodes, (IEnumerable<PX.Objects.PO.POReceipt>) receipts);
        receiptLineAdjustmentList.AddRange((IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>) collection);
      }
    }
    return receiptLineAdjustmentList.ToArray();
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public bool HasApplicableTransfers { get; set; }

  public LandedCostAllocationService() => this.HasApplicableTransfers = false;

  protected virtual PX.Objects.PO.POReceiptLine[] GetReceiptLines(
    PXGraph graph,
    IEnumerable<POLandedCostReceiptLine> landedCostReceiptLines)
  {
    List<PX.Objects.PO.POReceiptLine> poReceiptLineList = new List<PX.Objects.PO.POReceiptLine>();
    foreach (IGrouping<\u003C\u003Ef__AnonymousType91<string, string>, POLandedCostReceiptLine> source in landedCostReceiptLines.GroupBy(t => new
    {
      POReceiptType = t.POReceiptType,
      POReceiptNbr = t.POReceiptNbr
    }))
    {
      List<PX.Objects.PO.POReceiptLine> list1 = GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelect<PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>>>>.Config>.Select(graph, new object[2]
      {
        (object) source.Key.POReceiptType,
        (object) source.Key.POReceiptNbr
      })).ToList<PX.Objects.PO.POReceiptLine>();
      IEnumerable<int?> lineIds = source.Select<POLandedCostReceiptLine, int?>((Func<POLandedCostReceiptLine, int?>) (t => t.POReceiptLineNbr));
      List<PX.Objects.PO.POReceiptLine> list2 = list1.Where<PX.Objects.PO.POReceiptLine>((Func<PX.Objects.PO.POReceiptLine, bool>) (t => lineIds.Contains<int?>(t.LineNbr))).ToList<PX.Objects.PO.POReceiptLine>();
      poReceiptLineList.AddRange((IEnumerable<PX.Objects.PO.POReceiptLine>) list2);
    }
    return poReceiptLineList.ToArray();
  }

  protected virtual LandedCostCode[] GetLandedCostCodes(
    PXGraph graph,
    IEnumerable<POLandedCostDetail> landedCostDetails)
  {
    string[] array = landedCostDetails.Select<POLandedCostDetail, string>((Func<POLandedCostDetail, string>) (t => t.LandedCostCodeID)).Distinct<string>().ToArray<string>();
    return GraphHelper.RowCast<LandedCostCode>((IEnumerable) PXSelectBase<LandedCostCode, PXSelectReadonly<LandedCostCode, Where<LandedCostCode.landedCostCodeID, In<Required<LandedCostCode.landedCostCodeID>>>>.Config>.Select(graph, new object[1]
    {
      (object) array
    })).ToArray<LandedCostCode>();
  }

  protected virtual PX.Objects.PO.POReceipt[] GetReceipts(
    PXGraph graph,
    IEnumerable<POLandedCostReceiptLine> landedCostReceiptLines)
  {
    List<PX.Objects.PO.POReceipt> poReceiptList = new List<PX.Objects.PO.POReceipt>();
    foreach (IGrouping<\u003C\u003Ef__AnonymousType92<string>, POLandedCostReceiptLine> source in landedCostReceiptLines.GroupBy(t => new
    {
      POReceiptType = t.POReceiptType
    }))
    {
      string poReceiptType = source.Key.POReceiptType;
      string[] array = source.Select<POLandedCostReceiptLine, string>((Func<POLandedCostReceiptLine, string>) (t => t.POReceiptNbr)).Distinct<string>().ToArray<string>();
      List<PX.Objects.PO.POReceipt> list = GraphHelper.RowCast<PX.Objects.PO.POReceipt>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceipt, PXSelect<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceipt.receiptNbr, In<Required<PX.Objects.PO.POReceipt.receiptNbr>>>>>.Config>.Select(graph, new object[2]
      {
        (object) poReceiptType,
        (object) array
      })).ToList<PX.Objects.PO.POReceipt>();
      poReceiptList.AddRange((IEnumerable<PX.Objects.PO.POReceipt>) list);
    }
    return poReceiptList.ToArray();
  }

  public virtual bool IsApplicable(
    PXGraph graph,
    POLandedCostDetail aTran,
    LandedCostCode aCode,
    PX.Objects.PO.POReceiptLine aLine)
  {
    if (!this.IsApplicable(graph, aCode, aLine))
      return false;
    if (aTran.InventoryID.HasValue)
    {
      int? inventoryId1 = aTran.InventoryID;
      int? inventoryId2 = aLine.InventoryID;
      if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
        return false;
    }
    return true;
  }

  public virtual bool IsApplicable(PXGraph graph, LandedCostCode aCode, PX.Objects.PO.POReceiptLine aLine)
  {
    bool flag = false;
    if (aLine.ReceiptType == "RX")
    {
      INTran inTran = INTran.PK.Find(graph, aLine.OrigDocType, aLine.OrigRefNbr, aLine.OrigLineNbr);
      int num;
      if (inTran != null)
      {
        int? siteId1 = inTran.SiteID;
        int? siteId2 = aLine.SiteID;
        num = siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue ? 1 : 0;
      }
      else
        num = 1;
      flag = num != 0;
    }
    if (!flag)
      this.HasApplicableTransfers = true;
    if (flag || !(aCode.AllocationMethod != "N"))
      return false;
    return aLine.LineType == "GI" || aLine.LineType == "GR" || aLine.LineType == "GS" || aLine.LineType == "GP" || aLine.LineType == "NS" || aLine.LineType == "NP" || aLine.LineType == "NO" || aLine.LineType == "GF" || aLine.LineType == "NF" || aLine.LineType == "GM" || aLine.LineType == "NM";
  }

  public override Decimal CalcAllocationValue(
    PXGraph graph,
    AllocationServiceBase.AllocationItem allocationItem,
    Decimal aBaseTotal,
    Decimal aAllocationTotal)
  {
    Decimal num = 0M;
    if (this.IsApplicable(graph, allocationItem.LandedCostCode, allocationItem.ReceiptLine))
    {
      Decimal baseValue = this.GetBaseValue(allocationItem);
      num = aBaseTotal == 0M ? 0M : baseValue * aAllocationTotal / aBaseTotal;
    }
    return num;
  }

  public override AllocationServiceBase.POReceiptLineAdjustment CreateReceiptLineAdjustment(
    AllocationServiceBase.AllocationItem allocationItem,
    PX.Objects.PO.POReceiptLine receiptLine,
    Decimal qtyToAssign,
    int? branchID)
  {
    if ((allocationItem == null || allocationItem.ReceiptLine == null) && receiptLine == null)
      return (AllocationServiceBase.POReceiptLineAdjustment) new LandedCostAllocationService.POLandedCostReceiptLineAdjustment(new PX.Objects.PO.POReceiptLine(), (POLandedCostReceiptLine) null, (POLandedCostDetail) null, qtyToAssign, branchID);
    if (!(allocationItem is LandedCostAllocationService.LandedCostAllocationItem))
      return base.CreateReceiptLineAdjustment(allocationItem, receiptLine, qtyToAssign, branchID);
    LandedCostAllocationService.LandedCostAllocationItem costAllocationItem = (LandedCostAllocationService.LandedCostAllocationItem) allocationItem;
    return (AllocationServiceBase.POReceiptLineAdjustment) new LandedCostAllocationService.POLandedCostReceiptLineAdjustment(receiptLine ?? allocationItem.ReceiptLine, costAllocationItem.LandedCostReceiptLine, costAllocationItem.LandedCostDetail, qtyToAssign, branchID);
  }

  protected virtual Decimal GetAllocationAmount(
    POLandedCostDetail landedCostDetail,
    IEnumerable<PXResult<POLandedCostTax, PX.Objects.TX.Tax>> taxes)
  {
    POLandedCostTax poLandedCostTax = GraphHelper.RowCast<POLandedCostTax>((IEnumerable) taxes.Where<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>((Func<PXResult<POLandedCostTax, PX.Objects.TX.Tax>, bool>) (t => PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).TaxCalcLevel == "0" && PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).TaxType != "W" && !PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).ReverseTax.GetValueOrDefault()))).FirstOrDefault<POLandedCostTax>((Func<POLandedCostTax, bool>) (t =>
    {
      int? lineNbr1 = t.LineNbr;
      int? lineNbr2 = landedCostDetail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    }));
    return poLandedCostTax != null ? poLandedCostTax.TaxableAmt.GetValueOrDefault() : landedCostDetail.LineAmt.GetValueOrDefault();
  }

  public virtual List<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> AllocateLCOverRCTLines(
    PXGraph graph,
    POLandedCostDetail landedCostDetail,
    IEnumerable<POLandedCostReceiptLine> landedCostReceiptLines,
    IEnumerable<PXResult<POLandedCostTax, PX.Objects.TX.Tax>> taxes,
    IEnumerable<PX.Objects.PO.POReceiptLine> receiptLines,
    IEnumerable<LandedCostCode> landedCostCodes,
    IEnumerable<PX.Objects.PO.POReceipt> receipts)
  {
    LandedCostCode landedCostCode = landedCostCodes.Single<LandedCostCode>((Func<LandedCostCode, bool>) (t => t.LandedCostCodeID == landedCostDetail.LandedCostCodeID));
    List<PX.Objects.PO.POReceiptLine> list = receiptLines.Where<PX.Objects.PO.POReceiptLine>((Func<PX.Objects.PO.POReceiptLine, bool>) (rl => this.IsApplicable(graph, landedCostDetail, landedCostCode, rl))).ToList<PX.Objects.PO.POReceiptLine>();
    LandedCostAllocationService.LandedCostAllocationItem[] costAllocationItemArray1 = new LandedCostAllocationService.LandedCostAllocationItem[list.Count];
    Decimal allocationAmount = this.GetAllocationAmount(landedCostDetail, taxes);
    Decimal aBaseTotal = 0M;
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      PX.Objects.PO.POReceiptLine receiptLine = list[index1];
      POLandedCostReceiptLine landedCostReceiptLine = landedCostReceiptLines.Single<POLandedCostReceiptLine>((Func<POLandedCostReceiptLine, bool>) (t =>
      {
        if (!(t.POReceiptType == receiptLine.ReceiptType) || !(t.POReceiptNbr == receiptLine.ReceiptNbr))
          return false;
        int? poReceiptLineNbr = t.POReceiptLineNbr;
        int? lineNbr = receiptLine.LineNbr;
        return poReceiptLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poReceiptLineNbr.HasValue == lineNbr.HasValue;
      }));
      LandedCostAllocationService.LandedCostAllocationItem[] costAllocationItemArray2 = costAllocationItemArray1;
      int index2 = index1;
      LandedCostAllocationService.LandedCostAllocationItem costAllocationItem = new LandedCostAllocationService.LandedCostAllocationItem();
      costAllocationItem.LandedCostCode = landedCostCode;
      costAllocationItem.ReceiptLine = receiptLine;
      costAllocationItem.LandedCostDetail = landedCostDetail;
      costAllocationItem.LandedCostReceiptLine = landedCostReceiptLine;
      costAllocationItemArray2[index2] = costAllocationItem;
      aBaseTotal += this.GetBaseValue((AllocationServiceBase.AllocationItem) costAllocationItemArray1[index1]);
    }
    List<AllocationServiceBase.POReceiptLineAdjustment> receiptLineAdjustmentList = new List<AllocationServiceBase.POReceiptLineAdjustment>();
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    int? nullable = new int?();
    Decimal num3 = 0M;
    for (int index = 0; index < list.Count; ++index)
    {
      Decimal num4 = this.CalcAllocationValue(graph, (AllocationServiceBase.AllocationItem) costAllocationItemArray1[index], aBaseTotal, allocationAmount);
      num2 += num4;
      if (!nullable.HasValue || Math.Abs(num3) < Math.Abs(num4))
      {
        nullable = new int?(index);
        num3 = num4;
      }
      Decimal toDistribute = num2 - num1;
      if (toDistribute != 0M)
      {
        PX.Objects.PO.POReceiptLine maxLine = list[nullable.Value];
        PX.Objects.PO.POReceipt poReceipt = receipts.Single<PX.Objects.PO.POReceipt>((Func<PX.Objects.PO.POReceipt, bool>) (r => r.ReceiptType == maxLine.ReceiptType && r.ReceiptNbr == maxLine.ReceiptNbr));
        Decimal num5 = this.AllocateOverRCTLine(graph, receiptLineAdjustmentList, (AllocationServiceBase.AllocationItem) costAllocationItemArray1[nullable.Value], toDistribute, poReceipt.BranchID);
        if (num5 != 0M)
        {
          nullable = new int?();
          num3 = 0M;
          num1 += num5;
        }
      }
    }
    this.AllocateRestOverRCTLines((IList<AllocationServiceBase.POReceiptLineAdjustment>) receiptLineAdjustmentList, allocationAmount - num1);
    return receiptLineAdjustmentList.Cast<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>().ToList<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>();
  }

  public virtual bool ValidateLCTran(
    PXGraph graph,
    POLandedCostDoc doc,
    IEnumerable<POLandedCostReceiptLine> landedCostReceiptLines,
    POLandedCostDetail row,
    out string message)
  {
    Decimal num1 = 0M;
    int num2 = 0;
    if (row != null && !string.IsNullOrEmpty(row.LandedCostCodeID))
    {
      bool flag = true;
      LandedCostCode aCode = LandedCostCode.PK.Find(graph, row.LandedCostCodeID);
      foreach (PX.Objects.PO.POReceiptLine receiptLine in this.GetReceiptLines(graph, landedCostReceiptLines))
      {
        PX.Objects.PO.POReceiptLine it = receiptLine;
        POLandedCostReceiptLine landedCostReceiptLine = landedCostReceiptLines.Single<POLandedCostReceiptLine>((Func<POLandedCostReceiptLine, bool>) (t =>
        {
          if (!(t.POReceiptType == it.ReceiptType) || !(t.POReceiptNbr == it.ReceiptNbr))
            return false;
          int? poReceiptLineNbr = t.POReceiptLineNbr;
          int? lineNbr = it.LineNbr;
          return poReceiptLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poReceiptLineNbr.HasValue == lineNbr.HasValue;
        }));
        this.HasApplicableTransfers = false;
        if (this.IsApplicable(graph, row, aCode, it))
        {
          LandedCostAllocationService.LandedCostAllocationItem costAllocationItem1 = new LandedCostAllocationService.LandedCostAllocationItem();
          costAllocationItem1.LandedCostCode = aCode;
          costAllocationItem1.ReceiptLine = it;
          costAllocationItem1.LandedCostDetail = row;
          costAllocationItem1.LandedCostReceiptLine = landedCostReceiptLine;
          LandedCostAllocationService.LandedCostAllocationItem costAllocationItem2 = costAllocationItem1;
          num1 += this.GetBaseValue((AllocationServiceBase.AllocationItem) costAllocationItem2);
        }
        flag &= this.HasApplicableTransfers;
        ++num2;
      }
      if (!flag)
      {
        message = "Landed costs cannot be applied to items that are transferred within one warehouse.";
        return false;
      }
      switch (aCode.AllocationMethod)
      {
        case "C":
          message = "This Landed Cost is allocated by Amount , but Receipt total Amount is equal to zero";
          break;
        case "W":
          message = "This Landed Cost is allocated by Weight , but Receipt total Weight is equal to zero";
          break;
        case "V":
          message = "This Landed Cost is  allocated by Volume, but Receipt total Volume is equal to zero";
          break;
        case "Q":
          message = "This Landed Cost is allocated by Quantity , but Receipt total Quantity is equal to zero";
          break;
        case "N":
          message = "This Receipt does not have detail lines";
          num1 = (Decimal) num2;
          break;
        default:
          message = "Unknown Landed Cost alloction type";
          break;
      }
      if (num1 == 0M)
        return false;
    }
    message = string.Empty;
    return true;
  }

  public class LandedCostAllocationItem : AllocationServiceBase.AllocationItem
  {
    public POLandedCostReceiptLine LandedCostReceiptLine { get; set; }

    public POLandedCostDetail LandedCostDetail { get; set; }

    public override Decimal? Weight
    {
      get
      {
        Decimal? extWeight = (Decimal?) this.LandedCostReceiptLine?.ExtWeight;
        if (extWeight.HasValue)
          return extWeight;
        return this.ReceiptLine?.ExtWeight;
      }
    }

    public override Decimal? Volume
    {
      get
      {
        Decimal? extVolume = (Decimal?) this.LandedCostReceiptLine?.ExtVolume;
        if (extVolume.HasValue)
          return extVolume;
        return this.ReceiptLine?.ExtVolume;
      }
    }
  }

  public class POLandedCostReceiptLineAdjustment : AllocationServiceBase.POReceiptLineAdjustment
  {
    public POLandedCostReceiptLineAdjustment(
      PX.Objects.PO.POReceiptLine receiptLine,
      POLandedCostReceiptLine landedCostReceiptLine,
      POLandedCostDetail landedCostDetail,
      Decimal qtyToAssign,
      int? branchID)
      : base(receiptLine, qtyToAssign, branchID)
    {
      this.LandedCostReceiptLine = landedCostReceiptLine;
      this.LandedCostDetail = landedCostDetail;
    }

    public POLandedCostReceiptLine LandedCostReceiptLine { get; protected set; }

    public POLandedCostDetail LandedCostDetail { get; protected set; }
  }
}
