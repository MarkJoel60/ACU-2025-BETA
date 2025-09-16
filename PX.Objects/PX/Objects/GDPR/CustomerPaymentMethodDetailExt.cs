// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.CustomerPaymentMethodDetailExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select2<CustomerPaymentMethodDetail, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<CustomerPaymentMethodDetail.pMInstanceID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PX.Objects.CA.PaymentMethod.containsPersonalData, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>))]
[Serializable]
public sealed class CustomerPaymentMethodDetailExt : 
  PXCacheExtension<
  #nullable disable
  CustomerPaymentMethodDetail>,
  IPseudonymizable,
  INotable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  [PXDBGuidNotNull]
  public Guid? NoteID { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethodDetailExt.pseudonymizationStatus>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustomerPaymentMethodDetailExt.noteID>
  {
  }
}
