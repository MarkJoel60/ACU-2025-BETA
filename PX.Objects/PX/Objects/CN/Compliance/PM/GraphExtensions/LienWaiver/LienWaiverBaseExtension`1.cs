// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.LienWaiver.LienWaiverBaseExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.PM.CacheExtensions;
using PX.Objects.CN.Compliance.PM.DAC;
using PX.Objects.EP;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions.LienWaiver;

public abstract class LienWaiverBaseExtension<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  public FbqlSelect<SelectFromBase<LienWaiverRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  LienWaiverRecipient.projectId, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  LienWaiverRecipient>.View LienWaiverRecipients;
  public PXAction<PMProject> AddAllVendorClasses;

  [PXUIField(DisplayName = "Add All Vendor Classes")]
  [PXButton]
  public virtual void addAllVendorClasses()
  {
    ((PXSelectBase) this.LienWaiverRecipients).Cache.InsertAll((IEnumerable<object>) this.GetAvailableVendorClasses().Select<PX.Objects.AP.VendorClass, LienWaiverRecipient>((Func<PX.Objects.AP.VendorClass, LienWaiverRecipient>) (vc => new LienWaiverRecipient()
    {
      VendorClassId = vc.VendorClassID
    })));
  }

  public virtual void _(PX.Data.Events.RowSelected<PMProject> args)
  {
    PMProject row = args.Row;
    if (row == null)
      return;
    this.UpdateComplianceSettingsTabVisibility(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) args).Cache, row);
  }

  private void UpdateComplianceSettingsTabVisibility(PXCache cache, PMProject project)
  {
    bool flag = ((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current.ShouldGenerateConditional.GetValueOrDefault() || ((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current.ShouldGenerateUnconditional.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<PmProjectExtension.throughDateSourceConditional>(cache, (object) project, flag);
    PXUIFieldAttribute.SetVisible<PmProjectExtension.throughDateSourceUnconditional>(cache, (object) project, flag);
    ((PXSelectBase) this.LienWaiverRecipients).AllowSelect = flag;
  }

  private IEnumerable<PX.Objects.AP.VendorClass> GetAvailableVendorClasses()
  {
    IEnumerable<string> vendorClassIds = ((IEnumerable<LienWaiverRecipient>) ((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipients).SelectMain(Array.Empty<object>())).Select<LienWaiverRecipient, string>((Func<LienWaiverRecipient, string>) (r => r.VendorClassId));
    return PXSelectBase<PX.Objects.AP.VendorClass, PXViewOf<PX.Objects.AP.VendorClass>.BasedOn<SelectFromBase<PX.Objects.AP.VendorClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployeeClass>.On<BqlOperand<EPEmployeeClass.vendorClassID, IBqlString>.IsEqual<PX.Objects.AP.VendorClass.vendorClassID>>>>.Where<BqlOperand<EPEmployeeClass.vendorClassID, IBqlString>.IsNull>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()).FirstTableItems.Where<PX.Objects.AP.VendorClass>((Func<PX.Objects.AP.VendorClass, bool>) (vc => EnumerableExtensions.IsNotIn<string>(vc.VendorClassID, vendorClassIds)));
  }
}
