// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemLotSerialAttributesMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INItemLotSerialAttributesMaint : 
  PXGraph<
  #nullable disable
  INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>
{
  public FbqlSelect<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemLotSerial>.On<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.FK.INItemLotSerial>>, FbqlJoins.Inner<InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  InventoryItem.inventoryID>>>>, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.View ItemLotSerial;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.View CurrentItemLotSerial;
  public FbqlSelect<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<INTranSplit.FK.Tran>>, FbqlJoins.Left<PX.Objects.PO.POReceipt>.On<INTran.FK.POReceipt>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INTranSplit.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INTranSplit.lotSerialNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INTranSplit>.View History;

  [PXMergeAttributes]
  [Inventory(typeof (Search2<InventoryItem.inventoryID, InnerJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>>, Where2<Match<Current<AccessInfo.userName>>, And<InventoryItem.stkItem, Equal<True>, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>, And<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), IsKey = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (SearchFor<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr>.In<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemLotSerial>.On<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.FK.INItemLotSerial>>>.Where<BqlOperand<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.FromCurrent>>>), new System.Type[] {typeof (PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr), typeof (PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.mfgLotSerialNbr), typeof (INItemLotSerial.qtyOnHand), typeof (INItemLotSerial.qtyAvail), typeof (INItemLotSerial.expireDate)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.lotSerialNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemLotSerial.qtyOnHand> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemLotSerial.qtyAvail> e)
  {
  }

  [PXMergeAttributes]
  [INTranType.List]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.tranType> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Date")]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.tranDate> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.refNbr, Equal<BqlField<INTranSplit.refNbr, IBqlString>.FromCurrent>, And<INRegister.docType, Equal<BqlField<INTranSplit.docType, IBqlString>.FromCurrent>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Receipt Nbr.", Visible = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.pOReceiptNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Customer", Visible = true)]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Customer Name")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.acctName> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Shipment Nbr.", Visible = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.sOShipmentNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Invoice Nbr.", Visible = true)]
  [PXSelector(typeof (Search<SOInvoice.refNbr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.aRRefNbr> e)
  {
  }

  public INItemLotSerialAttributesMaint()
  {
    ((PXAction) this.Insert).SetEnabled(false);
    ((PXAction) this.Insert).SetVisible(false);
    ((PXAction) this.Delete).SetEnabled(false);
    ((PXAction) this.Delete).SetVisible(false);
    ((PXAction) this.CopyPaste).SetEnabled(false);
    ((PXAction) this.CopyPaste).SetVisible(false);
    ((PXSelectBase) this.ItemLotSerial).Cache.AllowInsert = ((PXSelectBase) this.ItemLotSerial).Cache.AllowDelete = false;
    ((PXSelectBase) this.History).Cache.AllowInsert = ((PXSelectBase) this.History).Cache.AllowDelete = ((PXSelectBase) this.History).Cache.AllowUpdate = false;
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    INItemLotSerialAttributesMaint.Configure(config.GetScreenConfigurationContext<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>());
  }

  protected static void Configure(
    WorkflowContext<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader> context)
  {
    context.AddScreenConfigurationFor((Func<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ScreenConfiguration.IStartConfigScreen, BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ScreenConfiguration.IConfigured) ((BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ActionDefinition.IContainerFillerActions>) (actions => actions.AddNew("ShowInventorySummary", (Func<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ActionDefinition.IConfigured>) (configAction => (BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.ActionDefinition.IConfigured) configAction.DisplayName("Inventory Summary").IsSidePanelScreen((Func<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.NavigationDefinition.IConfiguredSidePanel>) (sidePanelAction => sidePanelAction.NavigateToScreen<InventorySummaryEnq>().WithIcon("business").WithAssignments((Action<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.NavigationParameter.IContainerFillerNavigationActionParameters>) (assignment => assignment.Add<InventorySummaryEnqFilter.inventoryID>((Func<BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.NavigationParameter.INeedRightOperand, BoundedTo<INItemLotSerialAttributesMaint, PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.NavigationParameter.IConfigured>) (c => c.SetFromField<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID>()))))))))))));
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader> e)
  {
    if (e.Row == null)
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID)?.LotSerClassID);
    bool flag = inLotSerClass != null && inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.descr>(((PXSelectBase) this.ItemLotSerial).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.salesPrice>(((PXSelectBase) this.ItemLotSerial).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.recPrice>(((PXSelectBase) this.ItemLotSerial).Cache, (object) null, flag);
  }
}
