// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXPrimaryGraph(typeof (GLBudgetEntry), Filter = typeof (BudgetFilter))]
[PXCacheName("Budget")]
[Serializable]
public class GLBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Ledger">ledger</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? LedgerID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.GL.GLBudget.FinYear">financial year</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:FinYear.year" /> field.
  /// </value>
  [PXDBString(4, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [PXDBCreatedByID(Visible = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(Visible = false)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<GLBudget>.By<GLBudget.branchID, GLBudget.ledgerID, GLBudget.finYear>
  {
    public static GLBudget Find(
      PXGraph graph,
      int? branchID,
      int? ledgerID,
      string finYear,
      PKFindOptions options = 0)
    {
      return (GLBudget) PrimaryKeyOf<GLBudget>.By<GLBudget.branchID, GLBudget.ledgerID, GLBudget.finYear>.FindBy(graph, (object) branchID, (object) ledgerID, (object) finYear, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLBudget>.By<GLBudget.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLBudget>.By<GLBudget.ledgerID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudget.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudget.ledgerID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudget.finYear>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudget.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudget.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudget.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudget.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudget.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudget.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudget.Tstamp>
  {
  }
}
