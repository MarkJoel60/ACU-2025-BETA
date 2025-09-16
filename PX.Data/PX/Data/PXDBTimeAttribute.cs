// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>DateTime?</tt> type to the database
/// column of <tt>smalldatetime</tt> type. The field value holds only time
/// without date.</summary>
/// <remarks>
///   <para>The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name. </para>
///   <para>The field values keep only time without date. On the user interface, the field is represented by a control allowing a user to enter only a time value.</para>
///   <para>The attribute inherits properties of the PXDBDate attribute.</para>
/// </remarks>
/// <example><para>The code below binds the &lt;tt&gt;SunStartTime&lt;/tt&gt; DAC field to the database column with the same name and sets the default value for the field. Notice the setting of the &lt;tt&gt;DisplayMask&lt;/tt&gt; property inherited from the %PXDBDate:PX.Data.PXDBDateAttribute% attribute.</para>
///   <code title="Example" lang="CS">
/// [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
/// [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
/// public virtual DateTime? SunStartTime { ... }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBTimeAttribute : PXDBDateAttribute
{
  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBTimeAttribute() => base.PreserveTime = true;

  /// <summary>Gets the value that indicates whether the time part of a
  /// field value is preserved. Since the constructor sets this value to
  /// <tt>true</tt>, this property always returns <tt>true</tt>.</summary>
  public override bool PreserveTime
  {
    get => base.PreserveTime;
    set
    {
    }
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    if (e.DataValue == null)
      return;
    e.DataValue = (object) PXDBDateAttribute._MIN_VALUE.AddTicks(((System.DateTime) e.DataValue).TimeOfDay.Ticks);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    base.RowSelecting(sender, e);
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj == null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) PXDBDateAttribute._MIN_VALUE.AddTicks(((System.DateTime) obj).TimeOfDay.Ticks));
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    base.FieldUpdating(sender, e);
    if (e.NewValue == null)
      return;
    e.NewValue = (object) PXDBDateAttribute._MIN_VALUE.AddTicks(((System.DateTime) e.NewValue).TimeOfDay.Ticks);
  }
}
