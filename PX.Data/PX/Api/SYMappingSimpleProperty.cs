// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingSimpleProperty
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Api;

[PXHidden]
[Serializable]
public class SYMappingSimpleProperty : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal 
  #nullable disable
  Dictionary<string, KeyValuePair<string, string>> AttributeLikeGrids = new Dictionary<string, KeyValuePair<string, string>>();

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Scenario Name")]
  public virtual string Name { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen Name")]
  public virtual string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Provider Type")]
  [PXDefault]
  [PXSYProviderSelector]
  public virtual string ProviderType { get; set; }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? RefreshImportSimple { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? CreationProcessIsOn { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? MappingAlreadyLoaded { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? RefreshExistingMapping { get; set; }

  [PXDBGuid(false)]
  public Guid? MappingID => this.Mapping != null ? this.Mapping.MappingID : new Guid?();

  public SYMappingActive Mapping { get; set; }

  public FileInfo File { get; set; }

  public SYProviderMaint ProviderGraph { get; set; }

  internal Dictionary<string, SYImportSimple.KeyFieldModel[]> KeyObjectFieldDictionary { get; set; }

  internal Dictionary<string, SYImportSimple.NameDisplayNameModel[]> ObjectFieldDictionary { get; set; }

  internal List<SYImportSimple.NameDisplayNameModel> ValueFieldList { get; set; }

  internal List<SYImportSimple.NameDisplayNameModel> ObjectNameList { get; set; }

  internal List<string> KeyObjectNameList { get; set; }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingSimpleProperty.name>
  {
  }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingSimpleProperty.screenID>
  {
  }

  public abstract class providerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingSimpleProperty.providerType>
  {
  }

  public abstract class refreshImportSimple : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMappingSimpleProperty.refreshImportSimple>
  {
  }

  public abstract class creationProcessIsOn : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMappingSimpleProperty.creationProcessIsOn>
  {
  }

  public abstract class mappingAlreadyLoaded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMappingSimpleProperty.mappingAlreadyLoaded>
  {
  }

  public abstract class refreshExistingMapping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMappingSimpleProperty.refreshExistingMapping>
  {
  }

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingSimpleProperty.mappingID>
  {
  }
}
