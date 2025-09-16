// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractItemMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CT;

public class ContractItemMaint : PXGraph<ContractItemMaint, ContractItem>
{
  public PXSelect<ContractItem> ContractItems;
  public PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Current<ContractItem.contractItemID>>>> CurrentContractItem;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ContractTemplate, InnerJoin<ContractDetail, On<ContractTemplate.contractID, Equal<ContractDetail.contractID>>>, Where<ContractDetail.contractItemID, Equal<Current<ContractItem.contractItemID>>, And<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>> CurrentTemplates;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<Contract, InnerJoin<ContractDetail, On<Contract.contractID, Equal<ContractDetail.contractID>>>, Where<ContractDetail.contractItemID, Equal<Current<ContractItem.contractItemID>>, And<Contract.baseType, Equal<CTPRType.contract>, And<Contract.status, NotEqual<Contract.status.canceled>, And<Contract.status, NotEqual<Contract.status.expired>, And<Contract.status, NotEqual<Contract.status.draft>>>>>>> CurrentContracts;
  public CMSetupSelect CMSetup;
  public PXSetup<PX.Objects.GL.Company> Company;

  public ContractItemMaint()
  {
    ((PXSelectBase) this.CurrentTemplates).Cache.AllowInsert = ((PXSelectBase) this.CurrentTemplates).Cache.AllowUpdate = ((PXSelectBase) this.CurrentTemplates).Cache.AllowDelete = false;
    ((PXSelectBase) this.CurrentContracts).Cache.AllowInsert = ((PXSelectBase) this.CurrentContracts).Cache.AllowUpdate = ((PXSelectBase) this.CurrentContracts).Cache.AllowDelete = false;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(ContractItemMaint.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (ContractItemMaint.\u003C\u003Ec.\u003C\u003E9__6_0 = new PXFieldDefaulting((object) ContractItemMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__6_0))));
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PXUIFieldAttribute.SetVisible<ContractItem.curyID>(((PXSelectBase) this.ContractItems).Cache, (object) null, false);
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<ContractItem.curyID>(new PXFieldDefaulting((object) this, __methodptr(\u003C\u002Ector\u003Eb__6_1)));
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>), "A kit cannot be used on the [ScreenName] ([ScreenID]) form.", new Type[] {})]
  protected virtual void ContractItem_BaseItemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>), "A kit cannot be used on the [ScreenName] ([ScreenID]) form.", new Type[] {})]
  protected virtual void ContractItem_RenewalItemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>), "A kit cannot be used on the [ScreenName] ([ScreenID]) form.", new Type[] {})]
  protected virtual void ContractItem_RecurringItemID_CacheAttached(PXCache sender)
  {
  }

  public virtual void ContractItem_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row == null)
      return;
    this.SetControlsState(cache, row);
    ContractDetail contractDetail = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractItemID, Equal<Current<ContractItem.contractItemID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    ((PXSelectBase) this.ContractItems).Cache.AllowDelete = contractDetail == null;
    PXUIFieldAttribute.SetEnabled<ContractItem.curyID>(cache, (object) row, contractDetail == null);
    PXDefaultAttribute.SetPersistingCheck<ContractItem.recurringItemID>(cache, (object) row, row.RecurringType == "N" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetRequired<ContractItem.recurringItemID>(cache, row.RecurringType != "N");
    string message = (string) null;
    if (row.BaseItemID.HasValue)
      PXUIFieldAttribute.SetWarning<ContractItem.baseItemID>(cache, (object) row, !ContractItemMaint.IsValidBasePrice((PXGraph) this, row, ref message) ? "The item has no price set up in the currency selected in the Currency ID box." : (string) null);
    int? nullable = row.RecurringItemID;
    if (nullable.HasValue)
      PXUIFieldAttribute.SetWarning<ContractItem.recurringItemID>(cache, (object) row, !ContractItemMaint.IsValidRecurringPrice((PXGraph) this, row, ref message) ? "The item has no price set up in the currency selected in the Currency ID box." : (string) null);
    nullable = row.RenewalItemID;
    if (!nullable.HasValue)
      return;
    PXUIFieldAttribute.SetWarning<ContractItem.renewalItemID>(cache, (object) row, !ContractItemMaint.IsValidRenewalPrice((PXGraph) this, row, ref message) ? "The item has no price set up in the currency selected in the Currency ID box." : (string) null);
  }

  public virtual void ContractItem_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row == null)
      return;
    string str = (string) null;
    int? nullable = row.BaseItemID;
    if (nullable.HasValue)
      str = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.BaseItemID
      })).BaseUnit;
    nullable = row.RecurringItemID;
    if (nullable.HasValue && row.RecurringType != "N")
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.RecurringItemID
      }));
      if (str == null)
        str = inventoryItem.BaseUnit;
      else if (str != inventoryItem.BaseUnit)
        cache.RaiseExceptionHandling<ContractItem.recurringItemID>((object) row, (object) inventoryItem.InventoryCD, (Exception) new PXSetPropertyException("All Non-Stock items used to define a Contract Item must share same UOM. The Base Unit of current item differs from others."));
    }
    nullable = row.RenewalItemID;
    if (!nullable.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.RenewalItemID
    }));
    if (str == null)
    {
      string baseUnit = inventoryItem1.BaseUnit;
    }
    else
    {
      if (!(str != inventoryItem1.BaseUnit))
        return;
      cache.RaiseExceptionHandling<ContractItem.renewalItemID>((object) row, (object) inventoryItem1.InventoryCD, (Exception) new PXSetPropertyException("All Non-Stock items used to define a Contract Item must share same UOM. The Base Unit of current item differs from others."));
    }
  }

  public virtual void ContractItem_BasePriceOption_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is ContractItem row && row.BasePriceOption == "P")
    {
      Decimal? basePrice = row.BasePrice;
      Decimal num = 0M;
      if (basePrice.GetValueOrDefault() == num & basePrice.HasValue)
        sender.SetValueExt<ContractItem.basePrice>(e.Row, (object) 100M);
    }
    if (row == null || !(row.BasePriceOption == "I"))
      return;
    sender.SetDefaultExt<ContractItem.basePrice>((object) row);
  }

  public virtual void ContractItem_RenewalPriceOption_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is ContractItem row && row.RenewalPriceOption == "P")
    {
      Decimal? renewalPrice = row.RenewalPrice;
      Decimal num = 0M;
      if (renewalPrice.GetValueOrDefault() == num & renewalPrice.HasValue)
        sender.SetValueExt<ContractItem.renewalPrice>(e.Row, (object) 100M);
    }
    if (row == null || !(row.RenewalPriceOption == "I"))
      return;
    sender.SetDefaultExt<ContractItem.renewalPrice>((object) row);
  }

  public virtual void ContractItem_FixedRecurringPriceOption_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is ContractItem row && row.FixedRecurringPriceOption == "P")
    {
      Decimal? fixedRecurringPrice = row.FixedRecurringPrice;
      Decimal num = 0M;
      if (fixedRecurringPrice.GetValueOrDefault() == num & fixedRecurringPrice.HasValue)
        sender.SetValueExt<ContractItem.fixedRecurringPrice>(e.Row, (object) 100M);
    }
    if (row == null || !(row.FixedRecurringPriceOption == "I"))
      return;
    sender.SetDefaultExt<ContractItem.fixedRecurringPrice>((object) row);
  }

  public virtual void ContractItem_RecurringType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ContractItem row) || !(row.RecurringType == "N"))
      return;
    row.ResetUsageOnBilling = new bool?(false);
    sender.SetDefaultExt<ContractItem.recurringItemID>((object) row);
    sender.SetDefaultExt<ContractItem.fixedRecurringPriceOption>((object) row);
    sender.SetDefaultExt<ContractItem.fixedRecurringPrice>((object) row);
    sender.SetDefaultExt<ContractItem.usagePriceOption>((object) row);
    sender.SetDefaultExt<ContractItem.usagePrice>((object) row);
    sender.SetDefaultExt<ContractItem.depositItemID>((object) row);
  }

  public virtual void ContractItem_UsagePriceOption_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is ContractItem row && row.UsagePriceOption == "P")
    {
      Decimal? usagePrice = row.UsagePrice;
      Decimal num = 0M;
      if (usagePrice.GetValueOrDefault() == num & usagePrice.HasValue)
        sender.SetValueExt<ContractItem.usagePrice>(e.Row, (object) 100M);
    }
    if (row == null || !(row.UsagePriceOption == "I"))
      return;
    sender.SetDefaultExt<ContractItem.usagePrice>((object) row);
  }

  public virtual void ContractItem_Deposit_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ContractItem row) || !row.Deposit.GetValueOrDefault())
      return;
    sender.SetDefaultExt<ContractItem.recurringType>((object) row);
    sender.SetDefaultExt<ContractItem.recurringItemID>((object) row);
    sender.SetDefaultExt<ContractItem.resetUsageOnBilling>((object) row);
    sender.SetDefaultExt<ContractItem.fixedRecurringPriceOption>((object) row);
    sender.SetDefaultExt<ContractItem.fixedRecurringPrice>((object) row);
    sender.SetDefaultExt<ContractItem.usagePriceOption>((object) row);
    sender.SetDefaultExt<ContractItem.usagePrice>((object) row);
    sender.SetDefaultExt<ContractItem.depositItemID>((object) row);
    sender.SetDefaultExt<ContractItem.collectRenewFeeOnActivation>((object) row);
    sender.SetDefaultExt<ContractItem.renewalPriceOption>((object) row);
    sender.SetDefaultExt<ContractItem.renewalPrice>((object) row);
    sender.SetDefaultExt<ContractItem.renewalItemID>((object) row);
    sender.SetDefaultExt<ContractItem.recurringItemID>((object) row);
  }

  public virtual void ContractItem_DepositItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ContractItem row) || !row.DepositItemID.HasValue)
      return;
    sender.SetDefaultExt<ContractItem.resetUsageOnBilling>((object) row);
    sender.SetDefaultExt<ContractItem.maxQty>((object) row);
    sender.SetDefaultExt<ContractItem.minQty>((object) row);
    sender.SetDefaultExt<ContractItem.defaultQty>((object) row);
  }

  public virtual void ContractItem_BaseItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row == null)
      return;
    if (!row.BaseItemID.HasValue)
    {
      sender.SetDefaultExt<ContractItem.basePriceOption>((object) row);
      sender.SetDefaultExt<ContractItem.basePrice>((object) row);
      sender.SetDefaultExt<ContractItem.deposit>((object) row);
      row.ProrateSetup = new bool?(false);
    }
    this.SetValueRefundable(sender, row);
  }

  public virtual void ContractItem_RenewalItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row == null)
      return;
    if (!row.RenewalItemID.HasValue)
    {
      sender.SetDefaultExt<ContractItem.renewalPriceOption>((object) row);
      sender.SetDefaultExt<ContractItem.renewalPrice>((object) row);
      row.CollectRenewFeeOnActivation = new bool?(false);
    }
    this.SetValueRefundable(sender, row);
  }

  public virtual void ContractItem_RecurringItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row == null)
      return;
    if (!row.RecurringItemID.HasValue)
    {
      sender.SetDefaultExt<ContractItem.fixedRecurringPriceOption>((object) row);
      sender.SetDefaultExt<ContractItem.fixedRecurringPrice>((object) row);
      sender.SetDefaultExt<ContractItem.usagePriceOption>((object) row);
      sender.SetDefaultExt<ContractItem.usagePrice>((object) row);
    }
    this.SetValueRefundable(sender, row);
  }

  private void SetValueRefundable(PXCache sender, ContractItem item)
  {
    if (item.BaseItemID.HasValue || item.RenewalItemID.HasValue || item.RecurringItemID.HasValue)
      return;
    item.Refundable = new bool?(false);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName == "CurrentContractItem" && values != null)
    {
      values[(object) "BasePriceVal"] = PXCache.NotSetValue;
      values[(object) "RenewalPriceVal"] = PXCache.NotSetValue;
      values[(object) "FixedRecurringPriceVal"] = PXCache.NotSetValue;
      values[(object) "UsagePriceVal"] = PXCache.NotSetValue;
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  private void SetControlsState(PXCache cache, ContractItem row)
  {
    bool flag = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelectJoin<ContractDetail, InnerJoin<Contract, On<ContractDetail.contractID, Equal<Contract.contractID>>>, Where<ContractDetail.contractItemID, Equal<Required<ContractItem.contractItemID>>, And2<Where<Contract.status, Equal<Contract.status.active>, Or<Contract.status, Equal<Contract.status.inUpgrade>>>, And<Contract.baseType, Equal<CTPRType.contract>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.ContractItemID
    })) != null;
    PXUIFieldAttribute.SetEnabled<ContractItem.recurringType>(cache, (object) row, !flag);
    PXCache pxCache1 = cache;
    ContractItem contractItem1 = row;
    bool? deposit;
    int num1;
    if (row.RecurringType != "N" && !flag)
    {
      deposit = row.Deposit;
      num1 = !deposit.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.recurringItemID>(pxCache1, (object) contractItem1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<ContractItem.basePriceOption>(cache, (object) row, row.BaseItemID.HasValue);
    PXUIFieldAttribute.SetEnabled<ContractItem.basePrice>(cache, (object) row, row.BaseItemID.HasValue && row.BasePriceOption != "I");
    PXUIFieldAttribute.SetEnabled<ContractItem.renewalPriceOption>(cache, (object) row, row.RenewalItemID.HasValue);
    PXUIFieldAttribute.SetEnabled<ContractItem.renewalPrice>(cache, (object) row, row.RenewalItemID.HasValue && row.RenewalPriceOption != "I");
    PXCache pxCache2 = cache;
    ContractItem contractItem2 = row;
    int? nullable;
    int num2;
    if (row.RecurringType != "N")
    {
      nullable = row.RecurringItemID;
      if (nullable.HasValue)
      {
        num2 = !flag ? 1 : 0;
        goto label_7;
      }
    }
    num2 = 0;
label_7:
    PXUIFieldAttribute.SetEnabled<ContractItem.fixedRecurringPriceOption>(pxCache2, (object) contractItem2, num2 != 0);
    PXCache pxCache3 = cache;
    ContractItem contractItem3 = row;
    int num3;
    if (row.RecurringType != "N")
    {
      nullable = row.RecurringItemID;
      if (nullable.HasValue && row.FixedRecurringPriceOption != "I")
      {
        num3 = !flag ? 1 : 0;
        goto label_11;
      }
    }
    num3 = 0;
label_11:
    PXUIFieldAttribute.SetEnabled<ContractItem.fixedRecurringPrice>(pxCache3, (object) contractItem3, num3 != 0);
    PXCache pxCache4 = cache;
    ContractItem contractItem4 = row;
    int num4;
    if (row.RecurringType != "N")
    {
      nullable = row.RecurringItemID;
      num4 = nullable.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.usagePriceOption>(pxCache4, (object) contractItem4, num4 != 0);
    PXCache pxCache5 = cache;
    ContractItem contractItem5 = row;
    int num5;
    if (row.RecurringType != "N")
    {
      nullable = row.RecurringItemID;
      if (nullable.HasValue)
      {
        num5 = row.UsagePriceOption != "I" ? 1 : 0;
        goto label_18;
      }
    }
    num5 = 0;
label_18:
    PXUIFieldAttribute.SetEnabled<ContractItem.usagePrice>(pxCache5, (object) contractItem5, num5 != 0);
    PXUIFieldAttribute.SetEnabled<ContractItem.depositItemID>(cache, (object) row, row.RecurringType != "N");
    PXCache pxCache6 = cache;
    ContractItem contractItem6 = row;
    deposit = row.Deposit;
    int num6 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.collectRenewFeeOnActivation>(pxCache6, (object) contractItem6, num6 != 0);
    PXCache pxCache7 = cache;
    ContractItem contractItem7 = row;
    deposit = row.Deposit;
    int num7 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalPriceOption>(pxCache7, (object) contractItem7, num7 != 0);
    PXCache pxCache8 = cache;
    ContractItem contractItem8 = row;
    deposit = row.Deposit;
    int num8 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalPrice>(pxCache8, (object) contractItem8, num8 != 0);
    PXCache pxCache9 = cache;
    ContractItem contractItem9 = row;
    deposit = row.Deposit;
    int num9 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.renewalItemID>(pxCache9, (object) contractItem9, num9 != 0);
    PXCache pxCache10 = cache;
    ContractItem contractItem10 = row;
    deposit = row.Deposit;
    int num10 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.recurringItemID>(pxCache10, (object) contractItem10, num10 != 0);
    PXCache pxCache11 = cache;
    ContractItem contractItem11 = row;
    deposit = row.Deposit;
    int num11 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.recurringType>(pxCache11, (object) contractItem11, num11 != 0);
    PXCache pxCache12 = cache;
    ContractItem contractItem12 = row;
    deposit = row.Deposit;
    int num12 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.resetUsageOnBilling>(pxCache12, (object) contractItem12, num12 != 0);
    PXCache pxCache13 = cache;
    ContractItem contractItem13 = row;
    deposit = row.Deposit;
    int num13 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.fixedRecurringPriceOption>(pxCache13, (object) contractItem13, num13 != 0);
    PXCache pxCache14 = cache;
    ContractItem contractItem14 = row;
    deposit = row.Deposit;
    int num14 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.fixedRecurringPrice>(pxCache14, (object) contractItem14, num14 != 0);
    PXCache pxCache15 = cache;
    ContractItem contractItem15 = row;
    deposit = row.Deposit;
    int num15 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.usagePriceOption>(pxCache15, (object) contractItem15, num15 != 0);
    PXCache pxCache16 = cache;
    ContractItem contractItem16 = row;
    deposit = row.Deposit;
    int num16 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.usagePrice>(pxCache16, (object) contractItem16, num16 != 0);
    PXCache pxCache17 = cache;
    ContractItem contractItem17 = row;
    deposit = row.Deposit;
    int num17 = !deposit.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ContractItem.depositItemID>(pxCache17, (object) contractItem17, num17 != 0);
    PXCache pxCache18 = cache;
    ContractItem contractItem18 = row;
    nullable = row.BaseItemID;
    int num18;
    if (nullable.HasValue)
    {
      if (flag)
      {
        nullable = row.RecurringItemID;
        num18 = !nullable.HasValue ? 1 : 0;
      }
      else
        num18 = 1;
    }
    else
      num18 = 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.deposit>(pxCache18, (object) contractItem18, num18 != 0);
    PXCache pxCache19 = cache;
    ContractItem contractItem19 = row;
    nullable = row.BaseItemID;
    int num19 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.prorateSetup>(pxCache19, (object) contractItem19, num19 != 0);
    PXCache pxCache20 = cache;
    ContractItem contractItem20 = row;
    nullable = row.RenewalItemID;
    int num20 = nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.collectRenewFeeOnActivation>(pxCache20, (object) contractItem20, num20 != 0);
    PXCache pxCache21 = cache;
    ContractItem contractItem21 = row;
    int num21;
    if (row.RecurringType != "N")
    {
      nullable = row.DepositItemID;
      num21 = !nullable.HasValue ? 1 : 0;
    }
    else
      num21 = 0;
    PXUIFieldAttribute.SetEnabled<ContractItem.resetUsageOnBilling>(pxCache21, (object) contractItem21, num21 != 0);
  }

  protected virtual void ValidateQuantity(ContractItem row, Decimal? defaultQty)
  {
    Decimal valueOrDefault1 = defaultQty.GetValueOrDefault();
    Decimal valueOrDefault2 = row.MaxQty.GetValueOrDefault();
    Decimal valueOrDefault3 = row.MinQty.GetValueOrDefault();
    if (valueOrDefault2 < valueOrDefault3 || valueOrDefault1 < valueOrDefault3 || valueOrDefault1 > valueOrDefault2)
      throw new PXSetPropertyException("Included Quantity must be within the Min and Max limits.");
  }

  public virtual void ContractItem_DefaultQty_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || ((PXGraph) this).IsImport)
      return;
    ContractItem row = (ContractItem) e.Row;
    this.ValidateQuantity(row, (Decimal?) e.NewValue);
    if (row.DepositItemID.HasValue && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("Under Deposit contract item do not use {0}.", new object[1]
      {
        (object) "[defaultQty]"
      });
  }

  public virtual void ContractItem_MaxQty_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row != null && !((PXGraph) this).IsImport && row.DepositItemID.HasValue && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("Under Deposit contract item do not use {0}.", new object[1]
      {
        (object) "[maxQty]"
      });
  }

  public virtual void ContractItem_MinQty_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    ContractItem row = (ContractItem) e.Row;
    if (row != null && !((PXGraph) this).IsImport && row.DepositItemID.HasValue && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("Under Deposit contract item do not use {0}.", new object[1]
      {
        (object) "[minQty]"
      });
  }

  public static bool IsValidItemPrice(PXGraph graph, ContractItem item, out string message)
  {
    message = (string) null;
    return ContractItemMaint.IsValidBasePrice(graph, item, ref message) && ContractItemMaint.IsValidRecurringPrice(graph, item, ref message) && ContractItemMaint.IsValidRenewalPrice(graph, item, ref message);
  }

  public static bool IsValidItemPrice(PXGraph graph, ContractItem item)
  {
    string message = (string) null;
    return ContractItemMaint.IsValidBasePrice(graph, item, ref message) && ContractItemMaint.IsValidRecurringPrice(graph, item, ref message) && ContractItemMaint.IsValidRenewalPrice(graph, item, ref message);
  }

  private static bool IsValidBasePrice(PXGraph graph, ContractItem item, ref string message)
  {
    if (item == null || !item.BaseItemID.HasValue || !(item.BasePriceVal.GetValueOrDefault() == 0M))
      return true;
    message = "Setup Price";
    return false;
  }

  private static bool IsValidRecurringPrice(PXGraph graph, ContractItem item, ref string message)
  {
    if (item == null || !item.RecurringItemID.HasValue || !(item.FixedRecurringPriceVal.GetValueOrDefault() == 0M) && (!(item.UsagePriceOption != "M") || !(item.UsagePriceVal.GetValueOrDefault() == 0M)))
      return true;
    message = "Recurring Price";
    return false;
  }

  private static bool IsValidRenewalPrice(PXGraph graph, ContractItem item, ref string message)
  {
    if (item == null || !item.RenewalItemID.HasValue || !(item.RenewalPriceVal.GetValueOrDefault() == 0M))
      return true;
    message = "Renewal Price";
    return false;
  }
}
