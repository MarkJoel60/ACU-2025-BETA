// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSkill
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

[PXCacheName("Skill")]
[PXPrimaryGraph(typeof (SkillMaint))]
[Serializable]
public class FSSkill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? SkillID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  public virtual 
  #nullable disable
  string SkillCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Driver Skill")]
  public virtual bool? IsDriverSkill { get; set; }

  public class PK : PrimaryKeyOf<FSSkill>.By<FSSkill.skillID>
  {
    public static FSSkill Find(PXGraph graph, int? skillID, PKFindOptions options = 0)
    {
      return (FSSkill) PrimaryKeyOf<FSSkill>.By<FSSkill.skillID>.FindBy(graph, (object) skillID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSSkill>.By<FSSkill.skillCD>
  {
    public static FSSkill Find(PXGraph graph, string skillCD, PKFindOptions options = 0)
    {
      return (FSSkill) PrimaryKeyOf<FSSkill>.By<FSSkill.skillCD>.FindBy(graph, (object) skillCD, options);
    }
  }

  public abstract class skillID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSkill.skillID>
  {
  }

  public abstract class skillCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSkill.skillCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSkill.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSkill.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSkill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSkill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSkill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSkill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSkill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSkill.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSkill.Tstamp>
  {
  }

  public abstract class isDriverSkill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSkill.isDriverSkill>
  {
  }
}
