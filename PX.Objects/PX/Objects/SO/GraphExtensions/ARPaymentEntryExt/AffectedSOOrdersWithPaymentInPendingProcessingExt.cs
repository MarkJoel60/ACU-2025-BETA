// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARPaymentEntryExt.AffectedSOOrdersWithPaymentInPendingProcessingExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;

public class AffectedSOOrdersWithPaymentInPendingProcessingExt : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedSOOrdersWithPaymentInPendingProcessingExt, ARPaymentEntry, PX.Objects.SO.SOOrder, SOOrderEntry>
{
  private readonly IDictionary<(string orderType, string orderNbr), int?> _oldPaymentsNeedValidationCntrValues = (IDictionary<(string, string), int?>) new Dictionary<(string, string), int?>();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  private PXCache<PX.Objects.SO.SOOrder> orders => GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base);

  protected override bool PersistInSameTransaction => false;

  protected override bool EntityIsAffected(PX.Objects.SO.SOOrder order)
  {
    int? valueOriginal = (int?) ((PXCache) this.orders).GetValueOriginal<PX.Objects.SO.SOOrder.paymentsNeedValidationCntr>((object) order);
    if (object.Equals((object) valueOriginal, (object) order.PaymentsNeedValidationCntr))
      return false;
    this._oldPaymentsNeedValidationCntrValues[(order.OrderType, order.OrderNbr)] = valueOriginal;
    return true;
  }

  protected override void ProcessAffectedEntity(SOOrderEntry orderEntry, PX.Objects.SO.SOOrder order)
  {
    int? validationCntrValue = this._oldPaymentsNeedValidationCntrValues[(order.OrderType, order.OrderNbr)];
    int? needValidationCntr1 = order.PaymentsNeedValidationCntr;
    int num = 0;
    if (needValidationCntr1.GetValueOrDefault() == num & needValidationCntr1.HasValue && validationCntrValue.HasValue)
    {
      ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.LostLastPaymentInPendingProcessing))).FireOn((PXGraph) orderEntry, order);
    }
    else
    {
      int? nullable = validationCntrValue;
      int? needValidationCntr2 = order.PaymentsNeedValidationCntr;
      if (!(nullable.GetValueOrDefault() < needValidationCntr2.GetValueOrDefault() & nullable.HasValue & needValidationCntr2.HasValue))
        return;
      ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.ObtainedPaymentInPendingProcessing))).FireOn((PXGraph) orderEntry, order);
    }
  }

  protected override PX.Objects.SO.SOOrder ActualizeEntity(SOOrderEntry orderEntry, PX.Objects.SO.SOOrder order)
  {
    return PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) orderEntry.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    }));
  }
}
