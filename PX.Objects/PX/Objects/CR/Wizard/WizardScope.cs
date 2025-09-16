// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Wizard.WizardScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CR.Wizard;

/// <summary>
/// The scope that shows that the current execution stack is inside an action that is triggered from the wizard.
/// </summary>
/// <remarks>
/// The class can be used by actions that can be triggered from the wizard and outside the wizard.
/// </remarks>
[PXInternalUseOnly]
public class WizardScope : IDisposable
{
  private readonly bool prevState;
  private const string _WizardScope_ = "_WizardScope_";

  public static bool IsScoped => PXContext.GetSlot<bool>("_WizardScope_");

  public WizardScope()
  {
    this.prevState = WizardScope.IsScoped;
    PXContext.SetSlot<bool>(nameof (_WizardScope_), true);
  }

  public void Dispose() => PXContext.SetSlot<bool>("_WizardScope_", this.prevState);
}
