// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POAccrualComparer : IEqualityComparer<PX.Objects.AP.APTran>
{
  public bool Equals(PX.Objects.AP.APTran x, PX.Objects.AP.APTran y)
  {
    if (x.POAccrualType == y.POAccrualType)
    {
      Guid? accrualRefNoteId1 = x.POAccrualRefNoteID;
      Guid? accrualRefNoteId2 = y.POAccrualRefNoteID;
      if ((accrualRefNoteId1.HasValue == accrualRefNoteId2.HasValue ? (accrualRefNoteId1.HasValue ? (accrualRefNoteId1.GetValueOrDefault() == accrualRefNoteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        int? poAccrualLineNbr1 = x.POAccrualLineNbr;
        int? poAccrualLineNbr2 = y.POAccrualLineNbr;
        return poAccrualLineNbr1.GetValueOrDefault() == poAccrualLineNbr2.GetValueOrDefault() & poAccrualLineNbr1.HasValue == poAccrualLineNbr2.HasValue;
      }
    }
    return false;
  }

  public int GetHashCode(PX.Objects.AP.APTran obj)
  {
    int num1 = 17 * 23;
    int? hashCode = obj.POAccrualType?.GetHashCode();
    int num2 = (hashCode.HasValue ? new int?(num1 + hashCode.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
    Guid? accrualRefNoteId = obj.POAccrualRefNoteID;
    ref Guid? local1 = ref accrualRefNoteId;
    int? nullable1 = local1.HasValue ? new int?(local1.GetValueOrDefault().GetHashCode()) : new int?();
    int num3 = (nullable1.HasValue ? new int?(num2 + nullable1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
    int? poAccrualLineNbr = obj.POAccrualLineNbr;
    ref int? local2 = ref poAccrualLineNbr;
    int? nullable2 = local2.HasValue ? new int?(local2.GetValueOrDefault().GetHashCode()) : new int?();
    return (nullable2.HasValue ? new int?(num3 + nullable2.GetValueOrDefault()) : new int?()).GetValueOrDefault();
  }
}
