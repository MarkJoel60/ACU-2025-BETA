// Decompiled with JetBrains decompiler
// Type: PX.SM.UPSnapshot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Update;
using PX.Data.Update.Storage;
using PX.DbServices.Model;
using System;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXCacheName("Snapshot")]
public class UPSnapshot : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _SnapshotID;
  protected 
  #nullable disable
  string _Name;
  protected string _Description;
  protected System.DateTime? _Date;
  protected string _Version;
  protected string _dataType;
  protected int? _SourceCompany;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Snapshot ID", Enabled = false, Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? SnapshotID
  {
    get => this._SnapshotID;
    set => this._SnapshotID = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXUIField(DisplayName = "Ready For Export", Enabled = false)]
  [PXBool]
  public virtual bool? Prepared
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPSnapshot.snapshotID), typeof (UPSnapshot.version)})] get
    {
      bool flag1 = this.LinkedCompany.HasValue && PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Negative).Any<UPCompany>((Func<UPCompany, bool>) (c =>
      {
        int? companyId = c.CompanyID;
        int num = this.LinkedCompany.Value;
        return companyId.GetValueOrDefault() == num & companyId.HasValue;
      }));
      bool flag2 = false;
      try
      {
        flag2 = this.SnapshotID.HasValue && PXStorageHelper.IsStorageSetup() && PXStorageHelper.GetProvider().Exists(this.SnapshotID.Value);
      }
      catch
      {
      }
      if (!flag1 && !flag2)
        return new bool?();
      System.Version version1 = PXVersionHelper.Convert(IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version);
      System.Version version2 = PXVersionHelper.Convert(this.Version);
      return new bool?(flag2 && !string.IsNullOrWhiteSpace(this.Version) && version1.Equals(version2));
    }
  }

  [PXUIField(DisplayName = "Size on Disk (MB)", Enabled = false)]
  [PXDecimal(2)]
  public virtual Decimal? SizePrepared
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPSnapshot.snapshotID), typeof (UPSnapshot.prepared)})] get
    {
      bool? prepared = this.Prepared;
      bool flag = true;
      return !(prepared.GetValueOrDefault() == flag & prepared.HasValue) ? new Decimal?() : new Decimal?((Decimal) PXStorageHelper.GetProvider().GetSize(this.SnapshotID.Value) / 1048576M);
    }
  }

  [PXUIField(DisplayName = "Size", Enabled = false, Visible = false)]
  [PXLong]
  public virtual long? Size { get; set; }

  [PXUIField(DisplayName = "Creation Date", Enabled = false)]
  [PXDBDateAndTime(InputMask = "g")]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXUIField(DisplayName = "Version", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  [PXDBString(20, IsUnicode = true, IsFixed = true)]
  public virtual string Version
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPSnapshot.linkedCompany)})] get => this._Version;
    set => this._Version = value;
  }

  [PXUIField(DisplayName = "Host", Enabled = false, Visibility = PXUIVisibility.Invisible, Visible = false)]
  [PXDBString(256 /*0x0100*/, IsUnicode = true, IsFixed = true)]
  public virtual string Host { get; set; }

  [PXUIField(DisplayName = "Export Mode", Enabled = false)]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string ExportMode { get; set; }

  [PXUIField(DisplayName = "Customization", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  [PXDBString(IsUnicode = true, IsFixed = true)]
  public virtual string Customization { get; set; }

  [PXUIField(DisplayName = "Master Tenant", Enabled = false, Visibility = PXUIVisibility.Invisible, Visible = false)]
  [PXDBString(256 /*0x0100*/, IsUnicode = true, IsFixed = true)]
  public virtual string MasterCompany { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Tenant ID", Enabled = false)]
  [CompanySelector(DescriptionField = typeof (UPCompany.description))]
  public virtual int? SourceCompany
  {
    get => this._SourceCompany;
    set => this._SourceCompany = value;
  }

  [PXDBInt]
  public virtual int? LinkedCompany { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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
  [PXUIField(DisplayName = "Created")]
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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Safe")]
  public virtual bool? IsSafe { get; set; }

  [PXUIField(DisplayName = "Under Deletion", Enabled = false)]
  [PXDBBool]
  public virtual bool? IsUnderDeletion { get; set; }

  public class PK : PrimaryKeyOf<UPSnapshot>.By<UPSnapshot.snapshotID>
  {
    public static UPSnapshot Find(PXGraph graph, Guid? snapshotID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UPSnapshot>.By<UPSnapshot.snapshotID>.FindBy(graph, (object) snapshotID, options);
    }
  }

  public static class FK
  {
    public class SourceCompany : 
      PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>.ForeignKeyOf<UPSnapshot>.By<UPSnapshot.sourceCompany>
    {
    }
  }

  public abstract class snapshotID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshot.snapshotID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.name>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.description>
  {
  }

  public abstract class prepared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSnapshot.prepared>
  {
  }

  public abstract class sizePrepared : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UPSnapshot.sizePrepared>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  UPSnapshot.size>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPSnapshot.date>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.version>
  {
  }

  public abstract class host : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.host>
  {
  }

  public abstract class exportMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.exportMode>
  {
  }

  public abstract class customization : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.customization>
  {
  }

  public abstract class masterCompany : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSnapshot.masterCompany>
  {
  }

  public abstract class sourceCompany : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSnapshot.sourceCompany>
  {
  }

  public abstract class linkedCompany : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSnapshot.linkedCompany>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshot.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshot.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPSnapshot.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UPSnapshot.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshot.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPSnapshot.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UPSnapshot.lastModifiedDateTime>
  {
  }

  public abstract class isSafe : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSnapshot.isSafe>
  {
  }

  public abstract class isUnderDeletion : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSnapshot.isUnderDeletion>
  {
  }
}
