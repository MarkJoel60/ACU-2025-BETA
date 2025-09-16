// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LocationRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class LocationRestrictorAttribute : PXRestrictorAttribute
{
  protected Type _IsReceiptType;
  protected Type _IsSalesType;
  protected Type _IsTransferType;

  public LocationRestrictorAttribute(Type IsReceiptType, Type IsSalesType, Type IsTransferType)
    : base(typeof (Where<True>), string.Empty, Array.Empty<Type>())
  {
    this._IsReceiptType = IsReceiptType;
    this._IsSalesType = IsSalesType;
    this._IsTransferType = IsTransferType;
  }

  protected virtual BqlCommand WhereAnd(PXCache sender, PXSelectorAttribute selattr, Type Where)
  {
    return selattr.PrimarySelect.WhereAnd(Where);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    INLocation inLocation = (INLocation) null;
    try
    {
      inLocation = INLocation.PK.Find(sender.Graph, (int?) e.NewValue);
    }
    catch (FormatException ex)
    {
    }
    if (this._AlteredCmd == null || inLocation == null)
      return;
    bool? nullable1 = this.VerifyExpr(sender, e.Row, this._IsReceiptType);
    bool? nullable2 = this.VerifyExpr(sender, e.Row, this._IsSalesType);
    bool? nullable3 = this.VerifyExpr(sender, e.Row, this._IsTransferType);
    if (nullable1.GetValueOrDefault())
    {
      bool? receiptsValid = inLocation.ReceiptsValid;
      bool flag = false;
      if (receiptsValid.GetValueOrDefault() == flag & receiptsValid.HasValue)
        this.ThrowErrorItem("Selected Location is not valid for receipts.", e, (object) inLocation.LocationCD);
    }
    if (nullable2.GetValueOrDefault())
    {
      bool? salesValid = inLocation.SalesValid;
      bool flag1 = false;
      if (salesValid.GetValueOrDefault() == flag1 & salesValid.HasValue)
      {
        if (!e.ExternalCall)
        {
          bool? isSorting = inLocation.IsSorting;
          bool flag2 = false;
          if (!(isSorting.GetValueOrDefault() == flag2 & isSorting.HasValue))
            goto label_12;
        }
        this.ThrowErrorItem("Selected Location is not valid for sales.", e, (object) inLocation.LocationCD);
      }
    }
label_12:
    if (!nullable3.GetValueOrDefault())
      return;
    bool? transfersValid = inLocation.TransfersValid;
    bool flag3 = false;
    if (!(transfersValid.GetValueOrDefault() == flag3 & transfersValid.HasValue))
      return;
    if (!e.ExternalCall)
    {
      bool? isSorting = inLocation.IsSorting;
      bool flag4 = false;
      if (!(isSorting.GetValueOrDefault() == flag4 & isSorting.HasValue))
        return;
    }
    this.ThrowErrorItem("Selected Location is not valid for transfers.", e, (object) inLocation.LocationCD);
  }

  public virtual void ThrowErrorItem(
    string message,
    PXFieldVerifyingEventArgs e,
    object ErrorValue)
  {
    e.NewValue = ErrorValue;
    throw new PXSetPropertyException(message);
  }

  protected bool? VerifyExpr(PXCache cache, object data, Type whereType)
  {
    object obj = (object) null;
    bool? nullable = new bool?();
    ((IBqlUnary) Activator.CreateInstance(whereType)).Verify(cache, data, new List<object>(), ref nullable, ref obj);
    return nullable;
  }
}
