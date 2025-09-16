// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Tools.SerializationHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Tools;

public static class SerializationHelper
{
  public static byte[] GetBytes(string str)
  {
    byte[] dst = new byte[str.Length * 2];
    Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) dst, 0, dst.Length);
    return dst;
  }
}
