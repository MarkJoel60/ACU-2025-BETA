// Decompiled with JetBrains decompiler
// Type: PX.TM.PXEMailResponseAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Data;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows you to specify an action to be processed once email is attached.
/// </summary>
/// <example>
/// <code title="Example" lang="CS">
/// [PXEMailResponse("ReOpenCase")]</code>
/// </example>
public class PXEMailResponseAttribute : PXEventSubscriberAttribute
{
  private readonly string _action;

  public PXEMailResponseAttribute(string action) => this._action = action;

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    System.Type declaringType = bqlTable.GetProperty(this._FieldName).DeclaringType;
  }

  public static string GetAction(System.Type type)
  {
    foreach (PropertyInfo property in type.GetProperties())
    {
      if (property.DeclaringType == type && property.IsDefined(typeof (PXEMailResponseAttribute), false))
        return ((PXEMailResponseAttribute[]) property.GetCustomAttributes(typeof (PXEMailResponseAttribute), true))[0]._action;
    }
    return (string) null;
  }

  public static IEnumerable<System.Type> Types
  {
    get
    {
      foreach (System.Type table in ServiceManager.Tables)
      {
        foreach (PropertyInfo property in table.GetProperties())
        {
          if (property.DeclaringType == table && property.IsDefined(typeof (PXEMailResponseAttribute), false))
          {
            yield return table;
            break;
          }
        }
      }
    }
  }
}
