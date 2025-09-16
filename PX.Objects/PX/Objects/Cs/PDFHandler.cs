// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PDFHandler
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

#nullable disable
namespace PX.Objects.CS;

public class PDFHandler : IDisposable
{
  private readonly PdfDocument _document;

  public PDFHandler() => this._document = new PdfDocument();

  public void AddFile(string fileName, byte[] fileBytes)
  {
    if (!(Path.GetExtension(fileName).ToLower() == ".pdf"))
      return;
    try
    {
      this.AppendPdf(fileBytes);
    }
    catch
    {
    }
  }

  public byte[] GetPDFBytes()
  {
    byte[] pdfBytes = (byte[]) null;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      try
      {
        this._document.Save((Stream) memoryStream, true);
        pdfBytes = memoryStream.ToArray();
      }
      catch
      {
      }
    }
    return pdfBytes;
  }

  public void Dispose()
  {
    this._document.Close();
    this._document.Dispose();
  }

  private void AppendPdf(byte[] pdfPath)
  {
    PdfDocument pdfDocument;
    using (MemoryStream memoryStream = new MemoryStream(pdfPath))
      pdfDocument = PdfReader.Open((Stream) memoryStream, (PdfDocumentOpenMode) 1);
    int pageCount = pdfDocument.PageCount;
    for (int index = 0; index < pageCount; ++index)
      this._document.AddPage(pdfDocument.Pages[index]);
  }
}
