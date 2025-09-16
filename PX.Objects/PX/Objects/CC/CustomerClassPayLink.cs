// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.CustomerClassPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// Represents database fields which store Payment Link specific data.
/// </summary>
public sealed class CustomerClassPayLink : PXCacheExtension<
#nullable disable
CustomerClass>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  /// <summary>
  /// Disable the Payment Link feature for Cusotmers that belong to Customer Class.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Payment Link Processing")]
  public bool? DisablePayLink { get; set; }

  /// <summary>Payment Link delivery method (N - none, E - email).</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PayLinkDeliveryMethod.List]
  [PXUIField(DisplayName = "Delivery Method")]
  public string DeliveryMethod { get; set; }

  /// <summary>Allow delivery method override on the document level.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Delivery Method Override")]
  public bool? AllowOverrideDeliveryMethod { get; set; }

  /// <summary>
  /// Allowed means of payments for Payment Link (N - cc + eft, E - eft, C - credit card)
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PX.Objects.CC.PayLinkPaymentMethod.List]
  [PXUIField(DisplayName = "Allowed Means of Payment")]
  public string PayLinkPaymentMethod { get; set; }

  public abstract class disablePayLink : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClassPayLink.disablePayLink>
  {
  }

  public abstract class deliveryMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClassPayLink.deliveryMethod>
  {
  }

  public abstract class allowOverrideDeliveryMethod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClassPayLink.allowOverrideDeliveryMethod>
  {
  }

  public abstract class payLinkPaymentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClassPayLink.payLinkPaymentMethod>
  {
  }
}
