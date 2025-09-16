// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class DiffState
{
  private const int BAD_INDEX = -1;
  private int _startIndex;
  private int _length;

  public int StartIndex => this._startIndex;

  public int EndIndex => this._startIndex + this._length - 1;

  public int Length => this._length <= 0 ? (this._length != 0 ? 0 : 1) : this._length;

  public DiffStatus Status
  {
    get
    {
      return this._length <= 0 ? (this._length != -1 ? DiffStatus.Unknown : DiffStatus.NoMatch) : DiffStatus.Matched;
    }
  }

  public DiffState() => this.SetToUnkown();

  protected void SetToUnkown()
  {
    this._startIndex = -1;
    this._length = -2;
  }

  public void SetMatch(int start, int length)
  {
    this._startIndex = start;
    this._length = length;
  }

  public void SetNoMatch()
  {
    this._startIndex = -1;
    this._length = -1;
  }

  public bool HasValidLength(int newStart, int newEnd, int maxPossibleDestLength)
  {
    if (this._length > 0 && (maxPossibleDestLength < this._length || this._startIndex < newStart || this.EndIndex > newEnd))
      this.SetToUnkown();
    return this._length != -2;
  }
}
