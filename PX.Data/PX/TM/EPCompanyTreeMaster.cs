// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCompanyTreeMaster
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
/// Represents a workgroup in the company tree (a hierarchy of workgroups).
/// </summary>
/// <remarks>
/// Company tree is a model of your organization's hierarchy that includes temporary and permanent workgroups.
/// The company tree may reflect the administrative hierarchy and include sub-hierarchies of workgroups created within
/// specific branches or departments. The company tree is used for creating assignment rules in approval and assignment
/// maps and for determining the scope of the users who want to view items assigned to them.
/// The records of this type are created and edited on the <i>Company Tree (EP204061)</i> form,
/// which corresponds to the <see cref="!:PX.TM.CompanyTreeMaint" /> graph,
/// and imported on the <i>Import Company Tree (EP204060)</i> form, which
/// corresponds to the <see cref="!:PX.TM.ImportCompanyTreeMaint" /> graph.
/// </remarks>
[Serializable]
public class EPCompanyTreeMaster : EPCompanyTree
{
  /// <exclude />
  protected int? _TempChildID;
  /// <exclude />
  protected int? _TempParentID;

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.WorkGroupID" />
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Work Group", Enabled = false, Visibility = PXUIVisibility.Invisible)]
  [PXReferentialIntegrityCheck]
  public override int? WorkGroupID
  {
    get => this._WorkGroupID;
    set => this._WorkGroupID = value;
  }

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.Description" />
  [PXDBString(50, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Workgroup Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXCheckUnique(new System.Type[] {})]
  public override 
  #nullable disable
  string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXInt]
  public virtual int? TempChildID
  {
    get => this._TempChildID;
    set => this._TempChildID = value;
  }

  [PXInt]
  public virtual int? TempParentID
  {
    get => this._TempParentID;
    set => this._TempParentID = value;
  }

  public new abstract class workGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPCompanyTreeMaster.workGroupID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTreeMaster.description>
  {
  }

  public new abstract class parentWGID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMaster.parentWGID>
  {
  }

  public new abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMaster.sortOrder>
  {
  }

  public abstract class tempChildID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMaster.tempChildID>
  {
  }

  public abstract class tempParentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMaster.tempParentID>
  {
  }
}
