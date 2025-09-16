// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXCertificateProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Reports;
using PX.SM;
using System;
using System.Security.Cryptography.X509Certificates;

#nullable disable
namespace PX.Data.Reports;

public class PXCertificateProvider : CertificateProvider
{
  public virtual string Default => SitePolicy.PdfCertificateName;

  public virtual X509Certificate2 Create(string name)
  {
    if (name == null)
      return (X509Certificate2) null;
    CertificateMaintenance instance1 = PXGraph.CreateInstance<CertificateMaintenance>();
    PXDBCryptStringAttribute.SetDecrypted<Certificate.password>(instance1.CertificateFile.Cache, true);
    CetrificateFile data = (CetrificateFile) instance1.CertificateFile.Select((object) name);
    PXFieldState stateExt = instance1.GetStateExt("CertificateFile", (object) data, "password") as PXFieldState;
    Guid? fileId;
    int num;
    if (data == null)
    {
      num = 1;
    }
    else
    {
      fileId = data.FileID;
      num = !fileId.HasValue ? 1 : 0;
    }
    if (num != 0)
      return (X509Certificate2) null;
    UploadFileMaintenance instance2 = PXGraph.CreateInstance<UploadFileMaintenance>();
    fileId = data.FileID;
    Guid fileID = fileId.Value;
    FileInfo file = instance2.GetFile(fileID);
    return file != null ? new X509Certificate2(file.BinData, stateExt.Value.ToString(), X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet) : (X509Certificate2) null;
  }
}
