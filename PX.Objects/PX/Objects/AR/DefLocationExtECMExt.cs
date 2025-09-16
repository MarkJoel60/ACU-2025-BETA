// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DefLocationExtECMExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class DefLocationExtECMExt : PXGraphExtension<CustomerMaint.DefLocationExt, CustomerMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.eCM>();

  protected virtual void _(Events.RowUpdated<PX.Objects.CR.Standalone.Location> e)
  {
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<PX.Objects.CR.Standalone.Location>>) e).Cache.ObjectsEqual<PX.Objects.CR.Standalone.Location.taxRegistrationID>((object) e.Row, (object) e.OldRow) || ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current == null)
      return;
    ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current.IsECMValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Cache, (object) ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current);
  }
}
