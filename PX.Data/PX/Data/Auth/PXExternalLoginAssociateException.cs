// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.PXExternalLoginAssociateException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
[Serializable]
public class PXExternalLoginAssociateException : PXException
{
  public string ProviderID;
  public string ExternalID;
  public string ExternalEmail;

  public PXExternalLoginAssociateException(string provider, string id, string email)
    : this("The system cannot find the associated user. Sign in using your internal credentials, and the system will automatically associate your external account with an internal user. Alternatively, you can use another sign-in option.", provider, id, email)
  {
  }

  public PXExternalLoginAssociateException(
    string message,
    string provider,
    string id,
    string email)
    : base(message)
  {
    this.ProviderID = provider;
    this.ExternalID = id;
    this.ExternalEmail = email;
  }

  public PXExternalLoginAssociateException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXExternalLoginAssociateException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXExternalLoginAssociateException>(this, info);
    base.GetObjectData(info, context);
  }
}
