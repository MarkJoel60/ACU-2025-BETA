// Decompiled with JetBrains decompiler
// Type: PX.Data.AttribParams
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

public class AttribParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _screenID;
  protected string _typeName;

  [PXDBString(8, InputMask = "CCCCCCCC")]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen ID")]
  public virtual string ScreenID
  {
    get => this._screenID;
    set => this._screenID = value?.Replace(".", string.Empty);
  }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Type")]
  public virtual string TypeName
  {
    get => this._typeName;
    set => this._typeName = value?.Replace(".", string.Empty);
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AttribParams.screenID>
  {
  }

  public abstract class typeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AttribParams.typeName>
  {
  }
}
