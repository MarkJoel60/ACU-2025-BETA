// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Currency2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM;

[PXHidden]
[Serializable]
public class Currency2 : Currency
{
  public new abstract class curyID : BqlType<IBqlString, string>.Field<
  #nullable disable
  Currency2.curyID>
  {
  }

  public new abstract class decimalPlaces : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Currency2.decimalPlaces>
  {
  }
}
