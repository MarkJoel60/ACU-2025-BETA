// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployeeContract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Employee Contract")]
[Serializable]
public class EPEmployeeContract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _EmployeeID;
  protected int? _ContractID;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPEmployeeContract.employeeID>>>>))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Project/Contract")]
  [PXSelector(typeof (Search<Contract.contractID, Where<Contract.baseType, Equal<CTPRType.contract>, Or<Contract.baseType, Equal<CTPRType.project>>>, OrderBy<Desc<Contract.contractCD>>>), SubstituteKey = typeof (Contract.contractCD), Filterable = true)]
  [PXDefault]
  [PXCheckUnique(new Type[] {}, Where = typeof (Where<EPEmployeeContract.employeeID, Equal<Current<EPEmployeeContract.employeeID>>, And<EPEmployeeContract.contractID, Equal<Current<EPEmployeeContract.contractID>>>>))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeContract.employeeID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeContract.contractID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployeeContract.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeContract.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeeContract.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEmployeeContract.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeContract.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeeContract.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEmployeeContract.Tstamp>
  {
  }
}
