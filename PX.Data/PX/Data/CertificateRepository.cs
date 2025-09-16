// Decompiled with JetBrains decompiler
// Type: PX.Data.CertificateRepository
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

#nullable disable
namespace PX.Data;

internal class CertificateRepository : ICertificateRepository
{
  private const string ViewName = "CetrificateFile";
  private readonly PXGraph _graph;

  public CertificateRepository()
  {
    this._graph = new PXGraph();
    this._graph.Views.Add("CetrificateFile", new PXView(this._graph, true, PXSelectBase<CetrificateFile, PXSelect<CetrificateFile, Where<Certificate.name, Equal<Required<Certificate.name>>>>.Config>.GetCommand()));
    PXDBCryptStringAttribute.SetDecrypted<Certificate.password>(this._graph.Caches[typeof (CetrificateFile)], true);
  }

  X509Certificate2 ICertificateRepository.GetCertificate(string name)
  {
    CetrificateFile data = (CetrificateFile) PXSelectBase<CetrificateFile, PXSelect<CetrificateFile, Where<Certificate.name, Equal<Required<Certificate.name>>>>.Config>.Select(this._graph, (object) name);
    Guid? fileId = (Guid?) data?.FileID;
    if (!fileId.HasValue)
      return (X509Certificate2) null;
    string password = this._graph.GetStateExt("CetrificateFile", (object) data, "password") is PXFieldState stateExt ? stateExt.Value?.ToString() : (string) null;
    if (password == null)
      return (X509Certificate2) null;
    UploadFileRevision uploadFileRevision = (UploadFileRevision) PXSelectBase<UploadFileRevision, PXSelectJoin<UploadFileRevision, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>>.Config>.Select(this._graph, (object) fileId);
    return uploadFileRevision == null ? (X509Certificate2) null : new X509Certificate2(uploadFileRevision.Data, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
  }

  internal static X509Certificate2 GetCertificate(string name)
  {
    return ((ICertificateRepository) new CertificateRepository()).GetCertificate(name);
  }

  IEnumerable<string> ICertificateRepository.GetCertificateNames()
  {
    return PXSelectBase<Certificate, PXSelect<Certificate>.Config>.Select(this._graph).FirstTableItems.Select<Certificate, string>((Func<Certificate, string>) (c => c.Name));
  }
}
