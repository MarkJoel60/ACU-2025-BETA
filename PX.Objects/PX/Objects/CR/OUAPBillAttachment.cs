// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUAPBillAttachment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.InvoiceRecognition.DAC;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXInternalUseOnly]
[PXHidden]
public class OUAPBillAttachment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true, IsUnicode = true)]
  public virtual 
  #nullable disable
  string ItemId { get; set; }

  [PXString(IsKey = true, IsUnicode = true)]
  public virtual string Id { get; set; }

  [PXUIField(DisplayName = "File Name", Enabled = false)]
  [PXString(IsUnicode = true)]
  public virtual string Name { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string ContentType { get; set; }

  [PXUIField(DisplayName = "Selected")]
  [PXDefault(false)]
  [PXBool]
  public virtual bool? Selected { get; set; }

  [PXUIField(DisplayName = "File Hash")]
  [PXDBBinary(16 /*0x10*/, IsFixed = true)]
  public virtual byte[] FileHash { get; set; }

  [PXUIField(DisplayName = "File Data")]
  [PXDBBinary]
  public byte[] FileData { get; set; }

  [PXUIField(DisplayName = "Recognition Status")]
  [APRecognizedInvoiceRecognitionStatusList]
  [PXString(1, IsFixed = true)]
  public string RecognitionStatus { get; set; }

  [PXUIField(DisplayName = "Duplicate Link", Visible = false)]
  [PXGuid]
  public virtual Guid? DuplicateLink { get; set; }

  public abstract class itemId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUAPBillAttachment.itemId>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUAPBillAttachment.id>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUAPBillAttachment.name>
  {
  }

  public abstract class contentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUAPBillAttachment.contentType>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OUAPBillAttachment.selected>
  {
  }

  public abstract class fileHash : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  OUAPBillAttachment.fileHash>
  {
  }

  public abstract class fileData : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  OUAPBillAttachment.fileData>
  {
  }

  public abstract class recognitionStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUAPBillAttachment.recognitionStatus>
  {
  }

  public abstract class duplicateLink : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    OUAPBillAttachment.duplicateLink>
  {
  }
}
