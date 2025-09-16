// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CheckIsPendingApprovalException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CA;

public class CheckIsPendingApprovalException : PXException
{
  public CheckIsPendingApprovalException()
    : base("The check cannot be printed for this document. Checks can be printed only for documents that have the Pending Print status.")
  {
  }

  public CheckIsPendingApprovalException(string message, params object[] args)
    : base(message, args)
  {
  }

  public CheckIsPendingApprovalException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<CheckIsPendingApprovalException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<CheckIsPendingApprovalException>(this, info);
    base.GetObjectData(info, context);
  }
}
