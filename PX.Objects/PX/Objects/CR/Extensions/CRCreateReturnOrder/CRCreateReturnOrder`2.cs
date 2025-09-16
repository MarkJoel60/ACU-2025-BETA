// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateReturnOrder.CRCreateReturnOrder`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateReturnOrder;

/// <summary>
/// Extension that is used for creating return orders purposes.
/// </summary>
public abstract class CRCreateReturnOrder<TGraph, TMaster> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
{
  [PXCopyPasteHiddenView]
  [PXViewName("Create Sales Order")]
  public CRValidationFilter<CreateReturnOrderFilter> CreateOrderParams;
  public PXAction<TMaster> CreateReturnOrder;

  public static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [InjectDependency]
  internal IPXPageIndexingService PageService { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.PopupValidator = CRPopupValidator.Create<CreateReturnOrderFilter>(this.CreateOrderParams);
  }

  public CRPopupValidator.Generic<CreateReturnOrderFilter> PopupValidator { get; private set; }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search2<SOOrderType.orderType, InnerJoin<SOSetup, On<SOOrderType.orderType, Equal<SOSetup.defaultReturnOrderType>>>, Where<SOOrderType.active, Equal<boolTrue>>>))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<boolTrue>>), "Order Type '{0}' is not active.", new System.Type[] {typeof (SOOrderType.descr)})]
  [PXRestrictor(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.rM>>), "The order type cannot be used.", new System.Type[] {})]
  public virtual void _(
    PX.Data.Events.CacheAttached<CreateReturnOrderFilter.orderType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<CreateReturnOrderFilter.orderNbr> e)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable createReturnOrder(PXAdapter adapter)
  {
    if (!this.CheckBAccountStateBeforeConvert())
      throw new PXException("To create a return order, you must specify a business account that has the Customer or Customer & Vendor type.");
    if (!this.HasAccessToCreateReturnOrder())
      throw new PXException("You do not have access rights to create a return order. Contact your system administrator if you need your permissions to be updated.");
    if (WebDialogResultExtension.IsPositive(this.PopupValidator.AskExt()) && this.PopupValidator.TryValidate())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PX.Objects.CR.Extensions.CRCreateReturnOrder.CRCreateReturnOrder<TGraph, TMaster>.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new PX.Objects.CR.Extensions.CRCreateReturnOrder.CRCreateReturnOrder<TGraph, TMaster>.\u003C\u003Ec__DisplayClass14_0();
      this.Base.Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.graph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CcreateReturnOrder\u003Eb__0)));
    }
    return adapter.Get();
  }

  public virtual void DoCreateReturnOrder()
  {
    CreateReturnOrderFilter current1 = ((PXSelectBase<CreateReturnOrderFilter>) this.CreateOrderParams).Current;
    TMaster current2 = (TMaster) this.Base.Caches[typeof (TMaster)].Current;
    if (current1 == null || (object) current2 == null)
      return;
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    this.DoCreateReturnOrder(instance, current2, current1);
    if (!this.Base.IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    ((PXAction) instance.Save).Press();
  }

  public virtual void DoCreateReturnOrder(
    SOOrderEntry soGraph,
    TMaster document,
    CreateReturnOrderFilter filter)
  {
    SOOrder returnOrderEntity = this.CreateReturnOrderEntity(soGraph, document, filter);
    this.FillRelations(soGraph, document, returnOrderEntity);
    this.FillNotesAndFiles(soGraph, returnOrderEntity, document);
    this.FillUDFs(soGraph, returnOrderEntity, document);
    ((PXSelectBase<SOOrder>) soGraph.Document).Update(returnOrderEntity);
  }

  public virtual SOOrder CreateReturnOrderEntity(
    SOOrderEntry soGraph,
    TMaster document,
    CreateReturnOrderFilter filter)
  {
    SOOrder soOrder1 = new SOOrder();
    soOrder1.OrderType = ((PXSelectBase<CreateReturnOrderFilter>) this.CreateOrderParams).Current.OrderType ?? "RM";
    if (!string.IsNullOrWhiteSpace(filter.OrderNbr))
      soOrder1.OrderNbr = filter.OrderNbr;
    SOOrder soOrder2 = ((PXSelectBase<SOOrder>) soGraph.Document).Insert(soOrder1);
    SOOrder copy = PXCache<SOOrder>.CreateCopy(PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) soGraph.Document).Search<SOOrder.orderNbr>((object) soOrder2.OrderNbr, Array.Empty<object>())));
    return this.FillSalesOrder(soGraph, document, copy);
  }

  public virtual void FillNotesAndFiles(
    SOOrderEntry soGraph,
    SOOrder returnOrder,
    TMaster document)
  {
    PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof (TMaster)], (object) document, ((PXSelectBase) soGraph.Document).Cache, (object) returnOrder, this.Base.Caches[typeof (CRSetup)].Current as PXNoteAttribute.IPXCopySettings);
  }

  public virtual void FillUDFs(SOOrderEntry soGraph, SOOrder returnOrder, TMaster document)
  {
    UDFHelper.CopyAttributes(this.Base.Caches[typeof (TMaster)], (object) document, ((PXSelectBase) soGraph.Document).Cache, ((PXSelectBase) soGraph.Document).Cache.Current, returnOrder.OrderType);
  }

  public virtual SOOrder FillSalesOrder(
    SOOrderEntry docgraph,
    TMaster document,
    SOOrder salesOrder)
  {
    return salesOrder;
  }

  public virtual CRRelation FillRelations(
    SOOrderEntry docgraph,
    TMaster document,
    SOOrder salesOrder)
  {
    return (CRRelation) null;
  }

  public virtual bool CheckBAccountStateBeforeConvert() => true;

  public virtual bool HasAccessToCreateReturnOrder()
  {
    System.Type type = typeof (SOOrderEntry);
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, type);
    if (siteMapNode == null)
      return false;
    string primaryView = this.PageService.GetPrimaryView(type.FullName);
    PXCacheInfo cache = GraphHelper.GetGraphView(type, primaryView).Cache;
    PXCacheRights pxCacheRights;
    List<string> stringList1;
    List<string> stringList2;
    PXAccess.Provider.GetRights(siteMapNode.ScreenID, type.Name, cache.CacheType, ref pxCacheRights, ref stringList1, ref stringList2);
    return pxCacheRights >= 3;
  }
}
