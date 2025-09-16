// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DiffResultSpan
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class DiffResultSpan : IComparable
{
  private const int BAD_INDEX = -1;
  private int _destIndex;
  private int _sourceIndex;
  private int _length;
  private DiffResultSpanStatus _status;

  public int DestIndex => this._destIndex;

  public int SourceIndex => this._sourceIndex;

  public int Length => this._length;

  public DiffResultSpanStatus Status => this._status;

  protected DiffResultSpan(
    DiffResultSpanStatus status,
    int destIndex,
    int sourceIndex,
    int length)
  {
    this._status = status;
    this._destIndex = destIndex;
    this._sourceIndex = sourceIndex;
    this._length = length;
  }

  public static DiffResultSpan CreateNoChange(int destIndex, int sourceIndex, int length)
  {
    return new DiffResultSpan(DiffResultSpanStatus.NoChange, destIndex, sourceIndex, length);
  }

  public static DiffResultSpan CreateReplace(int destIndex, int sourceIndex, int length)
  {
    return new DiffResultSpan(DiffResultSpanStatus.Replace, destIndex, sourceIndex, length);
  }

  public static DiffResultSpan CreateDeleteSource(int sourceIndex, int length)
  {
    return new DiffResultSpan(DiffResultSpanStatus.DeleteSource, -1, sourceIndex, length);
  }

  public static DiffResultSpan CreateAddDestination(int destIndex, int length)
  {
    return new DiffResultSpan(DiffResultSpanStatus.AddDestination, destIndex, -1, length);
  }

  public void AddLength(int i) => this._length += i;

  public override string ToString()
  {
    return $"{this._status.ToString()} (Dest: {this._destIndex.ToString()},Source: {this._sourceIndex.ToString()}) {this._length.ToString()}";
  }

  public int CompareTo(object obj) => this._destIndex.CompareTo(((DiffResultSpan) obj)._destIndex);
}
