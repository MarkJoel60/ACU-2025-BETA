// Decompiled with JetBrains decompiler
// Type: PX.Data.IContentReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public interface IContentReader : IDisposable
{
  /// <summary>Move to next record in data source</summary>
  /// <returns>true, if the operation was performed successfully</returns>
  bool MoveNext();

  /// <summary>Find value in record by given key index</summary>
  /// <param name="index">index of value</param>
  /// <returns>null, if given key index was not found</returns>
  string GetValue(int index);

  /// <summary>Reset state of reader</summary>
  void Reset();

  /// <summary>Collection of value keys</summary>
  IDictionary<int, string> IndexKeyPairs { get; }
}
