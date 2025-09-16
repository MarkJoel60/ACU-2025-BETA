// Decompiled with JetBrains decompiler
// Type: PX.SM.AvailableVersion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Available Version")]
[Serializable]
public class AvailableVersion : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Type;
  protected string _Version;
  protected System.DateTime? _Date;
  protected bool? _Uploaded;
  protected bool? _Restricted;
  protected string _Description;
  protected string _Notes;
  protected byte[] _Launcher;
  protected string _PackageKey;

  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXUIField(DisplayName = "Version", Enabled = false)]
  [PXString(20, IsKey = true, InputMask = "")]
  [PXDefault("")]
  public virtual string Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [PXUIField(DisplayName = "Published Date", Enabled = false)]
  [PXDate(InputMask = "g")]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXUIField(DisplayName = "Ready to Install", Enabled = false)]
  [PXBool]
  public virtual bool? Uploaded
  {
    [PXDependsOnFields(new System.Type[] {typeof (AvailableVersion.launcher)})] get
    {
      return new bool?(this.Launcher != null && this.Launcher.Length != 0);
    }
  }

  [PXUIField(DisplayName = "Restricted", Enabled = false)]
  [PXBool]
  public virtual bool? Restricted
  {
    get => this._Restricted;
    set => this._Restricted = value;
  }

  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXString(1024 /*0x0400*/)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXUIField(DisplayName = "Notes", Enabled = false)]
  [PXString(4000)]
  public virtual string Notes
  {
    get => this._Notes;
    set => this._Notes = value;
  }

  [PXVariant]
  public virtual byte[] Launcher
  {
    get => this._Launcher;
    set => this._Launcher = value;
  }

  [PXString(1024 /*0x0400*/)]
  public virtual string PackageKey
  {
    get => this._PackageKey;
    set => this._PackageKey = value;
  }

  [PXUIField(DisplayName = "Validation Status", Visibility = PXUIVisibility.Invisible)]
  [PXInt]
  public virtual int? ValidationStatus { get; set; }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersion.type>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersion.version>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AvailableVersion.date>
  {
  }

  public abstract class uploaded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AvailableVersion.uploaded>
  {
  }

  public abstract class restricted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AvailableVersion.restricted>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersion.description>
  {
  }

  public abstract class notes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersion.notes>
  {
  }

  public abstract class launcher : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AvailableVersion.launcher>
  {
  }

  public abstract class packageKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersion.packageKey>
  {
  }

  public abstract class validationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AvailableVersion.validationStatus>
  {
    public static int Error = 1;
    public static int OK = 2;
  }
}
