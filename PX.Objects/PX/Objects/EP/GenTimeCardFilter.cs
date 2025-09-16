// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.GenTimeCardFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class GenTimeCardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private DateTime? _LastDateGenerated;
  private DateTime? _GenerateUntil;

  [PXDate]
  [PXUIField(DisplayName = "From", Enabled = false)]
  public virtual DateTime? LastDateGenerated
  {
    get => this._LastDateGenerated;
    set => this._LastDateGenerated = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Until")]
  public virtual DateTime? GenerateUntil
  {
    get => this._GenerateUntil;
    set => this._GenerateUntil = value;
  }

  public abstract class lastDateGenerated : 
    BqlType<IBqlDateTime, DateTime>.Field<
    #nullable disable
    GenTimeCardFilter.lastDateGenerated>
  {
  }

  public abstract class generateUntil : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GenTimeCardFilter.generateUntil>
  {
  }
}
