// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectManagerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR.Standalone;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

public class PMProjectManagerAttribute : PXAggregateAttribute, IPXFieldVerifyingSubscriber
{
  public PMProjectManagerAttribute()
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("EMPLOYEE", typeof (Search<EPEmployee.bAccountID>), typeof (EPEmployee.acctCD), new Type[4]
    {
      typeof (EPEmployee.bAccountID),
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.acctName),
      typeof (EPEmployee.departmentID)
    })
    {
      DescriptionField = typeof (EPEmployee.acctName)
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<BqlOperand<EPEmployee.vStatus, IBqlString>.IsEqual<VendorStatus.active>>), "The status of employee '{0}' is '{1}'.", new Type[2]
    {
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.vStatus)
    }));
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    EPEmployee epEmployee = ((PXSelectBase<EPEmployee>) new FbqlSelect<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.acctCD, IBqlString>.IsEqual<P.AsString>>, EPEmployee>.View(sender.Graph)).SelectSingle(new object[1]
    {
      e.NewValue
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException(e.Row as IBqlTable, "The status of employee '{0}' is '{1}'.", new object[2]
    {
      (object) typeof (EPEmployee.acctCD),
      (object) typeof (EPEmployee.vStatus)
    }));
  }
}
