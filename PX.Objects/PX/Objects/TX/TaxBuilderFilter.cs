// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBuilderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TaxBuilderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _State;

  [PXString(2, IsFixed = true)]
  [PXSelector(typeof (TXImportState.stateCode))]
  [PXUIField(DisplayName = "State")]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxBuilderFilter.state>
  {
  }
}
