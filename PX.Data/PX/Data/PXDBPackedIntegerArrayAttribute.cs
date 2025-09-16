// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBPackedIntegerArrayAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// Maps a DAC field of <tt>ushort[]</tt> type to the binary database column of variable length, using the One Hot encoding.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBPackedIntegerArrayAttribute : PXDBBinaryAttribute
{
  protected IEnumerable<ushort> ExtractHotBitPositions(byte[] bytes)
  {
    ushort pass = 0;
    byte[] numArray = bytes;
    for (int index = 0; index < numArray.Length; ++index)
    {
      byte b = numArray[index];
      int offset = (int) pass * 8;
      if (((int) b & 1) == 1)
        yield return (ushort) (1 + offset);
      if (((int) b & 2) == 2)
        yield return (ushort) (2 + offset);
      if (((int) b & 4) == 4)
        yield return (ushort) (3 + offset);
      if (((int) b & 8) == 8)
        yield return (ushort) (4 + offset);
      if (((int) b & 16 /*0x10*/) == 16 /*0x10*/)
        yield return (ushort) (5 + offset);
      if (((int) b & 32 /*0x20*/) == 32 /*0x20*/)
        yield return (ushort) (6 + offset);
      if (((int) b & 64 /*0x40*/) == 64 /*0x40*/)
        yield return (ushort) (7 + offset);
      if (((int) b & 128 /*0x80*/) == 128 /*0x80*/)
        yield return (ushort) (8 + offset);
      ++pass;
    }
    numArray = (byte[]) null;
  }

  protected byte ToHotBit(ushort value)
  {
    switch (value)
    {
      case 0:
        return 0;
      case 1:
        return 1;
      case 2:
        return 2;
      case 3:
        return 4;
      case 4:
        return 8;
      case 5:
        return 16 /*0x10*/;
      case 6:
        return 32 /*0x20*/;
      case 7:
        return 64 /*0x40*/;
      case 8:
        return 128 /*0x80*/;
      default:
        throw new ArgumentOutOfRangeException(nameof (value));
    }
  }

  protected byte[] PackToHotBits(IEnumerable<ushort> hotBitPositions)
  {
    ushort[] array = hotBitPositions.OrderBy<ushort, ushort>((Func<ushort, ushort>) (p => p)).ToArray<ushort>();
    byte[] hotBits = new byte[System.Math.Max(1, (int) (ushort) System.Math.Pow(2.0, (double) (ushort) System.Math.Ceiling(System.Math.Log((double) ((IEnumerable<ushort>) array).LastOrDefault<ushort>()) / System.Math.Log(2.0))) / 8)];
    ushort index = 0;
    foreach (ushort num in array)
    {
      while ((int) num > ((int) index + 1) * 8)
        ++index;
      hotBits[(int) index] |= this.ToHotBit((ushort) ((uint) num - (uint) index * 8U));
    }
    return hotBits;
  }

  protected override object DeserializeValue(byte[] bytes)
  {
    return bytes == null ? (object) null : (object) this.ExtractHotBitPositions(bytes).ToArray<ushort>();
  }

  protected override byte[] SerializeValue(object value)
  {
    return value == null ? (byte[]) null : this.PackToHotBits((IEnumerable<ushort>) (ushort[]) value);
  }
}
