// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintExt.ConvertStockToNonStockExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions.NonStockItemMaintExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintExt;

public class ConvertStockToNonStockExt : 
  ConvertStockToNonStockExtBase<InventoryItemMaint, NonStockItemMaint>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() && !PXAccess.FeatureInstalled<FeaturesSet.manufacturing>() && !PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && !PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>() && !PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && !PXAccess.FeatureInstalled<FeaturesSet.payrollModule>();
  }

  public override void Initialize()
  {
    base.Initialize();
    ((PXAction) this.convert).SetCaption("Convert to Non-Stock Item");
  }

  protected override int Verify(InventoryItem item, List<string> errors)
  {
    return this.VerifySiteStatus(item, errors) + base.Verify(item, errors) + this.VerifyPIClass(item, errors) + this.VerifyPI(item, errors) + this.VerifyKitSpec(item, errors) + this.VerifyINTransitLine(item, errors) + this.VerifyReplenishment(item, errors);
  }

  protected virtual int VerifySiteStatus(InventoryItem item, List<string> errors)
  {
    INSiteStatusByCostCenter[] array1 = GraphHelper.RowCast<INSiteStatusByCostCenter>((IEnumerable) PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>, And<BqlOperand<INSiteStatusByCostCenter.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INSiteStatusByCostCenter.siteID, IBqlInt>.IsNotEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) item.InventoryID,
      (object) ((PXSelectBase<INSetup>) this.Base.insetup).Current.TransitSiteID
    })).ToArray<INSiteStatusByCostCenter>();
    if (((IEnumerable<INSiteStatusByCostCenter>) array1).Any<INSiteStatusByCostCenter>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because its quantity on hand is less or more than zero in the following warehouses: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<INSiteStatusByCostCenter>) array1).Select<INSiteStatusByCostCenter, string>((Func<INSiteStatusByCostCenter, string>) (s => INSite.PK.Find((PXGraph) this.Base, s.SiteID)?.SiteCD?.Trim())))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    else
    {
      INLocationStatusByCostCenter[] array2 = GraphHelper.RowCast<INLocationStatusByCostCenter>((IEnumerable) PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>, And<BqlOperand<INLocationStatusByCostCenter.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocationStatusByCostCenter.siteID, IBqlInt>.IsNotEqual<P.AsInt>>>.Aggregate<To<GroupBy<INLocationStatusByCostCenter.siteID>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) item.InventoryID,
        (object) ((PXSelectBase<INSetup>) this.Base.insetup).Current.TransitSiteID
      })).ToArray<INLocationStatusByCostCenter>();
      if (((IEnumerable<INLocationStatusByCostCenter>) array2).Any<INLocationStatusByCostCenter>())
      {
        string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because its quantity on hand is less or more than zero in the following warehouses: {1}", new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", ((IEnumerable<INLocationStatusByCostCenter>) array2).Select<INLocationStatusByCostCenter, string>((Func<INLocationStatusByCostCenter, string>) (s => INSite.PK.Find((PXGraph) this.Base, s.SiteID)?.SiteCD?.Trim())))
        });
        PXTrace.WriteError(str);
        errors.Add(str);
        return array2.Length;
      }
    }
    return array1.Length;
  }

  protected virtual int VerifyPIClass(InventoryItem item, List<string> errors)
  {
    INPIClass[] array = GraphHelper.RowCast<INPIClass>((IEnumerable) PXSelectBase<INPIClass, PXViewOf<INPIClass>.BasedOn<SelectFromBase<INPIClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<Select<INPIClassItem, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIClassItem.inventoryID, Equal<P.AsInt>>>>>.And<INPIClassItem.FK.PIClass>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INPIClass>();
    if (((IEnumerable<INPIClass>) array).Any<INPIClass>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is selected on the Inventory Item Selection tab of the Physical Inventory Types (IN208900) form for the following inventory types: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<INPIClass>) array).Select<INPIClass, string>((Func<INPIClass, string>) (c => c.PIClassID)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyPI(InventoryItem item, List<string> errors)
  {
    INPIHeader[] array = GraphHelper.RowCast<INPIHeader>((IEnumerable) PXSelectBase<INPIHeader, PXViewOf<INPIHeader>.BasedOn<SelectFromBase<INPIHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIHeader.status, NotIn3<INPIHdrStatus.completed, INPIHdrStatus.cancelled>>>>>.And<Exists<Select<INPIDetail, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.inventoryID, Equal<P.AsInt>>>>>.And<INPIDetail.FK.PIHeader>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INPIHeader>();
    if (((IEnumerable<INPIHeader>) array).Any<INPIHeader>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included in an incomplete physical inventory count: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<INPIHeader>) array).Select<INPIHeader, string>((Func<INPIHeader, string>) (c => c.PIID)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyKitSpec(InventoryItem item, List<string> errors)
  {
    INKitSpecHdr[] array = GraphHelper.RowCast<INKitSpecHdr>((IEnumerable) PXSelectBase<INKitSpecHdr, PXViewOf<INKitSpecHdr>.BasedOn<SelectFromBase<INKitSpecHdr, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<Select<INKitSpecStkDet, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecStkDet.compInventoryID, Equal<P.AsInt>>>>>.And<INKitSpecStkDet.FK.KitSpecification>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INKitSpecHdr>();
    if (((IEnumerable<INKitSpecHdr>) array).Any<INKitSpecHdr>())
    {
      IEnumerable<string> values = ((IEnumerable<INKitSpecHdr>) array).Select<INKitSpecHdr, string>((Func<INKitSpecHdr, string>) (s => PXLocalizer.LocalizeFormat("the {0} kit, the {1} specification revision", new object[2]
      {
        (object) InventoryItem.PK.Find((PXGraph) this.Base, s.KitInventoryID)?.InventoryCD.Trim(),
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

  protected virtual int VerifyINTransitLine(InventoryItem item, List<string> errors)
  {
    if (PXResultset<INTransitLine>.op_Implicit(PXSelectBase<INTransitLine, PXViewOf<INTransitLine>.BasedOn<SelectFromBase<INTransitLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatusByCostCenter>.On<BqlOperand<INTransitLine.costSiteID, IBqlInt>.IsEqual<INLocationStatusByCostCenter.locationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) item.InventoryID
    })) == null)
      return 0;
    string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is in transit. For details, see the Goods In Transit (616500) report.", new object[1]
    {
      (object) item.InventoryCD.Trim()
    });
    PXTrace.WriteError(str);
    errors.Add(str);
    return 1;
  }

  protected virtual int VerifyReplenishment(InventoryItem item, List<string> errors)
  {
    INItemPlan[] array = GraphHelper.RowCast<INItemPlan>((IEnumerable) PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemPlan.planType, IBqlString>.IsEqual<INPlanConstants.plan90>>>.Aggregate<To<GroupBy<INItemPlan.fixedSource>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INItemPlan>();
    int num = 0;
    if (((IEnumerable<INItemPlan>) array).Any<INItemPlan>())
    {
      foreach (IGrouping<string, INItemPlan> grouping in ((IEnumerable<INItemPlan>) array).GroupBy<INItemPlan, string>((Func<INItemPlan, string>) (p => this.GetINItemPlanMessage(p))))
      {
        string str = PXLocalizer.LocalizeFormat(grouping.Key, new object[1]
        {
          (object) item.InventoryCD.Trim()
        });
        PXTrace.WriteError(str);
        errors.Add(str);
        ++num;
      }
    }
    return num;
  }

  protected override InventoryItem ConvertMainFields(
    NonStockItemMaint graph,
    InventoryItem source,
    InventoryItem newItem)
  {
    newItem = base.ConvertMainFields(graph, source, newItem);
    newItem.ItemType = "N";
    newItem.NonStockReceipt = new bool?(true);
    newItem.NonStockShip = new bool?(true);
    newItem.CompletePOLine = "Q";
    return newItem;
  }

  protected override void DeleteRelatedRecords(NonStockItemMaint graph, int? inventoryID)
  {
    base.DeleteRelatedRecords(graph, inventoryID);
    ConvertNonStockToStockExt implementation = ((PXGraph) graph).FindImplementation<ConvertNonStockToStockExt>();
    foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) implementation.AllItemSiteRecords).Select(new object[1]
    {
      (object) inventoryID
    }))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      ((PXSelectBase<INItemSite>) implementation.AllItemSiteRecords).Delete(inItemSite);
    }
    foreach (PXResult<INItemRep> pxResult in ((PXSelectBase<INItemRep>) implementation.AllReplenishmentRecords).Select(new object[1]
    {
      (object) inventoryID
    }))
    {
      INItemRep inItemRep = PXResult<INItemRep>.op_Implicit(pxResult);
      ((PXSelectBase<INItemRep>) implementation.AllReplenishmentRecords).Delete(inItemRep);
    }
    foreach (PXResult<INItemBoxEx> pxResult in ((PXSelectBase<INItemBoxEx>) implementation.Boxes).Select(new object[1]
    {
      (object) inventoryID
    }))
    {
      INItemBoxEx inItemBoxEx = PXResult<INItemBoxEx>.op_Implicit(pxResult);
      ((PXSelectBase<INItemBoxEx>) implementation.Boxes).Delete(inItemBoxEx);
    }
  }
}
