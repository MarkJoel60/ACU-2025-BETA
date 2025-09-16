// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCodeInTimeActivityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMWorkCodeInTimeActivityAttribute : PMWorkCodeAttribute
{
  private Type _OwnerIDField;

  public PMWorkCodeInTimeActivityAttribute(
    Type costCodeField,
    Type projectField,
    Type projectTaskField,
    Type laborItemField,
    Type ownerIDField)
    : base(costCodeField, projectField, projectTaskField, laborItemField, (Type) null)
  {
    this._OwnerIDField = ownerIDField;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.SetFieldUpdatedHandler(sender, this._OwnerIDField);
  }

  protected override int? GetEmployeeID(PXCache sender, object row)
  {
    if (this._OwnerIDField == (Type) null)
      return new int?();
    int? nullable = sender.GetValue(row, this._OwnerIDField.Name) as int?;
    if (!nullable.HasValue)
      return new int?();
    return ((PXSelectBase<EPEmployee>) new FbqlSelect<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.defContactID, IBqlInt>.IsEqual<P.AsInt>>, EPEmployee>.View(sender.Graph)).SelectSingle(new object[1]
    {
      (object) nullable
    })?.BAccountID;
  }
}
