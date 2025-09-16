// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVerifyEndDateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Verifies the integrity of a date range. That is, the start date of the range must be less than or equal to the end date.</summary>
/// <remarks>
/// <para>It can be applied to only the DAC-field that defines the end date of the range. This field should be of the <tt>DateTime</tt> type.</para>
/// <para>When a user changes the range incorrectly (sets the start date greater than the end date), the attribute either
/// displays an error message or the start date is set equal to the end date. The behavior depends on the attribute settings.</para>
/// <para>It is assumed that the field of start date is declared in the DAC-class before the field of the end date.
/// Otherwise, the attribute may work incorrectly.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBDate]
/// [PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.SelectorVisible)]
/// [PXVerifyEndDate(typeof(Contract.startDate), AllowAutoChange = false)]
/// public virtual DateTime? ExpireDate { get; set; }
/// </code>
/// </example>
public class PXVerifyEndDateAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertedSubscriber
{
  protected readonly string _startDateField;

  /// <summary>
  /// Gets or sets the flag of the date range autocorrection.
  /// If this flag is set, the start date becomes equal to the end date after it was changed by a user incorrectly.
  /// Otherwise, an error message appears on the changed field.
  /// </summary>
  public bool AllowAutoChange { get; set; }

  /// <summary>
  /// Gets or sets the flag of information about the autocorrection.
  /// If this flag is set, a warning message appears on the changed field.
  /// </summary>
  public bool AutoChangeWarning { get; set; }

  /// <summary>Initializes a new instance of the attribute.</summary>
  /// <param name="startDateField">Type of the start date DAC-field. This field should be of the <tt>DateTime</tt> type
  /// and declared before current (the end date) field in the DAC-class.</param>
  public PXVerifyEndDateAttribute(System.Type startDateField)
  {
    this._startDateField = startDateField.Name;
    this.AllowAutoChange = true;
    this.AutoChangeWarning = false;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldVerifying.AddHandler(sender.GetItemType(), this._startDateField, new PXFieldVerifying(this.StartDateVerifyning));
  }

  void IPXFieldVerifyingSubscriber.FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    System.DateTime? startDate = (System.DateTime?) sender.GetValue(e.Row, this._startDateField);
    System.DateTime? newValue = (System.DateTime?) e.NewValue;
    this.Verifying(sender, e.Row, startDate, newValue, this._startDateField, newValue);
  }

  private void StartDateVerifyning(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    object valuePending = sender.GetValuePending(e.Row, this._FieldName);
    if (valuePending != null && valuePending != PXCache.NotSetValue)
      return;
    System.DateTime? newValue = (System.DateTime?) e.NewValue;
    System.DateTime? endDate = (System.DateTime?) sender.GetValue(e.Row, this._FieldName);
    this.Verifying(sender, e.Row, newValue, endDate, this._FieldName, newValue);
  }

  void IPXRowInsertedSubscriber.RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    System.DateTime? startDate = (System.DateTime?) sender.GetValue(e.Row, this._startDateField);
    System.DateTime? nullable = (System.DateTime?) sender.GetValue(e.Row, this._FieldName);
    try
    {
      this.Verifying(sender, e.Row, startDate, nullable, this._startDateField, nullable);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) nullable, (Exception) ex);
    }
  }

  protected virtual void Verifying(
    PXCache sender,
    object row,
    System.DateTime? startDate,
    System.DateTime? endDate,
    string fieldName,
    System.DateTime? newValue)
  {
    if (!startDate.HasValue || !endDate.HasValue)
      return;
    System.DateTime? nullable1 = startDate;
    System.DateTime? nullable2 = endDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    if (this.AllowAutoChange)
    {
      sender.SetValueExt(row, fieldName, (object) newValue);
      if (!this.AutoChangeWarning)
        return;
      sender.RaiseExceptionHandling(fieldName, row, (object) endDate, (Exception) new PXSetPropertyException("'{0}' was changed automatically to {1}.", PXErrorLevel.Warning, new object[2]
      {
        (object) $"[{fieldName}]",
        (object) newValue
      }));
    }
    else
    {
      if (fieldName == this._FieldName)
        throw new PXSetPropertyException("{0} must be less than or equal to {1} '{2:d}'.", new object[3]
        {
          (object) PXUIFieldAttribute.GetDisplayName(sender, this._startDateField),
          (object) $"[{this._FieldName}]",
          (object) endDate
        });
      throw new PXSetPropertyException("{0} must be greater than or equal to {1} '{2:d}'.", new object[3]
      {
        (object) $"[{this._FieldName}]",
        (object) PXUIFieldAttribute.GetDisplayName(sender, this._startDateField),
        (object) startDate
      });
    }
  }
}
