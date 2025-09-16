// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Wizard.CRWizardBackException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CR.Wizard;

/// <summary>
/// The exception that shows that the <see cref="F:PX.Objects.CR.Wizard.WizardResult.Back" /> button is clicked in the wizard.
/// </summary>
[PXInternalUseOnly]
public class CRWizardBackException : CRWizardException
{
  public CRWizardBackException()
    : base("Wizard Back")
  {
  }

  public CRWizardBackException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
