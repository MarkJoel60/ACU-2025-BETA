// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterHeader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
namespace PX.Data;

/// <summary>
/// By default works with cache <see cref="T:PX.Data.PXFilterCache" />.
/// If you don't need this logic, create inheritor in your graph.
/// </summary>
[PXAutoSave]
[DebuggerDisplay("[{FilterName}]")]
[PXCacheName("Filter Header")]
[PXPrimaryGraph(typeof (CSFilterMaint))]
public class FilterHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFilterHeader
{
  protected Guid? _FilterID;
  protected 
  #nullable disable
  string _UserName;
  protected string _ScreenID;
  protected string _ViewName;
  protected string _FilterName;
  protected bool? _IsDefault;
  protected bool? _IsShared;
  protected bool? _IsShortcut;
  protected bool? _IsSystem;
  public const string SelectorScreenID = "SELECTOR";

  [PXDBSequentialGuid(IsKey = true)]
  [PXUIField(DisplayName = "ID")]
  [PXReferentialIntegrityCheck]
  public virtual Guid? FilterID
  {
    get => this._FilterID;
    set => this._FilterID = value;
  }

  [PXDefault]
  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(Visible = false)]
  public virtual string UserName
  {
    get => this._UserName;
    set => this._UserName = value;
  }

  [PXDefault]
  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXUIField(Visible = false)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDefault]
  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "View", Visible = false)]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXDefault]
  [PXDBLocalizableString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string FilterName
  {
    get => this._FilterName;
    set => this._FilterName = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is Default")]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is Shared")]
  public virtual bool? IsShared
  {
    get => this._IsShared;
    set => this._IsShared = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is Shortcut")]
  public virtual bool? IsShortcut
  {
    get => this._IsShortcut;
    set => this._IsShortcut = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? IsSystem
  {
    get => this._IsSystem;
    set => this._IsSystem = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Is Hidden", Visible = false)]
  public virtual bool? IsHidden { get; set; }

  [PXFilterOrder]
  [PXUIField(DisplayName = "Filter Order", Visible = false)]
  public virtual long? FilterOrder { get; set; }

  [PXBool]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? IsOwned { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteID { get; set; }

  public class PK : PrimaryKeyOf<FilterHeader>.By<FilterHeader.filterID>
  {
    public static FilterHeader Find(PXGraph graph, Guid? filterID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<FilterHeader>.By<FilterHeader.filterID>.FindBy(graph, (object) filterID, options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<PX.SM.SiteMap>.By<PX.SM.SiteMap.screenID>.ForeignKeyOf<FilterHeader>.By<FilterHeader.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PX.SM.PortalMap>.By<PX.SM.PortalMap.screenID>.ForeignKeyOf<FilterHeader>.By<FilterHeader.screenID>
    {
    }
  }

  public abstract class filterID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilterHeader.filterID>
  {
  }

  public abstract class userName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterHeader.userName>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterHeader.screenID>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterHeader.viewName>
  {
  }

  public abstract class filterName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterHeader.filterName>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isDefault>
  {
  }

  public abstract class isShared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isShared>
  {
  }

  public abstract class isShortcut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isShortcut>
  {
  }

  public abstract class isSystem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isSystem>
  {
  }

  public abstract class isHidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isHidden>
  {
  }

  public abstract class filterOrder : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FilterHeader.filterOrder>
  {
  }

  public abstract class isOwned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterHeader.isOwned>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilterHeader.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilterHeader.refNoteID>
  {
  }

  public class Definition : IPrefetchable, IPXCompanyDependent
  {
    public IEnumerable<FilterHeader> Headers { get; private set; }

    public void Prefetch()
    {
      List<FilterHeader> filterHeaderList = new List<FilterHeader>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<FilterHeader>(new PXDataField("FilterID"), new PXDataField("UserName"), new PXDataField("ScreenID"), new PXDataField("ViewName"), PXDBLocalizableStringAttribute.GetValueSelect(nameof (FilterHeader), "FilterName", false), new PXDataField("IsDefault"), new PXDataField("IsShared"), new PXDataField("IsShortcut"), new PXDataField("IsSystem"), new PXDataField("IsHidden"), (PXDataField) new PXDataField<FilterHeader.filterOrder>(), new PXDataField("NoteID"), (PXDataField) new PXDataField<FilterHeader.refNoteID>(), (PXDataField) new PXDataFieldOrder("CompanyID"), (PXDataField) new PXDataFieldOrder<FilterHeader.filterOrder>()))
        filterHeaderList.Add(new FilterHeader()
        {
          FilterID = pxDataRecord.GetGuid(0),
          UserName = pxDataRecord.GetString(1),
          ScreenID = pxDataRecord.GetString(2),
          ViewName = pxDataRecord.GetString(3),
          FilterName = pxDataRecord.GetString(4),
          IsDefault = pxDataRecord.GetBoolean(5),
          IsShared = pxDataRecord.GetBoolean(6),
          IsShortcut = pxDataRecord.GetBoolean(7),
          IsSystem = pxDataRecord.GetBoolean(8),
          IsHidden = pxDataRecord.GetBoolean(9),
          FilterOrder = pxDataRecord.GetInt64(10),
          NoteID = pxDataRecord.GetGuid(11),
          RefNoteID = pxDataRecord.GetGuid(12),
          IsOwned = new bool?()
        });
      this.Headers = (IEnumerable<FilterHeader>) filterHeaderList;
    }

    public static IEnumerable<FilterHeader> Get()
    {
      FilterHeader.Definition definition = PXContext.GetSlot<FilterHeader.Definition>();
      if (definition == null)
      {
        definition = PXDatabase.GetLocalizableSlot<FilterHeader.Definition>(nameof (FilterHeader), typeof (FilterHeader));
        if (definition != null)
          PXContext.SetSlot<FilterHeader.Definition>(definition);
      }
      return definition?.Headers;
    }
  }

  public class selectorConst : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FilterHeader.selectorConst>
  {
    public selectorConst()
      : base("SELECTOR")
    {
    }
  }
}
