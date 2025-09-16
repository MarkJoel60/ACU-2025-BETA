// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeActivityShiftCodeSelectorAttribute
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

public class TimeActivityShiftCodeSelectorAttribute : EPShiftCodeSelectorAttribute
{
  private Type _OwnerIDField;

  public TimeActivityShiftCodeSelectorAttribute(Type ownerIDField, Type dateField)
    : base(dateField)
  {
    this._OwnerIDField = ownerIDField;
    this._DateField = dateField;
  }

  protected override EPEmployee GetEmployee(PXCache cache, object row)
  {
    int? nullable = (int?) cache.GetValue(row, this._OwnerIDField.Name);
    return ((PXSelectBase<EPEmployee>) new FbqlSelect<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.defContactID, IBqlInt>.IsEqual<P.AsInt>>, EPEmployee>.View(cache.Graph)).SelectSingle(new object[1]
    {
      (object) nullable
    });
  }

  protected override object[] GetQueryParameters(PXCache cache, object row)
  {
    return new object[1]
    {
      cache.GetValue(row, this._DateField.Name)
    };
  }
}
