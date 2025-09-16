// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsInstallments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Terms Installments Detail")]
[Serializable]
public class TermsInstallments : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TermsID;
  protected short? _InstallmentNbr;
  protected short? _InstDays;
  protected Decimal? _InstPercent;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Terms.termsID))]
  [PXUIField(DisplayName = "TermsID", Visible = false)]
  [PXParent(typeof (Select<Terms, Where<Terms.termsID, Equal<Current<TermsInstallments.termsID>>>>))]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Inst.#", Visible = false, Enabled = false)]
  public virtual short? InstallmentNbr
  {
    get => this._InstallmentNbr;
    set => this._InstallmentNbr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Days")]
  public virtual short? InstDays
  {
    get => this._InstDays;
    set => this._InstDays = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Percent")]
  public virtual Decimal? InstPercent
  {
    get => this._InstPercent;
    set => this._InstPercent = value;
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

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TermsInstallments.termsID>
  {
  }

  public abstract class installmentNbr : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    TermsInstallments.installmentNbr>
  {
  }

  public abstract class instDays : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TermsInstallments.instDays>
  {
  }

  public abstract class instPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TermsInstallments.instPercent>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TermsInstallments.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TermsInstallments.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TermsInstallments.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TermsInstallments.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TermsInstallments.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TermsInstallments.lastModifiedDateTime>
  {
  }
}
