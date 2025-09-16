// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.IAsyncQueryable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Async;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal interface IAsyncQueryable
{
  /// <summary>Gets the type of the elements in the sequence.</summary>
  System.Type ElementType { get; }

  /// <summary>Gets the expression representing the sequence.</summary>
  Expression Expression { get; }

  /// <summary>Gets the query provider used to execute the sequence.</summary>
  IAsyncQueryProvider Provider { get; }
}
