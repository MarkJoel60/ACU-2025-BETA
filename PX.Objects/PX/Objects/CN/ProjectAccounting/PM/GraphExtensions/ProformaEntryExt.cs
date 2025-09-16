// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.ProformaEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class ProformaEntryExt : PXGraphExtension<ProformaEntry>
{
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMProforma.projectID>>>> ProjectProperties;
  public const string AIAReport = "PM644000";
  public const string AIAWithQtyReport = "PM644500";
  private const string SubsBillingReport = "PM650000";
  public PXAction<PMProforma> aia;
  public PXAction<PMProforma> substantiatedBilling;
  public PXAction<PMProforma> substantiatedBillingConsolidated;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new Type[] {typeof (PMTask.type)})]
  [PXFormula(typeof (Validate<PMProformaLine.costCodeID, PMProformaLine.description>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMProformaTransactLine.taskID> e)
  {
  }

  [PXUIField(DisplayName = "AIA Report")]
  [PXButton]
  public virtual IEnumerable Aia(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Base.Document).Current != null)
    {
      this.Base.RecalculateExternalTaxesSync = true;
      ((PXSelectBase) this.Base.Document).Cache.SetValue<PMProforma.isAIAOutdated>((object) ((PXSelectBase<PMProforma>) this.Base.Document).Current, (object) false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PMProforma>) this.Base.Document).Current);
      ((PXAction) this.Base.Save).Press();
      throw new PXReportRequiredException((IPXResultset) this.BuildResultsetForAIA(), this.GetAIAReportID(), "AIA Report", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Substantiated Billing")]
  [PXButton]
  public virtual IEnumerable SubstantiatedBilling(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Base.Project).Current != null && ((PXSelectBase<PMProforma>) this.Base.Document).Current != null)
      throw new PXReportRequiredException(this.ParametersForSubsBillingReport(), "PM650000", (PXBaseRedirectException.WindowMode) 2, "Substantiated Billing", (CurrentLocalization) null);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Substantiated Billing: Consolidated")]
  [PXButton]
  public virtual IEnumerable SubstantiatedBillingConsolidated(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Base.Project).Current != null && ((PXSelectBase<PMProforma>) this.Base.Document).Current != null)
    {
      PX.Objects.CN.ProjectAccounting.PM.Graphs.SubstantiatedBilling instance = PXGraph.CreateInstance<PX.Objects.CN.ProjectAccounting.PM.Graphs.SubstantiatedBilling>();
      ((PXSelectBase<PX.Objects.CN.ProjectAccounting.PM.Graphs.SubstantiatedBilling.Substantial>) instance.MasterView).Current = new PX.Objects.CN.ProjectAccounting.PM.Graphs.SubstantiatedBilling.Substantial()
      {
        ProjectID = ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractID,
        Mode = "P",
        FromDate = new DateTime?(this.StartDateOfCurrentMonth()),
        ToDate = ((PXGraph) this.Base).Accessinfo.BusinessDate,
        ProFormaRefNbr = ((PXSelectBase<PMProforma>) this.Base.Document).Current.RefNbr,
        IncludeLineAttachments = new bool?(true),
        IncludeNonBillable = new bool?(true)
      };
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Substantiated Billing: Consolidated");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  private DateTime StartDateOfCurrentMonth()
  {
    DateTime dateTime = ((PXGraph) this.Base).Accessinfo.BusinessDate ?? DateTime.Now;
    return dateTime.AddDays((double) (-dateTime.Day + 1));
  }

  private Dictionary<string, string> ParametersForSubsBillingReport()
  {
    return new Dictionary<string, string>()
    {
      {
        "ProjectID",
        ((PXSelectBase<PMProject>) this.Base.Project).Current?.ContractCD
      },
      {
        "TranMode",
        "P"
      },
      {
        "ProFormaRefNbr",
        ((PXSelectBase<PMProforma>) this.Base.Document).Current?.RefNbr
      },
      {
        "IncludeNonBillable",
        true.ToString()
      }
    };
  }

  public virtual string GetAIAReportID()
  {
    return ((PXSelectBase<PMProforma>) this.Base.Document).Current != null && ((PXSelectBase<PMProject>) this.Base.Project).Current != null && ((PXSelectBase<PMProject>) this.Base.Project).Current.IncludeQtyInAIA.GetValueOrDefault() ? "PM644500" : "PM644000";
  }

  public virtual PXReportResultset BuildResultsetForAIA()
  {
    PMRevenueBudget pmRevenueBudget1 = PXResultset<PMRevenueBudget>.op_Implicit(((PXSelectBase<PMRevenueBudget>) new PXSelectGroupBy<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProforma.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>, Aggregate<Sum<PMRevenueBudget.curyAmount, Sum<PMRevenueBudget.qty, Sum<PMRevenueBudget.curyChangeOrderAmount, Sum<PMRevenueBudget.changeOrderQty, Sum<PMRevenueBudget.curyRevisedAmount, Sum<PMRevenueBudget.revisedQty>>>>>>>>((PXGraph) this.Base)).Select(Array.Empty<object>()));
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals> proformaTotalsToDate = this.GetProformaTotalsToDate();
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, Decimal> billedRetainageToDate1 = this.Base.GetBilledRetainageToDate(((PXSelectBase<PMProforma>) this.Base.Document).Current.InvoiceDate);
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    foreach (ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals in proformaTotalsToDate.Values)
    {
      num1 += amountBaseTotals.CuryRetainage;
      num2 += amountBaseTotals.CuryLineTotal;
    }
    Decimal num3 = num1;
    foreach (Decimal num4 in billedRetainageToDate1.Values)
      num1 -= num4;
    PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelectGroupBy<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProforma.projectID>>, And<PMProforma.refNbr, Less<Current<PMProforma.refNbr>>, And<PMProforma.corrected, NotEqual<True>>>>, Aggregate<Max<PMProforma.refNbr>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    DateTime? cutoffDate = new DateTime?();
    Decimal? nullable1 = new Decimal?(0M);
    if (pmProforma != null)
    {
      cutoffDate = pmProforma.InvoiceDate;
      PMProformaLine pmProformaLine = PXResultset<PMProformaLine>.op_Implicit(((PXSelectBase<PMProformaLine>) new PXSelectGroupBy<PMProformaLine, Where<PMProformaLine.refNbr, LessEqual<Required<PMProforma.refNbr>>, And<PMProformaLine.projectID, Equal<Required<PMProforma.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, NotEqual<True>>>>>, Aggregate<Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.curyRetainage>>>>((PXGraph) this.Base)).Select(new object[2]
      {
        (object) pmProforma.RefNbr,
        (object) pmProforma.ProjectID
      }));
      if (pmProformaLine != null)
      {
        Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, Decimal> billedRetainageToDate2 = this.Base.GetBilledRetainageToDate(cutoffDate);
        Decimal valueOrDefault = pmProformaLine.CuryRetainage.GetValueOrDefault();
        foreach (Decimal num5 in billedRetainageToDate2.Values)
          valueOrDefault -= num5;
        nullable1 = new Decimal?(pmProformaLine.CuryLineTotal.GetValueOrDefault() - valueOrDefault);
      }
    }
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntryExt.ChangeOrderTotals> dictionary1 = new Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntryExt.ChangeOrderTotals>();
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, List<PMChangeOrderRevenueBudget>> dictionary2 = new Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, List<PMChangeOrderRevenueBudget>>();
    foreach (PXResult<PMChangeOrderRevenueBudget> pxResult in ((PXSelectBase<PMChangeOrderRevenueBudget>) new PXSelectJoinGroupBy<PMChangeOrderRevenueBudget, InnerJoin<PMChangeOrder, On<PMChangeOrder.refNbr, Equal<PMChangeOrderRevenueBudget.refNbr>>>, Where<PMChangeOrder.projectID, Equal<Current<PMProforma.projectID>>, And<PMChangeOrder.approved, Equal<True>, And<PMChangeOrder.completionDate, LessEqual<Current<PMProforma.invoiceDate>>, And<PMChangeOrderRevenueBudget.type, Equal<AccountType.income>>>>>, Aggregate<GroupBy<PMChangeOrderRevenueBudget.projectID, GroupBy<PMChangeOrderRevenueBudget.projectTaskID, GroupBy<PMChangeOrderRevenueBudget.inventoryID, GroupBy<PMChangeOrderRevenueBudget.costCodeID, GroupBy<PMChangeOrderRevenueBudget.accountGroupID, Sum<PMChangeOrderRevenueBudget.amount, Sum<PMChangeOrderBudget.qty>>>>>>>>>((PXGraph) this.Base)).Select(Array.Empty<object>()))
    {
      PMChangeOrderRevenueBudget orderRevenueBudget = PXResult<PMChangeOrderRevenueBudget>.op_Implicit(pxResult);
      int defaultCostCode = orderRevenueBudget.CostCodeID.Value;
      int emptyInventoryId = orderRevenueBudget.InventoryID.Value;
      int accountGroupID = orderRevenueBudget.AccountGroupID.Value;
      if (((PXSelectBase<PMProject>) this.Base.Project).Current.BudgetLevel == "T")
      {
        defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
        emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      }
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(orderRevenueBudget.ProjectTaskID.Value, defaultCostCode, emptyInventoryId, accountGroupID);
      List<PMChangeOrderRevenueBudget> orderRevenueBudgetList;
      if (dictionary2.TryGetValue(key, out orderRevenueBudgetList))
        orderRevenueBudgetList.Add(orderRevenueBudget);
      else
        dictionary2.Add(key, new List<PMChangeOrderRevenueBudget>()
        {
          orderRevenueBudget
        });
      ProformaEntryExt.ChangeOrderTotals changeOrderTotals1;
      Decimal? nullable2;
      if (!dictionary1.TryGetValue(key, out changeOrderTotals1))
      {
        ProformaEntryExt.ChangeOrderTotals changeOrderTotals2 = new ProformaEntryExt.ChangeOrderTotals();
        changeOrderTotals2.Key = key;
        ref ProformaEntryExt.ChangeOrderTotals local1 = ref changeOrderTotals2;
        nullable2 = orderRevenueBudget.Amount;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        local1.Amount = valueOrDefault1;
        ref ProformaEntryExt.ChangeOrderTotals local2 = ref changeOrderTotals2;
        nullable2 = orderRevenueBudget.Qty;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        local2.Quantity = valueOrDefault2;
        changeOrderTotals1 = changeOrderTotals2;
        dictionary1.Add(key, changeOrderTotals1);
      }
      else
      {
        ref ProformaEntryExt.ChangeOrderTotals local3 = ref changeOrderTotals1;
        Decimal amount = local3.Amount;
        nullable2 = orderRevenueBudget.Amount;
        Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
        local3.Amount = amount + valueOrDefault3;
        ref ProformaEntryExt.ChangeOrderTotals local4 = ref changeOrderTotals1;
        Decimal quantity = local4.Quantity;
        nullable2 = orderRevenueBudget.Qty;
        Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
        local4.Quantity = quantity + valueOrDefault4;
        dictionary1[key] = changeOrderTotals1;
      }
    }
    PXReportResultset input = new PXReportResultset(new Type[13]
    {
      typeof (PMProformaProgressLine),
      typeof (PMProformaLineInfo),
      typeof (PMRevenueBudget),
      typeof (PMTask),
      typeof (PMProforma),
      typeof (PMProformaInfo),
      typeof (PMProject),
      typeof (PX.Objects.AR.Customer),
      typeof (PMAddress),
      typeof (PMContact),
      typeof (PX.Objects.GL.Branch),
      typeof (CompanyBAccount),
      typeof (PMSiteAddress)
    });
    PMAddress pmAddress = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelectReadonly<PMAddress, Where<PMAddress.addressID, Equal<Required<PMAddress.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Base.Document).Current.BillAddressID
    }));
    PMSiteAddress pmSiteAddress = PXResultset<PMSiteAddress>.op_Implicit(PXSelectBase<PMSiteAddress, PXSelectReadonly<PMSiteAddress, Where<PMSiteAddress.addressID, Equal<Required<PMSiteAddress.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.SiteAddressID
    }));
    PMContact pmContact = PXResultset<PMContact>.op_Implicit(PXSelectBase<PMContact, PXSelectReadonly<PMContact, Where<PMContact.contactID, Equal<Required<PMContact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Base.Document).Current.BillContactID
    }));
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Base.Document).Current.BranchID
    }));
    CompanyBAccount companyBaccount = PXResultset<CompanyBAccount>.op_Implicit(PXSelectBase<CompanyBAccount, PXSelectReadonly<CompanyBAccount, Where<CompanyBAccount.organizationID, Equal<Required<CompanyBAccount.organizationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branch.OrganizationID
    }));
    PXSelectReadonly<PMTask, Where<PMTask.projectID, Equal<Current<PMProforma.projectID>>>> pxSelectReadonly1 = new PXSelectReadonly<PMTask, Where<PMTask.projectID, Equal<Current<PMProforma.projectID>>>>((PXGraph) this.Base);
    Dictionary<int, PMTask> dictionary3 = new Dictionary<int, PMTask>();
    object[] objArray1 = Array.Empty<object>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) pxSelectReadonly1).Select(objArray1))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      dictionary3.Add(pmTask.TaskID.Value, pmTask);
    }
    Decimal num6 = 0M;
    PMProformaProgressLine proformaProgressLine1 = (PMProformaProgressLine) null;
    HashSet<ProformaEntry.ProformaTotalsCounter.AmountBaseKey> amountBaseKeySet = new HashSet<ProformaEntry.ProformaTotalsCounter.AmountBaseKey>();
    PMProformaInfo data1 = new PMProformaInfo();
    data1.RefNbr = ((PXSelectBase<PMProforma>) this.Base.Document).Current.RefNbr;
    data1.OriginalContractTotal = pmRevenueBudget1.CuryAmount;
    data1.PriorProformaLineTotal = nullable1;
    data1.CompletedToDateLineTotal = new Decimal?(num2);
    data1.RetainageHeldToDateTotal = new Decimal?(num1);
    data1.ProgressBillingBase = "Q";
    foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.Base.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine copy = PXCache<PMProformaProgressLine>.CreateCopy(PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult));
      PMRevenueBudget pmRevenueBudget2 = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
      PMProformaLineInfo data2 = new PMProformaLineInfo();
      if (copy.ProgressBillingBase == "A")
        data1.ProgressBillingBase = "A";
      data2.RefNbr = copy.RefNbr;
      data2.LineNbr = copy.LineNbr;
      data2.ChangeOrderAmountToDate = new Decimal?(0M);
      data2.ChangeOrderQtyToDate = new Decimal?(0M);
      Decimal num7 = 0M;
      Decimal num8 = 0M;
      Decimal num9 = 0M;
      Decimal num10 = 0M;
      Decimal num11 = 0M;
      Decimal? nullable3 = pmRevenueBudget2.Qty;
      Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
      nullable3 = pmRevenueBudget2.ChangeOrderQty;
      Decimal valueOrDefault6 = nullable3.GetValueOrDefault();
      Decimal num12 = valueOrDefault5 + valueOrDefault6;
      if (num12 != 0M)
      {
        nullable3 = pmRevenueBudget2.CuryAmount;
        Decimal valueOrDefault7 = nullable3.GetValueOrDefault();
        nullable3 = pmRevenueBudget2.CuryChangeOrderAmount;
        Decimal valueOrDefault8 = nullable3.GetValueOrDefault();
        num11 = (valueOrDefault7 + valueOrDefault8) / num12;
      }
      int? nullable4 = copy.TaskID;
      int valueOrDefault9 = nullable4.GetValueOrDefault();
      nullable4 = copy.CostCodeID;
      int valueOrDefault10 = nullable4.GetValueOrDefault();
      nullable4 = copy.InventoryID;
      int valueOrDefault11 = nullable4.GetValueOrDefault();
      nullable4 = copy.AccountGroupID;
      int valueOrDefault12 = nullable4.GetValueOrDefault();
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key1 = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(valueOrDefault9, valueOrDefault10, valueOrDefault11, valueOrDefault12);
      amountBaseKeySet.Add(key1);
      ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals;
      if (proformaTotalsToDate.TryGetValue(key1, out amountBaseTotals))
      {
        Decimal curyLineTotal = amountBaseTotals.CuryLineTotal;
        nullable3 = copy.CuryLineTotal;
        Decimal valueOrDefault13 = nullable3.GetValueOrDefault();
        num8 = curyLineTotal - valueOrDefault13;
        num7 = amountBaseTotals.CuryRetainage;
        if (num3 != 0M && billedRetainageToDate1.ContainsKey(this.Base.PayByLineOffKey))
        {
          Decimal num13 = amountBaseTotals.CuryRetainage / num3;
          Decimal d = billedRetainageToDate1[this.Base.PayByLineOffKey] * num13;
          num9 = Math.Round(d, 2);
          Decimal num14 = d - num9;
          num6 += num14;
        }
      }
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key2 = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(key1.TaskID, key1.CostCodeID, key1.InventoryID, key1.AccountGroupID);
      billedRetainageToDate1.TryGetValue(key2, out num10);
      if (copy.ProgressBillingBase == "A")
      {
        copy.CuryPreviouslyInvoiced = new Decimal?(num8);
        if (num11 != 0M)
        {
          PMProformaProgressLine proformaProgressLine2 = copy;
          nullable3 = copy.CuryLineTotal;
          Decimal? nullable5 = new Decimal?(nullable3.GetValueOrDefault() / num11);
          proformaProgressLine2.Qty = nullable5;
          data2.PreviousQty = new Decimal?(num8 / num11);
        }
        else
          data2.PreviousQty = new Decimal?(0M);
      }
      else if (copy.ProgressBillingBase == "Q")
      {
        ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals quantityBaseTotals = this.Base.TotalsCounter.GetQuantityBaseTotals((PXGraph) this.Base, ((PXSelectBase<PMProforma>) this.Base.Document).Current.RefNbr, copy);
        data2.PreviousQty = new Decimal?(quantityBaseTotals.QuantityTotal);
        copy.CuryPreviouslyInvoiced = new Decimal?(quantityBaseTotals.CuryLineTotal);
        Decimal num15 = 0.0M;
        Decimal num16 = 0.0M;
        nullable3 = pmRevenueBudget2.RevisedQty;
        if (nullable3.GetValueOrDefault() != 0M)
        {
          ProformaEntry graph = this.Base;
          string uom1 = copy.UOM;
          string uom2 = pmRevenueBudget2.UOM;
          Decimal quantityTotal = quantityBaseTotals.QuantityTotal;
          nullable3 = copy.Qty;
          Decimal valueOrDefault14 = nullable3.GetValueOrDefault();
          Decimal num17 = quantityTotal + valueOrDefault14;
          ref Decimal local = ref num15;
          if (INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, uom1, uom2, num17, INPrecision.QUANTITY, out local))
          {
            Decimal num18 = num15;
            nullable3 = pmRevenueBudget2.RevisedQty;
            Decimal num19 = nullable3.Value;
            num16 = Math.Round(num18 / num19, 2);
          }
        }
        data2.QuantityBaseCompleterdPct = new Decimal?(num16);
      }
      int costCodeID = key2.CostCodeID;
      int inventoryID = key2.InventoryID;
      int accountGroupId = key2.AccountGroupID;
      if (((PXSelectBase<PMProject>) this.Base.Project).Current.BudgetLevel == "T")
      {
        costCodeID = CostCodeAttribute.GetDefaultCostCode();
        inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;
      }
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key3 = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(key2.TaskID, costCodeID, inventoryID, accountGroupId);
      ProformaEntryExt.ChangeOrderTotals changeOrderTotals;
      if (dictionary1.TryGetValue(key3, out changeOrderTotals))
      {
        data2.ChangeOrderAmountToDate = new Decimal?(changeOrderTotals.Amount);
        data2.ChangeOrderQtyToDate = new Decimal?(changeOrderTotals.Quantity);
      }
      PMProformaLineInfo proformaLineInfo = this.CustomizeProformaLineInfo(data2);
      if (((PXSelectBase<PMProject>) this.Base.Project).Current.RetainageMode == "C")
        copy.CuryRetainage = copy.CuryAllocatedRetainedAmount;
      else
        copy.CuryRetainage = new Decimal?(num7 - (num9 + num10));
      if (proformaProgressLine1 == null)
      {
        proformaProgressLine1 = copy;
      }
      else
      {
        nullable3 = copy.CuryRetainage;
        Decimal? curyRetainage = proformaProgressLine1.CuryRetainage;
        if (nullable3.GetValueOrDefault() > curyRetainage.GetValueOrDefault() & nullable3.HasValue & curyRetainage.HasValue)
          proformaProgressLine1 = copy;
      }
      PXReportResultset pxReportResultset = input;
      object[] objArray2 = new object[13];
      objArray2[0] = (object) copy;
      objArray2[1] = (object) proformaLineInfo;
      objArray2[2] = (object) pmRevenueBudget2;
      Dictionary<int, PMTask> dictionary4 = dictionary3;
      nullable4 = copy.TaskID;
      int key4 = nullable4.Value;
      objArray2[3] = (object) dictionary4[key4];
      objArray2[4] = (object) ((PXSelectBase<PMProforma>) this.Base.Document).Current;
      objArray2[5] = (object) data1;
      objArray2[6] = (object) ((PXSelectBase<PMProject>) this.Base.Project).Current;
      objArray2[7] = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.Customer).Current;
      objArray2[8] = (object) pmAddress;
      objArray2[9] = (object) pmContact;
      objArray2[10] = (object) branch;
      objArray2[11] = (object) companyBaccount;
      objArray2[12] = (object) pmSiteAddress;
      pxReportResultset.Add(objArray2);
    }
    List<PMChangeOrderRevenueBudget> source = new List<PMChangeOrderRevenueBudget>();
    foreach (KeyValuePair<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, List<PMChangeOrderRevenueBudget>> keyValuePair in dictionary2)
    {
      if (!amountBaseKeySet.Contains(keyValuePair.Key))
        source.AddRange((IEnumerable<PMChangeOrderRevenueBudget>) keyValuePair.Value);
    }
    PXSelectReadonly<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Current<PMProforma.projectID>>, And<PMChangeOrder.approved, Equal<True>, And<PMChangeOrder.completionDate, LessEqual<Current<PMProforma.invoiceDate>>>>>> pxSelectReadonly2 = new PXSelectReadonly<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Current<PMProforma.projectID>>, And<PMChangeOrder.approved, Equal<True>, And<PMChangeOrder.completionDate, LessEqual<Current<PMProforma.invoiceDate>>>>>>((PXGraph) this.Base);
    Decimal num20 = 0M;
    Decimal num21 = 0M;
    Decimal num22 = 0M;
    Decimal num23 = 0M;
    object[] objArray3 = Array.Empty<object>();
    foreach (PXResult<PMChangeOrder> pxResult in ((PXSelectBase<PMChangeOrder>) pxSelectReadonly2).Select(objArray3))
    {
      PMChangeOrder order = PXResult<PMChangeOrder>.op_Implicit(pxResult);
      Decimal? nullable6 = source.Where<PMChangeOrderRevenueBudget>((Func<PMChangeOrderRevenueBudget, bool>) (x => x.RefNbr == order.RefNbr)).Sum<PMChangeOrderRevenueBudget>((Func<PMChangeOrderRevenueBudget, Decimal?>) (x => x.Amount));
      Decimal valueOrDefault = nullable6.GetValueOrDefault();
      nullable6 = order.RevenueTotal;
      Decimal num24 = nullable6.GetValueOrDefault() - valueOrDefault;
      if (cutoffDate.HasValue)
      {
        DateTime dateTime = order.CompletionDate.Value;
        DateTime date1 = dateTime.Date;
        dateTime = cutoffDate.Value;
        DateTime date2 = dateTime.Date;
        if (date1 <= date2)
        {
          nullable6 = order.RevenueTotal;
          if (nullable6.GetValueOrDefault() < 0M)
          {
            num23 -= num24;
            continue;
          }
          num22 += num24;
          continue;
        }
      }
      nullable6 = order.RevenueTotal;
      if (nullable6.GetValueOrDefault() < 0M)
        num21 -= num24;
      else
        num20 += num24;
    }
    data1.ChangeOrderTotal = new Decimal?(num22 + num20 - num23 - num21);
    data1.ChangeOrderAdditions = new Decimal?(num20);
    data1.ChangeOrderAdditionsPrevious = new Decimal?(num22);
    data1.ChangeOrderDeduction = new Decimal?(num21);
    data1.ChangeOrderDeductionPrevious = new Decimal?(num23);
    PMProformaInfo pmProformaInfo = data1;
    Decimal? nullable7 = data1.OriginalContractTotal;
    Decimal? nullable8 = data1.ChangeOrderTotal;
    Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
    pmProformaInfo.RevisedContractTotal = nullable9;
    this.CustomizeProformaInfo(data1);
    if (proformaProgressLine1 != null)
    {
      PMProformaProgressLine proformaProgressLine3 = proformaProgressLine1;
      nullable8 = proformaProgressLine3.CuryRetainage;
      Decimal num25 = num6;
      Decimal? nullable10;
      if (!nullable8.HasValue)
      {
        nullable7 = new Decimal?();
        nullable10 = nullable7;
      }
      else
        nullable10 = new Decimal?(nullable8.GetValueOrDefault() + num25);
      proformaProgressLine3.CuryRetainage = nullable10;
    }
    if (this.GroupByTask())
      input = this.GroupResultsetByTask(input);
    return input;
  }

  public virtual PXReportResultset GroupResultsetByTask(PXReportResultset input)
  {
    PXReportResultset pxReportResultset = new PXReportResultset(new Type[13]
    {
      typeof (PMProformaProgressLine),
      typeof (PMProformaLineInfo),
      typeof (PMRevenueBudget),
      typeof (PMTask),
      typeof (PMProforma),
      typeof (PMProformaInfo),
      typeof (PMProject),
      typeof (PX.Objects.AR.Customer),
      typeof (PMAddress),
      typeof (PMContact),
      typeof (PX.Objects.GL.Branch),
      typeof (CompanyBAccount),
      typeof (PMSiteAddress)
    });
    Dictionary<int, object[]> dictionary1 = new Dictionary<int, object[]>();
    foreach (object[] objArray1 in input)
    {
      PMProformaProgressLine record1 = (PMProformaProgressLine) objArray1[0];
      PMProformaLineInfo record2 = (PMProformaLineInfo) objArray1[1];
      PMRevenueBudget record3 = (PMRevenueBudget) objArray1[2];
      Dictionary<int, object[]> dictionary2 = dictionary1;
      int? taskId = record1.TaskID;
      int key1 = taskId.Value;
      object[] objArray2;
      ref object[] local = ref objArray2;
      if (dictionary2.TryGetValue(key1, out local))
      {
        PMProformaProgressLine summary1 = (PMProformaProgressLine) objArray2[0];
        PMProformaLineInfo summary2 = (PMProformaLineInfo) objArray2[1];
        PMRevenueBudget summary3 = (PMRevenueBudget) objArray2[2];
        this.BudgetAddToSummary(summary3, record3);
        this.LineAddToSummary(summary1, record1);
        this.InfoAddToSummary(summary2, record2);
        if (record1.ProgressBillingBase == "Q")
        {
          Decimal num = summary3.Qty.GetValueOrDefault() + summary2.ChangeOrderQtyToDate.GetValueOrDefault();
          Decimal result;
          if (num != 0.0M && INUnitAttribute.TryConvertGlobalUnits((PXGraph) this.Base, summary1.UOM, summary3.UOM, summary2.PreviousQty.GetValueOrDefault() + summary1.Qty.GetValueOrDefault(), INPrecision.QUANTITY, out result))
            summary2.QuantityBaseCompleterdPct = new Decimal?(Math.Round(result / num, 2));
        }
      }
      else
      {
        pxReportResultset.Add(objArray1);
        Dictionary<int, object[]> dictionary3 = dictionary1;
        taskId = record1.TaskID;
        int key2 = taskId.Value;
        object[] objArray3 = objArray1;
        dictionary3.Add(key2, objArray3);
      }
    }
    return pxReportResultset;
  }

  public virtual bool GroupByTask()
  {
    return ((PXSelectBase<PMProject>) this.Base.Project).Current.AIALevel == "S";
  }

  public virtual void BudgetAddToSummary(PMRevenueBudget summary, PMRevenueBudget record)
  {
    PMRevenueBudget pmRevenueBudget1 = summary;
    Decimal? curyAmount = summary.CuryAmount;
    Decimal valueOrDefault1 = curyAmount.GetValueOrDefault();
    curyAmount = record.CuryAmount;
    Decimal valueOrDefault2 = curyAmount.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    pmRevenueBudget1.CuryAmount = nullable1;
    PMRevenueBudget pmRevenueBudget2 = summary;
    Decimal? qty = summary.Qty;
    Decimal valueOrDefault3 = qty.GetValueOrDefault();
    qty = record.Qty;
    Decimal valueOrDefault4 = qty.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    pmRevenueBudget2.Qty = nullable2;
  }

  public virtual void InfoAddToSummary(PMProformaLineInfo summary, PMProformaLineInfo record)
  {
    PMProformaLineInfo proformaLineInfo1 = summary;
    Decimal? orderAmountToDate = summary.ChangeOrderAmountToDate;
    Decimal valueOrDefault1 = orderAmountToDate.GetValueOrDefault();
    orderAmountToDate = record.ChangeOrderAmountToDate;
    Decimal valueOrDefault2 = orderAmountToDate.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    proformaLineInfo1.ChangeOrderAmountToDate = nullable1;
    PMProformaLineInfo proformaLineInfo2 = summary;
    Decimal? changeOrderQtyToDate = summary.ChangeOrderQtyToDate;
    Decimal valueOrDefault3 = changeOrderQtyToDate.GetValueOrDefault();
    changeOrderQtyToDate = record.ChangeOrderQtyToDate;
    Decimal valueOrDefault4 = changeOrderQtyToDate.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    proformaLineInfo2.ChangeOrderQtyToDate = nullable2;
    PMProformaLineInfo proformaLineInfo3 = summary;
    Decimal? previousQty = summary.PreviousQty;
    Decimal valueOrDefault5 = previousQty.GetValueOrDefault();
    previousQty = record.PreviousQty;
    Decimal valueOrDefault6 = previousQty.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    proformaLineInfo3.PreviousQty = nullable3;
  }

  public virtual void LineAddToSummary(
    PMProformaProgressLine summary,
    PMProformaProgressLine record)
  {
    PMProformaProgressLine proformaProgressLine1 = summary;
    Decimal? curyAmount = summary.CuryAmount;
    Decimal valueOrDefault1 = curyAmount.GetValueOrDefault();
    curyAmount = record.CuryAmount;
    Decimal valueOrDefault2 = curyAmount.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    proformaProgressLine1.CuryAmount = nullable1;
    PMProformaProgressLine proformaProgressLine2 = summary;
    Decimal? amount = summary.Amount;
    Decimal valueOrDefault3 = amount.GetValueOrDefault();
    amount = record.Amount;
    Decimal valueOrDefault4 = amount.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    proformaProgressLine2.Amount = nullable2;
    PMProformaProgressLine proformaProgressLine3 = summary;
    Decimal? materialStoredAmount1 = summary.CuryMaterialStoredAmount;
    Decimal valueOrDefault5 = materialStoredAmount1.GetValueOrDefault();
    materialStoredAmount1 = record.CuryMaterialStoredAmount;
    Decimal valueOrDefault6 = materialStoredAmount1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    proformaProgressLine3.CuryMaterialStoredAmount = nullable3;
    PMProformaProgressLine proformaProgressLine4 = summary;
    Decimal? materialStoredAmount2 = summary.MaterialStoredAmount;
    Decimal valueOrDefault7 = materialStoredAmount2.GetValueOrDefault();
    materialStoredAmount2 = record.MaterialStoredAmount;
    Decimal valueOrDefault8 = materialStoredAmount2.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault7 + valueOrDefault8);
    proformaProgressLine4.MaterialStoredAmount = nullable4;
    PMProformaProgressLine proformaProgressLine5 = summary;
    Decimal? curyRetainage = summary.CuryRetainage;
    Decimal valueOrDefault9 = curyRetainage.GetValueOrDefault();
    curyRetainage = record.CuryRetainage;
    Decimal valueOrDefault10 = curyRetainage.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(valueOrDefault9 + valueOrDefault10);
    proformaProgressLine5.CuryRetainage = nullable5;
    PMProformaProgressLine proformaProgressLine6 = summary;
    Decimal? retainage = summary.Retainage;
    Decimal valueOrDefault11 = retainage.GetValueOrDefault();
    retainage = record.Retainage;
    Decimal valueOrDefault12 = retainage.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(valueOrDefault11 + valueOrDefault12);
    proformaProgressLine6.Retainage = nullable6;
    PMProformaProgressLine proformaProgressLine7 = summary;
    Decimal? curyLineTotal = summary.CuryLineTotal;
    Decimal valueOrDefault13 = curyLineTotal.GetValueOrDefault();
    curyLineTotal = record.CuryLineTotal;
    Decimal valueOrDefault14 = curyLineTotal.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault13 + valueOrDefault14);
    proformaProgressLine7.CuryLineTotal = nullable7;
    PMProformaProgressLine proformaProgressLine8 = summary;
    Decimal? lineTotal = summary.LineTotal;
    Decimal valueOrDefault15 = lineTotal.GetValueOrDefault();
    lineTotal = record.LineTotal;
    Decimal valueOrDefault16 = lineTotal.GetValueOrDefault();
    Decimal? nullable8 = new Decimal?(valueOrDefault15 + valueOrDefault16);
    proformaProgressLine8.LineTotal = nullable8;
    PMProformaProgressLine proformaProgressLine9 = summary;
    Decimal? previouslyInvoiced1 = summary.CuryPreviouslyInvoiced;
    Decimal valueOrDefault17 = previouslyInvoiced1.GetValueOrDefault();
    previouslyInvoiced1 = record.CuryPreviouslyInvoiced;
    Decimal valueOrDefault18 = previouslyInvoiced1.GetValueOrDefault();
    Decimal? nullable9 = new Decimal?(valueOrDefault17 + valueOrDefault18);
    proformaProgressLine9.CuryPreviouslyInvoiced = nullable9;
    PMProformaProgressLine proformaProgressLine10 = summary;
    Decimal? previouslyInvoiced2 = summary.PreviouslyInvoiced;
    Decimal valueOrDefault19 = previouslyInvoiced2.GetValueOrDefault();
    previouslyInvoiced2 = record.PreviouslyInvoiced;
    Decimal valueOrDefault20 = previouslyInvoiced2.GetValueOrDefault();
    Decimal? nullable10 = new Decimal?(valueOrDefault19 + valueOrDefault20);
    proformaProgressLine10.PreviouslyInvoiced = nullable10;
    PMProformaProgressLine proformaProgressLine11 = summary;
    Decimal? qty = summary.Qty;
    Decimal valueOrDefault21 = qty.GetValueOrDefault();
    qty = record.Qty;
    Decimal valueOrDefault22 = qty.GetValueOrDefault();
    Decimal? nullable11 = new Decimal?(valueOrDefault21 + valueOrDefault22);
    proformaProgressLine11.Qty = nullable11;
  }

  public virtual PMProformaInfo CustomizeProformaInfo(PMProformaInfo data) => data;

  public virtual PMProformaLineInfo CustomizeProformaLineInfo(PMProformaLineInfo data) => data;

  public virtual Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals> GetProformaTotalsToDate()
  {
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals> proformaTotalsToDate = new Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals>();
    foreach (PXResult<PMProformaLine> pxResult in ((PXSelectBase<PMProformaLine>) new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PMProforma, On<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>, And<PMProforma.curyID, Equal<Current<PMProforma.curyID>>>>>>, Where<PMProformaLine.refNbr, LessEqual<Current<PMProforma.refNbr>>, And<PMProformaLine.projectID, Equal<Current<PMProforma.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, NotEqual<True>>>>>, Aggregate<GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.accountGroupID, Sum<PMProformaLine.curyRetainage, Sum<PMProformaLine.retainage, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.qty>>>>>>>>>>>((PXGraph) this.Base)).Select(Array.Empty<object>()))
    {
      PMProformaLine pmProformaLine = PXResult<PMProformaLine>.op_Implicit(pxResult);
      int valueOrDefault1 = pmProformaLine.TaskID.GetValueOrDefault();
      int valueOrDefault2 = pmProformaLine.CostCodeID.GetValueOrDefault();
      int? nullable1 = pmProformaLine.InventoryID;
      int valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = pmProformaLine.AccountGroupID;
      int valueOrDefault4 = nullable1.GetValueOrDefault();
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(valueOrDefault1, valueOrDefault2, valueOrDefault3, valueOrDefault4);
      ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals = new ProformaEntry.ProformaTotalsCounter.AmountBaseTotals();
      amountBaseTotals.Key = key;
      ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local1 = ref amountBaseTotals;
      Decimal? nullable2 = pmProformaLine.CuryRetainage;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      local1.CuryRetainage = valueOrDefault5;
      ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local2 = ref amountBaseTotals;
      nullable2 = pmProformaLine.Retainage;
      Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
      local2.Retainage = valueOrDefault6;
      ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local3 = ref amountBaseTotals;
      nullable2 = pmProformaLine.CuryLineTotal;
      Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
      local3.CuryLineTotal = valueOrDefault7;
      ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local4 = ref amountBaseTotals;
      nullable2 = pmProformaLine.LineTotal;
      Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
      local4.LineTotal = valueOrDefault8;
      proformaTotalsToDate.Add(key, amountBaseTotals);
    }
    return proformaTotalsToDate;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProforma> e)
  {
    if (this.Base.SuppressRowSeleted || e.Row == null)
      return;
    bool flag = ProformaEntryExt.IsProFormaCreatedAndSaved(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, e.Row);
    PXUIFieldAttribute.SetEnabled<PMProforma.projectNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, e.Row.Hold.GetValueOrDefault());
    ((PXAction) this.substantiatedBilling).SetEnabled(flag);
    ((PXAction) this.substantiatedBillingConsolidated).SetEnabled(flag);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProforma, PMProforma.projectNbr> e)
  {
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProforma, PMProforma.projectNbr>, PMProforma, object>) e).NewValue;
    if (string.IsNullOrEmpty(newValue) || newValue.Equals("N/A", StringComparison.InvariantCultureIgnoreCase))
      return;
    PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProforma.projectID>>, And<PMProforma.projectNbr, Equal<Required<PMProforma.projectNbr>>, And<PMProforma.corrected, NotEqual<True>>>>> pxSelect = new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProforma.projectID>>, And<PMProforma.projectNbr, Equal<Required<PMProforma.projectNbr>>, And<PMProforma.corrected, NotEqual<True>>>>>((PXGraph) this.Base);
    if (ProformaEntryExt.IsProFormaCreatedAndSaved(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProforma, PMProforma.projectNbr>>) e).Cache, e.Row))
      ((PXSelectBase<PMProforma>) pxSelect).WhereAnd<Where<PMProforma.refNbr, NotEqual<Current<PMProforma.refNbr>>>>();
    PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(((PXSelectBase<PMProforma>) pxSelect).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProforma, PMProforma.projectNbr>, PMProforma, object>) e).NewValue
    }));
    if (pmProforma != null)
      throw new PXSetPropertyException("The project already has the {0} pro forma invoice with this number.", new object[1]
      {
        (object) pmProforma.RefNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectNbr> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.ProjectProperties).Select(Array.Empty<object>()));
    if (pmProject == null || !(e.Row.ProjectNbr != "N/A") || !string.IsNullOrEmpty(pmProject.LastProformaNumber) && (string.IsNullOrEmpty(e.Row.ProjectNbr) || !(NumberHelper.GetTextPrefix(e.Row.ProjectNbr) == NumberHelper.GetTextPrefix(pmProject.LastProformaNumber)) || NumberHelper.GetNumericValue(e.Row.ProjectNbr) < NumberHelper.GetNumericValue(pmProject.LastProformaNumber)))
      return;
    pmProject.LastProformaNumber = e.Row.ProjectNbr;
    ((PXSelectBase<PMProject>) this.ProjectProperties).Update(pmProject);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProforma> e)
  {
    PMProformaRevision proformaRevision = PXResultset<PMProformaRevision>.op_Implicit(((PXSelectBase<PMProformaRevision>) new PXSelect<PMProformaRevision, Where<PMProformaRevision.refNbr, Equal<Required<PMProforma.refNbr>>, And<PMProformaRevision.revisionID, NotEqual<Required<PMProforma.revisionID>>>>, OrderBy<Desc<PMProformaRevision.revisionID>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[2]
    {
      (object) e.Row.RefNbr,
      (object) e.Row.RevisionID
    }));
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.ProjectProperties).Select(Array.Empty<object>()));
    if (pmProject == null || !(pmProject.LastProformaNumber == e.Row.ProjectNbr) || string.IsNullOrEmpty(pmProject.LastProformaNumber) || proformaRevision != null)
      return;
    pmProject.LastProformaNumber = NumberHelper.DecreaseNumber(pmProject.LastProformaNumber, 1);
    ((PXSelectBase<PMProject>) this.ProjectProperties).Update(pmProject);
  }

  private static bool IsProFormaCreatedAndSaved(PXCache cache, PMProforma proforma)
  {
    return cache != null && proforma != null && cache.GetStatus((object) proforma) != 2;
  }

  public struct ChangeOrderTotals
  {
    public ProformaEntry.ProformaTotalsCounter.AmountBaseKey Key { get; set; }

    public Decimal Amount { get; set; }

    public Decimal Quantity { get; set; }
  }
}
