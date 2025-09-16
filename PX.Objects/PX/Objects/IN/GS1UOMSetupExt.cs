// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GS1UOMSetupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.GS1;

#nullable disable
namespace PX.Objects.IN;

public static class GS1UOMSetupExt
{
  public static string GetUOMOf(this GS1UOMSetup setup, AI ai)
  {
    if (ai.Format != 3)
      return (string) null;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.KilogramCodes))
      return setup.Kilogram;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.PoundCodes))
      return setup.Pound;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.OunceCodes))
      return setup.Ounce;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.TroyOunceCodes))
      return setup.TroyOunce;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.KilogramPerSqrMetreCodes))
      return setup.KilogramPerSqrMetre;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.MetreCodes))
      return setup.Metre;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.InchCodes))
      return setup.Inch;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.FootCodes))
      return setup.Foot;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.YardCodes))
      return setup.Yard;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.SqrMetreCodes))
      return setup.SqrMetre;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.SqrInchCodes))
      return setup.SqrInch;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.SqrFootCodes))
      return setup.SqrFoot;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.SqrYardCodes))
      return setup.SqrYard;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.CubicMetreCodes))
      return setup.CubicMetre;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.CubicInchCodes))
      return setup.CubicInch;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.CubicFootCodes))
      return setup.CubicFoot;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.CubicYardCodes))
      return setup.CubicYard;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.LitreCodes))
      return setup.Litre;
    if (EnumerableExtensions.IsIn<AI>(ai, Codes.QuartCodes))
      return setup.Quart;
    return EnumerableExtensions.IsIn<AI>(ai, Codes.GallonUSCodes) ? setup.GallonUS : (string) null;
  }
}
