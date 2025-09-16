// Decompiled with JetBrains decompiler
// Type: PX.Distributed.PXReflectionDistributedObjectSerializer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.IO;

#nullable enable
namespace PX.Distributed;

/// <summary>
/// Uses <see cref="T:PX.Common.PXReflectionSerializer" /> internally (the same serializer that is used for the session state storage).
/// </summary>
internal class PXReflectionDistributedObjectSerializer : StreamingObjectSerializerBase
{
  protected override void Serialize<T>(T? graph, Stream stream)
  {
    PXReflectionSerializer.Serialize(stream, (object) graph);
  }

  protected override T? Deserialize<T>(Stream stream)
  {
    return (T) PXReflectionSerializer.Deserialize(stream);
  }
}
