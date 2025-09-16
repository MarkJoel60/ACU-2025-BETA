// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Terms
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// Represents Credit Terms used for Accounts Payable and Accounts Receivable documents.
/// The records of this type are created and edited through the Credit Terms (CS206500) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.TermsMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (TermsMaint)}, new Type[] {typeof (Select<Terms, Where<Terms.termsID, Equal<Current<Terms.termsID>>>>)})]
[PXCacheName("Terms")]
[Serializable]
public class Terms : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TermsID;
  protected string _Descr;
  protected string _VisibleTo;
  protected string _DueType;
  protected short? _DayDue00;
  protected short? _DayFrom00;
  protected short? _DayTo00;
  protected short? _DayDue01;
  protected short? _DayFrom01;
  protected short? _DayTo01;
  protected string _DiscType;
  protected short? _DayDisc;
  protected Decimal? _DiscPercent;
  protected string _InstallmentType;
  protected short? _InstallmentCntr;
  protected string _InstallmentFreq;
  protected string _InstallmentMthd;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Key field.
  /// Unique identifier of the Credit Terms record.
  /// </summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search3<Terms.termsID, OrderBy<Asc<Terms.termsID>>>), CacheGlobal = true)]
  [PXReferentialIntegrityCheck]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  /// <summary>The description of the Credit Terms.</summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// Determines the categories of Business Accounts, for whom these terms can be used.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"AL"</c> - All,
  /// <c>"VE"</c> - <see cref="T:PX.Objects.AP.Vendor">Vendors</see> only,
  /// <c>"CU"</c> - <see cref="T:PX.Objects.AR.Customer">Customers</see> only,
  /// <c>"DS"</c> - Disabled (can't be selected for Business Accounts or documents of any module).
  /// Defaults to <c>"AL"</c> - All.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AL")]
  [PXUIField(DisplayName = "Visible To")]
  [TermsVisibleTo.List]
  public virtual string VisibleTo
  {
    get => this._VisibleTo;
    set => this._VisibleTo = value;
  }

  /// <summary>
  /// Determines the way the Due Date is calucalated for documents under these Terms.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"N"</c> - Fixed Number of Days,
  /// <c>"D"</c> - Day of Next Month,
  /// <c>"E"</c> - End of Month,
  /// <c>"M"</c> - End of Next Month,
  /// <c>"T"</c> - Day of the Month,
  /// <c>"P"</c> - Fixed Number of Days starting Next Month,
  /// <c>"C"</c> - Custom.
  /// Defaults to <c>"N"</c> - Fixed Number of Days.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Due Date Type")]
  [TermsDueType.List]
  public virtual string DueType
  {
    get => this._DueType;
    set => this._DueType = value;
  }

  /// <summary>
  /// Defines the first Due Date for the terms.
  /// The meaning of this field varies depending on the value of the <see cref="P:PX.Objects.CS.Terms.DueType" /> field.
  /// </summary>
  /// <value>
  /// For Fixed Number of Days, this field is interpreted as the number of days between the document date and its Due Date;
  /// for Day of the Month and Day of Next Month the field holds the day of the month;
  /// for Custom DueType, this field is interpreted as the first Due Date (see <see cref="P:PX.Objects.CS.Terms.DayFrom00" /> and <see cref="P:PX.Objects.CS.Terms.DayTo00" />).
  /// </value>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Due Day 1")]
  public virtual short? DayDue00
  {
    get => this._DayDue00;
    set => this._DayDue00 = value;
  }

  /// <summary>
  /// The start day of the range, for which the Due Date of the document is set to the <see cref="P:PX.Objects.CS.Terms.DayDue00">first due date</see>.
  /// This field is relevant only for the Terms with Custom (<c>"C"</c>) <see cref="P:PX.Objects.CS.Terms.DueType" />.
  /// Corresponds to a day of the month.
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 31 /*0x1F*/)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day From 1", Enabled = false)]
  public virtual short? DayFrom00
  {
    get => this._DayFrom00;
    set => this._DayFrom00 = value;
  }

  /// <summary>
  /// The end day of the range, for which the Due Date of the document is set to the <see cref="P:PX.Objects.CS.Terms.DayDue00">first due date</see>.
  /// This field is relevant only for the Terms with Custom (<c>"C"</c>) <see cref="P:PX.Objects.CS.Terms.DueType" />.
  /// Corresponds to a day of the month.
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 31 /*0x1F*/)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day To 1")]
  public virtual short? DayTo00
  {
    get => this._DayTo00;
    set => this._DayTo00 = value;
  }

  /// <summary>
  /// The second Due Date used with Custom (<c>"C"</c>) <see cref="P:PX.Objects.CS.Terms.DueType" />
  /// (see <see cref="P:PX.Objects.CS.Terms.DayFrom01" /> and <see cref="P:PX.Objects.CS.Terms.DayTo01" />).
  /// Corresponds to a day of the month.
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 31 /*0x1F*/)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Due Day 2")]
  public virtual short? DayDue01
  {
    get => this._DayDue01;
    set => this._DayDue01 = value;
  }

  /// <summary>
  /// The start day of the range, for which the Due Date of the document is set to the <see cref="P:PX.Objects.CS.Terms.DayDue01">second due date</see>.
  /// This field is relevant only for the Terms with Custom (<c>"C"</c>) <see cref="P:PX.Objects.CS.Terms.DueType" />.
  /// Corresponds to a day of the month.
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 31 /*0x1F*/)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day From 2", Enabled = false)]
  public virtual short? DayFrom01
  {
    get => this._DayFrom01;
    set => this._DayFrom01 = value;
  }

  /// <summary>
  /// The end day of the range, for which the Due Date of the document is set to the <see cref="P:PX.Objects.CS.Terms.DayDue01">second due date</see>.
  /// This field is relevant only for the Terms with Custom (<c>"C"</c>) <see cref="P:PX.Objects.CS.Terms.DueType" />.
  /// Corresponds to a day of the month.
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 31 /*0x1F*/)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day To 2", Enabled = false)]
  public virtual short? DayTo01
  {
    get => this._DayTo01;
    set => this._DayTo01 = value;
  }

  /// <summary>
  /// Defines how the system determines whether Cash Discount is applicable for a document under these Terms on a particular date .
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"N"</c> - Fixed Number of Days,
  /// <c>"D"</c> - Day of Next Month,
  /// <c>"E"</c> - End of Month,
  /// <c>"M"</c> - End of Next Month,
  /// <c>"T"</c> - Day of the Month,
  /// <c>"P"</c> - Fixed Number of Days starting Next Month.
  /// Defaults to Fixed Number of Days (<c>"N"</c>).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Discount Type")]
  [TermsDiscType.List]
  public virtual string DiscType
  {
    get => this._DiscType;
    set => this._DiscType = value;
  }

  /// <summary>
  /// The number of days or a particular day of the month (depending on the value of <see cref="P:PX.Objects.CS.Terms.DiscType" />),
  /// during/before which the Cash Discount is applicable for the document.
  /// </summary>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Discount Day")]
  public virtual short? DayDisc
  {
    get => this._DayDisc;
    set => this._DayDisc = value;
  }

  /// <summary>The percent of the Cash Discount under these Terms.</summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount %")]
  public virtual Decimal? DiscPercent
  {
    get => this._DiscPercent;
    set => this._DiscPercent = value;
  }

  /// <summary>The type of installment.</summary>
  /// <value>
  /// Allowed values are:
  /// <c>"S"</c> - Single,
  /// <c>"M"</c> - Multiple.
  /// Defaults to Single (<c>"S"</c>).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Installment Type")]
  [PXDefault("S")]
  [TermsInstallmentType.List]
  public virtual string InstallmentType
  {
    get => this._InstallmentType;
    set => this._InstallmentType = value;
  }

  /// <summary>
  /// The number of Installments for these Terms.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.Terms.InstallmentType" /> is Multiple (<c>"M"</c>).
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Installments")]
  public virtual short? InstallmentCntr
  {
    get => this._InstallmentCntr;
    set => this._InstallmentCntr = value;
  }

  /// <summary>
  /// The frequency of Installments for these Terms.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.Terms.InstallmentType" /> is Multiple (<c>"M"</c>)
  /// and the <see cref="P:PX.Objects.CS.Terms.InstallmentMthd" /> is not Split by Percents in Table (<c>"S"</c>).
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"W"</c> - Weekly,
  /// <c>"M"</c> - Monthly,
  /// <c>"B"</c> - Semi-monthly (the second installment comes a half a month after the first one, and so on).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Installment Frequency")]
  [TermsInstallmentFrequency.List]
  public virtual string InstallmentFreq
  {
    get => this._InstallmentFreq;
    set => this._InstallmentFreq = value;
  }

  /// <summary>
  /// The method, according to which the amounts of installments are calculated.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.Terms.InstallmentType" /> is Multiple (<c>"M"</c>).
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"E"</c> - Equal Parts,
  /// <c>"A"</c> - All Tax in First Installment (the total amount before tax is split equally between installments and the entire amount of tax is added to the first installment),
  /// <c>"S"</c> - Split by Percents in Table (the days and amounts of installments are defined by the related <see cref="T:PX.Objects.CS.TermsInstallments" /> records).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [PXUIField(DisplayName = "Installment Method")]
  [TermsInstallmentMethod.List]
  public virtual string InstallmentMthd
  {
    get => this._InstallmentMthd;
    set => this._InstallmentMthd = value;
  }

  /// <summary>
  /// Indicates that percent of the Sales Order's Prepayment is more than zero.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prepayment Required", FieldClass = "DISTR")]
  public virtual bool? PrepaymentRequired { get; set; }

  /// <summary>The percent of the Sales Order's Prepayment.</summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepayment Percent", FieldClass = "DISTR")]
  public virtual Decimal? PrepaymentPct { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<Terms>.By<Terms.termsID>
  {
    public static Terms Find(PXGraph graph, string termsID, PKFindOptions options = 0)
    {
      return (Terms) PrimaryKeyOf<Terms>.By<Terms.termsID>.FindBy(graph, (object) termsID, options);
    }
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.termsID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.descr>
  {
  }

  public abstract class visibleTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.visibleTo>
  {
  }

  public abstract class dueType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.dueType>
  {
  }

  public abstract class dayDue00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayDue00>
  {
  }

  public abstract class dayFrom00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayFrom00>
  {
  }

  public abstract class dayTo00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayTo00>
  {
  }

  public abstract class dayDue01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayDue01>
  {
  }

  public abstract class dayFrom01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayFrom01>
  {
  }

  public abstract class dayTo01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayTo01>
  {
  }

  public abstract class discType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.discType>
  {
  }

  public abstract class dayDisc : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.dayDisc>
  {
  }

  public abstract class discPercent : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Terms.discPercent>
  {
  }

  public abstract class installmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.installmentType>
  {
  }

  public abstract class installmentCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Terms.installmentCntr>
  {
  }

  public abstract class installmentFreq : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.installmentFreq>
  {
  }

  public abstract class installmentMthd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Terms.installmentMthd>
  {
  }

  public abstract class prepaymentRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Terms.prepaymentRequired>
  {
  }

  public abstract class prepaymentPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Terms.prepaymentPct>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Terms.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Terms.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Terms.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Terms.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Terms.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Terms.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Terms.lastModifiedDateTime>
  {
  }
}
