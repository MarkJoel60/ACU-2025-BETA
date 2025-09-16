// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ChangeOrderClassMaint : PXGraph<ChangeOrderClassMaint, PMChangeOrderClass>
{
  public PXSelect<PMChangeOrderClass> Item;
  public PXSelect<PMChangeOrderClass, Where<PMChangeOrderClass.classID, Equal<Current<PMChangeOrderClass.classID>>>> ItemSettings;
  public PXSetup<PMSetup> Setup;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<PMChangeOrderClass, PMChangeOrder> Mapping;

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isCostBudgetEnabled> e)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isCostBudgetEnabled>, PMChangeOrderClass, object>) e).NewValue).GetValueOrDefault() && PXResultset<PMChangeOrderBudget>.op_Implicit(((PXSelectBase<PMChangeOrderBudget>) new PXSelectJoin<PMChangeOrderBudget, InnerJoin<PMChangeOrder, On<PMChangeOrderBudget.refNbr, Equal<PMChangeOrder.refNbr>>>, Where<PMChangeOrderBudget.type, Equal<AccountType.expense>, And<PMChangeOrder.classID, Equal<Current<PMChangeOrderClass.classID>>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<PMChangeOrderClass.isCostBudgetEnabled>("Before disabling cost budget modification for the change order class, please make sure there are no change orders belonging to this class that affect project cost budget.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isRevenueBudgetEnabled> e)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isRevenueBudgetEnabled>, PMChangeOrderClass, object>) e).NewValue).GetValueOrDefault() && PXResultset<PMChangeOrderBudget>.op_Implicit(((PXSelectBase<PMChangeOrderBudget>) new PXSelectJoin<PMChangeOrderBudget, InnerJoin<PMChangeOrder, On<PMChangeOrderBudget.refNbr, Equal<PMChangeOrder.refNbr>>>, Where<PMChangeOrderBudget.type, Equal<AccountType.income>, And<PMChangeOrder.classID, Equal<Current<PMChangeOrderClass.classID>>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<PMChangeOrderClass.isRevenueBudgetEnabled>("Before disabling revenue budget modification for the change order class, please make sure there are no change orders belonging to this class that affect project revenue budget.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isPurchaseOrderEnabled> e)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isPurchaseOrderEnabled>, PMChangeOrderClass, object>) e).NewValue).GetValueOrDefault() && PXResultset<PMChangeOrderLine>.op_Implicit(((PXSelectBase<PMChangeOrderLine>) new PXSelectJoin<PMChangeOrderLine, InnerJoin<PMChangeOrder, On<PMChangeOrderLine.refNbr, Equal<PMChangeOrder.refNbr>>>, Where<PMChangeOrder.classID, Equal<Current<PMChangeOrderClass.classID>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<PMChangeOrderClass.isPurchaseOrderEnabled>("Before disabling commitments modification for the change order class, please make sure there are no change orders belonging to this class that affect project commitments.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isAdvance> e)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderClass, PMChangeOrderClass.isAdvance>, PMChangeOrderClass, object>) e).NewValue).GetValueOrDefault() && PXResultset<PMChangeRequest>.op_Implicit(((PXSelectBase<PMChangeRequest>) new PXSelectJoin<PMChangeRequest, InnerJoin<PMChangeOrder, On<PMChangeRequest.changeOrderNbr, Equal<PMChangeOrder.refNbr>>>, Where<PMChangeOrder.classID, Equal<Current<PMChangeOrderClass.classID>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<PMChangeOrderClass.isAdvance>("The change order class cannot be modified because it is already used in multiple entities.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderClass> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<PMChangeOrderClass.isPurchaseOrderEnabled>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderClass>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault());
  }
}
