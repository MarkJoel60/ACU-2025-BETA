// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.JointPayeePerDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;

#nullable enable
namespace PX.Objects.CN.JointChecks;

[PXHidden]
[PXBreakInheritance]
[PXProjection(typeof (Select2<JointPayee, InnerJoin<APInvoice, On<APInvoice.docType, Equal<JointPayee.aPDocType>, And<APInvoice.refNbr, Equal<JointPayee.aPRefNbr>, And<APInvoice.paymentsByLinesAllowed, Equal<False>>>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<JointPayee.jointPayeeInternalId>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.jointPayeeID, Equal<JointPayee.jointPayeeId>>>>>>>>, Where<APAdjust.adjdRefNbr, IsNull>>))]
public class JointPayeePerDoc : JointPayee
{
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Vendor.acctName))]
  [PXDefault]
  [PXUIField(DisplayName = "Joint Payee Name")]
  public 
  #nullable disable
  string JointVendorName { get; set; }

  public new abstract class jointPayeeId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  JointPayeePerDoc.jointPayeeId>
  {
  }

  public new abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  JointPayeePerDoc.aPDocType>
  {
  }

  public new abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  JointPayeePerDoc.aPRefNbr>
  {
  }

  public new abstract class isMainPayee : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  JointPayeePerDoc.isMainPayee>
  {
  }

  public new abstract class jointPayeeExternalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePerDoc.jointPayeeExternalName>
  {
  }

  public abstract class jointVendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeePerDoc.jointVendorName>
  {
  }
}
