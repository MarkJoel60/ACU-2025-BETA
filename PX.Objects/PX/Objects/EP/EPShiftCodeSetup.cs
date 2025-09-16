// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class EPShiftCodeSetup : PXGraph<
#nullable disable
EPShiftCodeSetup>
{
  public PXSave<EPShiftCode> Save;
  public PXCancel<EPShiftCode> Cancel;
  public FbqlSelect<SelectFromBase<EPShiftCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  EPShiftCode.isManufacturingShift, IBqlBool>.IsEqual<
  #nullable disable
  False>>, EPShiftCode>.View Codes;
  public FbqlSelect<SelectFromBase<EPShiftCodeRate, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<EPShiftCodeRate.shiftID>.IsRelatedTo<EPShiftCode.shiftID>.AsSimpleKey.WithTablesOf<EPShiftCode, EPShiftCodeRate>, EPShiftCode, EPShiftCodeRate>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  EPShiftCodeRate.curyID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  EPShiftCodeRate>.View Rates;

  public virtual void _(
    Events.FieldUpdated<EPShiftCodeRate, EPShiftCodeRate.wageAmount> e)
  {
    if (e.Row == null)
      return;
    Decimal? newValue = e.NewValue as Decimal?;
    Decimal? nullable = newValue;
    Decimal? costingAmount = e.Row.CostingAmount;
    if (!(nullable.GetValueOrDefault() > costingAmount.GetValueOrDefault() & nullable.HasValue & costingAmount.HasValue))
      return;
    e.Row.CostingAmount = newValue;
    PXUIFieldAttribute.SetWarning<EPShiftCodeRate.costingAmount>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<EPShiftCodeRate, EPShiftCodeRate.wageAmount>>) e).Cache, (object) e.Row, "The costing amount cannot be less than the wage amount.");
  }

  public virtual void _(
    Events.FieldUpdated<EPShiftCodeRate, EPShiftCodeRate.costingAmount> e)
  {
    if (e.Row == null)
      return;
    Decimal? newValue = e.NewValue as Decimal?;
    Decimal? nullable = newValue;
    Decimal? wageAmount = e.Row.WageAmount;
    if (!(nullable.GetValueOrDefault() < wageAmount.GetValueOrDefault() & nullable.HasValue & wageAmount.HasValue))
      return;
    e.Row.WageAmount = newValue;
    PXUIFieldAttribute.SetWarning<EPShiftCodeRate.wageAmount>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<EPShiftCodeRate, EPShiftCodeRate.costingAmount>>) e).Cache, (object) e.Row, "The wage amount cannot be greater than the costing amount.");
  }

  public virtual void _(Events.RowSelected<EPShiftCodeRate> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache.GetStatus((object) e.Row) == 2;
    PXUIFieldAttribute.SetEnabled<EPShiftCodeRate.effectiveDate>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<EPShiftCodeRate.type>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<EPShiftCodeRate.percent>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache, (object) e.Row, flag && e.Row.Type == "PCT");
    PXUIFieldAttribute.SetEnabled<EPShiftCodeRate.wageAmount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache, (object) e.Row, flag && e.Row.Type == "AMT");
    PXUIFieldAttribute.SetEnabled<EPShiftCodeRate.costingAmount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EPShiftCodeRate>>) e).Cache, (object) e.Row, flag && e.Row.Type == "AMT");
  }

  public virtual void _(
    Events.FieldVerifying<EPShiftCodeRate, EPShiftCodeRate.effectiveDate> e)
  {
    DateTime newValue = (DateTime) ((Events.FieldVerifyingBase<Events.FieldVerifying<EPShiftCodeRate, EPShiftCodeRate.effectiveDate>, EPShiftCodeRate, object>) e).NewValue;
    DateTime? effectiveDate = (DateTime?) ((PXSelectBase<EPShiftCodeRate>) this.Rates).Select(Array.Empty<object>()).FirstTableItems.OrderByDescending<EPShiftCodeRate, DateTime?>((Func<EPShiftCodeRate, DateTime?>) (x => x.EffectiveDate)).FirstOrDefault<EPShiftCodeRate>()?.EffectiveDate;
    DateTime dateTime = newValue;
    DateTime? nullable = effectiveDate;
    if ((nullable.HasValue ? (dateTime < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<EPShiftCodeRate, EPShiftCodeRate.effectiveDate>>) e).Cache.RaiseExceptionHandling<EPShiftCodeRate.effectiveDate>((object) e.Row, e.OldValue, (Exception) new PXSetPropertyException("The shift code is already in use and no effective date can be added before {0}.", new object[1]
    {
      (object) effectiveDate
    }));
    ((Events.FieldVerifyingBase<Events.FieldVerifying<EPShiftCodeRate, EPShiftCodeRate.effectiveDate>, EPShiftCodeRate, object>) e).NewValue = (object) null;
  }

  public virtual void _(Events.RowPersisting<EPShiftCodeRate> e)
  {
    if (e.Row.Type == "AMT")
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
        e.Row.WageAmount = e.Row.CostingAmount;
      if (!e.Row.WageAmount.HasValue && PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
        ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache.RaiseExceptionHandling<EPShiftCodeRate.wageAmount>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<EPShiftCodeRate.wageAmount>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache)
        }));
      if (e.Row.CostingAmount.HasValue)
        return;
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache.RaiseExceptionHandling<EPShiftCodeRate.costingAmount>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPShiftCodeRate.costingAmount>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache)
      }));
    }
    else
    {
      if (e.Row.Percent.HasValue)
        return;
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache.RaiseExceptionHandling<EPShiftCodeRate.percent>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPShiftCodeRate.percent>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPShiftCodeRate>>) e).Cache)
      }));
    }
  }

  public static Decimal CalculateShiftWage(
    PXGraph graph,
    int? shiftID,
    DateTime? date,
    Decimal originalWage,
    Decimal otMultiplier)
  {
    EPShiftCodeRate effectiveRate = EPShiftCodeSetup.GetEffectiveRate(graph, shiftID, date);
    if (effectiveRate == null)
      return originalWage;
    if (effectiveRate.Type == "AMT")
    {
      Decimal num1 = originalWage;
      Decimal num2 = effectiveRate.WageAmount.GetValueOrDefault() * otMultiplier;
      return originalWage = num1 + num2;
    }
    Decimal num3 = originalWage;
    Decimal num4 = originalWage * effectiveRate.Percent.GetValueOrDefault() / 100M;
    return originalWage = num3 + num4;
  }

  public static Decimal CalculateShiftCosting(
    PXGraph graph,
    int? shiftID,
    DateTime? date,
    Decimal originalCost,
    Decimal otMultiplier)
  {
    EPShiftCodeRate effectiveRate = EPShiftCodeSetup.GetEffectiveRate(graph, shiftID, date);
    if (effectiveRate == null)
      return originalCost;
    if (effectiveRate.Type == "AMT")
    {
      Decimal num1 = originalCost;
      Decimal num2 = effectiveRate.CostingAmount.GetValueOrDefault() * otMultiplier;
      return originalCost = num1 + num2;
    }
    Decimal num3 = originalCost;
    Decimal num4 = originalCost * effectiveRate.Percent.GetValueOrDefault() / 100M;
    return originalCost = num3 + num4;
  }

  private static EPShiftCodeRate GetEffectiveRate(PXGraph graph, int? shiftID, DateTime? date)
  {
    return PXResultset<EPShiftCodeRate>.op_Implicit(((PXSelectBase<EPShiftCodeRate>) new FbqlSelect<SelectFromBase<EPShiftCodeRate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPShiftCodeRate.shiftID, Equal<P.AsInt>>>>>.And<BqlOperand<EPShiftCodeRate.effectiveDate, IBqlDateTime>.IsLessEqual<P.AsDateTime.UTC>>>.Order<By<BqlField<EPShiftCodeRate.effectiveDate, IBqlDateTime>.Desc>>, EPShiftCodeRate>.View(graph)).Select(new object[2]
    {
      (object) shiftID,
      (object) date
    }));
  }
}
