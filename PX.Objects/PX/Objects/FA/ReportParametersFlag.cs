// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.ReportParametersFlag
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.FA;

[Flags]
public enum ReportParametersFlag
{
  None = 0,
  Organization = 1,
  Branch = 2,
  BAccount = 4,
  FixedAsset = 8,
  Book = 16, // 0x00000010
}
