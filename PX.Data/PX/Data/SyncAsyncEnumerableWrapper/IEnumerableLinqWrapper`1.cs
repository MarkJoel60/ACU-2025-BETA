// Decompiled with JetBrains decompiler
// Type: PX.Data.SyncAsyncEnumerableWrapper.IEnumerableLinqWrapper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SyncAsyncEnumerableWrapper;

/// <summary>
/// A wrapper for using a single code point for synchronous and asynchronous fetching from the database. Used in
/// <see cref="M:PX.Data.PXGenericInqGrph.GetRecords(PX.Data.Description.GI.PXQueryDescription,System.Int32,System.Int32,System.Reactive.Disposables.CompositeDisposable,System.Func{PX.Data.SQLTree.Query,PX.Data.PXDataValue[],PX.Data.SyncAsyncEnumerableWrapper.IEnumerableLinqWrapper{PX.Data.PXDataRecord}})" />
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IEnumerableLinqWrapper<T>
{
  IEnumerable<T> AsEnumerable();

  IAsyncEnumerable<T> AsAsyncEnumerable();

  IEnumerableLinqWrapper<TResult> Select<TResult>(Func<T, TResult> selector);

  IEnumerableLinqWrapper<T> Where(Func<T, bool> filter);
}
