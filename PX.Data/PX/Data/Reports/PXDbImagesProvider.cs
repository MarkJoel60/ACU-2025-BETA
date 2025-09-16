// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXDbImagesProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Reports;
using PX.SM;
using System.Drawing;
using System.IO;

#nullable disable
namespace PX.Data.Reports;

public class PXDbImagesProvider : DbImagesProvider
{
  public virtual Image Load(string source)
  {
    string[] strArray = source.Split('$');
    if (strArray.Length > 1)
      source = strArray[1];
    return Image.FromStream((Stream) new MemoryStream((PXContext.GetSlot<UploadFileMaintenance>() ?? PXContext.SetSlot<UploadFileMaintenance>(PXGraph.CreateInstance<UploadFileMaintenance>())).GetFile(source).BinData));
  }
}
