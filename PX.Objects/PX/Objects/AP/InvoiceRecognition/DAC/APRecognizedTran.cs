// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.APRecognizedTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXHidden]
public class APRecognizedTran : APTran
{
  [PXString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Alternate ID")]
  public virtual 
  #nullable disable
  string AlternateID { get; set; }

  [APTranRecognizedInventoryItem(Filterable = true)]
  [PXUIField(Visible = false)]
  public virtual int? InternalAlternateID { get; set; }

  [PXInt]
  [PXUIField(Visible = false)]
  public virtual int? NumOfFoundIDByAlternate { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? InventoryIDManualInput { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Number", Enabled = false)]
  public virtual string RecognizedPONumber { get; set; }

  [PXString]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string PONumberJson { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDefault("N", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "PO Link Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [APPOLinkStatus.List]
  public virtual string POLinkStatus { get; set; }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedTran.alternateID>
  {
  }

  public abstract class internalAlternateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedTran.internalAlternateID>
  {
  }

  public abstract class numOfFoundIDByAlternate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedTran.numOfFoundIDByAlternate>
  {
  }

  public abstract class inventoryIDManualInput : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRecognizedTran.inventoryIDManualInput>
  {
  }

  public abstract class recognizedPONumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedTran.recognizedPONumber>
  {
  }

  public abstract class pONumberJson : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedTran.pONumberJson>
  {
  }

  public abstract class pOLinkStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedTran.pOLinkStatus>
  {
  }

  public new abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRecognizedTran.curyTranAmt>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedTran.refNbr>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRecognizedTran.tranType>
  {
  }
}
