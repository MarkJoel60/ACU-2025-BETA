// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportConstants
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CS;

public class RMReportConstants
{
  public const char DefaultWildcardChar = '_';
  public static readonly char[] WildcardChars = new char[2]
  {
    '?',
    ' '
  };
  public const char RangeIntersectionChar = '|';
  public const char RangeUnionChar = ',';
  public const char RangeDelimiterChar = ':';
  public const char NonExpandingWildcardChar = '*';

  public enum WildcardMode
  {
    Normal,
    Fixed,
  }

  public enum BetweenMode
  {
    ByChar,
    Fixed,
  }
}
