// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOEmailProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.RQ;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOEmailProcessing : 
  PXFilteredProcessingJoin<
  #nullable disable
  SOOrderProcessSelected, SOProcessFilter, LeftJoinSingleTable<PX.Objects.AR.Customer, On<BqlOperand<
  #nullable enable
  SOOrder.customerID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AR.Customer.bAccountID>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOOrderProcessSelected.behavior, 
  #nullable disable
  Equal<SOBehavior.tR>>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  SOOrder.orderDate, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOProcessFilter.endDate, IBqlDateTime>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SOProcessFilter.startDate>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  SOOrder.orderDate, IBqlDateTime>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOProcessFilter.startDate, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  And<WorkflowAction.IsEnabled<SOOrderProcessSelected, SOProcessFilter.action>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SOProcessFilter.showAll>, 
  #nullable disable
  Equal<True>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SOProcessFilter.action>, 
  #nullable disable
  In3<SOOrderProcess.WellKnownActions.SOOrderScreen.printSalesOrder, SOOrderProcess.WellKnownActions.SOOrderScreen.printQuote, SOOrderProcess.WellKnownActions.SOOrderScreen.printBlanket>>>>>.And<BqlOperand<
  #nullable enable
  SOOrder.printed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SOProcessFilter.action>, 
  #nullable disable
  In3<SOOrderProcess.WellKnownActions.SOOrderScreen.emailSalesOrder, SOOrderProcess.WellKnownActions.SOOrderScreen.emailQuote, SOOrderProcess.WellKnownActions.SOOrderScreen.emailBlanket>>>>>.And<BqlOperand<
  #nullable enable
  SOOrder.emailed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>>>
{
  public SOEmailProcessing(PXGraph graph)
    : base(graph)
  {
    ((PXProcessingBase<SOOrderProcessSelected>) this)._OuterView.WhereAndCurrent<SOProcessFilter>("ownerID");
  }

  public SOEmailProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    ((PXProcessingBase<SOOrderProcessSelected>) this)._OuterView.WhereAndCurrent<SOProcessFilter>("ownerID");
  }
}
