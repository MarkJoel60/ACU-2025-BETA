// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.SignManager.SignDialogResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Api.Mobile.SignManager;

/// <summary>Defines the values that indicate which button the user clicked in the dialog box
/// opened by the <see cref="M:PX.Data.Api.Mobile.SignManager.SignManager.Sign(PX.Data.PXView,System.Guid,System.Nullable{System.Guid}@)" /> method.</summary>
public enum SignDialogResult
{
  /// <summary>None of the actions were done.</summary>
  None,
  /// <summary>The user signed the document.</summary>
  Complete,
  /// <summary>The user declined to sign the document.</summary>
  Rejected,
}
