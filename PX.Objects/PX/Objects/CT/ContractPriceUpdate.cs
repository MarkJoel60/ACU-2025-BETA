// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractPriceUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CT;

public class ContractPriceUpdate : PXGraph<
#nullable disable
ContractPriceUpdate>
{
  public PXCancel<ContractPriceUpdate.ContractFilter> Cancel;
  public PXAction<ContractPriceUpdate.ContractFilter> ViewContract;
  public PXFilter<ContractPriceUpdate.ContractFilter> Filter;
  public PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Current<ContractPriceUpdate.ContractFilter.contractItemID>>>> SelectedContractItem;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<ContractDetail, ContractPriceUpdate.ContractFilter, InnerJoin<Contract, On<Contract.contractID, Equal<ContractDetail.contractID>>>> Items;
  public PXSelectJoin<ContractDetail, InnerJoin<Contract, On<Contract.contractID, Equal<ContractDetail.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<ContractDetail.contractItemID, Equal<Current<ContractPriceUpdate.ContractFilter.contractItemID>>>> ItemsView;
  public PXSetup<PX.Objects.GL.Company> Company;

  public ContractPriceUpdate()
  {
    ((PXProcessingBase<ContractDetail>) this.Items).SetSelected<ContractDetail.selected>();
    // ISSUE: method pointer
    ((PXProcessingBase<ContractDetail>) this.Items).SetProcessDelegate<ContractMaint>(new PXProcessingBase<ContractDetail>.ProcessItemDelegate<ContractMaint>((object) null, __methodptr(UpdatePrices)));
    ((PXProcessing<ContractDetail>) this.Items).SetProcessCaption("Update");
    ((PXProcessing<ContractDetail>) this.Items).SetProcessAllCaption("Update All");
    PXUIFieldAttribute.SetDisplayName<Contract.contractCD>(((PXGraph) this).Caches[typeof (Contract)], "Identifier");
  }

  protected virtual IEnumerable items()
  {
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(((PXSelectBase<ContractItem>) this.SelectedContractItem).Select(Array.Empty<object>()));
    List<PXResult<ContractDetail, Contract, PX.Objects.AR.Customer>> pxResultList = new List<PXResult<ContractDetail, Contract, PX.Objects.AR.Customer>>();
    foreach (PXResult<ContractDetail, Contract, PX.Objects.AR.Customer> pxResult in ((PXSelectBase<ContractDetail>) this.ItemsView).Select(Array.Empty<object>()))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, Contract, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      bool flag = false;
      ContractDetail copy = PXCache<ContractDetail>.CreateCopy(contractDetail);
      copy.ContractDetailID = new int?(-1);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.basePriceOption>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.basePrice>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.renewalPriceOption>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.renewalPrice>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.fixedRecurringPriceOption>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.fixedRecurringPrice>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.usagePriceOption>((object) copy);
      ((PXSelectBase) this.Items).Cache.SetDefaultExt<ContractDetail.usagePrice>((object) copy);
      Decimal? nullable1;
      Decimal? nullable2;
      if (contractItem.BaseItemID.HasValue)
      {
        if (!(contractItem.BasePriceOption != contractDetail.BasePriceOption))
        {
          nullable1 = contractItem.BasePrice;
          nullable2 = contractDetail.BasePrice;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = copy.BasePriceVal;
            nullable1 = contractDetail.BasePriceVal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              goto label_7;
          }
        }
        flag = true;
      }
label_7:
      if (contractItem.RenewalItemID.HasValue)
      {
        if (!(contractItem.RenewalPriceOption != contractDetail.RenewalPriceOption))
        {
          nullable1 = contractItem.RenewalPrice;
          nullable2 = contractDetail.RenewalPrice;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = copy.RenewalPriceVal;
            nullable1 = contractDetail.RenewalPriceVal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              goto label_12;
          }
        }
        flag = true;
      }
label_12:
      if (contractItem.RecurringItemID.HasValue)
      {
        if (!(contractItem.FixedRecurringPriceOption != contractDetail.FixedRecurringPriceOption))
        {
          nullable1 = contractItem.FixedRecurringPrice;
          nullable2 = contractDetail.FixedRecurringPrice;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = copy.FixedRecurringPriceVal;
            nullable1 = contractDetail.FixedRecurringPriceVal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              goto label_17;
          }
        }
        flag = true;
