// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.NonStockItemMaintExt.ConvertNonStockToStockExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.IN.GraphExtensions.InventoryItemMaintExt;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.NonStockItemMaintExt;

public class ConvertNonStockToStockExt : 
  ConvertStockToNonStockExtBase<
  #nullable disable
  NonStockItemMaint, InventoryItemMaint>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemSite.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>, INItemSite>.View AllItemSiteRecords;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemRep.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>, INItemRep>.View AllReplenishmentRecords;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemBoxEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemBoxEx.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>, INItemBoxEx>.View Boxes;

  public static bool IsActive() => ConvertStockToNonStockExt.IsActive();

  public override void Initialize()
  {
    base.Initialize();
    ((PXAction) this.convert).SetCaption("Convert to Stock Item");
  }

  protected override int Verify(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    int num = base.Verify(item, errors) + this.VerifyKitSpec(item, errors) + this.VerifyCashEntry(item, errors) + this.VerifyARSetup(item, errors);
    if (PXAccess.FeatureInstalled<FeaturesSet.contractManagement>())
      num = num + this.VerifyPMTran(item, errors) + this.VerifyContractTemplate(item, errors) + this.VerifyContractItem(item, errors);
    return num;
  }

  protected override int VerifyInventoryItem(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    if (item.ItemType != "N" || !item.NonStockReceipt.GetValueOrDefault() || !item.NonStockShip.GetValueOrDefault())
      throw new PXException("The {0} item cannot be converted. To convert a non-stock item to a stock item, the non-stock item must have the Non-Stock Item type and the Require Receipt and Require Shipment check boxes selected.", new object[1]
      {
        (object) item.InventoryCD.Trim()
      });
    return base.VerifyInventoryItem(item, errors);
  }

  protected virtual int VerifyKitSpec(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    INKitSpecHdr[] array = GraphHelper.RowCast<INKitSpecHdr>((IEnumerable) PXSelectBase<INKitSpecHdr, PXViewOf<INKitSpecHdr>.BasedOn<SelectFromBase<INKitSpecHdr, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<Select<INKitSpecNonStkDet, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.compInventoryID, Equal<P.AsInt>>>>>.And<INKitSpecNonStkDet.FK.KitSpecification>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INKitSpecHdr>();
    if (((IEnumerable<INKitSpecHdr>) array).Any<INKitSpecHdr>())
    {
      IEnumerable<string> values = ((IEnumerable<INKitSpecHdr>) array).Select<INKitSpecHdr, string>((Func<INKitSpecHdr, string>) (s => PXLocalizer.LocalizeFormat("the {0} kit, the {1} specification revision", new object[2]
      {
        (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, s.KitInventoryID)?.InventoryCD.Trim(),
        (object) s.RevisionID
      })));
      string str1 = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included as a component in the following revisions of kit specifications: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join("; ", values)
      });
      PXTrace.WriteError(str1);
      List<string> stringList = errors;
      string str2;
      if (array.Length != 1)
        str2 = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included as a component in at least one revision of kit specifications. For details, see the trace log: Click Tools > Trace on the form title bar.", new object[1]
        {
          (object) item.InventoryCD.Trim()
        });
      else
        str2 = str1;
      stringList.Add(str2);
    }
    return array.Length;
  }

  protected virtual int VerifyCashEntry(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    CAAdj[] array = GraphHelper.RowCast<CAAdj>((IEnumerable) PXSelectBase<CAAdj, PXViewOf<CAAdj>.BasedOn<SelectFromBase<CAAdj, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.released, NotEqual<True>>>>>.And<Exists<Select<CASplit, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CASplit.inventoryID, Equal<P.AsInt>>>>>.And<CASplit.FK.CashTransaction>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<CAAdj>();
    if (((IEnumerable<CAAdj>) array).Any<CAAdj>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included in the following cash entry documents that are not released: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<CAAdj>) array).Select<CAAdj, string>((Func<CAAdj, string>) (c => c.RefNbr)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected override List<string> GetAPTranTypes()
  {
    List<string> apTranTypes = base.GetAPTranTypes();
    apTranTypes.Add("ACR");
    apTranTypes.Add("QCK");
    apTranTypes.Add("VQC");
    return apTranTypes;
  }

  protected override string GetAPTranMessage(string apDocType)
  {
    switch (apDocType)
    {
      case "ACR":
        return "The {0} item cannot be converted because it is included in the following credit adjustments that are not released: {1}";
      case "QCK":
      case "VQC":
        return "The {0} item cannot be converted because it is included in the following quick checks that are not released: {1}";
      default:
        return base.GetAPTranMessage(apDocType);
    }
  }

  protected virtual int VerifyARSetup(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    if (PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXViewOf<ARSetup>.BasedOn<SelectFromBase<ARSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ARSetup.dunningFeeInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) item.InventoryID
    })) == null)
      return 0;
    string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is specified in the Dunning Fee Item box on the Dunning tab of the Accounts Receivable Preferences (AR101000) form.", new object[1]
    {
      (object) item.InventoryCD.Trim()
    });
    PXTrace.WriteError(str);
    errors.Add(str);
    return 1;
  }

  protected virtual int VerifyPMTran(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    Contract[] array = GraphHelper.RowCast<Contract>((IEnumerable) PXSelectBase<Contract, PXViewOf<Contract>.BasedOn<SelectFromBase<Contract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contract.baseType, Equal<CTPRType.contract>>>>, And<BqlOperand<Contract.status, IBqlString>.IsNotIn<Contract.status.draft, Contract.status.inApproval>>>>.And<Exists<Select<PMTran, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<PMTran.billed, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<Contract.contractID>>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<Contract>();
    if (((IEnumerable<Contract>) array).Any<Contract>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is specified in the Inventory ID box on the Unbilled tab of the Contract Usage (CT303000) form for the following contracts: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(",", ((IEnumerable<Contract>) array).Select<Contract, string>((Func<Contract, string>) (d => d.ContractCD.Trim())))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyContractTemplate(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    ContractTemplate[] array = GraphHelper.RowCast<ContractTemplate>((IEnumerable) PXSelectBase<ContractTemplate, PXViewOf<ContractTemplate>.BasedOn<SelectFromBase<ContractTemplate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ContractTemplate.caseItemID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<ContractTemplate>();
    if (((IEnumerable<ContractTemplate>) array).Any<ContractTemplate>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is specified in the Case Count Item box on the Summary tab of the Contract Templates (CT202000) form for the following active templates: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<ContractTemplate>) array).Select<ContractTemplate, string>((Func<ContractTemplate, string>) (d => d.ContractCD)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyContractItem(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    ContractItem[] array = GraphHelper.RowCast<ContractItem>((IEnumerable) PXSelectBase<ContractItem, PXViewOf<ContractItem>.BasedOn<SelectFromBase<ContractItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ContractItem.baseItemID, Equal<P.AsInt>>>>, Or<BqlOperand<ContractItem.renewalItemID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<ContractItem.recurringItemID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[3]
    {
      (object) item.InventoryID,
      (object) item.InventoryID,
      (object) item.InventoryID
    })).ToArray<ContractItem>();
    if (((IEnumerable<ContractItem>) array).Any<ContractItem>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because the {0} item is specified in the Recurring Item box, Setup Item box, or Renewal Item box on the Price Options tab of the Contract Items (CT201000) form for the following contract items: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<ContractItem>) array).Select<ContractItem, string>((Func<ContractItem, string>) (d => d.ContractItemCD)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual void VerifySingleLocationStatus(PX.Objects.IN.InventoryItem item)
  {
    INLocationStatusByCostCenter statusByCostCenter = PXResultset<INLocationStatusByCostCenter>.op_Implicit(PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>, And<BqlOperand<INLocationStatusByCostCenter.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocationStatusByCostCenter.siteID, IBqlInt>.IsNotEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) item.InventoryID,
      (object) ((PXSelectBase<INSetup>) this.Base.insetup).Current.TransitSiteID
    }));
    if (statusByCostCenter != null)
    {
      INSite inSite = INSite.PK.Find((PXGraph) this.Base, statusByCostCenter.SiteID);
      throw new PXException("The {0} item cannot be converted because its quantity on hand is less or more than zero in the following warehouses: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) inSite?.SiteCD.Trim()
      });
    }
  }

  protected override void OnBeforeCommitVerifyNoTransactionsCreated(PX.Objects.IN.InventoryItem item)
  {
    base.OnBeforeCommitVerifyNoTransactionsCreated(item);
    this.VerifySingleLocationStatus(item);
  }

  protected override PX.Objects.IN.InventoryItem ConvertMainFields(
    InventoryItemMaint graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem = base.ConvertMainFields(graph, source, newItem);
    InventoryItemCurySettings itemCurySettings = PXResultset<InventoryItemCurySettings>.op_Implicit(((PXSelectBase<InventoryItemCurySettings>) this.Base.ItemCurySettings).Select(new object[1]
    {
      (object) source.InventoryID
    }));
    int num = itemCurySettings != null ? (EnumerableExtensions.IsNotIn<Decimal?>(itemCurySettings.StdCost, new Decimal?(), new Decimal?(0M)) ? 1 : 0) : 0;
    newItem.ValMethod = num == 0 ? (string) null : "T";
    return newItem;
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    base._(e);
    PX.Objects.IN.InventoryItem row = e.Row;
    if ((row != null ? (row.IsConverted.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) null).For<PX.Objects.IN.InventoryItem.itemType>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    chained = chained.SameFor<PX.Objects.IN.InventoryItem.nonStockReceipt>();
    chained.SameFor<PX.Objects.IN.InventoryItem.nonStockShip>();
  }
}
