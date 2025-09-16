// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.VendorMaintEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.DAC;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class VendorMaintEPPExt : PXGraphExtension<VendorMaint>
{
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
  BqlField<VendorR.bAccountID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<
  #nullable enable
  BqlOperand<APPaymentProcessorVendor.locationID, IBqlInt>.IsEqual<BqlField<VendorR.defLocationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>, APPaymentProcessorVendor>.View CurrentOrgVendor;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Address> e, PXRowUpdated BaseInvoke)
  {
    if (BaseInvoke != null)
      BaseInvoke(e.Cache, e.Args);
    PX.Objects.CR.Address row = e.Row;
    if (row == null)
      return;
    APPaymentProcessorVendor current = this.CurrentOrgVendor.Current;
    if (current == null)
      return;
    VendorMaint.DefLocationExt extension = this.Base.GetExtension<VendorMaint.DefLocationExt>();
    PX.Objects.CR.Address topFirst1 = this.Base.GetExtension<VendorMaint.DefContactAddressExt>().DefAddress.Select().TopFirst;
    PX.Objects.CR.Address topFirst2 = extension.RemitAddress.Select().TopFirst;
    int? addressId1 = (int?) (extension.DefLocation.Current.OverrideRemitAddress.GetValueOrDefault() ? topFirst2 : topFirst1)?.AddressID;
    int? addressId2 = row.AddressID;
    if (!(addressId1.GetValueOrDefault() == addressId2.GetValueOrDefault() & addressId1.HasValue == addressId2.HasValue))
      return;
    current.IsRemittanceAddressChanged = new bool?(true);
    this.CurrentOrgVendor.Update(current);
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
}
