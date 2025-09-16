// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranSplit2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
[PXHidden]
[Serializable]
public class INTranSplit2 : INTranSplit
{
  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  INTranSplit2.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit2.refNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit2.lineNbr>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit2.subItemID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranSplit2.lotSerialNbr>
  {
  }

  public new abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit2.baseQty>
  {
  }
}
