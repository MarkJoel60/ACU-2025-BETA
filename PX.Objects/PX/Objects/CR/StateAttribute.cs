// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.StateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

public class StateAttribute : CountryStateSelectorAttribute, IPXRowPersistingSubscriber
{
  protected readonly System.Type CountryID;

  protected override string Type => "States";

  public StateAttribute(System.Type aCountryID)
    : base(BqlCommand.Compose(new System.Type[7]
    {
      typeof (Search<,>),
      typeof (PX.Objects.CS.State.stateID),
      typeof (Where<,>),
      typeof (PX.Objects.CS.State.countryID),
      typeof (Equal<>),
      typeof (Optional<>),
      aCountryID
    }))
  {
    this.CountryID = aCountryID;
    this.Filterable = true;
    this._UnconditionalSelect = this._PrimarySelect;
    this.DescriptionField = typeof (PX.Objects.CS.State.name);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (sender.Graph.IsContractBasedAPI)
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    System.Type declaringType = this.CountryID.DeclaringType;
    string name = this.CountryID.Name;
    StateAttribute stateAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) stateAttribute, __vmethodptr(stateAttribute, CountryChanged));
    fieldUpdated.AddHandler(declaringType, name, pxFieldUpdated);
  }

  protected virtual void CountryChanged(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (obj == null)
      return;
    try
    {
      sender.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
    }
    catch (Exception ex)
    {
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, obj, ex);
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || sender.Graph.IsContractBasedAPI || sender.Graph.IsCopyPasteContext)
      return;
    if (!(sender.GetValue(e.Row, this.CountryID.Name) is string countryID))
    {
      e.NewValue = (object) null;
    }
    else
    {
      try
      {
        e.NewValue = (object) this.ValidateStateByCountry(e.NewValue as string, countryID);
        ((CancelEventArgs) e).Cancel = true;
      }
      catch (LocalizationPreparedException ex)
      {
        throw new PXSetPropertyException(ex.Format, ex.Args);
      }
      finally
      {
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewValue, (Exception) null);
      }
    }
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!sender.Graph.IsContractBasedAPI)
      return;
    string countryID = sender.GetValue(e.Row, this.CountryID.Name) as string;
    string state = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal) as string;
    try
    {
      string str = this.ValidateStateByCountry(state, countryID);
      if (!(str != state))
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal, (object) str);
    }
    catch (LocalizationPreparedException ex)
    {
      throw new PXRowPersistingException(((PXEventSubscriberAttribute) this).FieldName, (object) state, ex.Format, ex.Args);
    }
  }
}
