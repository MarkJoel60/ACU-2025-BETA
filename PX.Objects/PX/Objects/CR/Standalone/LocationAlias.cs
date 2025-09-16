// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.LocationAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

[PXHidden]
[Serializable]
public class LocationAlias : PX.Objects.CR.Location
{
  public new abstract class bAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.bAccountID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.locationID>
  {
  }

  public new abstract class locationCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.locationCD>
  {
  }

  public new abstract class locType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.locType>
  {
  }

  public new abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.descr>
  {
  }

  public new abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.taxRegistrationID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.defAddressID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.defContactID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocationAlias.noteID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.status>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.isActive>
  {
  }

  public new abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.isDefault>
  {
  }

  public new abstract class isAPAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isAPAccountSameAsMain>
  {
  }

  public new abstract class isAPPaymentInfoSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isAPPaymentInfoSameAsMain>
  {
  }

  public new abstract class isARAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isARAccountSameAsMain>
  {
  }

  public new abstract class isRemitAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isRemitAddressSameAsMain>
  {
  }

  public new abstract class isRemitContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isRemitContactSameAsMain>
  {
  }

  public new abstract class cTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.cTaxZoneID>
  {
  }

  public new abstract class cTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cTaxCalcMode>
  {
  }

  public new abstract class cAvalaraExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cAvalaraExemptionNumber>
  {
  }

  public new abstract class cAvalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cAvalaraCustomerUsageType>
  {
  }

  public new abstract class cCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.cCarrierID>
  {
  }

  public new abstract class cShipTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cShipTermsID>
  {
  }

  public new abstract class cShipZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cShipZoneID>
  {
  }

  public new abstract class cFOBPointID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cFOBPointID>
  {
  }

  public new abstract class cResedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.cResedential>
  {
  }

  public new abstract class cSaturdayDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.cSaturdayDelivery>
  {
  }

  public new abstract class cGroundCollect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.cGroundCollect>
  {
  }

  public new abstract class cInsurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.cInsurance>
  {
  }

  public new abstract class cAdditionalHandling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.cAdditionalHandling>
  {
  }

  public new abstract class cLiftGate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.cLiftGate>
  {
  }

  public new abstract class cInsideDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.cInsideDelivery>
  {
  }

  public new abstract class cLimitedAccess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.cLimitedAccess>
  {
  }

  public new abstract class cLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  LocationAlias.cLeadTime>
  {
  }

  public new abstract class cBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cBranchID>
  {
  }

  public new abstract class cSalesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cSalesAcctID>
  {
  }

  public new abstract class cSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cSalesSubID>
  {
  }

  public new abstract class cPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cPriceClassID>
  {
  }

  public new abstract class cSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cSiteID>
  {
  }

  public new abstract class cDiscountAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cDiscountAcctID>
  {
  }

  public new abstract class cDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cDiscountSubID>
  {
  }

  public new abstract class cFreightAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cFreightAcctID>
  {
  }

  public new abstract class cFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cFreightSubID>
  {
  }

  public new abstract class cRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cRetainageAcctID>
  {
  }

  public new abstract class cRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cRetainageSubID>
  {
  }

  public new abstract class cShipComplete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cShipComplete>
  {
  }

  public new abstract class cOrderPriority : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAlias.cOrderPriority>
  {
  }

  public new abstract class cCalendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.cCalendarID>
  {
  }

  public new abstract class cDefProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cDefProjectID>
  {
  }

  public new abstract class cARAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cARAccountLocationID>
  {
  }

  public new abstract class cARAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cARAccountID>
  {
  }

  public new abstract class cARSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cARSubID>
  {
  }

  public new abstract class vTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.vTaxZoneID>
  {
  }

  public new abstract class vTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.vTaxCalcMode>
  {
  }

  public new abstract class vCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.vCarrierID>
  {
  }

  public new abstract class vShipTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.vShipTermsID>
  {
  }

  public new abstract class vFOBPointID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.vFOBPointID>
  {
  }

  public new abstract class vLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  LocationAlias.vLeadTime>
  {
  }

  public new abstract class vBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vBranchID>
  {
  }

  public new abstract class vExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vExpenseAcctID>
  {
  }

  public new abstract class vExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vExpenseSubID>
  {
  }

  public new abstract class vFreightAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vFreightAcctID>
  {
  }

  public new abstract class vFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vFreightSubID>
  {
  }

  public new abstract class vDiscountAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vDiscountAcctID>
  {
  }

  public new abstract class vDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vDiscountSubID>
  {
  }

