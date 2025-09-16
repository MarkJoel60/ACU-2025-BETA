// Decompiled with JetBrains decompiler
// Type: PX.SM.Locale
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Locale")]
[PXPrimaryGraph(typeof (LocaleMaintenance))]
public class Locale : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LocaleName;
  protected string _Description;
  protected string _TranslatedName;
  protected short? _Number;
  protected bool? _IsActive;
  protected string _CultureReadableName;
  protected bool? _IsDefault;
  protected bool? _IsAlternative;

  [PXDBString(10, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Locale Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXCultureSelector]
  [PXDefault]
  public virtual string LocaleName
  {
    get => this._LocaleName;
    set => this._LocaleName = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Locale Name in Locale Language", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  public virtual string TranslatedName
  {
    get => this._TranslatedName;
    set => this._TranslatedName = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Validation Warnings", Visible = false)]
  public virtual bool? ShowValidationWarnings { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sequence")]
  public virtual short? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(false)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXString]
  [PXUIField(DisplayName = "English Name", Enabled = false)]
  public virtual string CultureReadableName
  {
    get => this._CultureReadableName;
    set => this._CultureReadableName = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (LocaleFormat.formatID))]
  public virtual int? FormatID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Default Language", Enabled = false, Visible = false)]
  [PXDefault(false)]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Alternative Language", Enabled = false, Visible = false)]
  [PXDefault(false)]
  public virtual bool? IsAlternative
  {
    get => this._IsAlternative;
    set => this._IsAlternative = value;
  }

  public class PK : PrimaryKeyOf<Locale>.By<Locale.localeName>
  {
    public static Locale Find(PXGraph graph, string localeName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Locale>.By<Locale.localeName>.FindBy(graph, (object) localeName, options);
    }
  }

  /// <exclude />
  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Locale.localeName>
  {
  }

  /// <exclude />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Locale.description>
  {
  }

  public abstract class translatedName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Locale.translatedName>
  {
  }

  /// <exclude />
  public abstract class showValidationWarnings : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Locale.showValidationWarnings>
  {
  }

  /// <exclude />
  public abstract class number : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Locale.number>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Locale.isActive>
  {
  }

  /// <exclude />
  public abstract class cultureReadableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Locale.cultureReadableName>
  {
  }

  /// <exclude />
  public abstract class formatID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Locale.formatID>
  {
  }

  /// <exclude />
  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Locale.isDefault>
  {
  }

  /// <exclude />
  public abstract class isAlternative : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Locale.isAlternative>
  {
  }
}
