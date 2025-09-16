// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.UseLotSerialSpecificDetailsExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.FS;

public class UseLotSerialSpecificDetailsExt : PXGraphExtension<INLotSerClassMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails>, INLotSerClass, object>) e).NewValue == e.OldValue || e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails>>) e).Cache.GetStatus((object) e.Row) == 2)
      return;
    ISet<string> serviceOrdersNbrs = this.GetOpenServiceOrdersNbrs(e.Row.LotSerClassID);
    if (serviceOrdersNbrs.Any<string>())
    {
      PXTrace.WriteError("The state of the Specify Lot/Serial Price and Description check box cannot be changed because items with this lot or serial class exist in the following open service orders: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) serviceOrdersNbrs)
      });
      throw new PXSetPropertyException<INLotSerClass.useLotSerSpecificDetails>("The state of the check box cannot be changed because items with this lot or serial class exist in open documents. See the trace log for details.");
    }
  }

  private ISet<string> GetOpenServiceOrdersNbrs(string lotSerClassID)
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    return (ISet<string>) ((IQueryable<PXResult<FSServiceOrder>>) PXSelectBase<FSServiceOrder, PXViewOf<FSServiceOrder>.BasedOn<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemPlan>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.refNoteID, Equal<FSServiceOrder.noteID>>>>, And<BqlOperand<INItemPlan.refEntityType, IBqlString>.IsEqual<Constants.DACName<FSServiceOrder>>>>, And<BqlOperand<FSServiceOrder.canceled, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<FSServiceOrder.closed, IBqlBool>.IsNotEqual<True>>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INItemPlan.FK.InventoryItem>>>.Where<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<P.AsString>>.Order<By<BqlField<FSServiceOrder.createdDateTime, IBqlDateTime>.Desc, BqlField<FSServiceOrder.refNbr, IBqlString>.Asc>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 20, new object[1]
    {
      (object) lotSerClassID
    })).Select<PXResult<FSServiceOrder>, string>(Expression.Lambda<Func<PXResult<FSServiceOrder>, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult<FSServiceOrder>.get_Record), __typeref (PXResult<FSServiceOrder>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FSServiceOrder.get_RefNbr))), parameterExpression)).ToHashSet<string>();
  }
}
