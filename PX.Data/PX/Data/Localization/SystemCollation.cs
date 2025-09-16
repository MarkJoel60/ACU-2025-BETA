// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.SystemCollation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Localization;

/// <exclude />
public class SystemCollation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CollationName;
  protected string _LocaleName;

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string CollationName
  {
    get => this._CollationName;
    set => this._CollationName = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string CollationNameCS { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string CollationNameLatin { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string CollationNameCSLatin { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string SqlDialect { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string LocaleName
  {
    get => this._LocaleName;
    set => this._LocaleName = value;
  }

  [PXDBInt]
  public virtual int? Priority { get; set; }

  /// <exclude />
  public abstract class collationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SystemCollation.collationName>
  {
  }

  /// <exclude />
  public abstract class collationNameCS : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SystemCollation.collationNameCS>
  {
  }

  /// <exclude />
  public abstract class collationNameLatin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SystemCollation.collationNameLatin>
  {
  }

  /// <exclude />
  public abstract class collationNameCSLatin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SystemCollation.collationNameCSLatin>
  {
  }

  /// <exclude />
  public abstract class sqlDialect : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemCollation.sqlDialect>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemCollation.localeName>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SystemCollation.priority>
  {
  }
}
