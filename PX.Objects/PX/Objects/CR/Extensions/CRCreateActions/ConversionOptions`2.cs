// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.ConversionOptions`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class ConversionOptions<TTargetGraph, TTarget>
  where TTargetGraph : PXGraph, new()
  where TTarget : class, IBqlTable, INotable, new()
{
  public IList<ICRPreserveCachedRecordsFilter> PreserveCachedRecordsFilters { get; set; } = (IList<ICRPreserveCachedRecordsFilter>) new List<ICRPreserveCachedRecordsFilter>();

  public bool DoNotPersistAfterConvert { get; set; }

  public bool DoNotCancelAfterConvert { get; set; }
}
