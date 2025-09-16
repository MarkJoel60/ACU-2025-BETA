// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// The projection of the <see cref="T:PX.Objects.CR.Contact" /> table joined with the <see cref="T:PX.Objects.CR.BAccount" /> table
/// (on the condition that <see cref="P:PX.Objects.CR.Contact.BAccountID" /> is equal to <see cref="P:PX.Objects.CR.BAccount.BAccountID" />).
/// </summary>
[PXProjection(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>>), Persistent = false)]
[Serializable]
public class ContactAccount : Contact
{
  protected 
  #nullable disable
  string _AcctName;
  protected string _Type;
  protected string _AccountStatus;
  protected int? _DefContactID;

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.AcctName" />
  [PXDBString(60, IsUnicode = true, BqlField = typeof (BAccount.acctName))]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.Type" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (BAccount.type))]
  [PXUIField]
  [BAccountType.List]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.Status" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (BAccount.status))]
  [PXUIField(DisplayName = "Status")]
  [CustomerStatus.BusinessAccountNonCustomerList]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string AccountStatus
  {
    get => this._AccountStatus;
    set => this._AccountStatus = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.DefContactID" />
  [PXDBInt(BqlField = typeof (BAccount.defContactID))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.PrimaryContactID" />
  [PXDBInt(BqlField = typeof (BAccount.primaryContactID))]
  [PXUIField(DisplayName = "Primary Contact")]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? PrimaryContactID { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the <see cref="T:PX.Objects.CR.Contact">contact</see>,
  /// which the current <see cref="T:PX.Objects.CR.ContactAccount" /> represents, is primary for the <see cref="T:PX.Objects.CR.BAccount">business account</see>,
  /// which the current <see cref="T:PX.Objects.CR.ContactAccount" /> also represents.
  /// </summary>
  /// <value>
  /// The value is <see langword="true" /> when <see cref="P:PX.Objects.CR.ContactAccount.PrimaryContactID" />
  /// is equal to <see cref="P:PX.Objects.CR.Contact.ContactID" />.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.contactID), typeof (ContactAccount.primaryContactID)})]
  public override bool? IsPrimary
  {
    get
    {
      int? contactId = this.ContactID;
      int? primaryContactId = this.PrimaryContactID;
      return new bool?(contactId.GetValueOrDefault() == primaryContactId.GetValueOrDefault() & contactId.HasValue == primaryContactId.HasValue);
    }
    set
    {
    }
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAccount.acctName>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAccount.type>
  {
  }

  public abstract class accountStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccount.accountStatus>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactAccount.defContactID>
  {
  }

  public abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContactAccount.primaryContactID>
  {
  }

  public new abstract class isPrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactAccount.isPrimary>
  {
  }
}
