// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffStateList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class DiffStateList
{
  private DiffState[] _array;

  public DiffStateList(int destCount) => this._array = new DiffState[destCount];

  public DiffState GetByIndex(int index)
  {
    DiffState byIndex = this._array[index];
    if (byIndex == null)
    {
      byIndex = new DiffState();
      this._array[index] = byIndex;
    }
    return byIndex;
  }
}
