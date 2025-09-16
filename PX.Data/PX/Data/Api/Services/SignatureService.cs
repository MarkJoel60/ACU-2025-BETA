// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.SignatureService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Api.Mobile.SignManager;
using PX.Data.Api.Mobile.SignManager.DAC;
using System;

#nullable disable
namespace PX.Data.Api.Services;

[PXInternalUseOnly]
public class SignatureService : ISignatureService
{
  public void SetResult(
    PXGraph graph,
    string viewName,
    SignDialogResult signDialogResult,
    Guid? signedFileId)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.SetResult(graph, viewName, signDialogResult, signedFileId);
  }

  public Guid? SetSignature(PXGraph graph, string viewName, string signature)
  {
    return PX.Data.Api.Mobile.SignManager.SignManager.SetSignature(graph, viewName, signature);
  }

  public void SetSignatureMeta(PXGraph graph, string viewName, string signatureMeta)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.SetSignatureMeta(graph, viewName, signatureMeta);
  }

  public SignResult GetResult(PXGraph graph, string viewName)
  {
    return PX.Data.Api.Mobile.SignManager.SignManager.GetResult(graph, viewName);
  }

  public void ClearResults(PXGraph graph, string viewName)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.SetSignature(graph, viewName, (string) null);
    PX.Data.Api.Mobile.SignManager.SignManager.SetSignatureMeta(graph, viewName, (string) null);
    PX.Data.Api.Mobile.SignManager.SignManager.SetResult(graph, viewName, SignDialogResult.None, new Guid?());
  }
}
