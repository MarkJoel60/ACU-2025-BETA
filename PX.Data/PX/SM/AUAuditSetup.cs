// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Process;
using System;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (AUAuditMaintenance))]
[Serializable]
public class AUAuditSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Audited Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsActive { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  [AUAuditSetup.showFieldsType.List]
  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Show Fields")]
  public virtual int? ShowFieldsType { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXUIField(DisplayName = "Screen Name")]
  [PXString]
  public virtual string ScreenName
  {
    get
    {
      return string.Join(", ", PXAuditHelper.GetAuditedScreenIDs(this.ScreenID).Select<string, string>((Func<string, string>) (s => PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(s)?.Title)).Where<string>((Func<string, bool>) (t => !string.IsNullOrEmpty(t))));
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Screen ID", Enabled = false)]
  public virtual string VirtualScreenID
  {
    get
    {
      return string.Join(", ", PXAuditHelper.GetAuditedScreenIDs(this.ScreenID).Select<string, string>((Func<string, string>) (s => Mask.Format(">CC.CC.CC.CC", s))));
    }
  }

  public enum FieldType
  {
    AllFields,
    UIFields,
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditSetup.screenID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUAuditSetup.isActive>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditSetup.description>
  {
  }

  public abstract class showFieldsType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUAuditSetup.showFieldsType>
  {
    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new int[2]{ 0, 1 }, new string[2]
        {
          "All Fields",
          "UI Fields"
        })
      {
      }

      public class allFields : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Constant<
        #nullable disable
        AUAuditSetup.showFieldsType.ListAttribute.allFields>
      {
        public allFields()
          : base(0)
        {
        }
      }

      public class uIFields : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Constant<
        #nullable disable
        AUAuditSetup.showFieldsType.ListAttribute.uIFields>
      {
        public uIFields()
          : base(1)
        {
        }
      }
    }
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUAuditSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAuditSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUAuditSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUAuditSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAuditSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUAuditSetup.lastModifiedDateTime>
  {
  }

  public abstract class screenName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditSetup.screenName>
  {
  }

  public abstract class virtualScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAuditSetup.virtualScreenID>
  {
  }
}
