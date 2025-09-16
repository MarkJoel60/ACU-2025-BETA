// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectorMode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// The mode of the selector control that is specified in the <see cref="P:PX.Data.PXSelectorAttribute.SelectorMode" /> parameter
/// of the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> attribute.
/// </summary>
[Flags]
public enum PXSelectorMode
{
  /// <summary>An undefined behavior.</summary>
  Undefined = 0,
  /// <summary>
  /// The autocomplete feature is disabled, that is, the suggestions are not shown.
  /// </summary>
  NoAutocomplete = 1,
  /// <summary>
  /// The autocomplete feature is enabled. The suggestions are filtered with the provided mask.
  /// </summary>
  MaskAutocomplete = 32, // 0x00000020
  /// <summary>
  /// The autocomplete feature is enabled. The suggestions are shown for all entered symbols.
  /// </summary>
  AutocompleteMode = MaskAutocomplete | NoAutocomplete, // 0x00000021
  /// <summary>A user cannot enter symbols in the selector box.</summary>
  TextModeReadonly = 2,
  /// <summary>
  /// A user can enter symbols in the selector box.
  /// These symbols will be assigned as the box value (the <see cref="P:PX.Data.PXSelectorAttribute.ValueField" /> property).
  /// </summary>
  TextModeEditable = 4,
  /// <summary>
  /// A user can enter symbols in the selector box.
  /// The entered symbols are used to search for a record by the <see cref="P:PX.Data.PXSelectorAttribute.DescriptionField" /> value.
  /// </summary>
  TextModeSearch = TextModeEditable | TextModeReadonly, // 0x00000006
  TextMode = TextModeSearch, // 0x00000006
  /// <summary>
  /// The value in the selector box is composed as follows: <see cref="P:PX.Data.PXSelectorAttribute.ValueField" /> - <see cref="P:PX.Data.PXSelectorAttribute.DescriptionField" />.
  /// </summary>
  DisplayModeHint = 0,
  /// <summary>
  /// The selector box contains only <see cref="P:PX.Data.PXSelectorAttribute.ValueField" />.
  /// </summary>
  DisplayModeValue = 8,
  /// <summary>
  /// The selector box contains only <see cref="P:PX.Data.PXSelectorAttribute.DescriptionField" />.
  /// </summary>
  DisplayModeText = 16, // 0x00000010
  /// <summary>
  /// The selector box contains only <see cref="P:PX.Data.PXSelectorAttribute.DescriptionField" />.
  /// Use <see cref="F:PX.Data.PXSelectorMode.DisplayModeText" /> instead.
  /// </summary>
  DisplayMode = DisplayModeText | DisplayModeValue, // 0x00000018
}
