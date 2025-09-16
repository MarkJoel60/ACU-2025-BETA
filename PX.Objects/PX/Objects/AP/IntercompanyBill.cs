// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.IntercompanyBill
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class IntercompanyBill : APInvoice
{
  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  IntercompanyBill.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  IntercompanyBill.refNbr>
  {
  }

  public new abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyBill.invoiceNbr>
  {
  }

  public new abstract class intercompanyInvoiceNoteID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    IntercompanyBill.intercompanyInvoiceNoteID>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyBill.hasMultipleProjects>
  {
  }
}
