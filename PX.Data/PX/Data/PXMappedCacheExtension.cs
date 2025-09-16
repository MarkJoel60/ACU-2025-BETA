// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMappedCacheExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>The class that represents a mapped cache extension, which is used for implementation of reusable business logic.</summary>
public abstract class PXMappedCacheExtension : 
  PXCacheExtension,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  internal WeakReference _Base;

  public object Base => this._Base?.Target;
}
