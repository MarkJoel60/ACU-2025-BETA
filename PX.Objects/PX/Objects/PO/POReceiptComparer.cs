// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POReceiptComparer : IEqualityComparer<PX.Objects.AP.APTran>
{
  public bool Equals(PX.Objects.AP.APTran x, PX.Objects.AP.APTran y)
  {
    return x.ReceiptNbr == y.ReceiptNbr;
  }

  public int GetHashCode(PX.Objects.AP.APTran obj) => obj.ReceiptNbr.GetHashCode();
}
