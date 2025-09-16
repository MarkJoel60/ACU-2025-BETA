// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARMigrationModeDependentActionRestrictionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public class ARMigrationModeDependentActionRestrictionAttribute : PXAggregateAttribute
{
  public ARMigrationModeDependentActionRestrictionAttribute(
    bool restrictInMigrationMode,
    bool restrictForRegularDocumentInMigrationMode,
    bool restrictForUnreleasedMigratedDocumentInNormalMode)
  {
    if (restrictInMigrationMode)
      this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<ARSetup.migrationMode>, Equal<True>>), "Migration mode is activated in the Accounts Receivable module."));
    if (restrictForRegularDocumentInMigrationMode)
      this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<ARRegister.isMigratedRecord>, NotEqual<True>, And<Current<ARSetup.migrationMode>, Equal<True>>>), "The document cannot be processed because it was created when migration mode was deactivated. To process the document, clear the Activate Migration Mode check box on the Accounts Receivable Preferences (AR101000) form."));
    if (!restrictForUnreleasedMigratedDocumentInNormalMode)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXActionRestrictionAttribute(typeof (Where<Current<ARRegister.isMigratedRecord>, Equal<True>, And<Current<ARRegister.released>, NotEqual<True>, And<Current<ARSetup.migrationMode>, NotEqual<True>>>>), "The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Accounts Receivable Preferences (AR101000) form."));
  }
}
