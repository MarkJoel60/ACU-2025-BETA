// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AvailabilitySigns
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.IN;

public class AvailabilitySigns
{
  public Sign SignQtyAvail { get; private set; }

  public Sign SignQtyHardAvail { get; private set; }

  public Sign SignQtyActual { get; private set; }

  public AvailabilitySigns()
  {
  }

  public AvailabilitySigns(Decimal signQtyAvail, Decimal signQtyHardAvail, Decimal signQtyActual)
  {
    this.SignQtyAvail = Sign.Of(signQtyAvail);
    this.SignQtyHardAvail = Sign.Of(signQtyHardAvail);
    this.SignQtyActual = Sign.Of(signQtyActual);
  }
}
