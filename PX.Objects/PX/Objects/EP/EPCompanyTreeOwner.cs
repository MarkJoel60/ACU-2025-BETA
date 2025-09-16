// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCompanyTreeOwner
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXProjection(typeof (Select2<EPCompanyTree, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTree.workGroupID>, And<EPCompanyTreeMember.isOwner, Equal<True>>>, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>>>))]
[PXHidden]
[Serializable]
public class EPCompanyTreeOwner : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _WorkGroupID;
  protected 
  #nullable disable
  string _Description;
  protected int? _ParentWGID;
  protected int? _SortOrder;
  protected int? _WaitTime;
  protected bool? _BypassEscalation;
  protected bool? _UseCalendarTime;
  protected short? _AccessRights;
  protected Guid? _UserID;
  protected int? _OwnerID;

  [PXDBIdentity(BqlTable = typeof (EPCompanyTree))]
  [PXUIField]
  public virtual int? WorkGroupID
  {
    get => this._WorkGroupID;
    set => this._WorkGroupID = value;
  }

  [PXDBString(50, IsKey = true, InputMask = "", BqlTable = typeof (EPCompanyTree))]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt(BqlTable = typeof (EPCompanyTree))]
  [PXDefault(0)]
  [PXDBDefault(typeof (EPCompanyTree.workGroupID))]
  public virtual int? ParentWGID
  {
    get => this._ParentWGID;
    set => this._ParentWGID = new int?(value.GetValueOrDefault());
  }

  [PXDefault(0)]
  [PXDBInt(BqlTable = typeof (EPCompanyTree))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBTimeSpanLong]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Wait Time")]
  public virtual int? WaitTime
  {
    get => this._WaitTime;
    set => this._WaitTime = value;
  }

  [PXDBBool(BqlTable = typeof (EPCompanyTree))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bypass Escalation")]
  public virtual bool? BypassEscalation
  {
    get => this._BypassEscalation;
    set => this._BypassEscalation = value;
  }

  [PXDBBool(BqlTable = typeof (EPCompanyTree))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Calendar Time")]
  public virtual bool? UseCalendarTime
  {
    get => this._UseCalendarTime;
    set => this._UseCalendarTime = value;
  }

  [PXDBShort(BqlTable = typeof (EPCompanyTree))]
  [PXDefault(0)]
  public short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
  }

  [PXDBGuid(false, BqlField = typeof (EPEmployee.userID))]
  [PXUIField(DisplayName = "User ID")]
  public virtual Guid? UserID
  {
    get => this._UserID;
    set => this._UserID = value;
  }

  [PXDBInt(BqlField = typeof (EPEmployee.bAccountID))]
  [PXEPEmployeeSelector]
  [PXUIField]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeOwner.workGroupID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTreeOwner.description>
  {
  }

  public abstract class parentWGID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeOwner.parentWGID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeOwner.sortOrder>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeOwner.waitTime>
  {
  }

  public abstract class bypassEscalation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPCompanyTreeOwner.bypassEscalation>
  {
  }

  public abstract class useCalendarTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPCompanyTreeOwner.useCalendarTime>
  {
  }

  public abstract class accessRights : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    EPCompanyTreeOwner.accessRights>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPCompanyTreeOwner.userID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeOwner.ownerID>
  {
  }
}
