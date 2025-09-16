// Decompiled with JetBrains decompiler
// Type: PX.SM.BlobProviderSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class BlobProviderSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public 
  #nullable disable
  string Name { get; set; }

  [PXDBString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Value")]
  public string Value { get; set; }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlobProviderSettings.name>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlobProviderSettings.value>
  {
  }
}
