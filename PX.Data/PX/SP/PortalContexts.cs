// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalContexts
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.SP;

[PXInternalUseOnly]
[Flags]
public enum PortalContexts
{
  Legacy = 1,
  Modern = 2,
  Any = Modern | Legacy, // 0x00000003
}
