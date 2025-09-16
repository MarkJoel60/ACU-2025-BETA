// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CMSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.CM;

public class CMSetupMaint : PXGraph<CMSetupMaint>
{
  public PXSelect<CMSetup> cmsetup;
  public PXSave<CMSetup> Save;
  public PXCancel<CMSetup> Cancel;
  public PXSelectJoin<Currency, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.baseCuryID, Equal<Currency.curyID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>> basecurrency;
  public PXSetup<Company> company;
  public PXSelect<TranslDef, Where<TranslDef.translDefId, Equal<Current<CMSetup.translDefId>>>> baseTranslDef;

  public CMSetupMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<Company>) this.company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
  }
}
