// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.ProcessAssetFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXHidden]
[Serializable]
public class ProcessAssetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BookID;
  protected int? _ParentAssetID;
  protected int? _ClassID;

  [Organization(true)]
  public virtual int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (ProcessAssetFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  public virtual int? ParentAssetID
  {
    get => this._ParentAssetID;
    set => this._ParentAssetID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.classType>, And<FixedAsset.depreciable, Equal<True>, And<FixedAsset.underConstruction, NotEqual<True>>>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  public abstract class organizationID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ProcessAssetFilter.branchID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProcessAssetFilter.bookID>
  {
  }

  public abstract class parentAssetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProcessAssetFilter.parentAssetID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProcessAssetFilter.classID>
  {
  }
}