label_17:
        if (!(contractItem.UsagePriceOption != contractDetail.UsagePriceOption))
        {
          nullable1 = contractItem.UsagePrice;
          nullable2 = contractDetail.UsagePrice;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = copy.UsagePriceVal;
            nullable1 = contractDetail.UsagePriceVal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              goto label_21;
          }
        }
        flag = true;
      }
label_21:
      if (flag)
        pxResultList.Add(pxResult);
    }
    return (IEnumerable) pxResultList;
  }

  protected virtual void ContractFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void ContractItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ContractItem row))
      return;
    PXCache pxCache1 = sender;
    ContractItem contractItem1 = row;
    int? nullable = row.BaseItemID;
    int num1 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.basePriceOption>(pxCache1, (object) contractItem1, num1 != 0);
    PXCache pxCache2 = sender;
    ContractItem contractItem2 = row;
    nullable = row.BaseItemID;
    int num2 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.basePrice>(pxCache2, (object) contractItem2, num2 != 0);
    PXCache pxCache3 = sender;
    ContractItem contractItem3 = row;
    nullable = row.BaseItemID;
    int num3 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.basePriceVal>(pxCache3, (object) contractItem3, num3 != 0);
    PXCache cache1 = ((PXSelectBase) this.Items).Cache;
    nullable = row.BaseItemID;
    int num4 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.basePriceOption>(cache1, (object) null, num4 != 0);
    PXCache cache2 = ((PXSelectBase) this.Items).Cache;
    nullable = row.BaseItemID;
    int num5 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.basePrice>(cache2, (object) null, num5 != 0);
    PXCache cache3 = ((PXSelectBase) this.Items).Cache;
    nullable = row.BaseItemID;
    int num6 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.basePriceVal>(cache3, (object) null, num6 != 0);
    PXCache pxCache4 = sender;
    ContractItem contractItem4 = row;
    nullable = row.RenewalItemID;
    int num7 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalPriceOption>(pxCache4, (object) contractItem4, num7 != 0);
    PXCache pxCache5 = sender;
    ContractItem contractItem5 = row;
    nullable = row.RenewalItemID;
    int num8 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalPrice>(pxCache5, (object) contractItem5, num8 != 0);
    PXCache pxCache6 = sender;
    ContractItem contractItem6 = row;
    nullable = row.RenewalItemID;
    int num9 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalPriceVal>(pxCache6, (object) contractItem6, num9 != 0);
    PXCache cache4 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RenewalItemID;
    int num10 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.renewalPriceOption>(cache4, (object) null, num10 != 0);
    PXCache cache5 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RenewalItemID;
    int num11 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.renewalPrice>(cache5, (object) null, num11 != 0);
    PXCache cache6 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RenewalItemID;
    int num12 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.renewalPriceVal>(cache6, (object) null, num12 != 0);
    PXCache pxCache7 = sender;
    ContractItem contractItem7 = row;
    nullable = row.RecurringItemID;
    int num13 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.fixedRecurringPriceOption>(pxCache7, (object) contractItem7, num13 != 0);
    PXCache pxCache8 = sender;
    ContractItem contractItem8 = row;
    nullable = row.RecurringItemID;
    int num14 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.fixedRecurringPrice>(pxCache8, (object) contractItem8, num14 != 0);
    PXCache pxCache9 = sender;
    ContractItem contractItem9 = row;
    nullable = row.RecurringItemID;
    int num15 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.fixedRecurringPriceVal>(pxCache9, (object) contractItem9, num15 != 0);
    PXCache cache7 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num16 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.fixedRecurringPriceOption>(cache7, (object) null, num16 != 0);
    PXCache cache8 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num17 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.fixedRecurringPrice>(cache8, (object) null, num17 != 0);
    PXCache cache9 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num18 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.fixedRecurringPriceVal>(cache9, (object) null, num18 != 0);
    PXCache pxCache10 = sender;
    ContractItem contractItem10 = row;
    nullable = row.RecurringItemID;
    int num19 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.usagePriceOption>(pxCache10, (object) contractItem10, num19 != 0);
    PXCache pxCache11 = sender;
    ContractItem contractItem11 = row;
    nullable = row.RecurringItemID;
    int num20 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.usagePrice>(pxCache11, (object) contractItem11, num20 != 0);
    PXCache pxCache12 = sender;
    ContractItem contractItem12 = row;
    nullable = row.RecurringItemID;
    int num21 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.usagePriceVal>(pxCache12, (object) contractItem12, num21 != 0);
    PXCache cache10 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num22 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.usagePriceOption>(cache10, (object) null, num22 != 0);
    PXCache cache11 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num23 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.usagePrice>(cache11, (object) null, num23 != 0);
    PXCache cache12 = ((PXSelectBase) this.Items).Cache;
    nullable = row.RecurringItemID;
    int num24 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractDetail.usagePriceVal>(cache12, (object) null, num24 != 0);
  }

  protected static void UpdatePrices(ContractMaint graph, ContractDetail item)
  {
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) item.ContractID
    }));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (!CTPRType.IsTemplate(contract.BaseType))
      {
        ContractPriceUpdate.ContractMaintExt instance1 = PXGraph.CreateInstance<ContractPriceUpdate.ContractMaintExt>();
        ((PXSelectBase<Contract>) instance1.Contracts).Current = contract;
        if (contract.IsActive.GetValueOrDefault() && !contract.IsPendingUpdate.GetValueOrDefault() && contract.Status != "P")
        {
          CTBillEngine instance2 = PXGraph.CreateInstance<CTBillEngine>();
          instance2.Upgrade(contract.ContractID);
          ((PXGraph) instance2).Clear();
          ((PXSelectBase<Contract>) instance1.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) instance2, new object[1]
          {
            (object) item.ContractID
          }));
          item = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Required<ContractDetail.contractID>>, And<ContractDetail.lineNbr, Equal<Required<ContractDetail.lineNbr>>>>>.Config>.Select((PXGraph) instance2, new object[2]
          {
            (object) item.ContractID,
            (object) item.LineNbr
          }));
        }
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePriceOption>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePrice>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPriceOption>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPrice>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPriceOption>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPrice>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePriceOption>((object) item);
        ((PXSelectBase) instance1.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePrice>((object) item);
        ((PXSelectBase<ContractDetail>) instance1.ContractDetails).Update(item);
        ((PXGraph) instance1).Actions.PressSave();
      }
      else
      {
        TemplateMaint instance = PXGraph.CreateInstance<TemplateMaint>();
        ((PXSelectBase<ContractTemplate>) instance.Templates).Current = PXResultset<ContractTemplate>.op_Implicit(PXSelectBase<ContractTemplate, PXSelect<ContractTemplate, Where<ContractTemplate.contractID, Equal<Required<ContractTemplate.contractID>>>>.Config>.Select((PXGraph) graph, new object[1]
        {
          (object) item.ContractID
        }));
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePriceOption>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePrice>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPriceOption>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPrice>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPriceOption>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPrice>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePriceOption>((object) item);
        ((PXSelectBase) instance.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePrice>((object) item);
        ((PXSelectBase<ContractDetail>) instance.ContractDetails).Update(item);
        ((PXGraph) instance).Actions.PressSave();
      }
      transactionScope.Complete();
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewContract(PXAdapter adapter)
  {
    ContractDetail current = ((PXSelectBase<ContractDetail>) this.Items).Current;
    if (current != null)
    {
      Contract contract = ContractMaint.FindContract((PXGraph) this, current.ContractID);
      PXGraph pxGraph;
      string str;
      if (CTPRType.IsTemplate(contract.BaseType))
      {
        TemplateMaint instance = PXGraph.CreateInstance<TemplateMaint>();
        ((PXSelectBase<ContractTemplate>) instance.Templates).Current = PXResultset<ContractTemplate>.op_Implicit(((PXSelectBase<ContractTemplate>) instance.Templates).Search<ContractTemplate.contractID>((object) current.ContractID, Array.Empty<object>()));
        pxGraph = (PXGraph) instance;
        str = "View Contract Template";
      }
      else
      {
        ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
        ((PXSelectBase<Contract>) instance.Contracts).Current = contract;
        pxGraph = (PXGraph) instance;
        str = "View Contract";
      }
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, str);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<ContractPriceUpdate.ContractFilter>) this.Filter).Select(Array.Empty<object>());
  }

  protected class ContractMaintExt : ContractMaint
  {
    [PXDBInt(IsKey = true)]
    protected virtual void ContractDetail_ContractDetailID_CacheAttached()
    {
    }
  }

  [Serializable]
  public class ContractFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    [PXDefault]
    [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID>), typeof (ContractItem.contractItemCD), new Type[] {typeof (ContractItem.contractItemCD), typeof (ContractItem.descr)})]
    [PXUIField(DisplayName = "Item Code")]
    public virtual int? ContractItemID { get; set; }

    public abstract class contractItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ContractPriceUpdate.ContractFilter.contractItemID>
    {
    }
  }
}
