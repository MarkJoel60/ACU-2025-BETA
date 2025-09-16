// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXPrimaryGraph(typeof (RQRequestClassMaint))]
[PXCacheName("Request Class")]
[Serializable]
public class RQRequestClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReqClassID;
  protected string _Descr;
  protected bool? _VendorNotRequest;
  protected bool? _VendorMultiply;
  protected bool? _RestrictItemList;
  protected bool? _HideInventoryID;
  protected bool? _IssueRequestor;
  protected bool? _CustomerRequest;
  protected string _ExpenseAccountDefault;
  protected string _ExpenseSubMask;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected short? _PromisedLeadTime;
  protected int? _BudgetValidation;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
  [PXFieldDescription]
  public virtual string ReqClassID
  {
    get => this._ReqClassID;
    set => this._ReqClassID = value;
  }

  [PXDefault]
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VendorNotRequest
  {
    get => this._VendorNotRequest;
    set => this._VendorNotRequest = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VendorMultiply
  {
    get => this._VendorMultiply;
    set => this._VendorMultiply = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? RestrictItemList
  {
    get => this._RestrictItemList;
    set => this._RestrictItemList = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? HideInventoryID
  {
    get => this._HideInventoryID;
    set => this._HideInventoryID = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? IssueRequestor
  {
    get => this._IssueRequestor;
    set => this._IssueRequestor = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CustomerRequest
  {
    get => this._CustomerRequest;
    set => this._CustomerRequest = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Use Expense Account From")]
  [RQAccountSource.List]
  public virtual string ExpenseAccountDefault
  {
    get => this._ExpenseAccountDefault;
    set => this._ExpenseAccountDefault = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Expense Sub. From")]
  public virtual string ExpenseSubMask
  {
    get => this._ExpenseSubMask;
    set => this._ExpenseSubMask = value;
  }

  [PXDefault]
  [Account]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [PXDefault]
  [SubAccount(typeof (RQRequestClass.expenseAcctID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 3660)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Promised Lead Time (Days)")]
  public short? PromisedLeadTime
  {
    get => this._PromisedLeadTime;
    set => this._PromisedLeadTime = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXDefault(0)]
  [RQRequestClassBudget.List]
  public virtual int? BudgetValidation
  {
    get => this._BudgetValidation;
    set => this._BudgetValidation = value;
  }

  [PXNote(DescriptionField = typeof (RQRequestClass.reqClassID), Selector = typeof (Search<RQRequestClass.reqClassID>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<RQRequestClass>.By<RQRequestClass.reqClassID>
  {
    public static RQRequestClass Find(PXGraph graph, string reqClassID, PKFindOptions options = 0)
    {
      return (RQRequestClass) PrimaryKeyOf<RQRequestClass>.By<RQRequestClass.reqClassID>.FindBy(graph, (object) reqClassID, options);
    }
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestClass.reqClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestClass.descr>
  {
  }

  public abstract class vendorNotRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequestClass.vendorNotRequest>
  {
  }

  public abstract class vendorMultiply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestClass.vendorMultiply>
  {
  }

  public abstract class restrictItemList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequestClass.restrictItemList>
  {
  }

  public abstract class hideInventoryID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequestClass.hideInventoryID>
  {
  }

  public abstract class issueRequestor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestClass.issueRequestor>
  {
  }

  public abstract class customerRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequestClass.customerRequest>
  {
  }

  public abstract class expenseAccountDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClass.expenseAccountDefault>
  {
  }

  public abstract class expenseSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClass.expenseSubMask>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestClass.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestClass.expenseSubID>
  {
  }

  public abstract class promisedLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RQRequestClass.promisedLeadTime>
  {
  }

  public abstract class budgetValidation : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestClass.budgetValidation>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequestClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequestClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequestClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequestClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestClass.lastModifiedDateTime>
  {
  }
}
