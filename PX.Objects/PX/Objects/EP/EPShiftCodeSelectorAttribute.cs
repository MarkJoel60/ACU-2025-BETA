// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public abstract class EPShiftCodeSelectorAttribute : 
  PXSelectorAttribute,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber
{
  protected Type _DateField;

  protected EPShiftCodeSelectorAttribute(Type compareDateField)
    : base(((IBqlTemplate) BqlTemplate.OfCommand<FbqlSelect<SelectFromBase<EPShiftCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPShiftCodeRate>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPShiftCodeRate.shiftID, Equal<EPShiftCode.shiftID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPShiftCodeRate.effectiveDate, LessEqual<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.AsOptional>>>>>.Or<BqlOperand<Optional<BqlPlaceholder.A>, BqlPlaceholder.IBqlAny>.IsNull>>>>>.Where<BqlOperand<EPShiftCode.isManufacturingShift, IBqlBool>.IsEqual<False>>.Aggregate<To<GroupBy<EPShiftCode.shiftID>>>, EPShiftCode>.SearchFor<EPShiftCode.shiftID>>.Replace<BqlPlaceholder.A>(compareDateField)).ToType())
  {
    this.SubstituteKey = typeof (EPShiftCode.shiftCD);
    this.DescriptionField = typeof (EPShiftCode.description);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this._DateField != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(sender.GetItemType(), this._DateField.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0)));
  }

  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null)
      return;
    this.SetDefaultShiftCode(sender, e.Row);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    try
    {
      sender.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
    }
    catch (Exception ex)
    {
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, obj, ex);
    }
  }

  protected virtual void SetDefaultShiftCode(PXCache cache, object row)
  {
    EPEmployee employee = this.GetEmployee(cache, row);
    if (employee == null || !employee.ShiftID.HasValue || !this.IsShiftCodeEffective(cache, row, employee.ShiftID))
      return;
    cache.SetValue(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) employee.ShiftID);
  }

  protected virtual bool IsShiftCodeEffective(PXCache cache, object row, int? shiftID)
  {
    return new PXView(cache.Graph, true, this._LookupSelect).SelectMulti(this.GetQueryParameters(cache, row)).Select(x => new
    {
      ShiftCode = (EPShiftCode) ((PXResult) x)[typeof (EPShiftCode)],
      Rate = (EPShiftCodeRate) ((PXResult) x)[typeof (EPShiftCodeRate)]
    }).Any(x =>
    {
      EPShiftCode shiftCode = x.ShiftCode;
      if ((shiftCode != null ? (shiftCode.IsActive.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
      EPShiftCodeRate rate = x.Rate;
      if (rate == null)
        return !shiftID.HasValue;
      int? shiftId = rate.ShiftID;
      int? nullable = shiftID;
      return shiftId.GetValueOrDefault() == nullable.GetValueOrDefault() & shiftId.HasValue == nullable.HasValue;
    });
  }

  protected abstract EPEmployee GetEmployee(PXCache cache, object row);

  protected abstract object[] GetQueryParameters(PXCache cache, object row);
}
