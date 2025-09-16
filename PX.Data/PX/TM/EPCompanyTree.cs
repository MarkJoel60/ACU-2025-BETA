// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCompanyTree
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.TM;

/// <summary>
/// The base class for the workgroup class <see cref="T:PX.TM.EPCompanyTreeMaster" /> of the company tree (a hierarchy of workgroups).
/// </summary>
[PXCacheName("Workgroup")]
[Serializable]
public class EPCompanyTree : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  /// <summary>The idenifier of the workgroup.</summary>
  [PXDBIdentity]
  [PXUIField(DisplayName = "Work Group", Enabled = false, Visibility = PXUIVisibility.Invisible)]
  public virtual int? WorkGroupID
  {
    get => this._WorkGroupID;
    set => this._WorkGroupID = value;
  }

  /// <summary>
  /// An alphanumeric string of up to 50 characters that describes the workgroup.
  /// This field is used to add any information about the workgroup.
  /// </summary>
  [PXDBString(50, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Workgroup Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXCheckUnique(new System.Type[] {})]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The identifier of the parent workgroup.</summary>
  /// <value>
  /// Corresponds to the value of <see cref="P:PX.TM.EPCompanyTree.WorkGroupID" />
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Parent WorkGroup")]
  public virtual int? ParentWGID
  {
    get => this._ParentWGID;
    set => this._ParentWGID = new int?(value.GetValueOrDefault());
  }

  /// <summary>Sort order of the employees in the workgroup.</summary>
  /// <value>The default value is 0.</value>
  [PXDefault(0)]
  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  /// <summary>
  /// Wait time that is allowed for work item approval or processing.
  /// </summary>
  /// <value>
  /// Time span in minutes.
  /// The default value is 0.
  /// </value>
  [PXDBTimeSpanLong(Format = TimeSpanFormatType.DaysHoursMinites)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Wait Time")]
  public virtual int? WaitTime
  {
    get => this._WaitTime;
    set => this._WaitTime = value;
  }

  /// <summary>
  /// Specifies whether escalation may bypass this workgroup.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bypass Escalation")]
  public virtual bool? BypassEscalation
  {
    get => this._BypassEscalation;
    set => this._BypassEscalation = value;
  }

  /// <summary>
  /// Specifies whether the calendar time is used by the workgroup.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Calendar Time")]
  public virtual bool? UseCalendarTime
  {
    get => this._UseCalendarTime;
    set => this._UseCalendarTime = value;
  }

  /// <exclude />
  [PXDBShort]
  [PXDefault(0)]
  public short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>
  {
    public static EPCompanyTree Find(PXGraph graph, int? workgroupID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.FindBy(graph, (object) workgroupID, options);
    }
  }

  public class UK : PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.description>
  {
    public static EPCompanyTree Find(PXGraph graph, string description, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.description>.FindBy(graph, (object) description, options);
    }
  }

  public static class FK
  {
    public class ParentWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<EPCompanyTree>.By<EPCompanyTree.parentWGID>
    {
    }
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTree.workGroupID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPCompanyTree.description>
  {
  }

  public abstract class parentWGID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTree.parentWGID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTree.sortOrder>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTree.waitTime>
  {
  }

  public abstract class bypassEscalation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPCompanyTree.bypassEscalation>
  {
  }

  public abstract class useCalendarTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPCompanyTree.useCalendarTime>
  {
  }

  public abstract class accessRights : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  EPCompanyTree.accessRights>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPCompanyTree.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTree.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPCompanyTree.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPCompanyTree.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTree.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPCompanyTree.lastModifiedDateTime>
  {
  }
}
