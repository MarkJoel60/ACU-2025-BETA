// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.SignManager.PXSignatureRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.Api.Mobile.SignManager;

[PXInternalUseOnly]
public sealed class PXSignatureRequiredException : PXBaseRedirectException
{
  /// <summary>Identificator of the document that should be signed</summary>
  public Guid FileId { get; private set; }

  /// <summary>View name from which sign action is called</summary>
  public string ViewName { get; private set; }

  /// <summary>Connected graph</summary>
  public PXGraph Graph { get; private set; }

  public PXSignatureRequiredException(PXGraph graph, string viewName, Guid fileId)
    : base(fileId.ToString())
  {
    this.Graph = graph;
    this.ViewName = viewName;
    this.FileId = fileId;
  }

  public PXSignatureRequiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXSignatureRequiredException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSignatureRequiredException>(this, info);
    base.GetObjectData(info, context);
  }
}
