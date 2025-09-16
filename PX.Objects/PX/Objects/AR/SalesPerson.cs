// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesPerson
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
namespace PX.Objects.AR;

/// <summary>
/// Represents a salesperson who can be associated with sales of stock and non-stock
/// items in the system. To associate a salesperson with a sale, a salesperson's identifier
/// is recorded in a sales order or invoice line (<see cref="P:PX.Objects.SO.SOLine.SalesPersonID" /> and
/// <see cref="P:PX.Objects.AR.ARTran.SalesPersonID" />). The entities of this type can be edited on the
/// Salespersons (AR205000) form, which corresponds to the <see cref="T:PX.Objects.AR.SalesPersonMaint" /> graph.
/// </summary>
/// <remarks>
/// A salesperson can be matched to a company <see cref="T:PX.Objects.EP.EPEmployee">employee
/// </see> through its <see cref="P:PX.Objects.EP.EPEmployee.SalesPersonID" /> field.
/// </remarks>
[PXCacheName("Sales Person")]
[PXPrimaryGraph(typeof (SalesPersonMaint))]
[Serializable]
public class SalesPerson : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SalesPersonID;
  protected 
  #nullable disable
  string _SalesPersonCD;
  protected Decimal? _CommnPct;
  protected int? _SalesSubID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _Descr;
  protected bool? _IsActive;

  /// <summary>
  /// The unique integer identifier of the salesperson.
  /// This field is a surrogate identity field.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  /// <summary>
  /// The unique identifier of the salesperson.
  /// This field is the key field.
  /// </summary>
  [PXDefault]
  [SalesPersonRaw]
  [PXFieldDescription]
  public virtual string SalesPersonCD
  {
    get => this._SalesPersonCD;
    set => this._SalesPersonCD = value;
  }

  /// <summary>The default commission percentage of the salesperson.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Default Commission %")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  /// <summary>
  /// The default sales subaccount associated with the salesperson.
  /// The value of this field can be used to construct the <see cref="P:PX.Objects.AR.ARTran.SubID">sales subaccount</see> in the invoice line that
  /// references the salesperson according to the rules defined by <see cref="P:PX.Objects.AR.ARSetup.SalesSubMask" />.
  /// </summary>
  [SubAccount]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

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

  /// <summary>The name of the salesperson.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Name")]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the salesperson
  /// is active and can be used for recording sales in
  /// <see cref="T:PX.Objects.AR.ARTran">invoice lines</see> or <see cref="T:PX.Objects.SO.SOLine">
  /// sales order lines</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXNote(DescriptionField = typeof (SalesPerson.salesPersonCD))]
  public virtual Guid? NoteID { get; set; }

  public class PK : PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonID>
  {
    public static SalesPerson Find(PXGraph graph, int? salesPersonID, PKFindOptions options = 0)
    {
      return (SalesPerson) PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonID>.FindBy(graph, (object) salesPersonID, options);
    }
  }

  public class UK : PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonCD>
  {
    public static SalesPerson Find(PXGraph graph, string salesPersonCD, PKFindOptions options = 0)
    {
      return (SalesPerson) PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonCD>.FindBy(graph, (object) salesPersonCD, options);
    }
  }

  public static class FK
  {
    public class SalesSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<SalesPerson>.By<SalesPerson.salesSubID>
    {
    }
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesPerson.salesPersonID>
  {
  }

  public abstract class salesPersonCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesPerson.salesPersonCD>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SalesPerson.commnPct>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesPerson.salesSubID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SalesPerson.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SalesPerson.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesPerson.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesPerson.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SalesPerson.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesPerson.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesPerson.lastModifiedDateTime>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesPerson.descr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesPerson.isActive>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SalesPerson.noteID>
  {
  }
}
