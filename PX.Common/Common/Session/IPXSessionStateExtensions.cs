// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.IPXSessionStateExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Common.Session;

internal static class IPXSessionStateExtensions
{
  internal static void RemoveAll(this IPXSessionState _param0, Func<string, bool> _param1)
  {
    List<string> stringList = new List<string>();
    foreach (string key in _param0.Keys)
    {
      if (!string.IsNullOrEmpty(key) && _param1(key))
        stringList.Add(key);
    }
    foreach (string str in stringList)
      _param0.Remove(str);
  }
}
