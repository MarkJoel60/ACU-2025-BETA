// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Wizard.WizardResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.CR.Wizard;

/// <summary>
/// The aliases for <see cref="T:PX.Data.WebDialogResult" /> that are used inside the wizard.
/// </summary>
[PXInternalUseOnly]
public static class WizardResult
{
  /// <summary>The abort (Cancel) button.</summary>
  /// <remarks>
  /// Formally it is an abort button, but in the UI, it's name is Cancel.
  /// </remarks>
  public const WebDialogResult Abort = (WebDialogResult) 3;
  /// <summary>The Back button.</summary>
  public const WebDialogResult Back = (WebDialogResult) 72;
}
