// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingActive
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYMappingActive : SYMapping
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSYMappingActiveSelector]
  public override 
  #nullable disable
  string Name { get; set; }

  [PXDBString(1, IsFixed = true)]
  [SYMapping.mappingType.StringList]
  [PXDefault]
  public override string MappingType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [MappingStatus.StringList]
  [PXDefault("N")]
  public override string Status { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen Name", Visibility = PXUIVisibility.SelectorVisible)]
  public override string ScreenID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Screen Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenDescription
  {
    get
    {
      if (!string.IsNullOrEmpty(this.ScreenID))
      {
        PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.ScreenID);
        if (mapNodeByScreenId != null)
          return mapNodeByScreenId.Title;
      }
      return (string) null;
    }
  }

  [PXDBScalar(typeof (Search<SYProvider.providerType, Where<SYProvider.providerID, Equal<SYMapping.providerID>>>))]
  public string ProviderType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Batch Size", Enabled = true)]
  [PXUnboundDefault(0)]
  public int? BatchSize { get; set; }

  private void SetStatus(string status) => this.Status = status;

  public void SetStatusCreated() => this.SetStatus("N");

  public void SetStatusPrepared() => this.SetStatus("P");

  public void SetStatusProcessed() => this.SetStatus("I");

  public void SetStatusPartiallyProcessed() => this.SetStatus("F");

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingActive.name>
  {
  }

  public new abstract class mappingType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingActive.mappingType>
  {
    public class mappingConst : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMappingActive.mappingType.mappingConst>
    {
      public mappingConst()
        : base("")
      {
      }

      public mappingConst(string mapping)
        : base(mapping)
      {
      }
    }

    public class typeImport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMappingActive.mappingType.typeImport>
    {
      public typeImport()
        : base("I")
      {
      }
    }

    public class typeExport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMappingActive.mappingType.typeExport>
    {
      public typeExport()
        : base("E")
      {
      }
    }
  }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingActive.screenID>
  {
  }

  public abstract class screenDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingActive.screenDescription>
  {
  }

  public abstract class providerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingActive.providerType>
  {
  }

  public abstract class batchSize : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingActive.batchSize>
  {
  }
}
