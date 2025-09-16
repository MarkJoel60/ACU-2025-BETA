// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.INAdjustmentWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class INAdjustmentWrapper
{
  public INAdjustmentWrapper(PX.Objects.IN.INRegister doc, ICollection<INTran> transactions)
  {
    this.Document = doc;
    this.Transactions = transactions;
  }

  public PX.Objects.IN.INRegister Document { get; }

  public ICollection<INTran> Transactions { get; }
}
