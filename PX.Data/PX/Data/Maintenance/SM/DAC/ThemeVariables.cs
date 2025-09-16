// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.DAC.ThemeVariables
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Maintenance.SM.DAC;

public class ThemeVariables : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? EntityNoteID { get; set; }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  public 
  #nullable disable
  string Theme { get; set; }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  public string VariableName { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  public string Value { get; set; }

  public abstract class entityNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ThemeVariables.entityNoteID>
  {
  }

  public abstract class theme : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ThemeVariables.theme>
  {
  }

  public abstract class variableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ThemeVariables.variableName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ThemeVariables.value>
  {
  }
}
