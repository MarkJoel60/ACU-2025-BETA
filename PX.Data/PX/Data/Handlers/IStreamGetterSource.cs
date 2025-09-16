// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.IStreamGetterSource
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Handlers;

/// <exclude />
public interface IStreamGetterSource : IDisposable
{
  long Length { get; }

  void Initialise(Dictionary<string, string> args);

  void Read(Stream stream, long start, long stop);
}
