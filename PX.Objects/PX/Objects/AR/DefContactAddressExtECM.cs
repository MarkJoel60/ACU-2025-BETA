// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DefContactAddressExtECM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class DefContactAddressExtECM : 
  PXGraphExtension<CustomerMaint.DefContactAddressExt, CustomerMaint>
{
  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Address> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CR.Address>>) e).Cache.ObjectsEqual<PX.Objects.CR.Address.addressLine1, PX.Objects.CR.Address.addressLine2, PX.Objects.CR.Address.city, PX.Objects.CR.Address.countryID, PX.Objects.CR.Address.postalCode, PX.Objects.CR.Address.state>((object) e.Row, (object) e.OldRow) || ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current == null)
      return;
    ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current.IsECMValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Cache, (object) ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current);
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.eCM>();
}
