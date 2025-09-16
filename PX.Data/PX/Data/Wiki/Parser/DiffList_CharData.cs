// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffList_CharData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class DiffList_CharData : IDiffList
{
  private char[] _charList;

  public DiffList_CharData(string charData) => this._charList = charData.ToCharArray();

  public int Count() => this._charList.Length;

  public IComparable GetByIndex(int index) => (IComparable) this._charList[index];
}
