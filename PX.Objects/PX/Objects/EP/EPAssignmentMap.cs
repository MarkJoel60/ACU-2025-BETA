// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentMap
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.EP;

[EPAssignmentMapPrimaryGraph]
[PXCacheName("Assignment Map")]
[Serializable]
public class EPAssignmentMap : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssignmentMapID;
  protected 
  #nullable disable
  string _Name;
  protected string _EntityType;
  protected string _GraphType;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Map")]
  [EPAssignmentMapSelector]
  public virtual int? AssignmentMapID
  {
    get => this._AssignmentMapID;
    set => this._AssignmentMapID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Entity", Required = false)]
  [PXDependsOnFields(new Type[] {typeof (EPAssignmentMap.graphType)})]
  public virtual string EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Entity")]
  public virtual string GraphType
  {
    get => this._GraphType;
    set => this._GraphType = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Map Type")]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Assignment and Approval Map", "Assignment Map", "Approval Map"})]
  public virtual int? MapType { get; set; }

  [PXNote(DescriptionField = typeof (EPAssignmentMap.assignmentMapID), Selector = typeof (Search<EPAssignmentMap.assignmentMapID>))]
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

  public class PK : PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>
  {
    public static EPAssignmentMap Find(PXGraph graph, int? assignmentMapID, PKFindOptions options = 0)
    {
      return (EPAssignmentMap) PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.FindBy(graph, (object) assignmentMapID, options);
    }
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPAssignmentMap.assignmentMapID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentMap.name>
  {
  }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentMap.entityType>
  {
  }

  public abstract class graphType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentMap.graphType>
  {
  }

  public abstract class mapType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentMap.mapType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAssignmentMap.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPAssignmentMap.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAssignmentMap.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentMap.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentMap.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPAssignmentMap.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentMap.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentMap.lastModifiedDateTime>
  {
  }
}
