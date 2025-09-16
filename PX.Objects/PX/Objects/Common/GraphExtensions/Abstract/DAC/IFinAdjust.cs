// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.IFinAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public interface IFinAdjust : IAdjustment
{
  int? AdjdBranchID { get; set; }

  int? AdjgBranchID { get; set; }

  Decimal? CuryAdjgPPDAmt { get; set; }

  bool? AdjdHasPPDTaxes { get; set; }

  Decimal? AdjdCuryRate { get; set; }

  Decimal? AdjPPDAmt { get; set; }

  Decimal? CuryAdjdPPDAmt { get; set; }

  bool? VoidAppl { get; set; }
}
