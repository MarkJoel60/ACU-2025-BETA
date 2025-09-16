// Decompiled with JetBrains decompiler
// Type: PX.Data.DialogAnswer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[Serializable]
internal class DialogAnswer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _Index;
  protected 
  #nullable disable
  string _ViewName;
  protected int? _Answer;

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
  public virtual int? Answer
  {
    get => this._Answer;
    set => this._Answer = value;
  }

  /// <exclude />
  public abstract class index : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DialogAnswer.index>
  {
  }

  /// <exclude />
  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DialogAnswer.viewName>
  {
  }

  /// <exclude />
  public abstract class answer : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DialogAnswer.answer>
  {
  }
}
