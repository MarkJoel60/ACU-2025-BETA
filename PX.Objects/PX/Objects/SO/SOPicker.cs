// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPicker
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOPicker : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXParent(typeof (SOPicker.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOPickingWorksheet))]
  [PXUIField(DisplayName = "Picker Nbr.")]
  public virtual int? PickerNbr { get; set; }

  [Site]
  [PXDefault(typeof (SOPickingWorksheet.siteID))]
  [PXForeignReference(typeof (SOPicker.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
  public virtual int? SiteID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "User", Enabled = false)]
  [PXSelector(typeof (Search<Users.pKID, Where<Users.isHidden, Equal<False>>>), SubstituteKey = typeof (Users.username))]
  [PXForeignReference(typeof (SOPicker.FK.User))]
  public virtual Guid? UserID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Cart ID", IsReadOnly = true)]
  [PXSelector(typeof (SearchFor<INCart.cartID>.In<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXForeignReference(typeof (SOPicker.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Nbr. of Totes")]
  public int? NumberOfTotes { get; set; }

  [Location(typeof (SOPicker.siteID), DisplayName = "First Location", Enabled = false)]
  [PXDefault]
  [PXForeignReference(typeof (SOPicker.FK.FirstLocation))]
  public virtual int? FirstLocationID { get; set; }

  [Location(typeof (SOPicker.siteID), DisplayName = "Last Location", Enabled = false)]
  [PXDefault]
  [PXForeignReference(typeof (SOPicker.FK.LastLocation))]
  public virtual int? LastLocationID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Path Length")]
  public virtual int? PathLength { get; set; }

  [Location(typeof (SOPicker.siteID), DisplayName = "Sorting Location", Enabled = false)]
  [PXForeignReference(typeof (SOPicker.FK.SortingLocation))]
  public virtual int? SortingLocationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirmed")]
  public bool? Confirmed { get; set; }

  [PXString]
  public string PickListNbr
  {
    get => $"{this.WorksheetNbr}/{this.PickerNbr.ToString()}";
    set
    {
    }
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>
  {
    public static SOPicker Find(
      PXGraph graph,
      string worksheetNbr,
      int? pickerNbr,
      PKFindOptions options = 0)
    {
      return (SOPicker) PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.FindBy(graph, (object) worksheetNbr, (object) pickerNbr, options);
    }
  }

  public static class FK
  {
    public class User : PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOPicker>.By<SOPicker.userID>
    {
    }

    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPicker>.By<SOPicker.worksheetNbr>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<SOPicker>.By<SOPicker.siteID, SOPicker.cartID>
    {
    }

    public class FirstLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPicker>.By<SOPicker.firstLocationID>
    {
    }

    public class LastLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPicker>.By<SOPicker.lastLocationID>
    {
    }

    public class SortingLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPicker>.By<SOPicker.sortingLocationID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPicker>.By<SOPicker.siteID>
    {
    }
  }

  public abstract class worksheetNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPicker.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.pickerNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.siteID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPicker.userID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.cartID>
  {
  }

  public abstract class numberOfTotes : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.numberOfTotes>
  {
  }

  public abstract class firstLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.firstLocationID>
  {
  }

  public abstract class lastLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.lastLocationID>
  {
  }

  public abstract class pathLength : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.pathLength>
  {
  }

  public abstract class sortingLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPicker.sortingLocationID>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPicker.confirmed>
  {
  }

  public abstract class pickListNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPicker.pickListNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPicker.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPicker.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPicker.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPicker.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPicker.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPicker.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPicker.lastModifiedDateTime>
  {
  }
}
