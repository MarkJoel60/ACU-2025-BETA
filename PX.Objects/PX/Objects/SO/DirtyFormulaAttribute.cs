// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DirtyFormulaAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class DirtyFormulaAttribute : 
  PXAggregateAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber
{
  protected Dictionary<object, object> inserted;
  protected Dictionary<object, object> updated;

  private PXFormulaAttribute FormulaAttribute => (PXFormulaAttribute) this._Attributes[0];

  public bool ValidateAggregateCalculation
  {
    get => this.FormulaAttribute.ValidateAggregateCalculation;
    set => this.FormulaAttribute.ValidateAggregateCalculation = value;
  }

  public bool SkipZeroUpdates
  {
    get => this.FormulaAttribute.SkipZeroUpdates;
    set => this.FormulaAttribute.SkipZeroUpdates = value;
  }

  public DirtyFormulaAttribute(Type formulaType, Type aggregateType)
    : this(formulaType, aggregateType, false)
  {
  }

  public DirtyFormulaAttribute(Type formulaType, Type aggregateType, bool IsUnbound)
  {
    this._Attributes.Add(IsUnbound ? (PXEventSubscriberAttribute) (object) new PXUnboundFormulaAttribute(formulaType, aggregateType) : (PXEventSubscriberAttribute) (object) new PXFormulaAttribute(formulaType, aggregateType));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this.inserted.TryGetValue(e.Row, out object _))
      return;
    this.inserted[e.Row] = sender.CreateCopy(e.Row);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (this.updated.TryGetValue(e.Row, out object _))
      return;
    this.updated[e.Row] = sender.CreateCopy(e.Row);
  }

  public static void RaiseRowInserted<Field>(PXCache sender, PXRowInsertedEventArgs e) where Field : IBqlField
  {
    foreach (DirtyFormulaAttribute formulaAttribute in sender.GetAttributes<Field>(e.Row).OfType<DirtyFormulaAttribute>())
    {
      object obj;
      if (formulaAttribute.inserted.TryGetValue(e.Row, out obj))
      {
        List<IPXRowUpdatedSubscriber> updatedSubscriberList = new List<IPXRowUpdatedSubscriber>();
        ((PXEventSubscriberAttribute) formulaAttribute).GetSubscriber<IPXRowUpdatedSubscriber>(updatedSubscriberList);
        foreach (IPXRowUpdatedSubscriber updatedSubscriber in updatedSubscriberList)
          updatedSubscriber.RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
        formulaAttribute.inserted.Remove(e.Row);
      }
    }
  }

  public static void RaiseRowUpdated<Field>(PXCache sender, PXRowUpdatedEventArgs e) where Field : IBqlField
  {
    foreach (DirtyFormulaAttribute formulaAttribute in sender.GetAttributes<Field>(e.Row).OfType<DirtyFormulaAttribute>())
    {
      object obj;
      if (formulaAttribute.updated.TryGetValue(e.Row, out obj))
      {
        List<IPXRowUpdatedSubscriber> updatedSubscriberList = new List<IPXRowUpdatedSubscriber>();
        ((PXEventSubscriberAttribute) formulaAttribute).GetSubscriber<IPXRowUpdatedSubscriber>(updatedSubscriberList);
        foreach (IPXRowUpdatedSubscriber updatedSubscriber in updatedSubscriberList)
          updatedSubscriber.RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
        formulaAttribute.updated.Remove(e.Row);
      }
    }
  }
}
