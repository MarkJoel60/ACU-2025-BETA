// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDecimalListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Sets a dropdown list as the input control for a DAC field of
/// <tt>decimal</tt> type.</summary>
/// <remarks>
/// <para>The user will be able to select a value from the predefined
/// values list. Values are specified in the constructor as strings,
/// because the attribute derives from <tt>PXStringList</tt>. The
/// attribute converts a selected value to the <tt>decimal</tt> type that
/// is assigned to the field.</para>
/// <para>The DAC field data type must be defined using the <see cref="T:PX.Data.PXDBDecimalAttribute">PXDBDecimalString</see>
/// attribute.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDecimalList(
///     new string[] { "0.1", "0.5", "1.0", "10", "100" },
///     new string[] { "0.1", "0.5", "1.0", "10", "100" })]
/// public virtual decimal? InvoicePrecision { get; set; }
/// </code>
/// </example>
public class PXDecimalListAttribute : PXStringListAttribute
{
  /// <summary>
  /// Initializes a new instance with the provided lists of allowed values
  /// and labels. When a user selects a label in the user interface, the
  /// corresponding value is converted to <tt>decimal</tt> type and assigned to the
  /// field marked by the instance. The two lists must be of the same length.
  /// </summary>
  /// <param name="allowedValues">The array of string values the user will be able
  /// to select from. A string value is converted by the attribute to the decimal
  /// value.</param>
  /// <param name="allowedLabels">The array of labels corresponding to values and
  /// displayed in the user interface.</param>
  public PXDecimalListAttribute(string[] allowedValues, string[] allowedLabels)
    : base(allowedValues, allowedLabels)
  {
    this.IsLocalizable = false;
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string[] allowedValues = Array.ConvertAll<string, string>(this._AllowedValues, (Converter<string, string>) (a => Decimal.Parse(a, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture).ToString("F2", (IFormatProvider) sender.Graph.Culture)));
    string[] allowedLabels = Array.ConvertAll<string, string>(this._AllowedLabels, (Converter<string, string>) (a => Decimal.Parse(a, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture).ToString("F2", (IFormatProvider) sender.Graph.Culture)));
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(-1), (string) null, allowedValues, allowedLabels, new bool?(this._ExclusiveValues), (string) null);
  }
}
