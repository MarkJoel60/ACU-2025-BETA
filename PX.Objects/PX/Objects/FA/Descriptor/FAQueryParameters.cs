// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Descriptor.FAQueryParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.DAC;
using PX.Objects.GL.Attributes;

#nullable enable
namespace PX.Objects.FA.Descriptor;

public class FAQueryParameters : QueryParameters
{
  [OrganizationTree(typeof (FAQueryParameters.organizationID), typeof (FAQueryParameters.branchID), null, false)]
  public int? OrgBAccountID { get; set; }

  [PXSelector(typeof (Search<FixedAsset.assetID>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXDBInt]
  public virtual int? AssetID { get; set; }

  [PXSelector(typeof (Search<FABook.bookID>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXDBInt]
  public virtual int? BookID { get; set; }

  public new abstract class organizationID : IBqlField, IBqlOperand
  {
  }

  public new abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class orgBAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FAQueryParameters.orgBAccountID>
  {
  }

  public abstract class assetID : IBqlField, IBqlOperand
  {
  }

  public abstract class bookID : IBqlField, IBqlOperand
  {
  }
}
