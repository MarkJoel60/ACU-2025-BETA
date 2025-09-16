// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.DitaImageReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.DITA;

internal class DitaImageReader
{
  public void Read(Stream source, string fileName)
  {
    PXGraph graph = new PXGraph();
    int num = fileName.IndexOf("/");
    if (num > -1)
      fileName = fileName.Remove(0, num + 1);
    PXSelectJoin<UploadFile, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>>>, Where<UploadFile.name, Equal<Required<UploadFile.name>>>> pxSelectJoin = new PXSelectJoin<UploadFile, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>>>, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>(graph);
    List<byte> dataArray = new List<byte>();
    source.Seek(0L, SeekOrigin.Begin);
    for (int index = 0; (long) index < source.Length; ++index)
      dataArray.Add((byte) source.ReadByte());
    using (IEnumerator<PXResult<UploadFile>> enumerator = pxSelectJoin.Select((object) fileName).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<UploadFile, UploadFileRevision> current = (PXResult<UploadFile, UploadFileRevision>) enumerator.Current;
        this.UpdateFile((UploadFileRevision) current[typeof (UploadFileRevision)], (UploadFile) current[typeof (UploadFile)], dataArray, fileName, graph);
        return;
      }
    }
    this.InsertFile(dataArray, fileName, graph);
  }

  public void InsertFile(List<byte> dataArray, string fileName, PXGraph graph)
  {
    Guid guid = Guid.NewGuid();
    UploadFile uploadFile = new UploadFile()
    {
      PrimaryPageID = new Guid?(guid),
      PrimaryScreenID = "SM000000",
      FileID = new Guid?(guid)
    };
    uploadFile.CheckedOutBy = uploadFile.FileID;
    uploadFile.CheckedOutComment = "";
    uploadFile.CreatedByID = new Guid?(guid);
    uploadFile.CreatedDateTime = new System.DateTime?(System.DateTime.Now);
    uploadFile.LastRevisionID = new int?(1);
    uploadFile.Name = fileName;
    uploadFile.Versioned = new bool?(true);
    uploadFile.IsPublic = new bool?(true);
    graph.Caches[typeof (UploadFile)].Insert((object) uploadFile);
    graph.Caches[typeof (UploadFile)].Persist(PXDBOperation.Insert);
    UploadFileRevision uploadFileRevision = new UploadFileRevision()
    {
      FileID = uploadFile.FileID,
      FileRevisionID = new int?(1),
      Data = dataArray.ToArray(),
      Size = new int?(UploadFileHelper.BytesToKilobytes(dataArray.Count)),
      CreatedByID = uploadFile.FileID,
      CreatedDateTime = new System.DateTime?(System.DateTime.Now)
    };
    graph.Caches[typeof (UploadFileRevision)].Insert((object) uploadFileRevision);
    graph.Caches[typeof (UploadFileRevision)].Persist(PXDBOperation.Insert);
  }

  public void UpdateFile(
    UploadFileRevision uploadfilerevision,
    UploadFile uploadfile,
    List<byte> dataArray,
    string fileName,
    PXGraph graph)
  {
    uploadfile.Name = fileName;
    uploadfile.CreatedDateTime = new System.DateTime?(System.DateTime.Now);
    graph.Caches[typeof (UploadFile)].Update((object) uploadfile);
    graph.Caches[typeof (UploadFile)].Persist(PXDBOperation.Update);
    uploadfilerevision.Data = dataArray.ToArray();
    uploadfilerevision.CreatedDateTime = new System.DateTime?(System.DateTime.Now);
    graph.Caches[typeof (UploadFileRevision)].Update((object) uploadfilerevision);
    graph.Caches[typeof (UploadFileRevision)].Persist(PXDBOperation.Update);
  }
}
