// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNeedChangePasswordException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when a user requested to change a password.
/// </summary>
[Serializable]
public class PXNeedChangePasswordException : PXException
{
  public PXNeedChangePasswordException()
    : base("You need to change your password. Sign in by using your browser to set a new password. ")
  {
  }

  public PXNeedChangePasswordException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXNeedChangePasswordException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXNeedChangePasswordException>(this, info);
    base.GetObjectData(info, context);
  }
}
