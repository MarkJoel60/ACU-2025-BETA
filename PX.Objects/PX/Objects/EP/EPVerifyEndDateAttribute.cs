// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPVerifyEndDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPVerifyEndDateAttribute(Type startDateField) : 
  PXVerifyEndDateAttribute(startDateField),
  IPXFieldVerifyingSubscriber,
  IPXRowInsertedSubscriber
{
  void IPXRowInsertedSubscriber.RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    DateTime? nullable1 = (DateTime?) sender.GetValue(e.Row, this._startDateField);
    DateTime? nullable2 = (DateTime?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    try
    {
      base.Verifying(sender, e.Row, nullable1, nullable2, this._startDateField, nullable2);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) nullable2?.ToShortDateString(), (Exception) ex);
    }
  }

  protected virtual void Verifying(
    PXCache sender,
    object row,
    DateTime? startDateTime,
    DateTime? endDateTime,
    string fieldName,
    DateTime? newValue)
  {
    if (!startDateTime.HasValue || !endDateTime.HasValue)
      return;
    DateTime? date1 = startDateTime?.Date;
    DateTime? date2 = endDateTime?.Date;
    if ((date1.HasValue & date2.HasValue ? (date1.GetValueOrDefault() > date2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    if (this.AllowAutoChange)
    {
      sender.SetValueExt(row, fieldName, (object) newValue);
      date2 = newValue?.Date;
      DateTime date3 = PXTimeZoneInfo.Now.Date;
      if ((date2.HasValue ? (date2.GetValueOrDefault() < date3 ? 1 : 0) : 0) != 0 || !this.AutoChangeWarning)
        return;
      sender.RaiseExceptionHandling(fieldName, row, (object) endDateTime, (Exception) new PXSetPropertyException("'{0}' was changed automatically to {1}.", (PXErrorLevel) 2, new object[2]
      {
        (object) $"[{fieldName}]",
        (object) newValue?.ToShortDateString()
      }));
    }
    else
    {
      if (fieldName == ((PXEventSubscriberAttribute) this)._FieldName)
        throw new PXSetPropertyException("{0} must be less than or equal to {1} '{2:d}'.", new object[3]
        {
          (object) PXUIFieldAttribute.GetDisplayName(sender, this._startDateField),
          (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]",
          (object) endDateTime?.ToShortDateString()
        });
      throw new PXSetPropertyException("{0} must be greater than or equal to {1} '{2:d}'.", new object[3]
      {
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]",
        (object) PXUIFieldAttribute.GetDisplayName(sender, this._startDateField),
        (object) startDateTime?.ToShortDateString()
      });
    }
  }
}
