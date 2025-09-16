// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.Exceptions.PXContainerNotFoundException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Api.Mobile.Exceptions;

[Serializable]
internal class PXContainerNotFoundException : PXException
{
  public PXContainerNotFoundException(string containerName)
    : base("The {0} container was not found in the system.", (object) containerName)
  {
  }

  public PXContainerNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
