// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IINTranCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public interface IINTranCostCenterSupport : ICostCenterSupport<INTran>
{
  bool IsSupported(string inventorySource);

  bool IsSupported(INTran tran);

  string GetInventorySource(INTran tran);

  string GetDestinationInventorySource(INTran tran);

  IEnumerable<Type> GetDestinationFieldsDependOn();

  bool IsDestinationSpecificCostCenter(INTran tran);

  INCostCenter GetDestinationCostCenter(INTran tran);

  void OnInventorySourceChanged(INTran tran, string newInventorySource, bool isExternalCall);

  void OnDestinationInventorySourceChanged(
    INTran tran,
    string newInventorySource,
    bool isExternalCall);

  void ValidateForPersisting(INTran tran);

  void ValidateDestinationForPersisting(INTran tran);
}
