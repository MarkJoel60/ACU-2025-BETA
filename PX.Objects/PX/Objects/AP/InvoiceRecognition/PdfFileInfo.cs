// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.PdfFileInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXHidden]
public class PdfFileInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  public virtual Guid? RecognizedRecordRefNbr { get; set; }

  [PXUIField(DisplayName = "File ID", Enabled = false)]
  [PXDBGuid(false)]
  public virtual Guid? FileId { get; set; }

  public abstract class recognizedRecordRefNbr : 
    BqlType<IBqlGuid, Guid>.Field<
    #nullable disable
    PdfFileInfo.recognizedRecordRefNbr>
  {
  }

  public abstract class fileId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PdfFileInfo.fileId>
  {
  }
}
