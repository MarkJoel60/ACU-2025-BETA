// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemPartNumber
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXProjection(typeof (Select<INItemXRef, Where<INItemXRef.alternateType, In3<INAlternateType.global, INAlternateType.vPN, INAlternateType.cPN>>>))]
public class INItemPartNumber : INItemXRef
{
  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INItemPartNumber.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPartNumber.subItemID>
  {
  }

  public new abstract class alternateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemPartNumber.alternateType>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPartNumber.bAccountID>
  {
  }

  public new abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemPartNumber.alternateID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPartNumber.uOM>
  {
  }

  public new abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPartNumber.descr>
  {
  }
}
