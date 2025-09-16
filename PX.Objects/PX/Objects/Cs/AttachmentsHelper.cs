// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AttachmentsHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public class AttachmentsHelper
{
  private static byte[] GetMergedAttachmentsData(IEnumerable<FileInfo> files)
  {
    using (PDFHandler pdfHandler = new PDFHandler())
    {
      foreach (FileInfo file in files)
        pdfHandler.AddFile(file.OriginalName, file.BinData);
      return pdfHandler.GetPDFBytes();
    }
  }

  public static FileInfo CreateMergedPDF(IEnumerable<FileInfo> files, string fileName)
  {
    byte[] mergedAttachmentsData = AttachmentsHelper.GetMergedAttachmentsData(files);
    return mergedAttachmentsData == null ? (FileInfo) null : new FileInfo(Guid.NewGuid(), fileName, (string) null, mergedAttachmentsData);
  }
}
