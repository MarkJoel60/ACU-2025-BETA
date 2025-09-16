// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SiteAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Bql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class SiteAvailAttribute : SiteAttribute, IPXFieldDefaultingSubscriber
{
  protected System.Type _inventoryType;
  protected System.Type _subItemType;
  protected System.Type _costCenterType;
  protected readonly System.Type[] _hiddenSiteStatusColumns = new System.Type[2]
  {
    typeof (INSiteStatusByCostCenter.qtyAvail),
    typeof (INSiteStatusByCostCenter.qtyHardAvail)
  };

  public System.Type DocumentBranchType { get; set; }

  public SiteAvailAttribute(System.Type InventoryType)
  {
    this._inventoryType = InventoryType;
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) SiteAvailAttribute.CreateSelector(BqlCommand.AppendJoin<LeftJoin<PX.Objects.CR.Address, On<INSite.FK.Address>, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<PX.Objects.CR.Address.countryID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.stateID, Equal<PX.Objects.CR.Address.state>>>>>>(SiteAvailAttribute.Search), BqlTemplate.OfJoin<LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.siteID, Equal<INSite.siteID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<SiteAvailAttribute.InventoryPh>>, And<INSiteStatusByCostCenter.costCenterID, Equal<SiteAvailAttribute.CostCenterPh>>>>, LeftJoin<INItemSiteSettings, On<INItemSiteSettings.siteID, Equal<INSite.siteID>, And<INItemSiteSettings.inventoryID, Equal<CostCenter.freeStock>>>>>>.Replace<SiteAvailAttribute.InventoryPh>(InventoryType).ToType(), ((IEnumerable<System.Type>) new System.Type[8]
    {
      typeof (INSite.siteCD),
      typeof (INSiteStatusByCostCenter.qtyOnHand),
      typeof (INSite.descr),
      typeof (PX.Objects.CR.Address.addressLine1),
      typeof (PX.Objects.CR.Address.addressLine2),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CS.Country.description),
      typeof (PX.Objects.CS.State.name)
    }).Union<System.Type>((IEnumerable<System.Type>) this._hiddenSiteStatusColumns).ToArray<System.Type>());
  }

  public SiteAvailAttribute(System.Type InventoryType, System.Type SubItemType, System.Type CostCenterType)
  {
    this._inventoryType = InventoryType;
    this._subItemType = SubItemType;
    this._costCenterType = CostCenterType;
    System.Type centerExpression = this.GetCostCenterExpression();
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) SiteAvailAttribute.CreateSelector(BqlCommand.AppendJoin<LeftJoin<PX.Objects.CR.Address, On<INSite.FK.Address>>>(SiteAvailAttribute.Search), BqlTemplate.OfJoin<LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.siteID, Equal<INSite.siteID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<SiteAvailAttribute.InventoryPh>>, And<INSiteStatusByCostCenter.subItemID, Equal<Optional<SiteAvailAttribute.SubItemPh>>, And<SiteAvailAttribute.CostCenterPh>>>>>>.Replace<SiteAvailAttribute.InventoryPh>(InventoryType).Replace<SiteAvailAttribute.SubItemPh>(SubItemType).Replace<SiteAvailAttribute.CostCenterPh>(centerExpression).ToType(), ((IEnumerable<System.Type>) new System.Type[4]
    {
      typeof (INSite.siteCD),
      typeof (INSiteStatusByCostCenter.qtyOnHand),
      typeof (INSiteStatusByCostCenter.active),
      typeof (INSite.descr)
    }).Union<System.Type>((IEnumerable<System.Type>) this._hiddenSiteStatusColumns).ToArray<System.Type>());
  }

  public SiteAvailAttribute(
    System.Type InventoryType,
    System.Type SubItemType,
    System.Type CostCenterType,
    System.Type[] colsType)
  {
    this._inventoryType = InventoryType;
    this._subItemType = SubItemType;
    this._costCenterType = CostCenterType;
    System.Type centerExpression = this.GetCostCenterExpression();
    System.Type type = BqlTemplate.OfJoin<LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.siteID, Equal<INSite.siteID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<SiteAvailAttribute.InventoryPh>>, And<INSiteStatusByCostCenter.subItemID, Equal<Optional<SiteAvailAttribute.SubItemPh>>, And<SiteAvailAttribute.CostCenterPh>>>>>>.Replace<SiteAvailAttribute.InventoryPh>(InventoryType).Replace<SiteAvailAttribute.SubItemPh>(SubItemType).Replace<SiteAvailAttribute.CostCenterPh>(centerExpression).ToType();
    colsType = ((IEnumerable<System.Type>) colsType).Union<System.Type>((IEnumerable<System.Type>) this._hiddenSiteStatusColumns).ToArray<System.Type>();
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) SiteAvailAttribute.CreateSelector(SiteAvailAttribute.Search, type, colsType);
  }

  private System.Type GetCostCenterExpression()
  {
    return typeof (IConstant).IsAssignableFrom(this._costCenterType) ? BqlTemplate.OfCondition<Where<INSiteStatusByCostCenter.costCenterID, Equal<SiteAvailAttribute.CostCenterPh>>>.Replace<SiteAvailAttribute.CostCenterPh>(this._costCenterType).ToType() : BqlTemplate.OfCondition<Where<INSiteStatusByCostCenter.costCenterID, EqualSameCostCenter<Current2<SiteAvailAttribute.CostCenterPh>>>>.Replace<SiteAvailAttribute.CostCenterPh>(this._costCenterType).ToType();
  }

  private static PXDimensionSelectorAttribute CreateSelector(
    System.Type searchType,
    System.Type lookupJoin,
    System.Type[] colsType)
  {
    return new PXDimensionSelectorAttribute("INSITE", searchType, lookupJoin, typeof (INSite.siteCD), true, colsType)
    {
      DescriptionField = typeof (INSite.descr)
    };
  }

  private static System.Type Search { get; } = typeof (PX.Data.Search<INSite.siteID, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<Current<AccessInfo.userName>>>>>);

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    System.Type itemType = sender.GetItemType();
    string name = this._inventoryType.Name;
    SiteAvailAttribute siteAvailAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) siteAvailAttribute, __vmethodptr(siteAvailAttribute, InventoryID_FieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    this.HideSiteStatusColumns(sender.Graph);
  }

  public virtual void InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    try
    {
      sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    }
    catch (PXUnitConversionException ex)
    {
    }
    catch (PXSetPropertyException ex)
    {
      PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null);
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
    }
  }

  protected virtual void HideSiteStatusColumns(PXGraph graph)
  {
    PXCache cach = graph.Caches[typeof (INSiteStatusByCostCenter)];
    foreach (System.Type siteStatusColumn in this._hiddenSiteStatusColumns)
      PXUIFieldAttribute.SetVisible(cach, siteStatusColumn.Name, false);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel || e.Row == null)
      return;
    object obj = sender.GetValue(e.Row, this._inventoryType.Name);
    if (obj == null)
      return;
    string baseCuryId = this.GetBaseCuryID(sender.Graph);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(sender.Graph, new int?((int) obj), baseCuryId);
    if ((itemCurySettings != null ? (!itemCurySettings.DfltSiteID.HasValue ? 1 : 0) : 1) != 0)
      return;
    INSite inSite = PXResultset<INSite>.op_Implicit(PXSelectBase<INSite, PXSelectReadonly<INSite, Where<INSite.siteID, Equal<Required<INSite.siteID>>, And<Match<INSite, Current<AccessInfo.userName>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) itemCurySettings.DfltSiteID
    }));
    if (inSite == null)
      return;
    e.NewValue = (object) inSite.SiteID;
  }

  protected virtual string GetBaseCuryID(PXGraph graph)
  {
    if (this.DocumentBranchType == (System.Type) null)
      return graph.Accessinfo.BaseCuryID;
    System.Type itemType = BqlCommand.GetItemType(this.DocumentBranchType);
    PXCache cach = graph.Caches[itemType];
    int? nullable = cach.GetValue(cach.Current, this.DocumentBranchType.Name) as int?;
    if (cach?.GetStateExt((object) null, this.DocumentBranchType.Name) is PXBranchSelectorState)
    {
      string baseCuryId = PXAccess.GetBranchByBAccountID(nullable)?.BaseCuryID;
      if (baseCuryId != null)
        return baseCuryId;
      return ((PXAccess.Organization) PXAccess.GetOrganizationByID(nullable))?.BaseCuryID;
    }
    string baseCuryId1 = PXAccess.GetBranch(nullable)?.BaseCuryID;
    if (baseCuryId1 != null)
      return baseCuryId1;
    return ((PXAccess.Organization) PXAccess.GetOrganizationByID(nullable))?.BaseCuryID;
  }

  [PXHidden]
  private class InventoryPh : BqlPlaceholderBase
  {
  }

  [PXHidden]
  private class SubItemPh : BqlPlaceholderBase
  {
  }

  [PXHidden]
  private class CostCenterPh : BqlPlaceholderBase
  {
  }
}
