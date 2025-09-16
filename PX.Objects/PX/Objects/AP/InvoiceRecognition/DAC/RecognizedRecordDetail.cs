// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.RecognizedRecordDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXHidden]
public class RecognizedRecordDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (SelectFromBase<RecognizedRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedRecord.refNbr, Equal<BqlField<RecognizedRecordDetail.refNbr, IBqlGuid>.FromCurrent>>>>>.And<BqlOperand<RecognizedRecord.entityType, IBqlString>.IsEqual<BqlField<RecognizedRecordDetail.entityType, IBqlString>.FromCurrent>>>))]
  [PXDefault]
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? RefNbr { get; set; }

  [PXDefault]
  [RecognizedRecordEntityTypeList]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [Vendor]
  public int? VendorID { get; set; }

  [Branch(null, null, false, true, false, Required = false)]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  public int? DefaultBranchID { get; set; }

  [PXDBString(IsUnicode = true)]
  public string VendorName { get; set; }

  [PXDBInt]
  public int? VendorTermIndex { get; set; }

  [PXUIField(DisplayName = "Recognized Amount")]
  [PXDBDecimal]
  public Decimal? Amount { get; set; }

  [PXUIField(DisplayName = "Recognized Date", Visible = false)]
  [PXDBDate]
  public System.DateTime? Date { get; set; }

  [PXUIField(DisplayName = "Recognized Due Date", Visible = false)]
  [PXDBDate]
  public System.DateTime? DueDate { get; set; }

  [PXUIField(DisplayName = "Recognized Vendor Ref.", Visible = false)]
  [PXDBString(40, IsUnicode = true)]
  public string VendorRef { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedRecordDetail.refNbr>
  {
  }

  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordDetail.entityType>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedRecordDetail.vendorID>
  {
  }

  public abstract class defaultBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RecognizedRecordDetail.defaultBranchID>
  {
  }

  public abstract class vendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordDetail.vendorName>
  {
  }

  public abstract class vendorTermIndex : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RecognizedRecordDetail.vendorTermIndex>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RecognizedRecordDetail.amount>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  RecognizedRecordDetail.date>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecordDetail.dueDate>
  {
  }

  public abstract class vendorRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordDetail.vendorRef>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedRecordDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecordDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedRecordDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedRecordDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedRecordDetail.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RecognizedRecordDetail.tStamp>
  {
  }
}
