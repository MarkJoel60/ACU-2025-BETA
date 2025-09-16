// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UnitOfMeasureMaint
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

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(new Type[] {typeof (INUnit)}, new Type[] {typeof (Select<UnitOfMeasure, Where<UnitOfMeasure.unit, Equal<Current<INUnit.fromUnit>>>>)})]
public class UnitOfMeasureMaint : PXGraph<
#nullable disable
UnitOfMeasureMaint, UnitOfMeasure>
{
  public PXSelect<UnitOfMeasure> Unit;
  public FbqlSelect<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INUnit.unitType, 
  #nullable disable
  Equal<INUnitType.global>>>>, And<BqlOperand<
  #nullable enable
  INUnit.fromUnit, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  UnitOfMeasure.unit, IBqlString>.FromCurrent>>>>.And<
  #nullable disable
  Not<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INUnit.toUnit, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  UnitOfMeasure.unit, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INUnit.unitRate, IBqlDecimal>.IsEqual<
  #nullable disable
  decimal1>>>>>, INUnit>.View Units;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.Standalone.INUnit, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.Standalone.INUnit>.View DetailsUnit;

  protected virtual void _(Events.RowInserted<UnitOfMeasure> e)
  {
    UnitOfMeasure row = e.Row;
    if (row == null || string.IsNullOrEmpty(row.Unit))
      return;
    if (PXResultset<PX.Objects.IN.Standalone.INUnit>.op_Implicit(PXSelectBase<PX.Objects.IN.Standalone.INUnit, PXSelect<PX.Objects.IN.Standalone.INUnit, Where<INUnit.unitType, Equal<INUnitType.global>, And<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.Unit
    })) != null)
      return;
    PX.Objects.IN.Standalone.INUnit inUnit = new PX.Objects.IN.Standalone.INUnit();
    inUnit.UnitType = new short?((short) 3);
    inUnit.FromUnit = row.Unit;
    inUnit.ToUnit = row.Unit;
    inUnit.UnitRate = new Decimal?(1M);
    inUnit.UnitMultDiv = "M";
    ((PXSelectBase<PX.Objects.IN.Standalone.INUnit>) this.DetailsUnit).Insert(inUnit);
  }

  protected virtual void _(Events.RowSelected<UnitOfMeasure> e)
  {
    UnitOfMeasure row = e.Row;
    if (row == null)
      return;
    bool flag = row.Unit != null;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Units).Cache, (string) null, flag);
    ((PXSelectBase) this.Units).Cache.AllowInsert = flag;
    ((PXSelectBase) this.Units).Cache.AllowDelete = flag;
  }

  protected virtual void _(Events.RowDeleted<UnitOfMeasure> e)
  {
    UnitOfMeasure row = e.Row;
    if (row == null)
      return;
    foreach (PXResult<INUnit> pxResult in PXSelectBase<INUnit, PXSelect<INUnit, Where2<Where<INUnit.unitType, Equal<INUnitType.global>>, And<Where<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>, Or<INUnit.toUnit, Equal<Required<INUnit.toUnit>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.Unit,
      (object) row.Unit
    }))
      ((PXSelectBase<INUnit>) this.Units).Delete(PXResult<INUnit>.op_Implicit(pxResult));
  }

  protected virtual void _(Events.FieldUpdated<INUnit, INUnit.toUnit> e)
  {
    UnitOfMeasure current = ((PXSelectBase<UnitOfMeasure>) this.Unit).Current;
    if (current == null)
      return;
    INUnit row = e.Row;
    if (row == null || row.FromUnit != null)
      return;
    row.FromUnit = current.Unit;
  }

  protected virtual void _(Events.FieldVerifying<INUnit, INUnit.unitRate> e)
  {
    Decimal? newValue = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INUnit, INUnit.unitRate>, INUnit, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() == num & newValue.HasValue && (string) ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<INUnit, INUnit.unitRate>>) e).Cache.GetValue<INUnit.unitMultDiv>((object) e.Row) == "D")
      throw new PXSetPropertyException((IBqlTable) e.Row, "Incorrect value. The value to be entered must not be equal to {0}.", new object[1]
      {
        (object) "0"
      });
  }

  [PXMergeAttributes]
  [INUnit]
  [PXSelector(typeof (Search<UnitOfMeasure.unit, Where<UnitOfMeasure.unit, NotEqual<Current<UnitOfMeasure.unit>>>>), new Type[] {typeof (UnitOfMeasure.unit)})]
  protected virtual void _(Events.CacheAttached<INUnit.toUnit> e)
  {
  }

  public void AddNew(string unit)
  {
    if (string.IsNullOrEmpty(unit))
      return;
    if (PXResultset<UnitOfMeasure>.op_Implicit(PXSelectBase<UnitOfMeasure, PXSelect<UnitOfMeasure, Where<UnitOfMeasure.unit, Equal<Required<UnitOfMeasure.unit>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) unit
    })) != null)
      return;
    UnitOfMeasure instance = (UnitOfMeasure) ((PXSelectBase) this.Unit).Cache.CreateInstance();
    instance.Unit = unit;
    instance.Descr = unit;
    ((PXSelectBase) this.Unit).Cache.SetValueExt<UnitOfMeasure.descr>((object) ((PXSelectBase<UnitOfMeasure>) this.Unit).Insert(instance), (object) unit);
    ((PXGraph) this).Actions.PressSave();
  }
}
