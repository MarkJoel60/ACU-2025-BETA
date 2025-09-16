// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.RecognizedRecordProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.CloudServices.DAC;

/// <exclude />
[PXPrimaryGraph(new System.Type[] {typeof (APInvoiceEntry), typeof (ExpenseClaimDetailEntry), typeof (LeadMaint), typeof (ContactMaint)}, new System.Type[] {typeof (Select<APInvoice, Where<APInvoice.noteID, Equal<Current<RecognizedRecordProjection.documentLink>>>>), typeof (Select<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.noteID, Equal<Current<RecognizedRecordProjection.documentLink>>>>), typeof (Select<CRLead, Where<CRLead.noteID, Equal<Current<RecognizedRecordProjection.documentLink>>>>), typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.noteID, Equal<Current<RecognizedRecordProjection.documentLink>>>>)})]
[PXProjection(typeof (Select2<RecognizedRecord, LeftJoin<APInvoice, On<APInvoice.noteID, Equal<RecognizedRecord.documentLink>>, LeftJoin<EPExpenseClaimDetails, On<EPExpenseClaimDetails.noteID, Equal<RecognizedRecord.documentLink>>, LeftJoin<EPExpenseClaim, On<EPExpenseClaim.refNbr, Equal<EPExpenseClaimDetails.refNbr>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.noteID, Equal<RecognizedRecord.documentLink>>, LeftJoin<CRLead, On<CRLead.noteID, Equal<RecognizedRecord.documentLink>>>>>>>, Where<BqlOperand<RecognizedRecord.status, IBqlString>.IsNotEqual<PendingRecognitionStatus>>>), Persistent = false)]
[PXInternalUseOnly]
[PXCacheName("Recognized Record")]
[Serializable]
public sealed class RecognizedRecordProjection : RecognizedRecord
{
  [PXUIField(DisplayName = "Entity Type")]
  [PXDefault]
  [RecognizedRecordEntityTypeListWithAny]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBCreatedDateTime(DisplayMask = "d")]
  public virtual DateTime? CreatedDateTime
  {
    get => base.CreatedDateTime;
    set => base.CreatedDateTime = value;
  }

  [PXUIField(DisplayName = "Document Link")]
  [PXDefault]
  [PXDBGuid(true, IsKey = true)]
  [PXSelector(typeof (Search<RecognizedRecordProjection.refNbr, Where<RecognizedRecordProjection.entityType, Equal<Current<RecognizedRecordProjection.entityType>>>>))]
  public virtual Guid? RefNbr { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get
    {
      return this.EPRefNbr == null && !this.CNRefNbr.HasValue && !this.LDRefNbr.HasValue ? base.NoteID : this.DocumentLink;
    }
    set => base.NoteID = value;
  }

  public string Description { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (APInvoice.refNbr))]
  public string APRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (EPExpenseClaimDetails.claimDetailCD))]
  public string EPRefNbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Contact.contactID))]
  public int? CNRefNbr { get; set; }

  [PXDBInt(BqlField = typeof (CRLead.contactID))]
  public int? LDRefNbr { get; set; }

  public string RefNbr_description
  {
    get
    {
      string apRefNbr = this.APRefNbr;
      if (apRefNbr != null)
        return apRefNbr;
      string epRefNbr = this.EPRefNbr;
      if (epRefNbr != null)
        return epRefNbr;
      int? nullable = this.CNRefNbr;
      ref int? local1 = ref nullable;
      string refNbrDescription = local1.HasValue ? local1.GetValueOrDefault().ToString() : (string) null;
      if (refNbrDescription != null)
        return refNbrDescription;
      nullable = this.LDRefNbr;
      ref int? local2 = ref nullable;
      return !local2.HasValue ? (string) null : local2.GetValueOrDefault().ToString();
    }
  }

  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordProjection.entityType>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RecognizedRecordProjection.createdDateTime>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RecognizedRecordProjection.refNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecordProjection.noteID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordProjection.description>
  {
  }

  public abstract class aPRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordProjection.aPRefNbr>
  {
  }

  public abstract class ePRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordProjection.ePRefNbr>
  {
  }

  public abstract class cNRefNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecordProjection.cNRefNbr>
  {
  }

  public abstract class lDRefNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecordProjection.lDRefNbr>
  {
  }

  public abstract class documentLink : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedRecordProjection.documentLink>
  {
  }
}
