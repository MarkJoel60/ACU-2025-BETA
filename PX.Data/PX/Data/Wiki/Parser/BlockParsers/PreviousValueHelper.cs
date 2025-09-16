// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.BlockParsers.PreviousValueHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Wiki.Parser.BlockParsers;

[PXInternalUseOnly]
public static class PreviousValueHelper
{
  private const string PreviousFunction = "PREV";
  internal const string PreviousValue = "previousValue";
  private static readonly Token _previousValueToken = new Token("previousValue");

  internal static TokenLexems[] PrevLexems { get; } = new TokenLexems[5]
  {
    new TokenLexems(Token.chars, "PR"),
    new TokenLexems(Token.chars, "PRE"),
    new TokenLexems(Token.chars, "PREV"),
    new TokenLexems(Token.chars, "PREV("),
    new TokenLexems(PreviousValueHelper._previousValueToken, "PREV((")
  };

  public static string GetPrevFunctionInvocationText(string argument)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(argument, nameof (argument), (string) null);
    return $"PREV(({argument}))";
  }

  public static string GetPrevFunctionInvocationTextForPath(string path)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(path, nameof (path), (string) null);
    return "PREV" + path;
  }
}
