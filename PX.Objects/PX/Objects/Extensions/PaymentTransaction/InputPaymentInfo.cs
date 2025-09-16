// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.InputPaymentInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable enable
namespace PX.Objects.Extensions.PaymentTransaction;

public class InputPaymentInfo : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICCManualInputPaymentInfo
{
  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center Tran. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string PCTranNumber { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center Auth. Nbr.")]
  public virtual string AuthNumber { get; set; }

  public abstract class pCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputPaymentInfo.pCTranNumber>
  {
  }

  public abstract class authNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InputPaymentInfo.authNumber>
  {
  }
}
