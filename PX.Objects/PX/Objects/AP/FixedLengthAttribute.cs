// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.FixedLengthAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Attribute to create Fixed Width file, works in conjuction with FixedLengthFile class. Symbols are in uppercase by default (AlphaCharacterCaseStyle is Upper).
/// <example>
/// [FixedLength(StartPosition = 21, FieldLength = 4, RegexReplacePattern = @"[^0-9a-zA-Z]")]
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class FixedLengthAttribute : Attribute
{
  private int _FieldLength;
  private int _StartPosition;
  private PaddingEnum _PaddingStyle = PaddingEnum.Right;
  private AlphaCharacterCaseEnum _AlphaCharacterCaseStyle = AlphaCharacterCaseEnum.Upper;
  private char _PaddingChar = ' ';
  private string _RegexReplacePattern = string.Empty;

  public int FieldLength
  {
    get => this._FieldLength;
    set => this._FieldLength = value;
  }

  public int StartPosition
  {
    get => this._StartPosition;
    set => this._StartPosition = value;
  }

  public PaddingEnum PaddingStyle
  {
    get => this._PaddingStyle;
    set => this._PaddingStyle = value;
  }

  public AlphaCharacterCaseEnum AlphaCharacterCaseStyle
  {
    get => this._AlphaCharacterCaseStyle;
    set => this._AlphaCharacterCaseStyle = value;
  }

  public char PaddingChar
  {
    get => this._PaddingChar;
    set => this._PaddingChar = value;
  }

  public string RegexReplacePattern
  {
    get => this._RegexReplacePattern;
    set => this._RegexReplacePattern = value;
  }
}
