// Decompiled with JetBrains decompiler
// Type: PX.Data.VersionedModifiedPair
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[Serializable]
internal class VersionedModifiedPair
{
  public PXEntryStatus? Status;
  public bool WasChanged;
  public Dictionary<string, object> CopyOfItem;
}
