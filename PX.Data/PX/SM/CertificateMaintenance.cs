// Decompiled with JetBrains decompiler
// Type: PX.SM.CertificateMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class CertificateMaintenance : PXGraph<CertificateMaintenance>
{
  public PXSelect<Certificate> Certificates;
  public PXSelect<CetrificateFile, Where<Certificate.name, Equal<Required<Certificate.name>>>> CertificateFile;
  public PXSavePerRow<Certificate> Save;
  public PXCancel<Certificate> Cancel;
  public PXSelect<PreferencesSecurity, Where<PreferencesSecurity.dBCertificateName, Equal<Required<Certificate.name>>, Or<PreferencesSecurity.dBPrevCertificateName, Equal<Required<Certificate.name>>, Or<PreferencesSecurity.pdfCertificateName, Equal<Required<Certificate.name>>>>>> PreferencesByName;

  protected virtual void Certificate_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
  }

  protected virtual void Certificate_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is Certificate row) || !SitePolicy.IsCertificateUsage(row.Name))
      return;
    if (this.Certificates.Ask("Warning", "This certificate is already in use. If you delete this certificate, you will be not able to decrypt data which was encrypted with the help of this certificate. Delete the certificate anyway?", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
    {
      e.Cancel = true;
    }
    else
    {
      PreferencesSecurity preferencesSecurity = (PreferencesSecurity) this.PreferencesByName.Select((object) row.Name, (object) row.Name, (object) row.Name);
      if (preferencesSecurity == null)
        return;
      if (preferencesSecurity.DBCertificateName == row.Name)
        preferencesSecurity.DBCertificateName = (string) null;
      if (preferencesSecurity.DBPrevCertificateName == row.Name)
        preferencesSecurity.DBPrevCertificateName = (string) null;
      if (preferencesSecurity.PdfCertificateName == row.Name)
        preferencesSecurity.PdfCertificateName = (string) null;
      this.PreferencesByName.Cache.Update((object) preferencesSecurity);
    }
  }
}
