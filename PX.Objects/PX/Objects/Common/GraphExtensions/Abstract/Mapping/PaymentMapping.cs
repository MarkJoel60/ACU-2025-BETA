// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.PaymentMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class PaymentMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;

  /// <exclude />
  public Type Extension => typeof (Payment);

  /// <exclude />
  public Type Table => this._table;

  /// <summary>Creates the default mapping of the <see cref="!:DocumentWithLines" /> mapped cache extension to the specified table.</summary>
  /// <param name="table">A DAC.</param>
  public PaymentMapping(Type table) => this._table = table;

  public virtual Type BranchID => typeof (Payment.branchID);

  public virtual Type AdjDate => typeof (Payment.adjDate);

  public virtual Type AdjFinPeriodID => typeof (Payment.adjFinPeriodID);

  public virtual Type AdjTranPeriodID => typeof (Payment.adjTranPeriodID);
}
