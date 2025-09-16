// Decompiled with JetBrains decompiler
// Type: PX.Data.SequentialGuid
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class SequentialGuid
{
  [DllImport("rpcrt4.dll", SetLastError = true)]
  private static extern int UuidCreateSequential(out Guid value);

  public static Guid Generate()
  {
    Guid guid;
    if (SequentialGuid.UuidCreateSequential(out guid) == 1739)
      return Guid.NewGuid();
    byte[] byteArray = guid.ToByteArray();
    Array.Reverse((Array) byteArray, 0, 4);
    Array.Reverse((Array) byteArray, 4, 2);
    Array.Reverse((Array) byteArray, 6, 2);
    return new Guid(byteArray);
  }

  /// <exclude />
  [Flags]
  private enum RetUuidCodes
  {
    RPC_S_OK = 0,
    RPC_S_UUID_LOCAL_ONLY = 1824, // 0x00000720
    RPC_S_UUID_NO_ADDRESS = 1739, // 0x000006CB
  }
}
