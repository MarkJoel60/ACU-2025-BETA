// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentRoute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EPAssignmentMaint))]
[DebuggerDisplay("Name={Name} WorkgroupID={WorkgroupID} RouterID={RouterID}")]
[PXCacheName("Legacy Assignment Route")]
[Serializable]
public class EPAssignmentRoute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssignmentRouteID;
  protected int? _Parent;
  protected int? _AssignmentMapID;
  protected 
  #nullable disable
  string _RouterType;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected string _OwnerSource;
  protected bool? _UseWorkgroupByOwner;
  protected string _Name;
  protected int? _RouteID;
  protected int? _Sequence;
  protected string _RuleType;
  protected string _Icon;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Route ID")]
  [PXSelector(typeof (Search<EPAssignmentRoute.assignmentRouteID, Where<EPAssignmentRoute.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>>>), DescriptionField = typeof (EPAssignmentRoute.name))]
  [PXParent(typeof (Select<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentRouteID, Equal<Current<EPAssignmentRoute.parent>>>>))]
  public virtual int? AssignmentRouteID
  {
    get => this._AssignmentRouteID;
    set => this._AssignmentRouteID = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (EPAssignmentRoute.assignmentRouteID))]
  public virtual int? Parent
  {
    get => this._Parent;
    set => this._Parent = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (EPAssignmentMap.assignmentMapID))]
  [PXParent(typeof (Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPAssignmentRoute.assignmentMapID>>>>))]
  public virtual int? AssignmentMapID
  {
    get => this._AssignmentMapID;
    set => this._AssignmentMapID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [EPRouterType.List]
  [PXUIField(DisplayName = "Type")]
  [PXDefault("W")]
  public virtual string RouterType
  {
    get => this._RouterType;
    set => this._RouterType = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Assign to")]
  [PXCompanyTreeSelector]
  [PXDefault]
  [PXFormula(typeof (Default<EPAssignmentRoute.routerType>))]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [Owner(typeof (EPAssignmentRoute.workgroupID), DisplayName = "Assign to")]
  [PXChildUpdatable(AutoRefresh = true)]
  [PXDefault]
  [PXFormula(typeof (Default<EPAssignmentRoute.routerType>))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDBString(250)]
  [PXUIField]
  public virtual string OwnerSource
  {
    get => this._OwnerSource;
    set => this._OwnerSource = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Workgroup by Owner")]
  public virtual bool? UseWorkgroupByOwner
  {
    get => this._UseWorkgroupByOwner;
    set => this._UseWorkgroupByOwner = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (Coalesce<Search<EPAssignmentRoute.name, Where<EPAssignmentRoute.assignmentRouteID, Equal<Current<EPAssignmentRoute.assignmentRouteID>>>>, Search<EPCompanyTree.description, Where<EPCompanyTree.workGroupID, Equal<Current<EPAssignmentRoute.workgroupID>>>>>))]
  [PXFormula(typeof (Default<EPAssignmentRoute.workgroupID>))]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = string.IsNullOrEmpty(value) ? (string) null : value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Jump to")]
  [PXDefault]
  [PXSelector(typeof (Search<EPAssignmentRoute.assignmentRouteID, Where<EPAssignmentRoute.routerType, Equal<EPRouterType.group>, And<EPAssignmentRoute.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>>>>), new Type[] {typeof (EPAssignmentRoute.name)}, DescriptionField = typeof (EPAssignmentRoute.name))]
  public virtual int? RouteID
  {
    get => this._RouteID;
    set => this._RouteID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Seq.", Enabled = false)]
  public virtual int? Sequence
  {
    get => this._Sequence;
    set => this._Sequence = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PX.Objects.EP.RuleType.List]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Rule Type")]
  public virtual string RuleType
  {
    get => this._RuleType;
    set => this._RuleType = value;
  }

  [PXDefault(0)]
  [PXUIField]
  [PXDBTimeSpanLong]
  public virtual int? WaitTime { get; set; }

  [PXString(250)]
  public virtual string Icon
  {
    get => this._Icon;
    set => this._Icon = value;
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

  public abstract class assignmentRouteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPAssignmentRoute.assignmentRouteID>
  {
  }

  public abstract class parent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.parent>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPAssignmentRoute.assignmentMapID>
  {
  }

  public abstract class routerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRoute.routerType>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.ownerID>
  {
  }

  public abstract class ownerSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentRoute.ownerSource>
  {
  }

  public abstract class useWorkgroupByOwner : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPAssignmentRoute.useWorkgroupByOwner>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRoute.name>
  {
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.routeID>
  {
  }

  public abstract class sequence : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.sequence>
  {
  }

  public abstract class ruleType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRoute.ruleType>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRoute.waitTime>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRoute.icon>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPAssignmentRoute.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAssignmentRoute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentRoute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentRoute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPAssignmentRoute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentRoute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentRoute.lastModifiedDateTime>
  {
  }
}
