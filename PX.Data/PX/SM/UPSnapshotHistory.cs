// Decompiled with JetBrains decompiler
// Type: PX.SM.UPSnapshotHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Snapshot Restoration History")]
[Serializable]
public class UPSnapshotHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _HistoryID;
  protected Guid? _SnapshotID;
  protected int? _TargetCompany;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "History ID", Enabled = false, Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? HistoryID
  {
    get => this._HistoryID;
    set => this._HistoryID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Snapshot ID", Enabled = false, Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? SnapshotID
  {
    get => this._SnapshotID;
    set => this._SnapshotID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Company ID", Enabled = false)]
  [CompanySelector(DescriptionField = typeof (UPCompany.description))]
  public virtual int? TargetCompany
  {
    get => this._TargetCompany;
    set => this._TargetCompany = value;
  }

  [PXDisplaySelector(typeof (Users.pKID), new System.Type[] {typeof (Users.username)}, DescriptionField = typeof (Users.username), SubstituteKey = typeof (Users.username))]
  [PXUIField(DisplayName = "User", Enabled = false, Visible = true)]
  public virtual Guid? UserID
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPSnapshotHistory.createdByID)})] get
    {
      return this._CreatedByID;
    }
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "User", Enabled = false, Visible = true)]
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
  [PXUIField(DisplayName = "Restoration Date", Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Safe")]
  public virtual bool? IsSafe { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Dismissed")]
  public virtual bool? Dismissed { get; set; }

  public class PK : PrimaryKeyOf<UPSnapshotHistory>.By<UPSnapshotHistory.historyID>
  {
    public static UPSnapshotHistory Find(PXGraph graph, int? historyID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UPSnapshotHistory>.By<UPSnapshotHistory.historyID>.FindBy(graph, (object) historyID, options);
    }
  }

  public static class FK
  {
    public class Snapshot : 
      PrimaryKeyOf<UPSnapshot>.By<UPSnapshot.snapshotID>.ForeignKeyOf<UPSnapshotHistory>.By<UPSnapshotHistory.snapshotID>
    {
    }

    public class TargetCompany : 
      PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>.ForeignKeyOf<UPSnapshotHistory>.By<UPSnapshotHistory.targetCompany>
    {
    }
  }

  public abstract class historyID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSnapshotHistory.historyID>
  {
  }

  public abstract class snapshotID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshotHistory.snapshotID>
  {
  }

  public abstract class targetCompany : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSnapshotHistory.targetCompany>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UPSnapshotHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPSnapshotHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UPSnapshotHistory.createdDateTime>
  {
  }

  public abstract class isSafe : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSnapshotHistory.isSafe>
  {
  }

  public abstract class dismissed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSnapshotHistory.dismissed>
  {
  }
}
