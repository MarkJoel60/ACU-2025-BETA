// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GroupMaskHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

public static class GroupMaskHelper
{
  public static bool IsIncluded(byte[] entityMask, byte[] groupMask)
  {
    if (entityMask == null || groupMask == null)
      return false;
    for (int index = 0; index < Math.Min(entityMask.Length, groupMask.Length); ++index)
    {
      if (groupMask[index] != (byte) 0 && ((int) entityMask[index] & (int) groupMask[index]) == (int) groupMask[index])
        return true;
    }
    return false;
  }

  public static byte[] UpdateMask(bool isIncluded, byte[] oldEntityMask, byte[] groupMask)
  {
    if (groupMask == null || oldEntityMask == null && !isIncluded)
      return oldEntityMask;
    if (oldEntityMask == null)
      oldEntityMask = new byte[groupMask.Length];
    if (oldEntityMask.Length < groupMask.Length)
      Array.Resize<byte>(ref oldEntityMask, groupMask.Length);
    for (int index = 0; index < groupMask.Length; ++index)
    {
      if (groupMask[index] != (byte) 0)
        oldEntityMask[index] = isIncluded ? (byte) ((uint) oldEntityMask[index] | (uint) groupMask[index]) : (byte) ((uint) oldEntityMask[index] & (uint) ~groupMask[index]);
    }
    return oldEntityMask;
  }
}
