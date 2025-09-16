// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.IFinAdjustHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.GraphExtensions.Abstract.DAC;

#nullable disable
namespace PX.Objects.CM.Extensions;

public static class IFinAdjustHelper
{
  public static void FillDiscAmts(this IFinAdjust adj)
  {
    adj.CuryAdjgDiscAmt = adj.CuryAdjgPPDAmt;
    adj.CuryAdjdDiscAmt = adj.CuryAdjdPPDAmt;
    adj.AdjDiscAmt = adj.AdjPPDAmt;
  }
}
