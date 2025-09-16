// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXRemindDateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.EP;

/// <exclude />
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PXRemindDateAttribute : 
  PXDBDateAndTimeAttribute,
  IPXRowUpdatedSubscriber,
  IPXFieldDefaultingSubscriber
{
  public const double _MINUTES_IN_ONE_HOUR = 60.0;
  public const double _MINUTES_IN_HALF_DAY = 720.0;
  public const double _MINUTES_IN_ONE_DAY = 1440.0;
  private const double _DEFAULT_REMIND_AT = 15.0;
  private readonly System.Type _isReminderOnBqlField;
  protected int _isReminderOnFieldOrigin;
  private readonly System.Type _startDateBqlField;
  protected int _startDateFieldOrigin;
  protected double _reversedRemindAt;

  public PXRemindDateAttribute(System.Type isReminderOnBqlField, System.Type startDateBqlField)
  {
    this._isReminderOnBqlField = isReminderOnBqlField;
    this._startDateBqlField = startDateBqlField;
    this.RemindAt = 15.0;
    base.WithoutDisplayNames = true;
  }

  public override bool WithoutDisplayNames
  {
    get => base.WithoutDisplayNames;
    set
    {
    }
  }

  public double RemindAt
  {
    get => -1.0 * this._reversedRemindAt;
    set => this._reversedRemindAt = -1.0 * value;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._isReminderOnFieldOrigin = sender.GetFieldOrdinal(sender.GetField(this._isReminderOnBqlField));
    this._startDateFieldOrigin = sender.GetFieldOrdinal(sender.GetField(this._startDateBqlField));
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    object oldRow = e.OldRow;
    object row = e.Row;
    if (object.Equals(sender.GetValue(oldRow, this._isReminderOnFieldOrigin), sender.GetValue(row, this._isReminderOnFieldOrigin)) || !object.Equals(sender.GetValue(oldRow, this._FieldOrdinal), sender.GetValue(row, this._FieldOrdinal)))
      return;
    this.CorrectValue(sender, row);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    this.CorrectValue(sender, e.Row);
  }

  private void CorrectValue(PXCache sender, object row)
  {
    sender.SetValue(row, this._FieldOrdinal, (object) this.CalcCorrectValue(sender, row));
  }

  protected virtual System.DateTime? CalcCorrectValue(PXCache sender, object row)
  {
    System.DateTime? nullable = new System.DateTime?();
    if (sender.GetValue(row, this._isReminderOnFieldOrigin) is true)
      nullable = (System.DateTime?) sender.GetValue(row, this._startDateFieldOrigin);
    return nullable.HasValue ? new System.DateTime?(nullable.Value.AddMinutes(this._reversedRemindAt)) : new System.DateTime?();
  }
}
