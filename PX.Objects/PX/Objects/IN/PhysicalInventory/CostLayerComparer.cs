// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.CostLayerComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class CostLayerComparer : IComparer<INPIEntry.CostLayerInfo>
{
  private INItemSiteSettings _itemSite;

  public CostLayerComparer(INItemSiteSettings itemSite) => this._itemSite = itemSite;

  public virtual int Compare(INPIEntry.CostLayerInfo x, INPIEntry.CostLayerInfo y)
  {
    int num = this.CompareByCostLayerType(x, y);
    if (num != 0)
      return num;
    return this._itemSite.ValMethod == "F" ? this.CompareFIFO(x.CostLayer, y.CostLayer) : this.CompareDefault(x.CostLayer, y.CostLayer);
  }

  protected virtual int CompareDefault(INCostStatus x, INCostStatus y)
  {
    int? accountId1 = x.AccountID;
    int? accountId2 = y.AccountID;
    if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
    {
      int? subId1 = x.SubID;
      int? subId2 = y.SubID;
      if (subId1.GetValueOrDefault() == subId2.GetValueOrDefault() & subId1.HasValue == subId2.HasValue)
        goto label_8;
    }
    int? accountId3 = y.AccountID;
    int? invtAcctId1 = this._itemSite.InvtAcctID;
    if (accountId3.GetValueOrDefault() == invtAcctId1.GetValueOrDefault() & accountId3.HasValue == invtAcctId1.HasValue)
    {
      int? subId = y.SubID;
      int? invtSubId = this._itemSite.InvtSubID;
      if (subId.GetValueOrDefault() == invtSubId.GetValueOrDefault() & subId.HasValue == invtSubId.HasValue)
        return -1;
    }
    int? accountId4 = x.AccountID;
    int? invtAcctId2 = this._itemSite.InvtAcctID;
    if (accountId4.GetValueOrDefault() == invtAcctId2.GetValueOrDefault() & accountId4.HasValue == invtAcctId2.HasValue)
    {
      int? subId = x.SubID;
      int? invtSubId = this._itemSite.InvtSubID;
      if (subId.GetValueOrDefault() == invtSubId.GetValueOrDefault() & subId.HasValue == invtSubId.HasValue)
        return 1;
    }
label_8:
    int num1 = x.AccountID.Value;
    ref int local1 = ref num1;
    int? nullable = y.AccountID;
    int num2 = nullable.Value;
    int num3 = local1.CompareTo(num2);
    if (num3 != 0)
      return num3;
    nullable = x.SubID;
    num1 = nullable.Value;
    ref int local2 = ref num1;
    nullable = y.SubID;
    int num4 = nullable.Value;
    int num5 = local2.CompareTo(num4);
    return num5 != 0 ? num5 : x.CostID.Value.CompareTo(y.CostID.Value);
  }

  protected virtual int CompareFIFO(INCostStatus x, INCostStatus y)
  {
    int num1 = x.ReceiptDate.Value.CompareTo(y.ReceiptDate.Value);
    if (num1 != 0)
      return num1;
    int num2 = x.ReceiptNbr.CompareTo(y.ReceiptNbr);
    return num2 != 0 ? num2 : this.CompareDefault(x, y);
  }

  protected virtual int CompareByCostLayerType(INPIEntry.CostLayerInfo x, INPIEntry.CostLayerInfo y)
  {
    return x.CostLayerType.CompareTo(y.CostLayerType);
  }
}
