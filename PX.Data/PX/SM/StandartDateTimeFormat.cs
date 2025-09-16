// Decompiled with JetBrains decompiler
// Type: PX.SM.StandartDateTimeFormat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Standart Date Time Format")]
public class StandartDateTimeFormat : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Standart Pattern", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string Pattern { get; set; }

  public class PK : PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>
  {
    public static StandartDateTimeFormat Find(PXGraph graph, string pattern, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.FindBy(graph, (object) pattern, options);
    }
  }

  /// <exclude />
  public abstract class pattern : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StandartDateTimeFormat.pattern>
  {
  }
}
