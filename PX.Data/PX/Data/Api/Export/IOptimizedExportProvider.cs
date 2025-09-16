// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.IOptimizedExportProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data.Api.Export;

internal interface IOptimizedExportProvider : IDisposable
{
  IEnumerable<PXSYRow> DoSelect(
    long startRow,
    long selectTop,
    Dictionary<string, KeyValuePair<string, bool>[]> sorts,
    bool addTranslations,
    out PXSYTable table);

  IAsyncEnumerable<PXSYRow> DoSelectAsyncEnumerable(
    CancellationToken token,
    long startRow,
    long selectTop,
    Dictionary<string, KeyValuePair<string, bool>[]> sorts,
    bool addTranslations,
    out PXSYTable table);

  bool CanOptimize { get; }

  void SetRepeatingOption(SYMapping.RepeatingOption value);
}
