// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSMasterContract
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (MasterContractMaint))]
[PXGroupMask(typeof (InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSMasterContract.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class FSMasterContract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? MasterContractID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Master Contract ID")]
  [PXSelector(typeof (Search2<FSMasterContract.masterContractCD, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSMasterContract.customerID>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>), new Type[] {typeof (FSMasterContract.masterContractCD), typeof (FSMasterContract.descr), typeof (FSMasterContract.customerID), typeof (FSMasterContract.branchID)})]
  public virtual 
  #nullable disable
  string MasterContractCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Customer")]
  [FSSelectorBAccountTypeCustomerOrCombined]
  public virtual int? CustomerID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSMasterContract>.By<FSMasterContract.masterContractID>
  {
    public static FSMasterContract Find(
      PXGraph graph,
      int? masterContractID,
      PKFindOptions options = 0)
    {
      return (FSMasterContract) PrimaryKeyOf<FSMasterContract>.By<FSMasterContract.masterContractID>.FindBy(graph, (object) masterContractID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSMasterContract>.By<FSMasterContract.masterContractCD>
  {
    public static FSMasterContract Find(
      PXGraph graph,
      string masterContractCD,
      PKFindOptions options = 0)
    {
      return (FSMasterContract) PrimaryKeyOf<FSMasterContract>.By<FSMasterContract.masterContractCD>.FindBy(graph, (object) masterContractCD, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSMasterContract>.By<FSMasterContract.customerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSMasterContract>.By<FSMasterContract.branchID>
    {
    }
  }

  public abstract class masterContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSMasterContract.masterContractID>
  {
  }

  public abstract class masterContractCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSMasterContract.masterContractCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSMasterContract.descr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSMasterContract.customerID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSMasterContract.branchID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSMasterContract.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSMasterContract.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSMasterContract.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSMasterContract.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSMasterContract.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSMasterContract.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSMasterContract.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSMasterContract.Tstamp>
  {
  }
}
