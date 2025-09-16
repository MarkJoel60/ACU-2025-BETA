// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.TemplateMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CT;

public class TemplateMaint : PXGraph<TemplateMaint, ContractTemplate>
{
  public PXSelect<ContractTemplate, Where<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>> Templates;
  public PXSelect<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<ContractTemplate.contractID>>>> CurrentTemplate;
  public PXSelect<Contract, Where<Contract.templateID, Equal<Current<ContractTemplate.contractID>>>> Contracts;
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<ContractTemplate.contractID>>>> Billing;
  public PXSelect<ContractSLAMapping, Where<ContractSLAMapping.contractID, Equal<Current<ContractTemplate.contractID>>>> SLAMapping;
  public PXSelectJoin<ContractDetail, LeftJoin<ContractItem, On<ContractDetail.contractItemID, Equal<ContractItem.contractItemID>>>, Where<ContractDetail.contractID, Equal<Current<ContractTemplate.contractID>>>> ContractDetails;
  public PXSetup<PX.Objects.GL.Company> Company;
  public CSAttributeGroupList<ContractTemplate.contractID, Contract> AttributeGroup;
  public PXSelect<PX.Objects.AR.Customer> customer;
  public PXSelect<PX.Objects.CR.Location> customerLocation;
  public PXSelect<PX.Objects.IN.InventoryItem> inventoryItem;
  public PXSelect<PMTran> pmTran;
  public PXSelect<UsageData> usageData;
  public PXSelect<ContractItem> contractItem;
  public PXSelect<AccessInfo> accessInfo;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (ContractTemplate.contractID))]
  [PXParent(typeof (Select<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<ContractBillingSchedule.contractID>>>>))]
  protected virtual void ContractBillingSchedule_ContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (ContractTemplate.contractID))]
  [PXParent(typeof (Select<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<ContractSLAMapping.contractID>>>>))]
  protected virtual void ContractSLAMapping_ContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (ContractTemplate.contractID))]
  [PXParent(typeof (Select<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<ContractDetail.contractID>>>>))]
  protected virtual void ContractDetail_ContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(MinValue = 1)]
  [PXDefault(typeof (ContractTemplate.revID))]
  protected virtual void ContractDetail_RevID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [ContractLineNbr(typeof (ContractTemplate.lineCtr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  protected virtual void ContractDetail_LineNbr_CacheAttached(PXCache sender)
  {
  }

  public TemplateMaint()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.customerModule>())
    {
      PXUIFieldAttribute.SetVisible<ContractTemplate.caseItemID>(((PXSelectBase) this.Templates).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<Contract.min>(((PXSelectBase) this.Templates).Cache, (object) null, false);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(TemplateMaint.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (TemplateMaint.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXFieldDefaulting((object) TemplateMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__5_0))));
  }

  protected virtual void ContractTemplate_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.SetControlsState(e.Row as ContractTemplate, sender);
  }

  protected virtual void ContractTemplate_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    ContractTemplate row = e.Row as ContractTemplate;
    this.ValidateRefundPeriod(sender, row);
  }

  protected virtual void ContractTemplate_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is ContractTemplate row))
      return;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Insert(new ContractBillingSchedule()
    {
      ContractID = row.ContractID
    });
    if (((PXSelectBase) this.SLAMapping).Cache.GetStateExt<ContractSLAMapping.severity>((object) null) is PXStringState stateExt && stateExt.AllowedValues != null && stateExt.AllowedValues.Length != 0)
    {
      foreach (string allowedValue in stateExt.AllowedValues)
        ((PXSelectBase<ContractSLAMapping>) this.SLAMapping).Insert(new ContractSLAMapping()
        {
          ContractID = row.ContractID,
          Severity = allowedValue
        });
    }
    ((PXSelectBase) this.Billing).Cache.IsDirty = false;
    ((PXSelectBase) this.SLAMapping).Cache.IsDirty = false;
  }

  protected virtual void ContractTemplate_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is ContractTemplate && PXResultset<ContractTemplate>.op_Implicit(PXSelectBase<ContractTemplate, PXSelect<ContractTemplate, Where<ContractTemplate.templateID, Equal<Current<ContractTemplate.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted. One or more contracts are referencing this document.");
    }
  }

  protected virtual void ContractTemplate_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ContractTemplate row) || ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current == null)
      return;
    row.CuryID = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
  }

  protected virtual void ContractTemplate_Type_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractTemplate row = e.Row as ContractTemplate;
    this.SetControlsState(row, sender);
    if (row == null || !(row.Type == "U"))
      return;
    row.AutoRenew = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ContractTemplate, Contract.refundable> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ContractTemplate, Contract.refundable>, ContractTemplate, object>) e).OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.NewValue;
    if (nullable.GetValueOrDefault())
      return;
    e.Row.RefundPeriod = new int?(0);
  }

  private void SetControlsState(ContractTemplate row, PXCache sender)
  {
    ((PXSelectBase) this.Contracts).Cache.AllowInsert = false;
    ((PXSelectBase) this.Contracts).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Contracts).Cache.AllowDelete = false;
    if (row != null)
    {
      PXUIFieldAttribute.SetVisible<ContractTemplate.curyID>(sender, (object) row, TemplateMaint.IsMultyCurrency);
      PXUIFieldAttribute.SetVisible<ContractTemplate.rateTypeID>(sender, (object) row, TemplateMaint.IsMultyCurrency);
      PXUIFieldAttribute.SetVisible<Contract.allowOverrideCury>(sender, (object) row, TemplateMaint.IsMultyCurrency);
      PXUIFieldAttribute.SetVisible<Contract.allowOverrideRate>(sender, (object) row, TemplateMaint.IsMultyCurrency);
      PXUIFieldAttribute.SetEnabled<ContractTemplate.isContinuous>(sender, (object) row, row.Type == "R");
      PXUIFieldAttribute.SetEnabled<Contract.autoRenew>(sender, (object) row, row.Type != "U");
      PXUIFieldAttribute.SetEnabled<Contract.duration>(sender, (object) row, row.Type != "U");
      PXUIFieldAttribute.SetEnabled<ContractTemplate.durationType>(sender, (object) row, row.Type != "U");
      PXUIFieldAttribute.SetEnabled<ContractTemplate.expireDate>(sender, (object) row, row.Type != "U");
      PXUIFieldAttribute.SetEnabled<ContractTemplate.isContinuous>(sender, (object) row, row.Type != "U");
      PXUIFieldAttribute.SetEnabled<Contract.refundPeriod>(sender, (object) row, row.Refundable.GetValueOrDefault());
      ContractDetail contractDetail = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Current<ContractTemplate.contractID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
      PXUIFieldAttribute.SetEnabled<ContractTemplate.curyID>(sender, (object) row, contractDetail == null);
    }
    PXCache cache = ((PXSelectBase) this.AttributeGroup).Cache;
    int? contractId = row.ContractID;
    int num1 = 0;
    int num2 = contractId.GetValueOrDefault() > num1 & contractId.HasValue ? 1 : (((PXGraph) this).IsCopyPasteContext ? 1 : 0);
    cache.AllowInsert = num2 != 0;
  }

  private int GetMaximumDurationInDays(ContractTemplate contract)
  {
    switch (contract.DurationType)
    {
      case "A":
        return contract.Duration.Value * 366;
      case "Q":
        return contract.Duration.Value * 92;
      case "M":
        return contract.Duration.Value * 31 /*0x1F*/;
      case "C":
        return contract.Duration.Value;
      default:
        return 0;
    }
  }

  private void ValidateRefundPeriod(PXCache sender, ContractTemplate contract)
  {
    if (contract == null || !contract.Refundable.GetValueOrDefault() || !(contract.Type != "U"))
      return;
    int? refundPeriod = contract.RefundPeriod;
    int maximumDurationInDays = this.GetMaximumDurationInDays(contract);
    if (!(refundPeriod.GetValueOrDefault() > maximumDurationInDays & refundPeriod.HasValue))
      return;
    sender.RaiseExceptionHandling<Contract.refundPeriod>((object) contract, (object) contract.RefundPeriod, (Exception) new PXSetPropertyException("The value in the Refund Period box cannot exceed the contract duration."));
  }

  protected virtual void ContractDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    row.ContractID = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current.ContractID;
    this.ValidateUniqueness(sender, row, (CancelEventArgs) e);
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractItem == null)
      return;
    row.Qty = contractItem.DefaultQty;
  }

  protected virtual void ContractDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (((PXGraph) this).IsImport || !(e.Row is ContractDetail row))
      return;
    ContractItem contractItem1 = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractItem1 == null)
      return;
    bool? deposit = contractItem1.Deposit;
    bool flag = false;
    if (!(deposit.GetValueOrDefault() == flag & deposit.HasValue) || !contractItem1.DepositItemID.HasValue)
      return;
    ContractItem contractItem2 = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contractItem1.DepositItemID
    }));
    ContractDetail contractDetail = new ContractDetail();
    sender.SetValueExt<ContractDetail.contractItemID>((object) contractDetail, (object) contractItem2.ContractItemID);
    ((PXSelectBase<ContractDetail>) this.ContractDetails).Insert(contractDetail);
    ((PXSelectBase) this.ContractDetails).View.RequestRefresh();
  }

  protected virtual void ContractDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is ContractDetail newRow))
      return;
    this.ValidateUniqueness(sender, newRow, (CancelEventArgs) e);
  }

  protected virtual void ContractDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractItem == null || ContractItemMaint.IsValidItemPrice((PXGraph) this, contractItem))
      return;
    PXUIFieldAttribute.SetWarning<ContractDetail.contractItemID>(sender, (object) row, "The item has no price set up in the currency selected in the Currency ID box.");
  }

  protected virtual void ContractDetail_ContractItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractDetail row = e.Row as ContractDetail;
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractItem.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractItem == null || row == null)
      return;
    row.Description = contractItem.Descr;
    PXDBLocalizableStringAttribute.CopyTranslations<ContractItem.descr, ContractDetail.description>(((PXGraph) this).Caches[typeof (ContractItem)], (object) contractItem, ((PXGraph) this).Caches[typeof (ContractDetail)], (object) row);
    row.Qty = contractItem.DefaultQty;
  }

  protected virtual void ContractDetail_ContractItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ContractDetail row = (ContractDetail) e.Row;
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    ContractTemplate contractTemplate = PXResultset<ContractTemplate>.op_Implicit(PXSelectBase<ContractTemplate, PXSelect<ContractTemplate, Where<ContractTemplate.contractID, Equal<Required<ContractDetail.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractID
    }));
    if (contractItem != null && contractTemplate != null && contractItem.CuryID != contractTemplate.CuryID)
    {
      e.NewValue = (object) contractItem.ContractItemCD;
      throw new PXSetPropertyException("The contract item '{0}' cannot be added because its currency ({1}) does not match the {3} currency ({2})", new object[4]
      {
        (object) contractItem.ContractItemCD,
        (object) contractItem.CuryID,
        (object) contractTemplate.CuryID,
        (object) PXUIFieldAttribute.GetItemName(((PXSelectBase) this.CurrentTemplate).Cache)
      });
    }
  }

  protected virtual void ContractDetail_Qty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((ContractDetail) e.Row).ContractItemID
    }));
    if (contractItem != null)
    {
      Decimal? maxQty = contractItem.MaxQty;
      Decimal? newValue1 = (Decimal?) e.NewValue;
      if (!(maxQty.GetValueOrDefault() < newValue1.GetValueOrDefault() & maxQty.HasValue & newValue1.HasValue))
      {
        Decimal? minQty = contractItem.MinQty;
        Decimal? newValue2 = (Decimal?) e.NewValue;
        if (!(minQty.GetValueOrDefault() > newValue2.GetValueOrDefault() & minQty.HasValue & newValue2.HasValue))
          return;
      }
      throw new PXSetPropertyException("Included Quantity must be within the {0} and {1} limits.", new object[2]
      {
        (object) PXDBQuantityAttribute.Round(new Decimal?(contractItem.MinQty.GetValueOrDefault())),
        (object) PXDBQuantityAttribute.Round(new Decimal?(contractItem.MaxQty.GetValueOrDefault()))
      });
    }
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<CSAttributeGroup.entityClassID>>>>), LeaveChildren = true)]
  [PXDBDefault(typeof (ContractTemplate.contractStrID))]
  protected virtual void CSAttributeGroup_EntityClassID_CacheAttached(PXCache sender)
  {
  }

  public virtual void Persist()
  {
    List<ContractDetail> list = GraphHelper.RowCast<ContractDetail>((IEnumerable) ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>())).ToList<ContractDetail>();
    foreach (ContractDetail detail in list)
    {
      ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
      string itemCD;
      if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null && ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type == "D" && !TemplateMaint.ValidItemForOnDemand((PXGraph) this, detail, out itemCD))
      {
        ((PXSelectBase) this.ContractDetails).Cache.RaiseExceptionHandling<ContractDetail.contractItemID>((object) detail, (object) itemCD, (Exception) new PXException("For contracts with billing on demand, items cannot have any recurring settings."));
        ((PXSelectBase) this.ContractDetails).Cache.SetStatus((object) detail, (PXEntryStatus) 1);
      }
    }
    TemplateMaint.CheckContractOnDepositItems(list, (Contract) ((PXSelectBase<ContractTemplate>) this.Templates).Current);
    ((PXGraph) this).Persist();
  }

  private void ValidateUniqueness(PXCache sender, ContractDetail row, CancelEventArgs e)
  {
    if (!row.ContractItemID.HasValue)
      return;
    if (PXResultset<ContractDetail>.op_Implicit(((PXSelectBase<ContractDetail>) new PXSelect<ContractDetail, Where<ContractDetail.contractItemID, Equal<Required<ContractDetail.contractItemID>>, And<ContractDetail.contractID, Equal<Current<ContractTemplate.contractID>>, And<ContractDetail.contractDetailID, NotEqual<Required<ContractDetail.contractDetailID>>>>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) row.ContractItemID,
      (object) row.ContractDetailID
    })) == null)
      return;
    ContractItem contractItem = (ContractItem) PXSelectorAttribute.Select<ContractDetail.contractItemID>(sender, (object) row);
    sender.RaiseExceptionHandling<ContractDetail.contractItemID>((object) row, (object) contractItem.ContractItemCD, (Exception) new PXException("Item with this type and Inventory ID already exists for the selected contract."));
    e.Cancel = true;
  }

  private static bool IsMultyCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  public static bool ValidItemForOnDemand(PXGraph graph, ContractDetail detail, out string itemCD)
  {
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select(graph, new object[1]
    {
      (object) detail.ContractItemID
    }));
    int num1;
    if (contractItem != null)
    {
      int? nullable = contractItem.RecurringItemID;
      if (nullable.HasValue)
      {
        nullable = contractItem.DepositItemID;
        if (!nullable.HasValue)
        {
          Decimal? qty = detail.Qty;
          Decimal num2 = 0M;
          num1 = qty.GetValueOrDefault() > num2 & qty.HasValue ? 1 : 0;
          goto label_5;
        }
      }
    }
    num1 = 0;
label_5:
    bool flag = num1 != 0;
    itemCD = flag ? contractItem.ContractItemCD : (string) null;
    return !flag;
  }

  public static void CheckContractOnDepositItems(List<ContractDetail> list, Contract contract)
  {
    int num = list.Count<ContractDetail>((Func<ContractDetail, bool>) (det => det.Deposit.GetValueOrDefault()));
    if (num > 0 && contract.Type == "R")
      throw new PXSetPropertyException("Renewable contract can not contain deposit items.");
    if (num > 1)
      throw new PXSetPropertyException("A contract can not contain more than one deposit item.");
  }
}
