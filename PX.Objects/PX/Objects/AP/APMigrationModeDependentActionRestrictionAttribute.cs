// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APMigrationModeDependentActionRestrictionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APMigrationModeDependentActionRestrictionAttribute : PXAggregateAttribute
{
  public APMigrationModeDependentActionRestrictionAttribute(
    bool restrictInMigrationMode,
    bool restrictForRegularDocumentInMigrationMode,
    bool restrictForUnreleasedMigratedDocumentInNormalMode)
  {
    if (restrictInMigrationMode)
      this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<APSetup.migrationMode>, Equal<True>>), "Migration mode is activated in the Accounts Payable module."));
    if (restrictForRegularDocumentInMigrationMode)
      this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<APRegister.isMigratedRecord>, NotEqual<True>, And<Current<APSetup.migrationMode>, Equal<True>>>), "The document cannot be processed because it was created when migration mode was deactivated. To process the document, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form."));
    if (!restrictForUnreleasedMigratedDocumentInNormalMode)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<APRegister.isMigratedRecord>, Equal<True>, And<Current<APRegister.released>, NotEqual<True>, And<Current<APSetup.migrationMode>, NotEqual<True>>>>), "The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Accounts Payable Preferences (AP101000) form."));
  }
}
