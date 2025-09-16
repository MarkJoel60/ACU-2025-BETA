// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.CRQuote
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

public class CRQuote : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _QuoteNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (CROpportunityRevision.noteID))]
  public virtual Guid? QuoteID { get; set; }

  [AutoNumber(typeof (CRSetup.quoteNumberingID), typeof (AccessInfo.businessDate))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string QuoteNbr
  {
    get => this._QuoteNbr;
    set => this._QuoteNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Type", Visible = true)]
  [CRQuoteType]
  [PXDefault("D")]
  public virtual string QuoteType { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [CRQuoteStatus]
  [PXDefault]
  public virtual string Status { get; set; }

  [PXNote(DescriptionField = typeof (CRQuote.quoteNbr), Selector = typeof (CRQuote.quoteNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Date Created", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.selected>
  {
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.quoteID>
  {
  }

  public abstract class quoteNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.quoteNbr>
  {
  }

  public abstract class quoteType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.quoteType>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.subject>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.expirationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.status>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRQuote.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.lastModifiedDateTime>
  {
  }
}
