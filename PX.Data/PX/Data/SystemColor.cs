// Decompiled with JetBrains decompiler
// Type: PX.Data.SystemColor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

[Serializable]
public class SystemColor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ColorID;
  protected 
  #nullable disable
  string _ColorName;
  protected int? _ColorValue;

  [PXDBInt]
  public virtual int? ColorID
  {
    get => this._ColorID;
    set => this._ColorID = value;
  }

  [PXDBString(255 /*0xFF*/, IsKey = true, InputMask = "")]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Name")]
  public virtual string ColorName
  {
    get => this._ColorName;
    set => this._ColorName = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "RGB")]
  public virtual int? ColorValue
  {
    get => this._ColorValue;
    set => this._ColorValue = value;
  }

  public abstract class colorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SystemColor.colorID>
  {
  }

  public abstract class colorName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemColor.colorName>
  {
  }

  public abstract class colorValue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SystemColor.colorValue>
  {
  }
}
