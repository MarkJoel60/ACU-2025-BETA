// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>A DAC descriptor creation options.</summary>
public class DacDescriptorCreationOptions
{
  public const string DefaultNameValueSeparatorInField = ": ";
  public const string DefaultFieldsSeparator = ", ";
  public const string DefaultDacTypeWithFieldValuesSeparator = ": ";

  /// <summary>
  /// Gets the default DAC descriptor generation settings. The default settings are:
  /// <list type="bullet">
  /// <item>
  /// Default separator values <see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultFieldsSeparator" />, <see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultNameValueSeparatorInField" />,
  /// <br /><see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultDacTypeWithFieldValuesSeparator" />;</item>
  /// <item><see cref="F:PX.Data.DacDescriptorGeneration.DacFieldNamesInDacDescriptorStyle.AllExceptKeys" /> style for names of DAC fields;</item>
  /// <item><see cref="F:PX.Data.DacDescriptorGeneration.NullOrEmptyDacFieldValuesStyle.UseFieldAttributes" /> style for null or empty DAC fields values;</item>
  /// <item><see cref="F:PX.Data.DacDescriptorGeneration.DacKeysInDacDescriptorStyle.AlwaysInclude" /> style for DAC key fields;</item>
  /// </list>
  /// </summary>
  /// <value>The default DAC descriptor generation settings.</value>
  public static DacDescriptorCreationOptions Default { get; } = new DacDescriptorCreationOptions(DacFieldNamesInDacDescriptorStyle.All, NullOrEmptyDacFieldValuesStyle.UseFieldAttributes, DacKeysInDacDescriptorStyle.AlwaysInclude, DacTypeNameInDacDescriptorStyle.FullTypeName);

  /// <summary>
  /// Gets the style of using DAC field names in the DAC descriptor.
  /// </summary>
  /// <value>The style of using DAC field names in the DAC descriptor.</value>
  public DacFieldNamesInDacDescriptorStyle FieldNamesInDacDescriptorStyle { get; }

  /// <summary>
  /// Gets the style of adding null, empty or whitespace DAC field values to the DAC descriptor.
  /// </summary>
  /// <value>
  /// The style of adding null, empty or whitespace DAC field values to the DAC descriptor.
  /// </value>
  public NullOrEmptyDacFieldValuesStyle NullOrEmptyValuesStyle { get; }

  /// <summary>
  /// Gets the style of adding DAC key fields in the DAC descriptor.
  /// </summary>
  /// <value>The style of adding DAC key fields in the DAC descriptor.</value>
  public DacKeysInDacDescriptorStyle KeysInDescriptorStyle { get; }

  /// <summary>
  /// Gets the style of a name of the DAC type in the DAC descriptor.
  /// </summary>
  /// <value>
  /// The style of a name of the DAC type in the DAC descriptor.
  /// </value>
  public DacTypeNameInDacDescriptorStyle DacTypeNameStyle { get; }

  /// <summary>
  /// Gets the DAC fields separator. <see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultFieldsSeparator" /> is used by default.
  /// </summary>
  /// <value>The DAC fields separator.</value>
  public string FieldsSeparator { get; }

  /// <summary>
  /// Gets the separator between DAC field's name and value in the DAC descriptor. <see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultNameValueSeparatorInField" /> is used by default.
  /// </summary>
  /// <value>
  /// The separator between DAC field's name and value in the DAC descriptor.
  /// </value>
  public string NameValueSeparatorInField { get; }

  /// <summary>
  /// Gets the separator between DAC type name and DAC fields' values. <see cref="F:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.DefaultDacTypeWithFieldValuesSeparator" /> is used by default.
  /// </summary>
  /// <value>
  /// The separator between DAC type name and DAC fields' values.
  /// </value>
  public string DacTypeWithFieldValuesSeparator { get; }

  public DacDescriptorCreationOptions(
    DacFieldNamesInDacDescriptorStyle fieldNamesInDacDescriptorStyle,
    NullOrEmptyDacFieldValuesStyle nullOrEmptyValuesStyle,
    DacKeysInDacDescriptorStyle keysInDacDescriptorStyle,
    DacTypeNameInDacDescriptorStyle dacTypeNameStyle,
    string? fieldsSeparator = null,
    string? nameValueSeparatorInField = null,
    string? dacTypeWithFieldValuesSeparator = null)
  {
    this.FieldNamesInDacDescriptorStyle = fieldNamesInDacDescriptorStyle;
    this.NullOrEmptyValuesStyle = nullOrEmptyValuesStyle;
    this.KeysInDescriptorStyle = keysInDacDescriptorStyle;
    this.DacTypeNameStyle = dacTypeNameStyle;
    this.FieldsSeparator = fieldsSeparator ?? ", ";
    this.NameValueSeparatorInField = nameValueSeparatorInField ?? ": ";
    this.DacTypeWithFieldValuesSeparator = dacTypeWithFieldValuesSeparator ?? ": ";
  }

  public DacDescriptorCreationOptions With(
    DacFieldNamesInDacDescriptorStyle? fieldNamesInDacDescriptorStyle = null,
    NullOrEmptyDacFieldValuesStyle? nullOrEmptyValuesStyle = null,
    DacKeysInDacDescriptorStyle? keysInDacDescriptorStyle = null,
    DacTypeNameInDacDescriptorStyle? dacTypeNameStyle = null,
    string? fieldsSeparator = null,
    string? nameValueSeparatorInField = null,
    string? dacTypeWithFieldValuesSeparator = null)
  {
    return !fieldNamesInDacDescriptorStyle.HasValue && !nullOrEmptyValuesStyle.HasValue && !keysInDacDescriptorStyle.HasValue && !dacTypeNameStyle.HasValue && fieldsSeparator == null && nameValueSeparatorInField == null && dacTypeWithFieldValuesSeparator == null ? this : new DacDescriptorCreationOptions((DacFieldNamesInDacDescriptorStyle) ((int) fieldNamesInDacDescriptorStyle ?? (int) this.FieldNamesInDacDescriptorStyle), (NullOrEmptyDacFieldValuesStyle) ((int) nullOrEmptyValuesStyle ?? (int) this.NullOrEmptyValuesStyle), (DacKeysInDacDescriptorStyle) ((int) keysInDacDescriptorStyle ?? (int) this.KeysInDescriptorStyle), (DacTypeNameInDacDescriptorStyle) ((int) dacTypeNameStyle ?? (int) this.DacTypeNameStyle), fieldsSeparator ?? this.FieldsSeparator, nameValueSeparatorInField ?? this.NameValueSeparatorInField, dacTypeWithFieldValuesSeparator ?? this.DacTypeWithFieldValuesSeparator);
  }
}
