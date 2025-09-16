// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// A link between <see cref="T:PX.Objects.CA.CABatch" /> and <see cref="T:PX.Objects.AP.APPayment" />.
/// </summary>
[PXCacheName("CA Batch Details")]
[Serializable]
public class CABatchDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// This field is the key field.
  /// Corresponds to the <see cref="P:PX.Objects.CA.CABatch.BatchNbr" /> field.
  /// </summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CABatch.batchNbr))]
  [PXParent(typeof (Select<CABatch, Where<CABatch.batchNbr, Equal<Current<CABatchDetail.batchNbr>>>>))]
  public virtual 
  #nullable disable
  string BatchNbr { get; set; }

  /// <summary>
  /// This field is a part of the compound key of the document.
  /// It is either equals to <see cref="F:PX.Objects.GL.BatchModule.AP" /> or <see cref="F:PX.Objects.GL.BatchModule.PR" />in current implementation.
  /// Potentially it may be equal to <see cref="F:PX.Objects.GL.BatchModule.AR" />.
  /// </summary>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("AP")]
  [PXStringList(new string[] {"AP", "AR", "PR"}, new string[] {"AP", "AR", "PR"})]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public virtual string OrigModule { get; set; }

  /// <summary>
  /// The type of payment document.
  /// This field is a part of the compound key of the document.
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.DocType" /> field and the <see cref="P:PX.Objects.AP.APPayment.DocType" /> field.
  /// </summary>
  [PXDBString(3, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual string OrigDocType { get; set; }

  /// <summary>
  /// The payment's reference number.
  /// This number is a link to payment document on the Checks and Payments (AP302000) form.
  /// This field is a part of the compound key of the document.
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.RefNbr" /> field and the <see cref="P:PX.Objects.AP.APPayment.RefNbr" /> field.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// Key field used to differentiate between Direct Deposit splits in PR module. For other modules, it isn't necessary and defaults to 0.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  [PXDefault(0)]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBString(80 /*0x50*/)]
  [PXUIField(DisplayName = "Payment-Related Info (Addenda)")]
  [PXDefault]
  public virtual string AddendaPaymentRelatedInfo { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public virtual void Copy(PX.Objects.AP.APPayment payment)
  {
    this.OrigRefNbr = payment.RefNbr;
    this.OrigDocType = payment.DocType;
    this.OrigModule = "AP";
  }

  public virtual void Copy(PX.Objects.AR.ARPayment payment)
  {
    this.OrigRefNbr = payment.RefNbr;
    this.OrigDocType = payment.DocType;
    this.OrigModule = "AR";
  }

  public class PK : 
    PrimaryKeyOf<CABatchDetail>.By<CABatchDetail.batchNbr, CABatchDetail.origModule, CABatchDetail.origDocType, CABatchDetail.origRefNbr, CABatchDetail.origLineNbr>
  {
    public static CABatchDetail Find(
      PXGraph graph,
      string batchNbr,
      string origModule,
      string origDocType,
      string origRefNbr,
      int? origLineNbr,
      PKFindOptions options = 0)
    {
      return (CABatchDetail) PrimaryKeyOf<CABatchDetail>.By<CABatchDetail.batchNbr, CABatchDetail.origModule, CABatchDetail.origDocType, CABatchDetail.origRefNbr, CABatchDetail.origLineNbr>.FindBy(graph, (object) batchNbr, (object) origModule, (object) origDocType, (object) origRefNbr, (object) origLineNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccountBatch : 
      PrimaryKeyOf<CABatch>.By<CABatch.batchNbr>.ForeignKeyOf<CABatchDetail>.By<CABatchDetail.batchNbr>
    {
    }

    public class APPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<CABatchDetail>.By<CABatchDetail.origDocType, CABatchDetail.origRefNbr>
    {
    }
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatchDetail.batchNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatchDetail.origModule>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatchDetail.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABatchDetail.origRefNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABatchDetail.origLineNbr>
  {
    public const int DefaultValue = 0;

    public class defaultValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CABatchDetail.origLineNbr.defaultValue>
    {
      public defaultValue()
        : base(0)
      {
      }
    }
  }

  public abstract class addendaPaymentRelatedInfo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetail.addendaPaymentRelatedInfo>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABatchDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABatchDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABatchDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABatchDetail.lastModifiedDateTime>
  {
  }
}
