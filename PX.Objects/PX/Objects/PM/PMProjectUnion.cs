// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectUnion
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Project Union Locals")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProjectUnion : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Gets or sets the parent Project.</summary>
  protected int? _ProjectID;

  [Project(DisplayName = "Project ID", IsKey = true, DirtyRead = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMProjectUnion.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXForeignReference(typeof (Field<PMProjectUnion.unionID>.IsRelatedTo<PMUnion.unionID>))]
  [PXRestrictor(typeof (Where<PMUnion.isActive, Equal<True>>), "The {0} union local is inactive.", new Type[] {typeof (PMUnion.unionID)})]
  [PXSelector(typeof (Search<PMUnion.unionID>), DescriptionField = typeof (PMUnion.description))]
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Union Local")]
  public virtual 
  #nullable disable
  string UnionID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectUnion.projectID>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectUnion.unionID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProjectUnion.Tstamp>
  {
  }
}
