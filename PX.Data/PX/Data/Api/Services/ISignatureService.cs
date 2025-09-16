// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.ISignatureService
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
public interface ISignatureService
{
  SignResult GetResult(PXGraph graph, string viewName);

  void SetResult(
    PXGraph graph,
    string viewName,
    SignDialogResult signDialogResult,
    Guid? signedFileId);

  Guid? SetSignature(PXGraph graph, string viewName, string signature);

  void SetSignatureMeta(PXGraph graph, string viewName, string signatureMeta);

  void ClearResults(PXGraph graph, string viewName);
}
