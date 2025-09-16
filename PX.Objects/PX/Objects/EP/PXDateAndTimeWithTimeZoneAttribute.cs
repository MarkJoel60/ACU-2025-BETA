// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXDateAndTimeWithTimeZoneAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Date and Time are displayed and modified depending on the TimeZone.
/// </summary>
public class PXDateAndTimeWithTimeZoneAttribute : PXDateAndTimeAttribute
{
  private readonly Type _dateType;
  private readonly Type _timeZoneType;
  private string _displayNameDate;
  private string _displayNameTime;

  /// <summary>Gets or sets the display name for the input control that represents date.</summary>
  public string DisplayNameDate
  {
    get => this._displayNameDate;
    set => this._displayNameDate = value;
  }

  /// <summary>Gets or sets the display name for the input control that represents time.</summary>
  public string DisplayNameTime
  {
    get => this._displayNameTime;
    set => this._displayNameTime = value;
  }

  public PXDateAndTimeWithTimeZoneAttribute(Type dateType, Type timeZoneType)
  {
    this._dateType = dateType;
    this._timeZoneType = timeZoneType;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((PXDateAttribute) this).FieldSelecting(sender, e);
    Type bqlField1 = sender.Graph.Caches[sender.GetItemType()].GetBqlField(this._dateType.Name);
    object date = sender.GetValue(e.Row, bqlField1.Name);
    Type bqlField2 = sender.Graph.Caches[sender.GetItemType()].GetBqlField(this._timeZoneType.Name);
    object obj = sender.GetValue(e.Row, bqlField2.Name);
    if (date == null)
      return;
    if (obj == null)
      e.ReturnValue = date;
    else
      e.ReturnValue = (object) PXDateAndTimeWithTimeZoneAttribute.GetTimeZoneAdjustedActivityDate((DateTime) date, obj.ToString());
  }

  public static DateTime GetTimeZoneAdjustedActivityDate(DateTime date, string timeZoneId)
  {
    DateTime dateTime = date;
    PXTimeZoneInfo systemTimeZoneById = PXTimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    if (systemTimeZoneById != null && systemTimeZoneById.Id != timeZone.Id)
      dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone), systemTimeZoneById);
    return dateTime;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.RemoveHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName + "_Date", new PXFieldSelecting((object) this, __methodptr(Date_FieldSelecting)));
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.RemoveHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName + "_Time", new PXFieldSelecting((object) this, __methodptr(Time_FieldSelecting)));
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName + "_Date", new PXFieldSelecting((object) this, __methodptr(Date_DisplayName)));
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName + "_Time", new PXFieldSelecting((object) this, __methodptr(Time_DisplayName)));
  }

  public void Date_DisplayName(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this.Date_FieldSelecting(sender, e);
    PXFieldState returnState = (PXFieldState) e.ReturnState;
    if (returnState == null)
      return;
    foreach (PXDateAndTimeWithTimeZoneAttribute timeZoneAttribute in sender.GetAttributes(e.Row, ((PXEventSubscriberAttribute) this)._FieldName).OfType<PXDateAndTimeWithTimeZoneAttribute>())
    {
      if (timeZoneAttribute != null && timeZoneAttribute._displayNameDate != null)
        returnState.DisplayName = PXLocalizer.Localize(timeZoneAttribute._displayNameDate, sender.BqlTable.FullName);
    }
    e.ReturnState = (object) returnState;
  }

  public void Time_DisplayName(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this.Time_FieldSelecting(sender, e);
    PXFieldState returnState = (PXFieldState) e.ReturnState;
    if (returnState == null)
      return;
    foreach (PXDateAndTimeWithTimeZoneAttribute timeZoneAttribute in sender.GetAttributes(e.Row, ((PXEventSubscriberAttribute) this)._FieldName).OfType<PXDateAndTimeWithTimeZoneAttribute>())
    {
      if (timeZoneAttribute != null && timeZoneAttribute._displayNameTime != null)
        returnState.DisplayName = PXLocalizer.Localize(timeZoneAttribute._displayNameTime, sender.BqlTable.FullName);
    }
    e.ReturnState = (object) returnState;
  }
}
