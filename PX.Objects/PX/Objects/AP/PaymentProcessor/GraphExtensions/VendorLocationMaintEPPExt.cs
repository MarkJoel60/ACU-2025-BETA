// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.VendorLocationMaintEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.PaymentProcessor.Data;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class VendorLocationMaintEPPExt : PXGraphExtension<VendorLocationMaint>
{
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorVendor, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>.Where<
  #nullable enable
  BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorVendor.bAccountID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<PX.Objects.CR.Location.bAccountID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<APPaymentProcessorVendor.locationID, IBqlInt>.IsEqual<BqlField<PX.Objects.CR.Location.locationID, IBqlInt>.FromCurrent>>>, APPaymentProcessorVendor>.View PaymentProcessorVendor;
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorVendor, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
  #nullable enable
  PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<APPaymentProcessorVendor.organizationID>>>, 
  #nullable disable
  FbqlJoins.Inner<
  #nullable enable
  PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<BqlChainableConditionMirror<
  #nullable disable
  TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorVendor.bAccountID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<PX.Objects.CR.Location.bAccountID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<
  #nullable enable
  BqlOperand<APPaymentProcessorVendor.locationID, IBqlInt>.IsEqual<BqlField<PX.Objects.CR.Location.locationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>, APPaymentProcessorVendor>.View CurrentOrgVendor;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  public virtual void AddExternalPaymentProcessorVendor(
    APPaymentProcessorOrganization extOrganization,
    PX.Objects.CR.Location location,
    ExternalVendor externalVendor)
  {
    string paymentProcessorId = extOrganization.ExternalPaymentProcessorID;
    int? organizationId = extOrganization.OrganizationID;
    this.PaymentProcessorVendor.Insert(new APPaymentProcessorVendor()
    {
      OrganizationID = organizationId,
      BAccountID = location.BAccountID,
      LocationID = location.LocationID,
      ExternalPaymentProcessorID = paymentProcessorId,
      ExternalVendorID = externalVendor.Id,
      NetworkStatus = externalVendor.NetworkStatus,
      PayByType = externalVendor.PayByType
    });
    this.Base.Save.Press();
  }

  public virtual void UpdateExternalPaymentProcessorVendor(
    APPaymentProcessorVendor extVendor,
    PX.Objects.CR.Location location,
    ExternalVendor response)
  {
    string extPaymentProcessorId = extVendor.ExternalPaymentProcessorID;
    int? organizationId = extVendor.OrganizationID;
    APPaymentProcessorVendor paymentProcessorVendor = this.PaymentProcessorVendor.Select().RowCast<APPaymentProcessorVendor>().Where<APPaymentProcessorVendor>((Func<APPaymentProcessorVendor, bool>) (i =>
    {
      int? organizationId1 = i.OrganizationID;
      int? nullable = organizationId;
      if (organizationId1.GetValueOrDefault() == nullable.GetValueOrDefault() & organizationId1.HasValue == nullable.HasValue)
      {
        int? locationId1 = i.LocationID;
        int? locationId2 = location.LocationID;
        if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
        {
          int? baccountId1 = i.BAccountID;
          int? baccountId2 = location.BAccountID;
          if (baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue)
            return i.ExternalPaymentProcessorID == extPaymentProcessorId;
        }
      }
      return false;
    })).FirstOrDefault<APPaymentProcessorVendor>();
    paymentProcessorVendor.NetworkStatus = response.NetworkStatus;
    paymentProcessorVendor.PayByType = response.PayByType;
    this.PaymentProcessorVendor.Update(paymentProcessorVendor);
    this.Base.Save.Press();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Address> e, PXRowUpdated BaseInvoke)
  {
    if (BaseInvoke != null)
      BaseInvoke(e.Cache, e.Args);
    PX.Objects.CR.Address row = e.Row;
    if (row == null)
      return;
    APPaymentProcessorVendor current1 = this.CurrentOrgVendor.Current;
    if (current1 == null)
      return;
    PX.Objects.CR.Address current2 = this.Base.RemitAddress.Current;
    this.Base.LocationCurrent.Current.OverrideRemitAddress.GetValueOrDefault();
    int? addressId1 = (int?) current2?.AddressID;
    int? addressId2 = row.AddressID;
    if (!(addressId1.GetValueOrDefault() == addressId2.GetValueOrDefault() & addressId1.HasValue == addressId2.HasValue))
      return;
    current1.IsRemittanceAddressChanged = new bool?(true);
    this.CurrentOrgVendor.Update(current1);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<VendorPaymentMethodDetail> e, PXRowUpdated BaseInvoke)
  {
    if (BaseInvoke != null)
      BaseInvoke(e.Cache, e.Args);
    if (e.Row == null)
      return;
    APPaymentProcessorVendor current = this.CurrentOrgVendor.Current;
    if (current == null)
      return;
    current.IsBankDetailsChanged = new bool?(true);
    this.CurrentOrgVendor.Update(current);
  }

  public virtual void MarkEppVendorAddressUpdated(APPaymentProcessorVendor eppVendor)
  {
    APPaymentProcessorVendor paymentProcessorVendor = this.SelectEppVendor(eppVendor);
    paymentProcessorVendor.IsRemittanceAddressChanged = new bool?(false);
    this.PaymentProcessorVendor.Update(paymentProcessorVendor);
    this.Base.Save.Press();
  }

  public virtual void MarkEppVendorBankDetailsUpdated(APPaymentProcessorVendor eppVendor)
  {
    APPaymentProcessorVendor paymentProcessorVendor = this.SelectEppVendor(eppVendor);
    paymentProcessorVendor.IsBankDetailsChanged = new bool?(false);
    this.PaymentProcessorVendor.Update(paymentProcessorVendor);
    this.Base.Save.Press();
  }

  private APPaymentProcessorVendor SelectEppVendor(APPaymentProcessorVendor eppVendor)
  {
    return PXSelectBase<APPaymentProcessorVendor, PXViewOf<APPaymentProcessorVendor>.BasedOn<SelectFromBase<APPaymentProcessorVendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorVendor.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorVendor.bAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorVendor.locationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorVendor.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>>>.Config>.Select((PXGraph) this.Base, (object) eppVendor.ExternalPaymentProcessorID, (object) eppVendor.BAccountID, (object) eppVendor.LocationID, (object) eppVendor.OrganizationID).RowCast<APPaymentProcessorVendor>().FirstOrDefault<APPaymentProcessorVendor>();
  }
}
