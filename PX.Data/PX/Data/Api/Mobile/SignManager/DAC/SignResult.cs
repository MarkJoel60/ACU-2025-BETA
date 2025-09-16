// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.SignManager.DAC.SignResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Api.Mobile.SignManager.DAC;

[PXInternalUseOnly]
public class SignResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _Index;
  protected 
  #nullable disable
  string _ViewName;
  protected int? _Result;
  protected Guid? _SignedFileId;
  protected string _Signature;
  protected Guid? _SignatureId;
  protected string _FullName;

  [PXDBIdentity(IsKey = true)]
  public virtual int? Index
  {
    get => this._Index;
    set => this._Index = value;
  }

  [PXString]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXInt]
  public virtual int? Result
  {
    get => this._Result;
    set => this._Result = value;
  }

  [PXGuid]
  public virtual Guid? SignedFileId
  {
    get => this._SignedFileId;
    set => this._SignedFileId = value;
  }

  [PXString]
  public virtual string Signature
  {
    get => this._Signature;
    set => this._Signature = value;
  }

  [PXGuid]
  public virtual Guid? SignatureId
  {
    get => this._SignatureId;
    set => this._SignatureId = value;
  }

  [PXString]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  public class PK : PrimaryKeyOf<SignResult>.By<SignResult.index>
  {
    public static SignResult Find(PXGraph graph, int? index, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SignResult>.By<SignResult.index>.FindBy(graph, (object) index, options);
    }
  }

  /// <exclude />
  public abstract class index : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SignResult.index>
  {
  }

  /// <exclude />
  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SignResult.viewName>
  {
  }

  /// <exclude />
  public abstract class result : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SignResult.result>
  {
  }
}
