// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCompanyTreeH
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.TM;

/// <summary>
/// Represents a heirarchy record of the workgroup in the company tree.
/// </summary>
/// <remarks>
/// Company tree is a model of your organization's hierarchy that includes temporary and permanent workgroups.
/// The company tree may reflect the administrative hierarchy and include sub-hierarchies of workgroups created within
/// specific branches or departments. The company tree is used for creating assignment rules in approval and assignment
/// maps and for determining the scope of the users who want to view items assigned to them.
/// The records of this type are created and edited on the <i>Company Tree (EP204061)</i> form,
/// which corresponds to the <see cref="!:CompanyTreeMaint" /> graph,
/// and imported on the <i>Import Company Tree (EP204060)</i> form, which
/// corresponds to the <see cref="!:ImportCompanyTreeMaint" /> graph.
/// </remarks>
[Serializable]
public class EPCompanyTreeH : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _WorkGroupID;
  protected int? _ParentWGID;
  protected int? _WaitTime;
  protected int? _WorkGroupLevel;
  protected int? _ParentWGLevel;

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.WorkGroupID" />
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPCompanyTree.workGroupID))]
  public virtual int? WorkGroupID
  {
    get => this._WorkGroupID;
    set => this._WorkGroupID = value;
  }

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.ParentWGID" />
  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXDBDefault(typeof (EPCompanyTree.workGroupID))]
  public virtual int? ParentWGID
  {
    get => this._ParentWGID;
    set => this._ParentWGID = value;
  }

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.WaitTime" />
  [PXDBTimeSpanLong(Format = TimeSpanFormatType.DaysHoursMinites)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Wait Time")]
  public virtual int? WaitTime
  {
    get => this._WaitTime;
    set => this._WaitTime = value;
  }

  /// <summary>Represents the workgroup level in the hierarchy.</summary>
  /// <value>The default value is 0.</value>
  [PXDefault(0)]
  [PXDBInt]
  public virtual int? WorkGroupLevel
  {
    get => this._WorkGroupLevel;
    set => this._WorkGroupLevel = value;
  }

  /// <exclude />
  [PXDefault(0)]
  [PXDBInt]
  public virtual int? ParentWGLevel
  {
    get => this._ParentWGLevel;
    set => this._ParentWGLevel = value;
  }

  public abstract class workGroupID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeH.workGroupID>
  {
  }

  public abstract class parentWGID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeH.parentWGID>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeH.waitTime>
  {
  }

  public abstract class workGroupLevel : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeH.workGroupLevel>
  {
  }

  public abstract class parentWGLevel : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeH.parentWGLevel>
  {
  }
}
