// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POLineComparer : IEqualityComparer<PX.Objects.AP.APTran>
{
  public bool Equals(PX.Objects.AP.APTran x, PX.Objects.AP.APTran y)
  {
    if (!(x.POOrderType == y.POOrderType) || !(x.PONbr == y.PONbr))
      return false;
    int? poLineNbr1 = x.POLineNbr;
    int? poLineNbr2 = y.POLineNbr;
    return poLineNbr1.GetValueOrDefault() == poLineNbr2.GetValueOrDefault() & poLineNbr1.HasValue == poLineNbr2.HasValue;
  }

  public int GetHashCode(PX.Objects.AP.APTran obj)
  {
    int num1 = 17 * 23;
    int? hashCode1 = obj.POOrderType?.GetHashCode();
    int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
    int? hashCode2 = obj.PONbr?.GetHashCode();
    int num3 = (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
    int? poLineNbr = obj.POLineNbr;
    ref int? local = ref poLineNbr;
    int? nullable = local.HasValue ? new int?(local.GetValueOrDefault().GetHashCode()) : new int?();
    return (nullable.HasValue ? new int?(num3 + nullable.GetValueOrDefault()) : new int?()).GetValueOrDefault();
  }
}
