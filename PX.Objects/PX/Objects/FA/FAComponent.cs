// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAComponent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Component")]
[Serializable]
public class FAComponent : FixedAsset
{
  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [PXUIField]
  [FARecordType.List]
  public override 
  #nullable disable
  string RecordType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<FAComponent.assetCD, Where<FixedAsset.parentAssetID, IsNull, Or<FAComponent.assetCD, Equal<Current<FAComponent.assetCD>>>>>))]
  public override string AssetCD { get; set; }

  public new abstract class recordType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAComponent.recordType>
  {
  }

  public new abstract class assetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAComponent.assetCD>
  {
  }
}
