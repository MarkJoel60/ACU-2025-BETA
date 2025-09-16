// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodSelection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class FABookPeriodSelection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _GLBookCD;
  protected string _CurPeriodID;

  [PXString]
  [GLBookDefault]
  public virtual string GLBookCD
  {
    get => this._GLBookCD;
    set => this._GLBookCD = value;
  }

  [PXString]
  [CurrentGLBookPeriodDefault]
  public virtual string CurPeriodID
  {
    get => this._CurPeriodID;
    set => this._CurPeriodID = value;
  }

  public abstract class gLBookCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriodSelection.gLBookCD>
  {
  }

  public abstract class curPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodSelection.curPeriodID>
  {
  }
}
