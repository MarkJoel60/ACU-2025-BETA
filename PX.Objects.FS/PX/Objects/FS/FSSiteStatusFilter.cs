// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSiteStatusFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSSiteStatusFilter : INSiteStatusFilter
{
  protected int? _Mode;
  protected DateTime? _HistoryDate;

  [PXUIField(DisplayName = "Warehouse")]
  [Site]
  [InterBranchRestrictor(typeof (Where2<Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<FSServiceOrder.branchID>>>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<FSSchedule.branchID>>>>))]
  [PXDefault(typeof (PX.Objects.IN.INRegister.siteID))]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Selection Mode")]
  [SOAddItemMode.List]
  public virtual int? Mode
  {
    get => this._Mode;
    set => this._Mode = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Sold Since")]
  public virtual DateTime? HistoryDate
  {
    get => this._HistoryDate;
    set => this._HistoryDate = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Available Stock Items Only")]
  public override bool? OnlyAvailable { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Stock Items")]
  public virtual bool? IncludeIN { get; set; }

  [PXString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type")]
  [FSLineType.List]
  [PXDefault("<ALL>")]
  public virtual 
  #nullable disable
  string LineType { get; set; }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusFilter.siteID>
  {
  }

  public new abstract class inventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusFilter.inventory>
  {
  }

  public abstract class mode : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusFilter.mode>
  {
  }

  public abstract class historyDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSiteStatusFilter.historyDate>
  {
  }

  public new abstract class onlyAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSiteStatusFilter.onlyAvailable>
  {
  }

  public abstract class includeIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSiteStatusFilter.includeIN>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusFilter.lineType>
  {
  }
}
