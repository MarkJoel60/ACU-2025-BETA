// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.FirstAsyncResultOperator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Clauses.StreamedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal class FirstAsyncResultOperator : ValueFromSequenceResultOperatorBase
{
  /// <inheritdoc />
  public FirstAsyncResultOperator(bool returnDefaultWhenEmpty)
  {
    this.ReturnDefaultWhenEmpty = returnDefaultWhenEmpty;
  }

  public bool ReturnDefaultWhenEmpty { get; }

  /// <inheritdoc />
  public virtual IStreamedDataInfo GetOutputDataInfo(IStreamedDataInfo inputInfo)
  {
    ExceptionExtensions.ThrowOnNull<IStreamedDataInfo>(inputInfo, nameof (inputInfo), (string) null);
    if (!(inputInfo is StreamedSequenceInfo streamedSequenceInfo))
      throw new ArgumentException($"Parameter 'inputInfo' has unexpected type '{inputInfo.GetType().FullName}'.");
    return (IStreamedDataInfo) new StreamedSingleValueInfo(streamedSequenceInfo.ResultItemType, this.ReturnDefaultWhenEmpty);
  }

  private StreamedValueInfo GetSyncOutputDataInfo(IStreamedDataInfo inputInfo)
  {
    return (StreamedValueInfo) ((ResultOperatorBase) this).GetOutputDataInfo(inputInfo);
  }

  /// <inheritdoc />
  public virtual ResultOperatorBase Clone(CloneContext cloneContext)
  {
    return (ResultOperatorBase) new FirstResultOperator(this.ReturnDefaultWhenEmpty);
  }

  /// <inheritdoc />
  public virtual void TransformExpressions(Func<Expression, Expression> transformation)
  {
  }

  /// <inheritdoc />
  public virtual StreamedValue ExecuteInMemory<T>(StreamedSequence input)
  {
    IEnumerable<T> typedSequence = input.GetTypedSequence<T>();
    return new StreamedValue((object) (this.ReturnDefaultWhenEmpty ? typedSequence.FirstOrDefault<T>() : typedSequence.First<T>()), this.GetSyncOutputDataInfo((IStreamedDataInfo) input.DataInfo));
  }

  public virtual string ToString() => !this.ReturnDefaultWhenEmpty ? "First()" : "FirstOrDefault()";
}
