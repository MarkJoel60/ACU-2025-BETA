// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRetainageInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APRetainageInvoice : APRegister
{
  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  APRetainageInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRetainageInvoice.refNbr>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRetainageInvoice.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRetainageInvoice.origRefNbr>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRetainageInvoice.isRetainageDocument>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRetainageInvoice.released>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRetainageInvoice.paymentsByLinesAllowed>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRetainageInvoice.hasMultipleProjects>
  {
  }
}
