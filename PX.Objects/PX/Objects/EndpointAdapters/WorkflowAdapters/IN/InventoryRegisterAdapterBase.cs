// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.IN.InventoryRegisterAdapterBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.IN;

internal abstract class InventoryRegisterAdapterBase
{
  protected void INRegisterInsert(
    INRegisterEntryBase registerEntry,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    bool flag = true;
    EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    INRegister inRegister = PXResultset<INRegister>.op_Implicit(entityValueField == null || entityValueField.Value == null ? (PXResultset<INRegister>) null : registerEntry.INRegisterDataMember.Search<INRegister.refNbr>((object) entityValueField.Value, Array.Empty<object>()));
    if (inRegister == null)
    {
      inRegister = (INRegister) ((PXSelectBase) registerEntry.INRegisterDataMember).Cache.CreateInstance();
      if (entityValueField != null)
        inRegister.RefNbr = entityValueField.Value;
    }
    else
      flag = false;
    registerEntry.INRegisterDataMember.Current = flag ? registerEntry.INRegisterDataMember.Insert(inRegister) : registerEntry.INRegisterDataMember.Update(inRegister);
    ((PXGraph) registerEntry).SubscribeToPersistDependingOnBoolField<INRegister>(holdField, registerEntry.putOnHold, registerEntry.releaseFromHold);
  }

  protected void INRegisterUpdate(
    INRegisterEntryBase registerEntry,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) registerEntry).SubscribeToPersistDependingOnBoolField<INRegister>(holdField, registerEntry.putOnHold, registerEntry.releaseFromHold);
  }
}
