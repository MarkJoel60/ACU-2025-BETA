// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INLotSerClassMaintExt.UseLotSerialSpecificDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INLotSerClassMaintExt;

public class UseLotSerialSpecificDetailsExt : PXGraphExtension<INLotSerClassMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails> e)
  {
    if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails>>) e).Cache.GetStatus((object) e.Row) == 2)
      return;
    INLotSerClass row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.useLotSerSpecificDetails>, INLotSerClass, object>) e).NewValue == e.OldValue)
      return;
    PXResultset<PX.Objects.SO.SOOrder> source = PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemPlan>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.refNoteID, Equal<PX.Objects.SO.SOOrder.noteID>>>>, And<BqlOperand<INItemPlan.refEntityType, IBqlString>.IsEqual<PX.Objects.Common.Constants.DACName<PX.Objects.SO.SOOrder>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.completed, IBqlBool>.IsNotEqual<True>>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INItemPlan.FK.InventoryItem>>>.Where<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<P.AsString>>.Order<By<BqlField<PX.Objects.SO.SOOrder.createdDateTime, IBqlDateTime>.Desc, BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.Asc>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 20, new object[1]
    {
      (object) row.LotSerClassID
    });
    if (((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) source).Any<PXResult<PX.Objects.SO.SOOrder>>())
    {
      object[] objArray = new object[1];
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      objArray[0] = (object) string.Join(", ", (IEnumerable<string>) ((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) source).Select<PXResult<PX.Objects.SO.SOOrder>, string>(Expression.Lambda<Func<PXResult<PX.Objects.SO.SOOrder>, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult<PX.Objects.SO.SOOrder>.get_Record), __typeref (PXResult<PX.Objects.SO.SOOrder>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.SO.SOOrder.get_OrderNbr))), parameterExpression)).ToHashSet<string>());
      PXTrace.WriteError("The state of the Specify Lot/Serial Price and Description check box cannot be changed because items with this lot or serial class exist in the following open sales orders: {0}.", objArray);
      throw new PXSetPropertyException<INLotSerClass.useLotSerSpecificDetails>("The state of the check box cannot be changed because items with this lot or serial class exist in open documents. See the trace log for details.");
    }
  }
}
