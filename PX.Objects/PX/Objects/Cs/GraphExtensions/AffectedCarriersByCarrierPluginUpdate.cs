// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.GraphExtensions.AffectedCarriersByCarrierPluginUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.GraphExtensions;

public class AffectedCarriersByCarrierPluginUpdate : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedCarriersByCarrierPluginUpdate, CarrierPluginMaint, Carrier, CarrierMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.carrierIntegration>();

  protected override bool PersistInSameTransaction => false;

  private PXCache<Carrier> carriers => GraphHelper.Caches<Carrier>((PXGraph) this.Base);

  protected override IEnumerable<Carrier> GetAffectedEntities()
  {
    List<Carrier> affectedEntities = new List<Carrier>();
    CarrierPlugin current = ((PXSelectBase<CarrierPlugin>) this.Base.Plugin).Current;
    bool flag1 = ((PXSelectBase) this.Base.Plugin).Cache.GetStatus((object) current) == 1 && ((PXSelectBase) this.Base.Plugin).Cache.GetValueOriginal<CarrierPlugin.isActive>((object) current) != ((PXSelectBase) this.Base.Plugin).Cache.GetValue<CarrierPlugin.isActive>((object) current);
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      bool? isActive = current.IsActive;
      bool flag2 = false;
      num1 = isActive.GetValueOrDefault() == flag2 & isActive.HasValue ? 1 : 0;
    }
    int num2 = flag1 ? 1 : 0;
    if ((num1 & num2) != 0)
      affectedEntities = GraphHelper.RowCast<Carrier>((IEnumerable) PXSelectBase<Carrier, PXSelect<Carrier, Where<Carrier.carrierPluginID, Equal<Required<Carrier.carrierPluginID>>, And<Carrier.isExternal, Equal<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXSelectBase<CarrierPlugin>) this.Base.Plugin).Current.CarrierPluginID
      })).ToList<Carrier>();
    return (IEnumerable<Carrier>) affectedEntities;
  }

  protected override bool EntityIsAffected(Carrier carrier) => true;

  protected override void ProcessAffectedEntity(CarrierMaint carrierMaint, Carrier carrier)
  {
    carrier.IsActive = new bool?(false);
    ((PXSelectBase<Carrier>) carrierMaint.Carrier).Update(carrier);
  }
}
