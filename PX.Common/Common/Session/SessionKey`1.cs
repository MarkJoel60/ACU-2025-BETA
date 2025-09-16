// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.SessionKey`1
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable enable
namespace PX.Common.Session;

internal class SessionKey<TValue>(string _param1)
{
  private readonly string \u0002 = _param1 ?? throw new ArgumentNullException("key");

  internal string Key => this.\u0002;
}
