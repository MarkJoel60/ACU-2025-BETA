// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectTemplate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Project Template projection based on PMProject</summary>
[PXCacheName("Project Template")]
[PXProjection(typeof (Select<PMProject, Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.nonProject, Equal<False>>>>), Persistent = false)]
[Serializable]
public class PMProjectTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The project template ID.</summary>
  [PXDBIdentity(BqlField = typeof (PMProject.contractID))]
  public virtual int? ContractID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.ContractCD" />
  [PXDimensionSelector("PROJECT", typeof (Search<PMProjectTemplate.contractCD>), typeof (PMProjectTemplate.contractCD), new Type[] {typeof (PMProjectTemplate.contractCD)})]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PMProject.contractCD))]
  [PXUIField(DisplayName = "Project ID")]
  public virtual 
  #nullable disable
  string ContractCD { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.Description" />
  [PXDBLocalizableString(BqlField = typeof (PMProject.description))]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.Status" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (PMProject.status))]
  [ProjectStatus.TemplStatusList]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (Contract.createdDateTime))]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID(BqlField = typeof (Contract.createdByID))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (Contract.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (Contract.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.ProjectGroupID" />
  [PXDBString(BqlField = typeof (PMProject.projectGroupID))]
  [PXUIField(DisplayName = "Project Group")]
  public virtual string ProjectGroupID { get; set; }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectTemplate.contractID>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectTemplate.contractCD>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectTemplate.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectTemplate.status>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectTemplate.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProjectTemplate.createdByID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectTemplate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProjectTemplate.lastModifiedByID>
  {
  }

  public abstract class projectGroupID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectTemplate.projectGroupID>
  {
  }
}
