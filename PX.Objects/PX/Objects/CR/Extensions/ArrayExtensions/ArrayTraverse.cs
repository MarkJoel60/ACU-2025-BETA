// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ArrayExtensions.ArrayTraverse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CR.Extensions.ArrayExtensions;

internal class ArrayTraverse
{
  public int[] Position;
  private int[] maxLengths;

  public ArrayTraverse(Array array)
  {
    this.maxLengths = new int[array.Rank];
    for (int dimension = 0; dimension < array.Rank; ++dimension)
      this.maxLengths[dimension] = array.GetLength(dimension) - 1;
    this.Position = new int[array.Rank];
  }

  public bool Step()
  {
    for (int index1 = 0; index1 < this.Position.Length; ++index1)
    {
      if (this.Position[index1] < this.maxLengths[index1])
      {
        ++this.Position[index1];
        for (int index2 = 0; index2 < index1; ++index2)
          this.Position[index2] = 0;
        return true;
      }
    }
    return false;
  }
}
