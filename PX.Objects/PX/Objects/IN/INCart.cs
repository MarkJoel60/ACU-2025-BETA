// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCart
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INCart : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDefault(typeof (INSite.siteID))]
  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (INCart.FK.Site))]
  public int? SiteID { get; set; }

  [PXReferentialIntegrityCheck]
  [PXDBForeignIdentity(typeof (INCostSite))]
  [PXUIField]
  public int? CartID { get; set; }

  [PXDefault]
  [PXDimension("INLOCATION")]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<INCart.cartCD, Where<INCart.active, Equal<True>>>), DescriptionField = typeof (INCart.descr))]
  [PXCheckUnique(new Type[] {})]
  [PXFieldDescription]
  public 
  #nullable disable
  string CartCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public string Descr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? Active { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Assigned Number of Totes", Enabled = false)]
  public virtual int? AssignedNbrOfTotes { get; set; }

  [PXNote(DescriptionField = typeof (INCart.cartCD), Selector = typeof (INCart.cartCD))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>
  {
    public static INCart Find(PXGraph graph, int? siteID, int? cartID, PKFindOptions options = 0)
    {
      return (INCart) PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.FindBy(graph, (object) siteID, (object) cartID, options);
    }
  }

  public static class FK
  {
    public class Site : PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCart>.By<INCart.siteID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCart.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCart.cartID>
  {
  }

  public abstract class cartCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCart.cartCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCart.descr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INCart.active>
  {
  }

  public abstract class assignedNbrOfTotes : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCart.assignedNbrOfTotes>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCart.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCart.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCart.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCart.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCart.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCart.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCart.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCart.Tstamp>
  {
  }
}
