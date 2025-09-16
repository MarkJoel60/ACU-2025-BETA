// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("Asset Class")]
[Serializable]
public class FAClass : FixedAsset
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<FAClass.assetCD, LeftJoin<FixedAsset.FADetails, On<FixedAsset.FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FixedAsset.assetID>, And<FALocationHistory.revisionID, Equal<FixedAsset.FADetails.locationRevID>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FALocationHistory.locationID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FALocationHistory.employeeID>>, LeftJoin<FAClass, On<FAClass.assetID, Equal<FixedAsset.classID>>>>>>>, Where<FAClass.recordType, Equal<Current<FAClass.recordType>>>>), new Type[] {typeof (FAClass.assetCD), typeof (FAClass.description), typeof (FixedAsset.classID), typeof (FAClass.description), typeof (FixedAsset.depreciable), typeof (FAClass.underConstruction), typeof (FAClass.usefulLife), typeof (FAClass.assetTypeID), typeof (FixedAsset.FADetails.status), typeof (PX.Objects.GL.Branch.branchCD), typeof (EPEmployee.acctName), typeof (FALocationHistory.department)}, Filterable = true)]
  [FARecordType.Numbering]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AssetCD { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Fixed Asset Sub. from")]
  public override string FASubMask { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Accumulated Depreciation Sub. from")]
  [PXUIRequired(typeof (FixedAsset.depreciable))]
  public override string AccumDeprSubMask { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Depreciation Expense Sub. from")]
  [PXUIRequired(typeof (FixedAsset.depreciable))]
  public override string DeprExpenceSubMask { get; set; }

  [SubAccountMask(DisplayName = "Combine Proceeds Sub. from")]
  public override string ProceedsSubMask { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Gain/Loss Sub. from")]
  public override string GainLossSubMask { get; set; }

  public new class PK : PrimaryKeyOf<FAClass>.By<FAClass.assetID>
  {
    public static FAClass Find(PXGraph graph, int? assetID, PKFindOptions options = 0)
    {
      return (FAClass) PrimaryKeyOf<FAClass>.By<FAClass.assetID>.FindBy(graph, (object) assetID, options);
    }
  }

  public class UK : PrimaryKeyOf<FAClass>.By<FAClass.assetCD>
  {
    public static FAClass Find(PXGraph graph, string assetCD, PKFindOptions options = 0)
    {
      return (FAClass) PrimaryKeyOf<FAClass>.By<FAClass.assetCD>.FindBy(graph, (object) assetCD, options);
    }
  }

  public new abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAClass.assetID>
  {
  }

  public new abstract class recordType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAClass.recordType>
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAClass.active>
  {
  }

  public new abstract class assetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAClass.assetCD>
  {
  }

  public new abstract class assetTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAClass.assetTypeID>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAClass.description>
  {
  }

  public new abstract class usefulLife : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAClass.usefulLife>
  {
  }

  public new abstract class underConstruction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAClass.underConstruction>
  {
  }

  public new abstract class fASubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAClass.fASubMask>
  {
  }

  public new abstract class accumDeprSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAClass.accumDeprSubMask>
  {
  }

  public new abstract class deprExpenceSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAClass.deprExpenceSubMask>
  {
  }

  public new abstract class proceedsSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAClass.proceedsSubMask>
  {
  }

  public new abstract class gainLossSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAClass.gainLossSubMask>
  {
  }
}
