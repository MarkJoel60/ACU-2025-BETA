// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeSelectorWithEmployeeIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.EP;

public abstract class EPShiftCodeSelectorWithEmployeeIDAttribute : EPShiftCodeSelectorAttribute
{
  private Type _EmployeeIDField;

  protected EPShiftCodeSelectorWithEmployeeIDAttribute(Type employeeIDField, Type compareDateField)
    : base(compareDateField)
  {
    this._EmployeeIDField = employeeIDField;
  }

  protected override EPEmployee GetEmployee(PXCache cache, object row)
  {
    int? nullable = (int?) cache.GetValue(row, this._EmployeeIDField.Name);
    return ((PXSelectBase<EPEmployee>) new FbqlSelect<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<P.AsInt>>, EPEmployee>.View(cache.Graph)).SelectSingle(new object[1]
    {
      (object) nullable
    });
  }
}
