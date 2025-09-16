// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRoom
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

[PXCacheName("Room")]
[PXPrimaryGraph(typeof (RoomMaint))]
[Serializable]
public class FSRoom : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXParent(typeof (Select<FSBranchLocation, Where<FSBranchLocation.branchLocationID, Equal<Current<FSRoom.branchLocationID>>>>))]
  [PXDBDefault(typeof (FSBranchLocation.branchLocationID))]
  public virtual int? BranchLocationID { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">AAAAAAAAAA")]
  [PXUIField]
  [PXSelector(typeof (Search<FSRoom.roomID, Where<FSRoom.branchLocationID, Equal<Current<FSRoom.branchLocationID>>>>))]
  public virtual 
  #nullable disable
  string RoomID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Floor Nbr.")]
  public virtual int? FloorNbr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  public virtual string CustomRoomID { get; set; }

  [PXString]
  [PXFormula(typeof (FSRoom.descr))]
  public string FormCaptionDescription { get; set; }

  public class PK : PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>
  {
    public static FSRoom Find(
      PXGraph graph,
      int? branchLocationID,
      string roomID,
      PKFindOptions options = 0)
    {
      return (FSRoom) PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>.FindBy(graph, (object) branchLocationID, (object) roomID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSRoom>.By<FSRoom.recordID>
  {
    public static FSRoom Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (FSRoom) PrimaryKeyOf<FSRoom>.By<FSRoom.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationCD>.ForeignKeyOf<FSRoom>.By<FSRoom.branchLocationID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoom.recordID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoom.branchLocationID>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoom.roomID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoom.descr>
  {
  }

  public abstract class floorNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoom.floorNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoom.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoom.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoom.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoom.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoom.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoom.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoom.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSRoom.Tstamp>
  {
  }

  public abstract class customRoomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoom.customRoomID>
  {
  }
}
