// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.TextStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Allowed text styles. Can be a masked value.</summary>
[Flags]
public enum TextStyle : short
{
  None = 0,
  Bold = 1,
  Italic = 2,
  Underlined = 4,
  Striked = 8,
  Monotype = 16, // 0x0010
  Subscript = 32, // 0x0020
  Superscript = 64, // 0x0040
}
