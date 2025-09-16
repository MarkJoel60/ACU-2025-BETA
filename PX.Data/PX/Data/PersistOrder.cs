// Decompiled with JetBrains decompiler
// Type: PX.Data.PersistOrder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public enum PersistOrder
{
  NotSpecified = 0,
  Regular = 10, // 0x0000000A
  AtTheEndOfTransaction = 20, // 0x00000014
}
