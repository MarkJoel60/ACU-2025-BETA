// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.AP1099Yr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

[PXAccumulator(SingleRecord = true)]
[Serializable]
public class AP1099Yr : AP1099Year
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Yr.finYear>
  {
  }
}
