// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.SubaccountProviderBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

public abstract class SubaccountProviderBase : ISubaccountProvider
{
  protected readonly PXGraph _graph;

  protected SubaccountProviderBase(PXGraph graph) => this._graph = graph;

  public int? GetSubaccountID(string subaccountCD)
  {
    if (subaccountCD == null)
      return new int?();
    object subaccountId = (object) subaccountCD;
    this._graph.Caches[typeof (PX.Objects.CM.Currency)].RaiseFieldUpdating<PX.Objects.CM.Currency.roundingGainSubID>((object) new PX.Objects.CM.Currency(), ref subaccountId);
    return (int?) subaccountId;
  }

  public abstract string MakeSubaccount<Field>(
    string mask,
    object[] sourceFieldValues,
    Type[] sourceFields)
    where Field : IBqlField;
}
