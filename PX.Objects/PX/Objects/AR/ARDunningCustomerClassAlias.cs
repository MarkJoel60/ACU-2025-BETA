// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningCustomerClassAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
public class ARDunningCustomerClassAlias : ARDunningCustomerClass
{
  public new abstract class dunningLetterLevel : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ARDunningCustomerClassAlias.dunningLetterLevel>
  {
  }

  public new abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningCustomerClassAlias.customerClassID>
  {
  }
}
