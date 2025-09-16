// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPDepartment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (DepartmentMaint))]
[PXCacheName("Department")]
[Serializable]
public class EPDepartment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DepartmentID;
  protected string _Description;
  protected int? _ExpenseAccountID;
  protected int? _ExpenseSubID;
  protected string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [Account]
  public virtual int? ExpenseAccountID
  {
    get => this._ExpenseAccountID;
    set => this._ExpenseAccountID = value;
  }

  [SubAccount(typeof (EPDepartment.expenseAccountID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
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

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>
  {
    public static EPDepartment Find(PXGraph graph, string departmentID, PKFindOptions options = 0)
    {
      return (EPDepartment) PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.FindBy(graph, (object) departmentID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Expense Account</summary>
    public class ExpenseAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<EPDepartment>.By<EPDepartment.expenseAccountID>
    {
    }

    /// <summary>Expense Subaccount</summary>
    public class ExpenseSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<EPDepartment>.By<EPDepartment.expenseSubID>
    {
    }
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPDepartment.departmentID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPDepartment.description>
  {
  }

  public abstract class expenseAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPDepartment.expenseAccountID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPDepartment.expenseSubID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPDepartment.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPDepartment.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPDepartment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPDepartment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPDepartment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPDepartment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPDepartment.Tstamp>
  {
  }
}
