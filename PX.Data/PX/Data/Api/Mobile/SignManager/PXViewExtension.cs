// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.SignManager.PXViewExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Api.Mobile.SignManager.DAC;
using System;

#nullable disable
namespace PX.Data.Api.Mobile.SignManager;

[PXInternalUseOnly]
public static class PXViewExtension
{
  /// <summary>Call the Sign dialog for mobile</summary>
  /// <param name="fileId">Identificator of the document, that should be signed</param>
  /// <param name="signedFileId">Identificator of the signed copy of the document (if any)</param>
  /// <returns>Complete - in case when user accepted to sign the document
  /// Rejected - in case when user rejected to sign the document</returns>
  public static SignDialogResult Sign(this PXView view, Guid fileId, out Guid? signedFileId)
  {
    return PX.Data.Api.Mobile.SignManager.SignManager.Sign(view, fileId, out signedFileId);
  }

  /// <summary>Gets the value indicating user's choice in the dialog window displayed through one of the <tt>Sign()</tt> methods.</summary>
  public static SignDialogResult GetSignature(this PXView view, out Guid? signedFileId)
  {
    SignResult result = PX.Data.Api.Mobile.SignManager.SignManager.GetResult(view.Graph, view.Name);
    signedFileId = result.SignedFileId;
    return (SignDialogResult) result.Result.Value;
  }

  /// <summary>Sets the value indicating user's choice in the dialog window displayed through one of the <tt>Sign()</tt> methods.</summary>
  public static void SetSignature(this PXView view, SignDialogResult result, Guid? signedFielId)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.SetResult(view.Graph, view.Name, result, signedFielId);
  }
}
