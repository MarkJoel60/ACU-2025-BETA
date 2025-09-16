// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.OperationKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Async.Internal;

[DebuggerDisplay("{FormattedDisplay,nq}")]
internal sealed class OperationKey
{
  [Obsolete("Do not create OperationKey from OperationKey", true)]
  internal OperationKey(OperationKey key)
  {
    throw new ArgumentOutOfRangeException(nameof (key), (object) key, "Do not create OperationKey from OperationKey");
  }

  internal OperationKey(object original)
  {
    this.Original = original;
    this.String = OperationKey.StringifyAndValidate(original);
  }

  internal OperationKey(Guid guid)
  {
    this.Original = (object) guid;
    this.String = OperationKey.Stringify((object) guid);
  }

  private static string StringifyAndValidate(object? key)
  {
    if (key != null && key.GetType() != typeof (object) && !(key is OperationKey))
    {
      string str = OperationKey.Stringify(key);
      if (!string.IsNullOrEmpty(str))
        return str;
    }
    throw new ArgumentOutOfRangeException(nameof (key), key, "Invalid key for PXLongOperation");
  }

  private static string Stringify(object key) => key.ToString();

  internal static OperationKey For(PXGraph graph) => new OperationKey(graph.UID);

  internal static OperationKey New() => new OperationKey(Guid.NewGuid());

  internal static OperationKey? IfNotNull(object? key)
  {
    return key != null ? new OperationKey(key) : (OperationKey) null;
  }

  [Obsolete("Do not create OperationKey from OperationKey", true)]
  internal static OperationKey IfNotNull(OperationKey? key) => new OperationKey(key);

  internal object Original { get; }

  internal string String { get; }

  public override string ToString() => this.String;

  internal string FormattedDisplay => $"{this.Original.GetType().FullName}({this.String})";
}
