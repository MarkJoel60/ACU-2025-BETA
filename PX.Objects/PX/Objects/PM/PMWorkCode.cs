// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCode
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

/// <summary>
/// Represents a workers' compensation code.
/// The records of this type are created and edited through the Workers' Compensation Codes (PR209800) form
/// (which corresponds to the <see cref="!:PX.Objects.PR.PRWorkCodeMaint" /> graph).
/// </summary>
[PXCacheName("WorkCode")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMWorkCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Description;
  protected Guid? _NoteID;

  [PXReferentialIntegrityCheck]
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string WorkCodeID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>
  {
    public static PMWorkCode Find(PXGraph graph, string workCodeID, PKFindOptions options = 0)
    {
      return (PMWorkCode) PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.FindBy(graph, (object) workCodeID, options);
    }
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWorkCode.workCodeID>
  {
    public const int Length = 15;
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWorkCode.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWorkCode.isActive>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWorkCode.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMWorkCode.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWorkCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWorkCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCode.lastModifiedDateTime>
  {
  }
}
