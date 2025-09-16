// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.SourceElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class SourceElement : PXElement
{
  public List<SourceElement.SourcePart> Source = new List<SourceElement.SourcePart>();

  public SourceElement()
  {
  }

  public SourceElement(string content)
  {
    this.Source.Add(new SourceElement.SourcePart(content, SourceElement.SyntaxType.Text, SourceElement.DiffState.NoChange));
  }

  public enum SyntaxType
  {
    Bracket,
    Comment,
    StringLiteral,
    Number,
    Text,
    Keyword,
  }

  public enum DiffState
  {
    NoChange,
    Added,
    Removed,
  }

  [Flags]
  public enum TextStyle
  {
    Bold = 1,
    Italic = 2,
  }

  public class SourcePart
  {
    public string Value;
    public SourceElement.SyntaxType Syntax;
    public SourceElement.DiffState DiffState;
    public SourceElement.TextStyle TextStyle;

    public SourcePart(
      string Value,
      SourceElement.SyntaxType Syntax,
      SourceElement.DiffState DiffState,
      SourceElement.TextStyle TextStyle = (SourceElement.TextStyle) 0)
    {
      this.Value = Value;
      this.Syntax = Syntax;
      this.DiffState = DiffState;
      this.TextStyle = TextStyle;
    }
  }
}
