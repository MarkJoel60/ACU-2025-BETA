// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffList_TextFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class DiffList_TextFile : IDiffList
{
  private const int MaxLineLength = 2147483647 /*0x7FFFFFFF*/;
  private ArrayList _lines;

  public DiffList_TextFile(string fileName)
  {
    this._lines = new ArrayList();
    using (StreamReader streamReader = new StreamReader(fileName))
    {
      string str;
      while ((str = streamReader.ReadLine()) != null)
      {
        if (str.Length > int.MaxValue)
          throw new InvalidOperationException($"The file contains a line greater than {int.MaxValue.ToString()} characters.");
        this._lines.Add((object) new TextLine(str));
      }
    }
  }

  public int Count() => this._lines.Count;

  public IComparable GetByIndex(int index) => (IComparable) this._lines[index];
}
