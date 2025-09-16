// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.VendorPaymentMethodDetailAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
public class VendorPaymentMethodDetailAlias : VendorPaymentMethodDetail
{
  public new abstract class bAccountID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    VendorPaymentMethodDetailAlias.bAccountID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorPaymentMethodDetailAlias.locationID>
  {
  }

  public new abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetailAlias.paymentMethodID>
  {
  }

  public new abstract class detailID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetailAlias.detailID>
  {
  }
}
