// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RoutingException
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

/// <summary>
/// Exception thrown if a request to generate a route between locations fails.
/// </summary>
public class RoutingException : Exception
{
  internal RoutingException(string message)
    : base(message)
  {
  }
}
