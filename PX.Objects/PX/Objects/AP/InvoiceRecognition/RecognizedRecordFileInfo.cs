// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.RecognizedRecordFileInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using System;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal readonly struct RecognizedRecordFileInfo
{
  public string FileName { get; }

  public byte[] FileData { get; }

  public Guid FileId { get; }

  public RecognizedRecord RecognizedRecord { get; }

  public RecognizedRecordFileInfo(
    string fileName,
    byte[] fileData,
    Guid fileId,
    RecognizedRecord record)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(fileName, nameof (fileName), (string) null);
    ExceptionExtensions.ThrowOnNull<byte[]>(fileData, nameof (fileData), (string) null);
    this.FileName = fileName;
    this.FileData = fileData;
    this.FileId = fileId;
    this.RecognizedRecord = record;
  }

  public RecognizedRecordFileInfo(string fileName, byte[] fileData, Guid fileId)
    : this(fileName, fileData, fileId, (RecognizedRecord) null)
  {
  }
}
