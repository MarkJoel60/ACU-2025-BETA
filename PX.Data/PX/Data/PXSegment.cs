// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSegment
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of a single segment of a segmented field input control.</summary>
public class PXSegment
{
  /// <summary>The input mask for the segment:
  /// <list type="bullet"><item><description>C: <tt>MaskType.Ascii</tt></description></item>
  /// <item><description>a: <tt>MaskType.AlphaNumeric</tt></description></item>
  /// <item><description>9: <tt>MaskType.Numeric</tt></description></item>
  /// <item><description>?: <tt>MaskType.Alpha</tt></description></item></list></summary>
  public readonly char EditMask;
  /// <summary>The character to fill the value.</summary>
  public readonly char FillCharacter;
  /// <summary>The character to prompt for a value.</summary>
  public readonly char PromptCharacter;
  /// <summary>The number of characters in the segment.</summary>
  public readonly short Length;
  /// <summary>The value that indicates (if set to <see langword="true" />) that the new specified segment value should be validated.</summary>
  public readonly bool Validate;
  /// <summary>The value that specifies whether the letters in the segment are converted to uppercase or lowercase:
  /// <list type="bullet"><item><description>0: <tt>NotSet</tt></description></item>
  /// <item><description>1: <tt>Upper</tt></description></item>
  /// <item><description>2: <tt>Lower</tt></description></item></list></summary>
  public readonly short CaseConvert;
  /// <summary>The text alignment type in the segment:
  /// <list type="bullet"><item><description>1: <tt>Left</tt></description></item>
  /// <item><description>2: <tt>Right</tt></description></item></list></summary>
  public readonly short Align;
  /// <summary>The character that is used to separate the segment from the previous one.</summary>
  public readonly char Separator;
  /// <summary>The value that indicates (if set to <see langword="true" />) that the contents of the segment cannot be changed.</summary>
  public readonly bool ReadOnly;
  /// <summary>The description of the segment.</summary>
  public readonly string Descr;

  /// <summary>Creates an instance of the <tt>PXSegment</tt> class using the provided values.</summary>
  /// <param name="editMask">The input mask for the segment:
  /// <list type="bullet"><item><description>C: <tt>MaskType.Ascii</tt></description></item>
  /// <item><description>a: <tt>MaskType.AlphaNumeric</tt></description></item>
  /// <item><description>9: <tt>MaskType.Numeric</tt></description></item>
  /// <item><description>?: <tt>MaskType.Alpha</tt></description></item></list></param>
  /// <param name="fillCharacter">The character to fill the value.</param>
  /// <param name="length">The number of characters in the segment.</param>
  /// <param name="validate">The value that indicates (if set to <see langword="true" />) that the new specified segment value should be validated.</param>
  /// <param name="caseConverter">The value that specifies whether the letters in the segment are converted to uppercase or lowercase:
  /// <list type="bullet"><item><description>0: <tt>NotSet</tt></description></item>
  /// <item><description>1: <tt>Upper</tt></description></item>
  /// <item><description>2: <tt>Lower</tt></description></item></list></param>
  /// <param name="align">The text alignment type in the segment:
  /// <list type="bullet"><item><description>1: <tt>Left</tt></description></item>
  /// <item><description>2: <tt>Right</tt></description></item></list></param>
  /// <param name="separator">The character that is used to separate the segment from the previous one.</param>
  /// <param name="readOnly">The value that indicates (if set to <see langword="true" />) that the contents of the segment cannot be changed.</param>
  /// <param name="descr">The description of the segment.</param>
  /// <param name="promptCharacter">The character to prompt for a value.</param>
  public PXSegment(
    char editMask,
    char fillCharacter,
    short length,
    bool validate,
    short caseConverter,
    short align,
    char separator,
    bool readOnly,
    string descr,
    char promptCharacter = '_')
  {
    this.EditMask = editMask;
    this.FillCharacter = fillCharacter;
    this.PromptCharacter = promptCharacter;
    this.Length = length;
    this.Validate = validate;
    this.CaseConvert = caseConverter;
    this.Align = align;
    this.Separator = separator;
    this.ReadOnly = readOnly;
    this.Descr = descr;
  }
}