  public new abstract class vRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vRetainageAcctID>
  {
  }

  public new abstract class vRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vRetainageSubID>
  {
  }

  public new abstract class vRcptQtyMin : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationAlias.vRcptQtyMin>
  {
  }

  public new abstract class vRcptQtyMax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationAlias.vRcptQtyMax>
  {
  }

  public new abstract class vRcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationAlias.vRcptQtyThreshold>
  {
  }

  public new abstract class vRcptQtyAction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.vRcptQtyAction>
  {
  }

  public new abstract class vSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vSiteID>
  {
  }

  public new abstract class vPrintOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.vPrintOrder>
  {
  }

  public new abstract class vEmailOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationAlias.vEmailOrder>
  {
  }

  public new abstract class vDefProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vDefProjectID>
  {
  }

  public new abstract class vAPAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vAPAccountLocationID>
  {
  }

  public new abstract class vAPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vAPAccountID>
  {
  }

  public new abstract class vAPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vAPSubID>
  {
  }

  public new abstract class vPaymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vPaymentInfoLocationID>
  {
  }

  public new abstract class vRemitAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vRemitAddressID>
  {
  }

  public new abstract class vRemitContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vRemitContactID>
  {
  }

  public new abstract class vPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.vPaymentMethodID>
  {
  }

  public new abstract class vCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vCashAccountID>
  {
  }

  public new abstract class vPaymentLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAlias.vPaymentLeadTime>
  {
  }

  public new abstract class vPaymentByType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.vPaymentByType>
  {
  }

  public new abstract class vSeparateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.vSeparateCheck>
  {
  }

  public new abstract class vPrepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationAlias.vPrepaymentPct>
  {
  }

  public new abstract class vAllowAPBillBeforeReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.vAllowAPBillBeforeReceipt>
  {
  }

  public new abstract class locationAPAccountSubBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.locationAPAccountSubBAccountID>
  {
  }

  public new abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.aPAccountID>
  {
  }

  public new abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.aPSubID>
  {
  }

  public new abstract class locationARAccountSubBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.locationARAccountSubBAccountID>
  {
  }

  public new abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.aRAccountID>
  {
  }

  public new abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.aRSubID>
  {
  }

  public new abstract class locationAPPaymentInfoBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.locationAPPaymentInfoBAccountID>
  {
  }

  public new abstract class remitAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.remitAddressID>
  {
  }

  public new abstract class remitContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.remitContactID>
  {
  }

  public new abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.paymentMethodID>
  {
  }

  public new abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cashAccountID>
  {
  }

  public new abstract class paymentLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAlias.paymentLeadTime>
  {
  }

  public new abstract class separateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.separateCheck>
  {
  }

  public new abstract class paymentByType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.paymentByType>
  {
  }

  public new abstract class bAccountBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.bAccountBAccountID>
  {
  }

  public new abstract class vDefAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vDefAddressID>
  {
  }

  public new abstract class vDefContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.vDefContactID>
  {
  }

  public new abstract class cMPSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cMPSalesSubID>
  {
  }

  public new abstract class cMPExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cMPExpenseSubID>
  {
  }

  public new abstract class cMPFreightSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cMPFreightSubID>
  {
  }

  public new abstract class cMPDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cMPDiscountSubID>
  {
  }

  public new abstract class cMPGainLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.cMPGainLossSubID>
  {
  }

  public new abstract class cMPSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAlias.cMPSiteID>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LocationAlias.Tstamp>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocationAlias.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationAlias.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocationAlias.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAlias.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationAlias.lastModifiedDateTime>
  {
  }

  public new abstract class vSiteIDIsNull : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAlias.vSiteIDIsNull>
  {
  }

  public new abstract class isAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isAddressSameAsMain>
  {
  }

  public new abstract class isContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.isContactSameAsMain>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.overrideAddress>
  {
  }

  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.overrideContact>
  {
  }

  public new abstract class overrideRemitAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.overrideRemitAddress>
  {
  }

  public new abstract class overrideRemitContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAlias.overrideRemitContact>
  {
  }

  public new abstract class aPRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.aPRetainageAcctID>
  {
  }

  public new abstract class aPRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.aPRetainageSubID>
  {
  }

  public new abstract class aRRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.aRRetainageAcctID>
  {
  }

  public new abstract class aRRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAlias.aRRetainageSubID>
  {
  }

  public new abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationAlias.extRefNbr>
  {
  }
}
