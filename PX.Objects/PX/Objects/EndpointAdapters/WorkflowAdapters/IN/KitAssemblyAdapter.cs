// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.IN.KitAssemblyAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.IN;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class KitAssemblyAdapter
{
  [FieldsProcessed(new string[] {"ReferenceNbr", "Type", "Hold"})]
  protected virtual void KitAssembly_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    KitAssemblyEntry graph1 = (KitAssemblyEntry) graph;
    PXCache cache = ((PXSelectBase) graph1.Document).Cache;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    INKitRegister instance = (INKitRegister) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField2 != null)
    {
      string str = entityValueField2.Value;
      if (str == "Assembly")
        str = "P";
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<INKitRegister.docType>((object) instance, (object) str);
    }
    if (entityValueField1 != null)
      instance.RefNbr = entityValueField1.Value;
    ((PXSelectBase<INKitRegister>) graph1.Document).Current = ((PXSelectBase<INKitRegister>) graph1.Document).Insert(instance);
    INKitRegister current = cache.Current as INKitRegister;
    if (cache.Current == null)
      throw new InvalidOperationException("Cannot insert Kit Assembly.");
    if (((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
    {
      EntityValueField entityValueField3 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "KitInventoryID")));
      EntityValueField entityValueField4 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "Revision")));
      EntityValueField entityValueField5 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "WarehouseID")));
      EntityValueField entityValueField6 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "LocationID")));
      EntityValueField entityValueField7 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "Qty")));
      if (entityValueField3 != null)
      {
        cache.SetValueExt<INKitRegister.kitInventoryID>((object) current, (object) entityValueField3.Value);
        if (entityValueField4 != null)
          cache.SetValueExt<INKitRegister.kitRevisionID>((object) current, (object) entityValueField4.Value);
        if (entityValueField5 != null)
          current.LocationID = new int?();
        cache.SetValueExt<INKitRegister.siteID>((object) current, (object) entityValueField5.Value);
        if (entityValueField6 != null)
          cache.SetValueExt<INKitRegister.locationID>((object) current, (object) entityValueField6.Value);
        if (entityValueField7 != null)
          cache.SetValueExt<INKitRegister.qty>((object) current, (object) entityValueField7.Value);
        cache.Update((object) current);
        PXCache pxCache = (PXCache) GraphHelper.Caches<INKitTranSplit>((PXGraph) graph1);
        foreach (INKitTranSplit inKitTranSplit in pxCache.Inserted)
          pxCache.Delete((object) inKitTranSplit);
      }
    }
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<INKitRegister>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected virtual void KitAssembly_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    KitAssemblyEntry graph1 = (KitAssemblyEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<INKitRegister>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }
}
