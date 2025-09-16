// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDimensionWildcardAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Sets up the selector control for a DAC field that holds a
/// segmented key value and allows the <tt>?</tt> wildcard character. The
/// attribute combines the <see cref="T:PX.Data.PXDimensionAttribute" /> and <see cref="T:PX.Data.PXSelectorAttribute" />
/// attributes.</summary>
public class PXDimensionWildcardAttribute : PXAggregateAttribute, IPXFieldSelectingSubscriber
{
  /// <summary>The field from the referenced table that contains the description.</summary>
  public virtual System.Type DescriptionField
  {
    get => ((PXSelectorAttribute) this._Attributes[this._Attributes.Count - 1]).DescriptionField;
    set
    {
      ((PXSelectorAttribute) this._Attributes[this._Attributes.Count - 1]).DescriptionField = value;
    }
  }

  /// <summary>The wildcard string that matches any symbol in the segment.</summary>
  public virtual string Wildcard
  {
    get => ((PXDimensionAttribute) this._Attributes[this._Attributes.Count - 2]).Wildcard;
    set => ((PXDimensionAttribute) this._Attributes[this._Attributes.Count - 2]).Wildcard = value;
  }

  /// <summary>The list of labels for column headers that are displayed in the selector control.
  /// By default, the attribute uses display names of the fields.</summary>
  public virtual string[] Headers
  {
    get => ((PXSelectorAttribute) this._Attributes[this._Attributes.Count - 1]).Headers;
    set => ((PXSelectorAttribute) this._Attributes[this._Attributes.Count - 1]).Headers = value;
  }

  /// <summary>Creates a selector control.</summary>
  /// <param name="type">The referenced table, which should be either <see cref="T:PX.Data.IBqlField" /> or <see cref="T:PX.Data.IBqlSearch" />.</param>
  public PXDimensionWildcardAttribute(string dimension, System.Type type)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(dimension)
    {
      Wildcard = "?"
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(type));
  }

  /// <summary>Creates a selector control with overriding of the columns displayed in the lookup table of the control.</summary>
  /// <param name="type">The referenced table, which should be either <see cref="T:PX.Data.IBqlField" /> or <see cref="T:PX.Data.IBqlSearch" />.</param>
  /// <param name="fieldList">Fields to display in the lookup table of the selector control.</param>
  public PXDimensionWildcardAttribute(string dimension, System.Type type, params System.Type[] fieldList)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(dimension)
    {
      Wildcard = "?"
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(type, fieldList));
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!e.IsAltered)
      e.IsAltered = sender.HasAttributes(e.Row);
    new PXFieldSelecting(((PXDimensionAttribute) this._Attributes[this._Attributes.Count - 2]).FieldSelecting)(sender, e);
    if (!((PXDimensionAttribute) this._Attributes[this._Attributes.Count - 2]).ValidComboRequired)
      return;
    new PXFieldSelecting(((PXSelectorAttribute) this._Attributes[this._Attributes.Count - 1]).FieldSelecting)(sender, e);
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      subscribers.Remove(this._Attributes[this._Attributes.Count - 1] as ISubscriber);
    else if (typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber))
    {
      subscribers.Remove(this._Attributes[this._Attributes.Count - 2] as ISubscriber);
      subscribers.Remove(this._Attributes[this._Attributes.Count - 1] as ISubscriber);
    }
    else if (typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber))
      subscribers.Remove(this._Attributes[this._Attributes.Count - 2] as ISubscriber);
    else if (typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
    {
      subscribers.Remove(this._Attributes[this._Attributes.Count - 2] as ISubscriber);
    }
    else
    {
      if (!(typeof (ISubscriber) == typeof (IPXRowPersistedSubscriber)))
        return;
      subscribers.Remove(this._Attributes[this._Attributes.Count - 2] as ISubscriber);
    }
  }
}
