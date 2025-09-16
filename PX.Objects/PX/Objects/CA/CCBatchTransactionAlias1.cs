// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchTransactionAlias1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
public class CCBatchTransactionAlias1 : CCBatchTransaction
{
  public new abstract class selectedToHide : 
    BqlType<IBqlBool, bool>.Field<
    #nullable disable
    CCBatchTransactionAlias1.selectedToHide>
  {
  }

  public new abstract class selectedToUnhide : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCBatchTransactionAlias1.selectedToUnhide>
  {
  }

  public new abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchTransactionAlias1.batchID>
  {
  }

  public new abstract class pCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.pCTranNumber>
  {
  }

  public new abstract class settlementStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.settlementStatus>
  {
  }

  public new abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.invoiceNbr>
  {
  }

  public new abstract class submitTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransactionAlias1.submitTime>
  {
  }

  public new abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.procCenterCardTypeCode>
  {
  }

  public new abstract class accountNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.accountNumber>
  {
  }

  public new abstract class amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCBatchTransactionAlias1.amount>
  {
  }

  public new abstract class transactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCBatchTransactionAlias1.transactionID>
  {
  }

  public new abstract class originalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.originalStatus>
  {
  }

  public new abstract class currentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.currentStatus>
  {
  }

  public new abstract class processingStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.processingStatus>
  {
  }

  public new abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.docType>
  {
  }

  public new abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.refNbr>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransactionAlias1.createdDateTime>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCBatchTransactionAlias1.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.createdByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransactionAlias1.lastModifiedDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCBatchTransactionAlias1.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransactionAlias1.lastModifiedByScreenID>
  {
  }

  public new abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CCBatchTransactionAlias1.tstamp>
  {
  }

  public new abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatchTransactionAlias1.noteid>
  {
  }
}
