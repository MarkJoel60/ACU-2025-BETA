// Decompiled with JetBrains decompiler
// Type: PX.CS.RMNewColumnSetPanel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.CS;

[Serializable]
public class RMNewColumnSetPanel : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.CS.RMColumnSet.ColumnSetCode" />
  [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Column Set Code", Required = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ColumnSetCode { get; set; }

  /// <summary>Type of the new column set.</summary>
  [PXString(2, IsFixed = true)]
  [PXStringList(new string[] {"GL", "PM"}, new string[] {"GL", "PM"})]
  [PXUIField(DisplayName = "Type", Required = true)]
  [PXDefault("GL")]
  public virtual string Type { get; set; }

  /// <summary>Description of the new column set.</summary>
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Required = true)]
  [PXDefault]
  public virtual string Description { get; set; }

  public abstract class columnSetCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMNewColumnSetPanel.columnSetCode>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMNewColumnSetPanel.type>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMNewColumnSetPanel.description>
  {
  }
}
