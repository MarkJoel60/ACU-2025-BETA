// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.IPXSessionState
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Common.Session;

internal interface IPXSessionState
{
  IEnumerable<string> Keys { get; }

  object? Get(string _param1);

  bool TryGetValue(string _param1, [NotNullWhen(true)] out object? _param2);

  bool Contains(string _param1);

  void Set(string _param1, object? _param2);

  void Remove(string _param1);
}
