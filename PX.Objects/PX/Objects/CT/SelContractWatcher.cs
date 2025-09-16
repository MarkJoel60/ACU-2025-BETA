// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.SelContractWatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXProjection(typeof (Select2<ContractWatcher, RightJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<ContractWatcher.contactID>>>>), Persistent = true)]
[Serializable]
public class SelContractWatcher : ContractWatcher
{
  protected 
  #nullable disable
  string _displayName;
  protected string _FirstName;
  protected string _MidName;
  protected string _LastName;
  protected string _Title;
  protected string _Salutation;
  protected string _Phone1;
  protected int? _BAccountID;
  protected int? _ContactContactID;

  [PXUIField]
  [ContactDisplayName(typeof (SelContractWatcher.lastName), typeof (SelContractWatcher.firstName), typeof (SelContractWatcher.midName), typeof (SelContractWatcher.title), true, BqlField = typeof (PX.Objects.CR.Contact.displayName))]
  public virtual string DisplayName
  {
    get => this._displayName;
    set => this._displayName = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.firstName))]
  [PXUIField(DisplayName = "First Name")]
  public virtual string FirstName
  {
    get => this._FirstName;
    set => this._FirstName = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.midName))]
  [PXUIField(DisplayName = "Middle Name")]
  public virtual string MidName
  {
    get => this._MidName;
    set => this._MidName = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.lastName))]
  [PXUIField(DisplayName = "Last Name")]
  public virtual string LastName
  {
    get => this._LastName;
    set => this._LastName = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.title))]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.salutation))]
  [PXUIField]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  [PXDBString(50, BqlField = typeof (PX.Objects.CR.Contact.phone1))]
  [PXUIField]
  [PhoneValidation]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  [PXDBInt(IsKey = false, BqlField = typeof (PX.Objects.CR.Contact.bAccountID))]
  [PXDimensionSelector("BIZACCT", typeof (Search<BAccount.bAccountID>), typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName), DirtyRead = true)]
  [PXUIField]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Contact.contactID))]
  [PXUIField]
  [PXExtraKey]
  public virtual int? ContactContactID
  {
    get => new int?();
    set
    {
    }
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SelContractWatcher.displayName>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.firstName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.midName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.lastName>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.salutation>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelContractWatcher.phone1>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelContractWatcher.bAccountID>
  {
  }

  public abstract class contactContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SelContractWatcher.contactContactID>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelContractWatcher.contractID>
  {
  }
}
