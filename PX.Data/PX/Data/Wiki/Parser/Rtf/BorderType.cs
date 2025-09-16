// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.BorderType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

[Flags]
public enum BorderType
{
  None = 0,
  Single = 1,
  DoubleThick = 2,
  Shadowed = 4,
  Double = 8,
  Dotted = 16, // 0x00000010
  Dashed = 32, // 0x00000020
  Hairline = 64, // 0x00000040
  DashSmall = 128, // 0x00000080
  DotDash = 256, // 0x00000100
  DotDotDash = 512, // 0x00000200
  Triple = 1024, // 0x00000400
  Wavy = 2048, // 0x00000800
  DoubleWavy = 4096, // 0x00001000
  Striped = 8192, // 0x00002000
  Emboss = 16384, // 0x00004000
  Engrave = 32768, // 0x00008000
}
