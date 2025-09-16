// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.TextLine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class TextLine : IComparable
{
  public string Line;
  public int _hash;

  public TextLine(string str)
  {
    this.Line = str.Replace("\t", "    ");
    this._hash = str.GetHashCode();
  }

  public int CompareTo(object obj) => this._hash.CompareTo(((TextLine) obj)._hash);
}
