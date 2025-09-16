// Decompiled with JetBrains decompiler
// Type: PX.Data.QueryHints
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[Flags]
public enum QueryHints
{
  None = 0,
  SqlServerOptionRecompile = 1,
  SqlServerOptimizeForUnknown = 2,
  MySqlLowPriority = 4,
  MySqlHighPriority = 8,
  MySqlLockInShareMode = 16, // 0x00000010
  MySqlNoCache = 32, // 0x00000020
  MySqlGroupConcatMaxLength = 64, // 0x00000040
  SqlServerNoRecursionLimit = 128, // 0x00000080
}
