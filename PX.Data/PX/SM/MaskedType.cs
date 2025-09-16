// Decompiled with JetBrains decompiler
// Type: PX.SM.MaskedType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Masked Type")]
public class MaskedType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _EntityType;
  private string _Text;

  [PXString(128 /*0x80*/, IsKey = true)]
  [PXUIField(DisplayName = "Entity Type", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string EntityTypeName
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Entity Type")]
  public string Text
  {
    get => !string.IsNullOrEmpty(this._Text) ? this._Text : this._EntityType;
    set => this._Text = value;
  }

  public class PK : PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>
  {
    public static MaskedType Find(PXGraph graph, string entityTypeName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>.FindBy(graph, (object) entityTypeName, options);
    }
  }

  public abstract class entityTypeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MaskedType.entityTypeName>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MaskedType.text>
  {
  }
}
