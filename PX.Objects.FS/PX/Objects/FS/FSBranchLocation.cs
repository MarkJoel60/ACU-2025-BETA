// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSBranchLocation
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Branch Location")]
[PXPrimaryGraph(typeof (BranchLocationMaint))]
[Serializable]
public class FSBranchLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchLocationAddressID;
  protected int? _BranchLocationContactID;
  protected bool? _AllowOverrideContactAddress;

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXSelector(typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXUIField]
  [PXDefault]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string BranchLocationCD { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [FSDocumentAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? BranchLocationAddressID
  {
    get => this._BranchLocationAddressID;
    set => this._BranchLocationAddressID = value;
  }

  [PXDBInt]
  [FSDocumentContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? BranchLocationContactID
  {
    get => this._BranchLocationContactID;
    set => this._BranchLocationContactID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [SubAccount(DisplayName = "General Subaccount")]
  public virtual int? SubID { get; set; }

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

  [PXDefault]
  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr))]
  public virtual int? DfltSiteID { get; set; }

  [SubItem(DisplayName = "Default Subitem")]
  public virtual int? DfltSubItemID { get; set; }

  [INUnit(DisplayName = "Default Unit")]
  public virtual string DfltUOM { get; set; }

  [PXBool]
  [PXFormula(typeof (Current<FSSetup.manageRooms>))]
  [PXUIField(DisplayName = "Manage Rooms")]
  public virtual bool? RoomFeatureEnabled { get; set; }

  public class PK : PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>
  {
    public static FSBranchLocation Find(
      PXGraph graph,
      int? branchLocationID,
      PKFindOptions options = 0)
    {
      return (FSBranchLocation) PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.FindBy(graph, (object) branchLocationID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationCD>
  {
    public static FSBranchLocation Find(
      PXGraph graph,
      string branchLocationCD,
      PKFindOptions options = 0)
    {
      return (FSBranchLocation) PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationCD>.FindBy(graph, (object) branchLocationCD, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.branchID>
    {
    }

    public class Address : 
      PrimaryKeyOf<FSAddress>.By<FSAddress.addressID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationAddressID>
    {
    }

    public class Contract : 
      PrimaryKeyOf<FSContact>.By<FSContact.contactID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationContactID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.subID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.dfltSiteID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSBranchLocation>.By<FSBranchLocation.dfltSubItemID>
    {
    }
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSBranchLocation.branchLocationID>
  {
  }

  public abstract class branchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBranchLocation.branchLocationCD>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBranchLocation.branchID>
  {
  }

  public abstract class branchLocationAddressID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchLocationContactID : IBqlField, IBqlOperand
  {
  }

  public abstract class allowOverrideContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class descr : IBqlField, IBqlOperand
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBranchLocation.subID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSBranchLocation.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSBranchLocation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBranchLocation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBranchLocation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSBranchLocation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBranchLocation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBranchLocation.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSBranchLocation.Tstamp>
  {
  }

  public abstract class dfltSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBranchLocation.dfltSiteID>
  {
  }

  public abstract class dfltSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBranchLocation.dfltSubItemID>
  {
  }

  public abstract class dfltUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBranchLocation.dfltUOM>
  {
  }

  public abstract class roomFeatureEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSBranchLocation.roomFeatureEnabled>
  {
  }
}
