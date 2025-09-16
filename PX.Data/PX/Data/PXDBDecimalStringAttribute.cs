// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDecimalStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>decimal?</tt> type to the database
/// column of <tt>decimal</tt> type. The mapped DAC field can be
/// represented in the UI by a dropdown list using the <see cref="T:PX.Data.PXDecimalListAttribute">PXDecimalList</see>
/// attribute.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>In the UI, the field can be represented by a drop-down list with
/// specific values. The UI control is configured using the
/// <tt>PXDecimalList</tt> attribute.</para>
/// </remarks>
/// <example>
/// <code>
/// // A mapping of the DAC field to the database column
/// [PXDBDecimalString(1)]
/// // UI control configuration.
/// // The first list configures values assigned to the field,
/// // the second one configures displayed labels.
/// [PXDecimalList(new string[] { "0.1", "0.5", "1.0", "10", "100" },
///                new string[] { "0.1", "0.5", "1.0", "10", "100" })]
/// [PXDefault(TypeCode.Decimal, "0.1")]
/// [PXUIField(DisplayName = "Invoice Amount Precision")]
/// public virtual decimal? InvoicePrecision { get; set; }
/// </code>
/// </example>
public class PXDBDecimalStringAttribute : PXDBDecimalAttribute
{
  /// <summary>Initializes a new instance with the default precision, which
  /// equals 2.</summary>
  public PXDBDecimalStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given decimal value
  /// precision.</summary>
  public PXDBDecimalStringAttribute(int precision)
    : base(precision)
  {
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(10), new bool?(false), this._FieldName, new bool?(this._IsKey), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    if (e.ReturnValue == null || !(e.ReturnValue is Decimal))
      return;
    e.ReturnValue = (object) ((Decimal) e.ReturnValue).ToString("0.00", (IFormatProvider) sender.Graph.Culture);
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || !(e.NewValue is string))
      return;
    Decimal result;
    if (Decimal.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }
}
