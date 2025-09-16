// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Tools.UploadFileHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.Common.Tools;

public static class UploadFileHelper
{
  /// <summary>AttachDataAsFile</summary>
  /// <remarks>Invoke before persisting of DAC</remarks>
  public static void AttachDataAsFile(
    string fileName,
    string data,
    IDACWithNote dac,
    PXGraph graph)
  {
    FileInfo fileInfo = new FileInfo(Guid.NewGuid(), fileName, (string) null, SerializationHelper.GetBytes(data));
    if (!PXGraph.CreateInstance<UploadFileMaintenance>().SaveFile(fileInfo) && fileInfo.UID.HasValue)
      return;
    NoteDoc noteDoc = new NoteDoc()
    {
      NoteID = dac.NoteID,
      FileID = fileInfo.UID
    };
    graph.Caches[typeof (NoteDoc)].Insert((object) noteDoc);
    graph.Persist(typeof (NoteDoc), (PXDBOperation) 2);
  }
}
