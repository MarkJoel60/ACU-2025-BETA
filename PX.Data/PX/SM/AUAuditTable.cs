// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUAuditTable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Screen Name")]
  [PXParent(typeof (Select<AUAuditSetup, Where<AUAuditSetup.screenID, Equal<Current<AUAuditTable.screenID>>>>))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(100, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Table", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string TableName { get; set; }

  [AUAuditTable.showFieldsType.List]
  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Show Fields")]
  public virtual int? ShowFieldsType { get; set; }

  [PXDBString(512 /*0x0200*/)]
  public virtual string TableType { get; set; }

  [PXDBString(256 /*0x0100*/)]
  public virtual string Keys { get; set; }

  [PXBool]
  public virtual bool? IsInserted { get; set; }

  [PXString(100)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string TableDisplayName { get; set; }

  public enum FieldType
  {
    AllFields,
    UIFields,
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUAuditTable.isActive>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditTable.screenID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditTable.tableName>
  {
  }

  public abstract class showFieldsType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUAuditTable.showFieldsType>
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
        AUAuditTable.showFieldsType.ListAttribute.allFields>
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
        AUAuditTable.showFieldsType.ListAttribute.uIFields>
      {
        public uIFields()
          : base(1)
        {
        }
      }
    }
  }

  public abstract class tableType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditTable.tableType>
  {
  }

  public abstract class keys : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditTable.keys>
  {
  }

  public abstract class isInserted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUAuditTable.isInserted>
  {
  }

  public abstract class tableDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAuditTable.tableDisplayName>
  {
  }
}
