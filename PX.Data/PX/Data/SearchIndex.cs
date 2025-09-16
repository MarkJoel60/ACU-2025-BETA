// Decompiled with JetBrains decompiler
// Type: PX.Data.SearchIndex
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Data;

/// <summary>Indexed contents of searchable DACs.</summary>
[DebuggerDisplay("ID={NoteID} Category={Category} Content={Content}")]
[PXCacheName("Search Index")]
public class SearchIndex : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Content;
  protected byte[] _tstamp;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? IndexID { get; set; }

  [PXDBInt]
  public virtual int? Category { get; set; }

  [PXDBText(IsUnicode = true)]
  public virtual string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  public virtual string EntityType { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXInt]
  public virtual int? Top { get; set; }

  public class PK : PrimaryKeyOf<SearchIndex>.By<SearchIndex.noteID>
  {
    public static SearchIndex Find(PXGraph graph, Guid? noteID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SearchIndex>.By<SearchIndex.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SearchIndex.noteID>
  {
  }

  /// <exclude />
  public abstract class indexID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SearchIndex.indexID>
  {
  }

  /// <exclude />
  public abstract class category : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SearchIndex.category>
  {
  }

  /// <exclude />
  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SearchIndex.content>
  {
  }

  /// <exclude />
  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SearchIndex.entityType>
  {
  }

  /// <exclude />
  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SearchIndex.Tstamp>
  {
  }

  /// <exclude />
  public abstract class top : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SearchIndex.top>
  {
  }
}
