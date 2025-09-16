// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.DictionarySessionState
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Common.Session;

internal sealed class DictionarySessionState : IPXSessionState
{
  private readonly Dictionary<string, object?> \u0002 = new Dictionary<string, object>((IEqualityComparer<string>) PXSessionState.KeyComparer);

  IEnumerable<string> IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002()
  {
    return (IEnumerable<string>) this.\u0002.Keys;
  }

  object? IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002(
    string _param1)
  {
    object obj;
    return !this.\u0002.TryGetValue(_param1, out obj) ? (object) null : obj;
  }

  bool IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002(
    string _param1,
    [NotNullWhen(true)] out object? _param2)
  {
    return this.\u0002.TryGetValue(_param1, out _param2);
  }

  bool IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002(string _param1)
  {
    return this.\u0002.ContainsKey(_param1);
  }

  void IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002(
    string _param1,
    object? _param2)
  {
    this.\u0002[_param1] = _param2;
  }

  void IPXSessionState.\u0038fda9l369delzgn6ew9dljapn3h2fdu3\u2009\u2009\u2009\u0002(string _param1)
  {
    this.\u0002.Remove(_param1);
  }
}
