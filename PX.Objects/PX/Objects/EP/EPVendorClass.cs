// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPVendorClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class EPVendorClass : VendorClass
{
  public new abstract class vendorClassID : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    EPVendorClass.vendorClassID>
  {
  }

  public new abstract class discTakenAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPVendorClass.discTakenAcctID>
  {
  }

  public new abstract class discTakenSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPVendorClass.discTakenSubID>
  {
  }

  public new abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPVendorClass.expenseAcctID>
  {
  }

  public new abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPVendorClass.expenseSubID>
  {
  }
}
