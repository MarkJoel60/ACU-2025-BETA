// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.Descriptor.TaxAdjsutmentTaxPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX.Descriptor;

[PXUIField]
[PXDefault(null, typeof (Search2<TaxPeriod.taxPeriodID, InnerJoin<PX.Objects.GL.Branch, On<TaxPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<TaxPeriod.vendorID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.GL.Branch.branchID, Equal<Current<TaxAdjustment.branchID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>>>>>))]
[PXSelector(typeof (Search2<TaxPeriod.taxPeriodID, InnerJoin<PX.Objects.GL.Branch, On<TaxPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<TaxPeriod.vendorID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.GL.Branch.branchID, Equal<Current<TaxAdjustment.branchID>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
[PXRestrictor(typeof (Where<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>, Or<Current<TaxPeriod.status>, IsNull>>), "The tax period status is '{0}'.", new Type[] {typeof (TaxPeriod.status)})]
public class TaxAdjsutmentTaxPeriodSelectorAttribute : 
  FinPeriodIDAttribute,
  IPXFieldVerifyingSubscriber
{
  protected int SelectorAtrributeIndex = -1;
  protected int RestrictorAtrributeIndex = -1;

  public TaxAdjsutmentTaxPeriodSelectorAttribute()
    : base()
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in ((object) this).GetType().GetCustomAttributes(typeof (PXEventSubscriberAttribute), true).Cast<PXEventSubscriberAttribute>().ToArray<PXEventSubscriberAttribute>())
    {
      if (!((List<PXEventSubscriberAttribute>) this._Attributes).Contains(subscriberAttribute))
      {
        this._Attributes.Add(subscriberAttribute);
        switch (subscriberAttribute)
        {
          case PXSelectorAttribute _:
            this.SelectorAtrributeIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
            continue;
          case PXRestrictorAttribute _:
            this.RestrictorAtrributeIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
            continue;
          default:
            continue;
        }
      }
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      ((IPXFieldVerifyingSubscriber) this._Attributes[this.SelectorAtrributeIndex]).FieldVerifying(sender, e);
      ((IPXFieldVerifyingSubscriber) this._Attributes[this.RestrictorAtrributeIndex]).FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw;
    }
  }
}
