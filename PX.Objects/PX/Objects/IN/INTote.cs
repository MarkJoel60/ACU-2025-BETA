// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTote
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INTote : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int UnassignedToteID = 0;

  [PXDBDefault(typeof (INSite.siteID))]
  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (INTote.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBIdentity]
  [PXUIField]
  public int? ToteID { get; set; }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<INTote.toteCD, Where<INTote.active, Equal<True>>>), DescriptionField = typeof (INTote.descr))]
  [PXCheckUnique(new Type[] {})]
  [PXFieldDescription]
  public 
  #nullable disable
  string ToteCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public string Descr { get; set; }

  [PXDBInt]
  [PXSelector(typeof (SearchFor<INCart.cartID>.In<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INCart.siteID, IBqlInt>.IsEqual<BqlField<INTote.siteID, IBqlInt>.FromCurrent>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXUIField(DisplayName = "Assigned Cart ID")]
  [PXForeignReference]
  [PXParent(typeof (INTote.FK.Cart), LeaveChildren = true)]
  [PXFormula(null, typeof (CountCalc<INCart.assignedNbrOfTotes>))]
  public int? AssignedCartID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? Active { get; set; }

  [PXNote(DescriptionField = typeof (INTote.toteCD), Selector = typeof (INTote.toteCD))]
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

  public class PK : PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteID>
  {
    public static INTote Find(PXGraph graph, int? siteID, int? toteID, PKFindOptions options = 0)
    {
      return (INTote) PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteID>.FindBy(graph, (object) siteID, (object) toteID, options);
    }
  }

  public class UK : PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteCD>
  {
    public static INTote Find(PXGraph graph, int? siteID, string toteCD, PKFindOptions options = 0)
    {
      return (INTote) PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteCD>.FindBy(graph, (object) siteID, (object) toteCD, options);
    }
  }

  public static class FK
  {
    public class Site : PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTote>.By<INTote.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<INTote>.By<INTote.siteID, INTote.assignedCartID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTote.siteID>
  {
  }

  public abstract class toteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTote.toteID>
  {
  }

  public abstract class toteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTote.toteCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTote.descr>
  {
  }

  public abstract class assignedCartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTote.assignedCartID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTote.active>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTote.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTote.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTote.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTote.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTote.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTote.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTote.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTote.Tstamp>
  {
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Tote ID")]
  [PXSelector(typeof (INTote.toteID), SubstituteKey = typeof (INTote.toteCD), DescriptionField = typeof (INTote.descr), ValidateValue = false)]
  public class UnassignableToteAttribute : PXEntityAttribute, IPXFieldSelectingSubscriber
  {
    protected virtual bool ChildrenAttributesComeFirstFor<ISubscriber>()
    {
      return typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber) || ((PXAggregateAttribute) this).ChildrenAttributesComeFirstFor<ISubscriber>();
    }

    void IPXFieldSelectingSubscriber.FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.ReturnValue != null)
      {
        if (e.ReturnValue is string returnValue)
        {
          string str = 0.ToString();
          if (returnValue == str)
            goto label_4;
        }
        if (!(e.ReturnValue is 0))
          return;
      }
label_4:
      e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("<UNASSIGNED>");
    }
  }
}
