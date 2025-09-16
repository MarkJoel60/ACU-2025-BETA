// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LicenseTypeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class LicenseTypeMaint : PXGraph<LicenseTypeMaint>
{
  [PXImport(typeof (FSLicenseType))]
  public PXSelect<FSLicenseType> LicenseTypeRecords;
  public PXSave<FSLicenseType> Save;
  public PXCancel<FSLicenseType> Cancel;

  protected virtual void _(Events.RowDeleting<FSLicenseType> e)
  {
    if (e.Row == null)
      return;
    FSLicense fsLicense = PXResultset<FSLicense>.op_Implicit(PXSelectBase<FSLicense, PXSelect<FSLicense, Where<FSLicense.licenseTypeID, Equal<Required<FSLicenseType.licenseTypeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.LicenseTypeID
    }));
    FSServiceLicenseType serviceLicenseType = PXResultset<FSServiceLicenseType>.op_Implicit(PXSelectBase<FSServiceLicenseType, PXSelect<FSServiceLicenseType, Where<FSServiceLicenseType.licenseTypeID, Equal<Required<FSLicenseType.licenseTypeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.LicenseTypeID
    }));
    if (fsLicense != null || serviceLicenseType != null)
      throw new PXException("This license type is specified for at least one non-stock item on the Non-Stock Items (IN202000) form, or a license on the Licenses (FS201000) form.");
  }
}
