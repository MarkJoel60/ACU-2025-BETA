// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DAC.PXTablesSelectorForTrackingDeletionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Metadata;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Data.DeletedRecordsTracking.DAC;

internal class PXTablesSelectorForTrackingDeletionAttribute : PXCustomSelectorAttribute
{
  [InjectDependencyOnTypeLevel]
  private IDacRegistry DacRegistry { get; set; }

  public PXTablesSelectorForTrackingDeletionAttribute()
    : base(typeof (PXTablesSelectorForTrackingDeletionAttribute.SingleTable.fullName))
  {
  }

  internal IEnumerable GetRecords()
  {
    foreach (System.Type table in this.DacRegistry.Visible)
    {
      if (!table.IsDefined(typeof (PXProjectionAttribute), true) && PXDatabase.GetTableStructure(BqlCommand.GetTableName(table)) != null)
      {
        MemberInfo[] member = table.GetMember("NoteID");
        if (member != null && ((IEnumerable<MemberInfo>) member).Any<MemberInfo>())
        {
          PXTablesSelectorForTrackingDeletionAttribute.SingleTable record = new PXTablesSelectorForTrackingDeletionAttribute.SingleTable()
          {
            FullName = table.FullName,
            Name = table.Name
          };
          if (table.IsDefined(typeof (PXCacheNameAttribute), true))
          {
            PXCacheNameAttribute customAttribute = (PXCacheNameAttribute) table.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0];
            record.DisplayName = customAttribute.GetName();
          }
          yield return (object) record;
        }
      }
    }
  }

  [PXCacheName("Deleted Records Tracking Tables")]
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
      BqlType<IBqlString, string>.Field<PXTablesSelectorForTrackingDeletionAttribute.SingleTable.name>
    {
    }

    public abstract class displayName : 
      BqlType<IBqlString, string>.Field<PXTablesSelectorForTrackingDeletionAttribute.SingleTable.displayName>
    {
    }

    public abstract class fullName : 
      BqlType<IBqlString, string>.Field<PXTablesSelectorForTrackingDeletionAttribute.SingleTable.fullName>
    {
    }
  }
}
