// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.RecognizedRecordForProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXHidden]
public class RecognizedRecordForProcessing : RecognizedRecord
{
  [PXUIField(DisplayName = "Selected")]
  [PXBool]
  public virtual bool? Selected { get; set; }

  [PXUIField(DisplayName = "Recognition Result", Visible = false)]
  [PXString(IsUnicode = true)]
  public new virtual 
  #nullable disable
  string RecognitionResult { get; set; }

  [PXUIField(DisplayName = "Recognition Feedback", Visible = false)]
  [PXString(IsUnicode = true)]
  public new virtual string RecognitionFeedback { get; set; }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecognizedRecordForProcessing.selected>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecordForProcessing.createdDateTime>
  {
  }

  public new abstract class owner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecordForProcessing.owner>
  {
  }

  public new abstract class documentLink : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedRecordForProcessing.documentLink>
  {
  }

  public new abstract class recognitionResult : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordForProcessing.recognitionResult>
  {
  }

  public new abstract class recognitionFeedback : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordForProcessing.recognitionFeedback>
  {
  }
}
