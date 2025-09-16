// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadAllowedFileTypes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (UploadAllowedFileTypesMaint))]
[Serializable]
public class UploadAllowedFileTypes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FileExt;
  protected string _IconUrl;
  protected bool? _Forbidden;
  protected string _DefApplication;

  [PXDBString(IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "File Extension")]
  public virtual string FileExt
  {
    get => this._FileExt;
    set => this._FileExt = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Icon URL")]
  public virtual string IconUrl
  {
    get => this._IconUrl;
    set => this._IconUrl = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Forbidden")]
  public virtual bool? Forbidden
  {
    get => this._Forbidden;
    set => this._Forbidden = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Image")]
  public bool? IsImage { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Default Application")]
  public virtual string DefApplication
  {
    get => this._DefApplication;
    set => this._DefApplication = value;
  }

  public abstract class fileExt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadAllowedFileTypes.fileExt>
  {
  }

  public abstract class iconUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadAllowedFileTypes.iconUrl>
  {
  }

  public abstract class forbidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadAllowedFileTypes.forbidden>
  {
  }

  public abstract class isImage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadAllowedFileTypes.isImage>
  {
  }

  public abstract class defApplication : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadAllowedFileTypes.defApplication>
  {
  }
}
