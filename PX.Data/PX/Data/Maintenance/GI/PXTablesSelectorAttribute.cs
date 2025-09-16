// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.PXTablesSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Maintenance.GI;

public class PXTablesSelectorAttribute : PXCustomSelectorAttribute
{
  [InjectDependencyOnTypeLevel]
  private 
  #nullable disable
  IDacRegistry DacRegistry { get; set; }

  [InjectDependencyOnTypeLevel]
  private IEnumerable<IPXSchemaTableRestrictor> Restrictors { get; set; }

  [InjectDependencyOnTypeLevel]
  private ObsoleteTablesRestrictor ObsoleteTablesRestrictor { get; set; }

  public PXTablesSelectorAttribute()
    : base(typeof (PXTablesSelectorAttribute.SingleTable.fullName))
  {
  }

  protected PXTablesSelectorAttribute(System.Type type)
    : base(type)
  {
  }

  internal virtual IEnumerable GetRecords()
  {
    foreach (System.Type type in this.DacRegistry.Visible)
    {
      System.Type t = type;
      string str = (string) null;
      if (!this.Restrictors.Append<IPXSchemaTableRestrictor>((IPXSchemaTableRestrictor) this.ObsoleteTablesRestrictor).Any<IPXSchemaTableRestrictor>((Func<IPXSchemaTableRestrictor, bool>) (r => !r.IsAllowed(t))))
      {
        if (t.IsDefined(typeof (PXCacheNameAttribute), true))
          str = ((PXNameAttribute) t.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
        yield return (object) new PXTablesSelectorAttribute.SingleTable()
        {
          FullName = t.FullName,
          Name = t.Name,
          DisplayName = str
        };
      }
    }
  }

  [Serializable]
  public class SingleTable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(256 /*0x0100*/, InputMask = "")]
    [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
    public string Name { get; set; }

    [PXString(256 /*0x0100*/, InputMask = "")]
    [PXUIField(DisplayName = "Display Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
    public string DisplayName { get; set; }

    [PXString(512 /*0x0200*/, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Full Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
    public string FullName { get; set; }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXTablesSelectorAttribute.SingleTable.name>
    {
    }

    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXTablesSelectorAttribute.SingleTable.displayName>
    {
    }

    public abstract class fullName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXTablesSelectorAttribute.SingleTable.fullName>
    {
    }
  }
}
