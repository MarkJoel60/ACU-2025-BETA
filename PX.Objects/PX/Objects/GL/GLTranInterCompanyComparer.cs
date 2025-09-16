// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranInterCompanyComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class GLTranInterCompanyComparer : IEqualityComparer<GLTran>
{
  public bool Equals(GLTran t1, GLTran t2)
  {
    if (t1.Module == t2.Module && t1.BatchNbr == t2.BatchNbr && t1.RefNbr == t2.RefNbr)
    {
      long? curyInfoId1 = t1.CuryInfoID;
      long? curyInfoId2 = t2.CuryInfoID;
      if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
      {
        int? nullable1 = t1.BranchID;
        int? branchId = t2.BranchID;
        if (nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue)
        {
          int? nullable2 = t1.AccountID;
          nullable1 = t2.AccountID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = t1.SubID;
            nullable2 = t2.SubID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              bool? isInterCompany1 = t1.IsInterCompany;
              bool? isInterCompany2 = t2.IsInterCompany;
              return isInterCompany1.GetValueOrDefault() == isInterCompany2.GetValueOrDefault() & isInterCompany1.HasValue == isInterCompany2.HasValue;
            }
          }
        }
      }
    }
    return false;
  }

  public int GetHashCode(GLTran tran)
  {
    return (((((((37 * 397 + tran.Module.GetHashCode()) * 397 + tran.BatchNbr.GetHashCode()) * 397 + (tran.RefNbr ?? string.Empty).GetHashCode()) * 397 + tran.CuryInfoID.GetHashCode()) * 397 + tran.BranchID.GetHashCode()) * 397 + tran.AccountID.GetHashCode()) * 397 + tran.SubID.GetHashCode()) * 397 + tran.IsInterCompany.GetHashCode();
  }
}
