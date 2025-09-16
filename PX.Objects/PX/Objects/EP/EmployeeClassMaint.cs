// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EmployeeClassMaint : PXGraph<EmployeeClassMaint>
{
  public PXSelect<EPVendorClass> DummyVendorClass;
  public PXSelect<EPEmployeeClass> EmployeeClass;
  public PXSelect<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployeeClass.vendorClassID>>>> CurEmployeeClassRecord;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<EPEmployeeClass, EPEmployee> Mapping;
  public PXSave<EPEmployeeClass> Save;
  public PXAction<EPEmployeeClass> cancel;
  public PXInsert<EPEmployeeClass> Insert;
  public PXCopyPasteAction<EPEmployeeClass> Edit;
  public PXDelete<EPEmployeeClass> Delete;
  public PXFirst<EPEmployeeClass> First;
  public PXPrevious<EPEmployeeClass> Prev;
  public PXNext<EPEmployeeClass> Next;
  public PXLast<EPEmployeeClass> Last;
  public PXSelectReadonly<CMSetup> cmsetup;

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    EmployeeClassMaint employeeClassMaint = this;
    foreach (EPEmployeeClass epEmployeeClass in ((PXAction) new PXCancel<EPEmployeeClass>((PXGraph) employeeClassMaint, nameof (Cancel))).Press(a))
    {
      if (((PXSelectBase) employeeClassMaint.EmployeeClass).Cache.GetStatus((object) epEmployeeClass) == 2)
      {
        if (PXResultset<EPVendorClass>.op_Implicit(PXSelectBase<EPVendorClass, PXSelect<EPVendorClass, Where<EPVendorClass.vendorClassID, Equal<Required<EPVendorClass.vendorClassID>>>>.Config>.Select((PXGraph) employeeClassMaint, new object[1]
        {
          (object) epEmployeeClass.VendorClassID
        })) != null)
          ((PXSelectBase) employeeClassMaint.EmployeeClass).Cache.RaiseExceptionHandling<EPEmployeeClass.vendorClassID>((object) epEmployeeClass, (object) epEmployeeClass.VendorClassID, (Exception) new PXSetPropertyException("This ID is already used for the Vendor Class."));
      }
      yield return (object) epEmployeeClass;
    }
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Attribute")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CSAttributeGroup.attributeID> e)
  {
  }

  protected virtual void EPEmployeeClass_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<EPEmployeeClass.curyID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<EPEmployeeClass.curyRateTypeID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<VendorClass.allowOverrideCury>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<VendorClass.allowOverrideRate>(cache, (object) null, flag);
    if (e.Row == null)
      return;
    EPEmployeeClass row = (EPEmployeeClass) e.Row;
    PXUIFieldAttribute.SetEnabled<EPEmployeeClass.cashAcctID>(cache, e.Row, !string.IsNullOrEmpty(row.PaymentMethodID));
  }

  public virtual void EPEmployeeClass_PaymentMethodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<EPEmployeeClass.cashAcctID>(e.Row);
  }

  protected virtual void EPEmployeeClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is EPEmployeeClass))
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.vendorClassID, Equal<Current<EPEmployeeClass.vendorClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (epEmployee != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted because it is referenced by Employee - {0}", new object[1]
      {
        (object) epEmployee.AcctCD
      });
    }
  }

  protected virtual void EPEmployeeClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase) this.EmployeeClass).Cache.GetStatus(e.Row) != 2 || e.Operation == 3)
      return;
    if (PXResultset<EPVendorClass>.op_Implicit(PXSelectBase<EPVendorClass, PXSelect<EPVendorClass, Where<EPVendorClass.vendorClassID, Equal<Current<EPEmployeeClass.vendorClassID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>())) != null)
    {
      sender.IsDirty = false;
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(typeof (EPEmployeeClass.vendorClassID).Name, (object) null, "This ID is already used for the Vendor Class.");
    }
  }

  protected virtual void EPEmployeeClass_OvertimeMultiplier_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("The value must be greater than zero");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEmployeeClass, EPEmployeeClass.probationPeriodMonths> e)
  {
    EPEmployeeClass row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPEmployeeClass, EPEmployeeClass.probationPeriodMonths>, EPEmployeeClass, object>) e).NewValue == e.OldValue)
      return;
    if (!((IQueryable<PXResult<EPEmployee>>) PXSelectBase<EPEmployee, PXViewOf<EPEmployee>.BasedOn<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.vendorClassID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.VendorClassID
    })).Any<PXResult<EPEmployee>>())
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPEmployeeClass, EPEmployeeClass.probationPeriodMonths>>) e).Cache.RaiseExceptionHandling<EPEmployeeClass.probationPeriodMonths>((object) row, (object) null, (Exception) new PXSetPropertyException("The new probation period will be used only for newly hired employees of the {0} employee class. For existing employees, the probation period end dates should be adjusted manually.", (PXErrorLevel) 2, new object[1]
    {
      (object) row.VendorClassID
    }));
  }

  public CMSetup CMSETUP
  {
    get
    {
      return PXResultset<CMSetup>.op_Implicit(((PXSelectBase<CMSetup>) this.cmsetup).Select(Array.Empty<object>())) ?? new CMSetup();
    }
  }
}
