// Decompiled with JetBrains decompiler
// Type: PX.Data.PXForeignRecordDeletedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DacDescriptorGeneration;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXForeignRecordDeletedException : PXSetPropertyException, IExceptionWithDescriptor
{
  public PXForeignRecordDeletedException()
    : base("The record has been deleted.")
  {
  }

  public PXForeignRecordDeletedException(PX.Data.DacDescriptorGeneration.DacDescriptor foreignRecordDescriptor)
    : base(new PX.Data.DacDescriptorGeneration.DacDescriptor?(foreignRecordDescriptor), "The record has been deleted.")
  {
  }

  public PXForeignRecordDeletedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXForeignRecordDeletedException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXForeignRecordDeletedException>(this, info);
    base.GetObjectData(info, context);
  }
}
