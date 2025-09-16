// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.StreamedAsyncValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Remotion.Linq.Clauses.StreamedData;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal sealed class StreamedAsyncValue : IStreamedData
{
  public StreamedAsyncValue(object value, IStreamedDataInfo streamedDataInfo)
  {
    ExceptionExtensions.ThrowOnNull<IStreamedDataInfo>(streamedDataInfo, nameof (streamedDataInfo), (string) null);
    ExceptionExtensions.ThrowOnNull<object>(value, nameof (value), (string) null);
    this.DataInfo = streamedDataInfo;
    this.AsyncValue = value;
  }

  public IStreamedDataInfo DataInfo { get; }

  object IStreamedData.Value => this.AsyncValue;

  IStreamedDataInfo IStreamedData.DataInfo => this.DataInfo;

  public object AsyncValue { get; }
}
