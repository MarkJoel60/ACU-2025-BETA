// Decompiled with JetBrains decompiler
// Type: PX.Data.NotLoggedInException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <Exclude />
[Serializable]
internal class NotLoggedInException : Exception
{
  public NotLoggedInException()
  {
  }

  public NotLoggedInException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<NotLoggedInException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<NotLoggedInException>(this, info);
    base.GetObjectData(info, context);
  }
}
