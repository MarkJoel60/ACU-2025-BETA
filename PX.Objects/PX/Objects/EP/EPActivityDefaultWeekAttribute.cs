// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityDefaultWeekAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPActivityDefaultWeekAttribute : PXDefaultAttribute
{
  private readonly System.Type dateField;

  public EPActivityDefaultWeekAttribute(System.Type _dateField)
  {
    this.dateField = _dateField;
    this.PersistingCheck = (PXPersistingCheck) 2;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXEventSubscriberAttribute) this).CacheAttached(sender);
    if (!typeof (PMTimeActivity).IsAssignableFrom(sender.GetItemType()) && !typeof (CRPMTimeActivity).IsAssignableFrom(sender.GetItemType()))
      throw new PXArgumentException("Attribute '{0}' can be used only with DAC '{1}' or its inheritors", new object[2]
      {
        (object) ((object) this).GetType().Name,
        (object) typeof (PMTimeActivity).Name
      });
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    System.Type bqlField = sender.Graph.Caches[sender.GetItemType()].GetBqlField(this.dateField.Name);
    object date = sender.GetValue(e.Row, bqlField.Name);
    if (e.Row != null && date != null)
    {
      if (PXWeekSelector2Attribute.IsCustomWeek(sender.Graph))
      {
        try
        {
          e.NewValue = (object) PXWeekSelector2Attribute.GetWeekID(sender.Graph, (DateTime) date);
        }
        catch (PXException ex)
        {
          sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this).FieldName, e.Row, (object) true, (Exception) ex);
        }
      }
      else
        e.NewValue = (object) PXWeekSelector2Attribute.GetWeekID(sender.Graph, (DateTime) date);
    }
    else
      e.NewValue = (object) null;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    System.Type bqlField = sender.Graph.Caches[sender.GetItemType()].GetBqlField(this.dateField.Name);
    object date = sender.GetValue(e.Row, bqlField.Name);
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName);
    if (e.Row != null && date != null && obj == null && PXWeekSelector2Attribute.IsCustomWeek(sender.Graph))
    {
      if (e.Operation != 3)
      {
        try
        {
          PXWeekSelector2Attribute.GetWeekID(sender.Graph, (DateTime) date);
        }
        catch (PXException ex)
        {
          sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this).FieldName, e.Row, (object) true, (Exception) ex);
        }
      }
    }
    base.RowPersisting(sender, e);
  }
}
