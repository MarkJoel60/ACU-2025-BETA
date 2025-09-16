// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.NonProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.CT;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public sealed class NonProject : IBqlCreator, IBqlVerifier, IBqlOperand
{
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) NonProject.ID;
  }

  public static int ID
  {
    get
    {
      return PXDatabase.GetSlot<NonProject.ProjectDefinition>(typeof (NonProject).FullName, Array.Empty<Type>()).ID;
    }
  }

  private class ProjectDefinition : IPrefetchable, IPXCompanyDependent
  {
    public int ID;

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Contract>(new PXDataField[2]
        {
          (PXDataField) new PXDataField<Contract.contractID>(),
          (PXDataField) new PXDataFieldValue<Contract.nonProject>((object) 1)
        }))
          this.ID = pxDataRecord.GetInt32(0).GetValueOrDefault();
      }
    }
  }
}
