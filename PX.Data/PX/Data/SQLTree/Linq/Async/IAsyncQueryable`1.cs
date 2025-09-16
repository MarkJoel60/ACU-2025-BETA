// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.IAsyncQueryable`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal interface IAsyncQueryable<out T> : 
  IAsyncEnumerable<T>,
  IAsyncQueryable,
  IQueryable<T>,
  IEnumerable<T>,
  IEnumerable,
  IQueryable
{
}
