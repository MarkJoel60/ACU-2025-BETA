// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.RedirectExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.EP;
using System.Collections;

#nullable disable
namespace PX.Objects.Extensions;

public class RedirectExtension<Graph> : PXGraphExtension<Graph> where Graph : PXGraph
{
  public virtual IEnumerable ViewCustomerVendorEmployee<TBAccountID>(PXAdapter adapter) where TBAccountID : IBqlField
  {
    PXCache cach = this.Base.Caches[BqlCommand.GetItemType(typeof (TBAccountID))];
    object current = cach.Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<TBAccountID>>>>.Config>.Select((PXGraph) this.Base);
    int? field0 = (int?) cach.GetValue(current, typeof (TBAccountID).Name);
    if (baccount != null)
    {
      if (baccount.Type == "VE" || baccount.Type == "VC")
      {
        VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
        instance.BAccount.Current = (VendorR) instance.BAccount.Search<BAccountR.bAccountID>((object) field0);
        if (instance.BAccount.Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
          requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
          throw requiredException;
        }
        throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", (object) ((VendorR) PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<TBAccountID>>>>.Config>.Select((PXGraph) this.Base)).AcctCD));
      }
      if (baccount.Type == "CU")
      {
        CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
        instance.BAccount.Current = (PX.Objects.AR.Customer) instance.BAccount.Search<PX.Objects.AR.Customer.bAccountID>((object) field0);
        if (instance.BAccount.Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
          requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
          throw requiredException;
        }
        throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", (object) ((PX.Objects.AR.Customer) PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<TBAccountID>>>>.Config>.Select((PXGraph) this.Base)).AcctCD));
      }
      if (baccount.Type == "EP")
      {
        EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
        instance.Employee.Current = (PX.Objects.EP.EPEmployee) instance.Employee.Search<PX.Objects.EP.EPEmployee.bAccountID>((object) field0);
        if (instance.Employee.Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
          requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
          throw requiredException;
        }
      }
    }
    return adapter.Get();
  }

  public virtual IEnumerable ViewVendorLocation<TLocationID, TBAccountID>(PXAdapter adapter)
    where TLocationID : IBqlField
    where TBAccountID : IBqlField
  {
    PXCache cach = this.Base.Caches[BqlCommand.GetItemType(typeof (TBAccountID))];
    object current = cach.Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<TBAccountID>>>>.Config>.Select((PXGraph) this.Base);
    int? field0 = (int?) cach.GetValue(current, typeof (TLocationID).Name);
    VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
    instance.Location.Current = (PX.Objects.CR.Location) instance.Location.Search<PX.Objects.CR.Location.locationID>((object) field0, (object) baccount?.AcctCD);
    if (instance.Location.Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", (object) ((VendorR) PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<TBAccountID>>>>.Config>.Select((PXGraph) this.Base)).AcctCD));
  }
}
