// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShiftDifferentialRegisterEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class ShiftDifferentialRegisterEntryExt : PXGraphExtension<RegisterEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.shiftDifferential>();

  [PXOverride]
  public virtual PMTran CreateTransaction(
    RegisterEntry.CreatePMTran createPMTran,
    Func<RegisterEntry.CreatePMTran, PMTran> baseMethod)
  {
    PMTran data = baseMethod(createPMTran);
    this.Base.Transactions.Cache.SetValue<ShiftDifferentialPMTranExt.shiftID>((object) data, (object) createPMTran.TimeActivity.ShiftID);
    return this.Base.Transactions.Update(data);
  }
}
