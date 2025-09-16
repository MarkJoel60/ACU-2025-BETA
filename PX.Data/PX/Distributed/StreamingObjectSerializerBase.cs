// Decompiled with JetBrains decompiler
// Type: PX.Distributed.StreamingObjectSerializerBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.IO;

#nullable enable
namespace PX.Distributed;

internal abstract class StreamingObjectSerializerBase : IDistributedObjectSerializer
{
  protected abstract void Serialize<T>(T graph, Stream stream);

  void IDistributedObjectSerializer.Serialize<T>(T? graph, Stream stream)
  {
    this.Serialize<T>(graph, stream);
  }

  byte[] IDistributedObjectSerializer.Serialize<T>(T? graph)
  {
    using (SparseMemoryStream sparseMemoryStream = new SparseMemoryStream(2048 /*0x0800*/))
    {
      this.Serialize<T>(graph, (Stream) sparseMemoryStream);
      return sparseMemoryStream.ToArray();
    }
  }

  protected abstract T? Deserialize<T>(Stream stream);

  T? IDistributedObjectSerializer.Deserialize<T>(Stream stream) => this.Deserialize<T>(stream);

  T? IDistributedObjectSerializer.Deserialize<T>(byte[] serialized)
  {
    using (MemoryStream memoryStream = new MemoryStream(serialized))
      return this.Deserialize<T>((Stream) memoryStream);
  }
}
